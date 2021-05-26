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
    public class Internal_PackageController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Internal_Package
        public ActionResult Packages()
        {
            var packages = db.Packages.Include(i => i._Package);
            return View(packages.ToList());
        }

        // GET: Internal_Package/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Internal_Package internal_Package = db.Packages.Find(id);
            if (internal_Package == null)
            {
                return HttpNotFound();
            }
            return View(internal_Package);
        }

        // GET: Internal_Package/Create
        public ActionResult Create()
        {
            ViewBag.Type_id = new SelectList(db.Type_Packges, "Id", "Name");
            return View();
        }

        // POST: Internal_Package/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name_Pakage,Type_id,Phone,Ref_No,Price,Picture,Child_Price,Note_price,Transportation,overnight_Stay,overnight_Stay_name,Food,name_restaurant,No_Day,StartDate,EndDate,Description,Status")] Internal_Package internal_Package)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(internal_Package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Type_id = new SelectList(db.Type_Packges, "Id", "Name", internal_Package.Type_id);
            return View(internal_Package);
        }

        // GET: Internal_Package/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Internal_Package internal_Package = db.Packages.Find(id);
            if (internal_Package == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_id = new SelectList(db.Type_Packges, "Id", "Name", internal_Package.Type_id);
            return View(internal_Package);
        }

        // POST: Internal_Package/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name_Pakage,Type_id,Phone,Ref_No,Price,Picture,Child_Price,Note_price,Transportation,overnight_Stay,overnight_Stay_name,Food,name_restaurant,No_Day,StartDate,EndDate,Description,Status")] Internal_Package internal_Package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(internal_Package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_id = new SelectList(db.Type_Packges, "Id", "Name", internal_Package.Type_id);
            return View(internal_Package);
        }

        // GET: Internal_Package/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Internal_Package internal_Package = db.Packages.Find(id);
            if (internal_Package == null)
            {
                return HttpNotFound();
            }
            return View(internal_Package);
        }

        // POST: Internal_Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Internal_Package internal_Package = db.Packages.Find(id);
            db.Packages.Remove(internal_Package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
