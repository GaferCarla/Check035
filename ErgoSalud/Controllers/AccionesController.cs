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
    public class AccionesController : Controller
    {
        private Check035Entities db = new Check035Entities();

       
        // GET: Acciones/Create
        public ActionResult Create()
        {
            ViewBag.id_checklist_37 = new SelectList(db.Check_checklist_medidas_N01, "id", "QR1");
            return View();
        }

        // POST: Acciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_checklist_37,id_medida,accion,id_empresa,id_centro_trabajo,departamento,fecha_inicio,fecha_fin,file_evidencia,estatus,nivel")] Check_checklist_medidas_acciones_N01 check_checklist_medidas_acciones_N01)
        {
            if (ModelState.IsValid)
            {
                db.Check_checklist_medidas_acciones_N01.Add(check_checklist_medidas_acciones_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_checklist_37 = new SelectList(db.Check_checklist_medidas_N01, "id", "QR1", check_checklist_medidas_acciones_N01.id_checklist_37);
            return View(check_checklist_medidas_acciones_N01);
        }

        
        // GET: Acciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_checklist_medidas_acciones_N01 check_checklist_medidas_acciones_N01 = db.Check_checklist_medidas_acciones_N01.Find(id);
            if (check_checklist_medidas_acciones_N01 == null)
            {
                return HttpNotFound();
            }
            return View(check_checklist_medidas_acciones_N01);
        }

        // POST: Acciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Check_checklist_medidas_acciones_N01 check_checklist_medidas_acciones_N01 = db.Check_checklist_medidas_acciones_N01.Find(id);
            db.Check_checklist_medidas_acciones_N01.Remove(check_checklist_medidas_acciones_N01);
            db.SaveChanges();
            return RedirectToAction("Index","Medidas_Estatus");
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
