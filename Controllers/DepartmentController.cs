using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;

namespace Work_01.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        HRMSystemDbContext db = new HRMSystemDbContext();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.Department.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Department.Add(department);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Department department = db.Department.Find(id);

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Department department = db.Department.Find(id);

            db.Department.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}