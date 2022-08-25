using ErgoSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    public class NOM035Controller : Controller
    {
        private Check035Entities db = new Check035Entities();
        [Authorize]
        public ActionResult Index()
        {

            ViewBag.Checklists = (from Data in db.Check_checklist_N01
                                  where Data.Razon_social != null
                                  select Data.id).Count();
            ViewBag.Empresas_No = (from Data in db.ERGOS_Empresas_N01
                                   where Data.deleted_at == null
                                   select Data.id_empresa).Count();
            ViewBag.Surveys_NOM035 = (from Data in db.ERGOS_Cuestionarios_Trabajador_N01
                                      where Data.ERGOS_Empresas_N01.deleted_at == null && (Data.id_encuesta == 1 || Data.id_encuesta == 2 || Data.id_encuesta == 3)
                                      select Data.id_cuestionario_trabajador).Count();
            ViewBag.Surveys_CLIMA = (from Data in db.ERGOS_Cuestionarios_Trabajador_N01
                                     where Data.ERGOS_Empresas_N01.deleted_at == null && Data.id_encuesta == 4
                                     select Data.id_cuestionario_trabajador).Count();
            ViewBag.Surveys_E360 = (from Data in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where Data.ERGOS_Empresas_N01.deleted_at == null && Data.id_encuesta == 5
                                    select Data.id_cuestionario_trabajador).Count();


            ViewBag.Users_No = (from Data in db.ERGOS_Cuestionarios_Trabajador_N01
                                where Data.ERGOS_Empresas_N01.deleted_at == null && (Data.id_encuesta == 1 || Data.id_encuesta == 2 || Data.id_encuesta == 3) && Data.Survey_Status == 100
                                select Data.id_cuestionario_trabajador).Count();
            ViewBag.Centros_Trabajo = (from Data in db.ERGOS_Centros_Trabajo_N01
                                       where Data.deleted_at == null && Data.ERGOS_Empresas_N01.deleted_at == null
                                       select Data.id_centro_trabajo).Count();


            string UserName = User.Identity.Name;
            var id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_empresa).FirstOrDefault();
            int? id = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 3 select E.id_centro_trabajo).FirstOrDefault();
            ViewBag.My_Survey = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id_empresa select E.id_encuesta).FirstOrDefault();
            ViewBag.Centros_Trabajo_ae = (from Data in db.ERGOS_Centros_Trabajo_N01
                                          where Data.deleted_at == null && Data.ERGOS_Empresas_N01.deleted_at == null && Data.id_empresa == id_empresa
                                          select Data.id_centro_trabajo).Count();

            return View();
        }
    }
}