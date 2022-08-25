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
    public class PoliticasController : Controller
    {
        private Check035Entities db = new Check035Entities();

        // GET: Politicas
        public ActionResult Index()
        {
            return View(db.Comunica_Politicas_N01.ToList());
        }

        // GET: Politicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunica_Politicas_N01 comunica_Politicas_N01 = db.Comunica_Politicas_N01.Find(id);
            if (comunica_Politicas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(comunica_Politicas_N01);
        }

        // GET: Politicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Politicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descripcion,file_path,notas,created_by,created_at,updated_at,deleted_at")] Comunica_Politicas_N01 comunica_Politicas_N01)
        {
            if (ModelState.IsValid)
            {
                db.Comunica_Politicas_N01.Add(comunica_Politicas_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comunica_Politicas_N01);
        }

        // GET: Politicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunica_Politicas_N01 comunica_Politicas_N01 = db.Comunica_Politicas_N01.Find(id);
            if (comunica_Politicas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(comunica_Politicas_N01);
        }

        // POST: Politicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descripcion,file_path,notas,created_by,created_at,updated_at,deleted_at")] Comunica_Politicas_N01 comunica_Politicas_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comunica_Politicas_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comunica_Politicas_N01);
        }

        // GET: Politicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunica_Politicas_N01 comunica_Politicas_N01 = db.Comunica_Politicas_N01.Find(id);
            if (comunica_Politicas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(comunica_Politicas_N01);
        }

        // POST: Politicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comunica_Politicas_N01 comunica_Politicas_N01 = db.Comunica_Politicas_N01.Find(id);
            db.Comunica_Politicas_N01.Remove(comunica_Politicas_N01);
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
