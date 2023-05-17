using Subnetor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subnetor.Controllers
{
    public class SubnetInfoController : Controller
    {
        public ActionResult Index()
        {
            var session = AstraDB_Connection.GetSession();
            var query = "SELECT subnet_mask, needed_size, allocated_size FROM Subnettor";
            var resultSet = session.Execute(query);
            var subnetInfos = new List<NetworkInfo>();

            foreach (var row in resultSet)
            {
                var neededSize = row.GetValue<int>("needed_size");
                var allocatedSize = row.GetValue<int>("allocated_size");
                var ratio = (double)neededSize / allocatedSize;
                var visualization = $"<svg id='subnet-{subnetInfos.Count}' class='subnet' width='200' height='30'><rect class='allocated-rect' x='0' y='5' width='200' height='20' fill='#e0e0e0' rx='5' ry='5' /><rect class='needed-rect' x='0' y='5' width='{ratio * 200}' height='20' fill='#ff6f69' rx='5' ry='5' /></svg>";

                var subnetInfo = new NetworkInfo
                {
                    SubnetMask = row.GetValue<string>("subnet_mask"),
                    NeededSize = neededSize,
                    AllocatedSize = allocatedSize,
                    Visualization = visualization,
                    PercentageUsage = ((double)neededSize / allocatedSize) * 100
                };
                subnetInfos.Add(subnetInfo);
            }

            AstraDB_Connection.CloseSession();
            return View(subnetInfos);
        }
    }
}