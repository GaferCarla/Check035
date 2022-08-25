using ErgoSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using Ionic.Zip;
//using System.Net.Mail; 
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Cryptography;

namespace ErgoSalud.Controllers
{
    public class HomeController : Controller
    {

        public string Text = "";
        public string Clase = "";
        private Check035Entities db = new Check035Entities();
        public ActionResult Access_Denied()
        {
            return View();
        }
        public ActionResult E360_subordinados()
        {
            ViewBag.Evaluador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01.Where(x => x.id_empresa == 112), "id_cuestionario_trabajador", "id_trabajador");
            ViewBag.Evaluado = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01.Where(x => x.id_empresa == 112), "id_cuestionario_trabajador", "id_trabajador");
            return View();
        }
        public ActionResult Checklist_Results(int CT1, int CT2, int CT3, int CT4, int CT5, string seleccionados,
            int Q1, int Q2, int Q3, int Q4, int Q5, int Q6, int Q7, int Q8, int Q9, int Q10,
            int Q11, int Q12, int Q13, int Q14, int Q15, int Q16, int Q17, int Q18, int Q19, int Q20,
            int Q21, int Q22, int Q23, int Q24, int Q25, int Q26, int Q27, int Q28, int Q29, int Q30,
            int Q31, int Q32, int Q33, int Q34, int Q35, int Q36, int Q37)
        {
            ViewBag.CT1 = CT1;
            ViewBag.CT2 = CT2;
            ViewBag.CT3 = CT3;
            ViewBag.CT4 = CT4;
            ViewBag.CT5 = CT5;
            ViewBag.QTY_Q1 = Q1;
            ViewBag.QTY_Q2 = Q2;
            ViewBag.QTY_Q3 = Q3;
            ViewBag.QTY_Q4 = Q4;
            ViewBag.QTY_Q5 = Q5;
            ViewBag.QTY_Q6 = Q6;
            ViewBag.QTY_Q7 = Q7;
            ViewBag.QTY_Q8 = Q8;
            ViewBag.QTY_Q9 = Q9;
            ViewBag.QTY_Q10 = Q10;
            ViewBag.QTY_Q11 = Q11;
            ViewBag.QTY_Q12 = Q12;
            ViewBag.QTY_Q13 = Q13;
            ViewBag.QTY_Q14 = Q14;
            ViewBag.QTY_Q15 = Q15;
            ViewBag.QTY_Q16 = Q16;
            ViewBag.QTY_Q17 = Q17;
            ViewBag.QTY_Q18 = Q18;
            ViewBag.QTY_Q19 = Q19;
            ViewBag.QTY_Q20 = Q20;
            ViewBag.QTY_Q21 = Q21;
            ViewBag.QTY_Q22 = Q22;
            ViewBag.QTY_Q23 = Q23;
            ViewBag.QTY_Q24 = Q24;
            ViewBag.QTY_Q25 = Q25;
            ViewBag.QTY_Q26 = Q26;
            ViewBag.QTY_Q27 = Q27;
            ViewBag.QTY_Q28 = Q28;
            ViewBag.QTY_Q29 = Q29;
            ViewBag.QTY_Q30 = Q30;
            ViewBag.QTY_Q31 = Q31;
            ViewBag.QTY_Q32 = Q32;
            ViewBag.QTY_Q33 = Q33;
            ViewBag.QTY_Q34 = Q34;
            ViewBag.QTY_Q35 = Q35;
            ViewBag.QTY_Q36 = Q36;
            ViewBag.QTY_Q37 = Q37;
            var lista = new List<int>();
            var contador = 0;
            foreach (char nombre in seleccionados)
            {
                lista.Add(Int32.Parse(nombre.ToString()));
                if (Int32.Parse(nombre.ToString()) == 1)
                {
                    contador = contador + 1;
                }
            }
            ViewBag.lista = lista;
            ViewBag.contador = contador;
            ViewBag.completado = float.Parse(CT5.ToString()) / 8 * 100;
            ViewBag.completado2 = float.Parse(CT4.ToString()) / 5 * 100;
            //ViewBag.lista = new List<int> [R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12, R13, R14, R15, R16, R17, R18, R19, R20, R21, R22, R22, R23, R24, R25, R26, R27, R28, R29, R30, R31, R32, R33, R34, R35, R36, R37];
            return View();
        }
        public ActionResult Checklist_Results_37(int CT1, int CT2, int CT3, int CT4, int CT5, string seleccionados)
        {
            ViewBag.CT1 = CT1;
            ViewBag.CT2 = CT2;
            ViewBag.CT3 = CT3;
            ViewBag.CT4 = CT4;
            ViewBag.CT5 = CT5;
            //ViewBag.QTY_Q1 = Q1;
            //ViewBag.QTY_Q2 = Q2;
            //ViewBag.QTY_Q3 = Q3;
            //ViewBag.QTY_Q4 = Q4;
            //ViewBag.QTY_Q5 = Q5;
            //ViewBag.QTY_Q6 = Q6;
            //ViewBag.QTY_Q7 = Q7;
            //ViewBag.QTY_Q8 = Q8;
            //ViewBag.QTY_Q9 = Q9;
            //ViewBag.QTY_Q10 = Q10;
            //ViewBag.QTY_Q11 = Q11;
            //ViewBag.QTY_Q12 = Q12;
            //ViewBag.QTY_Q13 = Q13;
            //ViewBag.QTY_Q14 = Q14;
            //ViewBag.QTY_Q15 = Q15;
            //ViewBag.QTY_Q16 = Q16;
            //ViewBag.QTY_Q17 = Q17;
            //ViewBag.QTY_Q18 = Q18;
            //ViewBag.QTY_Q19 = Q19;
            //ViewBag.QTY_Q20 = Q20;
            //ViewBag.QTY_Q21 = Q21;
            //ViewBag.QTY_Q22 = Q22;
            //ViewBag.QTY_Q23 = Q23;
            //ViewBag.QTY_Q24 = Q24;
            //ViewBag.QTY_Q25 = Q25;
            //ViewBag.QTY_Q26 = Q26;
            //ViewBag.QTY_Q27 = Q27;
            //ViewBag.QTY_Q28 = Q28;
            //ViewBag.QTY_Q29 = Q29;
            //ViewBag.QTY_Q30 = Q30;
            //ViewBag.QTY_Q31 = Q31;
            //ViewBag.QTY_Q32 = Q32;
            //ViewBag.QTY_Q33 = Q33;
            //ViewBag.QTY_Q34 = Q34;
            //ViewBag.QTY_Q35 = Q35;
            //ViewBag.QTY_Q36 = Q36;
            //ViewBag.QTY_Q37 = Q37;
            var lista = new List<int>();
            var contador = 0;
            foreach (char nombre in seleccionados)
            {
                lista.Add(Int32.Parse(nombre.ToString()));
                if (Int32.Parse(nombre.ToString()) == 1)
                {
                    contador = contador + 1;
                }
            }
            ViewBag.lista = lista;
            ViewBag.contador = contador;
            ViewBag.completado = float.Parse(CT5.ToString()) / 8 * 100;
            ViewBag.completado2 = float.Parse(CT4.ToString()) / 5 * 100;
            //ViewBag.lista = new List<int> [R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12, R13, R14, R15, R16, R17, R18, R19, R20, R21, R22, R22, R23, R24, R25, R26, R27, R28, R29, R30, R31, R32, R33, R34, R35, R36, R37];
            return View();
        }
        [HttpPost]
        public JsonResult Insert_subordinado(int id_CT, int id_CT_Evaluado)
        {
            var commandText = "EXECUTE sp_adding_E360_subordinado @id_CT = @id_CT  ,@id_CT_Evaluado = @id_CT_Evaluado  ";
            var _id_CT = new SqlParameter("@id_CT", id_CT);
            var _id_CT_Evaluado = new SqlParameter("@id_CT_Evaluado", id_CT_Evaluado);
            db.Database.ExecuteSqlCommand(commandText, _id_CT, _id_CT_Evaluado);
            db.SaveChanges();

            string ok = "ok";
            return Json(ok, JsonRequestBehavior.AllowGet);
        }
        public ActionResult completed()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Admin_SyS")]
        public ActionResult multi_menu()
        {

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
            ViewBag.Users_No = (from Data in db.ERGOS_Usuarios_N01
                                select Data.id_user).Count();

            ViewBag.Centros_Trabajo = (from Data in db.ERGOS_Centros_Trabajo_N01
                                       where Data.deleted_at == null
                                       select Data.id_centro_trabajo).Count();
            ViewBag.id_empresa_Visitante = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null)
                .Where(A => A.id_empresa == 30), "id_empresa", "Razon_Social");
            return View();

        }
        [Authorize]
        public ActionResult Index()
        { 
            return View();
        }
        public ActionResult Download_ZIP()
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Server.MapPath("~/Directories/hello"));
                zip.Save(Server.MapPath("~/Directories/hello/sample.zip"));
                return File(Server.MapPath("~/Directories/hello/sample.zip"),
                                           "application/zip", "sample.zip");
            }
        }
        [Authorize(Roles = "Guest,Final_Guest")]
        public ActionResult Mi_Cuestionario()
        {

            string UserName = User.Identity.Name;
            int? id_cuestionario_trabajador = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 6 select E.id_cuestionario_trabajador).FirstOrDefault();
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 6 select E.id_empresa).FirstOrDefault();
            int id_encuesta = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id_empresa select E.id_encuesta).FirstOrDefault();

            if (id_encuesta == 5)
            {
                ViewBag.id_encuesta = id_encuesta;
                return View(db.E360_Cuestionario_Resultado_N01.Where(x => x.id_cuestionario_trabajador == id_cuestionario_trabajador));
            }
            else if (id_encuesta == 4) {

                ViewBag.id_encuesta = id_encuesta;
                return RedirectToAction("Mi_Cuestionario_Clima_Laboral", new { id_CT = id_cuestionario_trabajador });
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "Final_Guest")]
        public ActionResult Mi_Cuestionario_Clima_Laboral(int id_CT)
        {
            return View(db.ClimaLaboral_Cuestionario_Resultados_N01.Where(x => x.id_cuestionario_trabajador == id_CT));
        }
        [AllowAnonymous]
        public ActionResult checklist(string _token)
        {
            if (_token == "ddffsd")
            {
                ViewBag.id_evaluacion = new SelectList(db.ERGOS_Cuestionarios_N01.Where(x => x.id_cuestionario != 4).Where(x => x.id_cuestionario != 5).Where(x => x.id_cuestionario != 1), "id_cuestionario", "Cuestionario");
                return View();
            }
            else
            {
                return RedirectToAction("Access_Denied");
            }
        }
  
        [AllowAnonymous]
        public ActionResult admin_register(string _token)
        {
            if (_token == "Aj75sdA67ykl23")
            {
                ViewBag.id_evaluacion = new SelectList(db.ERGOS_Cuestionarios_N01.Where(x => x.id_cuestionario != 4).Where(x => x.id_cuestionario != 5).Where(x => x.id_cuestionario != 1), "id_cuestionario", "Cuestionario");
                return View();
            }
            else
            {
                return RedirectToAction("Access_Denied");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult admin_register(admin_register model)
        {

            if (model.Telefono == null) {
                model.Telefono = "NA";
            }

            if (model.Domicilio == null)
            {
                model.Domicilio = "NA";
            }

            if (model.Actividad_Principal == null)
            {
                model.Actividad_Principal = "NA";
            }
            if (model.Email == null)
            {
                model.Email = "NA";
            }

          

            if (ModelState.IsValid)
            {
                var commandText = "EXECUTE sp_create_admin_user_company @user = @User_Nombre_DB, @pass = @User_Password_DB ,@id_encuesta = @id_encuesta ,@empresa = @empresa, @tel = @tel,@rfc = @rfc ,@contacto = @contacto ,@actividad = @actividad, @email = @email, @fecha = @fecha, @domicilio = @domicilio";
                var name = new SqlParameter("@User_Nombre_DB", model.User_Nombre);
                var pass = new SqlParameter("@User_Password_DB", model.User_Password);
                var id_encuesta = new SqlParameter("@id_encuesta", model.id_evaluacion);
                var empresa = new SqlParameter("@empresa", model.Razon_Social);
                var telefono = new SqlParameter("@tel", model.Telefono);
                var rfc = new SqlParameter("@rfc", model.RFC);
                var contacto = new SqlParameter("@contacto", model.Contacto_Nombre);
                var actividad = new SqlParameter("@actividad", model.Actividad_Principal);
                var email = new SqlParameter("@email", model.Email);
                var fecha = new SqlParameter("@fecha", model.Fecha_Aplicacion);
                var domicilio = new SqlParameter("@domicilio", model.Domicilio);
                db.Database.ExecuteSqlCommand(commandText, name, pass, id_encuesta, empresa, telefono, rfc, contacto, actividad, email, fecha, domicilio);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            else
            {
                ViewBag.id_evaluacion = new SelectList(db.ERGOS_Cuestionarios_N01.Where(x => x.id_cuestionario != 4).Where(x => x.id_cuestionario != 5).Where(x => x.id_cuestionario != 1), "id_cuestionario", "Cuestionario",model.id_evaluacion);

                return View(model);
            }
           
        }
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }

        public ActionResult buzon()
        {
            return View();
        }

        public ActionResult dashboard()
        {
            return View();
        }

        public ActionResult Comunica()
        {
            return View();
        }

        public ActionResult comunica_feed()
        {
            return View();
        }

        public ActionResult comunica_form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ERGOS_Usuarios_N01 model, string returnUrl)
        {
            try
            {
                Check035Entities db = new Check035Entities();
                var dataItem = db.ERGOS_Usuarios_N01.Where(x => x.User_Nombre == model.User_Nombre && x.User_Password == model.User_Password).First();
                if (dataItem != null)
                {
                    FormsAuthentication.SetAuthCookie(dataItem.User_Nombre, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                             && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        var commandText = "EXECUTE log_data_login @user = @User_Nombre_DB  ,@pass = @User_Password_DB  ,@Status = @Estatus_DB";
                        var name = new SqlParameter("@User_Nombre_DB", model.User_Nombre);
                        var pass = new SqlParameter("@User_Password_DB", model.User_Password);
                        var status = new SqlParameter("@Estatus_DB", "1");
                        db.Database.ExecuteSqlCommand(commandText, name, pass, status);
                        db.SaveChanges();

                        if (User.IsInRole("Guest"))
                        {
                            return Redirect(returnUrl);
                        }
                        else if (User.IsInRole("Admin-Guest") || User.IsInRole("Admin_Centro"))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        var commandText = "EXECUTE log_data_login @user = @User_Nombre_DB  ,@pass = @User_Password_DB  ,@Status = @Estatus_DB";
                        var name = new SqlParameter("@User_Nombre_DB", model.User_Nombre);
                        var pass = new SqlParameter("@User_Password_DB", model.User_Password);
                        var status = new SqlParameter("@Estatus_DB", "1");
                        db.Database.ExecuteSqlCommand(commandText, name, pass, status);
                        db.SaveChanges();


                        if (User.IsInRole("Guest"))
                        {
                            return Redirect(returnUrl);
                        }
                        else if (User.IsInRole("Admin-Guest") || User.IsInRole("Admin_Centro")) 
                        {
                            return RedirectToAction("Index");
                        }
                        else 
                        {
                            return RedirectToAction("Index");
                        }

                    }
                }
                else
                {
                    var commandText = "EXECUTE log_data_login @user = @User_Nombre_DB  ,@pass = @User_Password_DB  ,@Status = @Estatus_DB";
                    var name = new SqlParameter("@User_Nombre_DB", model.User_Nombre);
                    var pass = new SqlParameter("@User_Password_DB", model.User_Password);
                    var status = new SqlParameter("@Estatus_DB", "0");
                    db.Database.ExecuteSqlCommand(commandText, name, pass, status);
                    db.SaveChanges();

                    ModelState.AddModelError("", "Invalid user/pass");
                    return View();
                }
            }
            catch
            {

                var commandText = "EXECUTE log_data_login @user = @User_Nombre_DB  ,@pass = @User_Password_DB  ,@Status = @Estatus_DB";
                var name = new SqlParameter("@User_Nombre_DB", model.User_Nombre);
                var pass = new SqlParameter("@User_Password_DB", model.User_Password);
                var status = new SqlParameter("@Estatus_DB", "999");
                db.Database.ExecuteSqlCommand(commandText, name, pass, status);
                db.SaveChanges();


                TempData["LOGIN"] = "Credenciales no validas";
                return View();

            }

        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    

    }
}