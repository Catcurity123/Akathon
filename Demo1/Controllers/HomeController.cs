using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calculator.Models;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
        public ActionResult Calculator()
        {

            return View(new CalculatorModel());
        }
        [HttpPost]
        public ActionResult Calculator(CalculatorModel model)
        {
            model.Calculate();

            return View("Calculator", model);
        }

    }
}