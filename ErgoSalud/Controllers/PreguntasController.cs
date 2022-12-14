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
    public class PreguntasController : Controller
    {
        private Check035Entities db = new Check035Entities();

        // GET: Preguntas
        public ActionResult Index()
        {
            var eRGOS_Preguntas_N01 = db.ERGOS_Preguntas_N01.Include(e => e.ERGOS_Cuestionarios_N01);
            return View(eRGOS_Preguntas_N01.ToList());
        }

        // GET: Preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Preguntas_N01 eRGOS_Preguntas_N01 = db.ERGOS_Preguntas_N01.Find(id);
            if (eRGOS_Preguntas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Preguntas_N01);
        }

        // GET: Preguntas/Create
        public ActionResult Create()
        {
            ViewBag.id_cuestionario = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario");
            return View();
        }

        // POST: Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pregunta,Preguntas,id_cuestionario,No_Pregunta")] ERGOS_Preguntas_N01 eRGOS_Preguntas_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Preguntas_N01.Add(eRGOS_Preguntas_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cuestionario = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Preguntas_N01.id_cuestionario);
            return View(eRGOS_Preguntas_N01);
        }

        // GET: Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Preguntas_N01 eRGOS_Preguntas_N01 = db.ERGOS_Preguntas_N01.Find(id);
            if (eRGOS_Preguntas_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cuestionario = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Preguntas_N01.id_cuestionario);
            return View(eRGOS_Preguntas_N01);
        }

        // POST: Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pregunta,Preguntas,id_cuestionario,No_Pregunta")] ERGOS_Preguntas_N01 eRGOS_Preguntas_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Preguntas_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cuestionario = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Preguntas_N01.id_cuestionario);
            return View(eRGOS_Preguntas_N01);
        }

        // GET: Preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Preguntas_N01 eRGOS_Preguntas_N01 = db.ERGOS_Preguntas_N01.Find(id);
            if (eRGOS_Preguntas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Preguntas_N01);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Preguntas_N01 eRGOS_Preguntas_N01 = db.ERGOS_Preguntas_N01.Find(id);
            db.ERGOS_Preguntas_N01.Remove(eRGOS_Preguntas_N01);
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
