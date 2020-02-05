﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        // static private List<string> Cheeses = new List<string>();
        static private Dictionary<string, string> Cheeses = new Dictionary<string, string>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            // Add the new cheese to existing cheeses
            Cheeses.Add(name, description);

            return Redirect("/Cheese");
        }

        public IActionResult Remove()
        {
            ViewBag.cheeseList = Cheeses;

            return View();
        }
        
        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult CheeseRemoval(string[] cheeseSelection)
        {
            foreach(string cheese in cheeseSelection)
            {
                Cheeses.Remove(cheese);
            }
            return Redirect("/Cheese");
        }
    }
}
