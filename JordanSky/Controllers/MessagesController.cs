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
    public class MessagesController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Messages
        public ActionResult Inbox()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var Messages = db.Messages.Where(z => z.Status == 1);
                return View(Messages.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }
        public ActionResult Read()
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var Messages = db.Messages.Where(z => z.Status == 2);
                return View(Messages.ToList());
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Message message = db.Messages.Find(id);
                if (message == null)
                {
                    return HttpNotFound();
                }
                return View(message);
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }

        // GET: Messages/Create
        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Send(Message message)
        {
            message.Status = 1;
            db.Messages.Add(message);
            db.SaveChanges();
            return Json(true);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Message message = db.Messages.Find(id);
                message.Status = 2;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Inbox");
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                Message message = db.Messages.Find(id);
                db.Messages.Remove(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");
          
        }

     
    }
}
