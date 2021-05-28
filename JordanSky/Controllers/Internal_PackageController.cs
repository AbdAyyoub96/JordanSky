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
            var tempPackage = db.Packages.Include(x => x._Package).Where(s=>s.Status==1).OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            ViewBag.xxx = tempPackage;
            ViewBag.Type = db.Type_Packges.ToList();

            return View(tempPackage);
        }

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
        public ActionResult AddPackages()
        {
            ViewBag.Type_id = new SelectList(db.Type_Packges, "Id", "Name");
            var ID_Row = db.Packages.OrderByDescending(o => o.Id).FirstOrDefault();
            if(ID_Row == null)
            {
                ViewBag.RefNo = "TRIP-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "1";
            }
            else
            {
                ViewBag.RefNo = "TRIP-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + (Convert.ToInt32(ID_Row.Id) + 1);
            }
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPackages( Internal_Package internal_Package, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPathPicture = Server.MapPath("~/Image_package/" + ImageName);
                file.SaveAs(physicalPathPicture);
                internal_Package.Picture = ImageName;
                internal_Package.Status = 1;
                db.Packages.Add(internal_Package);
                db.SaveChanges();
                return RedirectToAction("Packages");
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
