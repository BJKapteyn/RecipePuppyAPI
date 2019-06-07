﻿using System;
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

        public ActionResult RecipeList(string ing1, string ing2/*, int num*/)
        {
            List<Recipe> Recipes = RecipeAPIDAL.GetNewRecipe(ing1, ing2/*, num*/);

            return View(Recipes);
        }
        public ActionResult Ingredients() 
        {

          return View();
        }
        public ActionResult RecipeFavorite(string title, string image, string ingredients, string thumbNail)
        {
            User u = (User)Session["User"];
            User favU = db.Users.Where(login => login.UserID == u.UserID).ToList().First();
            Favorite fav = new Favorite();
            //favU.Email = u.Email;
            //favU.Favorites = u.Favorites;
            //favU.Password = u.Password;
            //favU.UserName = u.UserName;
            //favU.UserID = u.UserID;
            //favU.Name = u.Name;
            fav.Title = title;
            fav.Image = image;
            fav.Ingredients = ingredients;
            fav.Thumbnail = thumbNail;
            fav.FavUserID = u.UserID;
            fav.User = favU;
            
            db.Favorites.Add(fav);
            db.SaveChanges();
            return RedirectToAction("FavoriteList");
        }

        public ActionResult FavoriteList()
        {
            User u = (User)Session["User"];
            List<Favorite> favs = db.Favorites.Where(fav => fav.FavUserID == u.UserID).ToList();


            return View(favs);
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
            List < User > users = db.Users.ToList();
            User user = new User();
            for (int i = 0; i < users.Count; i++)
            {
                if (u.UserName == users[i].UserName)
                {
                    if (u.Password == users[i].Password)
                    {
                        Session["UserError"] = "";
                        user = users[i];
                        Session["User"] = user;
                    }
                    else
                    {
                        Session["UserError"] = "Incorrect username or password";
                        return RedirectToAction("~/Home/Login");
                    }
                }
                else
                {
                    Session["UserError"] = "Incorrect username or password";
                    return RedirectToAction("Login");
                }
            }
            //User user = db.Users.Where(login => login.UserName == u.UserName && login.Password == u.Password).ToList().First();
            //Session["User"] = (User)user;

            return View("Ingredients");
        }


    }
}