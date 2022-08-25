using ErgoSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    public class medidas_estatusController : Controller
    {
        private Check035Entities db = new Check035Entities();
        // GET: medidas_estatus
        [Authorize]
        public ActionResult Index()
        {
            string UserName = User.Identity.Name; 
            var info_user = db.ERGOS_Usuarios_N01.Where(x => x.User_Nombre == UserName).FirstOrDefault();
            var info_checklist_37 = db.Check_checklist_medidas_N01.Where(x => x.id_user == info_user.id_user).FirstOrDefault();
            if (info_checklist_37 != null)
            { 
                return RedirectToAction("listado_Evidencia", "Checklist", info_checklist_37);
            }
            else
            {
                return RedirectToAction("Medidas37", "Checklist");
            }
        }
        [Authorize]
        public ActionResult calendario()
        {
            return View();
        }


        public JsonResult GetEvents()
        {
            using (Check035Entities db = new Check035Entities())
            {
                string UserName = User.Identity.Name;
                var info_user = db.ERGOS_Usuarios_N01.Where(x => x.User_Nombre == UserName).FirstOrDefault();
                var events = (from P in db.Check_checklist_medidas_acciones_N01 where P.Check_checklist_medidas_N01.id_user == info_user.id_user select new { P.fecha_inicio, P.accion, P.id_medida, P.fecha_fin,  P.estatus }).ToList();
 
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}