using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subnetor.Models
{
    public class NetworkInfo
    {
        public string SubnetMask { get; set; }
        public int NeededSize { get; set; }
        public int AllocatedSize { get; set; }
        public double PercentageUsage { get; set; }

        public string Visualization { get; set; }
    }
}