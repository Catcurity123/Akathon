using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Subnetor.Models;

namespace Subnetor.Controllers
{
    public class SubnetCalculatorController : Controller
    {
        public ActionResult Index()
        {
            return View(new IPViewModel());
        }

        [HttpPost]
        public ActionResult Calculate(IPViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Parse IP address input
                IPAddress ipAddress;
                if (!IPAddress.TryParse(model.IPAddress, out ipAddress))
                {
                    ModelState.AddModelError("IPAddress", "Invalid IP address");
                    return View("Index", model);
                }

                // Parse prefix length input
                int prefixLength;
                if (!int.TryParse(model.PrefixLength.ToString(), out prefixLength))
                {
                    ModelState.AddModelError("PrefixLength", "Invalid prefix length");
                    return View("Index", model);
                }

                // Calculate subnet information
                uint neededSize = model.NeededSize;
                var subnetInfo = SubnetCalculatorModel.Calculate(ipAddress, prefixLength, neededSize);
                System.Diagnostics.Debug.WriteLine($"Allocated Size: {subnetInfo.AllocatedSize}");
                // Check if the needed size is greater than the available subnet size
                if (subnetInfo == null)
                {
                    ModelState.AddModelError("NeededSize", "The needed size is greater than the available subnet size");
                    return View("Index", model);
                }

                // Update model with subnet information
                model.SubnetMask = subnetInfo.SubnetMask.ToString();
                model.NetworkAddress = subnetInfo.NetworkAddress.ToString();
                model.BroadcastAddress = subnetInfo.BroadcastAddress.ToString();
                model.NumHosts = subnetInfo.NumHosts;
                model.StartAddress = subnetInfo.StartAddress.ToString();
                model.EndAddress = subnetInfo.EndAddress.ToString();
                model.NumAddresses = subnetInfo.NumAddresses;
                model.NeededSize = neededSize;
                model.AllocatedSize = subnetInfo.AllocatedSize;
                model.NewSubnetMask = subnetInfo.NewSubnetMask;
                string SubnetMask = model.NetworkAddress.ToString() + " /" + model.NewSubnetMask.ToString();

                // Insert the data into Astra DB
                var session = AstraDB_Connection.GetSession();
                var query = $"INSERT INTO Subnettor (subnet_mask, needed_size, allocated_size) VALUES ('{SubnetMask}', {(int)model.NeededSize}, {(int)model.AllocatedSize})";
                session.Execute(query);
                AstraDB_Connection.CloseSession();

                

                return View("Index", model);
            }
            else
            {
                return View("Index", model);
            }
        }


    }
}