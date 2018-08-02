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
    public class EmployeesController : Controller
    {
        private Model1 db = new Model1();

        #region GET_INDEX
        public ActionResult Index()
        {
            var ListData = (from c in db.tblDepartments
                            select new
                            {
                                DepartmentId = c.Id,
                                DepartmentName = c.DepartmentName
                            }).ToList();

            ViewBag.Departments = ListData;

            return View(db.tblEmployees.ToList());
        }
        #endregion

        #region GET_DETAILS
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }
        #endregion

        #region GET_CREATE
        public ActionResult Create()
        {
            var ListData = (from c in db.tblDepartments
                            select new
                            {
                                DepartmentId = c.Id,
                                DepartmentName = c.DepartmentName
                            }).ToList();

            ViewBag.Departments = ListData;

            return View();
        }
        #endregion

        #region POST_CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EmailId,DepartmentId")] tblEmployee tblEmployee)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployees.Add(tblEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEmployee);
        }

        #endregion

        #region GET_EDIT
        public ActionResult Edit(int? id)
        {
            var ListData = (from c in db.tblDepartments
                            select new
                            {
                                DepartmentId = c.Id,
                                DepartmentName = c.DepartmentName
                            }).ToList();

            ViewBag.Departments = ListData;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }
        #endregion

        #region POST_EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailId,DepartmentId")] tblEmployee tblEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEmployee);
        }
        #endregion

        #region GET_DELETE
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            if (tblEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployee);
        }
        #endregion

        #region POST_DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmployee tblEmployee = db.tblEmployees.Find(id);
            db.tblEmployees.Remove(tblEmployee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

    }
}
