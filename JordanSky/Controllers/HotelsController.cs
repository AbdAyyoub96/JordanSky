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
    public class HotelsController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Hotels
        public ActionResult Hotel()
        {
            var temphotels = db.Hotels.Include(h => h.city).OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            if (temphotels.Any())
            {
                foreach (var p in temphotels)
                {
                    p.defaultImage = db.Image_Hotels.OrderBy(x => x.Id).FirstOrDefault(y => y.Hotel_id == p.Id)?.ImagePath;
                }
            }
            ViewBag.Hotel = temphotels;
            ViewBag.City = db.Cities.Where(x => x.hotels.Count() >= 1).ToList();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.Type = db.Type_Hotels.Where(x => x.hotels.Count() >= 1).ToList();

            return View(temphotels);
        }
        public ActionResult Filter(Filter_serch obj)
        {

            if (obj.City_value == 0 && obj.FierstNum == 0 && obj.SecondNum == 0 && obj.rating_Value == 0 && obj.Category_Value == 0)
            {

                ViewBag.temphotels = TempData.Peek("temphotels");
                ViewBag.City = db.Cities.Where(x => x.hotels.Count() >= 1).ToList();
                ViewBag.Cate = db.Categories.ToList();
                ViewBag.Type = db.Type_Hotels.Where(x => x.hotels.Count() >= 1).ToList();
                return View();
            }
            else
            {
                var temphotels = new List<Hotel>();
                if (obj.FierstNum == 0 && obj.SecondNum == 0 && obj.rating_Value == -1 && obj.Category_Value == -1 && obj.City_value == -1)
                {
                    temphotels = db.Hotels.Include(x => x.city).OrderBy(x => Guid.NewGuid()).Take(1000).ToList();
                    if (temphotels.Any())
                    {
                        foreach (var p in temphotels)
                        {
                            p.defaultImage = db.Image_Hotels.OrderBy(x => x.Id).FirstOrDefault(y => y.Hotel_id == p.Id)?.ImagePath;
                        }
                    }
                    TempData["temphotels"] = temphotels;
                    return Json(true);
                }
                else
                {
                    double MaxValue = 0;
                    double MinValue = 0;
                    if (obj.FierstNum == 0 || obj.SecondNum == 0)
                    {
                        MaxValue = db.Hotels.Max(x => x.Price_Day);
                        MinValue = db.Hotels.Min(x => x.Price_Day);
                    }

                    temphotels = (from op in db.Hotels
                                 join pg in db.Cate_Hotels on op.Id equals pg.Hotel_id
                                 where ((obj.rating_Value != -1) ? pg.Category_id == obj.rating_Value : true)
                                 where ((obj.City_value != -1) ? op.City_id == obj.City_value : true) &&
                                    ((obj.Category_Value != -1) ? op.Type_Hotel_id == obj.Category_Value : true) &&
                                    (((obj.FierstNum != 0) ? (op.Price_Day >= obj.FierstNum) : (op.Price_Day >= MinValue)) && ((obj.SecondNum != 0) ? (op.Price_Day <= obj.SecondNum) : (op.Price_Day <= MaxValue)))

                                 select op).Include(x => x.city).Distinct().OrderBy(x => Guid.NewGuid()).Take(1000).ToList();
                    if (temphotels.Any())
                    {
                        foreach (var p in temphotels)
                        {
                            p.defaultImage = db.Image_Hotels.OrderBy(x => x.Id).FirstOrDefault(y => y.Hotel_id == p.Id)?.ImagePath;
                        }
                    }
                    TempData["temphotels"] = temphotels;

                    return Json(true);
                }
            }
        }
        public ActionResult All_hotel(string dist)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var hotel = db.Hotels.Include(m => m.city).Include(m => m.Hotel_type).ToList();
                if (dist != null)
                { ViewBag.msg = dist; }
                return View(hotel);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }
        public ActionResult New_Hotel(string dist)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (dist == null)
                {
                    ViewBag.Status = "Save";
                    var ID_Row = db.Hotels.OrderByDescending(o => o.Id).FirstOrDefault();
                    if (ID_Row == null)
                    {
                        ViewBag.RefNo = "HOTEL-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "1";
                    }
                    else
                    {
                        ViewBag.RefNo = "HOTEL-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + (Convert.ToInt32(ID_Row.Id) + 1);
                    }
                    ViewBag.City = db.Cities.ToList();
                    ViewBag.Type = db.Type_Hotels.ToList();
                    ViewBag.Cate = db.Categories.ToList();

                    return View();
                }
                else
                {
                    ViewBag.Status = "Update";
                    ViewBag.City = db.Cities.ToList();
                    ViewBag.Type = db.Hotels.ToList();
                    ViewBag.Cate = db.Categories.ToList();
                    var obj = TempData.Peek("DataHotel");
                    return View(obj);
                }
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        
        public ActionResult Save_Hotel(Hotel obj)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (obj.Id != 0)
                {
                    //Mazr3a mazr3a = db.Mazrs.Find(obj.Id);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    db.Hotels.Add(obj);
                    db.SaveChanges();
                    var ID_Row = db.Hotels.OrderByDescending(o => o.Id).FirstOrDefault();
                    string[] array = obj.Number.Split(',');
                    for (int i = 0; i < array.Length; i++)
                    {
                        Cate_Hotel cate = new Cate_Hotel();
                        cate.Category_id = Convert.ToInt32(array[i]);
                        cate.Hotel_id = ID_Row.Id;
                        db.Cate_Hotels.Add(cate);
                        db.SaveChanges();

                    }
                    return Json(true);
                }
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        public ActionResult DetailsHotel(int id)
        {
            ViewBag.Messg = TempData.Peek("Messg");
            var temphotels = db.Hotels.Where(m => m.Id == id).Include(x => x.city).FirstOrDefault();
            ViewBag.Images = db.Image_Hotels.Where(m => m.Hotel_id == id);
            ViewBag.Title = temphotels.Ref_No;
            ViewBag.Cate = db.Cate_Hotels.Where(m => m.Hotel_id == id).Include(x => x.category);
            ViewBag.ID = id;
            return View(temphotels);
        }

        public ActionResult Book(int id)
        {

            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public ActionResult Book(Booking_Hotel book)
        {
            book.Status = 0;
            db.Booking_Hotels.Add(book);
            db.SaveChanges();
            TempData["Messg"] = "Done";
            return Json(true);
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase[] files, int? Img_id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                foreach (HttpPostedFileBase file in files)
                    if (file != null)
                    {
                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/Image_Hotel/" + ImageName);
                        file.SaveAs(physicalPath);
                        Image_Hotel image = new Image_Hotel();
                        image.ImagePath = ImageName;
                        image.Hotel_id = (int)Img_id;
                        db.Image_Hotels.Add(image);
                        db.SaveChanges();
                    }
                var hotels = db.Hotels.Include(m => m.city).Include(m => m.Hotel_type);
                return View("All_hotel", hotels.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }

        public ActionResult Edit(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (id != null)
                {
                    Hotel hotel  = db.Hotels.Find(id);
                    ViewBag.City_id = new SelectList(db.Cities, "Id", "Name", hotel.City_id);
                    TempData["DataHotel"] = hotel;
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");


        }
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (id != null)
                {
                    Hotel hotel = db.Hotels.Find(id);
                    db.Hotels.Remove(hotel);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }
        public ActionResult Reservations()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var bookings = db.Booking_Hotels.Include(b => b.hotel).Where(z => z.Status == 0);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        public ActionResult Confirmed(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Booking_Hotel Confirm = db.Booking_Hotels.Find(id);
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
                var bookings = db.Booking_Hotels.Include(b => b.hotel).Where(z => z.Status == 1);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
        public ActionResult Cancled(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Booking_Hotel Cancle = db.Booking_Hotels.Find(id);
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
                var bookings = db.Booking_Hotels.Include(b => b.hotel).Where(z => z.Status == 2);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }

        public ActionResult DeleteRev(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Booking_Hotel delete = db.Booking_Hotels.Find(id);
                db.Booking_Hotels.Remove(delete);
                db.SaveChanges();
                return Json(true);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
    }
}
