using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Logic;
using WebShop.Logic.DB;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult SignUp()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            if(ModelState.IsValid)
            {
                // parbaudes - vai paroles sakrit?
                // vai lietotajs ar e-pastu jau neeksiste?
                if(model.Password != model.PasswordRepeat)
                {
                    ModelState.AddModelError("pass", "Passwords do not match!");
                }
                else
                {
                    //TODO: lietotaja atlase no DB pec e-pasta, izmantojot UserManager
                    // jabut rindai, kura mekle lietotaju pec e-pasta!!!!!
                    UserModel user = UserManager.GetByEmail(model.Email).ToModel();

                    if(user != null)
                    {
                        ModelState.AddModelError("mail", "User with this e-mail already exists!");
                    }
                    else
                    {
                        //TODO: saglabat ievaditos datus DB, izmantojot UserManager.
                        // rindai jabut par datu saglabasanu DB!!!
                        UserManager.Create(model.Email, model.Name, model.Password);

                        return RedirectToAction(nameof(SignIn));
                    }
                }
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult SignIn()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult SignIn(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                UserModel user = UserManager.GetByEmailAndPassword(model.Email, model.Password).ToModel();

                if(user == null)
                {
                    ModelState.AddModelError("user", "Invalid e-mail/password!");
                }
                else
                {
                    HttpContext.Session.SetUserName(user.Name);
                    HttpContext.Session.SetUserId(user.Id);
                    HttpContext.Session.SetIsAdmin(user.IsAdmin);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult MyCart()
        {
            var userCart = UserCartManager.GetByUser(HttpContext.Session.GetUserId());
            // attēlošanai nepieciešamas tikai preces
            var items = userCart.Select(c => c.Item.ToModel()).ToList();

            var distinctItems = items.GroupBy(x => x.Id).Select(group => {
                var item = group.First();
                item.Count = group.Count();
                return item;
            }).ToList();

            return View(distinctItems);
        }

        public IActionResult Confirm()

        {
            UserCartManager.ConfirmOrderAndDeleteItemsFromCart(HttpContext.Session.GetUserId());

            return View();
        }
    }
}
