using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using CheeseMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private bool invalidInput = false;

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = CheeseData.GetAll();
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
        public IActionResult NewCheese(Cheese newCheese)
        {
            Regex alpha = new Regex("^[a-zA-Z\\s]+$");
            if(alpha.IsMatch(newCheese.Name))
            {
                CheeseData.Add(newCheese);
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
            ViewBag.title = "Remove Cheese";
            ViewBag.cheeseList = CheeseData.GetAll();

            return View();
        }
        
        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult CheeseRemoval(int[] cheeseIds)
        {
            foreach(int id in cheeseIds)
            {
                CheeseData.Remove(id);
            }
            return Redirect("/Cheese");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheeseToEdit = CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int cheeseId, string name, string description)
        {
            Cheese cheeseToEdit = CheeseData.GetById(cheeseId);
            cheeseToEdit.Name = name;
            cheeseToEdit.Description = description;

            return Redirect("/Cheese");
        }
    }
}
