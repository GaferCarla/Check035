using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ErgoSalud.Models;

namespace ErgoSalud.Controllers
{
    [Authorize(Roles = "Admin_SyS")]
    public class grow_adminController : Controller
    {
        private Check035Entities db = new Check035Entities();

        // GET: grow_admin
        public ActionResult Index()
        {
            return View(db.Check_checklist_N01.ToList());
        }

        // GET: grow_admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_checklist_N01 check_checklist_N01 = db.Check_checklist_N01.Find(id);
            if (check_checklist_N01 == null)
            {
                return HttpNotFound();
            }
            return View(check_checklist_N01);
        }

        // GET: grow_admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: grow_admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Email,Comments,Calificacion,created_at,Razon_social")] Check_checklist_N01 check_checklist_N01)
        {
            if (ModelState.IsValid)
            {
                db.Check_checklist_N01.Add(check_checklist_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(check_checklist_N01);
        }

        // GET: grow_admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_checklist_N01 check_checklist_N01 = db.Check_checklist_N01.Find(id);
            if (check_checklist_N01 == null)
            {
                return HttpNotFound();
            }
            return View(check_checklist_N01);
        }

        // POST: grow_admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Email,Comments,Calificacion,created_at,Razon_social")] Check_checklist_N01 check_checklist_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(check_checklist_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(check_checklist_N01);
        }

        // GET: grow_admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_checklist_N01 check_checklist_N01 = db.Check_checklist_N01.Find(id);
            if (check_checklist_N01 == null)
            {
                return HttpNotFound();
            }
            return View(check_checklist_N01);
        }

        // POST: grow_admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Check_checklist_N01 check_checklist_N01 = db.Check_checklist_N01.Find(id);
            db.Check_checklist_N01.Remove(check_checklist_N01);
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
