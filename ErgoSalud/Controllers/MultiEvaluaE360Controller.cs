using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using ErgoSalud.Models;
using SelectPdf;

namespace ErgoSalud.Controllers
{
    //[Authorize(Roles = "Admin,Admin_Sys")]
    public class MultiEvaluaE360Controller : Controller
    {
        private Check035Entities db = new Check035Entities();
        public string[] Resultados_Colores = new string[15];
        public string[] Resultados_Colores_Sup = new string[15];
        public string[] Resultados_Colores_Emp = new string[15];
        public string[] Resultados_Colores_Sub = new string[15];
        public string[] Resultados_Colores_Com = new string[15];
        public string[] Resultados_Colores_Depts = new string[15];

        public int flag_editar = 0;
        public ActionResult Reporte_Departamentos_PDF(int id)
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
           // converter.Options.MinPageLoadTime = 20;
            converter.Header.Height = 30;
            converter.Footer.Height = 30;
            converter.Options.WebPageWidth = 1265; 
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReDeptos360ASsdafhafd9834bfjksdgF7DYTGBsdfghjygfDFGH78?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_E360_General_" + Nombre_empresa + ".pdf");
        }
        public ActionResult Reporte_General_PDF(int id)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre_empresa = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id select E.Razon_Social).FirstOrDefault(); 
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            //converter.Options.PdfPageSize = PdfPageSize.A4;
            //converter.Options.WebPageWidth = 1550;
            //converter.Options.MarginLeft = 0;
            //converter.Options.MarginRight = 0;
            //converter.Options.MarginTop = 10;
            //converter.Options.MarginBottom = 20;
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
            //converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReGE360ASDFD09876TRFGBNJKI49R8F7DYTGBDN4RVSDSDasjddsde3sdsw?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_E360_General_" + Nombre_empresa + ".pdf");
        }
        public ActionResult Reporte_Autoevaluacion_PDF(int id)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre_empresa = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id select E.Razon_Social).FirstOrDefault();
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            //converter.Options.PdfPageSize = PdfPageSize.A4;
            //converter.Options.WebPageWidth = 1550;
            //converter.Options.MarginLeft = 0;
            //converter.Options.MarginRight = 0;
            //converter.Options.MarginTop = 10;
            //converter.Options.MarginBottom = 20;
            //converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
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
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReGE360_EMPfcdusyhegbvrfn8d73y45t9fd8s7y4h5jtfiduyhe4eudyhdhdh444d3sdsw?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_E360_General_" + Nombre_empresa + ".pdf");
        }
        public ActionResult Reporte_Supervisores_PDF(int id)
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
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReGE360_SUPsdiuhygbrvtgnvjciusyg4h5jtig9f8d76t4g5thjgifc8x7s6wtg4tgv654ee3sdsw?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_E360_General_" + Nombre_empresa + ".pdf");
        }
        public ActionResult Reporte_Subordinados_PDF(int id)
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
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReGE360_SUBsuhfdifgdsg87654edcxystgw3b4h5jtigf8d7sywtg34brtjddsde3sdsw?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_E360_General_" + Nombre_empresa + ".pdf");
        }
        public ActionResult Reporte_Coworkers_PDF(int id)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre_empresa = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id select E.Razon_Social).FirstOrDefault();
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.WebPageWidth = 1550;
            converter.Options.MarginLeft = 0;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 20;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/ReGE360_COMPojhgf05FD09876TRSFSDFSDFFGBNJKI49R8F7DYTGBDN4RVSDSDasjddsde3sdsw?id_e=" + id);
            return File(pdf.Save(), "application/pdf;", "Reporte_E360_General_" + Nombre_empresa + ".pdf");
        }
        public JsonResult Searching_Departamentos(int id_empresa)
        {

            //####################################################################################################################
            //                                              DEPARTAMENTOS
            //                                  TODOS SON VARIABLES DEPARTAMENTOS TOMADOS DE LA BASE DE DATOS
            //
            //####################################################################################################################


            var result = (from Deptos in db.fn_MultiEvalua_E360_Empresa_Deptos(id_empresa)
                          select new Corte_Departamento
                          {
                              Departamento = Deptos.Departamento,
                              PROMEDIO_C_I = Deptos.PROMEDIO_C_I,
                              PROMEDIO_C_II = Deptos.PROMEDIO_C_II,
                              PROMEDIO_C_III = Deptos.PROMEDIO_C_III,
                              PROMEDIO_C_IV = Deptos.PROMEDIO_C_IV,
                              PROMEDIO_C_V = Deptos.PROMEDIO_C_V,
                              PROMEDIO_C_VI = Deptos.PROMEDIO_C_VI,
                              PROMEDIO_C_VII = Deptos.PROMEDIO_C_VII,
                              PROMEDIO_C_VIII = Deptos.PROMEDIO_C_VIII,
                              PROMEDIO_C_IX = Deptos.PROMEDIO_C_IX,
                              PROMEDIO_C_X = Deptos.PROMEDIO_C_X,
                              PROMEDIO_C_XI = Deptos.PROMEDIO_C_XI,
                              PROMEDIO_C_XII = Deptos.PROMEDIO_C_XII,
                              PROMEDIO_C_XIII = Deptos.PROMEDIO_C_XIII,
                              PROMEDIO_C_XIV = Deptos.PROMEDIO_C_XIV,
                              PROMEDIO_C_XV = Deptos.PROMEDIO_C_XV,
                              PROMEDIO_FINAL = Deptos.PROMEDIO_FINAL,
                              COLOR = Deptos.COLOR
                          }).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: MultiEvaluaE360
        private static string promedio_color(double? promedio)
        {
            string color;
            if (promedio <= 39)
                color = "red";
            else if (promedio >= 40 && promedio < 60)
                color = "orange";
            else if (promedio >= 60 && promedio < 80)
                color = "yellow";
            else if (promedio >= 80 && promedio < 100)
                color = "lightgreen";
            else if (promedio == 100)
                color = "green";
            else
                color = "white";
            return color;
        }

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
                    mail.Subject = "Evaluación 360";
                    mail.IsBodyHtml = true;
                    SmtpServer.EnableSsl = true; 

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
                       "<p>Te invitamos a acceder a la siguiente liga para contestar tu encuesta:  <a href='https://check035.com/Home/Mi_Cuestionario'>Mi Encuesta</a><p>" +
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
        public ActionResult General_Statistics(int id_e)
        {
            try
            {
                var deptos = (from Deptos in db.fn_MultiEvalua_E360_Empresa_Deptos(id_e)
                                                   select new Corte_Departamento
                              {
                                  Departamento = Deptos.Departamento,
                                  PROMEDIO_C_I = Deptos.PROMEDIO_C_I,
                                  PROMEDIO_C_II = Deptos.PROMEDIO_C_II,
                                  PROMEDIO_C_III = Deptos.PROMEDIO_C_III,
                                  PROMEDIO_C_IV = Deptos.PROMEDIO_C_IV,
                                  PROMEDIO_C_V = Deptos.PROMEDIO_C_V,
                                  PROMEDIO_C_VI = Deptos.PROMEDIO_C_VI,
                                  PROMEDIO_C_VII = Deptos.PROMEDIO_C_VII,
                                  PROMEDIO_C_VIII = Deptos.PROMEDIO_C_VIII,
                                  PROMEDIO_C_IX = Deptos.PROMEDIO_C_IX,
                                  PROMEDIO_C_X = Deptos.PROMEDIO_C_X,
                                  PROMEDIO_C_XI = Deptos.PROMEDIO_C_XI,
                                  PROMEDIO_C_XII = Deptos.PROMEDIO_C_XII,
                                  PROMEDIO_C_XIII = Deptos.PROMEDIO_C_XIII,
                                  PROMEDIO_C_XIV = Deptos.PROMEDIO_C_XIV,
                                  PROMEDIO_C_XV = Deptos.PROMEDIO_C_XV,
                                  PROMEDIO_FINAL = Deptos.PROMEDIO_FINAL,
                                  COLOR = Deptos.COLOR
                              }).ToArray();
                ViewBag.E360DeptosLeght = deptos.Length;
                ViewBag.E360Deptos = deptos;

                var Final_Empresa = db.fn_MultiEvalua_E360_Empresa(id_e).FirstOrDefault();
                var Final_Empresa_Com = db.fn_MultiEvalua_E360_Empresa_Com(id_e).FirstOrDefault();
                var Final_Empresa_Sup = db.fn_MultiEvalua_E360_Empresa_Sup(id_e).FirstOrDefault();
                var Final_Empresa_Sub = db.fn_MultiEvalua_E360_Empresa_Sub(id_e).FirstOrDefault();
                var Final_Empresa_Emp = db.fn_MultiEvalua_E360_Empresa_Emp(id_e).FirstOrDefault();
                //var Final_Empresa_Com = db.fn_MultiEvalua_E360_Empresa_D(id_e).FirstOrDefault();

                Resultados_Colores[0] = promedio_color(Final_Empresa.PROMEDIO_C_I_100);
                Resultados_Colores[1] = promedio_color(Final_Empresa.PROMEDIO_C_II_100);
                Resultados_Colores[2] = promedio_color(Final_Empresa.PROMEDIO_C_III_100);
                Resultados_Colores[3] = promedio_color(Final_Empresa.PROMEDIO_C_IV_100);
                Resultados_Colores[4] = promedio_color(Final_Empresa.PROMEDIO_C_V_100);
                Resultados_Colores[5] = promedio_color(Final_Empresa.PROMEDIO_C_VI_100);
                Resultados_Colores[6] = promedio_color(Final_Empresa.PROMEDIO_C_VII_100);
                Resultados_Colores[7] = promedio_color(Final_Empresa.PROMEDIO_C_VIII_100);
                Resultados_Colores[8] = promedio_color(Final_Empresa.PROMEDIO_C_IX_100);
                Resultados_Colores[9] = promedio_color(Final_Empresa.PROMEDIO_C_X_100);
                Resultados_Colores[10] = promedio_color(Final_Empresa.PROMEDIO_C_XI_100);
                Resultados_Colores[11] = promedio_color(Final_Empresa.PROMEDIO_C_XII_100);
                Resultados_Colores[12] = promedio_color(Final_Empresa.PROMEDIO_C_XIII_100);
                Resultados_Colores[13] = promedio_color(Final_Empresa.PROMEDIO_C_XIV_100);
                Resultados_Colores[14] = promedio_color(Final_Empresa.PROMEDIO_C_XV_100);

                Resultados_Colores_Emp[0] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_I_100);
                Resultados_Colores_Emp[1] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_II_100);
                Resultados_Colores_Emp[2] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_III_100);
                Resultados_Colores_Emp[3] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_IV_100);
                Resultados_Colores_Emp[4] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_V_100);
                Resultados_Colores_Emp[5] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_VI_100);
                Resultados_Colores_Emp[6] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_VII_100);
                Resultados_Colores_Emp[7] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_VIII_100);
                Resultados_Colores_Emp[8] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_IX_100);
                Resultados_Colores_Emp[9] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_X_100);
                Resultados_Colores_Emp[10] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_XI_100);
                Resultados_Colores_Emp[11] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_XII_100);
                Resultados_Colores_Emp[12] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_XIII_100);
                Resultados_Colores_Emp[13] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_XIV_100);
                Resultados_Colores_Emp[14] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_XV_100);

                Resultados_Colores_Sup[0] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_I_100);
                Resultados_Colores_Sup[1] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_II_100);
                Resultados_Colores_Sup[2] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_III_100);
                Resultados_Colores_Sup[3] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_IV_100);
                Resultados_Colores_Sup[4] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_V_100);
                Resultados_Colores_Sup[5] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_VI_100);
                Resultados_Colores_Sup[6] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_VII_100);
                Resultados_Colores_Sup[7] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_VIII_100);
                Resultados_Colores_Sup[8] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_IX_100);
                Resultados_Colores_Sup[9] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_X_100);
                Resultados_Colores_Sup[10] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_XI_100);
                Resultados_Colores_Sup[11] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_XII_100);
                Resultados_Colores_Sup[12] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_XIII_100);
                Resultados_Colores_Sup[13] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_XIV_100);
                Resultados_Colores_Sup[14] = promedio_color(Final_Empresa_Sup.PROMEDIO_C_XV_100);

                Resultados_Colores_Sub[0] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_I_100);
                Resultados_Colores_Sub[1] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_II_100);
                Resultados_Colores_Sub[2] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_III_100);
                Resultados_Colores_Sub[3] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_IV_100);
                Resultados_Colores_Sub[4] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_V_100);
                Resultados_Colores_Sub[5] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_VI_100);
                Resultados_Colores_Sub[6] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_VII_100);
                Resultados_Colores_Sub[7] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_VIII_100);
                Resultados_Colores_Sub[8] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_IX_100);
                Resultados_Colores_Sub[9] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_X_100);
                Resultados_Colores_Sub[10] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_XI_100);
                Resultados_Colores_Sub[11] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_XII_100);
                Resultados_Colores_Sub[12] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_XIII_100);
                Resultados_Colores_Sub[13] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_XIV_100);
                Resultados_Colores_Sub[14] = promedio_color(Final_Empresa_Sub.PROMEDIO_C_XV_100);

                Resultados_Colores_Com[0] = promedio_color(Final_Empresa_Com.PROMEDIO_C_I_100);
                Resultados_Colores_Com[1] = promedio_color(Final_Empresa_Com.PROMEDIO_C_II_100);
                Resultados_Colores_Com[2] = promedio_color(Final_Empresa_Com.PROMEDIO_C_III_100);
                Resultados_Colores_Com[3] = promedio_color(Final_Empresa_Com.PROMEDIO_C_IV_100);
                Resultados_Colores_Com[4] = promedio_color(Final_Empresa_Com.PROMEDIO_C_V_100);
                Resultados_Colores_Com[5] = promedio_color(Final_Empresa_Com.PROMEDIO_C_VI_100);
                Resultados_Colores_Com[6] = promedio_color(Final_Empresa_Com.PROMEDIO_C_VII_100);
                Resultados_Colores_Com[7] = promedio_color(Final_Empresa_Com.PROMEDIO_C_VIII_100);
                Resultados_Colores_Com[8] = promedio_color(Final_Empresa_Com.PROMEDIO_C_IX_100);
                Resultados_Colores_Com[9] = promedio_color(Final_Empresa_Com.PROMEDIO_C_X_100);
                Resultados_Colores_Com[10] = promedio_color(Final_Empresa_Com.PROMEDIO_C_XI_100);
                Resultados_Colores_Com[11] = promedio_color(Final_Empresa_Com.PROMEDIO_C_XII_100);
                Resultados_Colores_Com[12] = promedio_color(Final_Empresa_Com.PROMEDIO_C_XIII_100);
                Resultados_Colores_Com[13] = promedio_color(Final_Empresa_Com.PROMEDIO_C_XIV_100);
                Resultados_Colores_Com[14] = promedio_color(Final_Empresa_Com.PROMEDIO_C_XV_100);

        


                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();


                ViewBag.Final_Empresa = Final_Empresa;
                ViewBag.Final_Empresa_Com = Final_Empresa_Com;
                ViewBag.Final_Empresa_Emp = Final_Empresa_Emp;
                ViewBag.Final_Empresa_Sub = Final_Empresa_Sub;
                ViewBag.Final_Empresa_Sup = Final_Empresa_Sup;

                ViewBag.Resultados_Colores = Resultados_Colores;
                ViewBag.Resultados_Colores_Emp = Resultados_Colores_Emp;
                ViewBag.Resultados_Colores_Com = Resultados_Colores_Com;
                ViewBag.Resultados_Colores_Sub = Resultados_Colores_Sub;
                ViewBag.Resultados_Colores_Sup = Resultados_Colores_Sup;

                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult General_Statistics_Centro(int id_e, int id_c)
        {
            try
            {
                var Final_Empresa = db.fn_MultiEvalua_E360_Centro_Trabajo(id_e, id_c).FirstOrDefault();
                Resultados_Colores[0] = promedio_color(Final_Empresa.PROMEDIO_C_I_100);
                Resultados_Colores[1] = promedio_color(Final_Empresa.PROMEDIO_C_II_100);
                Resultados_Colores[2] = promedio_color(Final_Empresa.PROMEDIO_C_III_100);
                Resultados_Colores[3] = promedio_color(Final_Empresa.PROMEDIO_C_IV_100);
                Resultados_Colores[4] = promedio_color(Final_Empresa.PROMEDIO_C_V_100);
                Resultados_Colores[5] = promedio_color(Final_Empresa.PROMEDIO_C_VI_100);
                Resultados_Colores[6] = promedio_color(Final_Empresa.PROMEDIO_C_VII_100);
                Resultados_Colores[7] = promedio_color(Final_Empresa.PROMEDIO_C_VIII_100);
                Resultados_Colores[8] = promedio_color(Final_Empresa.PROMEDIO_C_IX_100);
                Resultados_Colores[9] = promedio_color(Final_Empresa.PROMEDIO_C_X_100);
                Resultados_Colores[10] = promedio_color(Final_Empresa.PROMEDIO_C_XI_100);
                Resultados_Colores[11] = promedio_color(Final_Empresa.PROMEDIO_C_XII_100);
                Resultados_Colores[12] = promedio_color(Final_Empresa.PROMEDIO_C_XIII_100);
                Resultados_Colores[13] = promedio_color(Final_Empresa.PROMEDIO_C_XIV_100);
                Resultados_Colores[14] = promedio_color(Final_Empresa.PROMEDIO_C_XV_100);


                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                join Centro in db.ERGOS_Centros_Trabajo_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo equals Centro.id_centro_trabajo
                                                where CT.id_empresa == id_e && CT.id_centro_trabajo == id_c
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
        public ActionResult Index()
        {
            var e360_Cuestionario_Resultado_N01 = db.E360_Cuestionario_Resultado_N01.Include(e => e.ERGOS_Cuestionarios_Trabajador_N01);
            return View(e360_Cuestionario_Resultado_N01.ToList());
        }
        public ActionResult Home()
        {
            return View();
        }
   

        // GET: MultiEvaluaE360/Create
        public ActionResult Create()
        {
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion");
            return View();
        }

        // POST: MultiEvaluaE360/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01)
        {
            if (ModelState.IsValid)
            {
                db.E360_Cuestionario_Resultado_N01.Add(e360_Cuestionario_Resultado_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", e360_Cuestionario_Resultado_N01.id_cuestionario_trabajador);
            return View(e360_Cuestionario_Resultado_N01);
        }

        // GET: MultiEvaluaE360/Edit/5 
        [Authorize(Roles = "Admin,Admin_Sys,Final_Guest")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01 = db.E360_Cuestionario_Resultado_N01.Find(id);
            if (e360_Cuestionario_Resultado_N01 == null)
            {
                return HttpNotFound();
            }


            int? id_cuestionario_trabajador = (from E in db.E360_Cuestionario_Resultado_N01 where E.id_cuestionario_resultado == id select E.id_cuestionario_trabajador).FirstOrDefault();
            int? id_centro_trabajo = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_cuestionario_trabajador select E.id_centro_trabajo).FirstOrDefault();
            string departamento = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_cuestionario_trabajador select E.Departamento).FirstOrDefault();
            int? Supervisor_Status = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_cuestionario_trabajador select E.Supervisor_Status).FirstOrDefault();

            if (e360_Cuestionario_Resultado_N01.id_CT_Evaluado != null)
            {

                ViewBag.evaluated_coworker = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == e360_Cuestionario_Resultado_N01.id_CT_Evaluado select E.Nombre).FirstOrDefault();
                ViewBag.disable_coworker = 1;
            }
            else
            {

                List<SelectListItem> viewList = new List<SelectListItem>();
                viewList.AddRange(new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01
                .Where(x => x.Departamento == departamento)
                .Where(x => x.id_centro_trabajo == id_centro_trabajo)
                .Where(x => x.E360_EBCW == null)
                .Where(x => x.Supervisor_Status != 1)
                .Where(x => x.id_cuestionario_trabajador != id_cuestionario_trabajador), "id_cuestionario_trabajador", "Nombre", e360_Cuestionario_Resultado_N01.id_CT_Evaluado));
                ViewBag.DatumRID = viewList;

                if (viewList.Count() > 0)
                {
                    ViewBag.id_CT_Evaluado = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01
                   .Where(x => x.Departamento == departamento)
                   .Where(x => x.id_centro_trabajo == id_centro_trabajo)
                   .Where(x => x.E360_EBCW == null)
                   .Where(x => x.Supervisor_Status != 1)
                   .Where(x => x.id_cuestionario_trabajador != id_cuestionario_trabajador), "id_cuestionario_trabajador", "Nombre", e360_Cuestionario_Resultado_N01.id_CT_Evaluado);
                }
                else
                {
                    ViewBag.id_CT_Evaluado = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01
                   .Where(x => x.id_centro_trabajo == id_centro_trabajo)
                   .Where(x => x.E360_EBCW == null)
                   .Where(x => x.Supervisor_Status != 1)
                   .Where(x => x.id_cuestionario_trabajador != id_cuestionario_trabajador), "id_cuestionario_trabajador", "Nombre", e360_Cuestionario_Resultado_N01.id_CT_Evaluado);
                }
            }

            if (Supervisor_Status == 1)
            {
                if (e360_Cuestionario_Resultado_N01.id_CT_Evaluado != null)
                { 
                    ViewBag.evaluated_employee = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == e360_Cuestionario_Resultado_N01.id_CT_Evaluado select E.Nombre).FirstOrDefault();
                    ViewBag.disable_employee = 1;
                }
                else {

                    ViewBag.id_CT_Evaluado = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01
                // .Where(x => x.Departamento == departamento)
                .Where(x => x.id_centro_trabajo == id_centro_trabajo)
                .Where(x => x.id_cuestionario_trabajador != id_cuestionario_trabajador), "id_cuestionario_trabajador", "Nombre", e360_Cuestionario_Resultado_N01.id_CT_Evaluado);
                }
            }
            //if (ViewBag.Data == null) {
            //    ViewBag.Data2 = 2;
            //}
            
                return View(e360_Cuestionario_Resultado_N01);
        }

        // POST: MultiEvaluaE360/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Admin_Sys,Final_Guest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e360_Cuestionario_Resultado_N01).State = EntityState.Modified;
                db.SaveChanges();

                if (User.IsInRole("Final_Guest"))
                    return RedirectToAction("Mi_Cuestionario","Home");
                else
                return RedirectToAction("Index");
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", e360_Cuestionario_Resultado_N01.id_cuestionario_trabajador);
            return View(e360_Cuestionario_Resultado_N01);
        }

        // GET: MultiEvaluaE360/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01 = db.E360_Cuestionario_Resultado_N01.Find(id);
            if (e360_Cuestionario_Resultado_N01 == null)
            {
                return HttpNotFound();
            }
            return View(e360_Cuestionario_Resultado_N01);
        }

        // POST: MultiEvaluaE360/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01 = db.E360_Cuestionario_Resultado_N01.Find(id);
            db.E360_Cuestionario_Resultado_N01.Remove(e360_Cuestionario_Resultado_N01);
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
