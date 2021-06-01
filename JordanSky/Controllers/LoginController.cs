using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JordanSky.Context;
using JordanSky.Entity;

namespace JordanSky.Controllers
{
    public class LoginController : Controller
    {
       
        private JordanSkyContext eco = new JordanSkyContext();
        // GET: Login
        public ActionResult Login()
        {
            Session["Check_User"] = false;
            return View();
        }
        
        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,Password")] User user)
        {
            

            var db = eco.Users.FirstOrDefault(s => s.Username == user.Username && s.Password == user.Password);
            if (db != null)
            {
                Session["Check_User"] = true;
                Session["user"] = db.Name;
                return RedirectToAction("Home", "Facilty");
            }
            else
            {
                ViewBag.error = "Error in username or password";
                return View();
            }
        }
        public ActionResult SignOut()
        {
            Session.Clear();
            Session["Check_User"] = false;
            return RedirectToAction("Login");
        }

    }
}
