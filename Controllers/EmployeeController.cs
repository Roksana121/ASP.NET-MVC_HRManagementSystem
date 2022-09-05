using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;
using Work_01.Models.ViewModels;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.IO;
using System.Data.Entity.Validation;
using System.Net;
using System.Data.Entity.Infrastructure;


namespace Work_01.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        HRMSystemDbContext db = new HRMSystemDbContext();
        // GET: Employee
        public ActionResult Index(int pageNumber = 1, string sort = "", string search = "")
        {
            ViewBag.sortBy = sort == "name" ? "name_desc" : "name";
            ViewBag.currentSort = sort;
            var data = db.Employee.Select(x => new EmployeeViewModels
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                Gender = x.Gender,
                JoinDate = x.JoinDate,
                Designation = x.Designation,
                Status = x.Status,
                DepartmentId = x.DepartmentId,
                Address = x.Address,
                Email = x.Email,
                Phone = x.Phone,
                SalaryAmmount = x.SalaryAmmount,
                Image = x.Image
            });
            switch (sort)
            {
                case "name":
                    data = data.OrderBy(x => x.EmployeeName);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(x => x.EmployeeName);
                    break;
                default:
                    data = data.OrderBy(x => x.EmployeeName);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.EmployeeName.ToLower().StartsWith(search.ToLower()));
            }


            return View(data.ToPagedList(pageNumber, 5));
        }

        public ActionResult Create()
        {
            ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");

            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeViewModels employeeViewModels)
        {
            if (ModelState.IsValid)
            {
                if (employeeViewModels.Picture != null)
                {
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString()
                        + Path.GetExtension(employeeViewModels.Picture.FileName));
                    employeeViewModels.Picture.SaveAs(Server.MapPath(filePath));

                    Employee employee = new Employee
                    {

                        EmployeeName = employeeViewModels.EmployeeName,
                        Gender = employeeViewModels.Gender,
                        JoinDate = employeeViewModels.JoinDate,
                        Designation = employeeViewModels.Designation,
                        Status = employeeViewModels.Status,
                        DepartmentId = employeeViewModels.DepartmentId,
                        Address = employeeViewModels.Address,
                        Email = employeeViewModels.Email,
                        Phone = employeeViewModels.Phone,
                        SalaryAmmount = employeeViewModels.SalaryAmmount,
                        Image = filePath
                    };
                    db.Employee.Add(employee);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");

            return PartialView("_error");
        }


        //----------------------//
        //public ActionResult Edit(int? id)
        //{
        //    //ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employee.Find(id);


        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    EmployeeViewModels employeeViewModels = new EmployeeViewModels
        //    {
        //        EmployeeId = employee.EmployeeId,
        //        EmployeeName = employee.EmployeeName,
        //        Gender = employee.Gender,
        //        JoinDate = employee.JoinDate,
        //        Designation = employee.Designation,
        //        Status = employee.Status,
        //        DepartmentId = (int)employee.DepartmentId,
        //        Address = employee.Address,
        //        Email = employee.Email,
        //        Phone = employee.Phone,
        //        SalaryAmmount = employee.SalaryAmmount,
        //        Image = employee.Image
        //    };
        //    ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
        //    return View(employeeViewModels);
        //}

        //[HttpPost]
        //public ActionResult Edit(EmployeeViewModels employeeViewModels)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string filePath = employeeViewModels.Image;
        //        if (employeeViewModels.Picture != null)
        //        {
        //            filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(employeeViewModels.Picture.FileName));
        //            employeeViewModels.Picture.SaveAs(Server.MapPath(filePath));

        //            Employee employee = new Employee
        //            {
        //                EmployeeId = employeeViewModels.EmployeeId,
        //                EmployeeName = employeeViewModels.EmployeeName,
        //                Gender = employeeViewModels.Gender,
        //                JoinDate = employeeViewModels.JoinDate,
        //                Designation = employeeViewModels.Designation,
        //                Status = employeeViewModels.Status,
        //                DepartmentId = (int)employeeViewModels.DepartmentId,
        //                Address = employeeViewModels.Address,
        //                Email = employeeViewModels.Email,
        //                Phone = employeeViewModels.Phone,
        //                SalaryAmmount = employeeViewModels.SalaryAmmount,
        //                Image = filePath
        //            };
        //            db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            Employee employee = new Employee
        //            {
        //                EmployeeId = employeeViewModels.EmployeeId,
        //                EmployeeName = employeeViewModels.EmployeeName,
        //                Gender = employeeViewModels.Gender,
        //                JoinDate = employeeViewModels.JoinDate,
        //                Designation = employeeViewModels.Designation,
        //                Status = employeeViewModels.Status,
        //                DepartmentId = (int)employeeViewModels.DepartmentId,
        //                Address = employeeViewModels.Address,
        //                Email = employeeViewModels.Email,
        //                Phone = employeeViewModels.Phone,
        //                SalaryAmmount = employeeViewModels.SalaryAmmount,
        //                Image = filePath
        //            };
        //            db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");

        //    return View(employeeViewModels);
        //}
        // --------------------------------//
        public ActionResult Edit(int? id)
        {
            ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
            if (id != null)
            {
                var emp = db.Employee.FirstOrDefault(x => x.EmployeeId == id);
                var ea = db.EmployeeAddress.FirstOrDefault(x => x.EmployeeId == id);
                var ep = db.EmployeePhoto.FirstOrDefault(x => x.EmployeeId == id);
                var es = db.EmployeeSalary.FirstOrDefault(x => x.EmployeeId == id);

                EmployeeViewModels employeeViewModels = new EmployeeViewModels
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = emp.EmployeeName,
                    Gender = emp.Gender,
                    JoinDate = emp.JoinDate,
                    Designation = emp.Designation,
                    Status = emp.Status,
                    DepartmentId = emp.DepartmentId,
                    Address = ea.Address,
                    Email = ea.Email,
                    Phone = ea.Phone,
                    SalaryAmmount = es.SalaryAmmount,
                    Image = ep.Image
                };
                return View(employeeViewModels);
            }
            return View();

        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModels Ev)
        {
            if (ModelState.IsValid)
            {
                if (Ev.Picture != null)
                {

                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() +
                    Path.GetExtension(Ev.Picture.FileName));
                    Ev.Picture.SaveAs(Server.MapPath(filePath));


                    Employee emp = new Employee

                    {
                        EmployeeId = Ev.EmployeeId,
                        EmployeeName = Ev.EmployeeName,
                        Gender = Ev.Gender,
                        JoinDate = Ev.JoinDate,
                        Designation = Ev.Designation,
                        Status = Ev.Status,
                        DepartmentId = Ev.DepartmentId
                    };
                    EmployeeAddress ea = new EmployeeAddress
                    {
                        EmployeeId = Ev.EmployeeId,
                        Address = Ev.Address,
                        Email = Ev.Email,
                        Phone = Ev.Phone

                    };

                    EmployeeSalary es = new EmployeeSalary
                    {
                        EmployeeId = Ev.EmployeeId,
                        SalaryAmmount = Ev.SalaryAmmount
                    };
                    EmployeePhoto ep = new EmployeePhoto
                    {
                        EmployeeId = Ev.EmployeeId,
                        Image = filePath
                    };

                    db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ea).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ep).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(es).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Employee emp = new Employee
                    {
                        EmployeeId = Ev.EmployeeId,
                        EmployeeName = Ev.EmployeeName,
                        Gender = Ev.Gender,
                        JoinDate = Ev.JoinDate,
                        Designation = Ev.Designation,
                        Status = Ev.Status,
                        DepartmentId = Ev.DepartmentId

                    };
                    EmployeeAddress ea = new EmployeeAddress
                    {
                        EmployeeId = Ev.EmployeeId,
                        Address = Ev.Address,
                        Email = Ev.Email,
                        Phone = Ev.Phone

                    };

                    EmployeeSalary es = new EmployeeSalary
                    {
                        EmployeeId = Ev.EmployeeId,
                        SalaryAmmount = Ev.SalaryAmmount
                    };
                    EmployeePhoto ep = new EmployeePhoto
                    {
                        EmployeeId = Ev.EmployeeId,
                        Image = Ev.Image
                    };
                    db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ea).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ep).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(es).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
            return View(Ev);
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {

                Employee Emp = db.Employee.Find(id);

                db.Entry(Emp).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
            return View();
        }
    }

}

