using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipePuppy.Models;

namespace RecipePuppy.Controllers
{
    public class HomeController : Controller
    {
        RecipeDBEntities db = new RecipeDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecipeList(string ing1, string ing2, int num)
        {
            List<Recipe> Recipes = RecipeAPIDAL.GetNewRecipe(ing1, ing2, num);

            return View(Recipes);
        }

        public ActionResult Ingredients()
        {
            return View();
        }

        public ActionResult Register(string UserName, string Password, string Name, string Email)
        {
            ViewBag.UserName = UserName;
            ViewBag.Password = Password;
            ViewBag.Name = Name;
            ViewBag.Email = Email;

            Session["User"] = User;

                return View();
        }
    }
}