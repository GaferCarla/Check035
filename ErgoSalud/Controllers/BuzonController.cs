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
    public class BuzonController : Controller
    {
        private Check035Entities db = new Check035Entities();

        // GET: Buzon
        public ActionResult Index()
        {
            var eRGOS_Buzon_N01 = db.ERGOS_Buzon_N01.Include(e => e.ERGOS_Usuarios_N01);
            return View(eRGOS_Buzon_N01.ToList());
        }

        // GET: Buzon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Buzon_N01 eRGOS_Buzon_N01 = db.ERGOS_Buzon_N01.Find(id);
            if (eRGOS_Buzon_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Buzon_N01);
        }

        // GET: Buzon/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.ERGOS_Usuarios_N01, "id_user", "User_Nombre");
            return View();
        }

        // POST: Buzon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_usuario,tipo,cat_sugerencia,cat_sugerencia_desc,denuncia_relacionada,denuncia_involucrados,denuncia_hechos_desc,denuncia_frecuencia,denuncia_soy,denuncia_mas_involucrados,alta_admin,notas,created_at,updated_at,deleted_at")] ERGOS_Buzon_N01 eRGOS_Buzon_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Buzon_N01.Add(eRGOS_Buzon_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.ERGOS_Usuarios_N01, "id_user", "User_Nombre", eRGOS_Buzon_N01.id_usuario);
            return View(eRGOS_Buzon_N01);
        }

        // GET: Buzon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Buzon_N01 eRGOS_Buzon_N01 = db.ERGOS_Buzon_N01.Find(id);
            if (eRGOS_Buzon_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.ERGOS_Usuarios_N01, "id_user", "User_Nombre", eRGOS_Buzon_N01.id_usuario);
            return View(eRGOS_Buzon_N01);
        }

        // POST: Buzon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_usuario,tipo,cat_sugerencia,cat_sugerencia_desc,denuncia_relacionada,denuncia_involucrados,denuncia_hechos_desc,denuncia_frecuencia,denuncia_soy,denuncia_mas_involucrados,alta_admin,notas,created_at,updated_at,deleted_at")] ERGOS_Buzon_N01 eRGOS_Buzon_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Buzon_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.ERGOS_Usuarios_N01, "id_user", "User_Nombre", eRGOS_Buzon_N01.id_usuario);
            return View(eRGOS_Buzon_N01);
        }

        // GET: Buzon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Buzon_N01 eRGOS_Buzon_N01 = db.ERGOS_Buzon_N01.Find(id);
            if (eRGOS_Buzon_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Buzon_N01);
        }

        // POST: Buzon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Buzon_N01 eRGOS_Buzon_N01 = db.ERGOS_Buzon_N01.Find(id);
            db.ERGOS_Buzon_N01.Remove(eRGOS_Buzon_N01);
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
