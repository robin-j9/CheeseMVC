using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Only alphabetic characters and spaces allowed.")]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "You must give your cheese a description.")]
        public string Description { get; set; }

        public CheeseType Type { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public List<SelectListItem> CheeseTypes { get; set; }

        public AddCheeseViewModel()
        {
            CheeseTypes = new List<SelectListItem>();

            var types = Enum.GetValues(typeof(CheeseType));

            // <option value="0">Hard</option>
            foreach (var t in types)
            {
                CheeseTypes.Add(new SelectListItem
                {
                    Value = ((int) t).ToString(),
                    Text = t.ToString()
                });
            }
            //CheeseTypes.Add(new SelectListItem {
            //    Value = ((int) CheeseType.Hard).ToString(),
            //    Text = CheeseType.Hard.ToString()
            //});

        }

        public Cheese CreateCheese()
        {
            return new Cheese
            {
                Name = Name,
                Description = Description,
                Type = Type,
                Rating = Rating
            };
        }

        public void EditCheese(Cheese cheese)
        {
            cheese.Name = Name;
            cheese.Description = Description;
            cheese.Type = Type;
            cheese.Rating = Rating;
        }
    }
}
