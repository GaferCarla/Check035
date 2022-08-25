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
    public class EmpresasController : Controller
    {
        private Check035Entities db = new Check035Entities();
        //GENERO
        public string Final_Beredict_M = "";
        public string Final_Beredict_H = "";
        //EDO CIVIL
        public string Final_Beredict_CA = "";
        public string Final_Beredict_SO = "";
        public string Final_Beredict_DI = "";
        public string Final_Beredict_UL = "";
        public string Final_Beredict_VI = "";
        //EDAD
        public string Final_Beredict_1 = "";
        public string Final_Beredict_2 = "";
        public string Final_Beredict_3 = "";
        public string Final_Beredict_4 = "";
        public string Final_Beredict_5 = "";
        public string Final_Beredict_6 = "";
        public string Final_Beredict_7 = "";
        public string Final_Beredict_8 = "";
        public string Final_Beredict_9 = "";
        public string Final_Beredict_10 = "";
        public string Final_Beredict_11 = "";
        public string Final_Beredict_12 = "";

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
        // GET: Empresas

        // GET: Empresas
        [Authorize(Roles = "Admin,Admin_SyS")]
        public ActionResult Index()
        {
            return View(db.ERGOS_Empresas_N01.ToList());
        }

        public JsonResult Searching_Gender(int id_empresa, int id_survey)
        {
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL MEN WHO ANSWERED SURVEY
            //####################################################################################################################


            string Reporte_Hombres = "SELECT [dbo].[fnReporte_Sexo_Hombres] ({0})";
            int Total_Reporte_Hombres = db.Database.SqlQuery<int>(Reporte_Hombres, parameters).FirstOrDefault();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL WOMEN WHO ANSWERED SURVEY
            //####################################################################################################################

            string Reporte_Mujeres = "SELECT [dbo].[fnReporte_Sexo_Mujeres] ({0})";
            int Total_Reporte_Mujeres = db.Database.SqlQuery<int>(Reporte_Mujeres, parameters).FirstOrDefault();

            //####################################################################################################################


            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_H = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Sexo == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_M = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Sexo == 2 select new { G.id_empresa }).Count();

            int Final_AVG_H = 0;
            int Final_AVG_M = 0;

            int Final_AVG = Final_Sum / Total_Surveys;
            if (Total_Surveys_H != 0)
            {
                Final_AVG_H = Total_Reporte_Hombres / Total_Surveys_H;
            }

            if (Total_Surveys_M != 0)
            {
                Final_AVG_M = Total_Reporte_Mujeres / Total_Surveys_M;
            }


            string Color_Nivel_H = "rgba(0, 0, 0, 1)";
            string Color_Nivel_M = "rgba(0, 0, 0, 1)";
            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     HOMBRES     ###################################################################
                if (Final_AVG_H < 50) { Final_Beredict_H = "Nulo"; Color_Nivel_H = Color_Nulo; }

                else if (Final_AVG_H >= 50 && Final_AVG_H < 75) { Final_Beredict_H = "Bajo"; Color_Nivel_H = Color_Bajo; }

                else if (Final_AVG_H >= 75 && Final_AVG_H < 99) { Final_Beredict_H = "Medio"; Color_Nivel_H = Color_Medio; }

                else if (Final_AVG_H >= 99 && Final_AVG_H < 140) { Final_Beredict_H = "Alto"; Color_Nivel_H = Color_Alto; }

                else if (Final_AVG_H >= 140) { Final_Beredict_H = "Muy Alto"; Color_Nivel_H = Color_Muy_Alto; }

                //#################################################     MUJERES     ###################################################################

                if (Final_AVG_M < 50) { Final_Beredict_M = "Nulo"; Color_Nivel_M = Color_Nulo; }

                else if (Final_AVG_M >= 50 && Final_AVG_M < 75) { Final_Beredict_M = "Bajo"; Color_Nivel_M = Color_Bajo; }

                else if (Final_AVG_M >= 75 && Final_AVG_M < 99) { Final_Beredict_M = "Medio"; Color_Nivel_M = Color_Medio; }

                else if (Final_AVG_M >= 99 && Final_AVG_M < 140) { Final_Beredict_M = "Alto"; Color_Nivel_M = Color_Alto; }

                else if (Final_AVG_M >= 140) { Final_Beredict_M = "Muy Alto"; Color_Nivel_M = Color_Muy_Alto; }

            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     HOMBRES     ###################################################################
                if (Final_AVG_H < 20) { Final_Beredict_H = "Nulo"; Color_Nivel_H = Color_Nulo; }

                else if (Final_AVG_H >= 20 && Final_AVG_H < 45) { Final_Beredict_H = "Bajo"; Color_Nivel_H = Color_Bajo; }

                else if (Final_AVG_H >= 45 && Final_AVG_H < 70) { Final_Beredict_H = "Medio"; Color_Nivel_H = Color_Medio; }

                else if (Final_AVG_H >= 70 && Final_AVG_H < 90) { Final_Beredict_H = "Alto"; Color_Nivel_H = Color_Alto; }

                else if (Final_AVG_H >= 90) { Final_Beredict_H = "Muy Alto"; Color_Nivel_H = Color_Muy_Alto; }

                //#################################################     MUJERES     ###################################################################

                if (Final_AVG_M < 20) { Final_Beredict_M = "Nulo"; Color_Nivel_M = Color_Nulo; }

                else if (Final_AVG_M >= 20 && Final_AVG_M < 45) { Final_Beredict_M = "Bajo"; Color_Nivel_M = Color_Bajo; }

                else if (Final_AVG_M >= 45 && Final_AVG_M < 70) { Final_Beredict_M = "Medio"; Color_Nivel_M = Color_Medio; }

                else if (Final_AVG_M >= 70 && Final_AVG_M < 90) { Final_Beredict_M = "Alto"; Color_Nivel_M = Color_Alto; }

                else if (Final_AVG_M >= 90) { Final_Beredict_M = "Muy Alto"; Color_Nivel_M = Color_Muy_Alto; }
            }

            var result = new { Final_AVG, Final_AVG_H, Final_AVG_M, Total_Surveys_H, Total_Surveys_M, Final_Beredict_H, Final_Beredict_M, Color_Nivel_H, Color_Nivel_M };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Searching_Edo_Civil(int id_empresa, int id_survey)
        {


            //####################################################################################################################
            //                                              ESTADO CIVIL
            //
            //                                          CASADO          =   1
            //                                          SOLTERO         =   2
            //                                          DIVORCIADO      =   3
            //                                          UNION LIBRE     =   4
            //                                          VIUDO           =   5
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL CASADOS WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Casados = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                          join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                          where CT.id_empresa == id_empresa && CT.Estado_Civil == 1
                                          select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL SOLTEROS WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Solteros = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Estado_Civil == 2
                                           select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL DIVORCIADO WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Divorciados = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                              join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                              where CT.id_empresa == id_empresa && CT.Estado_Civil == 3
                                              select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  UNION LIBRE WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Union_Libre = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                              join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                              where CT.id_empresa == id_empresa && CT.Estado_Civil == 4
                                              select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL VIUDO WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Viudos = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                         join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                         where CT.id_empresa == id_empresa && CT.Estado_Civil == 5
                                         select G.Calificacion).Sum();
            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_CA = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Estado_Civil == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_SO = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Estado_Civil == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_DI = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Estado_Civil == 3 select new { G.id_empresa }).Count();
            int Total_Surveys_UL = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Estado_Civil == 4 select new { G.id_empresa }).Count();
            int Total_Surveys_VI = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Estado_Civil == 5 select new { G.id_empresa }).Count();

            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_CA = Total_Reporte_Casados / Total_Surveys_CA;
            int? Final_AVG_SO = Total_Reporte_Solteros / Total_Surveys_SO;
            int? Final_AVG_DI = Total_Reporte_Divorciados / Total_Surveys_DI;
            int? Final_AVG_UL = Total_Reporte_Union_Libre / Total_Surveys_UL;
            int? Final_AVG_VI = Total_Reporte_Viudos / Total_Surveys_VI;

            string Color_Nivel_CA = "rgba(0, 0, 0, 1)";
            string Color_Nivel_SO = "rgba(0, 0, 0, 1)";
            string Color_Nivel_DI = "rgba(0, 0, 0, 1)";
            string Color_Nivel_UL = "rgba(0, 0, 0, 1)";
            string Color_Nivel_VI = "rgba(0, 0, 0, 1)";

            //#########################################################################################################################################################
            //                                                                SURVEY 3
            //#########################################################################################################################################################
            if (id_survey == 3)
            {
                //#################################################     CASADO     ########################################################################################################
                if (Final_AVG_CA < 50) { Final_Beredict_CA = "Nulo"; Color_Nivel_CA = Color_Nulo; }

                else if (Final_AVG_CA >= 50 && Final_AVG_CA < 75) { Final_Beredict_CA = "Bajo"; Color_Nivel_CA = Color_Bajo; }

                else if (Final_AVG_CA >= 75 && Final_AVG_CA < 99) { Final_Beredict_CA = "Medio"; Color_Nivel_CA = Color_Medio; }

                else if (Final_AVG_CA >= 99 && Final_AVG_CA < 140) { Final_Beredict_CA = "Alto"; Color_Nivel_CA = Color_Alto; }

                else if (Final_AVG_CA >= 140) { Final_Beredict_CA = "Muy Alto"; Color_Nivel_CA = Color_Muy_Alto; }

                //#################################################     SOLTERO     ########################################################################################################

                if (Final_AVG_SO < 50) { Final_Beredict_SO = "Nulo"; Color_Nivel_SO = Color_Nulo; }

                else if (Final_AVG_SO >= 50 && Final_AVG_SO < 75) { Final_Beredict_SO = "Bajo"; Color_Nivel_SO = Color_Bajo; }

                else if (Final_AVG_SO >= 75 && Final_AVG_SO < 99) { Final_Beredict_SO = "Medio"; Color_Nivel_SO = Color_Medio; }

                else if (Final_AVG_SO >= 99 && Final_AVG_SO < 140) { Final_Beredict_SO = "Alto"; Color_Nivel_SO = Color_Alto; }

                else if (Final_AVG_SO >= 140) { Final_Beredict_SO = "Muy Alto"; Color_Nivel_SO = Color_Muy_Alto; }

                //#################################################     DIVORCIADO     ####################################################################################################

                if (Final_AVG_DI < 50) { Final_Beredict_DI = "Nulo"; Color_Nivel_DI = Color_Nulo; }

                else if (Final_AVG_DI >= 50 && Final_AVG_DI < 75) { Final_Beredict_DI = "Bajo"; Color_Nivel_DI = Color_Bajo; }

                else if (Final_AVG_DI >= 75 && Final_AVG_DI < 99) { Final_Beredict_DI = "Medio"; Color_Nivel_DI = Color_Medio; }

                else if (Final_AVG_DI >= 99 && Final_AVG_DI < 140) { Final_Beredict_DI = "Alto"; Color_Nivel_DI = Color_Alto; }

                else if (Final_AVG_DI >= 140) { Final_Beredict_DI = "Muy Alto"; Color_Nivel_DI = Color_Muy_Alto; }

                //#################################################     UNION LIBRE     ###################################################################

                if (Final_AVG_UL < 50) { Final_Beredict_UL = "Nulo"; Color_Nivel_UL = Color_Nulo; }

                else if (Final_AVG_UL >= 50 && Final_AVG_UL < 75) { Final_Beredict_UL = "Bajo"; Color_Nivel_UL = Color_Bajo; }

                else if (Final_AVG_UL >= 75 && Final_AVG_UL < 99) { Final_Beredict_UL = "Medio"; Color_Nivel_UL = Color_Medio; }

                else if (Final_AVG_UL >= 99 && Final_AVG_UL < 140) { Final_Beredict_UL = "Alto"; Color_Nivel_UL = Color_Alto; }

                else if (Final_AVG_UL >= 140) { Final_Beredict_UL = "Muy Alto"; Color_Nivel_UL = Color_Muy_Alto; }

                //#################################################     UNION LIBRE     ###################################################################

                if (Final_AVG_VI < 50) { Final_Beredict_VI = "Nulo"; Color_Nivel_VI = Color_Nulo; }

                else if (Final_AVG_VI >= 50 && Final_AVG_VI < 75) { Final_Beredict_VI = "Bajo"; Color_Nivel_VI = Color_Bajo; }

                else if (Final_AVG_VI >= 75 && Final_AVG_VI < 99) { Final_Beredict_VI = "Medio"; Color_Nivel_VI = Color_Medio; }

                else if (Final_AVG_VI >= 99 && Final_AVG_VI < 140) { Final_Beredict_VI = "Alto"; Color_Nivel_VI = Color_Alto; }

                else if (Final_AVG_VI >= 140) { Final_Beredict_VI = "Muy Alto"; Color_Nivel_VI = Color_Muy_Alto; }

            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     CASADO     ########################################################################################################
                if (Final_AVG_CA < 20) { Final_Beredict_CA = "Nulo"; Color_Nivel_CA = Color_Nulo; }

                else if (Final_AVG_CA >= 20 && Final_AVG_CA < 45) { Final_Beredict_CA = "Bajo"; Color_Nivel_CA = Color_Bajo; }

                else if (Final_AVG_CA >= 45 && Final_AVG_CA < 70) { Final_Beredict_CA = "Medio"; Color_Nivel_CA = Color_Medio; }

                else if (Final_AVG_CA >= 70 && Final_AVG_CA < 90) { Final_Beredict_CA = "Alto"; Color_Nivel_CA = Color_Alto; }

                else if (Final_AVG_CA >= 90) { Final_Beredict_CA = "Muy Alto"; Color_Nivel_CA = Color_Muy_Alto; }

                //#################################################     SOLTERO     ########################################################################################################

                if (Final_AVG_SO < 20) { Final_Beredict_SO = "Nulo"; Color_Nivel_SO = Color_Nulo; }

                else if (Final_AVG_SO >= 20 && Final_AVG_SO < 45) { Final_Beredict_SO = "Bajo"; Color_Nivel_SO = Color_Bajo; }

                else if (Final_AVG_SO >= 45 && Final_AVG_SO < 70) { Final_Beredict_SO = "Medio"; Color_Nivel_SO = Color_Medio; }

                else if (Final_AVG_SO >= 70 && Final_AVG_SO < 90) { Final_Beredict_SO = "Alto"; Color_Nivel_SO = Color_Alto; }

                else if (Final_AVG_SO >= 90) { Final_Beredict_SO = "Muy Alto"; Color_Nivel_SO = Color_Muy_Alto; }

                //#################################################     DIVORCIADO     ####################################################################################################

                if (Final_AVG_DI < 20) { Final_Beredict_DI = "Nulo"; Color_Nivel_DI = Color_Nulo; }

                else if (Final_AVG_DI >= 20 && Final_AVG_DI < 45) { Final_Beredict_DI = "Bajo"; Color_Nivel_DI = Color_Bajo; }

                else if (Final_AVG_DI >= 45 && Final_AVG_DI < 70) { Final_Beredict_DI = "Medio"; Color_Nivel_DI = Color_Medio; }

                else if (Final_AVG_DI >= 70 && Final_AVG_DI < 90) { Final_Beredict_DI = "Alto"; Color_Nivel_DI = Color_Alto; }

                else if (Final_AVG_DI >= 90) { Final_Beredict_DI = "Muy Alto"; Color_Nivel_DI = Color_Muy_Alto; }

                //#################################################     UNION LIBRE     ###################################################################

                if (Final_AVG_UL < 20) { Final_Beredict_UL = "Nulo"; Color_Nivel_UL = Color_Nulo; }

                else if (Final_AVG_UL >= 20 && Final_AVG_UL < 45) { Final_Beredict_UL = "Bajo"; Color_Nivel_UL = Color_Bajo; }

                else if (Final_AVG_UL >= 45 && Final_AVG_UL < 70) { Final_Beredict_UL = "Medio"; Color_Nivel_UL = Color_Medio; }

                else if (Final_AVG_UL >= 70 && Final_AVG_UL < 90) { Final_Beredict_UL = "Alto"; Color_Nivel_UL = Color_Alto; }

                else if (Final_AVG_UL >= 90) { Final_Beredict_UL = "Muy Alto"; Color_Nivel_UL = Color_Muy_Alto; }

                //#################################################     UNION LIBRE     ###################################################################

                if (Final_AVG_VI < 20) { Final_Beredict_VI = "Nulo"; Color_Nivel_VI = Color_Nulo; }

                else if (Final_AVG_VI >= 20 && Final_AVG_VI < 45) { Final_Beredict_VI = "Bajo"; Color_Nivel_VI = Color_Bajo; }

                else if (Final_AVG_VI >= 45 && Final_AVG_VI < 70) { Final_Beredict_VI = "Medio"; Color_Nivel_VI = Color_Medio; }

                else if (Final_AVG_VI >= 70 && Final_AVG_VI < 90) { Final_Beredict_VI = "Alto"; Color_Nivel_VI = Color_Alto; }

                else if (Final_AVG_VI >= 90) { Final_Beredict_VI = "Muy Alto"; Color_Nivel_VI = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_CA,
                Final_AVG_SO,
                Final_AVG_DI,
                Final_AVG_UL,
                Final_AVG_VI,
                Total_Surveys_CA,
                Total_Surveys_SO,
                Total_Surveys_DI,
                Total_Surveys_UL,
                Total_Surveys_VI,
                Final_Beredict_CA,
                Final_Beredict_SO,
                Final_Beredict_DI,
                Final_Beredict_UL,
                Final_Beredict_VI,
                Color_Nivel_CA,
                Color_Nivel_SO,
                Color_Nivel_DI,
                Color_Nivel_UL,
                Color_Nivel_VI
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Nivel_Estudios(int id_empresa, int id_survey)
        {

            //####################################################################################################################
            //                                              NIVEL DE ESTUDIOS
            //
            //                                          Sin Información                 = 1   
            //                                          Primaria                        = 2  
            //                                          Secundaria                      = 3
            //                                          Preparatoria o Bachillerato     = 4
            //                                          Técnico Superior                = 5
            //                                          Licenciatura                    = 6
            //                                          Maestría                        = 7
            //                                          Doctorado                       = 8
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Sin Información WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Sin_Info = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 1
                                           select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Primaria WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Primaria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 2
                                           select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Secundaria WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Secundaria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 3
                                             select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  Preparatoria o Bachillerato WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Preparatoria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                               join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                               where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 4
                                               select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  Técnico Superior WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Tecnico = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                          join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                          where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 5
                                          select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Licenciatura WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Licenciatura = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                               join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                               where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 6
                                               select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Maestría WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Maestria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 7
                                           select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Doctorado WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Doctorado = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                            join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                            where CT.id_empresa == id_empresa && CT.Nivel_Estudios == 8
                                            select G.Calificacion).Sum();

            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_3 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 3 select new { G.id_empresa }).Count();
            int Total_Surveys_4 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 4 select new { G.id_empresa }).Count();
            int Total_Surveys_5 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 5 select new { G.id_empresa }).Count();
            int Total_Surveys_6 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 6 select new { G.id_empresa }).Count();
            int Total_Surveys_7 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 7 select new { G.id_empresa }).Count();
            int Total_Surveys_8 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Nivel_Estudios == 8 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_Sin_Info / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_Primaria / Total_Surveys_2;
            int? Final_AVG_3 = Total_Reporte_Secundaria / Total_Surveys_3;
            int? Final_AVG_4 = Total_Reporte_Preparatoria / Total_Surveys_4;
            int? Final_AVG_5 = Total_Reporte_Tecnico / Total_Surveys_5;
            int? Final_AVG_6 = Total_Reporte_Licenciatura / Total_Surveys_6;
            int? Final_AVG_7 = Total_Reporte_Maestria / Total_Surveys_7;
            int? Final_AVG_8 = Total_Reporte_Doctorado / Total_Surveys_8;

            string Color_Nivel_1 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_2 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_3 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_4 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_5 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_6 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_7 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_8 = "rgba(0, 0, 0, 1)";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     Sin Información Estudios     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Primaria     ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Secundaria     ###################################################################

                if (Final_AVG_3 < 50) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 50 && Final_AVG_3 < 75) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 75 && Final_AVG_3 < 99) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 99 && Final_AVG_3 < 140) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 140) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     Preparatoria    ###################################################################

                if (Final_AVG_4 < 50) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 50 && Final_AVG_4 < 75) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 75 && Final_AVG_4 < 99) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 99 && Final_AVG_4 < 140) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 140) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }

                //#################################################     Tecnico    ###################################################################

                if (Final_AVG_5 < 50) { Final_Beredict_5 = "Nulo"; Color_Nivel_5 = Color_Nulo; }

                else if (Final_AVG_5 >= 50 && Final_AVG_5 < 75) { Final_Beredict_5 = "Bajo"; Color_Nivel_5 = Color_Bajo; }

                else if (Final_AVG_5 >= 75 && Final_AVG_5 < 99) { Final_Beredict_5 = "Medio"; Color_Nivel_5 = Color_Medio; }

                else if (Final_AVG_5 >= 99 && Final_AVG_5 < 140) { Final_Beredict_5 = "Alto"; Color_Nivel_5 = Color_Alto; }

                else if (Final_AVG_5 >= 140) { Final_Beredict_5 = "Muy Alto"; Color_Nivel_5 = Color_Muy_Alto; }
                //#################################################     Licenciatura    ###################################################################

                if (Final_AVG_6 < 50) { Final_Beredict_6 = "Nulo"; Color_Nivel_6 = Color_Nulo; }

                else if (Final_AVG_6 >= 50 && Final_AVG_6 < 75) { Final_Beredict_6 = "Bajo"; Color_Nivel_6 = Color_Bajo; }

                else if (Final_AVG_6 >= 75 && Final_AVG_6 < 99) { Final_Beredict_6 = "Medio"; Color_Nivel_6 = Color_Medio; }

                else if (Final_AVG_6 >= 99 && Final_AVG_6 < 140) { Final_Beredict_6 = "Alto"; Color_Nivel_6 = Color_Alto; }

                else if (Final_AVG_6 >= 140) { Final_Beredict_6 = "Muy Alto"; Color_Nivel_6 = Color_Muy_Alto; }

                //#################################################     Maestria    ###################################################################

                if (Final_AVG_7 < 50) { Final_Beredict_7 = "Nulo"; Color_Nivel_7 = Color_Nulo; }

                else if (Final_AVG_7 >= 50 && Final_AVG_7 < 75) { Final_Beredict_7 = "Bajo"; Color_Nivel_7 = Color_Bajo; }

                else if (Final_AVG_7 >= 75 && Final_AVG_7 < 99) { Final_Beredict_7 = "Medio"; Color_Nivel_7 = Color_Medio; }

                else if (Final_AVG_7 >= 99 && Final_AVG_7 < 140) { Final_Beredict_7 = "Alto"; Color_Nivel_7 = Color_Alto; }

                else if (Final_AVG_7 >= 140) { Final_Beredict_7 = "Muy Alto"; Color_Nivel_7 = Color_Muy_Alto; }

                //#################################################     Doctorado    ###################################################################

                if (Final_AVG_8 < 50) { Final_Beredict_8 = "Nulo"; Color_Nivel_8 = Color_Nulo; }

                else if (Final_AVG_8 >= 50 && Final_AVG_8 < 75) { Final_Beredict_8 = "Bajo"; Color_Nivel_8 = Color_Bajo; }

                else if (Final_AVG_8 >= 75 && Final_AVG_8 < 99) { Final_Beredict_8 = "Medio"; Color_Nivel_8 = Color_Medio; }

                else if (Final_AVG_8 >= 99 && Final_AVG_8 < 140) { Final_Beredict_8 = "Alto"; Color_Nivel_8 = Color_Alto; }

                else if (Final_AVG_8 >= 140) { Final_Beredict_8 = "Muy Alto"; Color_Nivel_8 = Color_Muy_Alto; }

            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     Sin Información Estudios     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Primaria     ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Secundaria     ###################################################################

                if (Final_AVG_3 < 20) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 20 && Final_AVG_3 < 45) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 45 && Final_AVG_3 < 70) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 70 && Final_AVG_3 < 90) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 90) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     Preparatoria    ###################################################################

                if (Final_AVG_4 < 20) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 20 && Final_AVG_4 < 45) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 45 && Final_AVG_4 < 70) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 70 && Final_AVG_4 < 90) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 90) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }

                //#################################################     Tecnico    ###################################################################

                if (Final_AVG_5 < 20) { Final_Beredict_5 = "Nulo"; Color_Nivel_5 = Color_Nulo; }

                else if (Final_AVG_5 >= 20 && Final_AVG_5 < 45) { Final_Beredict_5 = "Bajo"; Color_Nivel_5 = Color_Bajo; }

                else if (Final_AVG_5 >= 45 && Final_AVG_5 < 70) { Final_Beredict_5 = "Medio"; Color_Nivel_5 = Color_Medio; }

                else if (Final_AVG_5 >= 70 && Final_AVG_5 < 90) { Final_Beredict_5 = "Alto"; Color_Nivel_5 = Color_Alto; }

                else if (Final_AVG_5 >= 90) { Final_Beredict_5 = "Muy Alto"; Color_Nivel_5 = Color_Muy_Alto; }
                //#################################################     Licenciatura    ###################################################################

                if (Final_AVG_6 < 20) { Final_Beredict_6 = "Nulo"; Color_Nivel_6 = Color_Nulo; }

                else if (Final_AVG_6 >= 20 && Final_AVG_6 < 45) { Final_Beredict_6 = "Bajo"; Color_Nivel_6 = Color_Bajo; }

                else if (Final_AVG_6 >= 45 && Final_AVG_6 < 70) { Final_Beredict_6 = "Medio"; Color_Nivel_6 = Color_Medio; }

                else if (Final_AVG_6 >= 70 && Final_AVG_6 < 90) { Final_Beredict_6 = "Alto"; Color_Nivel_6 = Color_Alto; }

                else if (Final_AVG_6 >= 90) { Final_Beredict_6 = "Muy Alto"; Color_Nivel_6 = Color_Muy_Alto; }

                //#################################################     Maestria    ###################################################################

                if (Final_AVG_7 < 20) { Final_Beredict_7 = "Nulo"; Color_Nivel_7 = Color_Nulo; }

                else if (Final_AVG_7 >= 20 && Final_AVG_7 < 45) { Final_Beredict_7 = "Bajo"; Color_Nivel_7 = Color_Bajo; }

                else if (Final_AVG_7 >= 45 && Final_AVG_7 < 70) { Final_Beredict_7 = "Medio"; Color_Nivel_7 = Color_Medio; }

                else if (Final_AVG_7 >= 70 && Final_AVG_7 < 90) { Final_Beredict_7 = "Alto"; Color_Nivel_7 = Color_Alto; }

                else if (Final_AVG_7 >= 90) { Final_Beredict_7 = "Muy Alto"; Color_Nivel_7 = Color_Muy_Alto; }

                //#################################################     Doctorado    ###################################################################

                if (Final_AVG_8 < 20) { Final_Beredict_8 = "Nulo"; Color_Nivel_8 = Color_Nulo; }

                else if (Final_AVG_8 >= 20 && Final_AVG_8 < 45) { Final_Beredict_8 = "Bajo"; Color_Nivel_8 = Color_Bajo; }

                else if (Final_AVG_8 >= 45 && Final_AVG_8 < 70) { Final_Beredict_8 = "Medio"; Color_Nivel_8 = Color_Medio; }

                else if (Final_AVG_8 >= 70 && Final_AVG_8 < 90) { Final_Beredict_8 = "Alto"; Color_Nivel_8 = Color_Alto; }

                else if (Final_AVG_8 >= 90) { Final_Beredict_8 = "Muy Alto"; Color_Nivel_8 = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Final_AVG_3,
                Final_AVG_4,
                Final_AVG_5,
                Final_AVG_6,
                Final_AVG_7,
                Final_AVG_8,
                Total_Surveys_1,
                Total_Surveys_2,
                Total_Surveys_3,
                Total_Surveys_4,
                Total_Surveys_5,
                Total_Surveys_6,
                Total_Surveys_7,
                Total_Surveys_8,
                Final_Beredict_1,
                Final_Beredict_2,
                Final_Beredict_3,
                Final_Beredict_4,
                Final_Beredict_5,
                Final_Beredict_6,
                Final_Beredict_7,
                Final_Beredict_8,
                Color_Nivel_1,
                Color_Nivel_2,
                Color_Nivel_3,
                Color_Nivel_4,
                Color_Nivel_5,
                Color_Nivel_6,
                Color_Nivel_7,
                Color_Nivel_8
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Tipo_Personal(int id_empresa, int id_survey)
        {

            //####################################################################################################################
            //                                             TIPO DE PERSONAL
            //
            //                                          Sindicalizado           = 1   
            //                                          Confianza               = 2 
            //                                          Ninguno                 = 3
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Sindicalizado WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Sin_Info = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Tipo_Personal == 1
                                           select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Confianza WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Primaria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Tipo_Personal == 2
                                           select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Ninguno WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Secundaria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_empresa == id_empresa && CT.Tipo_Personal == 3
                                             select G.Calificacion).Sum();

            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Personal == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Personal == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_3 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Personal == 3 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_Sin_Info / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_Primaria / Total_Surveys_2;
            int? Final_AVG_3 = Total_Reporte_Secundaria / Total_Surveys_3;

            string Color_Nivel_1 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_2 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_3 = "rgba(0, 0, 0, 1)";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     Sindicalizado     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Confianza     ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Ninguno     ###################################################################

                if (Final_AVG_3 < 50) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 50 && Final_AVG_3 < 75) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 75 && Final_AVG_3 < 99) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 99 && Final_AVG_3 < 140) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 140) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }


            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     Sindicalizado     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Confianza     ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Ninguno     ###################################################################

                if (Final_AVG_3 < 20) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 20 && Final_AVG_3 < 45) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 45 && Final_AVG_3 < 70) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 70 && Final_AVG_3 < 90) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 90) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Final_AVG_3,
                Total_Surveys_1,
                Total_Surveys_2,
                Total_Surveys_3,
                Final_Beredict_1,
                Final_Beredict_2,
                Final_Beredict_3,
                Color_Nivel_1,
                Color_Nivel_2,
                Color_Nivel_3,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Tipo_Contratacion(int id_empresa, int id_survey)
        {

            //####################################################################################################################
            //                                             TIPO DE CONTRATACION
            //
            //                                          Obra o Proyecto                         = 1   
            //                                          Tiempo Indeterminado - Permanente       = 2 
            //                                          Tiempo Determinado - Temporal           = 3
            //                                          Honorarios                              = 4
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Obra o Proyecto  WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_1 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Contratacion == 1
                                    select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Tiempo Indeterminado - Permanente  WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_2 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Contratacion == 2
                                    select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Tiempo Determinado - Temporal WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_3 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Contratacion == 3
                                    select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Honorarios WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_4 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Contratacion == 4
                                    select G.Calificacion).Sum();


            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Contratacion == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Contratacion == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_3 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Contratacion == 3 select new { G.id_empresa }).Count();
            int Total_Surveys_4 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Contratacion == 4 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_1 / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_2 / Total_Surveys_2;
            int? Final_AVG_3 = Total_Reporte_3 / Total_Surveys_3;
            int? Final_AVG_4 = Total_Reporte_4 / Total_Surveys_4;

            string Color_Nivel_1 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_2 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_3 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_4 = "rgba(0, 0, 0, 1)";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     Obra o Proyecto     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################      Tiempo Indeterminado - Permanente     ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Tiempo Determinado - Temporal     ###################################################################

                if (Final_AVG_3 < 50) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 50 && Final_AVG_3 < 75) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 75 && Final_AVG_3 < 99) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 99 && Final_AVG_3 < 140) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 140) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     Honorarios    ###################################################################

                if (Final_AVG_4 < 50) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 50 && Final_AVG_4 < 75) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 75 && Final_AVG_4 < 99) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 99 && Final_AVG_4 < 140) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 140) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }


            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     Obra o Proyecto     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################      Tiempo Indeterminado - Permanente     ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Tiempo Determinado - Temporal     ###################################################################

                if (Final_AVG_3 < 20) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 20 && Final_AVG_3 < 45) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 45 && Final_AVG_3 < 70) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 70 && Final_AVG_3 < 90) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 90) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     Honorarios    ###################################################################

                if (Final_AVG_4 < 20) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 20 && Final_AVG_4 < 45) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 45 && Final_AVG_4 < 70) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 70 && Final_AVG_4 < 90) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 90) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }


            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Final_AVG_3,
                Final_AVG_4,
                Total_Surveys_1,
                Total_Surveys_2,
                Total_Surveys_3,
                Total_Surveys_4,
                Final_Beredict_1,
                Final_Beredict_2,
                Final_Beredict_3,
                Final_Beredict_4,
                Color_Nivel_1,
                Color_Nivel_2,
                Color_Nivel_3,
                Color_Nivel_4,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Tipo_Puesto(int id_empresa, int id_survey)
        {

            //####################################################################################################################
            //                                             TIPO DE PUESTO
            //
            //                                          Operativo                = 1   
            //                                          Profesional o Técnico    = 2 
            //                                          Supervisor               = 3
            //                                          Gerente                  = 4
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Operativo WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Sin_Info = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Tipo_puesto == 1
                                           select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  Profesional o Técnico WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Primaria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                           join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                           where CT.id_empresa == id_empresa && CT.Tipo_puesto == 2
                                           select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Supervisor WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Secundaria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_empresa == id_empresa && CT.Tipo_puesto == 3
                                             select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Gerente WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_Preparatoria = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                               join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                               where CT.id_empresa == id_empresa && CT.Tipo_puesto == 4
                                               select G.Calificacion).Sum();

            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_puesto == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_puesto == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_3 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_puesto == 3 select new { G.id_empresa }).Count();
            int Total_Surveys_4 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_puesto == 4 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_Sin_Info / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_Primaria / Total_Surveys_2;
            int? Final_AVG_3 = Total_Reporte_Secundaria / Total_Surveys_3;
            int? Final_AVG_4 = Total_Reporte_Preparatoria / Total_Surveys_4;

            string Color_Nivel_1 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_2 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_3 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_4 = "rgba(0, 0, 0, 1)";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     Operativo     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Profesional o Técnico     ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Supervisor     ###################################################################

                if (Final_AVG_3 < 50) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 50 && Final_AVG_3 < 75) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 75 && Final_AVG_3 < 99) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 99 && Final_AVG_3 < 140) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 140) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     Gerente    ###################################################################

                if (Final_AVG_4 < 50) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 50 && Final_AVG_4 < 75) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 75 && Final_AVG_4 < 99) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 99 && Final_AVG_4 < 140) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 140) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }
            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     Operativo     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Profesional o Técnico     ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Supervisor     ###################################################################

                if (Final_AVG_3 < 20) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 20 && Final_AVG_3 < 45) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 45 && Final_AVG_3 < 70) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 70 && Final_AVG_3 < 90) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 90) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     Gerente    ###################################################################

                if (Final_AVG_4 < 20) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 20 && Final_AVG_4 < 45) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 45 && Final_AVG_4 < 70) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 70 && Final_AVG_4 < 90) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 90) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Final_AVG_3,
                Final_AVG_4,
                Total_Surveys_1,
                Total_Surveys_2,
                Total_Surveys_3,
                Total_Surveys_4,
                Final_Beredict_1,
                Final_Beredict_2,
                Final_Beredict_3,
                Final_Beredict_4,
                Color_Nivel_1,
                Color_Nivel_2,
                Color_Nivel_3,
                Color_Nivel_4,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Rotacion_Turno(int id_empresa, int id_survey)
        {

            //####################################################################################################################
            //                                             ROTACION DE JORNADA
            //
            //                                          SI    = 1 
            //                                          NO    = 2 
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Operativo WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_1 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Rotacion_Turno == 1
                                    select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  Profesional o Técnico WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_2 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Rotacion_Turno == 2
                                    select G.Calificacion).Sum();

            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Rotacion_Turno == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Rotacion_Turno == 2 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_1 / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_2 / Total_Surveys_2;

            string Color_Nivel_1 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_2 = "rgba(0, 0, 0, 1)";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     ROTACION SI     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################    ROTACION NO      ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }
            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     ROTACION SI     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################    ROTACION NO      ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Total_Surveys_1,
                Total_Surveys_2,
                Final_Beredict_1,
                Final_Beredict_2,
                Color_Nivel_1,
                Color_Nivel_2,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Tipo_Jornada(int id_empresa, int id_survey)
        {

            //####################################################################################################################
            //                                             TIPO DE JORNADA
            //
            //                                          Fijo Nocturno                = 1   
            //                                          Fijo Diurno    = 2 
            //                                          Fijo Mixto               = 3
            //
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  Fijo Nocturno WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_1 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Jornada == 1
                                    select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Fijo Diurno WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_2 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Jornada == 2
                                    select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL Fijo MixtO WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_3 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                    join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                    where CT.id_empresa == id_empresa && CT.Tipo_Jornada == 3
                                    select G.Calificacion).Sum();

            //#########################################################################################################################################################
            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Jornada == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Jornada == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_3 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Tipo_Jornada == 3 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_1 / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_2 / Total_Surveys_2;
            int? Final_AVG_3 = Total_Reporte_3 / Total_Surveys_3;

            string Color_Nivel_1 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_2 = "rgba(0, 0, 0, 1)";
            string Color_Nivel_3 = "rgba(0, 0, 0, 1)";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     Operativo     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Profesional o Técnico     ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Supervisor     ###################################################################

                if (Final_AVG_3 < 50) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 50 && Final_AVG_3 < 75) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 75 && Final_AVG_3 < 99) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 99 && Final_AVG_3 < 140) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 140) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }
            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     Operativo     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     Profesional o Técnico     ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     Supervisor     ###################################################################

                if (Final_AVG_3 < 20) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 20 && Final_AVG_3 < 45) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 45 && Final_AVG_3 < 70) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 70 && Final_AVG_3 < 90) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 90) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Final_AVG_3,
                Total_Surveys_1,
                Total_Surveys_2,
                Total_Surveys_3,
                Final_Beredict_1,
                Final_Beredict_2,
                Final_Beredict_3,
                Final_Beredict_4,
                Color_Nivel_1,
                Color_Nivel_2,
                Color_Nivel_3,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Searching_Edades(int id_empresa, int id_survey)
        {
            //####################################################################################################################
            //                                              EDADES
            //                                          15 - 19     =   1
            //                                          20 - 24     =   2
            //                                          25 - 29     =   3
            //                                          30 - 34     =   4
            //                                          35 - 39     =   5
            //                                          40 - 44     =   6
            //                                          45 - 49     =   7
            //                                          50 - 54     =   8 
            //                                          55 - 59     =   9
            //                                          60 - 64     =   10 
            //                                          65 - 69     =   11
            //                                          70 o más    =   12
            //####################################################################################################################

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL POINTS IN SURVEYS
            //####################################################################################################################
            string Total_Empresa = "SELECT [dbo].[fnDemo_N035_Final_Empresas] ({0})";
            Object[] parameters = { id_empresa };
            int Final_Sum = db.Database.SqlQuery<int>(Total_Empresa, parameters).FirstOrDefault();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 15 - 19  WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_15_19 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 1
                                        select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 20 - 24 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_20_24 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 2
                                        select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 25 - 29 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_25_29 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 3
                                        select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL  30 - 34 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_30_34 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 4
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 35 - 39 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_35_39 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 5
                                        select G.Calificacion).Sum();

            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 40 - 44 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_40_44 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 6
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 45 - 49 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_45_49 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 7
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 50 - 54  WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_50_54 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 8
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 55 - 59 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_55_59 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 9
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 60 - 64 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_60_64 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 10
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 65 - 69 WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_65_69 = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                        join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                        where CT.id_empresa == id_empresa && CT.Edad == 11
                                        select G.Calificacion).Sum();
            //####################################################################################################################
            //                                      GETTING DATA FROM ALL 70 o más WHO ANSWERED SURVEY
            //####################################################################################################################
            int? Total_Reporte_70_X = (from G in db.ERGOS_Cuestionarios_Resultados_N01
                                       join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on G.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                       where CT.id_empresa == id_empresa && CT.Edad == 12
                                       select G.Calificacion).Sum();
            //####################################################################################################################

            int Total_Surveys = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa select new { G.id_empresa }).Count();
            int Total_Surveys_1 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 1 select new { G.id_empresa }).Count();
            int Total_Surveys_2 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 2 select new { G.id_empresa }).Count();
            int Total_Surveys_3 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 3 select new { G.id_empresa }).Count();
            int Total_Surveys_4 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 4 select new { G.id_empresa }).Count();
            int Total_Surveys_5 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 5 select new { G.id_empresa }).Count();
            int Total_Surveys_6 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 6 select new { G.id_empresa }).Count();
            int Total_Surveys_7 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 7 select new { G.id_empresa }).Count();
            int Total_Surveys_8 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 8 select new { G.id_empresa }).Count();
            int Total_Surveys_9 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 9 select new { G.id_empresa }).Count();
            int Total_Surveys_10 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 10 select new { G.id_empresa }).Count();
            int Total_Surveys_11 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 11 select new { G.id_empresa }).Count();
            int Total_Surveys_12 = (from G in db.ERGOS_Cuestionarios_Trabajador_N01 where G.id_empresa == id_empresa && G.Edad == 12 select new { G.id_empresa }).Count();


            int Final_AVG = Final_Sum / Total_Surveys;
            int? Final_AVG_1 = Total_Reporte_15_19 / Total_Surveys_1;
            int? Final_AVG_2 = Total_Reporte_20_24 / Total_Surveys_2;
            int? Final_AVG_3 = Total_Reporte_25_29 / Total_Surveys_3;
            int? Final_AVG_4 = Total_Reporte_30_34 / Total_Surveys_4;
            int? Final_AVG_5 = Total_Reporte_35_39 / Total_Surveys_5;
            int? Final_AVG_6 = Total_Reporte_40_44 / Total_Surveys_6;
            int? Final_AVG_7 = Total_Reporte_45_49 / Total_Surveys_7;
            int? Final_AVG_8 = Total_Reporte_50_54 / Total_Surveys_8;
            int? Final_AVG_9 = Total_Reporte_55_59 / Total_Surveys_9;
            int? Final_AVG_10 = Total_Reporte_60_64 / Total_Surveys_10;
            int? Final_AVG_11 = Total_Reporte_65_69 / Total_Surveys_11;
            int? Final_AVG_12 = Total_Reporte_70_X / Total_Surveys_12;

            string Color_Nivel_1 = "";
            string Color_Nivel_2 = "";
            string Color_Nivel_3 = "";
            string Color_Nivel_4 = "";
            string Color_Nivel_5 = "";
            string Color_Nivel_6 = "";
            string Color_Nivel_7 = "";
            string Color_Nivel_8 = "";
            string Color_Nivel_9 = "";
            string Color_Nivel_10 = "";
            string Color_Nivel_11 = "";
            string Color_Nivel_12 = "";



            //####################################################################################################################
            //                                                                SURVEY 3
            //####################################################################################################################
            if (id_survey == 3)
            {

                //#################################################     15 - 19     ###################################################################
                if (Final_AVG_1 < 50) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 50 && Final_AVG_1 < 75) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 75 && Final_AVG_1 < 99) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 99 && Final_AVG_1 < 140) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 140) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     20_24     ###################################################################

                if (Final_AVG_2 < 50) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 50 && Final_AVG_2 < 75) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 75 && Final_AVG_2 < 99) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 99 && Final_AVG_2 < 140) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 140) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     25_29     ###################################################################

                if (Final_AVG_3 < 50) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 50 && Final_AVG_3 < 75) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 75 && Final_AVG_3 < 99) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 99 && Final_AVG_3 < 140) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 140) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     30_34    ###################################################################

                if (Final_AVG_4 < 50) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 50 && Final_AVG_4 < 75) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 75 && Final_AVG_4 < 99) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 99 && Final_AVG_4 < 140) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 140) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }

                //#################################################     35_39    ###################################################################

                if (Final_AVG_5 < 50) { Final_Beredict_5 = "Nulo"; Color_Nivel_5 = Color_Nulo; }

                else if (Final_AVG_5 >= 50 && Final_AVG_5 < 75) { Final_Beredict_5 = "Bajo"; Color_Nivel_5 = Color_Bajo; }

                else if (Final_AVG_5 >= 75 && Final_AVG_5 < 99) { Final_Beredict_5 = "Medio"; Color_Nivel_5 = Color_Medio; }

                else if (Final_AVG_5 >= 99 && Final_AVG_5 < 140) { Final_Beredict_5 = "Alto"; Color_Nivel_5 = Color_Alto; }

                else if (Final_AVG_5 >= 140) { Final_Beredict_5 = "Muy Alto"; Color_Nivel_5 = Color_Muy_Alto; }
                //#################################################     40_44    ###################################################################

                if (Final_AVG_6 < 50) { Final_Beredict_6 = "Nulo"; Color_Nivel_6 = Color_Nulo; }

                else if (Final_AVG_6 >= 50 && Final_AVG_6 < 75) { Final_Beredict_6 = "Bajo"; Color_Nivel_6 = Color_Bajo; }

                else if (Final_AVG_6 >= 75 && Final_AVG_6 < 99) { Final_Beredict_6 = "Medio"; Color_Nivel_6 = Color_Medio; }

                else if (Final_AVG_6 >= 99 && Final_AVG_6 < 140) { Final_Beredict_6 = "Alto"; Color_Nivel_6 = Color_Alto; }

                else if (Final_AVG_6 >= 140) { Final_Beredict_6 = "Muy Alto"; Color_Nivel_6 = Color_Muy_Alto; }

                //#################################################     45_49    ###################################################################

                if (Final_AVG_7 < 50) { Final_Beredict_7 = "Nulo"; Color_Nivel_7 = Color_Nulo; }

                else if (Final_AVG_7 >= 50 && Final_AVG_7 < 75) { Final_Beredict_7 = "Bajo"; Color_Nivel_7 = Color_Bajo; }

                else if (Final_AVG_7 >= 75 && Final_AVG_7 < 99) { Final_Beredict_7 = "Medio"; Color_Nivel_7 = Color_Medio; }

                else if (Final_AVG_7 >= 99 && Final_AVG_7 < 140) { Final_Beredict_7 = "Alto"; Color_Nivel_7 = Color_Alto; }

                else if (Final_AVG_7 >= 140) { Final_Beredict_7 = "Muy Alto"; Color_Nivel_7 = Color_Muy_Alto; }

                //#################################################     50_54    ###################################################################

                if (Final_AVG_8 < 50) { Final_Beredict_8 = "Nulo"; Color_Nivel_8 = Color_Nulo; }

                else if (Final_AVG_8 >= 50 && Final_AVG_8 < 75) { Final_Beredict_8 = "Bajo"; Color_Nivel_8 = Color_Bajo; }

                else if (Final_AVG_8 >= 75 && Final_AVG_8 < 99) { Final_Beredict_8 = "Medio"; Color_Nivel_8 = Color_Medio; }

                else if (Final_AVG_8 >= 99 && Final_AVG_8 < 140) { Final_Beredict_8 = "Alto"; Color_Nivel_8 = Color_Alto; }

                else if (Final_AVG_8 >= 140) { Final_Beredict_8 = "Muy Alto"; Color_Nivel_8 = Color_Muy_Alto; }

                //#################################################     55_59    ###################################################################

                if (Final_AVG_9 < 50) { Final_Beredict_9 = "Nulo"; Color_Nivel_9 = Color_Nulo; }

                else if (Final_AVG_9 >= 50 && Final_AVG_9 < 75) { Final_Beredict_9 = "Bajo"; Color_Nivel_9 = Color_Bajo; }

                else if (Final_AVG_9 >= 75 && Final_AVG_9 < 99) { Final_Beredict_9 = "Medio"; Color_Nivel_9 = Color_Medio; }

                else if (Final_AVG_9 >= 99 && Final_AVG_9 < 140) { Final_Beredict_9 = "Alto"; Color_Nivel_9 = Color_Alto; }

                else if (Final_AVG_9 >= 140) { Final_Beredict_9 = "Muy Alto"; Color_Nivel_9 = Color_Muy_Alto; }

                //#################################################     60_64    ###################################################################

                if (Final_AVG_10 < 50) { Final_Beredict_10 = "Nulo"; Color_Nivel_10 = Color_Nulo; }

                else if (Final_AVG_10 >= 50 && Final_AVG_10 < 75) { Final_Beredict_10 = "Bajo"; Color_Nivel_10 = Color_Bajo; }

                else if (Final_AVG_10 >= 75 && Final_AVG_10 < 99) { Final_Beredict_10 = "Medio"; Color_Nivel_10 = Color_Medio; }

                else if (Final_AVG_10 >= 99 && Final_AVG_10 < 140) { Final_Beredict_10 = "Alto"; Color_Nivel_10 = Color_Alto; }

                else if (Final_AVG_10 >= 140) { Final_Beredict_10 = "Muy Alto"; Color_Nivel_10 = Color_Muy_Alto; }

                //#################################################     65_69    ###################################################################

                if (Final_AVG_11 < 50) { Final_Beredict_11 = "Nulo"; Color_Nivel_11 = Color_Nulo; }

                else if (Final_AVG_11 >= 50 && Final_AVG_11 < 75) { Final_Beredict_11 = "Bajo"; Color_Nivel_11 = Color_Bajo; }

                else if (Final_AVG_11 >= 75 && Final_AVG_11 < 99) { Final_Beredict_11 = "Medio"; Color_Nivel_11 = Color_Medio; }

                else if (Final_AVG_11 >= 99 && Final_AVG_11 < 140) { Final_Beredict_11 = "Alto"; Color_Nivel_11 = Color_Alto; }

                else if (Final_AVG_11 >= 140) { Final_Beredict_11 = "Muy Alto"; Color_Nivel_11 = Color_Muy_Alto; }

                //#################################################     70_X    ###################################################################

                if (Final_AVG_12 < 50) { Final_Beredict_12 = "Nulo"; Color_Nivel_12 = Color_Nulo; }

                else if (Final_AVG_12 >= 50 && Final_AVG_12 < 75) { Final_Beredict_12 = "Bajo"; Color_Nivel_12 = Color_Bajo; }

                else if (Final_AVG_12 >= 75 && Final_AVG_12 < 99) { Final_Beredict_12 = "Medio"; Color_Nivel_12 = Color_Medio; }

                else if (Final_AVG_12 >= 99 && Final_AVG_12 < 140) { Final_Beredict_12 = "Alto"; Color_Nivel_12 = Color_Alto; }

                else if (Final_AVG_12 >= 140) { Final_Beredict_12 = "Muy Alto"; Color_Nivel_12 = Color_Muy_Alto; }

            }      //####################################################################################################################
                   //                                                                SURVEY 2
                   //####################################################################################################################
            else if (id_survey == 2)
            {
                //#################################################     15 - 19     ###################################################################
                if (Final_AVG_1 < 20) { Final_Beredict_1 = "Nulo"; Color_Nivel_1 = Color_Nulo; }

                else if (Final_AVG_1 >= 20 && Final_AVG_1 < 45) { Final_Beredict_1 = "Bajo"; Color_Nivel_1 = Color_Bajo; }

                else if (Final_AVG_1 >= 45 && Final_AVG_1 < 70) { Final_Beredict_1 = "Medio"; Color_Nivel_1 = Color_Medio; }

                else if (Final_AVG_1 >= 70 && Final_AVG_1 < 90) { Final_Beredict_1 = "Alto"; Color_Nivel_1 = Color_Alto; }

                else if (Final_AVG_1 >= 90) { Final_Beredict_1 = "Muy Alto"; Color_Nivel_1 = Color_Muy_Alto; }

                //#################################################     20_24     ###################################################################

                if (Final_AVG_2 < 20) { Final_Beredict_2 = "Nulo"; Color_Nivel_2 = Color_Nulo; }

                else if (Final_AVG_2 >= 20 && Final_AVG_2 < 45) { Final_Beredict_2 = "Bajo"; Color_Nivel_2 = Color_Bajo; }

                else if (Final_AVG_2 >= 45 && Final_AVG_2 < 70) { Final_Beredict_2 = "Medio"; Color_Nivel_2 = Color_Medio; }

                else if (Final_AVG_2 >= 70 && Final_AVG_2 < 90) { Final_Beredict_2 = "Alto"; Color_Nivel_2 = Color_Alto; }

                else if (Final_AVG_2 >= 90) { Final_Beredict_2 = "Muy Alto"; Color_Nivel_2 = Color_Muy_Alto; }

                //#################################################     25_29     ###################################################################

                if (Final_AVG_3 < 20) { Final_Beredict_3 = "Nulo"; Color_Nivel_3 = Color_Nulo; }

                else if (Final_AVG_3 >= 20 && Final_AVG_3 < 45) { Final_Beredict_3 = "Bajo"; Color_Nivel_3 = Color_Bajo; }

                else if (Final_AVG_3 >= 45 && Final_AVG_3 < 70) { Final_Beredict_3 = "Medio"; Color_Nivel_3 = Color_Medio; }

                else if (Final_AVG_3 >= 70 && Final_AVG_3 < 90) { Final_Beredict_3 = "Alto"; Color_Nivel_3 = Color_Alto; }

                else if (Final_AVG_3 >= 90) { Final_Beredict_3 = "Muy Alto"; Color_Nivel_3 = Color_Muy_Alto; }

                //#################################################     30_34    ###################################################################

                if (Final_AVG_4 < 20) { Final_Beredict_4 = "Nulo"; Color_Nivel_4 = Color_Nulo; }

                else if (Final_AVG_4 >= 20 && Final_AVG_4 < 45) { Final_Beredict_4 = "Bajo"; Color_Nivel_4 = Color_Bajo; }

                else if (Final_AVG_4 >= 45 && Final_AVG_4 < 70) { Final_Beredict_4 = "Medio"; Color_Nivel_4 = Color_Medio; }

                else if (Final_AVG_4 >= 70 && Final_AVG_4 < 90) { Final_Beredict_4 = "Alto"; Color_Nivel_4 = Color_Alto; }

                else if (Final_AVG_4 >= 90) { Final_Beredict_4 = "Muy Alto"; Color_Nivel_4 = Color_Muy_Alto; }

                //#################################################     35_39    ###################################################################

                if (Final_AVG_5 < 20) { Final_Beredict_5 = "Nulo"; Color_Nivel_5 = Color_Nulo; }

                else if (Final_AVG_5 >= 20 && Final_AVG_5 < 45) { Final_Beredict_5 = "Bajo"; Color_Nivel_5 = Color_Bajo; }

                else if (Final_AVG_5 >= 45 && Final_AVG_5 < 70) { Final_Beredict_5 = "Medio"; Color_Nivel_5 = Color_Medio; }

                else if (Final_AVG_5 >= 70 && Final_AVG_5 < 90) { Final_Beredict_5 = "Alto"; Color_Nivel_5 = Color_Alto; }

                else if (Final_AVG_5 >= 90) { Final_Beredict_5 = "Muy Alto"; Color_Nivel_5 = Color_Muy_Alto; }
                //#################################################     40_44    ###################################################################

                if (Final_AVG_6 < 20) { Final_Beredict_6 = "Nulo"; Color_Nivel_6 = Color_Nulo; }

                else if (Final_AVG_6 >= 20 && Final_AVG_6 < 45) { Final_Beredict_6 = "Bajo"; Color_Nivel_6 = Color_Bajo; }

                else if (Final_AVG_6 >= 45 && Final_AVG_6 < 70) { Final_Beredict_6 = "Medio"; Color_Nivel_6 = Color_Medio; }

                else if (Final_AVG_6 >= 70 && Final_AVG_6 < 90) { Final_Beredict_6 = "Alto"; Color_Nivel_6 = Color_Alto; }

                else if (Final_AVG_6 >= 90) { Final_Beredict_6 = "Muy Alto"; Color_Nivel_6 = Color_Muy_Alto; }

                //#################################################     45_49    ###################################################################

                if (Final_AVG_7 < 20) { Final_Beredict_7 = "Nulo"; Color_Nivel_7 = Color_Nulo; }

                else if (Final_AVG_7 >= 20 && Final_AVG_7 < 45) { Final_Beredict_7 = "Bajo"; Color_Nivel_7 = Color_Bajo; }

                else if (Final_AVG_7 >= 45 && Final_AVG_7 < 70) { Final_Beredict_7 = "Medio"; Color_Nivel_7 = Color_Medio; }

                else if (Final_AVG_7 >= 70 && Final_AVG_7 < 90) { Final_Beredict_7 = "Alto"; Color_Nivel_7 = Color_Alto; }

                else if (Final_AVG_7 >= 90) { Final_Beredict_7 = "Muy Alto"; Color_Nivel_7 = Color_Muy_Alto; }

                //#################################################     20_54    ###################################################################

                if (Final_AVG_8 < 20) { Final_Beredict_8 = "Nulo"; Color_Nivel_8 = Color_Nulo; }

                else if (Final_AVG_8 >= 20 && Final_AVG_8 < 45) { Final_Beredict_8 = "Bajo"; Color_Nivel_8 = Color_Bajo; }

                else if (Final_AVG_8 >= 45 && Final_AVG_8 < 70) { Final_Beredict_8 = "Medio"; Color_Nivel_8 = Color_Medio; }

                else if (Final_AVG_8 >= 70 && Final_AVG_8 < 90) { Final_Beredict_8 = "Alto"; Color_Nivel_8 = Color_Alto; }

                else if (Final_AVG_8 >= 90) { Final_Beredict_8 = "Muy Alto"; Color_Nivel_8 = Color_Muy_Alto; }

                //#################################################     55_59    ###################################################################

                if (Final_AVG_9 < 20) { Final_Beredict_9 = "Nulo"; Color_Nivel_9 = Color_Nulo; }

                else if (Final_AVG_9 >= 20 && Final_AVG_9 < 45) { Final_Beredict_9 = "Bajo"; Color_Nivel_9 = Color_Bajo; }

                else if (Final_AVG_9 >= 45 && Final_AVG_9 < 70) { Final_Beredict_9 = "Medio"; Color_Nivel_9 = Color_Medio; }

                else if (Final_AVG_9 >= 70 && Final_AVG_9 < 90) { Final_Beredict_9 = "Alto"; Color_Nivel_9 = Color_Alto; }

                else if (Final_AVG_9 >= 90) { Final_Beredict_9 = "Muy Alto"; Color_Nivel_9 = Color_Muy_Alto; }

                //#################################################     60_64    ###################################################################

                if (Final_AVG_10 < 20) { Final_Beredict_10 = "Nulo"; Color_Nivel_10 = Color_Nulo; }

                else if (Final_AVG_10 >= 20 && Final_AVG_10 < 45) { Final_Beredict_10 = "Bajo"; Color_Nivel_10 = Color_Bajo; }

                else if (Final_AVG_10 >= 45 && Final_AVG_10 < 70) { Final_Beredict_10 = "Medio"; Color_Nivel_10 = Color_Medio; }

                else if (Final_AVG_10 >= 70 && Final_AVG_10 < 90) { Final_Beredict_10 = "Alto"; Color_Nivel_10 = Color_Alto; }

                else if (Final_AVG_10 >= 90) { Final_Beredict_10 = "Muy Alto"; Color_Nivel_10 = Color_Muy_Alto; }

                //#################################################     65_69    ###################################################################

                if (Final_AVG_11 < 20) { Final_Beredict_11 = "Nulo"; Color_Nivel_11 = Color_Nulo; }

                else if (Final_AVG_11 >= 20 && Final_AVG_11 < 45) { Final_Beredict_11 = "Bajo"; Color_Nivel_11 = Color_Bajo; }

                else if (Final_AVG_11 >= 45 && Final_AVG_11 < 70) { Final_Beredict_11 = "Medio"; Color_Nivel_11 = Color_Medio; }

                else if (Final_AVG_11 >= 70 && Final_AVG_11 < 90) { Final_Beredict_11 = "Alto"; Color_Nivel_11 = Color_Alto; }

                else if (Final_AVG_11 >= 90) { Final_Beredict_11 = "Muy Alto"; Color_Nivel_11 = Color_Muy_Alto; }

                //#################################################     70_X    ###################################################################

                if (Final_AVG_12 < 20) { Final_Beredict_12 = "Nulo"; Color_Nivel_12 = Color_Nulo; }

                else if (Final_AVG_12 >= 20 && Final_AVG_12 < 45) { Final_Beredict_12 = "Bajo"; Color_Nivel_12 = Color_Bajo; }

                else if (Final_AVG_12 >= 45 && Final_AVG_12 < 70) { Final_Beredict_12 = "Medio"; Color_Nivel_12 = Color_Medio; }

                else if (Final_AVG_12 >= 70 && Final_AVG_12 < 90) { Final_Beredict_12 = "Alto"; Color_Nivel_12 = Color_Alto; }

                else if (Final_AVG_12 >= 90) { Final_Beredict_12 = "Muy Alto"; Color_Nivel_12 = Color_Muy_Alto; }
            }

            var result = new
            {
                Total_Surveys,
                Final_AVG_1,
                Final_AVG_2,
                Final_AVG_3,
                Final_AVG_4,
                Final_AVG_5,
                Final_AVG_6,
                Final_AVG_7,
                Final_AVG_8,
                Final_AVG_9,
                Final_AVG_10,
                Final_AVG_11,
                Final_AVG_12,
                Total_Surveys_1,
                Total_Surveys_2,
                Total_Surveys_3,
                Total_Surveys_4,
                Total_Surveys_5,
                Total_Surveys_6,
                Total_Surveys_7,
                Total_Surveys_8,
                Total_Surveys_9,
                Total_Surveys_10,
                Total_Surveys_12,
                Final_Beredict_1,
                Final_Beredict_2,
                Final_Beredict_3,
                Final_Beredict_4,
                Final_Beredict_5,
                Final_Beredict_6,
                Final_Beredict_7,
                Final_Beredict_8,
                Final_Beredict_9,
                Final_Beredict_10,
                Final_Beredict_11,
                Final_Beredict_12,
                Color_Nivel_1,
                Color_Nivel_2,
                Color_Nivel_3,
                Color_Nivel_4,
                Color_Nivel_5,
                Color_Nivel_6,
                Color_Nivel_7,
                Color_Nivel_8,
                Color_Nivel_9,
                Color_Nivel_10,
                Color_Nivel_11,
                Color_Nivel_12
            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        // GET: Empresas/Details/5
          
        public ActionResult Missing_Info()
        {
            return View();
        }


        public void Colores_Dimensiones(int? id)
        {



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
        [Authorize(Roles = "Admin-Guest")]
        public ActionResult General_Statistics_Admin_Guest()
        {
            string UserName = User.Identity.Name;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName && E.id_rol == 3 select E.id_empresa).FirstOrDefault();
            int? id_encuesta = (from E in db.ERGOS_Empresas_N01 where E.id_empresa == id_empresa select E.id_encuesta).FirstOrDefault();
            ViewBag.Survey = id_encuesta;
            ViewBag.id_empresa = id_empresa;

            ViewBag.Company = (from Company in db.ERGOS_Empresas_N01
                               where Company.id_empresa == id_empresa
                               select Company.Razon_Social).FirstOrDefault();
            ViewBag.Canalizados = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_empresa == id_empresa
                                   select Emp_Canalizados.Canalizacion).Count();

            try
            {
                string sqlQuery = "SELECT [dbo].[fnDemo_N035_Final_Centro_Trabajo] ({0})";
                Object[] parameters = { id_empresa };
                int activityCount = db.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                ViewBag.Final_Average = activityCount;

                if (id_encuesta == 3)
                {
                    ViewBag.Total_Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CR.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_encuesta == 3 && CT.id_empresa == id_empresa
                                             group CR by CR.id_pregunta into g
                                             orderby g.Sum(X => X.id_pregunta)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = g.Sum(X => (int?)X.Calificacion ?? 0)
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = (float)G.Sumatoria_Total / (float)G.Encuestados,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Total
                                            }).FirstOrDefault();


                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = (float)G.Sumatoria_Cat_II / (float)G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = (float)G.Sumatoria_Cat_III / (float)G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = (float)G.Sumatoria_Cat_IV / (float)G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();

                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_5_G = (float)G.Sumatoria_Cat_V / (float)G.Encuestados,
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


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_8_G = G.Total_Dominio_8
                                            }).FirstOrDefault();
                    ViewBag.Dom_9_Global = (from G in db.fnDemo_N035_Dominios_9_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_9_G = G.Total_Dominio_9
                                            }).FirstOrDefault();
                    ViewBag.Dom_10_Global = (from G in db.fnDemo_N035_Dominios_10_Resultados(id_empresa)
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

                    ViewBag.Dim_1_Global = (from G in db.fnDemo_N035_Dimension_1_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_1_G = G.Total_Dimension_1 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_2_Global = (from G in db.fnDemo_N035_Dimension_2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_2_G = G.Total_Dimension_2 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_3_Global = (from G in db.fnDemo_N035_Dimension_3_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_3_G = G.Total_Dimension_3
                                            }).FirstOrDefault();
                    ViewBag.Dim_4_Global = (from G in db.fnDemo_N035_Dimension_4_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_4_G = G.Total_Dimension_4 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_5_Global = (from G in db.fnDemo_N035_Dimension_5_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_5_G = G.Total_Dimension_5 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_6_Global = (from G in db.fnDemo_N035_Dimension_6_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_6_G = G.Total_Dimension_6 / 3
                                            }).FirstOrDefault();
                    ViewBag.Dim_7_Global = (from G in db.fnDemo_N035_Dimension_7_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_7_G = G.Total_Dimension_7 / 4
                                            }).FirstOrDefault();
                    ViewBag.Dim_8_Global = (from G in db.fnDemo_N035_Dimension_8_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_8_G = G.Total_Dimension_8 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_9_Global = (from G in db.fnDemo_N035_Dimension_9_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dimension_9_G = G.Total_Dimension_9 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_10_Global = (from G in db.fnDemo_N035_Dimension_10_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_10_G = G.Total_Dimension_10 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_11_Global = (from G in db.fnDemo_N035_Dimension_11_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_11_G = G.Total_Dimension_11 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_12_Global = (from G in db.fnDemo_N035_Dimension_12_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_12_G = G.Total_Dimension_12 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_13_Global = (from G in db.fnDemo_N035_Dimension_13_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_13_G = G.Total_Dimension_13 / 2
                                             }).FirstOrDefault();
                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_14_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_14_Resultados(id_empresa)
                                                    select new Respuestas
                                                    {
                                                        Dimension_14_G = G.Total_Dimension_14
                                                    }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_14_Global = (from G in db.fnDemo_N035_Dimension_14_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_14_G = G.Total_Dimension_14 / 2
                                             }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_15_Global = (from G in db.fnDemo_N035_Dimension_15_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_15_G = G.Total_Dimension_15 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_16_Global = (from G in db.fnDemo_N035_Dimension_16_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_16_G = G.Total_Dimension_16 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_17_Global = (from G in db.fnDemo_N035_Dimension_17_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_17_G = G.Total_Dimension_17 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_18_Global = (from G in db.fnDemo_N035_Dimension_18_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_18_G = G.Total_Dimension_18 / 5
                                             }).FirstOrDefault();
                    ViewBag.Dim_19_Global = (from G in db.fnDemo_N035_Dimension_19_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_19_G = G.Total_Dimension_19 / 5
                                             }).FirstOrDefault();
                    ViewBag.Dim_20_Global = (from G in db.fnDemo_N035_Dimension_20_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_20_G = G.Total_Dimension_20 / 4
                                             }).FirstOrDefault();


                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_21_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_21_Resultados(id_empresa)
                                                    select new Respuestas
                                                    {
                                                        Dimension_21_G = G.Total_Dimension_21
                                                    }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_21_Global = (from G in db.fnDemo_N035_Dimension_21_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_21_G = G.Total_Dimension_21 / 8
                                             }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################
                    ViewBag.Dim_22_Global = (from G in db.fnDemo_N035_Dimension_22_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_22_G = G.Total_Dimension_22 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_23_Global = (from G in db.fnDemo_N035_Dimension_23_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_23_G = G.Total_Dimension_23 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_24_Global = (from G in db.fnDemo_N035_Dimension_24_Resultados(id_empresa)
                                             select new Respuestas
                                             {
                                                 Dimension_24_G = G.Total_Dimension_24 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_25_Global = (from G in db.fnDemo_N035_Dimension_25_Resultados(id_empresa)
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
                                             where CT.id_encuesta == 2 && CT.id_empresa == id_empresa
                                             group CR by CR.id_pregunta into g
                                             orderby g.Sum(X => X.id_pregunta)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = g.Sum(X => (int?)X.Calificacion ?? 0)
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_E2_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = (float)G.Sumatoria_Cat_I / (float)G.Encuestados,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_I
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_E2_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = (float)G.Sumatoria_Cat_II / (float)G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_E2_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = (float)G.Sumatoria_Cat_III / (float)G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_E2_Resultados_Pilot(id_empresa)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = (float)G.Sumatoria_Cat_IV / (float)G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();


                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot(id_empresa)
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


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_E2_Resultados(id_empresa)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_E2_Resultados(id_empresa)
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


                    Colores_Dimensiones(id_empresa);

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
            return View();
        }
        [Authorize(Roles = "Admin,Admin_SyS")]
        public ActionResult General_Statistics(int id, int id_encuesta)
        {
            ViewBag.Survey = id_encuesta;
            ViewBag.id_empresa = id;

            ViewBag.Company = (from Company in db.ERGOS_Empresas_N01
                               where Company.id_empresa == id
                               select Company.Razon_Social).FirstOrDefault();
            ViewBag.Canalizados = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_empresa == id
                                   select Emp_Canalizados.Canalizacion).Count();

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


                    Colores_Dimensiones(id);

                }
                else
                {
                    return RedirectToAction("Missing_Info");
                }

            }
            catch (Exception ex)
            {
                  throw ex;
                //return RedirectToAction("Missing_Info");
            }
            return View();
        }



        [Authorize(Roles = "Admin,Admin_SyS")]
        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01.Where(x =>x.id_cuestionario != 4).Where(x => x.id_cuestionario != 5), "id_cuestionario", "Cuestionario");

            return View();
        }
        [Authorize(Roles = "Admin,Admin_SyS")]

        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empresa,RFC,Domicilio,Actividad_Principal,Razon_Social,Telefono,Contacto_Nombre,Email,id_encuesta,created_at,updated_at,Fecha_Aplicacion")] ERGOS_Empresas_N01 eRGOS_Empresas_N01)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                eRGOS_Empresas_N01.created_at = today;
                eRGOS_Empresas_N01.updated_at = today;
                db.ERGOS_Empresas_N01.Add(eRGOS_Empresas_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eRGOS_Empresas_N01);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);
            if (eRGOS_Empresas_N01 == null)
            {
                return HttpNotFound();
            }

            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Empresas_N01.id_encuesta);

            return View(eRGOS_Empresas_N01);
        }

        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empresa,RFC,Domicilio,Actividad_Principal,Razon_Social,Telefono,Contacto_Nombre,Email,id_encuesta,updated_at,Fecha_Aplicacion")] ERGOS_Empresas_N01 eRGOS_Empresas_N01)
        {
            if (ModelState.IsValid)
            {

                DateTime today = DateTime.Today;
                eRGOS_Empresas_N01.updated_at = today;
                db.Entry(eRGOS_Empresas_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


                //db.RFQ_Quotes_N01.Attach(rFQ_Quotes_N01);
                //db.Entry(rFQ_Quotes_N01).Property(x => x.PIC_path).IsModified = true;
                //db.SaveChanges();
                //return RedirectToAction("Index");

            }
            return View(eRGOS_Empresas_N01);
        }

        [Authorize(Roles = "Admin,Admin_SyS")]
        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);
            if (eRGOS_Empresas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Empresas_N01);
        }
        [Authorize(Roles = "Admin,Admin_SyS")]

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);

            // db.ERGOS_Empresas_N01.Remove(eRGOS_Empresas_N01);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            //db.Entry(eRGOS_Empresas_N01).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Index");

            DateTime today = DateTime.Today;
            eRGOS_Empresas_N01.deleted_at = today;
            db.ERGOS_Empresas_N01.Attach(eRGOS_Empresas_N01);
            db.Entry(eRGOS_Empresas_N01).Property(x => x.deleted_at).IsModified = true;
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
