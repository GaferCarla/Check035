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
    public class Reportes_CentroController : Controller
    {
        private Check035Entities db = new Check035Entities();
        // GET: Reportes_Centro
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReCOV9iuhgOwgfttJ6fobQArasdfg5dh7e3auhgfd656mi98uhngvkcos8uhyrnjgfd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //COVID_Cuestionario_Resultados_N01 cOVID_Cuestionario_Resultados_N01 = db.COVID_Cuestionario_Resultados_N01.Find(id);
            //if (cOVID_Cuestionario_Resultados_N01 == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "Nombre", cOVID_Cuestionario_Resultados_N01.id_cuestionario_trabajador);
            return View(/*cOVID_Cuestionario_Resultados_N01*/);
        }

        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                     TESTING COVID-19 PDF
        //
        //***********************************************************************************************************************************************************************************************************************************************************
        public ActionResult Covid_19()
        {
            return View();
        }

        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA LOS RESULTADOS GENERALES EN PDF 
        //
        //
        public void Colores_Dimensiones(int id)
        {

            //##############################################################################################################################
            // ######################################################  INICIO CALCULO Dimension #############################################
            //##############################################################################################################################

            ViewBag.Dim_E2_1_Global = (from G in db.fnDemo_N035_Dimension_1_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_1_G = G.Total_Dimension_1
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_2_Global = (from G in db.fnDemo_N035_Dimension_2_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_2_G = G.Total_Dimension_2
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_3_Global = (from G in db.fnDemo_N035_Dimension_3_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_3_G = G.Total_Dimension_3
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_4_Global = (from G in db.fnDemo_N035_Dimension_4_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_4_G = G.Total_Dimension_4 / 2
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_5_Global = (from G in db.fnDemo_N035_Dimension_5_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_5_G = G.Total_Dimension_5 / 2
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_6_Global = (from G in db.fnDemo_N035_Dimension_6_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_6_G = G.Total_Dimension_6 / 2
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_7_Global = (from G in db.fnDemo_N035_Dimension_7_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_7_G = G.Total_Dimension_7 / 3
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_8_Global = (from G in db.fnDemo_N035_Dimension_8_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_8_G = G.Total_Dimension_8 / 2
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_9_Global = (from G in db.fnDemo_N035_Dimension_9_E2_Resultados_CT(id)
                                       select new Respuestas
                                       {
                                           Dimension_9_G = G.Total_Dimension_9 / 2
                                       }).FirstOrDefault();
            ViewBag.Dim_E2_10_Global = (from G in db.fnDemo_N035_Dimension_10_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_10_G = G.Total_Dimension_10 / 3
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_11_Global = (from G in db.fnDemo_N035_Dimension_11_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_11_G = G.Total_Dimension_11 / 2
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_12_Global = (from G in db.fnDemo_N035_Dimension_12_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_12_G = G.Total_Dimension_12 / 2
                                        }).FirstOrDefault();
            //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

            ViewBag.Dim_E2_13_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_13_E2_Resultados_CT(id)
                                               select new Respuestas
                                               {
                                                   Dimension_13_G = G.Total_Dimension_13
                                               }).FirstOrDefault();
            //----------------------------------------------------------------------------------------
            ViewBag.Dim_E2_13_Global = (from G in db.fnDemo_N035_Dimension_13_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_13_G = G.Total_Dimension_13 / 2
                                        }).FirstOrDefault();
            //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################

            ViewBag.Dim_E2_14_Global = (from G in db.fnDemo_N035_Dimension_14_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_14_G = G.Total_Dimension_14
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_15_Global = (from G in db.fnDemo_N035_Dimension_15_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_15_G = G.Total_Dimension_15
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_16_Global = (from G in db.fnDemo_N035_Dimension_16_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_16_G = G.Total_Dimension_16 / 3
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_17_Global = (from G in db.fnDemo_N035_Dimension_17_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_17_G = G.Total_Dimension_17 / 2
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_18_Global = (from G in db.fnDemo_N035_Dimension_18_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_18_G = G.Total_Dimension_18 / 3
                                        }).FirstOrDefault();
            ViewBag.Dim_E2_19_Global = (from G in db.fnDemo_N035_Dimension_19_E2_Resultados_CT(id)
                                        select new Respuestas
                                        {
                                            Dimension_19_G = G.Total_Dimension_19 / 3
                                        }).FirstOrDefault();


            //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

            ViewBag.Dim_E2_20_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_20_E2_Resultados_CT(id)
                                               select new Respuestas
                                               {
                                                   Dimension_20_G = G.Total_Dimension_20
                                               }).FirstOrDefault();

            //----------------------------------------------------------------------------------------
            ViewBag.Dim_E2_20_Global = (from G in db.fnDemo_N035_Dimension_20_E2_Resultados_CT(id)
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
        public ActionResult ReGCTOwgfttJ6fobQArasdfg5dh7e3auhgfd656mxghhgf6hghIhma4Zpgf4osLw(int id, int id_encuesta)
        {
            ViewBag.Survey = id_encuesta;
            ViewBag.id_empresa = id;

            ViewBag.Company = (from Company in db.ERGOS_Centros_Trabajo_N01
                               where Company.id_centro_trabajo == id
                               select Company.ERGOS_Empresas_N01.Razon_Social).FirstOrDefault();



            ViewBag.Centro_Trabajo = (from Company in db.ERGOS_Centros_Trabajo_N01
                                      where Company.id_centro_trabajo == id
                                      select Company.Nombre_centro_trabajo).FirstOrDefault();

            ViewBag.Getting_canalizados = (from Empleado_C in db.ERGOS_Cuestionarios_Trabajador_N01
                                           where Empleado_C.id_centro_trabajo == id && Empleado_C.Canalizacion == 1
                                           select new Getting_canalizados { ID = Empleado_C.id_trabajador });

            ViewBag.Canalizados = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_centro_trabajo == id
                                   select Emp_Canalizados.Canalizacion).Count();
            //////////////////////////////////  CANALIZADOS ////////////////////////
            ViewBag.Canalizados = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                   where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_centro_trabajo == id
                                   select Emp_Canalizados.Canalizacion).Count();

            ViewBag.Canalizados_M = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                     where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_centro_trabajo == id && Emp_Canalizados.Sexo == 1
                                     select Emp_Canalizados.Canalizacion).Count();

            ViewBag.Canalizados_F = (from Emp_Canalizados in db.ERGOS_Cuestionarios_Trabajador_N01
                                     where Emp_Canalizados.Canalizacion == 1 && Emp_Canalizados.id_centro_trabajo == id && Emp_Canalizados.Sexo == 2
                                     select Emp_Canalizados.Canalizacion).Count();
            //////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                string sqlQuery = "SELECT [dbo].[fnDemo_N035_Final_Centro_Trabajo] ({0})";
                Object[] parameters = { id };
                int activityCount = db.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                ViewBag.Final_Average = activityCount;

                if (id_encuesta == 3)
                {
                    ViewBag.Total_Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                             join CT in db.ERGOS_Cuestionarios_Trabajador_N01 on CR.id_cuestionario_trabajador equals CT.id_cuestionario_trabajador
                                             where CT.id_encuesta == 3 && CT.id_centro_trabajo == id
                                             group CR by CR.id_pregunta into g
                                             orderby g.Sum(X => X.id_pregunta)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = g.Sum(X => (int?)X.Calificacion ?? 0)
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = G.Total_Categoria_1,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Total
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = G.Total_Categoria_2,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = G.Total_Categoria_3,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = G.Total_Categoria_4,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();

                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot_CT(id)
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


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_8_G = G.Total_Dominio_8
                                            }).FirstOrDefault();
                    ViewBag.Dom_9_Global = (from G in db.fnDemo_N035_Dominios_9_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_9_G = G.Total_Dominio_9
                                            }).FirstOrDefault();
                    ViewBag.Dom_10_Global = (from G in db.fnDemo_N035_Dominios_10_Resultados_CT(id)
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

                    ViewBag.Dim_1_Global = (from G in db.fnDemo_N035_Dimension_1_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_1_G = G.Total_Dimension_1 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_2_Global = (from G in db.fnDemo_N035_Dimension_2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_2_G = G.Total_Dimension_2 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_3_Global = (from G in db.fnDemo_N035_Dimension_3_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_3_G = G.Total_Dimension_3
                                            }).FirstOrDefault();
                    ViewBag.Dim_4_Global = (from G in db.fnDemo_N035_Dimension_4_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_4_G = G.Total_Dimension_4 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_5_Global = (from G in db.fnDemo_N035_Dimension_5_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_5_G = G.Total_Dimension_5 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_6_Global = (from G in db.fnDemo_N035_Dimension_6_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_6_G = G.Total_Dimension_6 / 3
                                            }).FirstOrDefault();
                    ViewBag.Dim_7_Global = (from G in db.fnDemo_N035_Dimension_7_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_7_G = G.Total_Dimension_7 / 4
                                            }).FirstOrDefault();
                    ViewBag.Dim_8_Global = (from G in db.fnDemo_N035_Dimension_8_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_8_G = G.Total_Dimension_8 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_9_Global = (from G in db.fnDemo_N035_Dimension_9_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dimension_9_G = G.Total_Dimension_9 / 2
                                            }).FirstOrDefault();
                    ViewBag.Dim_10_Global = (from G in db.fnDemo_N035_Dimension_10_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_10_G = G.Total_Dimension_10 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_11_Global = (from G in db.fnDemo_N035_Dimension_11_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_11_G = G.Total_Dimension_11 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_12_Global = (from G in db.fnDemo_N035_Dimension_12_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_12_G = G.Total_Dimension_12 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_13_Global = (from G in db.fnDemo_N035_Dimension_13_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_13_G = G.Total_Dimension_13 / 2
                                             }).FirstOrDefault();
                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_14_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_14_Resultados_CT(id)
                                                    select new Respuestas
                                                    {
                                                        Dimension_14_G = G.Total_Dimension_14
                                                    }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_14_Global = (from G in db.fnDemo_N035_Dimension_14_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_14_G = G.Total_Dimension_14 / 2
                                             }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_15_Global = (from G in db.fnDemo_N035_Dimension_15_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_15_G = G.Total_Dimension_15 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_16_Global = (from G in db.fnDemo_N035_Dimension_16_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_16_G = G.Total_Dimension_16 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_17_Global = (from G in db.fnDemo_N035_Dimension_17_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_17_G = G.Total_Dimension_17 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_18_Global = (from G in db.fnDemo_N035_Dimension_18_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_18_G = G.Total_Dimension_18 / 5
                                             }).FirstOrDefault();
                    ViewBag.Dim_19_Global = (from G in db.fnDemo_N035_Dimension_19_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_19_G = G.Total_Dimension_19 / 5
                                             }).FirstOrDefault();
                    ViewBag.Dim_20_Global = (from G in db.fnDemo_N035_Dimension_20_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_20_G = G.Total_Dimension_20 / 4
                                             }).FirstOrDefault();


                    //########################################### CALCULO DIMENSION APEGADO A NOM035   ######################################################################

                    ViewBag.Dim_21_Global_NOM035 = (from G in db.fnDemo_N035_Dimension_21_Resultados_CT(id)
                                                    select new Respuestas
                                                    {
                                                        Dimension_21_G = G.Total_Dimension_21
                                                    }).FirstOrDefault();
                    //----------------------------------------------------------------------------------------
                    ViewBag.Dim_21_Global = (from G in db.fnDemo_N035_Dimension_21_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_21_G = G.Total_Dimension_21 / 8
                                             }).FirstOrDefault();
                    //########################################### FIN CALCULO DIMENSION APEGADO A NOM035   ######################################################################
                    ViewBag.Dim_22_Global = (from G in db.fnDemo_N035_Dimension_22_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_22_G = G.Total_Dimension_22 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_23_Global = (from G in db.fnDemo_N035_Dimension_23_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_23_G = G.Total_Dimension_23 / 4
                                             }).FirstOrDefault();
                    ViewBag.Dim_24_Global = (from G in db.fnDemo_N035_Dimension_24_Resultados_CT(id)
                                             select new Respuestas
                                             {
                                                 Dimension_24_G = G.Total_Dimension_24 / 2
                                             }).FirstOrDefault();
                    ViewBag.Dim_25_Global = (from G in db.fnDemo_N035_Dimension_25_Resultados_CT(id)
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
                                             where CT.id_encuesta == 2 && CT.id_centro_trabajo == id
                                             group CR by CR.id_pregunta into g
                                             orderby g.Sum(X => X.id_pregunta)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = g.Sum(X => (int?)X.Calificacion ?? 0)
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_E2_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = G.Total_Categoria_1,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_I
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_E2_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = G.Total_Categoria_2,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_E2_Resultados_Pilot_CT(id)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = G.Total_Categoria_3,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_E2_Resultados_Pilot_CT(id)
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


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_E2_Resultados_CT(id)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_E2_Resultados_CT(id)
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
               // throw ex;
                return RedirectToAction("Missing_Info");
            }
            return View();
        }

        public ActionResult Missing_Info() {
            return View();
        }
        public ActionResult ReGCT_CL098UHBVCFRYUasasOwgfttJ6fobQArasdfg5dh7e3fgdgfggh65gdgfpgf4osLw(int id_e, int id_c)
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
                return View(db.ERGOS_Centros_Trabajo_N01.Find(id_e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ReGCT_E360OwgfDFGHJKJHGFDgf6hgh76TFD44osLw(int id_e, int id_c)
        {
            try
            {
                 
                var Final_Empresa = db.fn_MultiEvalua_E360_Centro_Trabajo(id_e,id_c).FirstOrDefault();
                var Final_Empresa_Com = db.fn_MultiEvalua_E360_Centro_Trabajo_Com(id_e, id_c).FirstOrDefault();
                var Final_Empresa_Sup = db.fn_MultiEvalua_E360_Centro_Trabajo_Sup(id_e, id_c).FirstOrDefault();
                var Final_Empresa_Sub = db.fn_MultiEvalua_E360_Centro_Trabajo_Sub(id_e, id_c).FirstOrDefault();
                var Final_Empresa_Emp = db.fn_MultiEvalua_E360_Centro_Trabajo_Emp(id_e, id_c).FirstOrDefault(); 

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

                return View(db.ERGOS_Centros_Trabajo_N01.Find(id_c));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA LOS RESULTADOS GENERALES EN EXCEL CENTRO DE TRABAJO 
        //
        //***********************************************************************************************************************************************************************************************************************************************************



        [HttpGet]
        public void ExportToExcel(int id_centro_trabajo)
        {
            List<ERGOS_Cuestionarios_Resultados_N01> emplist = db.ERGOS_Cuestionarios_Resultados_N01.Where(e => e.ERGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo == id_centro_trabajo).ToList();
            int? id_cuestionario = (from E in db.ERGOS_Centros_Trabajo_N01
                                    where E.id_centro_trabajo == id_centro_trabajo
                                    select E.ERGOS_Empresas_N01.id_encuesta).FirstOrDefault();

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
                    
                    if (item.ERGOS_Cuestionarios_Trabajador_N01.id_empresa is null)
                    { Preguntas.Cells[string.Format("I{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas.Cells[string.Format("I{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.ERGOS_Empresas_N01.Razon_Social; }

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
                Preguntas_S2.Cells["I2"].Value = "Empresa";
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


                    if (item.ERGOS_Cuestionarios_Trabajador_N01.id_empresa is null)
                    { Preguntas_S2.Cells[string.Format("I{0}", rowStart)].Value = "Sin Información"; }
                    else { Preguntas_S2.Cells[string.Format("I{0}", rowStart)].Value = item.ERGOS_Cuestionarios_Trabajador_N01.ERGOS_Empresas_N01.Razon_Social; }


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
    }
}
