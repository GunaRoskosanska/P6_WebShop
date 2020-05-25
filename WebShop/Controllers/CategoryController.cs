using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Logic;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult List()
        {
            if(!HttpContext.Session.GetIsAdmin())
            {
                //return RedirectToAction("Index", "Home"); vai 2.variants (droshaks) -
                return NotFound();
            }
            var categories = CategoryManager.GetAll().Select(c => c.ToModel()).ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!HttpContext.Session.GetIsAdmin())
            {
                return NotFound();
            }
            var model = new CategoryModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ParentId.HasValue)
                {
                    var category = CategoryManager.Get(model.ParentId.Value);
                    if (category == null)
                    {
                        ModelState.AddModelError("cat", "Category not found!");
                        return View(model);
                    }
                }

                CategoryManager.Create(model.Name, model.ParentId);
                return RedirectToAction(nameof(List));
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!HttpContext.Session.GetIsAdmin())
            {
                return NotFound();
            }

            CategoryManager.Delete(id);

            return RedirectToAction(nameof(List));
        }
    }
}