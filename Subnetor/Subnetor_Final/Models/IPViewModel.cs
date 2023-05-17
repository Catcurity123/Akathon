using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace Subnetor.Models
{
    public class IPViewModel
    {
        public IPViewModel()
        {
            IPAddress = "0.0.0.0";
            SubnetMask = "0.0.0.0";
            NetworkAddress = "0.0.0.0";
            BroadcastAddress = "0.0.0.0";
            NumHosts = 0;
            StartAddress = "0.0.0.0";
            EndAddress = "0.0.0.0";
            NumAddresses = 0;
            PrefixLength = 0;
            NeededSize = 0;
            AllocatedSize = 0;
            NewSubnetMask = 0;
        }

        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Invalid IP address")]
        public string IPAddress { get; set; }

        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Invalid subnet mask")]
        public string SubnetMask { get; set; }
        public uint NeededSize { get; set; }

        public string NetworkAddress { get; set; }
        public string BroadcastAddress { get; set; }
        public uint NumHosts { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public uint NumAddresses { get; set; }
        public int PrefixLength { get; set; }
        public uint AllocatedSize { get; set; }
        public int NewSubnetMask { get; set; }  
    }
}