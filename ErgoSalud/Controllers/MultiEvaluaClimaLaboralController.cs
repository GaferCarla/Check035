using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ErgoSalud.Models;
using SelectPdf;

namespace ErgoSalud.Controllers
{
    public class MultiEvaluaClimaLaboralController : Controller
    {
        private Check035Entities db = new Check035Entities();
        public string[] Resultados_Colores = new string[9];
        public int flag_editar = 0;
        private static string promedio_color(double? promedio)
        {
            string color;
            if (promedio < 39)
                color = "red";
            else if (promedio >= 39 && promedio < 60)
                color = "yellow";
            else if (promedio >= 60 && promedio < 80)
                color = "orange";
            else if (promedio >= 80 && promedio < 100)
                color = "lightgreen";
            else if (promedio == 100)
                color = "green";
            else
                color = "white";
            return color;
        }
        // GET: ClimaLaboral
        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult Index()
        {
            var climaLaboral_Cuestionario_Resultados_N01 = db.ClimaLaboral_Cuestionario_Resultados_N01.Include(c => c.ERGOS_Cuestionarios_Trabajador_N01);
            return View(climaLaboral_Cuestionario_Resultados_N01.ToList());
        }
        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult Home()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult Enviar_encuestas_Ind(int id_empresa, int? id_CT)
        {
            string UserName = User.Identity.Name;
            string usuario = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == id_CT select E.User_Nombre).FirstOrDefault();
            string password = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == id_CT select E.User_Password).FirstOrDefault();

