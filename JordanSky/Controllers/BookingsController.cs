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
    public class BookingsController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Bookings
        public ActionResult Index()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var bookings = db.Bookings.Include(b => b.mazr3);
                return View(bookings.ToList());
            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Booking booking = db.Bookings.Find(id);
                if (booking == null)
                {
                    return HttpNotFound();
                }
                return View(booking);
            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
      
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                ViewBag.Mazra3a_id = new SelectList(db.Mazrs, "Id", "Name");
                return View();
            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
         
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,StartDate,EndDate,Details,Status,Mazra3a_id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Mazra3a_id = new SelectList(db.Mazrs, "Id", "Name", booking.Mazra3a_id);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Booking booking = db.Bookings.Find(id);
                if (booking == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Mazra3a_id = new SelectList(db.Mazrs, "Id", "Ref_No", booking.Mazra3a_id);
                return View(booking);

            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
      
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,StartDate,EndDate,Details,Status,Mazra3a_id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Mazra3a_id = new SelectList(db.Mazrs, "Id", "Name", booking.Mazra3a_id);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true) {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Booking booking = db.Bookings.Find(id);
                if (booking == null)
                {
                    return HttpNotFound();
                }
                return View(booking);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
