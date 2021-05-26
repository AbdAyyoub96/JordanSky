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
    public class ImagesController : Controller
    {
        private JordanSkyContext db = new JordanSkyContext();

        // GET: Images
        public ActionResult Index(int id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true)
            {
                var images = db.Images.Where(x => x.Mazr3a_id == id).Include(i => i.mazr);
                TempData["Id"] = id;
                return View(images.ToList());

            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

                   }



        //public ActionResult getMazra3a(int ID)
        //{
        //    return Json(db.Mazrs.Where(o => o.City_id == ID).Select(x => new
        //    {
        //        Mazra3aID = x.City_id,
        //        Mazra3atName = x.Name
        //    }).ToList(), JsonRequestBehavior.AllowGet);
        //}


 

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToBoolean(Session["Check_User"]) == true) {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Images.Find(id);
                var x = TempData["Id"];
                if (image == null)
                {
                    return HttpNotFound();
                }
                db.Images.Remove(image);
                db.SaveChanges();
                return RedirectToAction("Index", new { ID = x });
            }
            Session["Check_User"] = false;
            return Redirect("~/Errors/error_404.html");

        }
    }
}
