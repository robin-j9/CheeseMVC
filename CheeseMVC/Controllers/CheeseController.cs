using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private Dictionary<string, string> Cheeses = new Dictionary<string, string>();
        static private Boolean invalidInput = false;

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;
            invalidInput = false;

            return View();
        }

        public IActionResult Add()
        {
            ViewBag.isInvalidInput = invalidInput;
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            Regex alpha = new Regex("^[a-zA-Z\\s]+$");
            if(alpha.IsMatch(name))
            {
                Cheeses.Add(name, description);
                return Redirect("/Cheese");
            }
            else
            {
                invalidInput = true;
                return Redirect("/Cheese/Add");
            }           
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
