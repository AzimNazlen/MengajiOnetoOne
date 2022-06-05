using MengajiOneToOneSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MengajiOneToOneSystem.Controllers
{
    public class ManageProfileController : Controller
    {
        private db_motoEntities db = new db_motoEntities();
        // GET: ManageProfile
        public ActionResult Index()
        {
            if(Session["userID"] != null)
            {
                ViewBag.UserName = Session["userName"];
                return View();
            }
            else
            {
                return RedirectToAction ("Login","User");
            }
        }

        public ActionResult Setting(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User u = db.Users.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

       
    }
}