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

    public class UsuariosController : Controller
    {
        private Check035Entities db = new Check035Entities();

        [Authorize(Roles = "Admin,Admin_SyS")]
        // GET: Usuarios
        public ActionResult Index()
        {
            var eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Include(e => e.ERGOS_Empresas_N01).Include(e => e.ERGOS_Roles_N01);
            return View(eRGOS_Usuarios_N01.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(id);
            if (eRGOS_Usuarios_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Usuarios_N01);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Admin_SyS")]
        public ActionResult Create()
        {
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            ViewBag.id_rol = new SelectList(db.ERGOS_Roles_N01.Where(R => R.id_rol != 4), "id_rol", "Nombre_Rol");
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");

            return View();
        }

        [Authorize(Roles = "Admin_SyS")]
        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_user,User_Nombre,User_Password,id_rol,id_empresa,id_centro_trabajo,email")] ERGOS_Usuarios_N01 eRGOS_Usuarios_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Usuarios_N01.Add(eRGOS_Usuarios_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            ViewBag.id_rol = new SelectList(db.ERGOS_Roles_N01.Where(R => R.id_rol != 4), "id_rol", "Nombre_Rol");
            return View(eRGOS_Usuarios_N01);
        }

        // GET: Usuarios/Edit/5
        //[Authorize(Roles = "Admin_SyS")]
        public ActionResult Edit(int? id, int? Encuesta_Admin)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Encuesta_Admin == null)
            {
                ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(id);
                if (eRGOS_Usuarios_N01 == null)
                {
                    return HttpNotFound();
                }
                ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Usuarios_N01.id_empresa);
                ViewBag.id_rol = new SelectList(db.ERGOS_Roles_N01.Where(R => R.id_rol != 4), "id_rol", "Nombre_Rol", eRGOS_Usuarios_N01.id_rol);
                ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(E => E.deleted_at == null).Where(E => E.id_empresa == eRGOS_Usuarios_N01.id_empresa), "id_centro_trabajo", "Nombre_centro_trabajo", eRGOS_Usuarios_N01.id_centro_trabajo);
                return View(eRGOS_Usuarios_N01);
            }
            else
            {
                int id_user = db.ERGOS_Usuarios_N01.Where(x => x.id_cuestionario_trabajador == id).Select(x => x.id_user).FirstOrDefault();
                ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(id_user);
                if (eRGOS_Usuarios_N01 == null)
                {
                    return HttpNotFound();
                }
                ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Usuarios_N01.id_empresa);
                ViewBag.id_rol = new SelectList(db.ERGOS_Roles_N01.Where(R => R.id_rol != 4), "id_rol", "Nombre_Rol", eRGOS_Usuarios_N01.id_rol);
                ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(E => E.deleted_at == null).Where(E => E.id_empresa == eRGOS_Usuarios_N01.id_empresa), "id_centro_trabajo", "Nombre_centro_trabajo", eRGOS_Usuarios_N01.id_centro_trabajo);
                return View(eRGOS_Usuarios_N01);
            }
          
        }


        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ERGOS_Usuarios_N01 eRGOS_Usuarios_N01)
        {
            if (User.IsInRole("Guest") || User.IsInRole("Final_Guest"))
            {
                return RedirectToAction("Index", "Home");
            }
          

            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Usuarios_N01).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Admin-Guest") || User.IsInRole("Admin_Centro"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else { 
                    return RedirectToAction("Index");
                }
            }
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Usuarios_N01.id_empresa);
            ViewBag.id_rol = new SelectList(db.ERGOS_Roles_N01.Where(R => R.id_rol != 4), "id_rol", "Nombre_Rol", eRGOS_Usuarios_N01.id_rol);
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            return View(eRGOS_Usuarios_N01);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string User_Name, string current_pass, string new_pass)
        {
            try
            {
                db.Database.ExecuteSqlCommand("EXECUTE Changing_Password @User_Name ='" + User_Name + "', @Pass ='" + current_pass + "', @New_pass ='" + new_pass + "'");
                db.SaveChanges();

                string mensaje = "Cambio de Contraseña Exitoso";
                return Json(new { mensaje });
            }
            catch (Exception ex)
            {
                throw ex;
                string mensaje = "Error al Cambiar la Contraseña";
                return Json(new { mensaje });
            }


        }

        [Authorize(Roles = "Admin_SyS")]
        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(id);
            if (eRGOS_Usuarios_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Usuarios_N01);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(id);
            db.ERGOS_Usuarios_N01.Remove(eRGOS_Usuarios_N01);
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
