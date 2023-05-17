using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subnetor.Models
{
    public class VLSM_Info_Model
    {
        public VLSM_Info_Model()
        {
            Name = "Subnet";
            RequiredSize = 0;
            AllocatedSize = 0;
            Address = "0.0.0.0";
            Mask = "0.0.0.0";
            DecMask = "0";
            AssignableRange = "0.0.0.0 - 0.0.0.0";
            Broadcast = "0.0.0.0";
        }

        public string Name { get; set; }
        public int RequiredSize { get; set; }
        public int AllocatedSize { get; set; }
        public string Address { get; set; }
        public string Mask { get; set; }
        public string DecMask { get; set; }
        public string AssignableRange { get; set; }
        public string Broadcast { get; set; }
    }
}