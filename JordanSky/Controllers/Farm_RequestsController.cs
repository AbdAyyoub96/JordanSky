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
    public class Farm_RequestsController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Farm_Requests
        public ActionResult New_Request()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var registers = db.Registers.Include(r => r.city).Where(a => a.Status == 1);
                return View(registers.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }
        public ActionResult Confirmed_Request()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var registers = db.Registers.Include(r => r.city).Where(a => a.Status == 2);
                return View(registers.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
           
        }

        // GET: Farm_Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Register register = db.Registers.Find(id);
                if (register == null)
                {
                    return HttpNotFound();
                }
                return View(register);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
       
        }

        // GET: Farm_Requests/Create
        public ActionResult Create()
        {
            ViewBag.City_id = db.Cities.ToList();
            return View();
        }

        // POST: Farm_Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Register register , HttpPostedFileBase FilePicture, HttpPostedFileBase Filelicense)
        {
            if (FilePicture != null && Filelicense != null)
            {
                string ImageName = System.IO.Path.GetFileName(FilePicture.FileName);
                string physicalPathPicture = Server.MapPath("~/Image_Facilty/" + ImageName);
                string licenseName = System.IO.Path.GetFileName(Filelicense.FileName);
                string physicalPathlicense = Server.MapPath("~/Image_Facilty/" + licenseName);
                FilePicture.SaveAs(physicalPathPicture);
                Filelicense.SaveAs(physicalPathlicense);
                register.Picture = ImageName;
                register.license = licenseName;
                register.Status = 1;
                db.Registers.Add(register);
                db.SaveChanges();
            }

            ViewBag.City_id = db.Cities.ToList();
            ViewBag.Messg = "Done";
            return View(register);
        }

        // GET: Farm_Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Register register = db.Registers.Find(id);
                register.Status = 2;
                db.Entry(register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("New_Request");
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
         
        }

        // GET: Farm_Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Register register = db.Registers.Find(id);
                db.Registers.Remove(register);
                db.SaveChanges();
                return RedirectToAction("Confirmed_Request");
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

         
        }

      
    }
}