// --------------//

//    public ActionResult Delete(int? id)
//    {
//        ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
//        if (id == null)
//        {
//            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
//        }
//        Employee employee = db.Employee.Find(id);
//        EmployeeAddress employeeAddress = db.EmployeeAddress.Find(id);
//        EmployeePhoto employeePhoto = db.EmployeePhoto.Find(id);
//        EmployeeSalary employeeSalary = db.EmployeeSalary.Find(id);

//        if (employee == null)
//        {
//            return HttpNotFound();
//        }
//        return View(employee);
//    }
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]

//    public ActionResult DeleteConfirm(int id)
//    {
//        if (id != null)
//        {
//            var Emp = db.Employee.FirstOrDefault(x => x.EmployeeId == id);
//            var ea = db.EmployeeAddress.FirstOrDefault(x => x.EmployeeId == id);
//            var ep = db.EmployeePhoto.FirstOrDefault(x => x.EmployeeId == id);
//            var es = db.EmployeeSalary.FirstOrDefault(x => x.EmployeeId == id);


//            db.EmployeeSalary.Remove(es);
//            db.EmployeePhoto.Remove(ep);
//            db.EmployeeAddress.Remove(ea);
//            db.Employee.Remove(Emp);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
//        return View("Index");
//    }


///-----------------------///
//////public ActionResult Edit(int? id)
//////{
//////    ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
//////    Employee Emp = db.Employee.Find(id);
//////    return View(Emp);
//////}
//////[HttpPost]
//////public ActionResult Edit(Employee Emp)
//////{
//////    if (ModelState.IsValid)
//////    {
//////        db.Entry(Emp).State = System.Data.Entity.EntityState.Modified;
//////        db.SaveChanges();
//////        return View("Index");
//////    }
//////    ViewBag.employee = new SelectList(db.Department, "DepartmentId", "DepartmentName");
//////    return View(Emp);
//////}

////        //public ActionResult Delete(int? id)
////        //{
////        //    if (id != null)
////        //    {
////        //        Department d = db.Departments.Find(id);

////        //        db.Entry(d).State = System.Data.Entity.EntityState.Deleted;
////        //        db.SaveChanges();
////        //        return RedirectToAction("Index");
////        //    }
////        //    return View();
////        //}
////    }
////}
//--------------------//