            var datos_correos = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa && E.id_cuestionario_trabajador == id_CT && E.Email != null select new { E.Nombre, E.Email, E.id_cuestionario_trabajador, E.id_encuesta }).ToArray();

            int flag = 1;
            var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                        where R.id_cuestionario_trabajador == id_CT
                        select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

            flag_editar = 0;
            if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
            {
                flag_editar = 1;
            }
            foreach (var correo in datos_correos)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.ionos.mx", 587);
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("identifica@check035.com", "sRS2Bz$!7@GnN6W");
                    mail.From = new MailAddress("No-Reply@check035.com", "Identifica NOM-035");
                    mail.Subject = "Evaluación Clima Laboral";
                    mail.IsBodyHtml = true;
                    SmtpServer.EnableSsl = true;

                    int id_Clima = (from E in db.ClimaLaboral_Cuestionario_Resultados_N01 where E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.id_cuestionario_resultado).FirstOrDefault();
                    mail.To.Add(correo.Email);

                    if (flag_editar == 1)
                    {
                        mail.Body = "<html> <head> <body> <div> <b>Buenos días: " + correo.Nombre + "</b><br>" +
                       "<p>Hoy arrancamos un proceso muy importante, en el cual tu participación es fundamental.</p>" +
                       "<p>Te invitamos a acceder a la siguiente liga para contestar tu encuesta:  <a href='https://identifica.check035.com/Encuestas/Edit/" + correo.id_cuestionario_trabajador + "'>Mi Encuesta</a><p>" +
                       "<p><br><br><b>Usuario:</b> " + usuario + " <Br><b> Contraseña:</b> " + password + " <p>" +
                       "<p> Los Exploradores recomendados son<br> 1.- Chrome <br>2.- Fire Fox<br>3.- Opera<br><b>Explorer</b> presenta conflictos con algunas funciones de la aplicación.<p>" +
                       "</div> </body> </html>";
                    }
                    else
                    {

                        mail.Body = "<html> <head> <body> <div> <b>Buenos días: " + correo.Nombre + "</b><br>" +
                       "<p>Hoy arrancamos un proceso muy importante, en el cual tu participación es fundamental.</p>" +
                       "<p>Te invitamos a acceder a la siguiente liga para contestar tu encuesta:  <a href='https://check035.com/MultiEvaluaClimaLaboral/Edit?id=" + id_Clima + "'>Mi Encuesta</a><p>" +
                       "<p><br><br><b>Usuario:</b> " + usuario + " <Br><b> Contraseña:</b> " + password + " <p>" +
                       "<p> Los Exploradores recomendados son<br> 1.- Chrome <br>2.- Fire Fox<br>3.- Opera<br><b>Explorer</b> presenta conflictos con algunas funciones de la aplicación.<p>" +
                       "</div> </body> </html>";
                    }


                    SmtpServer.Send(mail);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 1);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 1");
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 0);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 0");
                    db.SaveChanges();
                    flag = 2;
                }
            }

            if (User.IsInRole("Admin-Guest") || User.IsInRole("Guest") || User.IsInRole("Admin_Centro"))
                return RedirectToAction("Encuestas_Admin", new { User_Name = UserName, flag });
            else
                return RedirectToAction("Index", new { flag });
        }

        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult General_Statistics_Centro(int id_e, int id_c)
        {
            try
            {
                var Final_Empresa = db.fn_MultiEvalua_ClimaLaboral_Centro_Trabajo(id_e, id_c).FirstOrDefault();
                Resultados_Colores[0] = promedio_color(Final_Empresa.PROMEDIO_C_I_100);
                Resultados_Colores[1] = promedio_color(Final_Empresa.PROMEDIO_C_II_100);
                Resultados_Colores[2] = promedio_color(Final_Empresa.PROMEDIO_C_III_100);
                Resultados_Colores[3] = promedio_color(Final_Empresa.PROMEDIO_C_IV_100);
                Resultados_Colores[4] = promedio_color(Final_Empresa.PROMEDIO_C_V_100);
                Resultados_Colores[5] = promedio_color(Final_Empresa.PROMEDIO_C_VI_100);
                Resultados_Colores[6] = promedio_color(Final_Empresa.PROMEDIO_C_VII_100);
                Resultados_Colores[7] = promedio_color(Final_Empresa.PROMEDIO_C_VIII_100);
                Resultados_Colores[8] = promedio_color(Final_Empresa.PROMEDIO_C_IX_100);

                ViewBag.Evaluaciones_counter = (from CL in db.ClimaLaboral_Cuestionario_Resultados_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();

                ViewBag.Final_Empresa = Final_Empresa;
                ViewBag.Resultados_Colores = Resultados_Colores;
                return View(db.ERGOS_Centros_Trabajo_N01.Find(id_c));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult Reporte_General_PDF(int id)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre_empresa = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id select E.Razon_Social).FirstOrDefault();
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            converter.Options.DisplayHeader = true;
            converter.Options.DisplayFooter = true;
            converter.Header.DisplayOnFirstPage = false;
            converter.Footer.DisplayOnFirstPage = false;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Options.MaxPageLoadTime = 99999;
            converter.Header.Height = 30;
            converter.Footer.Height = 30;
            converter.Options.WebPageWidth = 1265;
            //converter.Options.PdfPageSize = PdfPageSize.A4;
            //converter.Options.WebPageWidth = 1550;
            //converter.Options.MarginLeft = 0;
            //converter.Options.MarginRight = 0;
            //converter.Options.MarginTop = 10;
            //converter.Options.MarginBottom = 20;
            //converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReGCLYbqtKJHGFDE87654asauIhmdsaJ6fobQAreA9mBUt4dmBa4Z7?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_ClimaLaboral_General_" + Nombre_empresa + ".pdf");
        }
        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult General_Statistics(int id_e)
        {
            try
            {
                var Final_Empresa = db.fn_MultiEvalua_ClimaLaboral_Empresa(id_e).FirstOrDefault();
                Resultados_Colores[0] = promedio_color(Final_Empresa.PROMEDIO_C_I_100);
                Resultados_Colores[1] = promedio_color(Final_Empresa.PROMEDIO_C_II_100);
                Resultados_Colores[2] = promedio_color(Final_Empresa.PROMEDIO_C_III_100);
                Resultados_Colores[3] = promedio_color(Final_Empresa.PROMEDIO_C_IV_100);
                Resultados_Colores[4] = promedio_color(Final_Empresa.PROMEDIO_C_V_100);
                Resultados_Colores[5] = promedio_color(Final_Empresa.PROMEDIO_C_VI_100);
                Resultados_Colores[6] = promedio_color(Final_Empresa.PROMEDIO_C_VII_100);
                Resultados_Colores[7] = promedio_color(Final_Empresa.PROMEDIO_C_VIII_100);
                Resultados_Colores[8] = promedio_color(Final_Empresa.PROMEDIO_C_IX_100);

                ViewBag.Evaluaciones_counter = (from CL in db.ClimaLaboral_Cuestionario_Resultados_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();


                ViewBag.Final_Empresa = Final_Empresa;
                ViewBag.Resultados_Colores = Resultados_Colores;
                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize(Roles = "Admin,Admin_Sys")]
        // GET: ClimaLaboral/Create
        public ActionResult Create()
        {
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion");
            return View();
        }

        // POST: ClimaLaboral/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cuestionario_resultado,id_cuestionario_trabajador,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,Q55,Q56,C_I,C_II,C_III,C_IV,C_V,C_VI,C_VII,C_VIII,C_XI")] ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01)
        {
            if (ModelState.IsValid)
            {
                db.ClimaLaboral_Cuestionario_Resultados_N01.Add(climaLaboral_Cuestionario_Resultados_N01);
                db.SaveChanges();
                TempData["Saving_Message"] = "Evaluación Almacenada Correctamente";
                return View();
            }

            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", climaLaboral_Cuestionario_Resultados_N01.id_cuestionario_trabajador);
            return View(climaLaboral_Cuestionario_Resultados_N01);
        }

        [Authorize(Roles = "Admin,Admin_Sys,Final_Guest")]
        // GET: ClimaLaboral/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01 = db.ClimaLaboral_Cuestionario_Resultados_N01.Find(id);
            if (climaLaboral_Cuestionario_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", climaLaboral_Cuestionario_Resultados_N01.id_cuestionario_trabajador);
            return View(climaLaboral_Cuestionario_Resultados_N01);
        }

        [Authorize(Roles = "Admin,Admin_Sys,Final_Guest")]
        // POST: ClimaLaboral/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cuestionario_resultado,id_cuestionario_trabajador,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,Q55,Q56,C_I,C_II,C_III,C_IV,C_V,C_VI,C_VII,C_VIII,C_XI")] ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01)
        {
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", climaLaboral_Cuestionario_Resultados_N01.id_cuestionario_trabajador);

            if (ModelState.IsValid)
            {
                db.Entry(climaLaboral_Cuestionario_Resultados_N01).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Saving_Edit_Message"] = "Evaluación Almacenada Correctamente";
                return View(climaLaboral_Cuestionario_Resultados_N01);
            }
            return View(climaLaboral_Cuestionario_Resultados_N01);
        }

        [Authorize(Roles = "Admin,Admin_Sys")]
        // GET: ClimaLaboral/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01 = db.ClimaLaboral_Cuestionario_Resultados_N01.Find(id);
            if (climaLaboral_Cuestionario_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            return View(climaLaboral_Cuestionario_Resultados_N01);
        }

        [Authorize(Roles = "Admin,Admin_Sys")]
        // POST: ClimaLaboral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01 = db.ClimaLaboral_Cuestionario_Resultados_N01.Find(id);
            db.ClimaLaboral_Cuestionario_Resultados_N01.Remove(climaLaboral_Cuestionario_Resultados_N01);
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
