using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement;


namespace EmployeeManagement.Controllers
{
    public class DepartmentsController : Controller
    {
        private Model1 db = new Model1();

        #region GET_INDEX
        public ActionResult Index()
        {
            return View(db.tblDepartments.ToList());
        }
        #endregion

        #region GET_DETAILS
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDepartment tblDepartment = db.tblDepartments.Find(id);
            if (tblDepartment == null)
            {
                return HttpNotFound();
            }
            return View(tblDepartment);
        }
        #endregion

        #region GET_CREATE
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region POST_CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentName")] tblDepartment tblDepartment)
        {
            if (ModelState.IsValid)
            {
                db.tblDepartments.Add(tblDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDepartment);
        }
        #endregion

        #region GET_EDIT
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDepartment tblDepartment = db.tblDepartments.Find(id);
            if (tblDepartment == null)
            {
                return HttpNotFound();
            }
            return View(tblDepartment);
        }
        #endregion

        #region POST_EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentName")] tblDepartment tblDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDepartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDepartment);
        }

        #endregion

        #region GET_DELETE
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDepartment tblDepartment = db.tblDepartments.Find(id);
            if (tblDepartment == null)
            {
                return HttpNotFound();
            }
            return View(tblDepartment);
        }
        #endregion

        #region POST_DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDepartment tblDepartment = db.tblDepartments.Find(id);
            db.tblDepartments.Remove(tblDepartment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region DISPOSE
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region CHECK_DUPLICATION
        public JsonResult IsAvailable(string DepartmentName, long Id = 0)
        {
            if (Id == 0)
            {
                var Check = db.tblDepartments.Where(x => x.DepartmentName == DepartmentName).Count();

                if (Check > 0)
                {
                    return Json("Department Already Exist", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var Check = db.tblDepartments.Where(x => x.DepartmentName == DepartmentName && x.Id != Id).Count();

                if (Check > 0)
                {
                    return Json("Department Already Exist", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

        }
        #endregion

    }
}
