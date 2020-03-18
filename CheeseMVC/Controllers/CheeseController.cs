using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private bool invalidInput = false;

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();
            invalidInput = false;

            return View(cheeses);
        }

        public IActionResult Add()
        {
            ViewBag.isInvalidInput = invalidInput;
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese newCheese = addCheeseViewModel.CreateCheese();
                CheeseData.Add(newCheese);
                return Redirect("/Cheese");
            }
            return View(addCheeseViewModel);
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
            Cheese cheeseToEdit = CheeseData.GetById(cheeseId);
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel
            {
                Name = cheeseToEdit.Name,
                Description = cheeseToEdit.Description,
                Type = cheeseToEdit.Type,
                Rating = cheeseToEdit.Rating,
                CheeseId = cheeseId
            };
            return View(editCheeseViewModel);
        }

        [HttpPost]
        //public IActionResult Edit(int cheeseId, string name, string description)
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese cheeseToEdit = CheeseData.GetById(editCheeseViewModel.CheeseId);
                editCheeseViewModel.EditCheese(cheeseToEdit);

                return Redirect("/Cheese");
            }
            return View(editCheeseViewModel);
        }
    }
}
