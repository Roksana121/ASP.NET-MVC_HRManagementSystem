using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;
using Work_01.Models.ViewModels;

namespace Work_01.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        HRMSystemDbContext db = new HRMSystemDbContext();
        // GET: Administrator
        public ActionResult Index()
        {
            var administratorInfo = (
                                    from a in db.Administrators
                                    join aad in db.AdministratorAddresses on a.AdministratorId equals aad.AdministratorId
                                    join ap in db.AdministratorPhoto on a.AdministratorId equals ap.AdministratorId
                                    select new AdministratorRetriveViewModels
                                    {
                                        AdministratorId = a.AdministratorId,
                                        AdministratorName = a.AdministratorName,
                                        Address = aad.Address,
                                        PostCode = aad.PostCode,
                                        Image = ap.Image
                                    }).ToList();

            return View(administratorInfo);
        }

        public ActionResult Create()
        {
            return View();
        }
       [HttpPost]
        public ActionResult Create(AdministratorViewModels administrator)
        {
            string msg = "";

            using (var context = new HRMSystemDbContext())
            {
                using (DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (administrator.Image != null)
                            {
                                string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() +
                                Path.GetExtension(administrator.Image.FileName));
                                administrator.Image.SaveAs(Server.MapPath(filePath));

                                Administrator a = new Administrator
                                {
                                    AdministratorName = administrator.AdministratorName

                                };
                                AdministratorAddress aad = new AdministratorAddress
                                {
                                    Address = administrator.Address,
                                    PostCode = administrator.PostCode,
                                    Administrator = a
                                };
                                AdministratorPhoto ap = new AdministratorPhoto
                                {
                                    Administrator = a,
                                    Image = filePath
                                };

                                db.AdministratorAddresses.Add(aad);
                                db.AdministratorPhoto.Add(ap);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbTran.Rollback();
                        msg = ex.Message;
                        ViewBag.msg = msg;
                    }
                }
            }

            return View();
        }
        public ActionResult Edit(int? id)
        {

            if (id != null)
            {
                var a = db.Administrators.FirstOrDefault(x => x.AdministratorId == id);
                var aad = db.AdministratorAddresses.FirstOrDefault(x => x.AdministratorId == id);
                var ap = db.AdministratorPhoto.FirstOrDefault(x => x.AdministratorId == id);

                AdministratorRetriveViewModels administratorRetriveViewModels = new AdministratorRetriveViewModels
                {
                    AdministratorId = a.AdministratorId,
                    AdministratorName = a.AdministratorName,
                    Address = aad.Address,
                    PostCode = aad.PostCode,
                    Image = ap.Image
                };
                return View(administratorRetriveViewModels);

            }

            return View();
        }
        [HttpPost]
        public ActionResult Edit(AdministratorViewModels av)
        {
            if (ModelState.IsValid)
            {
                if (av.Image != null)
                {

                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() +
                    Path.GetExtension(av.Image.FileName));
                    av.Image.SaveAs(Server.MapPath(filePath));


                    Administrator administrator = new Administrator
                    {
                        AdministratorId = av.AdministratorId,
                        AdministratorName = av.AdministratorName,

                    };
                    AdministratorAddress aad = new AdministratorAddress
                    {
                        AdministratorId = av.AdministratorId,
                        Address = av.Address,
                        PostCode = av.PostCode

                    };
                    AdministratorPhoto ap = new AdministratorPhoto
                    {
                        AdministratorId = av.AdministratorId,
                        Image = filePath
                    };
                    db.Entry(administrator).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(aad).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ap).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Administrator a = new Administrator
                    {
                        AdministratorId = av.AdministratorId,
                        AdministratorName = av.AdministratorName
                    };
                    AdministratorAddress aad = new AdministratorAddress
                    {
                        AdministratorId = av.AdministratorId,
                        Address = av.Address,
                        PostCode = av.PostCode,
                    };

                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(aad).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(av);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var a = db.Administrators.FirstOrDefault(x => x.AdministratorId == id);
                var aad = db.AdministratorAddresses.FirstOrDefault(x => x.AdministratorId == id);
                var ap = db.AdministratorPhoto.FirstOrDefault(x => x.AdministratorId == id);

                db.AdministratorPhoto.Remove(ap);
                db.AdministratorAddresses.Remove(aad);
                db.Administrators.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View();
        }
    }
}