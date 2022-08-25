  
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity; 
using System.Drawing;
using System.Linq;
using System.Net;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Cryptography;
using System.Web.Mvc;
using SelectPdf;
using ErgoSalud.Models; 
using OfficeOpenXml;
using PagedList; 
using System.IO; 
using ErgoSalud.Helper;
using Excel;
using System.Data.SqlClient;

namespace ErgoSalud.Controllers
{
    [Authorize]
    public class EncuestasController : Controller
    {
        public int flag_editar = 0;
        private Check035Entities db = new Check035Entities();
   

        [HttpPost]
        [Authorize(Roles = "Admin_SyS, Admin-Guest")] 
        public ActionResult carga_masiva(Upload_excel_records model)
        {

            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 3 select E.id_empresa).FirstOrDefault();
            var validTypes = new string[] { "application/octet-stream", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.ms-excel" };

            if (model.File != null)
            {
                if (model.File.ContentLength <= 0 || !validTypes.Contains(model.File.ContentType))
                {
                    ModelState.AddModelError("File", "Only the following file types are allowed: .xlsx or .xls");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.DataSet = Upload(model);
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(x => x.id_empresa == id_empresa), "id_centro_trabajo", "Nombre_centro_trabajo", model.id_centro_trabajo);
         //   db.Dispose();
            return View(model);
        }

        private DataSet Upload(Upload_excel_records model)
        {
            string guid = Guid.NewGuid().ToString();
            string uploadDir = "~/uploads/" + guid;
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(uploadDir), model.File.FileName);
            IO.CreateDirectoryIfMissing(filePath);
            model.FilePath = filePath;
            model.File.SaveAs(filePath);

            return ImportData(model);
        }

        private DataSet ImportData(Upload_excel_records model)
        {
            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 3 select E.id_empresa).FirstOrDefault();
           
            using (FileStream stream = System.IO.File.Open(model.FilePath, FileMode.Open, FileAccess.Read))
            {
                bool isNewOffice = model.FilePath.Contains(".xlsx");
                IExcelDataReader excelReader = isNewOffice
                  ? ExcelReaderFactory.CreateOpenXmlReader(stream)
                  : ExcelReaderFactory.CreateBinaryReader(stream);
               
                excelReader.IsFirstRowAsColumnNames = true;
               DataTable dt = excelReader.AsDataSet().Tables[0]; 
                        foreach (DataRow row in dt.Rows)
                        {
                        var commandText = "EXECUTE sp_creating_new_record @id_empresa = @id_empresa,@nombre=@nombre,@id_trabajador=@id_trabajador,@id_centro_trabajo=@id_centro_trabajo,@Ocupacion=@Ocupacion,@departamento=@departamento,@Email=@Email ";
                        var _id_empresa = new SqlParameter("@id_empresa", id_empresa);
                        var _nombre = new SqlParameter("@nombre", row[0]);
                        var _id_trabajador = new SqlParameter("@id_trabajador", row[1]);
                        var _id_centro_trabajo = new SqlParameter("@id_centro_trabajo", model.id_centro_trabajo);
                        var _Ocupacion = new SqlParameter("@Ocupacion", row[2]);
                        var _Email = new SqlParameter("@departamento", row[3]);
                        var _departamento = new SqlParameter("@Email", row[4]);
                        db.Database.ExecuteSqlCommand(commandText, _id_empresa, _nombre, _id_trabajador, _id_centro_trabajo, _Ocupacion, _Email, _departamento);
                        db.SaveChanges();
                    } 
                return excelReader.AsDataSet();
            }
        }

