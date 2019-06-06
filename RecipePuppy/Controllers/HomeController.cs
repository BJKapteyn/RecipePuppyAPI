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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecipeList(string ing1, string ing2, int num)
        {
            List<Recipe> Recipes = RecipeAPIDAL.GetNewRecipe(ing1, ing2, num);

            return View(Recipes);
        }


        public ActionResult RecipeFavorite(string title, string image, string ingredients, string thumbNail)
        {
            User u = (User)Session["User"];
            Favorite fav = new Favorite();
            fav.Title = title;
            fav.Image = image;
            fav.Ingredients = ingredients;
            fav.Thumbnail = thumbNail;
            fav.FavUserID = u.UserID;
            fav.User = u;
            db.Favorites.Add(fav);
            return RedirectToAction("RecipeList");
        }
    }
}