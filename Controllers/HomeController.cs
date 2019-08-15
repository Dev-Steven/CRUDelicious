using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // Inject context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes
            .OrderByDescending(x => x.created_at)
            .ToList();

            return View("Index", AllDishes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                var lastDish = dbContext.Dishes.Last();
                lastDish.created_at = DateTime.Now;
                lastDish.updated_at = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Index"); 
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Dish(int id)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(d => d.id == id);

            return View("Dish", RetrievedDish);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(d => d.id == id);
            dbContext.Dishes.Remove(RetrievedDish);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(d => d.id == id);

            return View("Edit", RetrievedDish);
        }

        [HttpPost("edit")]
        public IActionResult Change(Dish editDish, int id)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(d => d.id == id);
            if(ModelState.IsValid)
            {
                
                RetrievedDish.chef_name = editDish.chef_name;
                RetrievedDish.dish_name = editDish.dish_name;
                RetrievedDish.calories = editDish.calories;
                RetrievedDish.tastiness = editDish.tastiness;
                RetrievedDish.description = editDish.description;
                RetrievedDish.updated_at = DateTime.Now;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", RetrievedDish);
            }
           
        }
    }
}
