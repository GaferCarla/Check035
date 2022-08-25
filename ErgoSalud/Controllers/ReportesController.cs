using ErgoSalud.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    public class ReportesController : Controller
    {
        private Check035Entities db = new Check035Entities();
        //###################################################################              COLORES         ########################################

        public string Color_Nulo = "rgba(155, 229, 247, 0.8)";
        public string Color_Bajo = "rgba(107, 245, 110, 0.8)";
        public string Color_Medio = "rgba(255, 255, 0, 0.8)";
        public string Color_Alto = "rgba(255, 192, 0, 0.8)";
        public string Color_Muy_Alto = "rgba(255, 0, 0, 0.8)";
        //#########################################################     DIMENSIONES      ##################################################################
        public string[] Cat_Colores = new string[5];
        public string[] Cat_Nivel = new string[5];
        public string[] Dom_Colores = new string[10];
        public string[] Dom_Nivel = new string[10];
        public string[] Dim_Colores = new string[25];
        public string[] Dim_Nivel = new string[25];
        public string[] Dim_E2_Colores = new string[20];
        public string[] Dim_E2_Nivel = new string[20];

        public int?[] Resultados_Reales = new int?[15];

        public string[] Resultados_Colores = new string[20];
        public string[] Resultados_Colores_Sup = new string[15];
        public string[] Resultados_Colores_Emp = new string[15];
        public string[] Resultados_Colores_Sub = new string[15];
        public string[] Resultados_Colores_Com = new string[15];
        public string[] Resultados_Colores_Depts = new string[15];
        // GET: MultiEvaluaE360
        public static string promedio_color(double? promedio)
        {
            string color;
            if (promedio < 39)
                color = "red";
            else if (promedio >= 39 && promedio < 60)
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

        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA LOS RESULTADOS INDIVUDUALES DEL CUESTIONARIO EN PDF 
        //
        //***********************************************************************************************************************************************************************************************************************************************************
        public ActionResult RIndE360jsdhkdsgbf894378uyfsb9834t8hskdjgh3894ghoi987y4398duicfkgn509ufhhfg4Lw(int? id_CT)
        {
            if (id_CT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01 = db.E360_Cuestionario_Resultado_N01.Find(id_CT);
            if (e360_Cuestionario_Resultado_N01 == null)
            {
                return HttpNotFound();
            }
    

            e360_Cuestionario_Resultado_N01.C_I = e360_Cuestionario_Resultado_N01.C_I * 100 / 12;
            e360_Cuestionario_Resultado_N01.C_II = e360_Cuestionario_Resultado_N01.C_II * 100 / 18;
            e360_Cuestionario_Resultado_N01.C_III = e360_Cuestionario_Resultado_N01.C_III * 100 / 6;
            e360_Cuestionario_Resultado_N01.C_IV = e360_Cuestionario_Resultado_N01.C_IV * 100 / 9;
            e360_Cuestionario_Resultado_N01.C_V = e360_Cuestionario_Resultado_N01.C_V * 100 / 9;
            e360_Cuestionario_Resultado_N01.C_VI = e360_Cuestionario_Resultado_N01.C_VI * 100 / 6;
            e360_Cuestionario_Resultado_N01.C_VII = e360_Cuestionario_Resultado_N01.C_VII * 100 / 6;
            e360_Cuestionario_Resultado_N01.C_VIII = e360_Cuestionario_Resultado_N01.C_VIII * 100 / 9;
            e360_Cuestionario_Resultado_N01.C_IX = e360_Cuestionario_Resultado_N01.C_IX * 100 / 15;
            e360_Cuestionario_Resultado_N01.C_X = e360_Cuestionario_Resultado_N01.C_X * 100 / 9;
            e360_Cuestionario_Resultado_N01.C_XI = e360_Cuestionario_Resultado_N01.C_XI * 100 / 12;
            e360_Cuestionario_Resultado_N01.C_XII = e360_Cuestionario_Resultado_N01.C_XII * 100 / 9;
            e360_Cuestionario_Resultado_N01.C_XIII = e360_Cuestionario_Resultado_N01.C_XIII * 100 / 15;
            e360_Cuestionario_Resultado_N01.C_XIV = e360_Cuestionario_Resultado_N01.C_XIV * 100 / 9;
            e360_Cuestionario_Resultado_N01.C_XV = e360_Cuestionario_Resultado_N01.C_XV * 100 / 6;
            e360_Cuestionario_Resultado_N01.Calificacion = e360_Cuestionario_Resultado_N01.Calificacion * 100 / 150;

            Resultados_Colores[0] = promedio_color(e360_Cuestionario_Resultado_N01.C_I);
            Resultados_Colores[1] = promedio_color(e360_Cuestionario_Resultado_N01.C_II);
            Resultados_Colores[2] = promedio_color(e360_Cuestionario_Resultado_N01.C_III);
            Resultados_Colores[3] = promedio_color(e360_Cuestionario_Resultado_N01.C_IV);
            Resultados_Colores[4] = promedio_color(e360_Cuestionario_Resultado_N01.C_V);
            Resultados_Colores[5] = promedio_color(e360_Cuestionario_Resultado_N01.C_VI);
            Resultados_Colores[6] = promedio_color(e360_Cuestionario_Resultado_N01.C_VII);
            Resultados_Colores[7] = promedio_color(e360_Cuestionario_Resultado_N01.C_VIII);
            Resultados_Colores[8] = promedio_color(e360_Cuestionario_Resultado_N01.C_IX);
            Resultados_Colores[9] = promedio_color(e360_Cuestionario_Resultado_N01.C_X);
            Resultados_Colores[10] = promedio_color(e360_Cuestionario_Resultado_N01.C_XI);
            Resultados_Colores[11] = promedio_color(e360_Cuestionario_Resultado_N01.C_XII);
            Resultados_Colores[12] = promedio_color(e360_Cuestionario_Resultado_N01.C_XIII);
            Resultados_Colores[13] = promedio_color(e360_Cuestionario_Resultado_N01.C_XIV);
            Resultados_Colores[14] = promedio_color(e360_Cuestionario_Resultado_N01.C_XV);


            var Final_Empresa = db.E360_Cuestionario_Resultado_N01.Find(id_CT);
            var Evaluado = db.ERGOS_Cuestionarios_Trabajador_N01.Find(Final_Empresa.id_CT_Evaluado);
            ViewBag.Final_Empresa = Final_Empresa;
            ViewBag.Resultados_Colores = Resultados_Colores;
            if (Evaluado != null)
            {
                if (Evaluado.id_trabajador != null) { 
                    ViewBag.Evaluado = Evaluado.id_trabajador;
                }
            }
            else {

                ViewBag.Evaluado = " ";
            }
            return View(e360_Cuestionario_Resultado_N01);

        }
        public ActionResult RIndCLMAuirmBaf4AhwsdsaJ6fobQAreA9mBUt4dmB5CBNvjDfuIhma4ZpjMxmTosLw(int? id_CT)
        {
            if (id_CT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01 = db.ClimaLaboral_Cuestionario_Resultados_N01.Find(id_CT);
            if (climaLaboral_Cuestionario_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            Resultados_Reales[0] = climaLaboral_Cuestionario_Resultados_N01.C_I;
            Resultados_Reales[1] = climaLaboral_Cuestionario_Resultados_N01.C_II;
            Resultados_Reales[2] = climaLaboral_Cuestionario_Resultados_N01.C_III;
            Resultados_Reales[3] = climaLaboral_Cuestionario_Resultados_N01.C_IV;
            Resultados_Reales[4] = climaLaboral_Cuestionario_Resultados_N01.C_V;
            Resultados_Reales[5] = climaLaboral_Cuestionario_Resultados_N01.C_VI;
            Resultados_Reales[6] = climaLaboral_Cuestionario_Resultados_N01.C_VII;
            Resultados_Reales[7] = climaLaboral_Cuestionario_Resultados_N01.C_VIII;
            Resultados_Reales[8] = climaLaboral_Cuestionario_Resultados_N01.C_IX;

            climaLaboral_Cuestionario_Resultados_N01.C_I = climaLaboral_Cuestionario_Resultados_N01.C_I * 100 / 12;
            climaLaboral_Cuestionario_Resultados_N01.C_II = climaLaboral_Cuestionario_Resultados_N01.C_II * 100 / 12;
            climaLaboral_Cuestionario_Resultados_N01.C_III = climaLaboral_Cuestionario_Resultados_N01.C_III * 100 / 18;
            climaLaboral_Cuestionario_Resultados_N01.C_IV = climaLaboral_Cuestionario_Resultados_N01.C_IV * 100 / 27;
            climaLaboral_Cuestionario_Resultados_N01.C_V = climaLaboral_Cuestionario_Resultados_N01.C_V * 100 / 42;
            climaLaboral_Cuestionario_Resultados_N01.C_VI = climaLaboral_Cuestionario_Resultados_N01.C_VI * 100 / 24;
            climaLaboral_Cuestionario_Resultados_N01.C_VII = climaLaboral_Cuestionario_Resultados_N01.C_VII * 100 / 15;
            climaLaboral_Cuestionario_Resultados_N01.C_VIII = climaLaboral_Cuestionario_Resultados_N01.C_VIII * 100 / 6;
            climaLaboral_Cuestionario_Resultados_N01.C_IX = climaLaboral_Cuestionario_Resultados_N01.C_IX * 100 / 12;
            climaLaboral_Cuestionario_Resultados_N01.Calificacion = climaLaboral_Cuestionario_Resultados_N01.Calificacion * 100 / 168;

            Resultados_Colores[0] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_I);
            Resultados_Colores[1] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_II);
            Resultados_Colores[2] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_III);
            Resultados_Colores[3] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_IV);
            Resultados_Colores[4] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_V);
            Resultados_Colores[5] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_VI);
            Resultados_Colores[6] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_VII);
            Resultados_Colores[7] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_VIII);
            Resultados_Colores[8] = promedio_color(climaLaboral_Cuestionario_Resultados_N01.C_IX);

            var Final_Empresa = db.ClimaLaboral_Cuestionario_Resultados_N01.Find(id_CT);
            ViewBag.Final_Empresa = Final_Empresa;
            ViewBag.Resultados_Colores = Resultados_Colores;
            ViewBag.Resultados_Reales = Resultados_Reales;
            return View(climaLaboral_Cuestionario_Resultados_N01);

        }
        public ActionResult RInduirmBaf4AhwsdsaJ6fobQAreA9mBUt4dmB5CBNvjDfuIhma4ZpjMxmTosLw(int id_CT)
        {
            ViewBag.id_empleado = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where CR.id_cuestionario_trabajador == id_CT
                                   select CR.id_trabajador).FirstOrDefault();
            ViewBag.Employe_Name = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where CR.id_cuestionario_trabajador == id_CT
                                    select CR.Nombre).FirstOrDefault();
            ViewBag.Depto = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                             where CR.id_cuestionario_trabajador == id_CT
                             select CR.Departamento).FirstOrDefault();
            ViewBag.Edad = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                            where CR.id_cuestionario_trabajador == id_CT
                            select CR.DATA_Edades_N01.edad).FirstOrDefault();
            ViewBag.Estado_Civil = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where CR.id_cuestionario_trabajador == id_CT
                                    select CR.DATA_Estado_Civil_N01.Estado_Civil).FirstOrDefault();
            ViewBag.Experiencia_puesto = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                          where CR.id_cuestionario_trabajador == id_CT
                                          select CR.DATA_Experiencia_puesto_N01.Experiencia_puesto).FirstOrDefault();
            ViewBag.Nivel_Estudios = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                      where CR.id_cuestionario_trabajador == id_CT
                                      select CR.DATA_Nivel_Estudios_N01.Nivel_Estudios).FirstOrDefault();
            ViewBag.Rotacion_Turno = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                      where CR.id_cuestionario_trabajador == id_CT
                                      select CR.DATA_Rotacion_Turno_N01.Rotacion_Turno).FirstOrDefault();
            ViewBag.Sexo = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                            where CR.id_cuestionario_trabajador == id_CT
                            select CR.DATA_Sexo_N01.Sexo).FirstOrDefault();
            ViewBag.Tipo_Contratacion = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                         where CR.id_cuestionario_trabajador == id_CT
                                         select CR.DATA_Tipo_Contratacion_N01.Tipo_Contratacion).FirstOrDefault();
            ViewBag.Tipo_Jornada = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where CR.id_cuestionario_trabajador == id_CT
                                    select CR.DATA_Tipo_Jornada_N01.Tipo_Jornada).FirstOrDefault();
            ViewBag.Tipo_Personal = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                     where CR.id_cuestionario_trabajador == id_CT
                                     select CR.DATA_Tipo_Personal_N01.Tipo_Personal).FirstOrDefault();
            ViewBag.Tipo_puesto = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where CR.id_cuestionario_trabajador == id_CT
                                   select CR.DATA_Tipo_puesto_N01.Tipo_puesto).FirstOrDefault();

            try
            {
                ViewBag.Categorias = (from total in db.fnDemo_N035_Categorias_Pilot(id_CT)
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



        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA EL CUESTIONARIO EN PDF 
        //
        //***********************************************************************************************************************************************************************************************************************************************************
        public ActionResult AOwE360YbqttJ6foDFHDFHDFHf4AhwNvjDfuDFHDFHjMxmTo7UJYHTG45HBRF23SD4DGFFas(int? id_CT)
        {
            if (id_CT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            E360_Cuestionario_Resultado_N01 e360_Cuestionario_Resultado_N01 = db.E360_Cuestionario_Resultado_N01.Find(id_CT);
            if (e360_Cuestionario_Resultado_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", e360_Cuestionario_Resultado_N01.id_cuestionario_trabajador);
            return View(e360_Cuestionario_Resultado_N01);
        }
        public ActionResult AOwCLMAYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw(int? id_CT)
        {
            if (id_CT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClimaLaboral_Cuestionario_Resultados_N01 climaLaboral_Cuestionario_Resultados_N01 = db.ClimaLaboral_Cuestionario_Resultados_N01.Find(id_CT);
            if (climaLaboral_Cuestionario_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Ocupacion", climaLaboral_Cuestionario_Resultados_N01.id_cuestionario_trabajador);
            return View(climaLaboral_Cuestionario_Resultados_N01);
        }
        public ActionResult AOwYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw(int id_CT, int id_C)
        {
            ViewBag.id_empleado = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where CR.id_cuestionario_trabajador == id_CT
                                   select CR.id_trabajador).FirstOrDefault();
            ViewBag.Employe_Name = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where CR.id_cuestionario_trabajador == id_CT
                                    select CR.Nombre).FirstOrDefault();
            ViewBag.Depto = (from CR in db.ERGOS_Cuestionarios_Trabajador_N01
                             where CR.id_cuestionario_trabajador == id_CT
                             select CR.Departamento).FirstOrDefault();
            ViewBag.id_cuestionario = id_C;
            ViewBag.Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                               where CR.id_cuestionario_trabajador == id_CT
                               select new Respuestas { id_respuesta = CR.id_respuesta, id_pregunta = CR.id_pregunta }).ToArray();
             
            List<Surveys> datos = new List<Surveys>();

            if (id_C == 3)
            {
                datos = (from total in db.fn_Final_view_surveys(id_CT)
                         select new Surveys
                         {
                             id_Cuestionario_Resultado = total.id_Cuestionario_Resultado,
                             id_cuestionario = id_C,
                             id_respuesta = total.id_respuesta,
                             No_Pregunta = total.id_pregunta,
                             Preguntas = total.Preguntas
                         }).ToList();

                int? P73 = datos[72].id_respuesta;
                int? P74 = datos[73].id_respuesta;
                int? P75 = datos[74].id_respuesta;
                int? P76 = datos[75].id_respuesta;
                int? P77 = datos[76].id_respuesta;

                // 1 - SI  2 - NO
                if (P73 == 1 || P74 == 1 || P75 == 1 || P76 == 1 || P77 == 1)
                {
                    ViewBag.Show_GUIA_I = 1;
                }
                else if (P73 == null && P74 == null && P75 == null && P76 == null && P77 == null)
                {
                    ViewBag.Show_GUIA_I = 1;
                }
                else
                {
                    ViewBag.Show_GUIA_I = 2;
                }

            }
            else if (id_C == 2)
            {
             datos = (from total in db.fn_Final_view_surveys_S2(id_CT)
                         select new Surveys
                         {
                             id_Cuestionario_Resultado = total.id_Cuestionario_Resultado,
                             id_cuestionario = id_C,
                             id_respuesta = total.id_respuesta,
                             No_Pregunta = total.id_pregunta,
                             Preguntas = total.Preguntas
                         }).ToList();

                int? P47 = datos[46].id_respuesta;
                int? P48 = datos[47].id_respuesta;
                int? P49 = datos[48].id_respuesta;
                int? P50 = datos[49].id_respuesta;
                int? P51 = datos[50].id_respuesta;

                // 1 - SI  2 - NO
                if (P47 == 1 || P48 == 1 || P49 == 1 || P50 == 1 || P51 == 1)
                {
                    ViewBag.Show_GUIA_I = 1;
                }
                else if (P47 == null && P48 == null && P49 == null && P50 == null && P51 == null)
                {
                    ViewBag.Show_GUIA_I = 1;
                }
                else {
                    ViewBag.Show_GUIA_I = 2;
                }

            }


           

            //ViewBag.Show_GUIA_I_S3 = datos.;


            ViewBag.Survey = id_C;

            ViewBag.Survey = id_C;
            ViewBag.Survey_1 = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                join P in db.ERGOS_Preguntas_N01 on CR.id_pregunta equals P.id_pregunta
                                where CR.id_cuestionario_trabajador == id_CT && CR.id_encuesta == 1
                                group CR by new { P.No_Pregunta, P.Preguntas, CR.id_Cuestionario_Resultado, CR.id_respuesta, CR.id_encuesta } into X
                                select new Surveys { id_cuestionario = X.Key.id_encuesta, No_Pregunta = X.Key.No_Pregunta, Preguntas = X.Key.Preguntas, id_Cuestionario_Resultado = X.Key.id_Cuestionario_Resultado, id_respuesta = X.Key.id_respuesta }).OrderBy(x => x.No_Pregunta);

            // var datos2 = datos.Distinct(x => x.id_Cuestionario_Resultado);
            return View(datos);
        }

        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA LOS RESULTADOS GENERALES EN PDF 
        //
        //***********************************************************************************************************************************************************************************************************************************************************

        public ActionResult ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw(int? id, int id_encuesta)
        {

            ViewBag.Survey = id_encuesta;
            ViewBag.id_empresa = id;

            ViewBag.Company = (from Company in db.ERGOS_Empresas_N01
                               where Company.id_empresa == id
                               select Company.Razon_Social).FirstOrDefault();
            ViewBag.Canalizados = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_empresa == id
                                   select Emp_Canalizados.Canalizacion).Count();
            ViewBag.Getting_canalizados = (from Empleado_C in db.ERGOS_Cuestionarios_Trabajador_N01
                                           where Empleado_C.id_empresa == id && Empleado_C.Canalizacion == 1
                                           select new Getting_canalizados { ID = Empleado_C.id_trabajador });
            try
            {
                string sqlQuery = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
                Object[] parameters = { id };
                int activityCount = db.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                ViewBag.Final_Average = activityCount;

                if (id_encuesta == 3)
                {
                    ViewBag.Total_Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CR.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_encuesta == 3 && CT.id_empresa == id
                                             group CR by CR.id_pregunta into g
                                             orderby g.Sum(X => X.id_pregunta)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = g.Sum(X => (int?)X.Calificacion ?? 0)
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = G.Total_Categoria_1,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Total
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = G.Total_Categoria_2,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = G.Total_Categoria_3,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = G.Total_Categoria_4,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();

                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_5_G = G.Total_Categoria_5,
                                                SUMATORIA = G.Sumatoria_Cat_V
                                            }).FirstOrDefault();

                    //Definiendo COLORES DE CATEGORIAS   NULO - BAJO - MEDIO -ALTO -MUY ALTO

                    if (ViewBag.Cat_1_Global.Categoria_1_G < 5)
                    {

                        Cat_Colores[0] = Color_Nulo;
                        Cat_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 5 && ViewBag.Cat_1_Global.Categoria_1_G < 9)
                    {
                        Cat_Colores[0] = Color_Bajo;
                        Cat_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 9 && ViewBag.Cat_1_Global.Categoria_1_G < 11)
                    {
                        Cat_Colores[0] = Color_Medio;
                        Cat_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 11 && ViewBag.Cat_1_Global.Categoria_1_G < 14)
                    {
                        Cat_Colores[0] = Color_Alto;
                        Cat_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 14)
                    {
                        Cat_Colores[0] = Color_Muy_Alto;
                        Cat_Nivel[0] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_2_Global.Categoria_2_G < 15)
                    {
                        Cat_Colores[1] = Color_Nulo;
                        Cat_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 15 && ViewBag.Cat_2_Global.Categoria_2_G < 30)
                    {
                        Cat_Colores[1] = Color_Bajo;
                        Cat_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 30 && ViewBag.Cat_2_Global.Categoria_2_G < 45)
                    {
                        Cat_Colores[1] = Color_Medio;
                        Cat_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 45 && ViewBag.Cat_2_Global.Categoria_2_G < 60)
                    {
                        Cat_Colores[1] = Color_Alto;
                        Cat_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 60)
                    {
                        Cat_Colores[1] = Color_Muy_Alto;
                        Cat_Nivel[1] = "Muy Alto";
                    }

                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_3_Global.Categoria_3_G < 5)
                    {
                        Cat_Colores[2] = Color_Nulo;
                        Cat_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 5 && ViewBag.Cat_3_Global.Categoria_3_G < 7)
                    {
                        Cat_Colores[2] = Color_Bajo;
                        Cat_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 7 && ViewBag.Cat_3_Global.Categoria_3_G < 10)
                    {
                        Cat_Colores[2] = Color_Medio;
                        Cat_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 10 && ViewBag.Cat_3_Global.Categoria_3_G < 13)
                    {
                        Cat_Colores[2] = Color_Alto;
                        Cat_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 13)
                    {
                        Cat_Colores[2] = Color_Muy_Alto;
                        Cat_Nivel[2] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_4_Global.Categoria_4_G < 14)
                    {
                        Cat_Colores[3] = Color_Nulo;
                        Cat_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 14 && ViewBag.Cat_4_Global.Categoria_4_G < 29)
                    {
                        Cat_Colores[3] = Color_Bajo;
                        Cat_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 29 && ViewBag.Cat_4_Global.Categoria_4_G < 42)
                    {
                        Cat_Colores[3] = Color_Medio;
                        Cat_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 42 && ViewBag.Cat_4_Global.Categoria_4_G < 58)
                    {
                        Cat_Colores[3] = Color_Alto;
                        Cat_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 58)
                    {
                        Cat_Colores[3] = Color_Muy_Alto;
                        Cat_Nivel[3] = "Muy Alto";
                    }

                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_5_Global.Categoria_5_G < 10)
                    {
                        Cat_Colores[4] = Color_Nulo;
                        Cat_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 10 && ViewBag.Cat_5_Global.Categoria_5_G < 14)
                    {
                        Cat_Colores[4] = Color_Bajo;
                        Cat_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 14 && ViewBag.Cat_5_Global.Categoria_5_G < 18)
                    {
                        Cat_Colores[4] = Color_Medio;
                        Cat_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 18 && ViewBag.Cat_5_Global.Categoria_5_G < 23)
                    {
                        Cat_Colores[4] = Color_Alto;
                        Cat_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 23)
                    {
                        Cat_Colores[4] = Color_Muy_Alto;
                        Cat_Nivel[4] = "Muy Alto";
                    }

                    ViewBag.Colores = Cat_Colores;
                    ViewBag.Nivel = Cat_Nivel;


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_8_G = G.Total_Dominio_8
                                            }).FirstOrDefault();
                    ViewBag.Dom_9_Global = (from G in db.fnDemo_N035_Dominios_9_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_9_G = G.Total_Dominio_9
                                            }).FirstOrDefault();
                    ViewBag.Dom_10_Global = (from G in db.fnDemo_N035_Dominios_10_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dominio_10_G = G.Total_Dominio_10
                                             }).FirstOrDefault();
                    //-------------------------------------------------------------------------------------
                    //          Definiendo COLORES DE DOMINIOS   NULO - BAJO - MEDIO -ALTO -MUY ALTO
                    //-------------------------------------------------------------------------------------
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_1_Global.Dominio_1_G < 5)
                    {

                        Dom_Colores[0] = Color_Nulo;
                        Dom_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 5 && ViewBag.Dom_1_Global.Dominio_1_G < 9)
                    {
                        Dom_Colores[0] = Color_Bajo;
                        Dom_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 9 && ViewBag.Dom_1_Global.Dominio_1_G < 11)
                    {
                        Dom_Colores[0] = Color_Medio;
                        Dom_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 11 && ViewBag.Dom_1_Global.Dominio_1_G < 14)
                    {
                        Dom_Colores[0] = Color_Alto;
                        Dom_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 14)
                    {
                        Dom_Colores[0] = Color_Muy_Alto;
                        Dom_Nivel[0] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_2_Global.Dominio_2_G < 15)
                    {
                        Dom_Colores[1] = Color_Nulo;
                        Dom_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 15 && ViewBag.Dom_2_Global.Dominio_2_G < 21)
                    {
                        Dom_Colores[1] = Color_Bajo;
                        Dom_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 21 && ViewBag.Dom_2_Global.Dominio_2_G < 27)
                    {
                        Dom_Colores[1] = Color_Medio;
                        Dom_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 27 && ViewBag.Dom_2_Global.Dominio_2_G < 37)
                    {
                        Dom_Colores[1] = Color_Alto;
                        Dom_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 37)
                    {
                        Dom_Colores[1] = Color_Muy_Alto;
                        Dom_Nivel[1] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_3_Global.Dominio_3_G < 11)
                    {
                        Dom_Colores[2] = Color_Nulo;
                        Dom_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 11 && ViewBag.Dom_3_Global.Dominio_3_G < 16)
                    {
                        Dom_Colores[2] = Color_Bajo;
                        Dom_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 16 && ViewBag.Dom_3_Global.Dominio_3_G < 21)
                    {
                        Dom_Colores[2] = Color_Medio;
                        Dom_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 21 && ViewBag.Dom_3_Global.Dominio_3_G < 25)
                    {
                        Dom_Colores[2] = Color_Alto;
                        Dom_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 25)
                    {
                        Dom_Colores[2] = Color_Muy_Alto;
                        Dom_Nivel[2] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_4_Global.Dominio_4_G < 1)
                    {
                        Dom_Colores[3] = Color_Nulo;
                        Dom_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 1 && ViewBag.Dom_4_Global.Dominio_4_G < 2)
                    {
                        Dom_Colores[3] = Color_Bajo;
                        Dom_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 2 && ViewBag.Dom_4_Global.Dominio_4_G < 4)
                    {
                        Dom_Colores[3] = Color_Medio;
                        Dom_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 4 && ViewBag.Dom_4_Global.Dominio_4_G < 6)
                    {
                        Dom_Colores[3] = Color_Alto;
                        Dom_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 6)
                    {
                        Dom_Colores[3] = Color_Muy_Alto;
                        Dom_Nivel[3] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_5_Global.Dominio_5_G < 4)
                    {
                        Dom_Colores[4] = Color_Nulo;
                        Dom_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 4 && ViewBag.Dom_5_Global.Dominio_5_G < 6)
                    {
                        Dom_Colores[4] = Color_Bajo;
                        Dom_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 6 && ViewBag.Dom_5_Global.Dominio_5_G < 8)
                    {
                        Dom_Colores[4] = Color_Medio;
                        Dom_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 8 && ViewBag.Dom_5_Global.Dominio_5_G < 10)
                    {
                        Dom_Colores[4] = Color_Alto;
                        Dom_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 10)
                    {
                        Dom_Colores[4] = Color_Muy_Alto;
                        Dom_Nivel[4] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_6_Global.Dominio_6_G < 9)
                    {
                        Dom_Colores[5] = Color_Nulo;
                        Dom_Nivel[5] = "Nulo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 9 && ViewBag.Dom_6_Global.Dominio_6_G < 12)
                    {
                        Dom_Colores[5] = Color_Bajo;
                        Dom_Nivel[5] = "Bajo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 12 && ViewBag.Dom_6_Global.Dominio_6_G < 16)
                    {
                        Dom_Colores[5] = Color_Medio;
                        Dom_Nivel[5] = "Medio";

                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 16 && ViewBag.Dom_6_Global.Dominio_6_G < 20)
                    {
                        Dom_Colores[5] = Color_Alto;
                        Dom_Nivel[5] = "Alto";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 20)
                    {
                        Dom_Colores[5] = Color_Muy_Alto;
                        Dom_Nivel[5] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_7_Global.Dominio_7_G < 10)
                    {
                        Dom_Colores[6] = Color_Nulo;
                        Dom_Nivel[6] = "Nulo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 10 && ViewBag.Dom_7_Global.Dominio_7_G < 13)
                    {
                        Dom_Colores[6] = Color_Bajo;
                        Dom_Nivel[6] = "Bajo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 13 && ViewBag.Dom_7_Global.Dominio_7_G < 17)
                    {
                        Dom_Colores[6] = Color_Medio;
                        Dom_Nivel[6] = "Medio";

                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 17 && ViewBag.Dom_7_Global.Dominio_7_G < 21)
                    {
                        Dom_Colores[6] = Color_Alto;
                        Dom_Nivel[6] = "Alto";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 21)
                    {
                        Dom_Colores[6] = Color_Muy_Alto;
                        Dom_Nivel[6] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_8_Global.Dominio_8_G < 7)
                    {
                        Dom_Colores[7] = Color_Nulo;
                        Dom_Nivel[7] = "Nulo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 7 && ViewBag.Dom_8_Global.Dominio_8_G < 10)
                    {
                        Dom_Colores[7] = Color_Bajo;
                        Dom_Nivel[7] = "Bajo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 10 && ViewBag.Dom_8_Global.Dominio_8_G < 13)
                    {
                        Dom_Colores[7] = Color_Medio;
                        Dom_Nivel[7] = "Medio";

                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 13 && ViewBag.Dom_8_Global.Dominio_8_G < 16)
                    {
                        Dom_Colores[7] = Color_Alto;
                        Dom_Nivel[7] = "Alto";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 16)
                    {
                        Dom_Colores[7] = Color_Muy_Alto;
                        Dom_Nivel[7] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_9_Global.Dominio_9_G < 6)
                    {
                        Dom_Colores[8] = Color_Nulo;
                        Dom_Nivel[8] = "Nulo";
                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 6 && ViewBag.Dom_9_Global.Dominio_9_G < 10)
                    {
                        Dom_Colores[8] = Color_Bajo;
                        Dom_Nivel[8] = "Bajo";
                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 10 && ViewBag.Dom_9_Global.Dominio_9_G < 14)
                    {
                        Dom_Colores[8] = Color_Medio;
                        Dom_Nivel[8] = "Medio";

                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 14 && ViewBag.Dom_9_Global.Dominio_9_G < 18)
                    {
                        Dom_Colores[8] = Color_Alto;
                        Dom_Nivel[8] = "Alto";
                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 18)
                    {
                        Dom_Colores[8] = Color_Muy_Alto;
                        Dom_Nivel[8] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_10_Global.Dominio_10_G < 4)
                    {
                        Dom_Colores[9] = Color_Nulo;
                        Dom_Nivel[9] = "Nulo";
                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 4 && ViewBag.Dom_10_Global.Dominio_10_G < 6)
                    {
                        Dom_Colores[9] = Color_Bajo;
                        Dom_Nivel[9] = "Bajo";
                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 6 && ViewBag.Dom_10_Global.Dominio_10_G < 8)
                    {
                        Dom_Colores[9] = Color_Medio;
                        Dom_Nivel[9] = "Medio";

                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 8 && ViewBag.Dom_10_Global.Dominio_10_G < 10)
                    {
                        Dom_Colores[9] = Color_Alto;
                        Dom_Nivel[9] = "Alto";
                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 10)
                    {
                        Dom_Colores[9] = Color_Muy_Alto;
                        Dom_Nivel[9] = "Muy Alto";
                    }

                    ViewBag.Colores_Dom = Dom_Colores;
                    ViewBag.Nivel_Dom = Dom_Nivel;

                    //##############################################################################################################################
                    // ######################################################  INICIO CALCULO Dimension #############################################
                    //##############################################################################################################################

                    ViewBag.Dim_1_Global = (from G in db.fnDemo_N035_Dimension_1_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_1_G = G.Total_Dimension_1 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_2_Global = (from G in db.fnDemo_N035_Dimension_2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_2_G = G.Total_Dimension_2 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_3_Global = (from G in db.fnDemo_N035_Dimension_3_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_3_G = G.Total_Dimension_3
                                            }).FirstOrDefault();
                    ViewBag.Dim_4_Global = (from G in db.fnDemo_N035_Dimension_4_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_4_G = G.Total_Dimension_4 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_5_Global = (from G in db.fnDemo_N035_Dimension_5_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_5_G = G.Total_Dimension_5 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_6_Global = (from G in db.fnDemo_N035_Dimension_6_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_6_G = G.Total_Dimension_6 / 3
                                            }).FirstOrDefault();
                    ViewBag.Dim_7_Global = (from G in db.fnDemo_N035_Dimension_7_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_7_G = G.Total_Dimension_7 / 4
                                            }).FirstOrDefault();
                    ViewBag.Dim_8_Global = (from G in db.fnDemo_N035_Dimension_8_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_8_G = G.Total_Dimension_8 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_9_Global = (from G in db.fnDemo_N035_Dimension_9_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dimension_9_G = G.Total_Dimension_9 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_10_Global = (from G in db.fnDemo_N035_Dimension_10_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_10_G = G.Total_Dimension_10 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_11_Global = (from G in db.fnDemo_N035_Dimension_11_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_11_G = G.Total_Dimension_11 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_12_Global = (from G in db.fnDemo_N035_Dimension_12_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_12_G = G.Total_Dimension_12 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_13_Global = (from G in db.fnDemo_N035_Dimension_13_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_13_G = G.Total_Dimension_13 / 2
                                             }).FirstOrDefault();
                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_14_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_14_Resultados(id)
                                                    select new Respuestas
                                                    {
                                                        Dimension_14_G = G.Total_Dimension_14
                                                    }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_14_Global = (from G in db.fnDemo_N035_Dimension_14_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_14_G = G.Total_Dimension_14 / 2
                                             }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_15_Global = (from G in db.fnDemo_N035_Dimension_15_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_15_G = G.Total_Dimension_15 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_16_Global = (from G in db.fnDemo_N035_Dimension_16_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_16_G = G.Total_Dimension_16 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_17_Global = (from G in db.fnDemo_N035_Dimension_17_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_17_G = G.Total_Dimension_17 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_18_Global = (from G in db.fnDemo_N035_Dimension_18_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_18_G = G.Total_Dimension_18 / 5
                                             }).FirstOrDefault();
                    ViewBag.Dim_19_Global = (from G in db.fnDemo_N035_Dimension_19_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_19_G = G.Total_Dimension_19 / 5
                                             }).FirstOrDefault();
                    ViewBag.Dim_20_Global = (from G in db.fnDemo_N035_Dimension_20_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_20_G = G.Total_Dimension_20 / 4
                                             }).FirstOrDefault();


                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_21_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_21_Resultados(id)
                                                    select new Respuestas
                                                    {
                                                        Dimension_21_G = G.Total_Dimension_21
                                                    }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_21_Global = (from G in db.fnDemo_N035_Dimension_21_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_21_G = G.Total_Dimension_21 / 8
                                             }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################
                    ViewBag.Dim_22_Global = (from G in db.fnDemo_N035_Dimension_22_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_22_G = G.Total_Dimension_22 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_23_Global = (from G in db.fnDemo_N035_Dimension_23_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_23_G = G.Total_Dimension_23 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_24_Global = (from G in db.fnDemo_N035_Dimension_24_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_24_G = G.Total_Dimension_24 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_25_Global = (from G in db.fnDemo_N035_Dimension_25_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dimension_25_G = G.Total_Dimension_25 / 2
                                             }).FirstOrDefault();
                    //-------------------------------------------------------------------------------------
                    //          Definiendo COLORES DE Dimension   NULO - BAJO - MEDIO -ALTO -MUY ALTO
                    //-------------------------------------------------------------------------------------
                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_1_Global.Dimension_1_G >= 0 && ViewBag.Dim_1_Global.Dimension_1_G < 1)
                    {

                        Dim_Colores[0] = Color_Nulo;
                        Dim_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Dim_1_Global.Dimension_1_G >= 1 && ViewBag.Dim_1_Global.Dimension_1_G < 2)
                    {
                        Dim_Colores[0] = Color_Bajo;
                        Dim_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Dim_1_Global.Dimension_1_G >= 2 && ViewBag.Dim_1_Global.Dimension_1_G < 3)
                    {
                        Dim_Colores[0] = Color_Medio;
                        Dim_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Dim_1_Global.Dimension_1_G >= 3 && ViewBag.Dim_1_Global.Dimension_1_G < 4)
                    {
                        Dim_Colores[0] = Color_Alto;
                        Dim_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Dim_1_Global.Dimension_1_G >= 4)
                    {
                        Dim_Colores[0] = Color_Muy_Alto;
                        Dim_Nivel[0] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////#
                    if (ViewBag.Dim_2_Global.Dimension_2_G >= 0 && ViewBag.Dim_2_Global.Dimension_2_G < 1)
                    {

                        Dim_Colores[1] = Color_Nulo;
                        Dim_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Dim_2_Global.Dimension_2_G >= 1 && ViewBag.Dim_2_Global.Dimension_2_G < 2)
                    {
                        Dim_Colores[1] = Color_Bajo;
                        Dim_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Dim_2_Global.Dimension_2_G >= 2 && ViewBag.Dim_2_Global.Dimension_2_G < 3)
                    {
                        Dim_Colores[1] = Color_Medio;
                        Dim_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Dim_2_Global.Dimension_2_G >= 3 && ViewBag.Dim_2_Global.Dimension_2_G < 4)
                    {
                        Dim_Colores[1] = Color_Alto;
                        Dim_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Dim_2_Global.Dimension_2_G >= 4)
                    {
                        Dim_Colores[1] = Color_Muy_Alto;
                        Dim_Nivel[1] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_3_Global.Dimension_3_G >= 0 && ViewBag.Dim_3_Global.Dimension_3_G < 1)
                    {

                        Dim_Colores[2] = Color_Nulo;
                        Dim_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Dim_3_Global.Dimension_3_G >= 1 && ViewBag.Dim_3_Global.Dimension_3_G < 2)
                    {
                        Dim_Colores[2] = Color_Bajo;
                        Dim_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Dim_3_Global.Dimension_3_G >= 2 && ViewBag.Dim_3_Global.Dimension_3_G < 3)
                    {
                        Dim_Colores[2] = Color_Medio;
                        Dim_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Dim_3_Global.Dimension_3_G >= 3 && ViewBag.Dim_3_Global.Dimension_3_G < 4)
                    {
                        Dim_Colores[2] = Color_Alto;
                        Dim_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Dim_3_Global.Dimension_3_G >= 4)
                    {
                        Dim_Colores[2] = Color_Muy_Alto;
                        Dim_Nivel[2] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_4_Global.Dimension_4_G >= 0 && ViewBag.Dim_4_Global.Dimension_4_G < 1)
                    {

                        Dim_Colores[3] = Color_Nulo;
                        Dim_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Dim_4_Global.Dimension_4_G >= 1 && ViewBag.Dim_4_Global.Dimension_4_G < 2)
                    {
                        Dim_Colores[3] = Color_Bajo;
                        Dim_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Dim_4_Global.Dimension_4_G >= 2 && ViewBag.Dim_4_Global.Dimension_4_G < 3)
                    {
                        Dim_Colores[3] = Color_Medio;
                        Dim_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Dim_4_Global.Dimension_4_G >= 3 && ViewBag.Dim_4_Global.Dimension_4_G < 4)
                    {
                        Dim_Colores[3] = Color_Alto;
                        Dim_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Dim_4_Global.Dimension_4_G >= 4)
                    {
                        Dim_Colores[3] = Color_Muy_Alto;
                        Dim_Nivel[3] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_5_Global.Dimension_5_G >= 0 && ViewBag.Dim_5_Global.Dimension_5_G < 1)
                    {

                        Dim_Colores[4] = Color_Nulo;
                        Dim_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Dim_5_Global.Dimension_5_G >= 1 && ViewBag.Dim_5_Global.Dimension_5_G < 2)
                    {
                        Dim_Colores[4] = Color_Bajo;
                        Dim_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Dim_5_Global.Dimension_5_G >= 2 && ViewBag.Dim_5_Global.Dimension_5_G < 3)
                    {
                        Dim_Colores[4] = Color_Medio;
                        Dim_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Dim_5_Global.Dimension_5_G >= 3 && ViewBag.Dim_5_Global.Dimension_5_G < 4)
                    {
                        Dim_Colores[4] = Color_Alto;
                        Dim_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Dim_5_Global.Dimension_5_G >= 4)
                    {
                        Dim_Colores[4] = Color_Muy_Alto;
                        Dim_Nivel[4] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_6_Global.Dimension_6_G >= 0 && ViewBag.Dim_6_Global.Dimension_6_G < 1)
                    {

                        Dim_Colores[5] = Color_Nulo;
                        Dim_Nivel[5] = "Nulo";
                    }
                    else if (ViewBag.Dim_6_Global.Dimension_6_G >= 1 && ViewBag.Dim_6_Global.Dimension_6_G < 2)
                    {
                        Dim_Colores[5] = Color_Bajo;
                        Dim_Nivel[5] = "Bajo";
                    }
                    else if (ViewBag.Dim_6_Global.Dimension_6_G >= 2 && ViewBag.Dim_6_Global.Dimension_6_G < 3)
                    {
                        Dim_Colores[5] = Color_Medio;
                        Dim_Nivel[5] = "Medio";

                    }
                    else if (ViewBag.Dim_6_Global.Dimension_6_G >= 3 && ViewBag.Dim_6_Global.Dimension_6_G < 4)
                    {
                        Dim_Colores[5] = Color_Alto;
                        Dim_Nivel[5] = "Alto";
                    }
                    else if (ViewBag.Dim_6_Global.Dimension_6_G >= 4)
                    {
                        Dim_Colores[5] = Color_Muy_Alto;
                        Dim_Nivel[5] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_7_Global.Dimension_7_G >= 0 && ViewBag.Dim_7_Global.Dimension_7_G < 1)
                    {

                        Dim_Colores[6] = Color_Nulo;
                        Dim_Nivel[6] = "Nulo";
                    }
                    else if (ViewBag.Dim_7_Global.Dimension_7_G >= 1 && ViewBag.Dim_7_Global.Dimension_7_G < 2)
                    {
                        Dim_Colores[6] = Color_Bajo;
                        Dim_Nivel[6] = "Bajo";
                    }
                    else if (ViewBag.Dim_7_Global.Dimension_7_G >= 2 && ViewBag.Dim_7_Global.Dimension_7_G < 3)
                    {
                        Dim_Colores[6] = Color_Medio;
                        Dim_Nivel[6] = "Medio";

                    }
                    else if (ViewBag.Dim_7_Global.Dimension_7_G >= 3 && ViewBag.Dim_7_Global.Dimension_7_G < 4)
                    {
                        Dim_Colores[6] = Color_Alto;
                        Dim_Nivel[6] = "Alto";
                    }
                    else if (ViewBag.Dim_7_Global.Dimension_7_G >= 4)
                    {
                        Dim_Colores[6] = Color_Muy_Alto;
                        Dim_Nivel[6] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_8_Global.Dimension_8_G >= 0 && ViewBag.Dim_8_Global.Dimension_8_G < 1)
                    {

                        Dim_Colores[7] = Color_Nulo;
                        Dim_Nivel[7] = "Nulo";
                    }
                    else if (ViewBag.Dim_8_Global.Dimension_8_G >= 1 && ViewBag.Dim_8_Global.Dimension_8_G < 2)
                    {
                        Dim_Colores[7] = Color_Bajo;
                        Dim_Nivel[7] = "Bajo";
                    }
                    else if (ViewBag.Dim_8_Global.Dimension_8_G >= 2 && ViewBag.Dim_8_Global.Dimension_8_G < 3)
                    {
                        Dim_Colores[7] = Color_Medio;
                        Dim_Nivel[7] = "Medio";

                    }
                    else if (ViewBag.Dim_8_Global.Dimension_8_G >= 3 && ViewBag.Dim_8_Global.Dimension_8_G < 4)
                    {
                        Dim_Colores[7] = Color_Alto;
                        Dim_Nivel[7] = "Alto";
                    }
                    else if (ViewBag.Dim_8_Global.Dimension_8_G >= 4)
                    {
                        Dim_Colores[7] = Color_Muy_Alto;
                        Dim_Nivel[7] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_9_Global.Dimension_9_G >= 0 && ViewBag.Dim_9_Global.Dimension_9_G < 1)
                    {

                        Dim_Colores[8] = Color_Nulo;
                        Dim_Nivel[8] = "Nulo";
                    }
                    else if (ViewBag.Dim_9_Global.Dimension_9_G >= 1 && ViewBag.Dim_9_Global.Dimension_9_G < 2)
                    {
                        Dim_Colores[8] = Color_Bajo;
                        Dim_Nivel[8] = "Bajo";
                    }
                    else if (ViewBag.Dim_9_Global.Dimension_9_G >= 2 && ViewBag.Dim_9_Global.Dimension_9_G < 3)
                    {
                        Dim_Colores[8] = Color_Medio;
                        Dim_Nivel[8] = "Medio";

                    }
                    else if (ViewBag.Dim_9_Global.Dimension_9_G >= 3 && ViewBag.Dim_9_Global.Dimension_9_G < 4)
                    {
                        Dim_Colores[8] = Color_Alto;
                        Dim_Nivel[8] = "Alto";
                    }
                    else if (ViewBag.Dim_9_Global.Dimension_9_G >= 4)
                    {
                        Dim_Colores[8] = Color_Muy_Alto;
                        Dim_Nivel[8] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_10_Global.Dimension_10_G >= 0 && ViewBag.Dim_10_Global.Dimension_10_G < 1)
                    {

                        Dim_Colores[9] = Color_Nulo;
                        Dim_Nivel[9] = "Nulo";
                    }
                    else if (ViewBag.Dim_10_Global.Dimension_10_G >= 1 && ViewBag.Dim_10_Global.Dimension_10_G < 2)
                    {
                        Dim_Colores[9] = Color_Bajo;
                        Dim_Nivel[9] = "Bajo";
                    }
                    else if (ViewBag.Dim_10_Global.Dimension_10_G >= 2 && ViewBag.Dim_10_Global.Dimension_10_G < 3)
                    {
                        Dim_Colores[9] = Color_Medio;
                        Dim_Nivel[9] = "Medio";

                    }
                    else if (ViewBag.Dim_10_Global.Dimension_10_G >= 3 && ViewBag.Dim_10_Global.Dimension_10_G < 4)
                    {
                        Dim_Colores[9] = Color_Alto;
                        Dim_Nivel[9] = "Alto";
                    }
                    else if (ViewBag.Dim_10_Global.Dimension_10_G >= 4)
                    {
                        Dim_Colores[9] = Color_Muy_Alto;
                        Dim_Nivel[9] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_11_Global.Dimension_11_G >= 0 && ViewBag.Dim_11_Global.Dimension_11_G < 1)
                    {

                        Dim_Colores[10] = Color_Nulo;
                        Dim_Nivel[10] = "Nulo";
                    }
                    else if (ViewBag.Dim_11_Global.Dimension_11_G >= 1 && ViewBag.Dim_11_Global.Dimension_11_G < 2)
                    {
                        Dim_Colores[10] = Color_Bajo;
                        Dim_Nivel[10] = "Bajo";
                    }
                    else if (ViewBag.Dim_11_Global.Dimension_11_G >= 2 && ViewBag.Dim_11_Global.Dimension_11_G < 3)
                    {
                        Dim_Colores[10] = Color_Medio;
                        Dim_Nivel[10] = "Medio";

                    }
                    else if (ViewBag.Dim_11_Global.Dimension_11_G >= 3 && ViewBag.Dim_11_Global.Dimension_11_G < 4)
                    {
                        Dim_Colores[10] = Color_Alto;
                        Dim_Nivel[10] = "Alto";
                    }
                    else if (ViewBag.Dim_11_Global.Dimension_11_G >= 4)
                    {
                        Dim_Colores[10] = Color_Muy_Alto;
                        Dim_Nivel[10] = "Muy Alto";
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_12_Global.Dimension_12_G >= 0 && ViewBag.Dim_12_Global.Dimension_12_G < 1)
                    {

                        Dim_Colores[11] = Color_Nulo;
                        Dim_Nivel[11] = "Nulo";
                    }
                    else if (ViewBag.Dim_12_Global.Dimension_12_G >= 1 && ViewBag.Dim_12_Global.Dimension_12_G < 2)
                    {
                        Dim_Colores[11] = Color_Bajo;
                        Dim_Nivel[11] = "Bajo";
                    }
                    else if (ViewBag.Dim_12_Global.Dimension_12_G >= 2 && ViewBag.Dim_12_Global.Dimension_12_G < 3)
                    {
                        Dim_Colores[11] = Color_Medio;
                        Dim_Nivel[11] = "Medio";

                    }
                    else if (ViewBag.Dim_12_Global.Dimension_12_G >= 3 && ViewBag.Dim_12_Global.Dimension_12_G < 4)
                    {
                        Dim_Colores[11] = Color_Alto;
                        Dim_Nivel[11] = "Alto";
                    }
                    else if (ViewBag.Dim_12_Global.Dimension_12_G >= 4)
                    {
                        Dim_Colores[11] = Color_Muy_Alto;
                        Dim_Nivel[11] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_13_Global.Dimension_13_G >= 0 && ViewBag.Dim_13_Global.Dimension_13_G < 1)
                    {

                        Dim_Colores[12] = Color_Nulo;
                        Dim_Nivel[12] = "Nulo";
                    }
                    else if (ViewBag.Dim_13_Global.Dimension_13_G >= 1 && ViewBag.Dim_13_Global.Dimension_13_G < 2)
                    {
                        Dim_Colores[12] = Color_Bajo;
                        Dim_Nivel[12] = "Bajo";
                    }
                    else if (ViewBag.Dim_13_Global.Dimension_13_G >= 2 && ViewBag.Dim_13_Global.Dimension_13_G < 3)
                    {
                        Dim_Colores[12] = Color_Medio;
                        Dim_Nivel[12] = "Medio";

                    }
                    else if (ViewBag.Dim_13_Global.Dimension_13_G >= 3 && ViewBag.Dim_13_Global.Dimension_13_G < 4)
                    {
                        Dim_Colores[12] = Color_Alto;
                        Dim_Nivel[12] = "Alto";
                    }
                    else if (ViewBag.Dim_13_Global.Dimension_13_G >= 4)
                    {
                        Dim_Colores[12] = Color_Muy_Alto;
                        Dim_Nivel[12] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_14_Global_NOM035.Dimension_14_G >= 0 && ViewBag.Dim_14_Global_NOM035.Dimension_14_G < 1)
                    {

                        Dim_Colores[13] = Color_Nulo;
                        Dim_Nivel[13] = "Nulo";
                        ViewBag.Dim_14_Global.Dimension_14_G = 0;
                    }
                    else if (ViewBag.Dim_14_Global_NOM035.Dimension_14_G >= 1 && ViewBag.Dim_14_Global_NOM035.Dimension_14_G < 2)
                    {
                        Dim_Colores[13] = Color_Bajo;
                        Dim_Nivel[13] = "Bajo";
                        ViewBag.Dim_14_Global.Dimension_14_G = 1;
                    }
                    else if (ViewBag.Dim_14_Global_NOM035.Dimension_14_G >= 2 && ViewBag.Dim_14_Global_NOM035.Dimension_14_G < 4)
                    {
                        Dim_Colores[13] = Color_Medio;
                        Dim_Nivel[13] = "Medio";
                        ViewBag.Dim_14_Global.Dimension_14_G = 2;

                    }
                    else if (ViewBag.Dim_14_Global_NOM035.Dimension_14_G >= 4 && ViewBag.Dim_14_Global_NOM035.Dimension_14_G < 6)
                    {
                        Dim_Colores[13] = Color_Alto;
                        Dim_Nivel[13] = "Alto";
                        ViewBag.Dim_14_Global.Dimension_14_G = 3;
                    }
                    else if (ViewBag.Dim_14_Global_NOM035.Dimension_14_G >= 6)
                    {
                        Dim_Colores[13] = Color_Muy_Alto;
                        Dim_Nivel[13] = "Muy Alto";
                        ViewBag.Dim_14_Global.Dimension_14_G = 4;
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_15_Global.Dimension_15_G >= 0 && ViewBag.Dim_15_Global.Dimension_15_G < 1)
                    {

                        Dim_Colores[14] = Color_Nulo;
                        Dim_Nivel[14] = "Nulo";
                    }
                    else if (ViewBag.Dim_15_Global.Dimension_15_G >= 1 && ViewBag.Dim_15_Global.Dimension_15_G < 2)
                    {
                        Dim_Colores[14] = Color_Bajo;
                        Dim_Nivel[14] = "Bajo";
                    }
                    else if (ViewBag.Dim_15_Global.Dimension_15_G >= 2 && ViewBag.Dim_15_Global.Dimension_15_G < 3)
                    {
                        Dim_Colores[14] = Color_Medio;
                        Dim_Nivel[14] = "Medio";

                    }
                    else if (ViewBag.Dim_15_Global.Dimension_15_G >= 3 && ViewBag.Dim_15_Global.Dimension_15_G < 4)
                    {
                        Dim_Colores[14] = Color_Alto;
                        Dim_Nivel[14] = "Alto";
                    }
                    else if (ViewBag.Dim_15_Global.Dimension_15_G >= 4)
                    {
                        Dim_Colores[14] = Color_Muy_Alto;
                        Dim_Nivel[14] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_16_Global.Dimension_16_G >= 0 && ViewBag.Dim_16_Global.Dimension_16_G < 1)
                    {

                        Dim_Colores[15] = Color_Nulo;
                        Dim_Nivel[15] = "Nulo";
                    }
                    else if (ViewBag.Dim_16_Global.Dimension_16_G >= 1 && ViewBag.Dim_16_Global.Dimension_16_G < 2)
                    {
                        Dim_Colores[15] = Color_Bajo;
                        Dim_Nivel[15] = "Bajo";
                    }
                    else if (ViewBag.Dim_16_Global.Dimension_16_G >= 2 && ViewBag.Dim_16_Global.Dimension_16_G < 3)
                    {
                        Dim_Colores[15] = Color_Medio;
                        Dim_Nivel[15] = "Medio";

                    }
                    else if (ViewBag.Dim_16_Global.Dimension_16_G >= 3 && ViewBag.Dim_16_Global.Dimension_16_G < 4)
                    {
                        Dim_Colores[15] = Color_Alto;
                        Dim_Nivel[15] = "Alto";
                    }
                    else if (ViewBag.Dim_16_Global.Dimension_16_G >= 4)
                    {
                        Dim_Colores[15] = Color_Muy_Alto;
                        Dim_Nivel[15] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_17_Global.Dimension_17_G >= 0 && ViewBag.Dim_17_Global.Dimension_17_G < 1)
                    {

                        Dim_Colores[16] = Color_Nulo;
                        Dim_Nivel[16] = "Nulo";
                    }
                    else if (ViewBag.Dim_17_Global.Dimension_17_G >= 1 && ViewBag.Dim_17_Global.Dimension_17_G < 2)
                    {
                        Dim_Colores[16] = Color_Bajo;
                        Dim_Nivel[16] = "Bajo";
                    }
                    else if (ViewBag.Dim_17_Global.Dimension_17_G >= 2 && ViewBag.Dim_17_Global.Dimension_17_G < 3)
                    {
                        Dim_Colores[16] = Color_Medio;
                        Dim_Nivel[16] = "Medio";

                    }
                    else if (ViewBag.Dim_17_Global.Dimension_17_G >= 3 && ViewBag.Dim_17_Global.Dimension_17_G < 4)
                    {
                        Dim_Colores[16] = Color_Alto;
                        Dim_Nivel[16] = "Alto";
                    }
                    else if (ViewBag.Dim_17_Global.Dimension_17_G >= 4)
                    {
                        Dim_Colores[16] = Color_Muy_Alto;
                        Dim_Nivel[16] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_18_Global.Dimension_18_G >= 0 && ViewBag.Dim_18_Global.Dimension_18_G < 1)
                    {

                        Dim_Colores[17] = Color_Nulo;
                        Dim_Nivel[17] = "Nulo";
                    }
                    else if (ViewBag.Dim_18_Global.Dimension_18_G >= 1 && ViewBag.Dim_18_Global.Dimension_18_G < 2)
                    {
                        Dim_Colores[17] = Color_Bajo;
                        Dim_Nivel[17] = "Bajo";
                    }
                    else if (ViewBag.Dim_18_Global.Dimension_18_G >= 2 && ViewBag.Dim_18_Global.Dimension_18_G < 3)
                    {
                        Dim_Colores[17] = Color_Medio;
                        Dim_Nivel[17] = "Medio";

                    }
                    else if (ViewBag.Dim_18_Global.Dimension_18_G >= 3 && ViewBag.Dim_18_Global.Dimension_18_G < 4)
                    {
                        Dim_Colores[17] = Color_Alto;
                        Dim_Nivel[17] = "Alto";
                    }
                    else if (ViewBag.Dim_18_Global.Dimension_18_G >= 4)
                    {
                        Dim_Colores[17] = Color_Muy_Alto;
                        Dim_Nivel[17] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_19_Global.Dimension_19_G >= 0 && ViewBag.Dim_19_Global.Dimension_19_G < 1)
                    {

                        Dim_Colores[18] = Color_Nulo;
                        Dim_Nivel[18] = "Nulo";
                    }
                    else if (ViewBag.Dim_19_Global.Dimension_19_G >= 1 && ViewBag.Dim_19_Global.Dimension_19_G < 2)
                    {
                        Dim_Colores[18] = Color_Bajo;
                        Dim_Nivel[18] = "Bajo";
                    }
                    else if (ViewBag.Dim_19_Global.Dimension_19_G >= 2 && ViewBag.Dim_19_Global.Dimension_19_G < 3)
                    {
                        Dim_Colores[18] = Color_Medio;
                        Dim_Nivel[18] = "Medio";

                    }
                    else if (ViewBag.Dim_19_Global.Dimension_19_G >= 3 && ViewBag.Dim_19_Global.Dimension_19_G < 4)
                    {
                        Dim_Colores[18] = Color_Alto;
                        Dim_Nivel[18] = "Alto";
                    }
                    else if (ViewBag.Dim_19_Global.Dimension_19_G >= 4)
                    {
                        Dim_Colores[18] = Color_Muy_Alto;
                        Dim_Nivel[18] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_20_Global.Dimension_20_G >= 0 && ViewBag.Dim_20_Global.Dimension_20_G < 1)
                    {

                        Dim_Colores[19] = Color_Nulo;
                        Dim_Nivel[19] = "Nulo";
                    }
                    else if (ViewBag.Dim_20_Global.Dimension_20_G >= 1 && ViewBag.Dim_20_Global.Dimension_20_G < 2)
                    {
                        Dim_Colores[19] = Color_Bajo;
                        Dim_Nivel[19] = "Bajo";
                    }
                    else if (ViewBag.Dim_20_Global.Dimension_20_G >= 2 && ViewBag.Dim_20_Global.Dimension_20_G < 3)
                    {
                        Dim_Colores[19] = Color_Medio;
                        Dim_Nivel[19] = "Medio";

                    }
                    else if (ViewBag.Dim_20_Global.Dimension_20_G >= 3 && ViewBag.Dim_20_Global.Dimension_20_G < 4)
                    {
                        Dim_Colores[19] = Color_Alto;
                        Dim_Nivel[19] = "Alto";
                    }
                    else if (ViewBag.Dim_20_Global.Dimension_20_G >= 4)
                    {
                        Dim_Colores[19] = Color_Muy_Alto;
                        Dim_Nivel[19] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_21_Global_NOM035.Dimension_21_G >= 0 && ViewBag.Dim_21_Global_NOM035.Dimension_21_G < 7)
                    {
                        Dim_Colores[20] = Color_Nulo;
                        Dim_Nivel[20] = "Nulo";
                        ViewBag.Dim_21_Global.Dimension_21_G = 0;
                    }
                    else if (ViewBag.Dim_21_Global_NOM035.Dimension_21_G >= 7 && ViewBag.Dim_21_Global_NOM035.Dimension_21_G < 10)
                    {
                        Dim_Colores[20] = Color_Bajo;
                        Dim_Nivel[20] = "Bajo";
                        ViewBag.Dim_21_Global.Dimension_21_G = 1;
                    }
                    else if (ViewBag.Dim_21_Global_NOM035.Dimension_21_G >= 10 && ViewBag.Dim_21_Global_NOM035.Dimension_21_G < 13)
                    {
                        Dim_Colores[20] = Color_Medio;
                        Dim_Nivel[20] = "Medio";
                        ViewBag.Dim_21_Global.Dimension_21_G = 2;

                    }
                    else if (ViewBag.Dim_21_Global_NOM035.Dimension_21_G >= 13 && ViewBag.Dim_21_Global_NOM035.Dimension_21_G < 16)
                    {
                        Dim_Colores[20] = Color_Alto;
                        Dim_Nivel[20] = "Alto";
                        ViewBag.Dim_21_Global.Dimension_21_G = 3;
                    }
                    else if (ViewBag.Dim_21_Global_NOM035.Dimension_21_G >= 16)
                    {
                        Dim_Colores[20] = Color_Muy_Alto;
                        Dim_Nivel[20] = "Muy Alto";
                        ViewBag.Dim_21_Global.Dimension_21_G = 4;
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_22_Global.Dimension_22_G >= 0 && ViewBag.Dim_22_Global.Dimension_22_G < 1)
                    {

                        Dim_Colores[21] = Color_Nulo;
                        Dim_Nivel[21] = "Nulo";
                    }
                    else if (ViewBag.Dim_22_Global.Dimension_22_G >= 1 && ViewBag.Dim_22_Global.Dimension_22_G < 2)
                    {
                        Dim_Colores[21] = Color_Bajo;
                        Dim_Nivel[21] = "Bajo";
                    }
                    else if (ViewBag.Dim_22_Global.Dimension_22_G >= 2 && ViewBag.Dim_22_Global.Dimension_22_G < 3)
                    {
                        Dim_Colores[21] = Color_Medio;
                        Dim_Nivel[21] = "Medio";

                    }
                    else if (ViewBag.Dim_22_Global.Dimension_22_G >= 3 && ViewBag.Dim_22_Global.Dimension_22_G < 4)
                    {
                        Dim_Colores[21] = Color_Alto;
                        Dim_Nivel[21] = "Alto";
                    }
                    else if (ViewBag.Dim_22_Global.Dimension_22_G >= 4)
                    {
                        Dim_Colores[21] = Color_Muy_Alto;
                        Dim_Nivel[21] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_23_Global.Dimension_23_G >= 0 && ViewBag.Dim_23_Global.Dimension_23_G < 1)
                    {

                        Dim_Colores[22] = Color_Nulo;
                        Dim_Nivel[22] = "Nulo";
                    }
                    else if (ViewBag.Dim_23_Global.Dimension_23_G >= 1 && ViewBag.Dim_23_Global.Dimension_23_G < 2)
                    {
                        Dim_Colores[22] = Color_Bajo;
                        Dim_Nivel[22] = "Bajo";
                    }
                    else if (ViewBag.Dim_23_Global.Dimension_23_G >= 2 && ViewBag.Dim_23_Global.Dimension_23_G < 3)
                    {
                        Dim_Colores[22] = Color_Medio;
                        Dim_Nivel[22] = "Medio";

                    }
                    else if (ViewBag.Dim_23_Global.Dimension_23_G >= 3 && ViewBag.Dim_23_Global.Dimension_23_G < 4)
                    {
                        Dim_Colores[22] = Color_Alto;
                        Dim_Nivel[22] = "Alto";
                    }
                    else if (ViewBag.Dim_23_Global.Dimension_23_G >= 4)
                    {
                        Dim_Colores[22] = Color_Muy_Alto;
                        Dim_Nivel[22] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_24_Global.Dimension_24_G >= 0 && ViewBag.Dim_24_Global.Dimension_24_G < 1)
                    {

                        Dim_Colores[23] = Color_Nulo;
                        Dim_Nivel[23] = "Nulo";
                    }
                    else if (ViewBag.Dim_24_Global.Dimension_24_G >= 1 && ViewBag.Dim_24_Global.Dimension_24_G < 2)
                    {
                        Dim_Colores[23] = Color_Bajo;
                        Dim_Nivel[23] = "Bajo";
                    }
                    else if (ViewBag.Dim_24_Global.Dimension_24_G >= 2 && ViewBag.Dim_24_Global.Dimension_24_G < 3)
                    {
                        Dim_Colores[23] = Color_Medio;
                        Dim_Nivel[23] = "Medio";

                    }
                    else if (ViewBag.Dim_24_Global.Dimension_24_G >= 3 && ViewBag.Dim_24_Global.Dimension_24_G < 4)
                    {
                        Dim_Colores[23] = Color_Alto;
                        Dim_Nivel[23] = "Alto";
                    }
                    else if (ViewBag.Dim_24_Global.Dimension_24_G >= 4)
                    {
                        Dim_Colores[23] = Color_Muy_Alto;
                        Dim_Nivel[23] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_25_Global.Dimension_25_G >= 0 && ViewBag.Dim_25_Global.Dimension_25_G < 1)
                    {

                        Dim_Colores[24] = Color_Nulo;
                        Dim_Nivel[24] = "Nulo";
                    }
                    else if (ViewBag.Dim_25_Global.Dimension_25_G >= 1 && ViewBag.Dim_25_Global.Dimension_25_G < 2)
                    {
                        Dim_Colores[24] = Color_Bajo;
                        Dim_Nivel[24] = "Bajo";
                    }
                    else if (ViewBag.Dim_25_Global.Dimension_25_G >= 2 && ViewBag.Dim_25_Global.Dimension_25_G < 3)
                    {
                        Dim_Colores[24] = Color_Medio;
                        Dim_Nivel[24] = "Medio";

                    }
                    else if (ViewBag.Dim_25_Global.Dimension_25_G >= 3 && ViewBag.Dim_25_Global.Dimension_25_G < 4)
                    {
                        Dim_Colores[24] = Color_Alto;
                        Dim_Nivel[24] = "Alto";
                    }
                    else if (ViewBag.Dim_25_Global.Dimension_25_G >= 4)
                    {
                        Dim_Colores[24] = Color_Muy_Alto;
                        Dim_Nivel[24] = "Muy Alto";
                    }
                    ViewBag.Colores_Dim = Dim_Colores;
                    ViewBag.Nivel_Dim = Dim_Nivel;


                }
                else if (id_encuesta == 2)
                {
                    ViewBag.Total_Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CR.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_encuesta == 2 && CT.id_empresa == id
                                             group CR by CR.id_pregunta into g
                                             orderby g.Sum(X => X.id_pregunta)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = g.Sum(X => (int?)X.Calificacion ?? 0)
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = G.Total_Categoria_1,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_I
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = G.Total_Categoria_2,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = G.Total_Categoria_3,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = G.Total_Categoria_4,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();


                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                SUMATORIA = 0
                                            }).FirstOrDefault();


                    //Definiendo COLORES DE CATEGORIAS   NULO - BAJO - MEDIO -ALTO -MUY ALTO

                    if (ViewBag.Cat_1_Global.Categoria_1_G < 3)
                    {
                        Cat_Colores[0] = Color_Nulo;
                        Cat_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 3 && ViewBag.Cat_1_Global.Categoria_1_G < 5)
                    {
                        Cat_Colores[0] = Color_Bajo;
                        Cat_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 5 && ViewBag.Cat_1_Global.Categoria_1_G < 7)
                    {
                        Cat_Colores[0] = Color_Medio;
                        Cat_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 7 && ViewBag.Cat_1_Global.Categoria_1_G < 9)
                    {
                        Cat_Colores[0] = Color_Alto;
                        Cat_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 9)
                    {
                        Cat_Colores[0] = Color_Muy_Alto;
                        Cat_Nivel[0] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_2_Global.Categoria_2_G < 10)
                    {
                        Cat_Colores[1] = Color_Nulo;
                        Cat_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 10 && ViewBag.Cat_2_Global.Categoria_2_G < 20)
                    {
                        Cat_Colores[1] = Color_Bajo;
                        Cat_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 20 && ViewBag.Cat_2_Global.Categoria_2_G < 30)
                    {
                        Cat_Colores[1] = Color_Medio;
                        Cat_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 30 && ViewBag.Cat_2_Global.Categoria_2_G < 40)
                    {
                        Cat_Colores[1] = Color_Alto;
                        Cat_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 40)
                    {
                        Cat_Colores[1] = Color_Muy_Alto;
                        Cat_Nivel[1] = "Muy Alto";
                    }

                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_3_Global.Categoria_3_G < 4)
                    {
                        Cat_Colores[2] = Color_Nulo;
                        Cat_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 4 && ViewBag.Cat_3_Global.Categoria_3_G < 6)
                    {
                        Cat_Colores[2] = Color_Bajo;
                        Cat_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 6 && ViewBag.Cat_3_Global.Categoria_3_G < 9)
                    {
                        Cat_Colores[2] = Color_Medio;
                        Cat_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 9 && ViewBag.Cat_3_Global.Categoria_3_G < 12)
                    {
                        Cat_Colores[2] = Color_Alto;
                        Cat_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 12)
                    {
                        Cat_Colores[2] = Color_Muy_Alto;
                        Cat_Nivel[2] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_4_Global.Categoria_4_G < 10)
                    {
                        Cat_Colores[3] = Color_Nulo;
                        Cat_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 10 && ViewBag.Cat_4_Global.Categoria_4_G < 18)
                    {
                        Cat_Colores[3] = Color_Bajo;
                        Cat_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 18 && ViewBag.Cat_4_Global.Categoria_4_G < 28)
                    {
                        Cat_Colores[3] = Color_Medio;
                        Cat_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 28 && ViewBag.Cat_4_Global.Categoria_4_G < 38)
                    {
                        Cat_Colores[3] = Color_Alto;
                        Cat_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 38)
                    {
                        Cat_Colores[3] = Color_Muy_Alto;
                        Cat_Nivel[3] = "Muy Alto";
                    }

                    //////////////////////////////////////////NO HAY CATEGORIA 5 EN ENCUESTA II ////////////////////////////////////////////


                    ViewBag.Colores = Cat_Colores;
                    ViewBag.Nivel = Cat_Nivel;


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_8_G = G.Total_Dominio_8
                                            }).FirstOrDefault();
                    ViewBag.Dom_9_Global = 0;
                    ViewBag.Dom_10_Global = 0;

                    //-------------------------------------------------------------------------------------
                    //          Definiendo COLORES DE DOMINIOS   NULO - BAJO - MEDIO -ALTO -MUY ALTO
                    //-------------------------------------------------------------------------------------
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_1_Global.Dominio_1_G < 3)
                    {

                        Dom_Colores[0] = Color_Nulo;
                        Dom_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 3 && ViewBag.Dom_1_Global.Dominio_1_G < 5)
                    {
                        Dom_Colores[0] = Color_Bajo;
                        Dom_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 5 && ViewBag.Dom_1_Global.Dominio_1_G < 7)
                    {
                        Dom_Colores[0] = Color_Medio;
                        Dom_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 7 && ViewBag.Dom_1_Global.Dominio_1_G < 9)
                    {
                        Dom_Colores[0] = Color_Alto;
                        Dom_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 9)
                    {
                        Dom_Colores[0] = Color_Muy_Alto;
                        Dom_Nivel[0] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_2_Global.Dominio_2_G < 12)
                    {
                        Dom_Colores[1] = Color_Nulo;
                        Dom_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 12 && ViewBag.Dom_2_Global.Dominio_2_G < 16)
                    {
                        Dom_Colores[1] = Color_Bajo;
                        Dom_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 16 && ViewBag.Dom_2_Global.Dominio_2_G < 20)
                    {
                        Dom_Colores[1] = Color_Medio;
                        Dom_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 20 && ViewBag.Dom_2_Global.Dominio_2_G < 24)
                    {
                        Dom_Colores[1] = Color_Alto;
                        Dom_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 24)
                    {
                        Dom_Colores[1] = Color_Muy_Alto;
                        Dom_Nivel[1] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_3_Global.Dominio_3_G < 5)
                    {
                        Dom_Colores[2] = Color_Nulo;
                        Dom_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 5 && ViewBag.Dom_3_Global.Dominio_3_G < 8)
                    {
                        Dom_Colores[2] = Color_Bajo;
                        Dom_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 8 && ViewBag.Dom_3_Global.Dominio_3_G < 11)
                    {
                        Dom_Colores[2] = Color_Medio;
                        Dom_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 11 && ViewBag.Dom_3_Global.Dominio_3_G < 14)
                    {
                        Dom_Colores[2] = Color_Alto;
                        Dom_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 14)
                    {
                        Dom_Colores[2] = Color_Muy_Alto;
                        Dom_Nivel[2] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_4_Global.Dominio_4_G < 1)
                    {
                        Dom_Colores[3] = Color_Nulo;
                        Dom_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 1 && ViewBag.Dom_4_Global.Dominio_4_G < 2)
                    {
                        Dom_Colores[3] = Color_Bajo;
                        Dom_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 2 && ViewBag.Dom_4_Global.Dominio_4_G < 4)
                    {
                        Dom_Colores[3] = Color_Medio;
                        Dom_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 4 && ViewBag.Dom_4_Global.Dominio_4_G < 6)
                    {
                        Dom_Colores[3] = Color_Alto;
                        Dom_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 6)
                    {
                        Dom_Colores[3] = Color_Muy_Alto;
                        Dom_Nivel[3] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_5_Global.Dominio_5_G < 1)
                    {
                        Dom_Colores[4] = Color_Nulo;
                        Dom_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 1 && ViewBag.Dom_5_Global.Dominio_5_G < 2)
                    {
                        Dom_Colores[4] = Color_Bajo;
                        Dom_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 2 && ViewBag.Dom_5_Global.Dominio_5_G < 4)
                    {
                        Dom_Colores[4] = Color_Medio;
                        Dom_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 4 && ViewBag.Dom_5_Global.Dominio_5_G < 6)
                    {
                        Dom_Colores[4] = Color_Alto;
                        Dom_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 6)
                    {
                        Dom_Colores[4] = Color_Muy_Alto;
                        Dom_Nivel[4] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_6_Global.Dominio_6_G < 3)
                    {
                        Dom_Colores[5] = Color_Nulo;
                        Dom_Nivel[5] = "Nulo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 3 && ViewBag.Dom_6_Global.Dominio_6_G < 5)
                    {
                        Dom_Colores[5] = Color_Bajo;
                        Dom_Nivel[5] = "Bajo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 5 && ViewBag.Dom_6_Global.Dominio_6_G < 8)
                    {
                        Dom_Colores[5] = Color_Medio;
                        Dom_Nivel[5] = "Medio";

                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 8 && ViewBag.Dom_6_Global.Dominio_6_G < 11)
                    {
                        Dom_Colores[5] = Color_Alto;
                        Dom_Nivel[5] = "Alto";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 11)
                    {
                        Dom_Colores[5] = Color_Muy_Alto;
                        Dom_Nivel[5] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_7_Global.Dominio_7_G < 5)
                    {
                        Dom_Colores[6] = Color_Nulo;
                        Dom_Nivel[6] = "Nulo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 5 && ViewBag.Dom_7_Global.Dominio_7_G < 8)
                    {
                        Dom_Colores[6] = Color_Bajo;
                        Dom_Nivel[6] = "Bajo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 8 && ViewBag.Dom_7_Global.Dominio_7_G < 11)
                    {
                        Dom_Colores[6] = Color_Medio;
                        Dom_Nivel[6] = "Medio";

                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 11 && ViewBag.Dom_7_Global.Dominio_7_G < 14)
                    {
                        Dom_Colores[6] = Color_Alto;
                        Dom_Nivel[6] = "Alto";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 14)
                    {
                        Dom_Colores[6] = Color_Muy_Alto;
                        Dom_Nivel[6] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_8_Global.Dominio_8_G < 7)
                    {
                        Dom_Colores[7] = Color_Nulo;
                        Dom_Nivel[7] = "Nulo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 7 && ViewBag.Dom_8_Global.Dominio_8_G < 10)
                    {
                        Dom_Colores[7] = Color_Bajo;
                        Dom_Nivel[7] = "Bajo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 10 && ViewBag.Dom_8_Global.Dominio_8_G < 13)
                    {
                        Dom_Colores[7] = Color_Medio;
                        Dom_Nivel[7] = "Medio";

                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 13 && ViewBag.Dom_8_Global.Dominio_8_G < 16)
                    {
                        Dom_Colores[7] = Color_Alto;
                        Dom_Nivel[7] = "Alto";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 16)
                    {
                        Dom_Colores[7] = Color_Muy_Alto;
                        Dom_Nivel[7] = "Muy Alto";
                    }



                    ViewBag.Colores_Dom = Dom_Colores;
                    ViewBag.Nivel_Dom = Dom_Nivel;


                    //##############################################################################################################################
                    // ######################################################  INICIO CALCULO Dimension #############################################
                    //##############################################################################################################################

                    ViewBag.Dim_E2_1_Global = (from G in db.fnDemo_N035_Dimension_1_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_1_G = G.Total_Dimension_1
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_2_Global = (from G in db.fnDemo_N035_Dimension_2_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_2_G = G.Total_Dimension_2
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_3_Global = (from G in db.fnDemo_N035_Dimension_3_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_3_G = G.Total_Dimension_3
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_4_Global = (from G in db.fnDemo_N035_Dimension_4_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_4_G = G.Total_Dimension_4 / 2
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_5_Global = (from G in db.fnDemo_N035_Dimension_5_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_5_G = G.Total_Dimension_5 / 2
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_6_Global = (from G in db.fnDemo_N035_Dimension_6_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_6_G = G.Total_Dimension_6 / 2
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_7_Global = (from G in db.fnDemo_N035_Dimension_7_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_7_G = G.Total_Dimension_7 / 3
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_8_Global = (from G in db.fnDemo_N035_Dimension_8_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_8_G = G.Total_Dimension_8 / 2
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_9_Global = (from G in db.fnDemo_N035_Dimension_9_E2_Resultados(id)
                                               select new Respuestas
                                               {
                                                   Dimension_9_G = G.Total_Dimension_9 / 2
                                               }).FirstOrDefault();
                    ViewBag.Dim_E2_10_Global = (from G in db.fnDemo_N035_Dimension_10_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_10_G = G.Total_Dimension_10 / 3
                                                }).FirstOrDefault();
                    ViewBag.Dim_E2_11_Global = (from G in db.fnDemo_N035_Dimension_11_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_11_G = G.Total_Dimension_11 / 2
                                                }).FirstOrDefault();
                    ViewBag.Dim_E2_12_Global = (from G in db.fnDemo_N035_Dimension_12_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_12_G = G.Total_Dimension_12 / 2
                                                }).FirstOrDefault();
                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_E2_13_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_13_E2_Resultados(id)
                                                       select new Respuestas
                                                       {
                                                           Dimension_13_G = G.Total_Dimension_13
                                                       }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_E2_13_Global = (from G in db.fnDemo_N035_Dimension_13_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_13_G = G.Total_Dimension_13 / 2
                                                }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_E2_14_Global = (from G in db.fnDemo_N035_Dimension_14_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_14_G = G.Total_Dimension_14
                                                }).FirstOrDefault();

                    ViewBag.Dim_E2_15_Global = (from G in db.fnDemo_N035_Dimension_15_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_15_G = G.Total_Dimension_15
                                                }).FirstOrDefault();
                    ViewBag.Dim_E2_16_Global = (from G in db.fnDemo_N035_Dimension_16_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_16_G = G.Total_Dimension_16 / 3
                                                }).FirstOrDefault();
                    ViewBag.Dim_E2_17_Global = (from G in db.fnDemo_N035_Dimension_17_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_17_G = G.Total_Dimension_17 / 2
                                                }).FirstOrDefault();
                    ViewBag.Dim_E2_18_Global = (from G in db.fnDemo_N035_Dimension_18_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_18_G = G.Total_Dimension_18 / 3
                                                }).FirstOrDefault();
                    ViewBag.Dim_E2_19_Global = (from G in db.fnDemo_N035_Dimension_19_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_19_G = G.Total_Dimension_19 / 3
                                                }).FirstOrDefault();


                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_E2_20_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_20_E2_Resultados(id)
                                                       select new Respuestas
                                                       {
                                                           Dimension_20_G = G.Total_Dimension_20
                                                       }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_E2_20_Global = (from G in db.fnDemo_N035_Dimension_20_E2_Resultados(id)
                                                select new Respuestas
                                                {
                                                    Dimension_20_G = G.Total_Dimension_20 / 8
                                                }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################



                    //-------------------------------------------------------------------------------------
                    //          Definiendo COLORES DE Dimension   NULO - BAJO - MEDIO -ALTO -MUY ALTO
                    //-------------------------------------------------------------------------------------
                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_1_Global.Dimension_1_G >= 0 && ViewBag.Dim_E2_1_Global.Dimension_1_G < 1)
                    {

                        Dim_Colores[0] = Color_Nulo;
                        Dim_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_1_Global.Dimension_1_G >= 1 && ViewBag.Dim_E2_1_Global.Dimension_1_G < 2)
                    {
                        Dim_Colores[0] = Color_Bajo;
                        Dim_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_1_Global.Dimension_1_G >= 2 && ViewBag.Dim_E2_1_Global.Dimension_1_G < 3)
                    {
                        Dim_Colores[0] = Color_Medio;
                        Dim_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_1_Global.Dimension_1_G >= 3 && ViewBag.Dim_E2_1_Global.Dimension_1_G < 4)
                    {
                        Dim_Colores[0] = Color_Alto;
                        Dim_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_1_Global.Dimension_1_G >= 4)
                    {
                        Dim_Colores[0] = Color_Muy_Alto;
                        Dim_Nivel[0] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////#
                    if (ViewBag.Dim_E2_2_Global.Dimension_2_G >= 0 && ViewBag.Dim_E2_2_Global.Dimension_2_G < 1)
                    {

                        Dim_Colores[1] = Color_Nulo;
                        Dim_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_2_Global.Dimension_2_G >= 1 && ViewBag.Dim_E2_2_Global.Dimension_2_G < 2)
                    {
                        Dim_Colores[1] = Color_Bajo;
                        Dim_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_2_Global.Dimension_2_G >= 2 && ViewBag.Dim_E2_2_Global.Dimension_2_G < 3)
                    {
                        Dim_Colores[1] = Color_Medio;
                        Dim_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_2_Global.Dimension_2_G >= 3 && ViewBag.Dim_E2_2_Global.Dimension_2_G < 4)
                    {
                        Dim_Colores[1] = Color_Alto;
                        Dim_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_2_Global.Dimension_2_G >= 4)
                    {
                        Dim_Colores[1] = Color_Muy_Alto;
                        Dim_Nivel[1] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_3_Global.Dimension_3_G >= 0 && ViewBag.Dim_E2_3_Global.Dimension_3_G < 1)
                    {

                        Dim_Colores[2] = Color_Nulo;
                        Dim_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_3_Global.Dimension_3_G >= 1 && ViewBag.Dim_E2_3_Global.Dimension_3_G < 2)
                    {
                        Dim_Colores[2] = Color_Bajo;
                        Dim_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_3_Global.Dimension_3_G >= 2 && ViewBag.Dim_E2_3_Global.Dimension_3_G < 3)
                    {
                        Dim_Colores[2] = Color_Medio;
                        Dim_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_3_Global.Dimension_3_G >= 3 && ViewBag.Dim_E2_3_Global.Dimension_3_G < 4)
                    {
                        Dim_Colores[2] = Color_Alto;
                        Dim_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_3_Global.Dimension_3_G >= 4)
                    {
                        Dim_Colores[2] = Color_Muy_Alto;
                        Dim_Nivel[2] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_4_Global.Dimension_4_G >= 0 && ViewBag.Dim_E2_4_Global.Dimension_4_G < 1)
                    {

                        Dim_Colores[3] = Color_Nulo;
                        Dim_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_4_Global.Dimension_4_G >= 1 && ViewBag.Dim_E2_4_Global.Dimension_4_G < 2)
                    {
                        Dim_Colores[3] = Color_Bajo;
                        Dim_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_4_Global.Dimension_4_G >= 2 && ViewBag.Dim_E2_4_Global.Dimension_4_G < 3)
                    {
                        Dim_Colores[3] = Color_Medio;
                        Dim_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_4_Global.Dimension_4_G >= 3 && ViewBag.Dim_E2_4_Global.Dimension_4_G < 4)
                    {
                        Dim_Colores[3] = Color_Alto;
                        Dim_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_4_Global.Dimension_4_G >= 4)
                    {
                        Dim_Colores[3] = Color_Muy_Alto;
                        Dim_Nivel[3] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_5_Global.Dimension_5_G >= 0 && ViewBag.Dim_E2_5_Global.Dimension_5_G < 1)
                    {

                        Dim_Colores[4] = Color_Nulo;
                        Dim_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_5_Global.Dimension_5_G >= 1 && ViewBag.Dim_E2_5_Global.Dimension_5_G < 2)
                    {
                        Dim_Colores[4] = Color_Bajo;
                        Dim_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_5_Global.Dimension_5_G >= 2 && ViewBag.Dim_E2_5_Global.Dimension_5_G < 3)
                    {
                        Dim_Colores[4] = Color_Medio;
                        Dim_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_5_Global.Dimension_5_G >= 3 && ViewBag.Dim_E2_5_Global.Dimension_5_G < 4)
                    {
                        Dim_Colores[4] = Color_Alto;
                        Dim_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_5_Global.Dimension_5_G >= 4)
                    {
                        Dim_Colores[4] = Color_Muy_Alto;
                        Dim_Nivel[4] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_6_Global.Dimension_6_G >= 0 && ViewBag.Dim_E2_6_Global.Dimension_6_G < 1)
                    {

                        Dim_Colores[5] = Color_Nulo;
                        Dim_Nivel[5] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_6_Global.Dimension_6_G >= 1 && ViewBag.Dim_E2_6_Global.Dimension_6_G < 2)
                    {
                        Dim_Colores[5] = Color_Bajo;
                        Dim_Nivel[5] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_6_Global.Dimension_6_G >= 2 && ViewBag.Dim_E2_6_Global.Dimension_6_G < 3)
                    {
                        Dim_Colores[5] = Color_Medio;
                        Dim_Nivel[5] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_6_Global.Dimension_6_G >= 3 && ViewBag.Dim_E2_6_Global.Dimension_6_G < 4)
                    {
                        Dim_Colores[5] = Color_Alto;
                        Dim_Nivel[5] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_6_Global.Dimension_6_G >= 4)
                    {
                        Dim_Colores[5] = Color_Muy_Alto;
                        Dim_Nivel[5] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_7_Global.Dimension_7_G >= 0 && ViewBag.Dim_E2_7_Global.Dimension_7_G < 1)
                    {

                        Dim_Colores[6] = Color_Nulo;
                        Dim_Nivel[6] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_7_Global.Dimension_7_G >= 1 && ViewBag.Dim_E2_7_Global.Dimension_7_G < 2)
                    {
                        Dim_Colores[6] = Color_Bajo;
                        Dim_Nivel[6] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_7_Global.Dimension_7_G >= 2 && ViewBag.Dim_E2_7_Global.Dimension_7_G < 3)
                    {
                        Dim_Colores[6] = Color_Medio;
                        Dim_Nivel[6] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_7_Global.Dimension_7_G >= 3 && ViewBag.Dim_E2_7_Global.Dimension_7_G < 4)
                    {
                        Dim_Colores[6] = Color_Alto;
                        Dim_Nivel[6] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_7_Global.Dimension_7_G >= 4)
                    {
                        Dim_Colores[6] = Color_Muy_Alto;
                        Dim_Nivel[6] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_8_Global.Dimension_8_G >= 0 && ViewBag.Dim_E2_8_Global.Dimension_8_G < 1)
                    {

                        Dim_Colores[7] = Color_Nulo;
                        Dim_Nivel[7] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_8_Global.Dimension_8_G >= 1 && ViewBag.Dim_E2_8_Global.Dimension_8_G < 2)
                    {
                        Dim_Colores[7] = Color_Bajo;
                        Dim_Nivel[7] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_8_Global.Dimension_8_G >= 2 && ViewBag.Dim_E2_8_Global.Dimension_8_G < 3)
                    {
                        Dim_Colores[7] = Color_Medio;
                        Dim_Nivel[7] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_8_Global.Dimension_8_G >= 3 && ViewBag.Dim_E2_8_Global.Dimension_8_G < 4)
                    {
                        Dim_Colores[7] = Color_Alto;
                        Dim_Nivel[7] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_8_Global.Dimension_8_G >= 4)
                    {
                        Dim_Colores[7] = Color_Muy_Alto;
                        Dim_Nivel[7] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_9_Global.Dimension_9_G >= 0 && ViewBag.Dim_E2_9_Global.Dimension_9_G < 1)
                    {

                        Dim_Colores[8] = Color_Nulo;
                        Dim_Nivel[8] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_9_Global.Dimension_9_G >= 1 && ViewBag.Dim_E2_9_Global.Dimension_9_G < 2)
                    {
                        Dim_Colores[8] = Color_Bajo;
                        Dim_Nivel[8] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_9_Global.Dimension_9_G >= 2 && ViewBag.Dim_E2_9_Global.Dimension_9_G < 3)
                    {
                        Dim_Colores[8] = Color_Medio;
                        Dim_Nivel[8] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_9_Global.Dimension_9_G >= 3 && ViewBag.Dim_E2_9_Global.Dimension_9_G < 4)
                    {
                        Dim_Colores[8] = Color_Alto;
                        Dim_Nivel[8] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_9_Global.Dimension_9_G >= 4)
                    {
                        Dim_Colores[8] = Color_Muy_Alto;
                        Dim_Nivel[8] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_10_Global.Dimension_10_G >= 0 && ViewBag.Dim_E2_10_Global.Dimension_10_G < 1)
                    {

                        Dim_Colores[9] = Color_Nulo;
                        Dim_Nivel[9] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_10_Global.Dimension_10_G >= 1 && ViewBag.Dim_E2_10_Global.Dimension_10_G < 2)
                    {
                        Dim_Colores[9] = Color_Bajo;
                        Dim_Nivel[9] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_10_Global.Dimension_10_G >= 2 && ViewBag.Dim_E2_10_Global.Dimension_10_G < 3)
                    {
                        Dim_Colores[9] = Color_Medio;
                        Dim_Nivel[9] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_10_Global.Dimension_10_G >= 3 && ViewBag.Dim_E2_10_Global.Dimension_10_G < 4)
                    {
                        Dim_Colores[9] = Color_Alto;
                        Dim_Nivel[9] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_10_Global.Dimension_10_G >= 4)
                    {
                        Dim_Colores[9] = Color_Muy_Alto;
                        Dim_Nivel[9] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dim_E2_11_Global.Dimension_11_G >= 0 && ViewBag.Dim_E2_11_Global.Dimension_11_G < 1)
                    {

                        Dim_Colores[10] = Color_Nulo;
                        Dim_Nivel[10] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_11_Global.Dimension_11_G >= 1 && ViewBag.Dim_E2_11_Global.Dimension_11_G < 2)
                    {
                        Dim_Colores[10] = Color_Bajo;
                        Dim_Nivel[10] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_11_Global.Dimension_11_G >= 2 && ViewBag.Dim_E2_11_Global.Dimension_11_G < 3)
                    {
                        Dim_Colores[10] = Color_Medio;
                        Dim_Nivel[10] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_11_Global.Dimension_11_G >= 3 && ViewBag.Dim_E2_11_Global.Dimension_11_G < 4)
                    {
                        Dim_Colores[10] = Color_Alto;
                        Dim_Nivel[10] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_11_Global.Dimension_11_G >= 4)
                    {
                        Dim_Colores[10] = Color_Muy_Alto;
                        Dim_Nivel[10] = "Muy Alto";
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_12_Global.Dimension_12_G >= 0 && ViewBag.Dim_E2_12_Global.Dimension_12_G < 1)
                    {

                        Dim_Colores[11] = Color_Nulo;
                        Dim_Nivel[11] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_12_Global.Dimension_12_G >= 1 && ViewBag.Dim_E2_12_Global.Dimension_12_G < 2)
                    {
                        Dim_Colores[11] = Color_Bajo;
                        Dim_Nivel[11] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_12_Global.Dimension_12_G >= 2 && ViewBag.Dim_E2_12_Global.Dimension_12_G < 3)
                    {
                        Dim_Colores[11] = Color_Medio;
                        Dim_Nivel[11] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_12_Global.Dimension_12_G >= 3 && ViewBag.Dim_E2_12_Global.Dimension_12_G < 4)
                    {
                        Dim_Colores[11] = Color_Alto;
                        Dim_Nivel[11] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_12_Global.Dimension_12_G >= 4)
                    {
                        Dim_Colores[11] = Color_Muy_Alto;
                        Dim_Nivel[11] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G >= 0 && ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G < 1)
                    {

                        Dim_Colores[12] = Color_Nulo;
                        Dim_Nivel[12] = "Nulo";
                        ViewBag.Dim_E2_13_Global.Dimension_13_G = 0;
                    }
                    else if (ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G >= 1 && ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G < 2)
                    {
                        Dim_Colores[12] = Color_Bajo;
                        Dim_Nivel[12] = "Bajo";
                        ViewBag.Dim_E2_13_Global.Dimension_13_G = 1;
                    }
                    else if (ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G >= 2 && ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G < 4)
                    {
                        Dim_Colores[12] = Color_Medio;
                        Dim_Nivel[12] = "Medio";
                        ViewBag.Dim_E2_13_Global.Dimension_13_G = 2;

                    }
                    else if (ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G >= 4 && ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G < 6)
                    {
                        Dim_Colores[12] = Color_Alto;
                        Dim_Nivel[12] = "Alto";
                        ViewBag.Dim_E2_13_Global.Dimension_13_G = 3;
                    }
                    else if (ViewBag.Dim_E2_13_Global_NOM035.Dimension_13_G >= 6)
                    {
                        Dim_Colores[12] = Color_Muy_Alto;
                        Dim_Nivel[12] = "Muy Alto";
                        ViewBag.Dim_E2_13_Global.Dimension_13_G = 4;
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////// 

                    if (ViewBag.Dim_E2_14_Global.Dimension_14_G >= 0 && ViewBag.Dim_E2_14_Global.Dimension_14_G < 1)
                    {

                        Dim_Colores[13] = Color_Nulo;
                        Dim_Nivel[13] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_14_Global.Dimension_14_G >= 1 && ViewBag.Dim_E2_14_Global.Dimension_14_G < 2)
                    {
                        Dim_Colores[13] = Color_Bajo;
                        Dim_Nivel[13] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_14_Global.Dimension_14_G >= 2 && ViewBag.Dim_E2_14_Global.Dimension_14_G < 3)
                    {
                        Dim_Colores[13] = Color_Medio;
                        Dim_Nivel[13] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_14_Global.Dimension_14_G >= 3 && ViewBag.Dim_E2_14_Global.Dimension_14_G < 4)
                    {
                        Dim_Colores[13] = Color_Alto;
                        Dim_Nivel[13] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_14_Global.Dimension_14_G >= 4)
                    {
                        Dim_Colores[13] = Color_Muy_Alto;
                        Dim_Nivel[13] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_15_Global.Dimension_15_G >= 0 && ViewBag.Dim_E2_15_Global.Dimension_15_G < 1)
                    {

                        Dim_Colores[14] = Color_Nulo;
                        Dim_Nivel[14] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_15_Global.Dimension_15_G >= 1 && ViewBag.Dim_E2_15_Global.Dimension_15_G < 2)
                    {
                        Dim_Colores[14] = Color_Bajo;
                        Dim_Nivel[14] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_15_Global.Dimension_15_G >= 2 && ViewBag.Dim_E2_15_Global.Dimension_15_G < 3)
                    {
                        Dim_Colores[14] = Color_Medio;
                        Dim_Nivel[14] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_15_Global.Dimension_15_G >= 3 && ViewBag.Dim_E2_15_Global.Dimension_15_G < 4)
                    {
                        Dim_Colores[14] = Color_Alto;
                        Dim_Nivel[14] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_15_Global.Dimension_15_G >= 4)
                    {
                        Dim_Colores[14] = Color_Muy_Alto;
                        Dim_Nivel[14] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_16_Global.Dimension_16_G >= 0 && ViewBag.Dim_E2_16_Global.Dimension_16_G < 1)
                    {

                        Dim_Colores[15] = Color_Nulo;
                        Dim_Nivel[15] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_16_Global.Dimension_16_G >= 1 && ViewBag.Dim_E2_16_Global.Dimension_16_G < 2)
                    {
                        Dim_Colores[15] = Color_Bajo;
                        Dim_Nivel[15] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_16_Global.Dimension_16_G >= 2 && ViewBag.Dim_E2_16_Global.Dimension_16_G < 3)
                    {
                        Dim_Colores[15] = Color_Medio;
                        Dim_Nivel[15] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_16_Global.Dimension_16_G >= 3 && ViewBag.Dim_E2_16_Global.Dimension_16_G < 4)
                    {
                        Dim_Colores[15] = Color_Alto;
                        Dim_Nivel[15] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_16_Global.Dimension_16_G >= 4)
                    {
                        Dim_Colores[15] = Color_Muy_Alto;
                        Dim_Nivel[15] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_17_Global.Dimension_17_G >= 0 && ViewBag.Dim_E2_17_Global.Dimension_17_G < 1)
                    {

                        Dim_Colores[16] = Color_Nulo;
                        Dim_Nivel[16] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_17_Global.Dimension_17_G >= 1 && ViewBag.Dim_E2_17_Global.Dimension_17_G < 2)
                    {
                        Dim_Colores[16] = Color_Bajo;
                        Dim_Nivel[16] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_17_Global.Dimension_17_G >= 2 && ViewBag.Dim_E2_17_Global.Dimension_17_G < 3)
                    {
                        Dim_Colores[16] = Color_Medio;
                        Dim_Nivel[16] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_17_Global.Dimension_17_G >= 3 && ViewBag.Dim_E2_17_Global.Dimension_17_G < 4)
                    {
                        Dim_Colores[16] = Color_Alto;
                        Dim_Nivel[16] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_17_Global.Dimension_17_G >= 4)
                    {
                        Dim_Colores[16] = Color_Muy_Alto;
                        Dim_Nivel[16] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_18_Global.Dimension_18_G >= 0 && ViewBag.Dim_E2_18_Global.Dimension_18_G < 1)
                    {

                        Dim_Colores[17] = Color_Nulo;
                        Dim_Nivel[17] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_18_Global.Dimension_18_G >= 1 && ViewBag.Dim_E2_18_Global.Dimension_18_G < 2)
                    {
                        Dim_Colores[17] = Color_Bajo;
                        Dim_Nivel[17] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_18_Global.Dimension_18_G >= 2 && ViewBag.Dim_E2_18_Global.Dimension_18_G < 3)
                    {
                        Dim_Colores[17] = Color_Medio;
                        Dim_Nivel[17] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_18_Global.Dimension_18_G >= 3 && ViewBag.Dim_E2_18_Global.Dimension_18_G < 4)
                    {
                        Dim_Colores[17] = Color_Alto;
                        Dim_Nivel[17] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_18_Global.Dimension_18_G >= 4)
                    {
                        Dim_Colores[17] = Color_Muy_Alto;
                        Dim_Nivel[17] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_19_Global.Dimension_19_G >= 0 && ViewBag.Dim_E2_19_Global.Dimension_19_G < 1)
                    {

                        Dim_Colores[18] = Color_Nulo;
                        Dim_Nivel[18] = "Nulo";
                    }
                    else if (ViewBag.Dim_E2_19_Global.Dimension_19_G >= 1 && ViewBag.Dim_E2_19_Global.Dimension_19_G < 2)
                    {
                        Dim_Colores[18] = Color_Bajo;
                        Dim_Nivel[18] = "Bajo";
                    }
                    else if (ViewBag.Dim_E2_19_Global.Dimension_19_G >= 2 && ViewBag.Dim_E2_19_Global.Dimension_19_G < 3)
                    {
                        Dim_Colores[18] = Color_Medio;
                        Dim_Nivel[18] = "Medio";

                    }
                    else if (ViewBag.Dim_E2_19_Global.Dimension_19_G >= 3 && ViewBag.Dim_E2_19_Global.Dimension_19_G < 4)
                    {
                        Dim_Colores[18] = Color_Alto;
                        Dim_Nivel[18] = "Alto";
                    }
                    else if (ViewBag.Dim_E2_19_Global.Dimension_19_G >= 4)
                    {
                        Dim_Colores[18] = Color_Muy_Alto;
                        Dim_Nivel[18] = "Muy Alto";
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////// 
                    if (ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G >= 0 && ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G < 7)
                    {

                        Dim_Colores[19] = Color_Nulo;
                        Dim_Nivel[19] = "Nulo";
                        ViewBag.Dim_E2_20_Global.Dimension_20_G = 0;
                    }
                    else if (ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G >= 7 && ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G < 10)
                    {
                        Dim_Colores[19] = Color_Bajo;
                        Dim_Nivel[19] = "Bajo";
                        ViewBag.Dim_E2_20_Global.Dimension_20_G = 1;
                    }
                    else if (ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G >= 10 && ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G < 13)
                    {
                        Dim_Colores[19] = Color_Medio;
                        Dim_Nivel[19] = "Medio";
                        ViewBag.Dim_E2_20_Global.Dimension_20_G = 2;

                    }
                    else if (ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G >= 13 && ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G < 16)
                    {
                        Dim_Colores[19] = Color_Alto;
                        Dim_Nivel[19] = "Alto";
                        ViewBag.Dim_E2_20_Global.Dimension_20_G = 3;
                    }
                    else if (ViewBag.Dim_E2_20_Global_NOM035.Dimension_20_G >= 16)
                    {
                        Dim_Colores[19] = Color_Muy_Alto;
                        Dim_Nivel[19] = "Muy Alto";
                        ViewBag.Dim_E2_20_Global.Dimension_20_G = 4;
                    }



                    ViewBag.Colores_Dim = Dim_Colores;
                    ViewBag.Nivel_Dim = Dim_Nivel;

                }
                else
                {
                    return RedirectToAction("Missing_Info");
                }

            }
            catch (Exception ex)
            {
                //  throw ex;
                return RedirectToAction("Missing_Info");
            }


            //############################################################################################################################################################################################################################################################################
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);
            if (eRGOS_Empresas_N01 == null)
            {
                return HttpNotFound();
            }

            ViewBag.Empresa = eRGOS_Empresas_N01.Razon_Social;
            // ViewBag.Centro_Trabajo = eRGOS_Empresas_N01.Nombre_centro_trabajo;
            ViewBag.fecha = "09/26/2019";
            ViewBag.RFC = eRGOS_Empresas_N01.RFC;
            ViewBag.Domicilio = eRGOS_Empresas_N01.Domicilio;
            ViewBag.tel = eRGOS_Empresas_N01.Telefono;
            ViewBag.giro = eRGOS_Empresas_N01.Actividad_Principal;
            ViewBag.contact_name = eRGOS_Empresas_N01.Contacto_Nombre;
            ViewBag.contact_mail = eRGOS_Empresas_N01.Email;
            ViewBag.cedula = "CAFI940130HCHSRR10";



            //ViewBag.Encuestados_M = Encuestados_M;
            //ViewBag.Encuestados_F = Encuestados_F;
            //ViewBag.Encuestados = Encuestados_M + Encuestados_F;
            //////////////////////////////////////////               Filling_Dept_Table             //////////////////////////////////////////////////////////////////////////////////////////////////////////



            //////////////////////////////////  CANALIZADOS ////////////////////////
            ViewBag.Canalizados = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_empresa == id
                                   select Emp_Canalizados.Canalizacion).Count();

            ViewBag.Canalizados_M = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                     where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_empresa == id && Emp_Canalizados.Sexo == 1
                                     select Emp_Canalizados.Canalizacion).Count();

            ViewBag.Canalizados_F = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                     where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_empresa == id && Emp_Canalizados.Sexo == 2
                                     select Emp_Canalizados.Canalizacion).Count();

            //////////////////////////////////////////////////
            //  ViewBag.Conclusiones = conclusiones.ToString();

            int? Total_Mujeres_C = (from Canalizacion in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where Canalizacion.id_empresa == id && Canalizacion.id_empresa == eRGOS_Empresas_N01.id_empresa && Canalizacion.Sexo == 2
                                    select Canalizacion.Canalizacion).Sum();
            if (Total_Mujeres_C == null)
            {
                Total_Mujeres_C = 0;
            }

            ViewBag.Total_Mujeres_C = Total_Mujeres_C;
            int? Total_Hombres_C = (from Canalizacion in db.ERGOS_Cuestionarios_Trabajador_N01
                                    where Canalizacion.id_empresa == id && Canalizacion.id_empresa == eRGOS_Empresas_N01.id_empresa && Canalizacion.Sexo == 1
                                    select Canalizacion.Canalizacion).Sum();
            if (Total_Hombres_C == null)
            {
                Total_Hombres_C = 0;
            }

            ViewBag.Total_Hombres_C = Total_Hombres_C;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Empresas_N01.id_empresa);
            return View(eRGOS_Empresas_N01);
        }
        public ActionResult ReGCLYbqtKJHGFDE87654asauIhmdsaJ6fobQAreA9mBUt4dmBa4Z7(int? id_e)
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


        public void Corte_CLIMA(int id_empresa)
        {
            var emplist = db.fn_MultiEvalua_Clima_Corte_Empresa(id_empresa).ToList();
            int rowStart = 3;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet Preguntas = pck.Workbook.Worksheets.Add("Base de Datos Usuarios");

            Preguntas.Cells["A1:DZ2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Preguntas.Cells["A1:DZ2"].Style.Font.Bold = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("white")));
            Preguntas.Cells["A2:DZ2"].Style.Font.Size = 11;
            Preguntas.Cells["A1:DZ1"].Style.Font.Size = 25;
            Preguntas.Cells["P2:DZ2"].Style.TextRotation = 90;
            Preguntas.Cells["A1:DZ1"].Value = "CORTE DE SEGUIMIETNO";
            Preguntas.Cells["A1:DZ1"].Merge = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Name = "Calibri";
            Preguntas.Cells["A1:DZ2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9071ae")));

            Preguntas.Cells["A2"].Value = "Nombre";
            Preguntas.Cells["B2"].Value = "Folio"; 
            Preguntas.Cells["C2"].Value = "Estatus";

            foreach (var item in emplist)
            {
                Preguntas.Cells[string.Format("A{0}", rowStart)].Value = item.Nombre;
                Preguntas.Cells[string.Format("B{0}", rowStart)].Value = item.id_trabajador; 

                if (item.Calificacion is null)
                {
                    Preguntas.Cells[string.Format("C{0}", rowStart)].Value = "NO COMPLETADO";
                }
                else
                {
                    Preguntas.Cells[string.Format("C{0}", rowStart)].Value = "COMPLETADO";
                }

                rowStart = rowStart + 1;
            }

            Preguntas.Cells["A:DZ"].AutoFitColumns();
            Preguntas.Column(7).BestFit = true;
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Usuarios y Contraseñas.xlsx");
            Response.Flush();
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        #region REPORTES 360

        public void Corte_E360(int id_empresa)
        { 
            var emplist = db.fn_MultiEvalua_E360_Corte_Empresa(id_empresa).ToList();
            int rowStart = 3;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet Preguntas = pck.Workbook.Worksheets.Add("Base de Datos Usuarios");

            Preguntas.Cells["A1:DZ2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Preguntas.Cells["A1:DZ2"].Style.Font.Bold = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("white")));
            Preguntas.Cells["A2:DZ2"].Style.Font.Size = 11;
            Preguntas.Cells["A1:DZ1"].Style.Font.Size = 25;
            Preguntas.Cells["P2:DZ2"].Style.TextRotation = 90;
            Preguntas.Cells["A1:DZ1"].Value = "CORTE DE SEGUIMIETNO";
            Preguntas.Cells["A1:DZ1"].Merge = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Name = "Calibri";
            Preguntas.Cells["A1:DZ2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9071ae")));

            Preguntas.Cells["A2"].Value = "Nombre";
            Preguntas.Cells["B2"].Value = "Folio";
            Preguntas.Cells["C2"].Value = "Evaluando";
            Preguntas.Cells["D2"].Value = "Estatus";

            foreach (var item in emplist)
            {
                Preguntas.Cells[string.Format("A{0}", rowStart)].Value = item.Nombre; 
                Preguntas.Cells[string.Format("B{0}", rowStart)].Value = item.id_trabajador;
                Preguntas.Cells[string.Format("C{0}", rowStart)].Value = item.Evaluado; 

                if (item.Calificacion is null)
                {
                    Preguntas.Cells[string.Format("D{0}", rowStart)].Value = "NO COMPLETADO";
                }
                else
                {
                    Preguntas.Cells[string.Format("D{0}", rowStart)].Value = "COMPLETADO";
                }

                rowStart = rowStart + 1;
            }

            Preguntas.Cells["A:DZ"].AutoFitColumns();
            Preguntas.Column(7).BestFit = true;
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Usuarios y Contraseñas.xlsx");
            Response.Flush();
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public ActionResult ReGE360ASDFD09876TRFGBNJKI49R8F7DYTGBDN4RVSDSDasjddsde3sdsw(int? id_e)
        {
            try
            {
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
        [HttpPost]
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
        public ActionResult ReDeptos360ASsdafhafd9834bfjksdgF7DYTGBsdfghjygfDFGH78(int? id_e)
        {
            try
            {


                var result = (from Deptos in db.fn_MultiEvalua_E360_Empresa_Deptos(id_e)
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

         
                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();

 

                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult ReGE360_EMPfcdusyhegbvrfn8d73y45t9fd8s7y4h5jtfiduyhe4eudyhdhdh444d3sdsw(int? id_e)
        {
            try
            {  
                var Final_Empresa_Emp = db.fn_MultiEvalua_E360_Empresa_Emp(id_e).FirstOrDefault();  
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
                Resultados_Colores_Emp[14] = promedio_color(Final_Empresa_Emp.PROMEDIO_C_XV_100); 

                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();

                 
                ViewBag.Final_Empresa_Emp = Final_Empresa_Emp;  
                ViewBag.Resultados_Colores_Emp = Resultados_Colores_Emp; 
                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ReGE360_SUPsdiuhygbrvtgnvjciusyg4h5jtig9f8d76t4g5thjgifc8x7s6wtg4tgv654ee3sdsw(int? id_e)
        {
            try
            {
                var Final_Empresa_Sup = db.fn_MultiEvalua_E360_Empresa_Sup(id_e).FirstOrDefault();
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

                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();


                ViewBag.Final_Empresa_Sup = Final_Empresa_Sup;
                ViewBag.Resultados_Colores_Sup = Resultados_Colores_Sup;
                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ReGE360_SUBsuhfdifgdsg87654edcxystgw3b4h5jtigf8d7sywtg34brtjddsde3sdsw(int? id_e)
        {
            try
            {


                var Final_Empresa_Sub = db.fn_MultiEvalua_E360_Empresa_Sub(id_e).FirstOrDefault();
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

                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();


                ViewBag.Final_Empresa_Sub = Final_Empresa_Sub;
                ViewBag.Resultados_Colores_Sub = Resultados_Colores_Sub;
                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ReGE360_COMPojhgf05FD09876TRSFSDFSDFFGBNJKI49R8F7DYTGBDN4RVSDSDasjddsde3sdsw(int? id_e)
        {
            try
            { 

                var Final_Empresa_Com = db.fn_MultiEvalua_E360_Empresa_Com(id_e).FirstOrDefault();
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

                ViewBag.Final_Empresa_Com = Final_Empresa_Com;
                ViewBag.Resultados_Colores_Com = Resultados_Colores_Com;

                ViewBag.Evaluaciones_counter = (from CL in db.E360_Cuestionario_Resultado_N01
                                                join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CL.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                                join E in db.ERGOS_Empresas_N01 on CL.ERGOS_Cuestionarios_Trabajador_N01.id_empresa equals E.id_empresa
                                                where CT.id_empresa == id_e
                                                select CL.id_cuestionario_trabajador).Count();

                 
                return View(db.ERGOS_Empresas_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        public void ExportToExcel_Users_Empresa(int id_empresa)
        {
            List<ERGOS_Usuarios_N01> emplist = db.ERGOS_Usuarios_N01.Where(e => e.id_empresa == id_empresa).Where(e => e.id_rol == 6).ToList();
            int rowStart = 3;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet Preguntas = pck.Workbook.Worksheets.Add("Base de Datos Usuarios");

            Preguntas.Cells["A1:DZ2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Preguntas.Cells["A1:DZ2"].Style.Font.Bold = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("white")));
            Preguntas.Cells["A2:DZ2"].Style.Font.Size = 11;
            Preguntas.Cells["A1:DZ1"].Style.Font.Size = 25;
            Preguntas.Cells["P2:DZ2"].Style.TextRotation = 90;
            Preguntas.Cells["A1:DZ1"].Value = "USUARIOS";
            Preguntas.Cells["A1:DZ1"].Merge = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Name = "Calibri";
            Preguntas.Cells["A1:DZ2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9071ae")));

            Preguntas.Cells["A2"].Value = "Empresa";
            Preguntas.Cells["B2"].Value = "Centro de Trabajo";
            Preguntas.Cells["C2"].Value = "No. Folio";

            foreach (var item in emplist)
            {
                Preguntas.Cells[string.Format("A{0}", rowStart)].Value = item.ERGOS_Empresas_N01.Razon_Social;

                if (item.id_centro_trabajo is null )
                {
                    Preguntas.Cells[string.Format("B{0}", rowStart)].Value = "Sin Información"; }
                else
                {
                    Preguntas.Cells[string.Format("B{0}", rowStart)].Value = item.ERGOS_Centros_Trabajo_N01.Nombre_centro_trabajo;
                }


                Preguntas.Cells[string.Format("C{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.id_trabajador;
                Preguntas.Cells[string.Format("D{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Nombre;
                Preguntas.Cells[string.Format("E{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Email;
                Preguntas.Cells[string.Format("F{0}", rowStart)].Value = item.User_Nombre;
                Preguntas.Cells[string.Format("G{0}", rowStart)].Value = item.User_Password;
                rowStart = rowStart + 1;
            }

            Preguntas.Cells["A:DZ"].AutoFitColumns();
            Preguntas.Column(7).BestFit = true;
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Usuarios y Contraseñas.xlsx"); 
            Response.Flush();
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        public void ExportToExcel_Users_Centro(int id_centro_trabajo)
        {
            List<ERGOS_Usuarios_N01> emplist = db.ERGOS_Usuarios_N01.Where(e => e.id_centro_trabajo == id_centro_trabajo).Where(e => e.id_rol == 6).ToList();
            int rowStart = 3;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet Preguntas = pck.Workbook.Worksheets.Add("Base de Datos Usuarios");

            Preguntas.Cells["A1:DZ2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Preguntas.Cells["A1:DZ2"].Style.Font.Bold = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("white")));
            Preguntas.Cells["A2:DZ2"].Style.Font.Size = 11;
            Preguntas.Cells["A1:DZ1"].Style.Font.Size = 25;
            Preguntas.Cells["P2:DZ2"].Style.TextRotation = 90;
            Preguntas.Cells["A1:DZ1"].Value = "USUARIOS";
            Preguntas.Cells["A1:DZ1"].Merge = true;
            Preguntas.Cells["A1:DZ2"].Style.Font.Name = "Calibri";
            Preguntas.Cells["A1:DZ2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#9071ae")));

            Preguntas.Cells["A2"].Value = "Empresa";
            Preguntas.Cells["B2"].Value = "Centro de Trabajo";
            Preguntas.Cells["C2"].Value = "No. Folio";
            Preguntas.Cells["D2"].Value = "Nombre";
            Preguntas.Cells["E2"].Value = "Correo Electrónico";
            Preguntas.Cells["F2"].Value = "Usuario";
            Preguntas.Cells["G2"].Value = "Contraseña";
            foreach (var item in emplist)
            {
                Preguntas.Cells[string.Format("A{0}", rowStart)].Value = item.ERGOS_Empresas_N01.Razon_Social;

                if (item.id_centro_trabajo is null)
                {
                    Preguntas.Cells[string.Format("B{0}", rowStart)].Value = "Sin Información";
                }
                else
                {
                    Preguntas.Cells[string.Format("B{0}", rowStart)].Value = item.ERGOS_Centros_Trabajo_N01.Nombre_centro_trabajo;
                }

                Preguntas.Cells[string.Format("C{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.id_trabajador;
                Preguntas.Cells[string.Format("D{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Nombre;
                Preguntas.Cells[string.Format("E{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.Email;
                Preguntas.Cells[string.Format("F{0}", rowStart)].Value = item.User_Nombre;
                Preguntas.Cells[string.Format("G{0}", rowStart)].Value = item.User_Password;
                rowStart = rowStart + 1;
            }

            Preguntas.Cells["A:DZ"].AutoFitColumns();
            Preguntas.Column(7).BestFit = true;
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Usuarios y Contraseñas.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}