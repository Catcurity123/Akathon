using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;

namespace Subnetor.Models
{
    public class SubnetCalculatorModel
    {
        public static SubnetInfo Calculate(IPAddress ipAddress, int prefixLength, uint neededSize)
        {
            var subnetMask = CalculateSubnetMask(prefixLength);
            var networkAddress = CalculateNetworkAddress(ipAddress, subnetMask);
            var broadcastAddress = CalculateBroadcastAddress(networkAddress, subnetMask);
            var numHosts = CalculateNumHosts(prefixLength);
            var startAddress = IncrementIPAddress(networkAddress);
            var numAddresses = numHosts + 2;
            var allocatedSize = CalculateAllocatedSize(neededSize, prefixLength);

            //Calculate the endaddress
            var endAddressBytes = new byte[4];
            Array.Copy(startAddress.GetAddressBytes(), endAddressBytes, 4);
            var allocatedSizeBytes = BitConverter.GetBytes(allocatedSize - 1);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(allocatedSizeBytes);
            }
            for (var i = 3; i >= 0; i--)
            {
                endAddressBytes[i] += allocatedSizeBytes[i];
                if (endAddressBytes[i] > 255)
                {
                    if (i > 0)
                    {
                        endAddressBytes[i - 1]++;
                    }
                    endAddressBytes[i] = (byte)(endAddressBytes[i] - 256);
                }
            }
            var endAddress = new IPAddress(endAddressBytes);

            var newSubnetMask = CalculateNewSubnetMask(neededSize, prefixLength);

            return new SubnetInfo(
                subnetMask,
                networkAddress,
                broadcastAddress,
                numHosts,
                startAddress,
                endAddress,
                numAddresses,
                neededSize,
                allocatedSize,
                newSubnetMask);
            
        }

        private static IPAddress CalculateSubnetMask(int prefixLength)
        {
            var binaryMask = new string('1', prefixLength).PadRight(32, '0');
            var subnetMask = new IPAddress(
                Enumerable.Range(0, 4)
                          .Select(i => Convert.ToByte(binaryMask.Substring(i * 8, 8), 2))
                          .ToArray());

            return subnetMask;
        }

        private static IPAddress CalculateNetworkAddress(IPAddress ipAddress, IPAddress subnetMask)
        {
            var ipAddressBytes = ipAddress.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();
            var networkAddressBytes = new byte[4];

            for (var i = 0; i < 4; i++)
            {
                networkAddressBytes[i] = (byte)(ipAddressBytes[i] & subnetMaskBytes[i]);
            }

            return new IPAddress(networkAddressBytes);
        }

        private static IPAddress CalculateBroadcastAddress(IPAddress networkAddress, IPAddress subnetMask)
        {
            var networkAddressBytes = networkAddress.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();
            var broadcastAddressBytes = new byte[4];

            for (var i = 0; i < 4; i++)
            {
                broadcastAddressBytes[i] = (byte)(networkAddressBytes[i] | ~subnetMaskBytes[i]);
            }

            return new IPAddress(broadcastAddressBytes);
        }

        private static uint CalculateNumHosts(int prefixLength)
        {
            return (uint)Math.Pow(2, 32 - prefixLength) - 2;
        }

        private static int CalculateNewSubnetMask(uint neededSize, int prefixLength)
        {

            // calculate the number of host bits required to accommodate the needed size
            var numHostBitsRequired = (int)Math.Ceiling(Math.Log(neededSize, 2));

            // calculate the number of bits required to represent the maximum number of hosts
            var numMaxHostBits = 32 - prefixLength;

            // calculate the number of additional subnet bits required
            var numSubnetBitsRequired = Math.Max(0, numMaxHostBits - numHostBitsRequired);

            // calculate the new prefix length for the subnet
            var newPrefixLength = prefixLength + numSubnetBitsRequired;

            return newPrefixLength;
        }

        private static uint CalculateAllocatedSize(uint neededSize, int prefixLength)
        {

            // calculate the number of host bits required to accommodate the needed size
            var numHostBitsRequired = (int)Math.Ceiling(Math.Log(neededSize, 2));

            // calculate the number of bits required to represent the maximum number of hosts
            var numMaxHostBits = 32 - prefixLength;

            // calculate the number of additional subnet bits required
            var numSubnetBitsRequired = Math.Max(0, numMaxHostBits - numHostBitsRequired);

            // calculate the new prefix length for the subnet
            var newPrefixLength = prefixLength + numSubnetBitsRequired;

            // calculate the number of addresses in the subnet
            var numAddresses = (uint)Math.Pow(2, 32 - newPrefixLength);

            if (neededSize > numAddresses)
            {
                throw new ArgumentException($"The requested size ({neededSize}) is larger than the number of available addresses ({numAddresses}).");
            }
            // check if the subnet has enough addresses
            if (numAddresses < neededSize + 2)
            {
                // if not, add an additional subnet bit
                newPrefixLength++;
            }

            // calculate the allocated size based on the new prefix length
            var allocatedSize = (uint)Math.Pow(2, 32 - newPrefixLength);
            return allocatedSize - 2;
        }

        private static IPAddress IncrementIPAddress(IPAddress ipAddress)
        {
            var ipAddressBytes = ipAddress.GetAddressBytes();
            var incrementedAddressBytes = new byte[4];
            Array.Copy(ipAddressBytes, incrementedAddressBytes, 4);

            for (var i = 3; i >= 0; i--)
            {
                if (incrementedAddressBytes[i] == 255)
                {
                    incrementedAddressBytes[i] = 0;
                }
                else
                {
                    incrementedAddressBytes[i]++;
                    break;
                }
            }

            return new IPAddress(incrementedAddressBytes);
        }

        private static IPAddress DecrementIPAddress(IPAddress ipAddress)
        {
            var ipAddressBytes = ipAddress.GetAddressBytes();
            var decrementedAddressBytes = new byte[4];
            Array.Copy(ipAddressBytes, decrementedAddressBytes, 4);

            for (var i = 3; i >= 0; i--)
            {
                if (decrementedAddressBytes[i] == 0)
                {
                    decrementedAddressBytes[i] = 255;
                }
                else
                {
                    decrementedAddressBytes[i]--;
                    break;
                }
            }

            return new IPAddress(decrementedAddressBytes);
        }
    }

    public class SubnetInfo
    {
        public IPAddress SubnetMask { get; set; }
        public IPAddress NetworkAddress { get; set; }
        public IPAddress BroadcastAddress { get; set; }
        public uint NumHosts { get; set; }
        public IPAddress StartAddress { get; set; }
        public IPAddress EndAddress { get; set; }
        public uint NumAddresses { get; set; }
        public uint NeededSize { get; set; }
        public uint AllocatedSize { get; set; }
        public int NewSubnetMask { get; set; }

        public SubnetInfo(IPAddress subnetMask, IPAddress networkAddress, IPAddress broadcastAddress, uint numHosts, IPAddress startAddress, IPAddress endAddress, uint numAddresses, uint neededSize, uint allocatedSize, int newSubnetMask)
        {
            SubnetMask = subnetMask;
            NetworkAddress = networkAddress;
            BroadcastAddress = broadcastAddress;
            NumHosts = numHosts;
            StartAddress = startAddress;
            EndAddress = endAddress;
            NumAddresses = numAddresses;
            NeededSize = neededSize;
            AllocatedSize = allocatedSize;
            NewSubnetMask = newSubnetMask;
        }
    }
}