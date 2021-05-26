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
    public class UsersController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Users
        public ActionResult Employee()
        {
            if (Convert.ToBoolean( Session["Check_User"])==true) {
                var users = db.Users.Where(u => u.Type_id != 1).Include(u => u.type_User);
                return View(users.ToList());
            }
            Session["Check_User"]= false; 
            return Redirect("~/Errors/error_404.html");
            
        }

        // GET: Users/Details/5
       

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                ViewBag.Type_id = new SelectList(db.Type_Users, "id", "Type");
                return View();
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
         
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Type_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Employee");
            }

            ViewBag.Type_id = new SelectList(db.Type_Users, "id", "Type", user.Type_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true) { 
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_id = new SelectList(db.Type_Users, "id", "Type", user.Type_id);
            return View(user);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Type_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_id = new SelectList(db.Type_Users, "id", "Type", user.Type_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    User user = db.Users.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                
            }
             Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    
    }
}
