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
using JordanSky.Models;

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
        public ActionResult PackagesList()
        {
            var tempPackage = db.Packages.Include(x => x._Package).ToList();
            ViewBag.xxx = tempPackage;
            ViewBag.Type = db.Type_Packges.ToList();

            return View(tempPackage);
        }
        public ActionResult UploadImage(HttpPostedFileBase file, int? Img_id)
        {
                if (file != null)
                {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Image_package/" + ImageName);
                    file.SaveAs(physicalPath);
                    Image_Package image = new Image_Package();
                    image.ImagePath = ImageName;
                    image.Package_id = (int)Img_id;
                    db.Image_Packages.Add(image);
                    db.SaveChanges();
                }
            var tempPackage = db.Packages.Include(x => x._Package).ToList();
            return View("PackagesList", tempPackage);

        }

        public ActionResult DetailsPackages(int id)
        {
            ViewBag.Messg = TempData.Peek("Messg");
            ViewBag.Price = TempData.Peek("Price");
            var tempPackage = db.Packages.Where(m => m.Id == id).Include(x => x._Package).FirstOrDefault();
            ViewBag.Images = db.Image_Packages.Where(m => m.Package_id == id);
            ViewBag.ID = id;
            return View(tempPackage);
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
        public ActionResult Filter(Filter_Package obj)
        {

            if (obj.Type == 0 && obj.Car == 0 && obj.Food == 0 && obj.Hotel == 0)
            {

                ViewBag.tempPackage = TempData.Peek("tempPackage");
                ViewBag.Type = db.Type_Packges.ToList();
                return View();
            }
            else
            {
                var tempPackage = new List<Internal_Package>();
                if (obj.Type == -1 && obj.Car == -1 && obj.Food == -1 && obj.Hotel == -1)
                {
                    tempPackage = db.Packages.Include(x => x._Package).Where(s => s.Status == 1).OrderBy(x => Guid.NewGuid()).Take(1000).ToList();
                    TempData["tempPackage"] = tempPackage;
                    return Json(true);
                }
                else
                {
                    tempPackage = (from op in db.Packages
                                  where ((obj.Type != -1) ? op.Type_id == obj.Type : true) &&
                                    ((obj.Food != -1) ? op.Food == obj.Food : true) &&
                                     ((obj.Hotel != -1) ? op.overnight_Stay == obj.Hotel : true) &&
                                      ((obj.Car != -1) ? op.Transportation == obj.Car : true)

                                 select op).Include(x => x._Package).Distinct().OrderBy(x => Guid.NewGuid()).Take(1000).ToList();
                    TempData["tempPackage"] = tempPackage;

                    return Json(true);
                }
            }
        }

        public ActionResult Book(int id)
        {
            ViewBag.Start = db.Startings.ToList();
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public ActionResult Book(Booking_package book)
        {
            var total = db.Packages.Where(y => y.Id == book.Package_id).FirstOrDefault();
            var Child = total.Child_Price;
            var Pepole = total.Price;
            var price = (book.No_Child * Child) + (book.No_Pepole * Pepole);
            TempData["Price"] = price;
            book.TotalPrice = price;
            book.Status = 0;
            db.Booking_Packages.Add(book);
            db.SaveChanges();
            TempData["Messg"] = "Done";
            return Json(true);
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
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Booking_package delete = db.Booking_Packages.Find(id);
                db.Booking_Packages.Remove(delete);
                db.SaveChanges();
                return Json(true);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        public ActionResult Reservations()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var bookings = db.Booking_Packages.Include(b => b._Package).Include(a =>a._Locations).Where(z => z.Status == 0);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        public ActionResult Confirmed(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Booking_package Confirm = db.Booking_Packages.Find(id);
                Confirm.Status = 1;
                db.Entry(Confirm).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }

        public ActionResult Confirmed_reservations()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var bookings = db.Booking_Packages.Include(b => b._Package).Include(a => a._Locations).Where(z => z.Status == 1);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        public ActionResult Cancled(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Booking_package Cancle = db.Booking_Packages.Find(id);
                Cancle.Status = 2;
                db.Entry(Cancle).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }
        public ActionResult Canceled_reservations()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var bookings = db.Booking_Packages.Include(b => b._Package).Include(a => a._Locations).Where(z => z.Status == 2);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
    }
}
