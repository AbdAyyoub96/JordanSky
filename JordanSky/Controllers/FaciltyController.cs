using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JordanSky.Context;
using JordanSky.Entity;
using JordanSky.Models;

namespace JordanSky.Controllers
{

    public class FaciltyController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();
        // GET: Mazr3a
        public ActionResult Index()
        {
            var tempMazra = db.Mazrs.Include(x => x.city).OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            if (tempMazra.Any())
            {
                foreach (var p in tempMazra)
                {
                    p.defaultImage = db.Images.OrderBy(x => x.Id).FirstOrDefault(y => y.Mazr3a_id == p.Id)?.ImagePath;
                }
            }
            
            ViewBag.xxx = tempMazra;
            ViewBag.City = db.Cities.ToList();
            ViewBag.Cate = db.Categories.ToList();
            ViewBag.Type = db.Type_s.ToList();

            return View(tempMazra);
        }
        public ActionResult Filter(Filter_serch obj)
        {
            
            if (obj.City_value==0 && obj.FierstNum==0 && obj.SecondNum==0 && obj.rating_Value==0 && obj.Category_Value==0)
            {
               
                ViewBag.tempMazra = TempData.Peek("tempMazra");
                ViewBag.City = db.Cities.ToList();
                ViewBag.Cate = db.Categories.ToList();
                ViewBag.Type = db.Type_s.ToList();
                return View();
            }
            else
            {
                var tempMazra = new List<Mazr3a>();
                if (obj.FierstNum == 0 && obj.SecondNum == 0 && obj.rating_Value == -1 && obj.Category_Value == -1 && obj.City_value == -1)
                {
                    tempMazra = db.Mazrs.Include(x => x.city).OrderBy(x => Guid.NewGuid()).Take(1000).ToList();
                    if (tempMazra.Any())
                    {
                        foreach (var p in tempMazra)
                        {
                            p.defaultImage = db.Images.OrderBy(x => x.Id).FirstOrDefault(y => y.Mazr3a_id == p.Id)?.ImagePath;
                        }
                    }
                    TempData["tempMazra"] = tempMazra;
                    return Json(true);
                }
                else
                {
                    double MaxValue = 0;
                    double MinValue = 0;
                    if (obj.FierstNum == 0 || obj.SecondNum == 0)
                    {
                        MaxValue = db.Mazrs.Max(x => x.Price);
                        MinValue = db.Mazrs.Min(x => x.Price);
                    }

                    tempMazra = (from op in db.Mazrs
                                 join pg in db.Cate_Mazra3s on op.Id equals pg.Mazra3a_id
                                 where((obj.rating_Value != -1) ? pg.Category_id == obj.rating_Value : true) 
                                 where ((obj.City_value != -1) ? op.City_id == obj.City_value : true )&& 
                                    ((obj.Category_Value != -1) ? op.Type_Product_id == obj.Category_Value : true) &&
                                    (((obj.FierstNum != 0) ? (op.Price >= obj.FierstNum) : (op.Price >= MinValue)) && ((obj.SecondNum != 0) ? (op.Price <= obj.SecondNum) : (op.Price <= MaxValue)))

                                 select op).Include(x => x.city).Distinct().OrderBy(x => Guid.NewGuid()).Take(1000).ToList();
                    if (tempMazra.Any())
                    {
                        foreach (var p in tempMazra)
                        {
                            p.defaultImage = db.Images.OrderBy(x => x.Id).FirstOrDefault(y => y.Mazr3a_id == p.Id)?.ImagePath;
                        }
                    }
                    TempData["tempMazra"] = tempMazra;
                 
                    return Json(true);
                }
            }
        }
        public ActionResult Home(string dist)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var mazrs = db.Mazrs.Include(m => m.city).Include(m => m.product);
                if (dist != null)
                { ViewBag.msg = dist; }
                return View(mazrs.ToList());
            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

           
           
        }

        // GET: Mazr3a/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Messg = TempData.Peek("Messg");
            var tempMazra = db.Mazrs.Where(m => m.Id == id).Include(x => x.city).FirstOrDefault();
            ViewBag.Images = db.Images.Where(m => m.Mazr3a_id == id);
            ViewBag.Title = tempMazra.Ref_No;
            ViewBag.ID = id;
            return View(tempMazra);
        }

        // GET: Mazr3a/Create
        public ActionResult Add_Mazr3a(string dist)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (dist == null)
                {
                    ViewBag.Status = "Save";
                    var ID_Row = db.Mazrs.OrderByDescending(o => o.Id).FirstOrDefault();
                    ViewBag.RefNo = "JORDAN-" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + (Convert.ToUInt32(ID_Row.Id) + 1);
                    ViewBag.City = db.Cities.ToList();
                    ViewBag.Type = db.Type_s.ToList();
                    ViewBag.Cate = db.Categories.ToList();

                    return View();
                }
                else
                {
                    ViewBag.Status = "Update";
                    ViewBag.City = db.Cities.ToList();
                    ViewBag.Type = db.Type_s.ToList();
                    ViewBag.Cate = db.Categories.ToList();
                    var obj = TempData.Peek("DataMazra");
                    return View(obj);
                }
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
          
        }
        public ActionResult Book(int id)
        {
         
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public ActionResult Book(Booking book)
        {
            int x = book.Id;
            book.Mazra3a_id = book.Id;
            book.Id = 0;
            book.Status = 0;
            db.Bookings.Add(book);
            db.SaveChanges();
            TempData["Messg"] = "Done";            
            return RedirectToAction("Details", new {ID =x });
        }

        public ActionResult SaveMazra3a(Mazr3a obj)
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
                    db.Mazrs.Add(obj);
                    db.SaveChanges();
                    var ID_Row = db.Mazrs.OrderByDescending(o => o.Id).FirstOrDefault();
                    string[] array = obj.Number.Split(',');
                    for (int i = 0; i < array.Length; i++)
                    {
                        Cate_Mazra3a cate_Mazra3A = new Cate_Mazra3a();
                        cate_Mazra3A.Category_id = Convert.ToInt32(array[i]);
                        cate_Mazra3A.Mazra3a_id = ID_Row.Id;
                        db.Cate_Mazra3s.Add(cate_Mazra3A);
                        db.SaveChanges();

                    }
                    return Json(true);
                }
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
           
            
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int? Img_id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (file != null)
                {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Image_Facilty/" + ImageName);
                    file.SaveAs(physicalPath);
                    Image image = new Image();
                    image.ImagePath = ImageName;
                    image.Mazr3a_id = (int)Img_id;
                    db.Images.Add(image);
                    db.SaveChanges();
                }
                var mazrs = db.Mazrs.Include(m => m.city).Include(m => m.product);
                return View("Home", mazrs.ToList());
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

                    Mazr3a mazr3a = db.Mazrs.Find(id);
                    ViewBag.City_id = new SelectList(db.Cities, "Id", "Name", mazr3a.City_id);
                    TempData["DataMazra"] = mazr3a;

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
                    Mazr3a mazr3a = db.Mazrs.Find(id);
                    db.Mazrs.Remove(mazr3a);
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
                var bookings = db.Bookings.Include(b => b.mazr3).Where(z => z.Status == 0);
                return View(bookings.ToList());
            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
         
        }
        
        public ActionResult Confirmed_reservations()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true) {
                var bookings = db.Bookings.Include(b => b.mazr3).Where(z => z.Status == 1);
                return View(bookings.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
         
        }
        public ActionResult Canceled_reservations()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var bookings = db.Bookings.Include(b => b.mazr3).Where(z => z.Status == 2);
                return View(bookings.ToList());
            }
                Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
         
        }

       
    }
}
