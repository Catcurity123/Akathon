using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Controllers
{
    public class AstraDBController : Controller
    {
        // GET: AstraDB
        public ActionResult Index()
        {
            var model = new AstraDBModel();
            var data = model.GetData();
            model.Close();
            return View(data);
        }
    }
}