        [Authorize(Roles = "Admin_SyS, Admin-Guest")]
        public ActionResult carga_masiva()
        {

            string UserName_Current = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName_Current select E.id_empresa).FirstOrDefault();
   
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(x => x.id_empresa == id_empresa), "id_centro_trabajo", "Nombre_centro_trabajo");


            if (User.IsInRole("Admin-Guest"))
            {
                ViewBag.customer = id_empresa;

            }
            return View();
        }

        [Authorize(Roles = "Admin,Admin_SyS")]
        // GET: Encuestas
        public ActionResult Index(int? page, int? flag)
        {
            if (flag != null)
            {
                if (flag == 1)
                {
                    ViewData["Mail"] = "Correos Enviados Exitosamente";
                }
                else
                {
                    ViewData["Mail_Error"] = "Los correos no pudieron ser enviados";

                }
            }
            //List<ERGOS_Empresas_N01> Folios = db.ERGOS_Empresas_N01.ToList();
            ViewBag.ERGOS_Empresas_N01List = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");

            // var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01);
            return View(db.ERGOS_Cuestionarios_Trabajador_N01.Include(c => c.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.ERGOS_Empresas_N01.deleted_at == null).ToList().ToPagedList(page ?? 1, 40));
        }

        [Authorize(Roles = "Admin_SyS, Admin-Guest")]
        public ActionResult Encuestas_Admin(int? page, string User_Name, int? flag, int? id_empresa, string Usuario)
        {
            string UserName = User.Identity.Name;
            if (flag != null)
            {
                ViewData["Mail"] = "Correos Enviados Exitosamente";
            }
            if (id_empresa == null)
            {
                id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_empresa).FirstOrDefault();
            }
            ViewBag.id_empresa = id_empresa;
            ViewBag.id_encuesta = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id_empresa select E.id_encuesta).FirstOrDefault();
            ViewBag.Encuestas_Contestadas = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa && E.Survey_Status == 100 select E.id_cuestionario_trabajador).Count();
            ViewBag.Encuestas_Totales = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa select E.id_cuestionario_trabajador).Count();


            ViewBag.Encuestas_Areas_Total = (from E in db.fn_Total_Encuestas_Area(id_empresa) select new Encuesta_Admin_Progress { Departamento = E.Departamento_Total_E, Encuestas_Totales = E.Total_de_Encuestas, Encuestas_Contestadas = E.Encuestas_Contestadas }).ToList();


            if (Usuario != null)
            {
                var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Usuarios_N01).Include(e => e.DATA_Departamentos_N01).Include(e => e.DATA_Edades_N01).Include(e => e.DATA_Estado_Civil_N01).Include(e => e.DATA_Experiencia_puesto_N01).Include(e => e.DATA_Experiencia_puesto_N011).Include(e => e.DATA_Nivel_Estudios_N01).Include(e => e.DATA_Rotacion_Turno_N01).Include(e => e.DATA_Sexo_N01).Include(e => e.DATA_Tipo_Contratacion_N01).Include(e => e.DATA_Tipo_Jornada_N01).Include(e => e.DATA_Tipo_Personal_N01).Include(e => e.DATA_Tipo_puesto_N01).Include(e => e.ERGOS_Centros_Trabajo_N01).Include(e => e.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.id_empresa == id_empresa).Where(e => e.ERGOS_Empresas_N01.deleted_at == null).Where(e => e.Nombre.Contains(Usuario));
                return View(eRGOS_Cuestionarios_Trabajador_N01.ToList().ToPagedList(page ?? 1, 150));
            }
            else
            {

                var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Usuarios_N01).Include(e => e.DATA_Departamentos_N01).Include(e => e.DATA_Edades_N01).Include(e => e.DATA_Estado_Civil_N01).Include(e => e.DATA_Experiencia_puesto_N01).Include(e => e.DATA_Experiencia_puesto_N011).Include(e => e.DATA_Nivel_Estudios_N01).Include(e => e.DATA_Rotacion_Turno_N01).Include(e => e.DATA_Sexo_N01).Include(e => e.DATA_Tipo_Contratacion_N01).Include(e => e.DATA_Tipo_Jornada_N01).Include(e => e.DATA_Tipo_Personal_N01).Include(e => e.DATA_Tipo_puesto_N01).Include(e => e.ERGOS_Centros_Trabajo_N01).Include(e => e.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.id_empresa == id_empresa).Where(e => e.ERGOS_Empresas_N01.deleted_at == null);
                return View(eRGOS_Cuestionarios_Trabajador_N01.ToList().ToPagedList(page ?? 1, 150));
            }
        }

     

        [Authorize(Roles = "Admin_Centro")]
        public ActionResult Encuestas_Centro(int? page, string User_Name, int? flag)
        {
            if (flag != null)
            {
                ViewData["Mail"] = "Correos Enviados Exitosamente";
            }
            string UserName_Current = User.Identity.Name;
            int? id_centro_trabajo = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName_Current select E.id_centro_trabajo).FirstOrDefault();
            ViewBag.Encuestas_Contestadas = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_centro_trabajo == id_centro_trabajo && E.Survey_Status == 100 select E.id_cuestionario_trabajador).Count();
            ViewBag.Encuestas_Totales = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_centro_trabajo == id_centro_trabajo select E.id_cuestionario_trabajador).Count();


            ViewBag.Encuestas_Areas_Total = (from E in db.fn_Total_Encuestas_Area_CT(id_centro_trabajo) select new Encuesta_Admin_Progress { Departamento = E.Departamento_Total_E, Encuestas_Totales = E.Total_de_Encuestas, Encuestas_Contestadas = E.Encuestas_Contestadas }).ToList();

            return View(db.ERGOS_Cuestionarios_Trabajador_N01.Include(c => c.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.id_centro_trabajo == id_centro_trabajo).Where(e => e.ERGOS_Empresas_N01.deleted_at == null).ToList().ToPagedList(page ?? 1, 40));
        }
        public ActionResult Reporte_Resultado_Individual_CLMA(int id_CT)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";

            string Nombre = (from E in db.ClimaLaboral_Cuestionario_Resultados_N01 
                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on E.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                             where E.id_cuestionario_trabajador == id_CT select CT.Nombre).FirstOrDefault();
            int? id_empresa = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_empresa).FirstOrDefault();
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
            converter.Options.WebPageWidth = 1200;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf2 = converter.ConvertUrl(url + "Reportes/RIndCLMAuirmBaf4AhwsdsaJ6fobQAreA9mBUt4dmB5CBNvjDfuIhma4ZpjMxmTosLw?id_CT=" + id_CT);

            return File(pdf2.Save(), "application/pdf;", "Reporte_" + Nombre + ".pdf");
        }
        public ActionResult Reporte_Resultado_Individual_E360(int id_CT)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";

            string Nombre = (from E in db.E360_Cuestionario_Resultado_N01
                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on E.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                             where E.id_cuestionario_trabajador == id_CT
                             select CT.Nombre).FirstOrDefault();
            int? id_empresa = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_empresa).FirstOrDefault();
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
            converter.Options.WebPageWidth = 1200;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf2 = converter.ConvertUrl(url + "Reportes/RIndE360jsdhkdsgbf894378uyfsb9834t8hskdjgh3894ghoi987y4398duicfkgn509ufhhfg4Lw?id_CT=" + id_CT);

            return File(pdf2.Save(), "application/pdf;", "Reporte_E360" + Nombre + ".pdf");
        }
        public ActionResult Reporte_Resultado_Individual(int id_CT)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.Nombre).FirstOrDefault();
            int? id_empresa = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_empresa).FirstOrDefault();
            string id_trabajador = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_trabajador).FirstOrDefault();
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
            converter.Options.WebPageWidth = 1200;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf2 = converter.ConvertUrl(url + "Reportes/RInduirmBaf4AhwsdsaJ6fobQAreA9mBUt4dmB5CBNvjDfuIhma4ZpjMxmTosLw?id_CT=" + id_CT);


            return File(pdf2.Save(), "application/pdf;", "Reporte_General_" + Nombre + ".pdf");
        }

        public ActionResult Reporte_General_PDF(int id, int id_C)
        {
       
                string Empresa = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id select E.Razon_Social).FirstOrDefault();
                var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
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
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                SelectPdf.PdfDocument pdf1 = converter.ConvertUrl(url + "Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id=" + id + "&id_encuesta=" + id_C);
         

            return File(pdf1.Save(), "application/pdf;", "Reporte_General_" + Empresa + ".pdf");

        }
        public ActionResult Reporte_General_Clima(int id, int id_C)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string centro_trabajo_nombre = (from Company in db.ERGOS_Empresas_N01
                                            where Company.id_empresa == id
                                            select Company.Razon_Social).FirstOrDefault();
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
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf1 = converter.ConvertUrl(url + "Reportes/ReGCLYbqtKJHGFDE87654asauIhmdsaJ6fobQAreA9mBUt4dmBa4Z7?id=" + id + "&id_encuesta=" + id_C);
            //var html = new System.Net.WebClient().DownloadString(url + "Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id=" + id);

            return File(pdf1.Save(), "application/pdf;", centro_trabajo_nombre + "_Reporte_Final.pdf");
            //return File(pdf.Save(), "application/pdf;", "Reporte_Final.pdf");
        }
        public ActionResult Reporte_General_PDF_E360(int id, int id_C)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string centro_trabajo_nombre = (from Company in db.ERGOS_Empresas_N01
                                            where Company.id_empresa == id
                                            select Company.Razon_Social).FirstOrDefault();
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
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf1 = converter.ConvertUrl(url + "Reportes/ReGE360ASDFD09876TRFGBNJKI49R8F7DYTGBDN4RVSDSDasjddsde3sdsw?id=" + id + "&id_encuesta=" + id_C);
            //var html = new System.Net.WebClient().DownloadString(url + "Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id=" + id);

            return File(pdf1.Save(), "application/pdf;", centro_trabajo_nombre + "_Reporte_Final.pdf");
            //return File(pdf.Save(), "application/pdf;", "Reporte_Final.pdf");
        }
        [HttpGet]
        public void ExportToExcel(int id_empresa)
        {
            List<ERGOS_Cuestionarios_Resultados_N01> emplist = db.ERGOS_Cuestionarios_Resultados_N01.Where(e => e.ERGOS_Cuestionarios_Trabajador_N01.id_empresa == id_empresa).ToList();
            int? id_cuestionario = (from E in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where E.ERGOS_Empresas_N01.id_empresa == id_empresa
                                    select E.id_encuesta).FirstOrDefault();


            int rowStart = 3;
            int counter_flag = 0;

            if (id_cuestionario == 3)
            {
                ExcelPackage pck = new ExcelPackage();
                ExcelWorksheet Preguntas = pck.Workbook.Worksheets.Add("Base de Datos Encuestados");

                Preguntas.Cells["A1:DZ2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Preguntas.Cells["A1:DZ2"].Style.Font.Bold = true;
                Preguntas.Cells["A1:DZ2"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("white")));
                Preguntas.Cells["A2:DZ2"].Style.Font.Size = 11;
                Preguntas.Cells["A1:DZ1"].Style.Font.Size = 25;
                Preguntas.Cells["P2:DZ2"].Style.TextRotation = 90;
                Preguntas.Cells["A1:DZ1"].Value = "Check035";
                Preguntas.Cells["A1:DZ1"].Merge = true;
                Preguntas.Cells["A1:DZ2"].Style.Font.Name = "Calibri";
                Preguntas.Cells["A1:DZ2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9071ae")));
                //SURVEY______________3

                Preguntas.Cells["A2"].Value = "Nombre";
                Preguntas.Cells["B2"].Value = "No. De Folio";
                Preguntas.Cells["C2"].Value = "Ocupación";
                Preguntas.Cells["D2"].Value = "Departamento";
                Preguntas.Cells["E2"].Value = "Género";

                // NEW ADDED 09/16/2019 ################################
                Preguntas.Cells["F2"].Value = "Edad";
                Preguntas.Cells["G2"].Value = "Estado Civil";
                Preguntas.Cells["H2"].Value = "Nivel de Estudios";
                Preguntas.Cells["I2"].Value = "Empresa";
                Preguntas.Cells["J2"].Value = "Tipo de Puesto";
                Preguntas.Cells["K2"].Value = "Tipo de Contratación";
                Preguntas.Cells["L2"].Value = "Tipo de Jornada";
                Preguntas.Cells["M2"].Value = "Rotación de Turno";
                Preguntas.Cells["N2"].Value = "Experiencia Puesto Laboral";
                Preguntas.Cells["O2"].Value = "Experiencia Puesto Actual";
                // ########################################################

                Preguntas.Cells["P2"].Value = "PREGUNTA 1";
                Preguntas.Cells["Q2"].Value = "PREGUNTA 2";
                Preguntas.Cells["R2"].Value = "PREGUNTA 3";
                Preguntas.Cells["S2"].Value = "PREGUNTA 4";
                Preguntas.Cells["T2"].Value = "PREGUNTA 5";
                Preguntas.Cells["U2"].Value = "PREGUNTA 6";
                Preguntas.Cells["V2"].Value = "PREGUNTA 7";
                Preguntas.Cells["W2"].Value = "PREGUNTA 8";
                Preguntas.Cells["X2"].Value = "PREGUNTA 9";
                Preguntas.Cells["Y2"].Value = "PREGUNTA 10";
                Preguntas.Cells["Z2"].Value = "PREGUNTA 11";
                Preguntas.Cells["AA2"].Value = "PREGUNTA 12";
                Preguntas.Cells["AB2"].Value = "PREGUNTA 13";
                Preguntas.Cells["AC2"].Value = "PREGUNTA 14";
                Preguntas.Cells["AD2"].Value = "PREGUNTA 15";
                Preguntas.Cells["AE2"].Value = "PREGUNTA 16";
                Preguntas.Cells["AF2"].Value = "PREGUNTA 17";
                Preguntas.Cells["AG2"].Value = "PREGUNTA 18";
                Preguntas.Cells["AH2"].Value = "PREGUNTA 19";
                Preguntas.Cells["AI2"].Value = "PREGUNTA 20";
                Preguntas.Cells["AJ2"].Value = "PREGUNTA 21";
                Preguntas.Cells["AK2"].Value = "PREGUNTA 22";
                Preguntas.Cells["AL2"].Value = "PREGUNTA 23";
                Preguntas.Cells["AM2"].Value = "PREGUNTA 24";
                Preguntas.Cells["AN2"].Value = "PREGUNTA 25";
                Preguntas.Cells["AO2"].Value = "PREGUNTA 26";
                Preguntas.Cells["AP2"].Value = "PREGUNTA 27";
                Preguntas.Cells["AQ2"].Value = "PREGUNTA 28";
                Preguntas.Cells["AR2"].Value = "PREGUNTA 29";
                Preguntas.Cells["AS2"].Value = "PREGUNTA 30";
                Preguntas.Cells["AT2"].Value = "PREGUNTA 31";
                Preguntas.Cells["AU2"].Value = "PREGUNTA 32";
                Preguntas.Cells["AV2"].Value = "PREGUNTA 33";
                Preguntas.Cells["AW2"].Value = "PREGUNTA 34";
                Preguntas.Cells["AX2"].Value = "PREGUNTA 35";
                Preguntas.Cells["AY2"].Value = "PREGUNTA 36";
                Preguntas.Cells["AZ2"].Value = "PREGUNTA 37";
                Preguntas.Cells["BA2"].Value = "PREGUNTA 38";
                Preguntas.Cells["BB2"].Value = "PREGUNTA 39";
                Preguntas.Cells["BC2"].Value = "PREGUNTA 40";
                Preguntas.Cells["BD2"].Value = "PREGUNTA 41";
                Preguntas.Cells["BE2"].Value = "PREGUNTA 42";
                Preguntas.Cells["BF2"].Value = "PREGUNTA 43";
                Preguntas.Cells["BG2"].Value = "PREGUNTA 44";
                Preguntas.Cells["BH2"].Value = "PREGUNTA 45";
                Preguntas.Cells["BI2"].Value = "PREGUNTA 46";
                Preguntas.Cells["BJ2"].Value = "PREGUNTA 47";
                Preguntas.Cells["BK2"].Value = "PREGUNTA 48";
                Preguntas.Cells["BL2"].Value = "PREGUNTA 49";
                Preguntas.Cells["BM2"].Value = "PREGUNTA 50";
                Preguntas.Cells["BN2"].Value = "PREGUNTA 51";
                Preguntas.Cells["BO2"].Value = "PREGUNTA 52";
                Preguntas.Cells["BP2"].Value = "PREGUNTA 53";
                Preguntas.Cells["BQ2"].Value = "PREGUNTA 54";
                Preguntas.Cells["BR2"].Value = "PREGUNTA 55";
                Preguntas.Cells["BS2"].Value = "PREGUNTA 56";
                Preguntas.Cells["BT2"].Value = "PREGUNTA 57";
                Preguntas.Cells["BU2"].Value = "PREGUNTA 58";
                Preguntas.Cells["BV2"].Value = "PREGUNTA 59";
                Preguntas.Cells["BW2"].Value = "PREGUNTA 60";
                Preguntas.Cells["BX2"].Value = "PREGUNTA 61";
                Preguntas.Cells["BY2"].Value = "PREGUNTA 62";
                Preguntas.Cells["BZ2"].Value = "PREGUNTA 63";
                Preguntas.Cells["CA2"].Value = "PREGUNTA 64";
                Preguntas.Cells["CB2"].Value = "PREGUNTA 65";
                Preguntas.Cells["CC2"].Value = "PREGUNTA 66";
                Preguntas.Cells["CD2"].Value = "PREGUNTA 67";
                Preguntas.Cells["CE2"].Value = "PREGUNTA 68";
                Preguntas.Cells["CF2"].Value = "PREGUNTA 69";
                Preguntas.Cells["CG2"].Value = "PREGUNTA 70";
                Preguntas.Cells["CH2"].Value = "PREGUNTA 71";
                Preguntas.Cells["CI2"].Value = "PREGUNTA 72";
                Preguntas.Cells["CJ2"].Value = "Canalizacion";
                Preguntas.Cells["CK2"].Value = "Puntaje Final";
                Preguntas.Cells["CL2"].Value = "Calif Final";

                //***************************************************
                // IMPRIMIENDO DOMINIOS EN EXCEL CUESTIONARIO III   //
                //***************************************************

                Preguntas.Cells["CM2"].Value = "Categoría 1";
                Preguntas.Cells["CN2"].Value = "Categoría 2";
                Preguntas.Cells["CO2"].Value = "Categoría 3";
                Preguntas.Cells["CP2"].Value = "Categoría 4";
                Preguntas.Cells["CQ2"].Value = "Categoría 5";
                Preguntas.Cells["CR2"].Value = "Dominio 1";
                Preguntas.Cells["CS2"].Value = "Dominio 2";
                Preguntas.Cells["CT2"].Value = "Dominio 3";
                Preguntas.Cells["CU2"].Value = "Dominio 4";
                Preguntas.Cells["CV2"].Value = "Dominio 5";
                Preguntas.Cells["CW2"].Value = "Dominio 6";
                Preguntas.Cells["CX2"].Value = "Dominio 7";
                Preguntas.Cells["CY2"].Value = "Dominio 8";
                Preguntas.Cells["CZ2"].Value = "Dominio 9";
                Preguntas.Cells["DA2"].Value = "Dominio 10";
                Preguntas.Cells["DB2"].Value = "Dimensión 1";
                Preguntas.Cells["DC2"].Value = "Dimensión 2";
                Preguntas.Cells["DD2"].Value = "Dimensión 3";
                Preguntas.Cells["DE2"].Value = "Dimensión 4";
                Preguntas.Cells["DF2"].Value = "Dimensión 5";
                Preguntas.Cells["DG2"].Value = "Dimensión 6";
                Preguntas.Cells["DH2"].Value = "Dimensión 7";
                Preguntas.Cells["DI2"].Value = "Dimensión 8";
                Preguntas.Cells["DJ2"].Value = "Dimensión 9";
                Preguntas.Cells["DK2"].Value = "Dimensión 10";
                Preguntas.Cells["DL2"].Value = "Dimensión 11";
                Preguntas.Cells["DM2"].Value = "Dimensión 12";
                Preguntas.Cells["DN2"].Value = "Dimensión 13";
                Preguntas.Cells["DO2"].Value = "Dimensión 14";
                Preguntas.Cells["DP2"].Value = "Dimensión 15";
                Preguntas.Cells["DQ2"].Value = "Dimensión 16";
                Preguntas.Cells["DR2"].Value = "Dimensión 17";
                Preguntas.Cells["DS2"].Value = "Dimensión 18";
                Preguntas.Cells["DT2"].Value = "Dimensión 19";
                Preguntas.Cells["DU2"].Value = "Dimensión 20";
                Preguntas.Cells["DV2"].Value = "Dimensión 21";
                Preguntas.Cells["DW2"].Value = "Dimensión 22";
                Preguntas.Cells["DX2"].Value = "Dimensión 23";
                Preguntas.Cells["DY2"].Value = "Dimensión 24";
                Preguntas.Cells["DZ2"].Value = "Dimensión 25";




                foreach (var item in emplist)
                {
                    Preguntas.Cells[string.Format("A{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Nombre;
                    Preguntas.Cells[string.Format("B{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.id_trabajador;
                    Preguntas.Cells[string.Format("I{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.ERGOS_Empresas_N01.Razon_Social;
                    Preguntas.Cells[string.Format("C{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Ocupacion;
                    Preguntas.Cells[string.Format("D{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Departamento;


                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Canalizacion is null)
                    { Preguntas.Cells[string.Format("CJ{0}", rowStart)].Value = "Sin Información"; }
                    else
                    {
                        if (item.ERGOS_Cuestionarios_Trabajador_N01.Canalizacion == 1)
                        { Preguntas.Cells[string.Format("CJ{0}", rowStart)].Value = "Si"; }
                        else if (item.ERGOS_Cuestionarios_Trabajador_N01.Canalizacion == 0) { Preguntas.Cells[string.Format("CJ{0}", rowStart)].Value = "No"; }
                    }
                    /*PUNTAJE FINAL*/
                    Preguntas.Cells[string.Format("CK{0}", rowStart)].Formula = string.Format("SUM(P{0}:CI{0})", rowStart);
                    //CALIFICACION FINAL
                    Preguntas.Cells[string.Format("CL{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(P{0}:CI{0}),0) >= 0,IFERROR(SUM(P{0}:CI{0}),0) <50)),\"NULO\",IF((AND(IFERROR(SUM(P{0}:CI{0}),0) >= 50,IFERROR(SUM(P{0}:CI{0}),0) <75)),\"BAJO\",IF((AND(IFERROR(SUM(P{0}:CI{0}),0) >= 75,IFERROR(SUM(P{0}:CI{0}),0) <99)),\"MEDIO\",IF((AND(IFERROR(SUM(P{0}:CI{0}),0) >= 99,IFERROR(SUM(P{0}:CI{0}),0) <140)),\"ALTO\",IF((AND(IFERROR(SUM(P{0}:CI{0}),0) >= 140,IFERROR(SUM(P{0}:CI{0}),0) <=300)),\"MUY ALTO\",\"N/A\")))))", rowStart);

                    //Preguntas_S2.Cells[string.Format("BL{0}", rowStart)].Formula = string.Format("IF((AND(SUM(P{0}:BI{0} <20)),\"NULO\",IF((AND(SUM(P{0}:BI{0} >= 20,SUM(P{0}:BI{0} <45)),\"BAJO\",IF((AND(SUM(P{0}:BI{0} >= 45,SUM(P{0}:BI{0} <70)),\"MEDIO\",IF((AND(SUM(P{0}:BI{0} >= 70,SUM(P{0}:BI{0} <90)),\"ALTO\",IF((AND(SUM(P{0}:BI{0} >= 90,SUM(P{0}:BI{0} =200)),\"MUY ALTO\",\"N/A\")))))", rowStart);

                    /********************   CATEGORIAS 1 A 5  ********************/
                    Preguntas.Cells[string.Format("CM{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 0,IFERROR(SUM(P{0}:T{0}),0) <5)),\"NULO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 5,IFERROR(SUM(P{0}:T{0}),0) <9)),\"BAJO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 9,IFERROR(SUM(P{0}:T{0}),0) <11)),\"MEDIO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 11,IFERROR(SUM(P{0}:T{0}),0) <14)),\"ALTO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 14,IFERROR(SUM(P{0}:T{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CN{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) >= 0,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) <15)),\"NULO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) >= 15,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) <30)),\"BAJO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) >= 30,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) <45)),\"MEDIO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) >= 45,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) <60)),\"ALTO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) >= 60,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0},AL{0}:AS{0},AX{0},AY{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CO{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AF{0}:AK{0}),0) >= 0,IFERROR(SUM(AF{0}:AK{0}),0) <5)),\"NULO\",IF((AND(IFERROR(SUM(AF{0}:AK{0}),0) >= 5,IFERROR(SUM(AF{0}:AK{0}),0) <7)),\"BAJO\",IF((AND(IFERROR(SUM(AF{0}:AK{0}),0) >= 7,IFERROR(SUM(AF{0}:AK{0}),0) <10)),\"MEDIO\",IF((AND(IFERROR(SUM(AF{0}:AK{0}),0) >= 10,IFERROR(SUM(AF{0}:AK{0}),0) <13)),\"ALTO\",IF((AND(IFERROR(SUM(AF{0}:AK{0}),0) >= 13,IFERROR(SUM(AF{0}:AK{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CP{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) >= 0,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) <14)),\"NULO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) >= 14,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) <29)),\"BAJO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) >= 29,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) <42)),\"MEDIO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) >= 42,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) <58)),\"ALTO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) >= 58,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BI{0},CF{0}:CI{0},BT{0}:CA{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CQ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(BJ{0}:BS{0}),0) >= 0,IFERROR(SUM(BJ{0}:BS{0}),0) <10)),\"NULO\",IF((AND(IFERROR(SUM(BJ{0}:BS{0}),0) >= 10,IFERROR(SUM(BJ{0}:BS{0}),0) <14)),\"BAJO\",IF((AND(IFERROR(SUM(BJ{0}:BS{0}),0) >= 14,IFERROR(SUM(BJ{0}:BS{0}),0) <18)),\"MEDIO\",IF((AND(IFERROR(SUM(BJ{0}:BS{0}),0) >= 18,IFERROR(SUM(BJ{0}:BS{0}),0) <23)),\"ALTO\",IF((AND(IFERROR(SUM(BJ{0}:BS{0}),0) >= 23,IFERROR(SUM(BJ{0}:BS{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);


                    /********************   DOMINIOS 1 A 10  ********************/
                    Preguntas.Cells[string.Format("CR{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 0,IFERROR(SUM(P{0}:T{0}),0) <5)),\"NULO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 5,IFERROR(SUM(P{0}:T{0}),0) <9)),\"BAJO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 9,IFERROR(SUM(P{0}:T{0}),0) <11)),\"MEDIO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 11,IFERROR(SUM(P{0}:T{0}),0) <14)),\"ALTO\",IF((AND(IFERROR(SUM(P{0}:T{0}),0) >= 14,IFERROR(SUM(P{0}:T{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CS{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) >= 0,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) <15)),\"NULO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) >= 15,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) <21)),\"BAJO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) >= 21,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) <27)),\"MEDIO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) >= 27,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) <37)),\"ALTO\",IF((AND(IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) >= 37,IFERROR(SUM(U{0}:AE{0},CB{0}:CE{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CT{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) >= 0,IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) <11)),\"NULO\",IF((AND(IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) >= 11,IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) <16)),\"BAJO\",IF((AND(IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) >= 16,IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) <21)),\"MEDIO\",IF((AND(IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) >= 21,IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) <25)),\"ALTO\",IF((AND(IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) >= 25,IFERROR(SUM(AL{0}:AS{0},AX{0},AY{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CU{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 0,IFERROR(SUM(AF{0}:AG{0}),0) <1)),\"NULO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 1,IFERROR(SUM(AF{0}:AG{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 2,IFERROR(SUM(AF{0}:AG{0}),0) <4)),\"MEDIO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 4,IFERROR(SUM(AF{0}:AG{0}),0) <6)),\"ALTO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 6,IFERROR(SUM(AF{0}:AG{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CV{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AH{0}:AK{0}),0) >= 0,IFERROR(SUM(AH{0}:AK{0}),0) <4)),\"NULO\",IF((AND(IFERROR(SUM(AH{0}:AK{0}),0) >= 4,IFERROR(SUM(AH{0}:AK{0}),0) <6)),\"BAJO\",IF((AND(IFERROR(SUM(AH{0}:AK{0}),0) >= 6,IFERROR(SUM(AH{0}:AK{0}),0) <8)),\"MEDIO\",IF((AND(IFERROR(SUM(AH{0}:AK{0}),0) >= 8,IFERROR(SUM(AH{0}:AK{0}),0) <10)),\"ALTO\",IF((AND(IFERROR(SUM(AH{0}:AK{0}),0) >= 10,IFERROR(SUM(AH{0}:AK{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CW{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) >= 0,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) <9)),\"NULO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) >= 9,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) <12)),\"BAJO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) >= 12,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) <16)),\"MEDIO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) >= 16,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) <20)),\"ALTO\",IF((AND(IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) >= 20,IFERROR(SUM(AT{0}:AW{0},AZ{0}:BD{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                   
                    Preguntas.Cells[string.Format("CX{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) >= 0,IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) <10)),\"NULO\",IF((AND(IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) >= 10,IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) <13)),\"BAJO\",IF((AND(IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) >= 13,IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) <17)),\"MEDIO\",IF((AND(IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) >= 17,IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) <21)),\"ALTO\",IF((AND(IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) >= 21,IFERROR(SUM(BE{0}:BI{0},CF{0}:CI{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CY{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 0,IFERROR(SUM(BT{0}:CA{0}),0) <7)),\"NULO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 7,IFERROR(SUM(BT{0}:CA{0}),0) <10)),\"BAJO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 10,IFERROR(SUM(BT{0}:CA{0}),0) <13)),\"MEDIO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 13,IFERROR(SUM(BT{0}:CA{0}),0) <16)),\"ALTO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 16,IFERROR(SUM(BT{0}:CA{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("CZ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(BJ{0}:BO{0}),0) >= 0,IFERROR(SUM(BJ{0}:BO{0}),0) <6)),\"NULO\",IF((AND(IFERROR(SUM(BJ{0}:BO{0}),0) >= 6,IFERROR(SUM(BJ{0}:BO{0}),0) <10)),\"BAJO\",IF((AND(IFERROR(SUM(BJ{0}:BO{0}),0) >= 10,IFERROR(SUM(BJ{0}:BO{0}),0) <14)),\"MEDIO\",IF((AND(IFERROR(SUM(BJ{0}:BO{0}),0) >= 14,IFERROR(SUM(BJ{0}:BO{0}),0) <18)),\"ALTO\",IF((AND(IFERROR(SUM(BJ{0}:BO{0}),0) >= 18,IFERROR(SUM(BJ{0}:BO{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DA{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(BP{0}:BS{0}),0) >= 0,IFERROR(SUM(BP{0}:BS{0}),0) <4)),\"NULO\",IF((AND(IFERROR(SUM(BP{0}:BS{0}),0) >= 4,IFERROR(SUM(BP{0}:BS{0}),0) <6)),\"BAJO\",IF((AND(IFERROR(SUM(BP{0}:BS{0}),0) >= 6,IFERROR(SUM(BP{0}:BS{0}),0) <8)),\"MEDIO\",IF((AND(IFERROR(SUM(BP{0}:BS{0}),0) >= 8,IFERROR(SUM(BP{0}:BS{0}),0) <10)),\"ALTO\",IF((AND(IFERROR(SUM(BP{0}:BS{0}),0) >= 10,IFERROR(SUM(BP{0}:BS{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);


                    /********************   DIMENSIONES 1 A 25  ********************/

                    Preguntas.Cells[string.Format("DB{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(P{0},R{0}),0) >= 0,IFERROR(AVERAGE(P{0},R{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(P{0},R{0}),0) >= 1,IFERROR(AVERAGE(P{0},R{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(P{0},R{0}),0) >= 2,IFERROR(AVERAGE(P{0},R{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(P{0},R{0}),0) >= 3,IFERROR(AVERAGE(P{0},R{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(P{0},R{0}),0) >= 3,IFERROR(AVERAGE(P{0},R{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DC{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(Q{0},S{0}),0) >= 0,IFERROR(AVERAGE(Q{0},S{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(Q{0},S{0}),0) >= 1,IFERROR(AVERAGE(Q{0},S{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(Q{0},S{0}),0) >= 2,IFERROR(AVERAGE(Q{0},S{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(Q{0},S{0}),0) >= 3,IFERROR(AVERAGE(Q{0},S{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(Q{0},S{0}),0) >= 3,IFERROR(AVERAGE(Q{0},S{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DD{0}", rowStart)].Formula = string.Format("IF((AND(T{0} >= 0,T{0} <1)),\"NULO\",IF((AND(T{0} >= 1,T{0} <2)),\"BAJO\",IF((AND(T{0} >= 2,T{0} <3)),\"MEDIO\",IF((AND(T{0} >= 3,T{0} <4)),\"ALTO\",IF((AND(T{0} >= 3,T{0} =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DE{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(U{0},AA{0}),0) >= 0,IFERROR(AVERAGE(U{0},AA{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(U{0},AA{0}),0) >= 1,IFERROR(AVERAGE(U{0},AA{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(U{0},AA{0}),0) >= 2,IFERROR(AVERAGE(U{0},AA{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(U{0},AA{0}),0) >= 3,IFERROR(AVERAGE(U{0},AA{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(U{0},AA{0}),0) >= 3,IFERROR(AVERAGE(U{0},AA{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DF{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 0,IFERROR(AVERAGE(V{0},W{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 1,IFERROR(AVERAGE(V{0},W{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 2,IFERROR(AVERAGE(V{0},W{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 3,IFERROR(AVERAGE(V{0},W{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 3,IFERROR(AVERAGE(V{0},W{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DG{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) >= 0,IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) >= 1,IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) >= 2,IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) >= 3,IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) >= 3,IFERROR(AVERAGE(X{0},Y{0},Z{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DH{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(CB{0}:CE{0}),0) >= 0,IFERROR(AVERAGE(CB{0}:CE{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(CB{0}:CE{0}),0) >= 1,IFERROR(AVERAGE(CB{0}:CE{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(CB{0}:CE{0}),0) >= 2,IFERROR(AVERAGE(CB{0}:CE{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(CB{0}:CE{0}),0) >= 3,IFERROR(AVERAGE(CB{0}:CE{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(CB{0}:CE{0}),0) >= 3,IFERROR(AVERAGE(CB{0}:CE{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DI{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AB{0},AC{0}),0) >= 0,IFERROR(AVERAGE(AB{0},AC{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AB{0},AC{0}),0) >= 1,IFERROR(AVERAGE(AB{0},AC{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AB{0},AC{0}),0) >= 2,IFERROR(AVERAGE(AB{0},AC{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AB{0},AC{0}),0) >= 3,IFERROR(AVERAGE(AB{0},AC{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AB{0},AC{0}),0) >= 3,IFERROR(AVERAGE(AB{0},AC{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DJ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AD{0},AE{0}),0) >= 0,IFERROR(AVERAGE(AD{0},AE{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AD{0},AE{0}),0) >= 1,IFERROR(AVERAGE(AD{0},AE{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AD{0},AE{0}),0) >= 2,IFERROR(AVERAGE(AD{0},AE{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AD{0},AE{0}),0) >= 3,IFERROR(AVERAGE(AD{0},AE{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AD{0},AE{0}),0) >= 3,IFERROR(AVERAGE(AD{0},AE{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DK{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AN{0}:AQ{0}),0) >= 0,IFERROR(AVERAGE(AN{0}:AQ{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AN{0}:AQ{0}),0) >= 1,IFERROR(AVERAGE(AN{0}:AQ{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AN{0}:AQ{0}),0) >= 2,IFERROR(AVERAGE(AN{0}:AQ{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AN{0}:AQ{0}),0) >= 3,IFERROR(AVERAGE(AN{0}:AQ{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AN{0}:AQ{0}),0) >= 3,IFERROR(AVERAGE(AN{0}:AQ{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DL{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AL{0}:AM{0}),0) >= 0,IFERROR(AVERAGE(AL{0}:AM{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AL{0}:AM{0}),0) >= 1,IFERROR(AVERAGE(AL{0}:AM{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AL{0}:AM{0}),0) >= 2,IFERROR(AVERAGE(AL{0}:AM{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AL{0}:AM{0}),0) >= 3,IFERROR(AVERAGE(AL{0}:AM{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AL{0}:AM{0}),0) >= 3,IFERROR(AVERAGE(AL{0}:AM{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DM{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AR{0}:AS{0}),0) >= 0,IFERROR(AVERAGE(AR{0}:AS{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AR{0}:AS{0}),0) >= 1,IFERROR(AVERAGE(AR{0}:AS{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AR{0}:AS{0}),0) >= 2,IFERROR(AVERAGE(AR{0}:AS{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AR{0}:AS{0}),0) >= 3,IFERROR(AVERAGE(AR{0}:AS{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AR{0}:AS{0}),0) >= 3,IFERROR(AVERAGE(AR{0}:AS{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DN{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AX{0}:AY{0}),0) >= 0,IFERROR(AVERAGE(AX{0}:AY{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AX{0}:AY{0}),0) >= 1,IFERROR(AVERAGE(AX{0}:AY{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AX{0}:AY{0}),0) >= 2,IFERROR(AVERAGE(AX{0}:AY{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AX{0}:AY{0}),0) >= 3,IFERROR(AVERAGE(AX{0}:AY{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AX{0}:AY{0}),0) >= 3,IFERROR(AVERAGE(AX{0}:AY{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DO{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 0,IFERROR(SUM(AF{0}:AG{0}),0) <1)),\"NULO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 1,IFERROR(SUM(AF{0}:AG{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 2,IFERROR(SUM(AF{0}:AG{0}),0) <4)),\"MEDIO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 4,IFERROR(SUM(AF{0}:AG{0}),0) <6)),\"ALTO\",IF((AND(IFERROR(SUM(AF{0}:AG{0}),0) >= 6,IFERROR(SUM(AF{0}:AG{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DP{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AH{0}:AI{0}),0) >= 0,IFERROR(AVERAGE(AH{0}:AI{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AH{0}:AI{0}),0) >= 1,IFERROR(AVERAGE(AH{0}:AI{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AH{0}:AI{0}),0) >= 2,IFERROR(AVERAGE(AH{0}:AI{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AH{0}:AI{0}),0) >= 3,IFERROR(AVERAGE(AH{0}:AI{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AH{0}:AI{0}),0) >= 3,IFERROR(AVERAGE(AH{0}:AI{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DQ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AJ{0}:AK{0}),0) >= 0,IFERROR(AVERAGE(AJ{0}:AK{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AJ{0}:AK{0}),0) >= 1,IFERROR(AVERAGE(AJ{0}:AK{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AJ{0}:AK{0}),0) >= 2,IFERROR(AVERAGE(AJ{0}:AK{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AJ{0}:AK{0}),0) >= 3,IFERROR(AVERAGE(AJ{0}:AK{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AJ{0}:AK{0}),0) >= 3,IFERROR(AVERAGE(AJ{0}:AK{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DR{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AT{0}:AW{0}),0) >= 0,IFERROR(AVERAGE(AT{0}:AW{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AT{0}:AW{0}),0) >= 1,IFERROR(AVERAGE(AT{0}:AW{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AT{0}:AW{0}),0) >= 2,IFERROR(AVERAGE(AT{0}:AW{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AT{0}:AW{0}),0) >= 3,IFERROR(AVERAGE(AT{0}:AW{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AT{0}:AW{0}),0) >= 3,IFERROR(AVERAGE(AT{0}:AW{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DS{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AZ{0}:BD{0}),0) >= 0,IFERROR(AVERAGE(AZ{0}:BD{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AZ{0}:BD{0}),0) >= 1,IFERROR(AVERAGE(AZ{0}:BD{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AZ{0}:BD{0}),0) >= 2,IFERROR(AVERAGE(AZ{0}:BD{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AZ{0}:BD{0}),0) >= 3,IFERROR(AVERAGE(AZ{0}:BD{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AZ{0}:BD{0}),0) >= 3,IFERROR(AVERAGE(AZ{0}:BD{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DT{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BE{0}:BI{0}),0) >= 0,IFERROR(AVERAGE(BE{0}:BI{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BE{0}:BI{0}),0) >= 1,IFERROR(AVERAGE(BE{0}:BI{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BE{0}:BI{0}),0) >= 2,IFERROR(AVERAGE(BE{0}:BI{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BE{0}:BI{0}),0) >= 3,IFERROR(AVERAGE(BE{0}:BI{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BE{0}:BI{0}),0) >= 3,IFERROR(AVERAGE(BE{0}:BI{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DU{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(CF{0}:CI{0}),0) >= 0,IFERROR(AVERAGE(CF{0}:CI{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(CF{0}:CI{0}),0) >= 1,IFERROR(AVERAGE(CF{0}:CI{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(CF{0}:CI{0}),0) >= 2,IFERROR(AVERAGE(CF{0}:CI{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(CF{0}:CI{0}),0) >= 3,IFERROR(AVERAGE(CF{0}:CI{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(CF{0}:CI{0}),0) >= 3,IFERROR(AVERAGE(CF{0}:CI{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DV{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 0,IFERROR(SUM(BT{0}:CA{0}),0) <7)),\"NULO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 7,IFERROR(SUM(BT{0}:CA{0}),0) <10)),\"BAJO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 10,IFERROR(SUM(BT{0}:CA{0}),0) <13)),\"MEDIO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 13,IFERROR(SUM(BT{0}:CA{0}),0) <16)),\"ALTO\",IF((AND(IFERROR(SUM(BT{0}:CA{0}),0) >= 16,IFERROR(SUM(BT{0}:CA{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);


                    Preguntas.Cells[string.Format("DW{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BJ{0}:BK{0}),0) >= 0,IFERROR(AVERAGE(BJ{0}:BK{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BJ{0}:BK{0}),0) >= 1,IFERROR(AVERAGE(BJ{0}:BK{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BJ{0}:BK{0}),0) >= 2,IFERROR(AVERAGE(BJ{0}:BK{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BJ{0}:BK{0}),0) >= 3,IFERROR(AVERAGE(BJ{0}:BK{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BJ{0}:BK{0}),0) >= 3,IFERROR(AVERAGE(BJ{0}:BK{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DX{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BL{0}:BO{0}),0) >= 0,IFERROR(AVERAGE(BL{0}:BO{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BL{0}:BO{0}),0) >= 1,IFERROR(AVERAGE(BL{0}:BO{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BL{0}:BO{0}),0) >= 2,IFERROR(AVERAGE(BL{0}:BO{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BL{0}:BO{0}),0) >= 3,IFERROR(AVERAGE(BL{0}:BO{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BL{0}:BO{0}),0) >= 3,IFERROR(AVERAGE(BL{0}:BO{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DY{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BR{0}:BS{0}),0) >= 0,IFERROR(AVERAGE(BR{0}:BS{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BR{0}:BS{0}),0) >= 1,IFERROR(AVERAGE(BR{0}:BS{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BR{0}:BS{0}),0) >= 2,IFERROR(AVERAGE(BR{0}:BS{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BR{0}:BS{0}),0) >= 3,IFERROR(AVERAGE(BR{0}:BS{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BR{0}:BS{0}),0) >= 3,IFERROR(AVERAGE(BR{0}:BS{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas.Cells[string.Format("DZ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BP{0}:BQ{0}),0) >= 0,IFERROR(AVERAGE(BP{0}:BQ{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BP{0}:BQ{0}),0) >= 1,IFERROR(AVERAGE(BP{0}:BQ{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BP{0}:BQ{0}),0) >= 2,IFERROR(AVERAGE(BP{0}:BQ{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BP{0}:BQ{0}),0) >= 3,IFERROR(AVERAGE(BP{0}:BQ{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BP{0}:BQ{0}),0) >= 3,IFERROR(AVERAGE(BP{0}:BQ{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);




                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Sexo is null)
                    { Preguntas.Cells[string.Format("E{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("E{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Sexo_N01.Sexo; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Edad is null)
                    { Preguntas.Cells[string.Format("F{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("F{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Edades_N01.edad; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Estado_Civil is null)
                    { Preguntas.Cells[string.Format("G{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("G{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Estado_Civil_N01.Estado_Civil; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Nivel_Estudios is null)
                    { Preguntas.Cells[string.Format("H{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("H{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Nivel_Estudios_N01.Nivel_Estudios; }
                    
                    if (item.ERGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo is null)
                    { Preguntas.Cells[string.Format("I{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("I{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.ERGOS_Centros_Trabajo_N01.Nombre_centro_trabajo; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Tipo_puesto is null)
                    { Preguntas.Cells[string.Format("J{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("J{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Tipo_puesto_N01.Tipo_puesto; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Tipo_Contratacion is null)
                    { Preguntas.Cells[string.Format("K{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("K{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Tipo_Contratacion_N01.Tipo_Contratacion; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Tipo_Jornada is null)
                    { Preguntas.Cells[string.Format("L{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("L{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Tipo_Jornada_N01.Tipo_Jornada; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Rotacion_Turno is null)
                    { Preguntas.Cells[string.Format("M{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("M{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Rotacion_Turno_N01.Rotacion_Turno; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Experiencia_puesto_actual is null)
                    { Preguntas.Cells[string.Format("N{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("N{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Experiencia_puesto_N01.Experiencia_puesto; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Experiencia_puesto_laboral is null)
                    { Preguntas.Cells[string.Format("O{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("O{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Experiencia_puesto_N011.Experiencia_puesto; }





                    switch (item.id_pregunta)
                    {
                        case 1:

                            Preguntas.Cells[string.Format("P{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("P{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                //  Preguntas.Cells[string.Format("P{0}", rowStart)].Value = "NULO";
                                Preguntas.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                // Preguntas.Cells[string.Format("P{0}", rowStart)].Value = "BAJO";
                                Preguntas.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                //  Preguntas.Cells[string.Format("P{0}", rowStart)].Value = "MEDIO";
                                Preguntas.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                //   Preguntas.Cells[string.Format("P{0}", rowStart)].Value = "ALTO";
                                Preguntas.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                //  Preguntas.Cells[string.Format("P{0}", rowStart)].Value = "MUY ALTO";
                                Preguntas.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 2:
                            Preguntas.Cells[string.Format("Q{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("Q{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                //Preguntas.Cells[string.Format("Q{0}", rowStart)].Value = "NULO";
                                Preguntas.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                //    Preguntas.Cells[string.Format("Q{0}", rowStart)].Value = "BAJO";
                                Preguntas.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                //  Preguntas.Cells[string.Format("Q{0}", rowStart)].Value = "MEDIO";
                                Preguntas.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                //  Preguntas.Cells[string.Format("Q{0}", rowStart)].Value = "ALTO";
                                Preguntas.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                //     Preguntas.Cells[string.Format("Q{0}", rowStart)].Value = "MUY ALTO";
                                Preguntas.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 3:
                            Preguntas.Cells[string.Format("R{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("R{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 4:
                            Preguntas.Cells[string.Format("S{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("S{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 5:
                            Preguntas.Cells[string.Format("T{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("T{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 6:
                            Preguntas.Cells[string.Format("U{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("U{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 7:
                            Preguntas.Cells[string.Format("V{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("V{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 8:
                            Preguntas.Cells[string.Format("W{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("W{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 9:
                            Preguntas.Cells[string.Format("X{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("X{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 10:
                            Preguntas.Cells[string.Format("Y{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("Y{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 11:
                            Preguntas.Cells[string.Format("Z{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("Z{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 12:
                            Preguntas.Cells[string.Format("AA{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AA{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 13:
                            Preguntas.Cells[string.Format("AB{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AB{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 14:
                            Preguntas.Cells[string.Format("AC{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AC{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 15:
                            Preguntas.Cells[string.Format("AD{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AD{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 16:
                            Preguntas.Cells[string.Format("AE{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AE{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 17:
                            Preguntas.Cells[string.Format("AF{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AF{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 18:
                            Preguntas.Cells[string.Format("AG{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AG{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 19:
                            Preguntas.Cells[string.Format("AH{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AH{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 20:
                            Preguntas.Cells[string.Format("AI{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AI{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 21:
                            Preguntas.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AJ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 22:
                            Preguntas.Cells[string.Format("AK{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AK{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 23:
                            Preguntas.Cells[string.Format("AL{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AL{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 24:
                            Preguntas.Cells[string.Format("AM{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AM{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 25:
                            Preguntas.Cells[string.Format("AN{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AN{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 26:
                            Preguntas.Cells[string.Format("AO{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AO{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 27:
                            Preguntas.Cells[string.Format("AP{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AP{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 28:
                            Preguntas.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AQ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 29:
                            Preguntas.Cells[string.Format("AR{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AR{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 30:
                            Preguntas.Cells[string.Format("AS{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AS{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 31:
                            Preguntas.Cells[string.Format("AT{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AT{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 32:
                            Preguntas.Cells[string.Format("AU{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AU{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 33:
                            Preguntas.Cells[string.Format("AV{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AV{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 34:
                            Preguntas.Cells[string.Format("AW{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AW{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 35:
                            Preguntas.Cells[string.Format("AX{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AX{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 36:
                            Preguntas.Cells[string.Format("AY{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AY{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 37:
                            Preguntas.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("AZ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 38:
                            Preguntas.Cells[string.Format("BA{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BA{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 39:
                            Preguntas.Cells[string.Format("BB{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BB{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 40:
                            Preguntas.Cells[string.Format("BC{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BC{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 41:
                            Preguntas.Cells[string.Format("BD{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BD{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 42:
                            Preguntas.Cells[string.Format("BE{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BE{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 43:
                            Preguntas.Cells[string.Format("BF{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BF{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 44:
                            Preguntas.Cells[string.Format("BG{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BG{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 45:
                            Preguntas.Cells[string.Format("BH{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BH{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 46:
                            Preguntas.Cells[string.Format("BI{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BI{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 47:
                            Preguntas.Cells[string.Format("BJ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BJ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 48:
                            Preguntas.Cells[string.Format("BK{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BK{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 49:
                            Preguntas.Cells[string.Format("BL{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BL{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 50:
                            Preguntas.Cells[string.Format("BM{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BM{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 51:
                            Preguntas.Cells[string.Format("BN{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BN{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 52:
                            Preguntas.Cells[string.Format("BO{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BO{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 53:
                            Preguntas.Cells[string.Format("BP{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BP{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 54:
                            Preguntas.Cells[string.Format("BQ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BQ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 55:
                            Preguntas.Cells[string.Format("BR{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BR{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 56:
                            Preguntas.Cells[string.Format("BS{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BS{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 57:
                            Preguntas.Cells[string.Format("BT{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BT{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 58:
                            Preguntas.Cells[string.Format("BU{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BU{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 59:
                            Preguntas.Cells[string.Format("BV{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BV{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 60:
                            Preguntas.Cells[string.Format("BW{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BW{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 61:
                            Preguntas.Cells[string.Format("BX{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BX{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 62:
                            Preguntas.Cells[string.Format("BY{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BY{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 63:
                            Preguntas.Cells[string.Format("BZ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("BZ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("BZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("BZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("BZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("BZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("BZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 64:
                            Preguntas.Cells[string.Format("CA{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CA{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 65:
                            Preguntas.Cells[string.Format("CB{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CB{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 66:
                            Preguntas.Cells[string.Format("CC{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CC{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 67:
                            Preguntas.Cells[string.Format("CD{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CD{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 68:
                            Preguntas.Cells[string.Format("CE{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CE{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 69:
                            Preguntas.Cells[string.Format("CF{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CF{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 70:
                            Preguntas.Cells[string.Format("CG{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CG{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 71:
                            Preguntas.Cells[string.Format("CH{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CH{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 72:
                            Preguntas.Cells[string.Format("CI{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas.Cells[string.Format("CI{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas.Cells[string.Format("CI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas.Cells[string.Format("CI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas.Cells[string.Format("CI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas.Cells[string.Format("CI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas.Cells[string.Format("CI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            counter_flag = 1;
                            break;
                        default:
                            break;
                    }
                    if (counter_flag == 1)
                    {
                        rowStart++;
                        counter_flag = 0;
                    }

                }

                Preguntas.Cells["A:DZ"].AutoFitColumns();
                Preguntas.Column(7).BestFit = true;
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "Reporte_Proyecto.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();

            }
            else if (id_cuestionario == 2)
            {
                ExcelPackage pck_S2 = new ExcelPackage();

                ExcelWorksheet Preguntas_S2 = pck_S2.Workbook.Worksheets.Add("Base de Datos Encuestados");


                //SURVEY______________2
                Preguntas_S2.Cells["A2"].Value = "Nombre";
                Preguntas_S2.Cells["B2"].Value = "No. De Folio";
                Preguntas_S2.Cells["C2"].Value = "Ocupación";
                Preguntas_S2.Cells["D2"].Value = "Departamento";
                Preguntas_S2.Cells["E2"].Value = "Género";

                // NEW ADDED 09/16/2019 ################################
                Preguntas_S2.Cells["F2"].Value = "Edad";
                Preguntas_S2.Cells["G2"].Value = "Estado Civil";
                Preguntas_S2.Cells["H2"].Value = "Nivel de Estudios";
                Preguntas_S2.Cells["I2"].Value = "Centro de Trabajo";
                Preguntas_S2.Cells["J2"].Value = "Tipo de Puesto";
                Preguntas_S2.Cells["K2"].Value = "Tipo de Contratación";
                Preguntas_S2.Cells["L2"].Value = "Tipo de Jornada";
                Preguntas_S2.Cells["M2"].Value = "Rotación de Turno";
                Preguntas_S2.Cells["N2"].Value = "Experiencia Puesto Laboral";
                Preguntas_S2.Cells["O2"].Value = "Experiencia Puesto Actual";
                // ########################################################

                Preguntas_S2.Cells["P2"].Value = "PREGUNTA 1";
                Preguntas_S2.Cells["Q2"].Value = "PREGUNTA 2";
                Preguntas_S2.Cells["R2"].Value = "PREGUNTA 3";
                Preguntas_S2.Cells["S2"].Value = "PREGUNTA 4";
                Preguntas_S2.Cells["T2"].Value = "PREGUNTA 5";
                Preguntas_S2.Cells["U2"].Value = "PREGUNTA 6";
                Preguntas_S2.Cells["V2"].Value = "PREGUNTA 7";
                Preguntas_S2.Cells["W2"].Value = "PREGUNTA 8";
                Preguntas_S2.Cells["X2"].Value = "PREGUNTA 9";
                Preguntas_S2.Cells["Y2"].Value = "PREGUNTA 10";
                Preguntas_S2.Cells["Z2"].Value = "PREGUNTA 11";
                Preguntas_S2.Cells["AA2"].Value = "PREGUNTA 12";
                Preguntas_S2.Cells["AB2"].Value = "PREGUNTA 13";
                Preguntas_S2.Cells["AC2"].Value = "PREGUNTA 14";
                Preguntas_S2.Cells["AD2"].Value = "PREGUNTA 15";
                Preguntas_S2.Cells["AE2"].Value = "PREGUNTA 16";
                Preguntas_S2.Cells["AF2"].Value = "PREGUNTA 17";
                Preguntas_S2.Cells["AG2"].Value = "PREGUNTA 18";
                Preguntas_S2.Cells["AH2"].Value = "PREGUNTA 19";
                Preguntas_S2.Cells["AI2"].Value = "PREGUNTA 20";
                Preguntas_S2.Cells["AJ2"].Value = "PREGUNTA 21";
                Preguntas_S2.Cells["AK2"].Value = "PREGUNTA 22";
                Preguntas_S2.Cells["AL2"].Value = "PREGUNTA 23";
                Preguntas_S2.Cells["AM2"].Value = "PREGUNTA 24";
                Preguntas_S2.Cells["AN2"].Value = "PREGUNTA 25";
                Preguntas_S2.Cells["AO2"].Value = "PREGUNTA 26";
                Preguntas_S2.Cells["AP2"].Value = "PREGUNTA 27";
                Preguntas_S2.Cells["AQ2"].Value = "PREGUNTA 28";
                Preguntas_S2.Cells["AR2"].Value = "PREGUNTA 29";
                Preguntas_S2.Cells["AS2"].Value = "PREGUNTA 30";
                Preguntas_S2.Cells["AT2"].Value = "PREGUNTA 31";
                Preguntas_S2.Cells["AU2"].Value = "PREGUNTA 32";
                Preguntas_S2.Cells["AV2"].Value = "PREGUNTA 33";
                Preguntas_S2.Cells["AW2"].Value = "PREGUNTA 34";
                Preguntas_S2.Cells["AX2"].Value = "PREGUNTA 35";
                Preguntas_S2.Cells["AY2"].Value = "PREGUNTA 36";
                Preguntas_S2.Cells["AZ2"].Value = "PREGUNTA 37";
                Preguntas_S2.Cells["BA2"].Value = "PREGUNTA 38";
                Preguntas_S2.Cells["BB2"].Value = "PREGUNTA 39";
                Preguntas_S2.Cells["BC2"].Value = "PREGUNTA 40";
                Preguntas_S2.Cells["BD2"].Value = "PREGUNTA 41";
                Preguntas_S2.Cells["BE2"].Value = "PREGUNTA 42";
                Preguntas_S2.Cells["BF2"].Value = "PREGUNTA 43";
                Preguntas_S2.Cells["BG2"].Value = "PREGUNTA 44";
                Preguntas_S2.Cells["BH2"].Value = "PREGUNTA 45";
                Preguntas_S2.Cells["BI2"].Value = "PREGUNTA 46";
                Preguntas_S2.Cells["BJ2"].Value = "Canalizacion";
                Preguntas_S2.Cells["BK2"].Value = "Puntaje Final";
                Preguntas_S2.Cells["BL2"].Value = "Calif Final";

                //***************************************************
                // IMPRIMIENDO CATEGORIAS DOMINIOS Y DIMENSIONES EN EXCEL CUESTIONARIO II   //
                //***************************************************
                Preguntas_S2.Cells["BM2"].Value = "Categoría 1";
                Preguntas_S2.Cells["BN2"].Value = "Categoría 2";
                Preguntas_S2.Cells["BO2"].Value = "Categoría 3";
                Preguntas_S2.Cells["BP2"].Value = "Categoría 4"; 
                Preguntas_S2.Cells["BQ2"].Value = "Dominio 1";
                Preguntas_S2.Cells["BR2"].Value = "Dominio 2";
                Preguntas_S2.Cells["BS2"].Value = "Dominio 3";
                Preguntas_S2.Cells["BT2"].Value = "Dominio 4";
                Preguntas_S2.Cells["BU2"].Value = "Dominio 5";
                Preguntas_S2.Cells["BV2"].Value = "Dominio 6";
                Preguntas_S2.Cells["BW2"].Value = "Dominio 7";
                Preguntas_S2.Cells["BX2"].Value = "Dominio 8"; 
                Preguntas_S2.Cells["BY2"].Value = "Dimensión 1";
                Preguntas_S2.Cells["BZ2"].Value = "Dimensión 2";
                Preguntas_S2.Cells["CA2"].Value = "Dimensión 3";
                Preguntas_S2.Cells["CB2"].Value = "Dimensión 4";
                Preguntas_S2.Cells["CC2"].Value = "Dimensión 5";
                Preguntas_S2.Cells["CD2"].Value = "Dimensión 6";
                Preguntas_S2.Cells["CE2"].Value = "Dimensión 7";
                Preguntas_S2.Cells["CF2"].Value = "Dimensión 8";
                Preguntas_S2.Cells["CG2"].Value = "Dimensión 9";
                Preguntas_S2.Cells["CH2"].Value = "Dimensión 10";
                Preguntas_S2.Cells["CI2"].Value = "Dimensión 11";
                Preguntas_S2.Cells["CJ2"].Value = "Dimensión 12";
                Preguntas_S2.Cells["CK2"].Value = "Dimensión 13";
                Preguntas_S2.Cells["CL2"].Value = "Dimensión 14";
                Preguntas_S2.Cells["CM2"].Value = "Dimensión 15";
                Preguntas_S2.Cells["CN2"].Value = "Dimensión 16";
                Preguntas_S2.Cells["CO2"].Value = "Dimensión 17";
                Preguntas_S2.Cells["CP2"].Value = "Dimensión 18";
                Preguntas_S2.Cells["CQ2"].Value = "Dimensión 19";
                Preguntas_S2.Cells["CR2"].Value = "Dimensión 20";
            
                //Preguntas_S2.Cells["BK2"].Value = "Condiciones peligrosas e inseguras";
                //Preguntas_S2.Cells["BL2"].Value = "Condiciones deficientes e insalubres";
                //Preguntas_S2.Cells["BM2"].Value = "Trabajos peligrosos";
                //Preguntas_S2.Cells["BN2"].Value = "Cargas cuantitativas";
                //Preguntas_S2.Cells["BO2"].Value = "Ritmos de trabajo acelerado";
                //Preguntas_S2.Cells["BP2"].Value = "Carga mental";
                //Preguntas_S2.Cells["BQ2"].Value = "Cargas psicológicas emocionales";
                //Preguntas_S2.Cells["BR2"].Value = "Cargas de alta responsabilidad";
                //Preguntas_S2.Cells["BS2"].Value = "Cargas contradictorias o inconsistentes";
                //Preguntas_S2.Cells["BT2"].Value = "Falta de control y autonomía sobre el trabajo";
                //Preguntas_S2.Cells["BU2"].Value = "Limitada o nula posibilidad de desarrollo";
                //Preguntas_S2.Cells["BV2"].Value = "Limitada o inexistente capacitación";
                //Preguntas_S2.Cells["BW2"].Value = "Jornadas de trabajo extensas";
                //Preguntas_S2.Cells["BX2"].Value = "Influencia del trabajo fuera del centro laboral";
                //Preguntas_S2.Cells["BY2"].Value = "Influencia de las responsabilidades familiares";
                //Preguntas_S2.Cells["BZ2"].Value = "Escasa claridad de funciones";
                //Preguntas_S2.Cells["CA2"].Value = "Características del liderazgo";
                //Preguntas_S2.Cells["CB2"].Value = "Relaciones sociales en el trabajo";
                //Preguntas_S2.Cells["CC2"].Value = "Deficiente relación con los colaboradores que supervisa";
                //Preguntas_S2.Cells["CD2"].Value = "Violencia laboral";



                foreach (var item in emplist)
                {          

                    Preguntas_S2.Cells["A1:CR2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    //Preguntas_S2.Cells[string.Format("BK3:CD{0}", emplist.Count())].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    Preguntas_S2.Cells["A1:CR2"].Style.Font.Bold = true;
                    Preguntas_S2.Cells["A1:CR2"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("white")));
                    Preguntas_S2.Cells["A2:CR2"].Style.Font.Size = 11;
                    Preguntas_S2.Cells["A1:CR1"].Style.Font.Size = 25;
                    Preguntas_S2.Cells["P2:CR2"].Style.TextRotation = 90;
                    Preguntas_S2.Cells["A1:CR1"].Value = "";
                    Preguntas_S2.Cells["A1:CR1"].Merge = true;
                    Preguntas_S2.Cells["A1:CR1"].Value = "Check035";
                    Preguntas_S2.Cells["A1:CR1"].Merge = true;
                    Preguntas_S2.Cells["A1:CR2"].Style.Font.Name = "Calibri";
                    Preguntas_S2.Cells["A1:CR2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9071ae")));

                    //  Preguntas_S2.Column.AutoFit();


                    Preguntas_S2.Cells[string.Format("A{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Nombre;
                    Preguntas_S2.Cells[string.Format("B{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.id_trabajador;
                    Preguntas_S2.Cells[string.Format("C{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Ocupacion;
                    Preguntas_S2.Cells[string.Format("D{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Departamento;

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Sexo is null)
                    { Preguntas_S2.Cells[string.Format("E{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("E{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Sexo_N01.Sexo; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Edad is null)
                    { Preguntas_S2.Cells[string.Format("F{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("F{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Edades_N01.edad; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Estado_Civil is null)
                    { Preguntas_S2.Cells[string.Format("G{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("G{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Estado_Civil_N01.Estado_Civil; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Nivel_Estudios is null)
                    { Preguntas_S2.Cells[string.Format("H{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("H{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Nivel_Estudios_N01.Nivel_Estudios; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo is null)
                    { Preguntas_S2.Cells[string.Format("I{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("I{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.ERGOS_Centros_Trabajo_N01.Nombre_centro_trabajo; }


                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Tipo_puesto is null)
                    { Preguntas_S2.Cells[string.Format("J{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("J{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Tipo_puesto_N01.Tipo_puesto; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Tipo_Contratacion is null)
                    { Preguntas_S2.Cells[string.Format("K{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("K{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Tipo_Contratacion_N01.Tipo_Contratacion; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Tipo_Jornada is null)
                    { Preguntas_S2.Cells[string.Format("L{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("L{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Tipo_Jornada_N01.Tipo_Jornada; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Rotacion_Turno is null)
                    { Preguntas_S2.Cells[string.Format("M{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("M{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Rotacion_Turno_N01.Rotacion_Turno; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Experiencia_puesto_actual is null)
                    { Preguntas_S2.Cells[string.Format("N{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("N{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Experiencia_puesto_N01.Experiencia_puesto; }

                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Experiencia_puesto_laboral is null)
                    { Preguntas_S2.Cells[string.Format("O{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("O{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.DATA_Experiencia_puesto_N011.Experiencia_puesto; }




                    if (item.ERGOS_Cuestionarios_Trabajador_N01.Canalizacion is null)
                    { Preguntas_S2.Cells[string.Format("BJ{0}", rowStart)].Value = "Sin Información"; }
                    else
                    {
                        if (item.ERGOS_Cuestionarios_Trabajador_N01.Canalizacion == 1)
                        { Preguntas_S2.Cells[string.Format("BJ{0}", rowStart)].Value = "Si"; }
                        else if (item.ERGOS_Cuestionarios_Trabajador_N01.Canalizacion == 0) { Preguntas_S2.Cells[string.Format("BJ{0}", rowStart)].Value = "No"; }
                    }


                  


                    /*PUNTAJE FINAL*/
                    Preguntas_S2.Cells[string.Format("BK{0}", rowStart)].Formula = string.Format("SUM(P{0}:BI{0})", rowStart);
                    //CALIFICACION FINAL
                    Preguntas_S2.Cells[string.Format("BL{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(P{0}:BI{0}),0) >= 0,IFERROR(SUM(P{0}:BI{0}),0) <20)),\"NULO\",IF((AND(IFERROR(SUM(P{0}:BI{0}),0) >= 20,IFERROR(SUM(P{0}:BI{0}),0) <45)),\"BAJO\",IF((AND(IFERROR(SUM(P{0}:BI{0}),0) >= 45,IFERROR(SUM(P{0}:BI{0}),0) <70)),\"MEDIO\",IF((AND(IFERROR(SUM(P{0}:BI{0}),0) >= 70,IFERROR(SUM(P{0}:BI{0}),0) <90)),\"ALTO\",IF((AND(IFERROR(SUM(P{0}:BI{0}),0) >= 90,IFERROR(SUM(P{0}:BI{0}),0) <=200)),\"MUY ALTO\",\"N/A\")))))", rowStart);

                    //Preguntas_S2.Cells[string.Format("BL{0}", rowStart)].Formula = string.Format("IF((AND(SUM(P{0}:BI{0} <20)),\"NULO\",IF((AND(SUM(P{0}:BI{0} >= 20,SUM(P{0}:BI{0} <45)),\"BAJO\",IF((AND(SUM(P{0}:BI{0} >= 45,SUM(P{0}:BI{0} <70)),\"MEDIO\",IF((AND(SUM(P{0}:BI{0} >= 70,SUM(P{0}:BI{0} <90)),\"ALTO\",IF((AND(SUM(P{0}:BI{0} >= 90,SUM(P{0}:BI{0} =200)),\"MUY ALTO\",\"N/A\")))))", rowStart);

                    /********************   CATEGORIAS 1 A 4  ********************/
                    Preguntas_S2.Cells[string.Format("BM{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 0,IFERROR(SUM(P{0}:R{0}),0) <3)),\"NULO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 3,IFERROR(SUM(P{0}:R{0}),0) <5)),\"BAJO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 5,IFERROR(SUM(P{0}:R{0}),0) <7)),\"MEDIO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 7,IFERROR(SUM(P{0}:R{0}),0) <9)),\"ALTO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 9,IFERROR(SUM(P{0}:R{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BN{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) >= 0,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) <10)),\"NULO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) >= 10,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) <20)),\"BAJO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) >= 20,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) <30)),\"MEDIO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) >= 30,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) <40)),\"ALTO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) >= 40,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0},AG{0}:AK{0},AO{0},AP{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BO{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AC{0}:AF{0}),0) >= 0,IFERROR(SUM(AC{0}:AF{0}),0) <4)),\"NULO\",IF((AND(IFERROR(SUM(AC{0}:AF{0}),0) >= 4,IFERROR(SUM(AC{0}:AF{0}),0) <6)),\"BAJO\",IF((AND(IFERROR(SUM(AC{0}:AF{0}),0) >= 6,IFERROR(SUM(AC{0}:AF{0}),0) <9)),\"MEDIO\",IF((AND(IFERROR(SUM(AC{0}:AF{0}),0) >= 9,IFERROR(SUM(AC{0}:AF{0}),0) <12)),\"ALTO\",IF((AND(IFERROR(SUM(AC{0}:AF{0}),0) >= 12,IFERROR(SUM(AC{0}:AF{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BP{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) >= 0,IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) <10)),\"NULO\",IF((AND(IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) >= 10,IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) <20)),\"BAJO\",IF((AND(IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) >= 20,IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) <30)),\"MEDIO\",IF((AND(IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) >= 30,IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) <40)),\"ALTO\",IF((AND(IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) >= 40,IFERROR(SUM(AQ{0}:BC{0},AL{0}:AN{0},BG{0}:BI{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);

                    /********************   DOMINIOS 1 A 8  ********************/
                    Preguntas_S2.Cells[string.Format("BQ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 0,IFERROR(SUM(P{0}:R{0}),0) <3)),\"NULO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 3,IFERROR(SUM(P{0}:R{0}),0) <5)),\"BAJO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 5,IFERROR(SUM(P{0}:R{0}),0) <7)),\"MEDIO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 7,IFERROR(SUM(P{0}:R{0}),0) <9)),\"ALTO\",IF((AND(IFERROR(SUM(P{0}:R{0}),0) >= 9,IFERROR(SUM(P{0}:R{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                   
                    Preguntas_S2.Cells[string.Format("BR{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) >= 0,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) <12)),\"NULO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) >= 12,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) <16)),\"BAJO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) >= 16,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) <20)),\"MEDIO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) >= 20,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) <24)),\"ALTO\",IF((AND(IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) >= 24,IFERROR(SUM(S{0}:AB{0},BD{0}:BF{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BS{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) >= 0,IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) <5)),\"NULO\",IF((AND(IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) >= 5,IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) <8)),\"BAJO\",IF((AND(IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) >= 8,IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) <11)),\"MEDIO\",IF((AND(IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) >= 11,IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) <14)),\"ALTO\",IF((AND(IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) >= 14,IFERROR(SUM(AG{0}:AK{0},AO{0},AP{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                   
                    Preguntas_S2.Cells[string.Format("BT{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 0,IFERROR(SUM(AC{0}:AD{0}),0) <1)),\"NULO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 1,IFERROR(SUM(AC{0}:AD{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 2,IFERROR(SUM(AC{0}:AD{0}),0) <4)),\"MEDIO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 4,IFERROR(SUM(AC{0}:AD{0}),0) <6)),\"ALTO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 6,IFERROR(SUM(AC{0}:AD{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BU{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AE{0},AF{0}),0) >= 0,IFERROR(SUM(AE{0},AF{0}),0) <1)),\"NULO\",IF((AND(IFERROR(SUM(AE{0},AF{0}),0) >= 1,IFERROR(SUM(AE{0},AF{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(SUM(AE{0},AF{0}),0) >= 2,IFERROR(SUM(AE{0},AF{0}),0) <4)),\"MEDIO\",IF((AND(IFERROR(SUM(AE{0},AF{0}),0) >= 4,IFERROR(SUM(AE{0},AF{0}),0) <6)),\"ALTO\",IF((AND(IFERROR(SUM(AE{0},AF{0}),0) >= 6,IFERROR(SUM(AE{0},AF{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BV{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) >= 0,IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) <3)),\"NULO\",IF((AND(IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) >= 3,IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) <5)),\"BAJO\",IF((AND(IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) >= 5,IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) <8)),\"MEDIO\",IF((AND(IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) >= 8,IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) <11)),\"ALTO\",IF((AND(IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) >= 11,IFERROR(SUM(AL{0}:AN{0},AQ{0}:AR{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BW{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) >= 0,IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) <5)),\"NULO\",IF((AND(IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) >= 5,IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) <8)),\"BAJO\",IF((AND(IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) >= 8,IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) <11)),\"MEDIO\",IF((AND(IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) >= 11,IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) <14)),\"ALTO\",IF((AND(IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) >= 14,IFERROR(SUM(AS{0}:AU{0},BG{0}:BI{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BX{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 0,IFERROR(SUM(AV{0}:BC{0}),0) <7)),\"NULO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 7,IFERROR(SUM(AV{0}:BC{0}),0) <10)),\"BAJO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 10,IFERROR(SUM(AV{0}:BC{0}),0) <13)),\"MEDIO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 13,IFERROR(SUM(AV{0}:BC{0}),0) <16)),\"ALTO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 16,IFERROR(SUM(AV{0}:BC{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);


                    /********************   DIMENSIONES 0 A 20  ********************/
                    Preguntas_S2.Cells[string.Format("BY{0}", rowStart)].Formula = string.Format("IF((AND(Q{0} >= 0,Q{0} <1)),\"NULO\",IF((AND(Q{0} >= 1,Q{0} <2)),\"BAJO\",IF((AND(Q{0} >= 2,Q{0} <3)),\"MEDIO\",IF((AND(Q{0} >= 3,Q{0} <4)),\"ALTO\",IF((AND(Q{0} >= 3,Q{0} =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("BZ{0}", rowStart)].Formula = string.Format("IF((AND(P{0} >= 0,P{0} <1)),\"NULO\",IF((AND(P{0} >= 1,P{0} <2)),\"BAJO\",IF((AND(P{0} >= 2,P{0} <3)),\"MEDIO\",IF((AND(P{0} >= 3,P{0} <4)),\"ALTO\",IF((AND(P{0} >= 3,P{0} =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CA{0}", rowStart)].Formula = string.Format("IF((AND(R{0} >= 0,R{0} <1)),\"NULO\",IF((AND(R{0} >= 1,R{0} <2)),\"BAJO\",IF((AND(R{0} >= 2,R{0} <3)),\"MEDIO\",IF((AND(R{0} >= 3,R{0} <4)),\"ALTO\",IF((AND(R{0} >= 3,R{0} =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CB{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(S{0},X{0}),0) >= 0,IFERROR(AVERAGE(S{0},X{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(S{0},X{0}),0) >= 1,IFERROR(AVERAGE(S{0},X{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(S{0},X{0}),0) >= 2,IFERROR(AVERAGE(S{0},X{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(S{0},X{0}),0) >= 3,IFERROR(AVERAGE(S{0},X{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(S{0},X{0}),0) >= 3,IFERROR(AVERAGE(S{0},X{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CC{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(T{0},U{0}),0) >= 0,IFERROR(AVERAGE(T{0},U{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(T{0},U{0}),0) >= 1,IFERROR(AVERAGE(T{0},U{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(T{0},U{0}),0) >= 2,IFERROR(AVERAGE(T{0},U{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(T{0},U{0}),0) >= 3,IFERROR(AVERAGE(T{0},U{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(T{0},U{0}),0) >= 3,IFERROR(AVERAGE(T{0},U{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CD{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 0,IFERROR(AVERAGE(V{0},W{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 1,IFERROR(AVERAGE(V{0},W{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 2,IFERROR(AVERAGE(V{0},W{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 3,IFERROR(AVERAGE(V{0},W{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(V{0},W{0}),0) >= 3,IFERROR(AVERAGE(V{0},W{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CE{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BD{0}:BF{0}),0) >= 0,IFERROR(AVERAGE(BD{0}:BF{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BD{0}:BF{0}),0) >= 1,IFERROR(AVERAGE(BD{0}:BF{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BD{0}:BF{0}),0) >= 2,IFERROR(AVERAGE(BD{0}:BF{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BD{0}:BF{0}),0) >= 3,IFERROR(AVERAGE(BD{0}:BF{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BD{0}:BF{0}),0) >= 3,IFERROR(AVERAGE(BD{0}:BF{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CF{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(Y{0},Z{0}),0) >= 0,IFERROR(AVERAGE(Y{0},Z{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(Y{0},Z{0}),0) >= 1,IFERROR(AVERAGE(Y{0},Z{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(Y{0},Z{0}),0) >= 2,IFERROR(AVERAGE(Y{0},Z{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(Y{0},Z{0}),0) >= 3,IFERROR(AVERAGE(Y{0},Z{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(Y{0},Z{0}),0) >= 3,IFERROR(AVERAGE(Y{0},Z{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CG{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AA{0}:AB{0}),0) >= 0,IFERROR(AVERAGE(AA{0}:AB{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AA{0}:AB{0}),0) >= 1,IFERROR(AVERAGE(AA{0}:AB{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AA{0}:AB{0}),0) >= 2,IFERROR(AVERAGE(AA{0}:AB{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AA{0}:AB{0}),0) >= 3,IFERROR(AVERAGE(AA{0}:AB{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AA{0}:AB{0}),0) >= 3,IFERROR(AVERAGE(AA{0}:AB{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CH{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AI{0}:AK{0}),0) >= 0,IFERROR(AVERAGE(AI{0}:AK{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AI{0}:AK{0}),0) >= 1,IFERROR(AVERAGE(AI{0}:AK{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AI{0}:AK{0}),0) >= 2,IFERROR(AVERAGE(AI{0}:AK{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AI{0}:AK{0}),0) >= 3,IFERROR(AVERAGE(AI{0}:AK{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AI{0}:AK{0}),0) >= 3,IFERROR(AVERAGE(AI{0}:AK{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CI{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AG{0}:AH{0}),0) >= 0,IFERROR(AVERAGE(AG{0}:AH{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AG{0}:AH{0}),0) >= 1,IFERROR(AVERAGE(AG{0}:AH{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AG{0}:AH{0}),0) >= 2,IFERROR(AVERAGE(AG{0}:AH{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AG{0}:AH{0}),0) >= 3,IFERROR(AVERAGE(AG{0}:AH{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AG{0}:AH{0}),0) >= 3,IFERROR(AVERAGE(AG{0}:AH{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CJ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AO{0}:AP{0}),0) >= 0,IFERROR(AVERAGE(AO{0}:AP{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AO{0}:AP{0}),0) >= 1,IFERROR(AVERAGE(AO{0}:AP{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AO{0}:AP{0}),0) >= 2,IFERROR(AVERAGE(AO{0}:AP{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AO{0}:AP{0}),0) >= 3,IFERROR(AVERAGE(AO{0}:AP{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AO{0}:AP{0}),0) >= 3,IFERROR(AVERAGE(AO{0}:AP{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    
                    Preguntas_S2.Cells[string.Format("CK{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 0,IFERROR(SUM(AC{0}:AD{0}),0) <1)),\"NULO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 1,IFERROR(SUM(AC{0}:AD{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 2,IFERROR(SUM(AC{0}:AD{0}),0) <4)),\"MEDIO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 4,IFERROR(SUM(AC{0}:AD{0}),0) <6)),\"ALTO\",IF((AND(IFERROR(SUM(AC{0}:AD{0}),0) >= 6,IFERROR(SUM(AC{0}:AD{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CL{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AE{0}),0) >= 0,IFERROR(AVERAGE(AE{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AE{0}),0) >= 1,IFERROR(AVERAGE(AE{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AE{0}),0) >= 2,IFERROR(AVERAGE(AE{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AE{0}),0) >= 3,IFERROR(AVERAGE(AE{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AE{0}),0) >= 3,IFERROR(AVERAGE(AE{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CM{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AF{0}),0) >= 0,IFERROR(AVERAGE(AF{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AF{0}),0) >= 1,IFERROR(AVERAGE(AF{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AF{0}),0) >= 2,IFERROR(AVERAGE(AF{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AF{0}),0) >= 3,IFERROR(AVERAGE(AF{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AF{0}),0) >= 3,IFERROR(AVERAGE(AF{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CN{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AL{0}:AN{0}),0) >= 0,IFERROR(AVERAGE(AL{0}:AN{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AL{0}:AN{0}),0) >= 1,IFERROR(AVERAGE(AL{0}:AN{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AL{0}:AN{0}),0) >= 2,IFERROR(AVERAGE(AL{0}:AN{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AL{0}:AN{0}),0) >= 3,IFERROR(AVERAGE(AL{0}:AN{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AL{0}:AN{0}),0) >= 3,IFERROR(AVERAGE(AL{0}:AN{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CO{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AQ{0}:AR{0}),0) >= 0,IFERROR(AVERAGE(AQ{0}:AR{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AQ{0}:AR{0}),0) >= 1,IFERROR(AVERAGE(AQ{0}:AR{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AQ{0}:AR{0}),0) >= 2,IFERROR(AVERAGE(AQ{0}:AR{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AQ{0}:AR{0}),0) >= 3,IFERROR(AVERAGE(AQ{0}:AR{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AQ{0}:AR{0}),0) >= 3,IFERROR(AVERAGE(AQ{0}:AR{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CP{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(AS{0}:AU{0}),0) >= 0,IFERROR(AVERAGE(AS{0}:AU{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(AS{0}:AU{0}),0) >= 1,IFERROR(AVERAGE(AS{0}:AU{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(AS{0}:AU{0}),0) >= 2,IFERROR(AVERAGE(AS{0}:AU{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(AS{0}:AU{0}),0) >= 3,IFERROR(AVERAGE(AS{0}:AU{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(AS{0}:AU{0}),0) >= 3,IFERROR(AVERAGE(AS{0}:AU{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CQ{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(AVERAGE(BG{0}:BI{0}),0) >= 0,IFERROR(AVERAGE(BG{0}:BI{0}),0) <1)),\"NULO\",IF((AND(IFERROR(AVERAGE(BG{0}:BI{0}),0) >= 1,IFERROR(AVERAGE(BG{0}:BI{0}),0) <2)),\"BAJO\",IF((AND(IFERROR(AVERAGE(BG{0}:BI{0}),0) >= 2,IFERROR(AVERAGE(BG{0}:BI{0}),0) <3)),\"MEDIO\",IF((AND(IFERROR(AVERAGE(BG{0}:BI{0}),0) >= 3,IFERROR(AVERAGE(BG{0}:BI{0}),0) <4)),\"ALTO\",IF((AND(IFERROR(AVERAGE(BG{0}:BI{0}),0) >= 3,IFERROR(AVERAGE(BG{0}:BI{0}),0) =4)),\"MUY ALTO\",\"N/A\")))))", rowStart);
                    Preguntas_S2.Cells[string.Format("CR{0}", rowStart)].Formula = string.Format("IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 0,IFERROR(SUM(AV{0}:BC{0}),0) <7)),\"NULO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 7,IFERROR(SUM(AV{0}:BC{0}),0) <10)),\"BAJO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 10,IFERROR(SUM(AV{0}:BC{0}),0) <13)),\"MEDIO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 13,IFERROR(SUM(AV{0}:BC{0}),0) <16)),\"ALTO\",IF((AND(IFERROR(SUM(AV{0}:BC{0}),0) >= 16,IFERROR(SUM(AV{0}:BC{0}),0) <=100)),\"MUY ALTO\",\"N/A\")))))", rowStart);

                    switch (item.id_pregunta)
                    {
                        case 1:

                            Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("P{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }



                            break;
                        case 2:
                            Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("Q{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 3:
                            Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("R{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 4:
                            Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("S{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 5:
                            Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("T{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 6:
                            Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("U{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 7:
                            Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("V{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 8:
                            Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("W{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 9:
                            Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("X{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 10:
                            Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("Y{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 11:
                            Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("Z{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 12:
                            Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 13:
                            Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 14:
                            Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 15:
                            Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 16:
                            Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 17:
                            Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 18:
                            Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 19:
                            Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 20:
                            Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 21:
                            Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AJ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 22:
                            Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AK{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 23:
                            Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AL{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 24:
                            Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AM{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 25:
                            Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AN{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 26:
                            Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AO{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 27:
                            Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AP{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 28:
                            Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AQ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 29:
                            Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AR{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 30:
                            Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AS{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 31:
                            Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AT{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 32:
                            Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AU{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 33:
                            Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AV{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 34:
                            Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AW{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 35:
                            Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AX{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 36:
                            Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AY{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 37:
                            Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("AZ{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 38:
                            Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BA{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 39:
                            Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BB{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 40:
                            Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BC{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 41:
                            Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BD{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 42:
                            Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BE{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 43:
                            Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BF{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 44:
                            Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BG{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 45:
                            Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BH{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            break;
                        case 46:
                            Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Value = item.Calificacion;
                            if (item.Calificacion == 0)
                            {
                                Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9BE5F7")));
                            }
                            else if (item.Calificacion == 1)
                            {
                                Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#6BF56E")));
                            }
                            else if (item.Calificacion == 2)
                            {
                                Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFFF00")));
                            }
                            else if (item.Calificacion == 3)
                            {
                                Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FFC000")));
                            }
                            else if (item.Calificacion == 4)
                            {
                                Preguntas_S2.Cells[string.Format("BI{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#FF0000")));
                            }
                            counter_flag = 1;
                            break;
                        default:
                            break;
                    }
                    if (counter_flag == 1)
                    {
                        rowStart++;
                        counter_flag = 0;
                    }

                    //return View()
                }

                Preguntas_S2.Cells["A:DZ"].AutoFitColumns();
                Preguntas_S2.Cells.AutoFitColumns();
                Preguntas_S2.Column(7).BestFit = true;
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "Reporte_Proyecto.xlsx");
                Response.BinaryWrite(pck_S2.GetAsByteArray());
                Response.End();
            }


        }


        public ActionResult Reporte_PDF(int id_CT, int id_C)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";

            string Nombre = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.Nombre).FirstOrDefault();
            string id_trabajador = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_trabajador).FirstOrDefault();
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
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/AOwYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw?id_CT=" + id_CT + "&id_C=" + id_C);
            return File(pdf.Save(), "application/pdf;", "Encuesta_" + id_trabajador + "_" + Nombre + ".pdf");
            
        }
        public ActionResult Reporte_PDF_Clima(int id_CT)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.Nombre).FirstOrDefault();
            int? id_empresa = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_empresa).FirstOrDefault();
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
            converter.Options.WebPageWidth = 1700;
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/AOwCLMAYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw?id_CT=" + id_CT);
            return File(pdf.Save(), "application/pdf;", "Encuesta_ClimaLaboral_" + Nombre + ".pdf");
        }
        public ActionResult Reporte_PDF_E360(int id_CT)
        {
            var url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/";
            string Nombre = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.Nombre).FirstOrDefault();
            int id_ = (from E in db.E360_Cuestionario_Resultado_N01 where E.id_cuestionario_trabajador == id_CT select E.id_cuestionario_resultado).FirstOrDefault();
            int? Evaluando = (from E in db.E360_Cuestionario_Resultado_N01 where E.id_cuestionario_trabajador == id_CT select E.id_CT_Evaluado).FirstOrDefault();
            int? id_empresa = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_cuestionario_trabajador == id_CT select E.id_empresa).FirstOrDefault();
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
            //converter.Options.MarginBottom = 20;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            SelectPdf.PdfDocument pdf = converter.ConvertUrl(url + "Reportes/AOwE360YbqttJ6foDFHDFHDFHf4AhwNvjDfuDFHDFHjMxmTo7UJYHTG45HBRF23SD4DGFFas?id_CT=" + id_CT);
            return File(pdf.Save(), "application/pdf;", "Encuesta_E360_" + Nombre +"_"+Evaluando+"_" + id_ + ".pdf");
        }

        [Authorize(Roles = "Admin-Guest,Admin_Centro")]
        public ActionResult Enviar_encuestas_Ind(string User_Name, int? id_CT)
        {

            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_empresa).FirstOrDefault();

            var datos_correos = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa && E.id_cuestionario_trabajador == id_CT select new { E.Nombre, E.Email, E.id_cuestionario_trabajador, E.id_encuesta }).ToArray();
            string usuario_guest = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 2 select E.User_Nombre).FirstOrDefault();
            string password_guest = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 2 select E.User_Password).FirstOrDefault();

            int flag = 1;
            foreach (var correo in datos_correos)
            {

                try
                {
                    var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                                where R.id_cuestionario_trabajador == correo.id_cuestionario_trabajador
                                select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

                    flag_editar = 0;
                    if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                        || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
                    {
                        flag_editar = 1;
                    }
                    string usuario = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Nombre).FirstOrDefault();
                    string password = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Password).FirstOrDefault();

                    if ((usuario == "" || usuario == null) && (password == "" || password == null))
                    {
                        usuario = usuario_guest;
                        password = password_guest;
                    }
                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com")); 
                    mail.To.Add(new MailboxAddress("Evaluación", correo.Email));
                    mail.Subject = "Mi evaluación";
                    BodyBuilder cuerpo_correo = new BodyBuilder(); 

                    if (flag_editar == 1)
                    {
                        cuerpo_correo.HtmlBody = "<head>"+
                        "<meta http-equiv='X-UA-Compatible' content='IE=edge'>"+
                        "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                        "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
                        "<title>Check 035</title>"+
                        "</head>" +
                        "&nbsp;" +
                        "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td class='two-left' align='center' valign='top' width='170'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='260'>" +
                        "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                        "</tr></tbody></table></td>" +
                        "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
                        "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
                        "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
                        "</tr> " +
                        "<tr><td valign='top' align='center'>" +
                        "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                                                                <tbody>" +
                        "                                                                    <tr>" +
                        "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
                        "                                                                            <multiline><a href='https://identifica.check035.com/Encuestas/Edit/" + correo.id_cuestionario_trabajador + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
                        "                                                                        </td>" +
                        "                                                                    </tr>" +
                        "                                                                </tbody>" +
                        "                                                            </table>" +
                        "                                                        </td>" +
                        "                                                        </tr> " +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>"+usuario+"</strong><br>Contraseña: <strong>"+password+"</strong></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                        "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' height='40'></td>" +
                        "</tr><tr><td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
                        "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
                        "  <ol>" +
                        "  <li>Chrome</li>" +
                        "  <li>Firefox</li>" +
                        "  <li>Opera</li>" +
                        "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
                        "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";

           
                    }
                    else
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
       "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
       "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
       "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
       "<title>Check 035</title>" +
       "</head>" +
       "&nbsp;" +
       "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
       "<tbody>" +
       "<tr>" +
       "<td class='two-left' align='center' valign='top' width='170'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "</td>" +
       "<td class='two-left' align='center' valign='top' width='260'>" +
       "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
       "</tr></tbody></table></td>" +
       "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
       "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
       "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
       "</tr> " +
       "<tr><td valign='top' align='center'>" +
       "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                                                                <tbody>" +
       "                                                                    <tr>" +
       "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
       "                                                                            <multiline><a href='https://identifica.check035.com/Trabajador_Resultados/Encuesta?id_CT=" + correo.id_cuestionario_trabajador + "&id_C=" + correo.id_encuesta + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
       "                                                                        </td>" +
       "                                                                    </tr>" +
       "                                                                </tbody>" +
       "                                                            </table>" +
       "                                                        </td>" +
       "                                                        </tr> " +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>"+usuario+"</strong><br>Contraseña: <strong>"+password+"</strong></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
       "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' height='40'></td>" +
       "</tr><tr><td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
       "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
       "  <ol>" +
       "  <li>Chrome</li>" +
       "  <li>Firefox</li>" +
       "  <li>Opera</li>" +
       "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
       "  </ol>" +
       "</td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='left' valign='top'>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
       "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";

                    }

                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
                    SmtpServer.Send(mail); 

                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 1);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 1");
                    db.SaveChanges();
                }
                catch (Exception exception)
                {
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 0);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 0");
                    db.SaveChanges();

                }
            }

            return RedirectToAction("Encuestas_Admin", new { User_Name = User_Name, flag });
        }

        [Authorize(Roles = "Admin,Admin_SyS")]
        public ActionResult Enviar_encuestas_Ind_Admin(int id_empresa, int? id_CT)
        {
            string UserName = User.Identity.Name;
            // int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == User_Name select E.id_empresa).FirstOrDefault();
            string usuario_guest = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 2 select E.User_Nombre).FirstOrDefault();
            string password_guest = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 2 select E.User_Password).FirstOrDefault();

            var datos_correos = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa && E.id_cuestionario_trabajador == id_CT select new { E.Nombre, E.Email, E.id_cuestionario_trabajador, E.id_encuesta }).ToArray();
            ////////////////
            string[] datos_correos_2 = { "irving.cast94@gmail.com", "castsoft@outlook.com" };
            /////////////
            int flag = 1;

            foreach (var correo in datos_correos)
            {
                try
                {
                    var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                                where R.id_cuestionario_trabajador == correo.id_cuestionario_trabajador
                                select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

                    flag_editar = 0;
                    if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                        || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
                    {
                        flag_editar = 1;
                    }
                    string usuario = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Nombre).FirstOrDefault();
                    string password = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Password).FirstOrDefault();

                    if ((usuario == "" || usuario == null) && (password == "" || password == null))
                    {
                        usuario = usuario_guest;
                        password = password_guest;
                    }
                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com"));
                    mail.To.Add(new MailboxAddress("Evaluación", correo.Email));
                    mail.Subject = "Check List 035";
                    BodyBuilder cuerpo_correo = new BodyBuilder();

                    if (flag_editar == 1)
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
                        "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                        "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                        "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
                        "<title>Check 035</title>" +
                        "</head>" +
                        "&nbsp;" +
                        "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td class='two-left' align='center' valign='top' width='170'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='260'>" +
                        "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                        "</tr></tbody></table></td>" +
                        "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
                        "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
                        "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
                        "</tr> " +
                        "<tr><td valign='top' align='center'>" +
                        "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                                                                <tbody>" +
                        "                                                                    <tr>" +
                        "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
                        "                                                                            <multiline><a href='https://identifica.check035.com/Encuestas/Edit/" + correo.id_cuestionario_trabajador + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
                        "                                                                        </td>" +
                        "                                                                    </tr>" +
                        "                                                                </tbody>" +
                        "                                                            </table>" +
                        "                                                        </td>" +
                        "                                                        </tr> " +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>" + usuario + "</strong><br>Contraseña: <strong>" + password + "</strong></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                        "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' height='40'></td>" +
                        "</tr><tr><td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
                        "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
                        "  <ol>" +
                        "  <li>Chrome</li>" +
                        "  <li>Firefox</li>" +
                        "  <li>Opera</li>" +
                        "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
                        "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";


                    }
                    else
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
       "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
       "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
       "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
       "<title>Check 035</title>" +
       "</head>" +
       "&nbsp;" +
       "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
       "<tbody>" +
       "<tr>" +
       "<td class='two-left' align='center' valign='top' width='170'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "</td>" +
       "<td class='two-left' align='center' valign='top' width='260'>" +
       "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
       "</tr></tbody></table></td>" +
       "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
       "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
       "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
       "</tr> " +
       "<tr><td valign='top' align='center'>" +
       "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                                                                <tbody>" +
       "                                                                    <tr>" +
       "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
       "                                                                            <multiline><a href='https://identifica.check035.com/Trabajador_Resultados/Encuesta?id_CT=" + correo.id_cuestionario_trabajador + "&id_C=" + correo.id_encuesta + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
       "                                                                        </td>" +
       "                                                                    </tr>" +
       "                                                                </tbody>" +
       "                                                            </table>" +
       "                                                        </td>" +
       "                                                        </tr> " +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>" + usuario + "</strong><br>Contraseña: <strong>" + password + "</strong></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
       "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' height='40'></td>" +
       "</tr><tr><td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
       "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
       "  <ol>" +
       "  <li>Chrome</li>" +
       "  <li>Firefox</li>" +
       "  <li>Opera</li>" +
       "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
       "  </ol>" +
       "</td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='left' valign='top'>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
       "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";

                    }

                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
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
                    return RedirectToAction("Index", new { flag });
                }
            }

            return RedirectToAction("Index", new { flag });
        }

        [Authorize(Roles = "Admin-Guest")]
        public ActionResult Enviar_encuestas(string User_Name)
        {


            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == User_Name select E.id_empresa).FirstOrDefault();

            var datos_correos = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa && E.Email != null && (E.Survey_Status != 100 || E.Survey_Status != 200 || E.Survey_Status == null) select new { E.Nombre, E.Email, E.id_cuestionario_trabajador, E.id_encuesta }).ToArray();

            string usuario_guest = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 2 select E.User_Nombre).FirstOrDefault();
            string password_guest = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 2 select E.User_Password).FirstOrDefault();

            int flag = 1;
            foreach (var correo in datos_correos)
            {
                try
                {
                    var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                                where R.id_cuestionario_trabajador == correo.id_cuestionario_trabajador
                                select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

                    flag_editar = 0;
                    if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                        || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
                    {
                        flag_editar = 1;
                    }
                    string usuario = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Nombre).FirstOrDefault();
                    string password = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Password).FirstOrDefault();

                    if ((usuario == "" || usuario == null) && (password == "" || password == null)) { 
                        usuario = usuario_guest;
                        password = password_guest;
                    }
  

                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com"));
                    mail.To.Add(new MailboxAddress("Evaluación", correo.Email));
                    mail.Subject = "Check List 035";
                    BodyBuilder cuerpo_correo = new BodyBuilder();

                    if (flag_editar == 1)
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
                        "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                        "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                        "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
                        "<title>Check 035</title>" +
                        "</head>" +
                        "&nbsp;" +
                        "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td class='two-left' align='center' valign='top' width='170'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='260'>" +
                        "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                        "</tr></tbody></table></td>" +
                        "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
                        "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
                        "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
                        "</tr> " +
                        "<tr><td valign='top' align='center'>" +
                        "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                                                                <tbody>" +
                        "                                                                    <tr>" +
                        "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
                        "                                                                            <multiline><a href='https://identifica.check035.com/Encuestas/Edit/" + correo.id_cuestionario_trabajador + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
                        "                                                                        </td>" +
                        "                                                                    </tr>" +
                        "                                                                </tbody>" +
                        "                                                            </table>" +
                        "                                                        </td>" +
                        "                                                        </tr> " +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>" + usuario + "</strong><br>Contraseña: <strong>" + password + "</strong></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                        "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' height='40'></td>" +
                        "</tr><tr><td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
                        "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
                        "  <ol>" +
                        "  <li>Chrome</li>" +
                        "  <li>Firefox</li>" +
                        "  <li>Opera</li>" +
                        "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
                        "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";


                    }
                    else
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
       "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
       "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
       "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
       "<title>Check 035</title>" +
       "</head>" +
       "&nbsp;" +
       "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
       "<tbody>" +
       "<tr>" +
       "<td class='two-left' align='center' valign='top' width='170'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "</td>" +
       "<td class='two-left' align='center' valign='top' width='260'>" +
       "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
       "</tr></tbody></table></td>" +
       "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
       "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
       "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
       "</tr> " +
       "<tr><td valign='top' align='center'>" +
       "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                                                                <tbody>" +
       "                                                                    <tr>" +
       "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
       "                                                                            <multiline><a href='https://identifica.check035.com/Trabajador_Resultados/Encuesta?id_CT=" + correo.id_cuestionario_trabajador +  "&id_C=" + correo.id_encuesta + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
       "                                                                        </td>" +
       "                                                                    </tr>" +
       "                                                                </tbody>" +
       "                                                            </table>" +
       "                                                        </td>" +
       "                                                        </tr> " +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>" + usuario + "</strong><br>Contraseña: <strong>" + password + "</strong></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
       "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' height='40'></td>" +
       "</tr><tr><td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
       "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
       "  <ol>" +
       "  <li>Chrome</li>" +
       "  <li>Firefox</li>" +
       "  <li>Opera</li>" +
       "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
       "  </ol>" +
       "</td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='left' valign='top'>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
       "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";

                    }



                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
                    SmtpServer.Send(mail);

                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 1);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 1");
                    db.SaveChanges();
                }
                catch (Exception exception)
                {
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 0);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 0");
                    db.SaveChanges();

                }
            }

            return RedirectToAction("Encuestas_Admin", new { User_Name = User_Name, flag });
        }

        [Authorize(Roles = "Admin_Centro")]
        public ActionResult Enviar_encuestas_Centro(string User_Name)
        {  
            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == User_Name select E.id_empresa).FirstOrDefault();
            int? id_centro = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == User_Name select E.id_centro_trabajo).FirstOrDefault();

            var datos_correos = (from E in db.ERGOS_Cuestionarios_Trabajador_N01 where E.id_empresa == id_empresa && E.id_centro_trabajo == id_centro && E.Email != null && (E.Survey_Status != 100 || E.Survey_Status != 200 || E.Survey_Status == null) select new { E.Nombre, E.Email, E.id_cuestionario_trabajador, E.id_encuesta }).ToArray();

        
            int flag = 1;
            foreach (var correo in datos_correos)
            {
                try
                {
                    var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                                where R.id_cuestionario_trabajador == correo.id_cuestionario_trabajador
                                select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

                    flag_editar = 0;
                    if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                        || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
                    {
                        flag_editar = 1;
                    }
                    string usuario = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Nombre).FirstOrDefault();
                    string password = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == correo.id_cuestionario_trabajador select E.User_Password).FirstOrDefault();


                   
                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Identifica NOM-035", "check035@check035.com"));
                    mail.To.Add(new MailboxAddress("Evaluación", correo.Email));
                    mail.Subject = "Check List 035";
                    BodyBuilder cuerpo_correo = new BodyBuilder();

                    if (flag_editar == 1)
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
                        "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                        "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                        "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
                        "<title>Check 035</title>" +
                        "</head>" +
                        "&nbsp;" +
                        "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td class='two-left' align='center' valign='top' width='170'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</td>" +
                        "<td class='two-left' align='center' valign='top' width='260'>" +
                        "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
                        "</tr></tbody></table></td>" +
                        "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
                        "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
                        "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
                        "</tr> " +
                        "<tr><td valign='top' align='center'>" +
                        "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
                        "                                                                <tbody>" +
                        "                                                                    <tr>" +
                        "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
                        "                                                                            <multiline><a href='https://identifica.check035.com/Encuestas/Edit/" + correo.id_cuestionario_trabajador + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
                        "                                                                        </td>" +
                        "                                                                    </tr>" +
                        "                                                                </tbody>" +
                        "                                                            </table>" +
                        "                                                        </td>" +
                        "                                                        </tr> " +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
                        "                                                        </tr>" +
                        "                                                        <tr>" +
                        "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
                        "                                                        </tr>" +
                        "<tr>" +
                        "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>" + usuario + "</strong><br>Contraseña: <strong>" + password + "</strong></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
                        "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                        "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td align='center' valign='top' height='40'></td>" +
                        "</tr><tr><td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
                        "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
                        "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
                        "  <ol>" +
                        "  <li>Chrome</li>" +
                        "  <li>Firefox</li>" +
                        "  <li>Opera</li>" +
                        "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
                        "  </ol>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' valign='top'></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'>" +
                        "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
                        "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
                        "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
                        "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";


                    }
                    else
                    {
                        cuerpo_correo.HtmlBody = "<head>" +
       "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
       "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
       "<meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>" +
       "<title>Check 035</title>" +
       "</head>" +
       "&nbsp;" +
       "<table style='background: #FFFFFF;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#3f3f3f'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='border-bottom: #f8f8f8 solid 1px;' align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0'>" +
       "<tbody>" +
       "<tr>" +
       "<td class='two-left' align='center' valign='top' width='170'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 2px; line-height: 2px;' height='2'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 16px; color: #34435a; font-weight: normal;' align='center' valign='top'></td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-size: 28px; line-height: 28px;' align='center' valign='top' height='28'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "</td>" +
       "<td class='two-left' align='center' valign='top' width='260'>" +
       "<table border='0' width='125' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='middle'><a href='https://check035.com/'><img src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='' width='130' height='60' /></a></td>" +
       "</tr></tbody></table></td>" +
       "<td class='two-left' align='center' valign='top' width='170'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody</table></td></tr></tbody></table><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top' bgcolor='#FFFFFF'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-size: 45px; line-height: 45px;' align='center' valign='top' height='45'></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>" +
       "Hoy arrancamos un proceso muy importante en el cual tu participación es fundamental ya que nos permitirá identificar los factores de riesgo psicosocial en el trabajo.</td>" +
       "</tr>" +
       "<tr>" +
       "<td style='font-family:Poppins, sans-serif, Verdana; font-size:22px; color:#ff5757; font-weight:bold; line-height:32px; padding-top:25px; padding-bottom:25px;' mc:edit='wm-75' valign='top' align='center'><multiline>" +
       "Te invitamos a realizar la encuesta diagnóstico<br>en el siguiente enlace:</multiline></td>" +
       "</tr> " +
       "<tr><td valign='top' align='center'>" +
       "                                                            <table width='300' cellspacing='0' cellpadding='0' border='0' align='center'>" +
       "                                                                <tbody>" +
       "                                                                    <tr>" +
       "                          <td style='background:#7d45ee; -moz-border-radius: 25px; border-radius: 25px; font-family:Poppins, Verdana, Arial; font-size:14px; font-weight:bold; text-transform:uppercase; color:#FFF; ' mc:edit='wm-77' valign='middle' height='45' align='center'>" +
       "                                                                            <multiline><a href='https://identifica.check035.com/Trabajador_Resultados/Encuesta?id_CT=" + correo.id_cuestionario_trabajador + "&id_C=" + correo.id_encuesta + "' style='text-decoration:none; color:#FFF;'>Realizar encuesta ahora</a></multiline>" +
       "                                                                        </td>" +
       "                                                                    </tr>" +
       "                                                                </tbody>" +
       "                                                            </table>" +
       "                                                        </td>" +
       "                                                        </tr> " +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Utiliza los siguientes datos de acceso para iniciar sesión:</td>" +
       "                                                        </tr>" +
       "                                                        <tr>" +
       "                                                        <td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td>" +
       "                                                        </tr>" +
       "<tr>" +
       "<td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #34435a; font-weight: normal;' align='center' valign='top'>Usuario: <strong>" + usuario + "</strong><br>Contraseña: <strong>" + password + "</strong></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 25px;' align='center' valign='top' height='25'></td></tr><tr>" +
       "<td style='font-size: 55px; line-height: 15px;' align='center' valign='top' height='15'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody>" +
       "<tr><td align='center' valign='top'><table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='middle' bgcolor='#ffffff'><!--Section-->" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' bgcolor='#ff5757'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td align='center' valign='top' height='40'></td>" +
       "</tr><tr><td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody><tr><td style='font-size: 20px; line-height: 20px;' align='center' valign='top' height='20'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 22px; color: #fff; font-weight: bold; line-height: 32px;' align='left' valign='top'>Recomendamos el uso de los siguientes navegadores:</td>" +
       "</tr><tr><td style='font-size: 10px; line-height: 10px;' align='center' valign='top' height='10'></td>" +
       "</tr><tr><td style='font-family: Poppins, sans-serif, Verdana; font-size: 18px; color: #fff; font-weight: normal; line-height: 24px;' align='left' valign='top'>" +
       "  <ol>" +
       "  <li>Chrome</li>" +
       "  <li>Firefox</li>" +
       "  <li>Opera</li>" +
       "  <li>Edge (puede presentar conflictos con funciones de la aplicaci&oacute;n)</li>" +
       "  </ol>" +
       "</td>" +
       "</tr>" +
       "<tr>" +
       "<td align='center' valign='top'></td>" +
       "</tr>" +
       "</tbody>" +
       "</table></td></tr><tr><td class='two-left' style='font-size: 30px; line-height: 30px;' align='center' valign='top' height='30'></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'>" +
       "<table class='main' border='0' width='800' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='background: #fff;' align='left' valign='top' bgcolor='#292828'>" +
       "<table class='two-left-inner' border='0' width='600' cellspacing='0' cellpadding='0' align='center'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr>" +
       "<tr>" +
       "<td align='left' valign='top'>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='right'>" +
       "<tbody>" +
       "<tr>" +
       "<td align='center' valign='top'><a href='https://check035.com'><img class='alignnone' style='display: block;' src='https://check035.com/wp-content/uploads/2021/11/logo-web.png' alt='Logo Grow Incubadora de Mentes Creativas' width='130' height='60' /></a></td>" +
       "</tr>" +
       "</tbody>" +
       "</table>" +
       "<table class='two-left-inner' border='0' cellspacing='0' cellpadding='0' align='left'>" +
       "<tbody>" +
       "<tr>" +
       "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #000; font-weight: normal; padding-top: 20px;' align='center' valign='top'>Check 035</td>" +
       "</tr></tbody></table></td></tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr><tr><td style='line-height: 20px;' align='left' valign='top' height='20'><img style='display: block;' src='https://vivegrow.com/00_JP_Mailing/SelfMgmtBootCamp/images/space.png' alt='' width='4' height='4' /></td>" +
       "</tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>&nbsp;&nbsp;</td></tr></tbody></table>";

                    }

                    mail.Body = cuerpo_correo.ToMessageBody();
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.CheckCertificateRevocation = true;
                    SmtpServer.Connect("smtpout.secureserver.net", 465, true);
                    SmtpServer.Authenticate("check035@check035.com", "ZaZc3JnfTSH%*4c");
                    SmtpServer.Send(mail);

                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 1);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 1");
                    db.SaveChanges();
                     
                }
                catch (Exception exception)
                {
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_mail_status @id_CT =" + correo.id_cuestionario_trabajador + ",@status = " + 0);
                    db.Database.ExecuteSqlCommand("EXECUTE log_data_send_mails @user ='" + UserName + "',@mail_send_to = '" + correo.Email + "',@Status = 0");
                    db.SaveChanges();

                }
            }

            return RedirectToAction("Encuestas_Admin", new { User_Name = User_Name, flag });
        }
        public ActionResult Mailing_Surveys()
        {





            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult Glabal_Results()
        {
            return View();
        }


        public ActionResult Search_Empresas(int ERGOS_Empresas_N01List)
        {
            var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.id_empresa == ERGOS_Empresas_N01List);
            return View(eRGOS_Cuestionarios_Trabajador_N01.ToList());
        }

        [Authorize(Roles = "Admin-Guest")]
        public ActionResult Resultados_Admin(int id, string User_Name)
        {
            try
            {
                ViewBag.Categorias = (from total in db.fnDemo_N035_Categorias_Pilot(id)
                                      select new Respuestas
                                      {
                                          Canalizado = total.Canalizado,
                                          Dominio_1 = total.Dom_1,
                                          Dominio_2 = total.Dom_2,
                                          Dominio_3 = total.Dom_3,
                                          Dominio_4 = total.Dom_4,
                                          Dominio_5 = total.Dom_5,
                                          Dominio_6 = total.Dom_6,
                                          Dominio_7 = total.Dom_7,
                                          Dominio_8 = total.Dom_8,
                                          Dominio_9 = total.Dom_9,
                                          Dominio_10 = total.Dom_10,
                                          Categoria_1 = total.Cat_1,
                                          Categoria_2 = total.Cat_2,
                                          Categoria_3 = total.Cat_3,
                                          Categoria_4 = total.Cat_4,
                                          Categoria_5 = total.CAT_5,
                                          id_cuestionario = total.id_encuesta,
                                          Final = total.FINAL
                                      }).FirstOrDefault();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize(Roles = "Admin,Admin_Sys")]
        public ActionResult Resultados(int id)
        {
            try
            {
                ViewBag.Categorias = (from total in db.fnDemo_N035_Categorias_Pilot(id)
                                      select new Respuestas
                                      {
                                          Canalizado = total.Canalizado,
                                          Dominio_1 = total.Dom_1,
                                          Dominio_2 = total.Dom_2,
                                          Dominio_3 = total.Dom_3,
                                          Dominio_4 = total.Dom_4,
                                          Dominio_5 = total.Dom_5,
                                          Dominio_6 = total.Dom_6,
                                          Dominio_7 = total.Dom_7,
                                          Dominio_8 = total.Dom_8,
                                          Dominio_9 = total.Dom_9,
                                          Dominio_10 = total.Dom_10,
                                          Categoria_1 = total.Cat_1,
                                          Categoria_2 = total.Cat_2,
                                          Categoria_3 = total.Cat_3,
                                          Categoria_4 = total.Cat_4,
                                          Categoria_5 = total.CAT_5,
                                          id_cuestionario = total.id_encuesta,
                                          Final = total.FINAL
                                      }).FirstOrDefault();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
                //return View();
            }

        }


        // GET: Encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            if (eRGOS_Cuestionarios_Trabajador_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }
        
        public JsonResult Get_E360_Compañeros(int id_empresa)
        {

            object dato = null;

                try
                { 

                    var coworkers_gotten = (from Centros in db.ERGOS_Cuestionarios_Trabajador_N01 
                                            where Centros.id_centro_trabajo == id_empresa 
                                            select new { Centros.id_cuestionario_trabajador, Centros.id_trabajador });

                    dato = coworkers_gotten.ToArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            return Json(dato, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Centros_Trabajo(int id_empresa)
        {

            object dato = null;

            if (User.IsInRole("Admin-Guest") || User.IsInRole("Admin_SyS") || User.IsInRole("Admin"))
            {
                try
                {

                    var productos_gotten = (from Centros in db.ERGOS_Centros_Trabajo_N01
                                            join Empresas in db.ERGOS_Empresas_N01 on Centros.id_empresa equals Empresas.id_empresa
                                            where Empresas.id_empresa == id_empresa
                                            select new { Centros.Nombre_centro_trabajo, Centros.id_centro_trabajo });

                    dato = productos_gotten.ToArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else if (User.IsInRole("Admin_Centro"))
            {
                try
                {


                    string UserName = User.Identity.Name;
                    int? id_centro_trabajo = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_centro_trabajo).FirstOrDefault();


                    var productos_gotten = (from Centros in db.ERGOS_Centros_Trabajo_N01
                                            join Empresas in db.ERGOS_Empresas_N01 on Centros.id_empresa equals Empresas.id_empresa
                                            where Empresas.id_empresa == id_empresa && Centros.id_centro_trabajo == id_centro_trabajo
                                            select new { Centros.Nombre_centro_trabajo, Centros.id_centro_trabajo });

                    dato = productos_gotten.ToArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            return Json(dato, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin,Admin_SyS,Admin-Guest,Admin_Centro")]
        // GET: Encuestas/Create
        public ActionResult Create(int? id_survey)
        {
            switch (id_survey)
            {
                case 4:
                    ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null).Where(A => A.id_encuesta == id_survey), "id_empresa", "Razon_Social");
                    break;
                case 5:
                    ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null).Where(A => A.id_encuesta == id_survey), "id_empresa", "Razon_Social");
                    break;
                default:
                    ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null).Where(A => A.id_encuesta == 1 || A.id_encuesta == 2 || A.id_encuesta == 3), "id_empresa", "Razon_Social");
                    break;
            }
            //ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo", 0);
            ViewBag.Sexo = new SelectList(db.DATA_Sexo_N01, "id_Sexo", "Sexo");
            ViewBag.Edad = new SelectList(db.DATA_Edades_N01, "id_edad", "edad");
            ViewBag.Estado_Civil = new SelectList(db.DATA_Estado_Civil_N01, "id_Estado_Civil", "Estado_Civil");
            ViewBag.Nivel_Estudios = new SelectList(db.DATA_Nivel_Estudios_N01, "id_Nivel_Estudios", "Nivel_Estudios");
            ViewBag.Tipo_puesto = new SelectList(db.DATA_Tipo_puesto_N01, "id_Tipo_puesto", "Tipo_puesto");
            ViewBag.Tipo_Contratacion = new SelectList(db.DATA_Tipo_Contratacion_N01, "id_Tipo_Contratacion", "Tipo_Contratacion");
            ViewBag.Tipo_Jornada = new SelectList(db.DATA_Tipo_Jornada_N01, "id_Tipo_Jornada", "Tipo_Jornada");
            ViewBag.Rotacion_Turno = new SelectList(db.DATA_Rotacion_Turno_N01, "id_Rotacion_Turno", "Rotacion_Turno");
            ViewBag.Experiencia_puesto_actual = new SelectList(db.DATA_Experiencia_puesto_N01, "id_Experiencia_puesto", "Experiencia_puesto");
            ViewBag.Experiencia_puesto_laboral = new SelectList(db.DATA_Experiencia_puesto_N01, "id_Experiencia_puesto", "Experiencia_puesto");
            ViewBag.Tipo_Personal = new SelectList(db.DATA_Tipo_Personal_N01, "id_Tipo_Personal", "Tipo_Personal");
            ViewBag.E360_EBCW = new SelectList(db.DATA_Tipo_Personal_N01, "id_Tipo_Personal", "Tipo_Personal");


            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_empresa).FirstOrDefault();
            int? id_centro_trabajo = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_centro_trabajo).FirstOrDefault();
            ViewBag.id_empresa_Visitante = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null).Where(A => A.id_empresa == id_empresa), "id_empresa", "Razon_Social");
            ViewBag.id_centro_trabajo_Visitante = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(E => E.deleted_at == null).Where(F => F.id_centro_trabajo == id_centro_trabajo), "id_empresa", "Razon_Social");

            return View();
        }

        // POST: Encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01)
        {
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social"); 
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(E => E.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo");

            ViewBag.Sexo = new SelectList(db.DATA_Sexo_N01, "id_Sexo", "Sexo");
            ViewBag.Edad = new SelectList(db.DATA_Edades_N01, "id_edad", "edad");
            ViewBag.Estado_Civil = new SelectList(db.DATA_Estado_Civil_N01, "id_Estado_Civil", "Estado_Civil");
            ViewBag.Nivel_Estudios = new SelectList(db.DATA_Nivel_Estudios_N01, "id_Nivel_Estudios", "Nivel_Estudios");
            ViewBag.Tipo_puesto = new SelectList(db.DATA_Tipo_puesto_N01, "id_Tipo_puesto", "Tipo_puesto");
            ViewBag.Tipo_Contratacion = new SelectList(db.DATA_Tipo_Contratacion_N01, "id_Tipo_Contratacion", "Tipo_Contratacion");
            ViewBag.Tipo_Jornada = new SelectList(db.DATA_Tipo_Jornada_N01, "id_Tipo_Jornada", "Tipo_Jornada");
            ViewBag.Rotacion_Turno = new SelectList(db.DATA_Rotacion_Turno_N01, "id_Rotacion_Turno", "Rotacion_Turno");
            ViewBag.Experiencia_puesto_actual = new SelectList(db.DATA_Experiencia_puesto_N01, "id_Experiencia_puesto", "Experiencia_puesto");
            ViewBag.Experiencia_puesto_laboral = new SelectList(db.DATA_Experiencia_puesto_N01, "id_Experiencia_puesto", "Experiencia_puesto");
            ViewBag.Tipo_Personal = new SelectList(db.DATA_Tipo_Personal_N01, "id_Tipo_Personal", "Tipo_Personal");


            int? id_empresa = eRGOS_Cuestionarios_Trabajador_N01.id_empresa;

            // SE QUITO NEW Y {} PARA DEJAR DE SER TIPO ANONIMO Y ASI PODER ASIGNARLO

            int? id_encuesta = (from total in db.ERGOS_Empresas_N01
                                where total.id_empresa == id_empresa
                                select total.id_encuesta).FirstOrDefault();

            int? resultado_busqueda = (from verificacion in db.ERGOS_Cuestionarios_Trabajador_N01
                                       where verificacion.id_centro_trabajo == eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo &&
                                       verificacion.id_encuesta == id_encuesta &&
                                       verificacion.id_trabajador == eRGOS_Cuestionarios_Trabajador_N01.id_trabajador
                                       select verificacion.id_encuesta).Count();

            if (ModelState.IsValid && resultado_busqueda == 0)
            {

                eRGOS_Cuestionarios_Trabajador_N01.id_encuesta = id_encuesta;
                db.ERGOS_Cuestionarios_Trabajador_N01.Add(eRGOS_Cuestionarios_Trabajador_N01);
                db.SaveChanges();

                int? id_CT = (from Cuestionario in db.ERGOS_Cuestionarios_Trabajador_N01
                              where Cuestionario.id_empresa == id_empresa && Cuestionario.id_encuesta == id_encuesta && Cuestionario.id_trabajador == eRGOS_Cuestionarios_Trabajador_N01.id_trabajador
                              && Cuestionario.fecha == eRGOS_Cuestionarios_Trabajador_N01.fecha
                              select Cuestionario.id_cuestionario_trabajador).FirstOrDefault();



                if (User.IsInRole("Admin") || User.IsInRole("Admin_SyS"))
                {
                    if (eRGOS_Cuestionarios_Trabajador_N01.id_encuesta == 4)
                        return RedirectToAction("Index", "MultiEvaluaClimaLaboral");
                    else if (eRGOS_Cuestionarios_Trabajador_N01.id_encuesta == 5)
                        return RedirectToAction("Index", "MultiEvaluaE360");
                    else
                        return RedirectToAction("Index");
                }
                else if (User.IsInRole("Admin-Guest"))
                {
                    return RedirectToAction("Encuestas_Admin", "Encuestas");

                }
                else if (User.IsInRole("Guest"))
                {
                    return RedirectToAction("Encuesta", "Trabajador_Resultados", new { id_CT = id_CT, id_C = id_encuesta });


                }
                else if (User.IsInRole("Admin_Centro"))
                {

                    return RedirectToAction("Encuestas_Centro", "Encuestas");
                }
            }

            if (resultado_busqueda != 0)
            {
                TempData["Folio_Existente"] = "El numero de Folio ya está dado de alta para la empresa Seleccionada";
            }
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: Encuestas/Edit/5
        public ActionResult Edit(int? id, string User_Name)
        {

            string UserName_Current = User.Identity.Name;
            if (User.IsInRole("Final_Guest"))
            {
                int UserId = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName_Current select E.id_user).FirstOrDefault();
                ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(UserId);
                if (eRGOS_Usuarios_N01.id_cuestionario_trabajador != id)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            if (eRGOS_Cuestionarios_Trabajador_N01 == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
            ViewBag.Sexo = new SelectList(db.DATA_Sexo_N01, "id_Sexo", "Sexo", eRGOS_Cuestionarios_Trabajador_N01.Sexo);
            ViewBag.Edad = new SelectList(db.DATA_Edades_N01, "id_edad", "edad", eRGOS_Cuestionarios_Trabajador_N01.Edad);
            ViewBag.Estado_Civil = new SelectList(db.DATA_Estado_Civil_N01, "id_Estado_Civil", "Estado_Civil", eRGOS_Cuestionarios_Trabajador_N01.Estado_Civil);
            ViewBag.Nivel_Estudios = new SelectList(db.DATA_Nivel_Estudios_N01, "id_Nivel_Estudios", "Nivel_Estudios", eRGOS_Cuestionarios_Trabajador_N01.Nivel_Estudios);
            ViewBag.Tipo_puesto = new SelectList(db.DATA_Tipo_puesto_N01, "id_Tipo_puesto", "Tipo_puesto", eRGOS_Cuestionarios_Trabajador_N01.Tipo_puesto);
            ViewBag.Tipo_Contratacion = new SelectList(db.DATA_Tipo_Contratacion_N01, "id_Tipo_Contratacion", "Tipo_Contratacion", eRGOS_Cuestionarios_Trabajador_N01.Tipo_Contratacion);
            ViewBag.Tipo_Jornada = new SelectList(db.DATA_Tipo_Jornada_N01, "id_Tipo_Jornada", "Tipo_Jornada", eRGOS_Cuestionarios_Trabajador_N01.Tipo_Jornada);
            ViewBag.Rotacion_Turno = new SelectList(db.DATA_Rotacion_Turno_N01, "id_Rotacion_Turno", "Rotacion_Turno", eRGOS_Cuestionarios_Trabajador_N01.Rotacion_Turno);
            ViewBag.Experiencia_puesto_actual = new SelectList(db.DATA_Experiencia_puesto_N01, "id_Experiencia_puesto", "Experiencia_puesto", eRGOS_Cuestionarios_Trabajador_N01.Experiencia_puesto_actual);
            ViewBag.Experiencia_puesto_laboral = new SelectList(db.DATA_Experiencia_puesto_N01, "id_Experiencia_puesto", "Experiencia_puesto", eRGOS_Cuestionarios_Trabajador_N01.Experiencia_puesto_laboral);
            ViewBag.Tipo_Personal = new SelectList(db.DATA_Tipo_Personal_N01, "id_Tipo_Personal", "Tipo_Personal", eRGOS_Cuestionarios_Trabajador_N01.Tipo_Personal);
            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.ERGOS_Cuestionarios_N01.id_cuestionario);
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName_Current select E.id_empresa).FirstOrDefault();
         
            //return View();
            //int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == User_Name select E.id_empresa).FirstOrDefault();
            if (User.IsInRole("Admin") || User.IsInRole("Admin_SyS"))
            {
                ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);
                ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null).Where(d => d.id_empresa == eRGOS_Cuestionarios_Trabajador_N01.id_empresa), "id_centro_trabajo", "Nombre_centro_trabajo", eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo);

            }
            else if (User.IsInRole("Admin-Guest") || User.IsInRole("Guest") || User.IsInRole("Final_Guest"))
            {
                ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null).Where(d => d.id_empresa == eRGOS_Cuestionarios_Trabajador_N01.id_empresa).Where(d =>d.id_centro_trabajo == eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo), "id_centro_trabajo", "Nombre_centro_trabajo", eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo);
                ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null).Where(E => E.id_empresa == id_empresa), "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);
            }
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // POST: Encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Cuestionarios_Trabajador_N01).State = EntityState.Modified;
                db.SaveChanges();
                int id_survey = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == eRGOS_Cuestionarios_Trabajador_N01.id_empresa select E.id_encuesta).FirstOrDefault();
                int id_Clima = (from E in db.ClimaLaboral_Cuestionario_Resultados_N01 where E.id_cuestionario_trabajador == eRGOS_Cuestionarios_Trabajador_N01.id_cuestionario_trabajador select E.id_cuestionario_resultado).FirstOrDefault();
               // int id_E360 = (from E in db.E360_Cuestionario_Resultado_N01 where E.id_cuestionario_trabajador == eRGOS_Cuestionarios_Trabajador_N01.id_cuestionario_trabajador select E.id_cuestionario_resultado).FirstOrDefault();
                if (User.IsInRole("Admin") || User.IsInRole("Admin_SyS") || User.IsInRole("Admin-Guest"))
                {
                    return RedirectToAction("Index");
                }
                else if (User.IsInRole("Guest") || User.IsInRole("Admin-Guest") || User.IsInRole("Final_Guest"))
                {
                    if (id_survey == 4)
                    {
                        return RedirectToAction("Edit", "MultiEvaluaClimaLaboral", new { id = id_Clima });
                    }
                    else if (id_survey == 5)
                    {
                        return RedirectToAction("Mi_Cuestionario", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Encuesta", "Trabajador_Resultados", new { id_CT = eRGOS_Cuestionarios_Trabajador_N01.id_cuestionario_trabajador, id_C = eRGOS_Cuestionarios_Trabajador_N01.id_encuesta });
                    }
                }
            }
            //ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01, "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: Encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            if (eRGOS_Cuestionarios_Trabajador_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // POST: Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string UserName = User.Identity.Name;
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            db.ERGOS_Cuestionarios_Trabajador_N01.Remove(eRGOS_Cuestionarios_Trabajador_N01);
            db.SaveChanges();
            if (User.IsInRole("Admin") || User.IsInRole("Admin_SyS"))
            {
                return RedirectToAction("Index");
            }
            else if (User.IsInRole("Admin-Guest"))
            {
                return RedirectToAction("Encuestas_Admin", "Encuestas", new { User_Name = UserName });
            }
            else if (User.IsInRole("Admin_Centro"))
            {
                return RedirectToAction("Encuestas_Centro", "Encuestas", new { User_Name = UserName });
            }
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
