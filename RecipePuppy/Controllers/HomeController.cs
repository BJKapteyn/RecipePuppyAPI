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

        [HttpPost]
        public ActionResult RecipeFavorite(Recipe r)
        {
            //User user = (User)Session["User"];
           // Favorite f = new Favorite(r);

            //db.Favorites.Add(f);
            //db.SaveChanges();

            return RedirectToAction("RecipeList");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNewUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();

            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
           
            User user = db.Users.Where(login => login.UserName == u.UserName && login.Password == u.Password).ToList().First();

            Session["User"] = (User)user;

            return View("RecipeList");
        }


    }
}