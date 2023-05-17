using Subnetor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace Subnetor.Controllers
{
    public class VLSMCalculatorController : Controller
    {
        // GET: VLSMCalculator
        public ActionResult Index()
        {
            var model = new List<VLSM_Info_Model>
            {

            };
            return View(model);
        }

        // POST: VLSMCalculator/Calculate
        [HttpPost]
        public ActionResult Calculate(string ip, string mask, int subnets, List<int> subnetHosts)
        {
            return null;
        }
    }
}