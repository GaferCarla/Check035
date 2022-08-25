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
    public class Avisos_NotificacionesController : Controller
    {
        private Check035Entities db = new Check035Entities();

        // GET: Avisos_Notificaciones
        public ActionResult Index()
        {
            var h = db.Comunica_Avisos_N01.ToList();
            return View(db.Comunica_Avisos_N01.ToList());
        }
        public ActionResult Index2()
        {
            var h = db.Comunica_Avisos_N01.ToList();
            return View(db.Comunica_Avisos_N01.ToList());
        }

        // GET: Avisos_Notificaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunica_Avisos_N01 comunica_Avisos_N01 = db.Comunica_Avisos_N01.Find(id);
            if (comunica_Avisos_N01 == null)
            {
                return HttpNotFound();
            }
            return View(comunica_Avisos_N01);
        }

        // GET: Avisos_Notificaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avisos_Notificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,prioridad,aviso,rango_vision,fecha_inicio,fecha_fin,notas,created_by,updated_by,deleted_by,created_at,updated_at,deleted_at")] Comunica_Avisos_N01 comunica_Avisos_N01)
        {
            if (ModelState.IsValid)
            {
                db.Comunica_Avisos_N01.Add(comunica_Avisos_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comunica_Avisos_N01);
        }

        // GET: Avisos_Notificaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunica_Avisos_N01 comunica_Avisos_N01 = db.Comunica_Avisos_N01.Find(id);
            if (comunica_Avisos_N01 == null)
            {
                return HttpNotFound();
            }
            return View(comunica_Avisos_N01);
        }

        // POST: Avisos_Notificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,prioridad,aviso,rango_vision,fecha_inicio,fecha_fin,notas,created_by,updated_by,deleted_by,created_at,updated_at,deleted_at")] Comunica_Avisos_N01 comunica_Avisos_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comunica_Avisos_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comunica_Avisos_N01);
        }

        // GET: Avisos_Notificaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comunica_Avisos_N01 comunica_Avisos_N01 = db.Comunica_Avisos_N01.Find(id);
            if (comunica_Avisos_N01 == null)
            {
                return HttpNotFound();
            }
            return View(comunica_Avisos_N01);
        }

        // POST: Avisos_Notificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comunica_Avisos_N01 comunica_Avisos_N01 = db.Comunica_Avisos_N01.Find(id);
            db.Comunica_Avisos_N01.Remove(comunica_Avisos_N01);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteConfirmed2(int id)
        {
            Comunica_Avisos_N01 comunica_Avisos_N01 = db.Comunica_Avisos_N01.Find(id);
            db.Comunica_Avisos_N01.Remove(comunica_Avisos_N01);
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
