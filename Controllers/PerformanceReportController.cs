
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MengajiOneToOneSystem.Models;

namespace MengajiOneToOneSystem.Controllers
{
    public class PerformanceReportController : Controller
    {
        private db_motoEntities db = new db_motoEntities();

        // GET: Performance View for teacher
        public ActionResult IndexTeacher()
        {
            var performanceReports = db.PerformanceReports.Include(p =>Session["UserID"]);
            return View(performanceReports.ToList());
        }

        // GET: Performance View for student
        public ActionResult IndexStudent()
        {
            var performanceReports = db.PerformanceReports.Include(p => Session["UserID"]);
            return View(performanceReports.ToList());
        }

        // GET: PerformanceReports details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceReport performanceReport = db.PerformanceReports.Find(id);
            if (performanceReport == null)
            {
                return HttpNotFound();
            }
            return View(performanceReport);
        }

        // GET: PerformanceReports/Create
        public ActionResult Create()
        {
            //ViewBag.t_id = new SelectList(db.Teachers, "t_id", "t_password");
            ViewBag.u_id = new SelectList(db.Users, "u_id", "u_password");
            return View();
        }

        // POST: PerformanceReports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_month,t_id,u_id,student_performance,class_date,class_ref,p_status,p_id")] PerformanceReport performanceReport)
        {
            if (ModelState.IsValid)
            {
                db.PerformanceReports.Add(performanceReport);
                db.SaveChanges();
                return RedirectToAction("Performance", "_PerformanceTeacherView");
            }

            //ViewBag.t_id = new SelectList(db.Teachers, "t_id", "t_password", performanceReport.t_id);
            ViewBag.u_id = new SelectList(db.Users, "u_id", "u_password", performanceReport.u_id);
            return View(performanceReport);
        }

        // GET: PerformanceReports1/Edit/5
        public ActionResult Edit(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceReport performanceReport = db.PerformanceReports.Find(id);
            if (performanceReport == null)
            {
                return HttpNotFound();
            }
            //ViewBag.t_id = new SelectList(db.Teachers, "t_id", "t_password", performanceReport.t_id);
            ViewBag.u_id = new SelectList(db.Users, "u_id", "u_password", performanceReport.u_id);
            return View(performanceReport);
        }

        // POST: PerformanceReports1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_month,t_id,u_id,student_performance,class_date,class_ref,p_status,p_id")] PerformanceReport performanceReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performanceReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Performance","_PerformanceTeacherView");
            }
            //ViewBag.t_id = new SelectList(db.Teachers, "t_id", "t_password", performanceReport.t_id);
            ViewBag.u_id = new SelectList(db.Users, "u_id", "u_password", performanceReport.u_id);
            return View(performanceReport);
        }

        // GET: PerformanceReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceReport performanceReport = db.PerformanceReports.Find(id);
            if (performanceReport == null)
            {
                return HttpNotFound();
            }
            return View(performanceReport);
        }

        // POST: PerformanceReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerformanceReport performanceReport = db.PerformanceReports.Find(id);
            db.PerformanceReports.Remove(performanceReport);
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
