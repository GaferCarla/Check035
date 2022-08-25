using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ErgoSalud.Models;

namespace ErgoSalud.Controllers
{
    public class Trabajador_ResultadosController : Controller
    {
        private Check035Entities db = new Check035Entities();

        // GET: Trabajador_Resultados
        public ActionResult Index()
        {
            var eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Include(e => e.ERGOS_Cuestionarios_Trabajador_N01).Include(e => e.ERGOS_Preguntas_N01).Include(e => e.ERGOS_Respuestas_N01);

            return View(eRGOS_Cuestionarios_Resultados_N01.ToList());
        }


        public JsonResult Saving_Answer(int? id_CR, int? Respuesta)
        {
            //db.Database.ExecuteSqlCommand("EXECUTE Answering_Survey @id_CR =" + id_CR + ", @Respuesta = " + Respuesta );
            //db.SaveChanges();

            string mensaje = "Exito";

            return Json(new { mensaje = mensaje });
        }

        //[HttpPost]
        //public JsonResult cambio_logica_guardado(int[] id_CR, int[] id_R)
        //{ 
        //    try {
        //        var commandText = "EXECUTE Answering_Survey_SEP_2020_S2 " +
        //          "@id_CR_1 = @id_CR_1,@id_CR_2 = @id_CR_2,@id_CR_3 = @id_CR_3,@id_CR_4 = @id_CR_4,@id_CR_5 = @id_CR_5,@id_CR_6 = @id_CR_6,@id_CR_7 = @id_CR_7,@id_CR_8 = @id_CR_8,@id_CR_9 = @id_CR_9,@id_CR_10 = @id_CR_10" +
        //          ",@id_CR_11 = @id_CR_11,@id_CR_12 = @id_CR_12,@id_CR_13 = @id_CR_13,@id_CR_14 = @id_CR_14,@id_CR_15 = @id_CR_15,@id_CR_16 = @id_CR_16,@id_CR_17 = @id_CR_17,@id_CR_18 = @id_CR_18,@id_CR_19 = @id_CR_19,@id_CR_20 = @id_CR_20" +
        //          ",@id_CR_21 = @id_CR_21,@id_CR_22 = @id_CR_22,@id_CR_23 = @id_CR_23,@id_CR_24 = @id_CR_24,@id_CR_25 = @id_CR_25,@id_CR_26 = @id_CR_26,@id_CR_27 = @id_CR_27,@id_CR_28 = @id_CR_28,@id_CR_29 = @id_CR_29,@id_CR_30 = @id_CR_30" +
        //          ",@id_CR_31 = @id_CR_31,@id_CR_32 = @id_CR_32,@id_CR_33 = @id_CR_33,@id_CR_34 = @id_CR_34,@id_CR_35 = @id_CR_35,@id_CR_36 = @id_CR_36,@id_CR_37 = @id_CR_37,@id_CR_38 = @id_CR_38,@id_CR_39 = @id_CR_39,@id_CR_40 = @id_CR_40" +
        //          ",@id_CR_41 = @id_CR_41,@id_CR_42 = @id_CR_42,@id_CR_43 = @id_CR_43,@id_CR_44 = @id_CR_44,@id_CR_45 = @id_CR_45,@id_CR_46 = @id_CR_46,@id_CR_47 = @id_CR_47,@id_CR_48 = @id_CR_48,@id_CR_49 = @id_CR_49,@id_CR_50 = @id_CR_50" +
        //          ",@id_CR_51 = @id_CR_51,@id_CR_52 = @id_CR_52,@id_CR_53 = @id_CR_53,@id_CR_54 = @id_CR_54,@id_CR_55 = @id_CR_55,@id_CR_56 = @id_CR_56,@id_CR_57 = @id_CR_57,@id_CR_58 = @id_CR_58,@id_CR_59 = @id_CR_59,@id_CR_60 = @id_CR_60" +
        //          ",@id_CR_61 = @id_CR_61,@id_CR_62 = @id_CR_62,@id_CR_63 = @id_CR_63,@id_CR_64 = @id_CR_64,@id_CR_65 = @id_CR_65"+
        //          ",@id_R_1 = @id_R_1,@id_R_2 = @id_R_2,@id_R_3 = @id_R_3,@id_R_4 = @id_R_4,@id_R_5 = @id_R_5,@id_R_6 = @id_R_6,@id_R_7 = @id_R_7,@id_R_8 = @id_R_8,@id_R_9 = @id_R_9,@id_R_10 = @id_R_10" +
        //          ",@id_R_11 = @id_R_11,@id_R_12 = @id_R_12,@id_R_13 = @id_R_13,@id_R_14 = @id_R_14,@id_R_15 = @id_R_15,@id_R_16 = @id_R_16,@id_R_17 = @id_R_17,@id_R_18 = @id_R_18,@id_R_19 = @id_R_19,@id_R_20 = @id_R_20" +
        //          ",@id_R_21 = @id_R_21,@id_R_22 = @id_R_22,@id_R_23 = @id_R_23,@id_R_24 = @id_R_24,@id_R_25 = @id_R_25,@id_R_26 = @id_R_26,@id_R_27 = @id_R_27,@id_R_28 = @id_R_28,@id_R_29 = @id_R_29,@id_R_30 = @id_R_30" +
        //          ",@id_R_31 = @id_R_31,@id_R_32 = @id_R_32,@id_R_33 = @id_R_33,@id_R_34 = @id_R_34,@id_R_35 = @id_R_35,@id_R_36 = @id_R_36,@id_R_37 = @id_R_37,@id_R_38 = @id_R_38,@id_R_39 = @id_R_39,@id_R_40 = @id_R_40" +
        //          ",@id_R_41 = @id_R_41,@id_R_42 = @id_R_42,@id_R_43 = @id_R_43,@id_R_44 = @id_R_44,@id_R_45 = @id_R_45,@id_R_46 = @id_R_46,@id_R_47 = @id_R_47,@id_R_48 = @id_R_48,@id_R_49 = @id_R_49,@id_R_50 = @id_R_50" +
        //          ",@id_R_51 = @id_R_51,@id_R_52 = @id_R_52,@id_R_53 = @id_R_53,@id_R_54 = @id_R_54,@id_R_55 = @id_R_55,@id_R_56 = @id_R_56,@id_R_57 = @id_R_57,@id_R_58 = @id_R_58,@id_R_59 = @id_R_59,@id_R_60 = @id_R_60" +
        //          ",@id_R_61 = @id_R_61,@id_R_62 = @id_R_62,@id_R_63 = @id_R_63,@id_R_64 = @id_R_64,@id_R_65 = @id_R_65";


        //        //var commandText = "EXECUTE Answering_Survey_SEP_2020 " +
        //        //   "@id_CR_1 = @id_CR_1,@id_CR_2 = @id_CR_2,@id_CR_3 = @id_CR_3,@id_CR_4 = @id_CR_4,@id_CR_5 = @id_CR_5,@id_CR_6 = @id_CR_6,@id_CR_7 = @id_CR_7,@id_CR_8 = @id_CR_8,@id_CR_9 = @id_CR_9,@id_CR_10 = @id_CR_10" +
        //        //   "@id_CR_11 = @id_CR_11,@id_CR_12 = @id_CR_12,@id_CR_13 = @id_CR_13,@id_CR_14 = @id_CR_14,@id_CR_15 = @id_CR_15,@id_CR_16 = @id_CR_16,@id_CR_17 = @id_CR_17,@id_CR_18 = @id_CR_18,@id_CR_19 = @id_CR_19,@id_CR_20 = @id_CR_20" +
        //        //   "@id_CR_21 = @id_CR_21,@id_CR_22 = @id_CR_22,@id_CR_23 = @id_CR_23,@id_CR_24 = @id_CR_24,@id_CR_25 = @id_CR_25,@id_CR_26 = @id_CR_26,@id_CR_27 = @id_CR_27,@id_CR_28 = @id_CR_28,@id_CR_29 = @id_CR_29,@id_CR_30 = @id_CR_30" +
        //        //   "@id_CR_31 = @id_CR_31,@id_CR_32 = @id_CR_32,@id_CR_33 = @id_CR_33,@id_CR_34 = @id_CR_34,@id_CR_35 = @id_CR_35,@id_CR_36 = @id_CR_36,@id_CR_37 = @id_CR_37,@id_CR_38 = @id_CR_38,@id_CR_39 = @id_CR_39,@id_CR_40 = @id_CR_40" +
        //        //   "@id_CR_41 = @id_CR_41,@id_CR_42 = @id_CR_42,@id_CR_43 = @id_CR_43,@id_CR_44 = @id_CR_44,@id_CR_45 = @id_CR_45,@id_CR_46 = @id_CR_46,@id_CR_47 = @id_CR_47,@id_CR_48 = @id_CR_48,@id_CR_49 = @id_CR_49,@id_CR_50 = @id_CR_50" +
        //        //   "@id_CR_51 = @id_CR_51,@id_CR_52 = @id_CR_52,@id_CR_53 = @id_CR_53,@id_CR_54 = @id_CR_54,@id_CR_55 = @id_CR_55,@id_CR_56 = @id_CR_56,@id_CR_57 = @id_CR_57,@id_CR_58 = @id_CR_58,@id_CR_59 = @id_CR_59,@id_CR_60 = @id_CR_60" +
        //        //   "@id_CR_61 = @id_CR_61,@id_CR_62 = @id_CR_62,@id_CR_63 = @id_CR_63,@id_CR_64 = @id_CR_64,@id_CR_65 = @id_CR_65,@id_CR_66 = @id_CR_66,@id_CR_67 = @id_CR_67,@id_CR_68 = @id_CR_68,@id_CR_69 = @id_CR_69,@id_CR_70 = @id_CR_70" +
        //        //   "@id_CR_71 = @id_CR_71,@id_CR_72 = @id_CR_72,@id_CR_73 = @id_CR_73,@id_CR_74 = @id_CR_74,@id_CR_75 = @id_CR_75,@id_CR_76 = @id_CR_76,@id_CR_77 = @id_CR_77,@id_CR_78 = @id_CR_78,@id_CR_79 = @id_CR_79,@id_CR_80 = @id_CR_80" +
        //        //   "@id_CR_81 = @id_CR_81,@id_CR_82 = @id_CR_82,@id_CR_83 = @id_CR_83,@id_CR_84 = @id_CR_84,@id_CR_85 = @id_CR_85,@id_CR_86 = @id_CR_86,@id_CR_87 = @id_CR_87,@id_CR_88 = @id_CR_88" +
        //        //   "@id_R_1 = @id_R_1,@id_R_2 = @id_R_2,@id_R_3 = @id_R_3,@id_R_4 = @id_R_4,@id_R_5 = @id_R_5,@id_R_6 = @id_R_6,@id_R_7 = @id_R_7,@id_R_8 = @id_R_8,@id_R_9 = @id_R_9,@id_R_10 = @id_R_10" +
        //        //   "@id_R_11 = @id_R_11,@id_R_12 = @id_R_12,@id_R_13 = @id_R_13,@id_R_14 = @id_R_14,@id_R_15 = @id_R_15,@id_R_16 = @id_R_16,@id_R_17 = @id_R_17,@id_R_18 = @id_R_18,@id_R_19 = @id_R_19,@id_R_20 = @id_R_20" +
        //        //   "@id_R_21 = @id_R_21,@id_R_22 = @id_R_22,@id_R_23 = @id_R_23,@id_R_24 = @id_R_24,@id_R_25 = @id_R_25,@id_R_26 = @id_R_26,@id_R_27 = @id_R_27,@id_R_28 = @id_R_28,@id_R_29 = @id_R_29,@id_R_30 = @id_R_30" +
        //        //   "@id_R_31 = @id_R_31,@id_R_32 = @id_R_32,@id_R_33 = @id_R_33,@id_R_34 = @id_R_34,@id_R_35 = @id_R_35,@id_R_36 = @id_R_36,@id_R_37 = @id_R_37,@id_R_38 = @id_R_38,@id_R_39 = @id_R_39,@id_R_40 = @id_R_40" +
        //        //   "@id_R_41 = @id_R_41,@id_R_42 = @id_R_42,@id_R_43 = @id_R_43,@id_R_44 = @id_R_44,@id_R_45 = @id_R_45,@id_R_46 = @id_R_46,@id_R_47 = @id_R_47,@id_R_48 = @id_R_48,@id_R_49 = @id_R_49,@id_R_50 = @id_R_50" +
        //        //   "@id_R_51 = @id_R_51,@id_R_52 = @id_R_52,@id_R_53 = @id_R_53,@id_R_54 = @id_R_54,@id_R_55 = @id_R_55,@id_R_56 = @id_R_56,@id_R_57 = @id_R_57,@id_R_58 = @id_R_58,@id_R_59 = @id_R_59,@id_R_60 = @id_R_60" +
        //        //   "@id_R_61 = @id_R_61,@id_R_62 = @id_R_62,@id_R_63 = @id_R_63,@id_R_64 = @id_R_64,@id_R_65 = @id_R_65,@id_R_66 = @id_R_66,@id_R_67 = @id_R_67,@id_R_68 = @id_R_68,@id_R_69 = @id_R_69,@id_R_70 = @id_R_70" +
        //        //   "@id_R_71 = @id_R_71,@id_R_72 = @id_R_72,@id_R_73 = @id_R_73,@id_R_74 = @id_R_74,@id_R_75 = @id_R_75,@id_R_76 = @id_R_76,@id_R_77 = @id_R_77,@id_R_78 = @id_R_78,@id_R_79 = @id_R_79,@id_R_80 = @id_R_80" +
        //        //   "@id_R_81 = @id_R_81,@id_R_82 = @id_R_82,@id_R_83 = @id_R_83,@id_R_84 = @id_R_84,@id_R_85 = @id_R_85,@id_R_86 = @id_R_86,@id_R_87 = @id_R_87,@id_R_88 = @id_R_88";

        //        var id_R_1 = new SqlParameter("@id_R_1", id_R[0]);
        //        var id_R_2 = new SqlParameter("@id_R_2", id_R[1]);
        //        var id_R_3 = new SqlParameter("@id_R_3", id_R[2]);
        //        var id_R_4 = new SqlParameter("@id_R_4", id_R[3]);
        //        var id_R_5 = new SqlParameter("@id_R_5", id_R[4]);
        //        var id_R_6 = new SqlParameter("@id_R_6", id_R[5]);
        //        var id_R_7 = new SqlParameter("@id_R_7", id_R[6]);
        //        var id_R_8 = new SqlParameter("@id_R_8", id_R[7]);
        //        var id_R_9 = new SqlParameter("@id_R_9", id_R[8]);
        //        var id_R_10 = new SqlParameter("@id_R_10", id_R[9]);
        //        var id_R_11 = new SqlParameter("@id_R_11", id_R[10]);
        //        var id_R_12 = new SqlParameter("@id_R_12", id_R[11]);
        //        var id_R_13 = new SqlParameter("@id_R_13", id_R[12]);
        //        var id_R_14 = new SqlParameter("@id_R_14", id_R[13]);
        //        var id_R_15 = new SqlParameter("@id_R_15", id_R[14]);
        //        var id_R_16 = new SqlParameter("@id_R_16", id_R[15]);
        //        var id_R_17 = new SqlParameter("@id_R_17", id_R[16]);
        //        var id_R_18 = new SqlParameter("@id_R_18", id_R[17]);
        //        var id_R_19 = new SqlParameter("@id_R_19", id_R[18]);
        //        var id_R_20 = new SqlParameter("@id_R_20", id_R[19]);
        //        var id_R_21 = new SqlParameter("@id_R_21", id_R[20]);
        //        var id_R_22 = new SqlParameter("@id_R_22", id_R[21]);
        //        var id_R_23 = new SqlParameter("@id_R_23", id_R[22]);
        //        var id_R_24 = new SqlParameter("@id_R_24", id_R[23]);
        //        var id_R_25 = new SqlParameter("@id_R_25", id_R[24]);
        //        var id_R_26 = new SqlParameter("@id_R_26", id_R[25]);
        //        var id_R_27 = new SqlParameter("@id_R_27", id_R[26]);
        //        var id_R_28 = new SqlParameter("@id_R_28", id_R[27]);
        //        var id_R_29 = new SqlParameter("@id_R_29", id_R[28]);
        //        var id_R_30 = new SqlParameter("@id_R_30", id_R[29]);
        //        var id_R_31 = new SqlParameter("@id_R_31", id_R[30]);
        //        var id_R_32 = new SqlParameter("@id_R_32", id_R[31]);
        //        var id_R_33 = new SqlParameter("@id_R_33", id_R[32]);
        //        var id_R_34 = new SqlParameter("@id_R_34", id_R[33]);
        //        var id_R_35 = new SqlParameter("@id_R_35", id_R[34]);
        //        var id_R_36 = new SqlParameter("@id_R_36", id_R[35]);
        //        var id_R_37 = new SqlParameter("@id_R_37", id_R[36]);
        //        var id_R_38 = new SqlParameter("@id_R_38", id_R[37]);
        //        var id_R_39 = new SqlParameter("@id_R_39", id_R[38]);
        //        var id_R_40 = new SqlParameter("@id_R_40", id_R[39]);
        //        var id_R_41 = new SqlParameter("@id_R_41", id_R[40]);
        //        var id_R_42 = new SqlParameter("@id_R_42", id_R[41]);
        //        var id_R_43 = new SqlParameter("@id_R_43", id_R[42]);
        //        var id_R_44 = new SqlParameter("@id_R_44", id_R[43]);
        //        var id_R_45 = new SqlParameter("@id_R_45", id_R[44]);
        //        var id_R_46 = new SqlParameter("@id_R_46", id_R[45]);
        //        var id_R_47 = new SqlParameter("@id_R_47", id_R[46]);
        //        var id_R_48 = new SqlParameter("@id_R_48", id_R[47]);
        //        var id_R_49 = new SqlParameter("@id_R_49", id_R[48]);
        //        var id_R_50 = new SqlParameter("@id_R_50", id_R[49]);
        //        var id_R_51 = new SqlParameter("@id_R_51", id_R[50]);
        //        var id_R_52 = new SqlParameter("@id_R_52", id_R[51]);
        //        var id_R_53 = new SqlParameter("@id_R_53", id_R[52]);
        //        var id_R_54 = new SqlParameter("@id_R_54", id_R[53]);
        //        var id_R_55 = new SqlParameter("@id_R_55", id_R[54]);
        //        var id_R_56 = new SqlParameter("@id_R_56", id_R[55]);
        //        var id_R_57 = new SqlParameter("@id_R_57", id_R[56]);
        //        var id_R_58 = new SqlParameter("@id_R_58", id_R[57]);
        //        var id_R_59 = new SqlParameter("@id_R_59", id_R[58]);
        //        var id_R_60 = new SqlParameter("@id_R_60", id_R[59]);
        //        var id_R_61 = new SqlParameter("@id_R_61", id_R[60]);
        //        var id_R_62 = new SqlParameter("@id_R_62", id_R[61]);
        //        var id_R_63 = new SqlParameter("@id_R_63", id_R[62]);
        //        var id_R_64 = new SqlParameter("@id_R_64", id_R[63]);
        //        var id_R_65 = new SqlParameter("@id_R_65", id_R[64]);
        //        //var id_R_66 = new SqlParameter("@id_R_66", id_R[65]);
        //        //var id_R_67 = new SqlParameter("@id_R_67", id_R[66]);
        //        //var id_R_68 = new SqlParameter("@id_R_68", id_R[67]);
        //        //var id_R_69 = new SqlParameter("@id_R_69", id_R[68]);
        //        //var id_R_70 = new SqlParameter("@id_R_70", id_R[69]);
        //        //var id_R_71 = new SqlParameter("@id_R_71", id_R[70]);
        //        //var id_R_72 = new SqlParameter("@id_R_72", id_R[71]);
        //        //var id_R_73 = new SqlParameter("@id_R_73", id_R[72]);
        //        //var id_R_74 = new SqlParameter("@id_R_74", id_R[73]);
        //        //var id_R_75 = new SqlParameter("@id_R_75", id_R[74]);
        //        //var id_R_76 = new SqlParameter("@id_R_76", id_R[75]);
        //        //var id_R_77 = new SqlParameter("@id_R_77", id_R[76]);
        //        //var id_R_78 = new SqlParameter("@id_R_78", id_R[77]);
        //        //var id_R_79 = new SqlParameter("@id_R_79", id_R[78]);
        //        //var id_R_80 = new SqlParameter("@id_R_80", id_R[79]);
        //        //var id_R_81 = new SqlParameter("@id_R_81", id_R[80]);
        //        //var id_R_82 = new SqlParameter("@id_R_82", id_R[81]);
        //        //var id_R_83 = new SqlParameter("@id_R_83", id_R[82]);
        //        //var id_R_84 = new SqlParameter("@id_R_84", id_R[83]);
        //        //var id_R_85 = new SqlParameter("@id_R_85", id_R[84]);
        //        //var id_R_86 = new SqlParameter("@id_R_86", id_R[85]);
        //        //var id_R_87 = new SqlParameter("@id_R_87", id_R[86]);
        //        var id_CR_1 = new SqlParameter("@id_CR_1", id_CR[0]);
        //        var id_CR_2 = new SqlParameter("@id_CR_2", id_CR[1]);
        //        var id_CR_3 = new SqlParameter("@id_CR_3", id_CR[2]);
        //        var id_CR_4 = new SqlParameter("@id_CR_4", id_CR[3]);
        //        var id_CR_5 = new SqlParameter("@id_CR_5", id_CR[4]);
        //        var id_CR_6 = new SqlParameter("@id_CR_6", id_CR[5]);
        //        var id_CR_7 = new SqlParameter("@id_CR_7", id_CR[6]);
        //        var id_CR_8 = new SqlParameter("@id_CR_8", id_CR[7]);
        //        var id_CR_9 = new SqlParameter("@id_CR_9", id_CR[8]);
        //        var id_CR_10 = new SqlParameter("@id_CR_10", id_CR[9]);
        //        var id_CR_11 = new SqlParameter("@id_CR_11", id_CR[10]);
        //        var id_CR_12 = new SqlParameter("@id_CR_12", id_CR[11]);
        //        var id_CR_13 = new SqlParameter("@id_CR_13", id_CR[12]);
        //        var id_CR_14 = new SqlParameter("@id_CR_14", id_CR[13]);
        //        var id_CR_15 = new SqlParameter("@id_CR_15", id_CR[14]);
        //        var id_CR_16 = new SqlParameter("@id_CR_16", id_CR[15]);
        //        var id_CR_17 = new SqlParameter("@id_CR_17", id_CR[16]);
        //        var id_CR_18 = new SqlParameter("@id_CR_18", id_CR[17]);
        //        var id_CR_19 = new SqlParameter("@id_CR_19", id_CR[18]);
        //        var id_CR_20 = new SqlParameter("@id_CR_20", id_CR[19]);
        //        var id_CR_21 = new SqlParameter("@id_CR_21", id_CR[20]);
        //        var id_CR_22 = new SqlParameter("@id_CR_22", id_CR[21]);
        //        var id_CR_23 = new SqlParameter("@id_CR_23", id_CR[22]);
        //        var id_CR_24 = new SqlParameter("@id_CR_24", id_CR[23]);
        //        var id_CR_25 = new SqlParameter("@id_CR_25", id_CR[24]);
        //        var id_CR_26 = new SqlParameter("@id_CR_26", id_CR[25]);
        //        var id_CR_27 = new SqlParameter("@id_CR_27", id_CR[26]);
        //        var id_CR_28 = new SqlParameter("@id_CR_28", id_CR[27]);
        //        var id_CR_29 = new SqlParameter("@id_CR_29", id_CR[28]);
        //        var id_CR_30 = new SqlParameter("@id_CR_30", id_CR[29]);
        //        var id_CR_31 = new SqlParameter("@id_CR_31", id_CR[30]);
        //        var id_CR_32 = new SqlParameter("@id_CR_32", id_CR[31]);
        //        var id_CR_33 = new SqlParameter("@id_CR_33", id_CR[32]);
        //        var id_CR_34 = new SqlParameter("@id_CR_34", id_CR[33]);
        //        var id_CR_35 = new SqlParameter("@id_CR_35", id_CR[34]);
        //        var id_CR_36 = new SqlParameter("@id_CR_36", id_CR[35]);
        //        var id_CR_37 = new SqlParameter("@id_CR_37", id_CR[36]);
        //        var id_CR_38 = new SqlParameter("@id_CR_38", id_CR[37]);
        //        var id_CR_39 = new SqlParameter("@id_CR_39", id_CR[38]);
        //        var id_CR_40 = new SqlParameter("@id_CR_40", id_CR[39]);
        //        var id_CR_41 = new SqlParameter("@id_CR_41", id_CR[40]);
        //        var id_CR_42 = new SqlParameter("@id_CR_42", id_CR[41]);
        //        var id_CR_43 = new SqlParameter("@id_CR_43", id_CR[42]);
        //        var id_CR_44 = new SqlParameter("@id_CR_44", id_CR[43]);
        //        var id_CR_45 = new SqlParameter("@id_CR_45", id_CR[44]);
        //        var id_CR_46 = new SqlParameter("@id_CR_46", id_CR[45]);
        //        var id_CR_47 = new SqlParameter("@id_CR_47", id_CR[46]);
        //        var id_CR_48 = new SqlParameter("@id_CR_48", id_CR[47]);
        //        var id_CR_49 = new SqlParameter("@id_CR_49", id_CR[48]);
        //        var id_CR_50 = new SqlParameter("@id_CR_50", id_CR[49]);
        //        var id_CR_51 = new SqlParameter("@id_CR_51", id_CR[50]);
        //        var id_CR_52 = new SqlParameter("@id_CR_52", id_CR[51]);
        //        var id_CR_53 = new SqlParameter("@id_CR_53", id_CR[52]);
        //        var id_CR_54 = new SqlParameter("@id_CR_54", id_CR[53]);
        //        var id_CR_55 = new SqlParameter("@id_CR_55", id_CR[54]);
        //        var id_CR_56 = new SqlParameter("@id_CR_56", id_CR[55]);
        //        var id_CR_57 = new SqlParameter("@id_CR_57", id_CR[56]);
        //        var id_CR_58 = new SqlParameter("@id_CR_58", id_CR[57]);
        //        var id_CR_59 = new SqlParameter("@id_CR_59", id_CR[58]);
        //        var id_CR_60 = new SqlParameter("@id_CR_60", id_CR[59]);
        //        var id_CR_61 = new SqlParameter("@id_CR_61", id_CR[60]);
        //        var id_CR_62 = new SqlParameter("@id_CR_62", id_CR[61]);
        //        var id_CR_63 = new SqlParameter("@id_CR_63", id_CR[62]);
        //        var id_CR_64 = new SqlParameter("@id_CR_64", id_CR[63]);
        //        var id_CR_65 = new SqlParameter("@id_CR_65", id_CR[64]);
        //        //var id_CR_66 = new SqlParameter("@id_CR_66", id_CR[65]);
        //        //var id_CR_67 = new SqlParameter("@id_CR_67", id_CR[66]);
        //        //var id_CR_68 = new SqlParameter("@id_CR_68", id_CR[67]);
        //        //var id_CR_69 = new SqlParameter("@id_CR_69", id_CR[68]);
        //        //var id_CR_70 = new SqlParameter("@id_CR_70", id_CR[69]);
        //        //var id_CR_71 = new SqlParameter("@id_CR_71", id_CR[70]);
        //        //var id_CR_72 = new SqlParameter("@id_CR_72", id_CR[71]);
        //        //var id_CR_73 = new SqlParameter("@id_CR_73", id_CR[72]);
        //        //var id_CR_74 = new SqlParameter("@id_CR_74", id_CR[73]);
        //        //var id_CR_75 = new SqlParameter("@id_CR_75", id_CR[74]);
        //        //var id_CR_76 = new SqlParameter("@id_CR_76", id_CR[75]);
        //        //var id_CR_77 = new SqlParameter("@id_CR_77", id_CR[76]);
        //        //var id_CR_78 = new SqlParameter("@id_CR_78", id_CR[77]);
        //        //var id_CR_79 = new SqlParameter("@id_CR_79", id_CR[78]);
        //        //var id_CR_80 = new SqlParameter("@id_CR_80", id_CR[79]);
        //        //var id_CR_81 = new SqlParameter("@id_CR_81", id_CR[80]);
        //        //var id_CR_82 = new SqlParameter("@id_CR_82", id_CR[81]);
        //        //var id_CR_83 = new SqlParameter("@id_CR_83", id_CR[82]);
        //        //var id_CR_84 = new SqlParameter("@id_CR_84", id_CR[83]);
        //        //var id_CR_85 = new SqlParameter("@id_CR_85", id_CR[84]);
        //        //var id_CR_86 = new SqlParameter("@id_CR_86", id_CR[85]);
        //        //var id_CR_87 = new SqlParameter("@id_CR_87", id_CR[86]);
        //        db.Database.ExecuteSqlCommand(commandText, id_R_1
        //        , id_R_2
        //        , id_R_3
        //        , id_R_4
        //        , id_R_5
        //        , id_R_6
        //        , id_R_7
        //        , id_R_8
        //        , id_R_9
        //        , id_R_10
        //        , id_R_11
        //        , id_R_12
        //        , id_R_13
        //        , id_R_14
        //        , id_R_15
        //        , id_R_16
        //        , id_R_17
        //        , id_R_18
        //        , id_R_19
        //        , id_R_20
        //        , id_R_21
        //        , id_R_22
        //        , id_R_23
        //        , id_R_24
        //        , id_R_25
        //        , id_R_26
        //        , id_R_27
        //        , id_R_28
        //        , id_R_29
        //        , id_R_30
        //        , id_R_31
        //        , id_R_32
        //        , id_R_33
        //        , id_R_34
        //        , id_R_35
        //        , id_R_36
        //        , id_R_37
        //        , id_R_38
        //        , id_R_39
        //        , id_R_40
        //        , id_R_41
        //        , id_R_42
        //        , id_R_43
        //        , id_R_44
        //        , id_R_45
        //        , id_R_46
        //        , id_R_47
        //        , id_R_48
        //        , id_R_49
        //        , id_R_50
        //        , id_R_51
        //        , id_R_52
        //        , id_R_53
        //        , id_R_54
        //        , id_R_55
        //        , id_R_56
        //        , id_R_57
        //        , id_R_58
        //        , id_R_59
        //        , id_R_60
        //        , id_R_61
        //        , id_R_62
        //        , id_R_63
        //        , id_R_64
        //        , id_R_65
        //       // , id_R_66
        //        //, id_R_67
        //        //, id_R_68
        //        //, id_R_69
        //        //, id_R_70
        //        //, id_R_71
        //        //, id_R_72
        //        //, id_R_73
        //        //, id_R_74
        //        //, id_R_75
        //        //, id_R_76
        //        //, id_R_77
        //        //, id_R_78
        //        //, id_R_79
        //        //, id_R_80
        //        //, id_R_81
        //        //, id_R_82
        //        //, id_R_83
        //        //, id_R_84
        //        //, id_R_85
        //        //, id_R_86
        //        //, id_R_87
        //        , id_CR_1
        //        , id_CR_2
        //        , id_CR_3
        //        , id_CR_4
        //        , id_CR_5
        //        , id_CR_6
        //        , id_CR_7
        //        , id_CR_8
        //        , id_CR_9
        //        , id_CR_10
        //        , id_CR_11
        //        , id_CR_12
        //        , id_CR_13
        //        , id_CR_14
        //        , id_CR_15
        //        , id_CR_16
        //        , id_CR_17
        //        , id_CR_18
        //        , id_CR_19
        //        , id_CR_20
        //        , id_CR_21
        //        , id_CR_22
        //        , id_CR_23
        //        , id_CR_24
        //        , id_CR_25
        //        , id_CR_26
        //        , id_CR_27
        //        , id_CR_28
        //        , id_CR_29
        //        , id_CR_30
        //        , id_CR_31
        //        , id_CR_32
        //        , id_CR_33
        //        , id_CR_34
        //        , id_CR_35
        //        , id_CR_36
        //        , id_CR_37
        //        , id_CR_38
        //        , id_CR_39
        //        , id_CR_40
        //        , id_CR_41
        //        , id_CR_42
        //        , id_CR_43
        //        , id_CR_44
        //        , id_CR_45
        //        , id_CR_46
        //        , id_CR_47
        //        , id_CR_48
        //        , id_CR_49
        //        , id_CR_50
        //        , id_CR_51
        //        , id_CR_52
        //        , id_CR_53
        //        , id_CR_54
        //        , id_CR_55
        //        , id_CR_56
        //        , id_CR_57
        //        , id_CR_58
        //        , id_CR_59
        //        , id_CR_60
        //        , id_CR_61
        //        , id_CR_62
        //        , id_CR_63
        //        , id_CR_64
        //        , id_CR_65);
        //        //, id_CR_66
        //        //, id_CR_67
        //        //, id_CR_68
        //        //, id_CR_69
        //        //, id_CR_70
        //        //, id_CR_71
        //        //, id_CR_72
        //        //, id_CR_73
        //        //, id_CR_74
        //        //, id_CR_75
        //        //, id_CR_76
        //        //, id_CR_77
        //        //, id_CR_78
        //        //, id_CR_79
        //        //, id_CR_80
        //        //, id_CR_81
        //        //, id_CR_82
        //        //, id_CR_83
        //        //, id_CR_84
        //        //, id_CR_85
        //        //, id_CR_86
        //        //, id_CR_87);
        //        db.SaveChanges();
        //    }
        //    catch(Exception ex) { 
        //        throw ex; 
        //    } 
        //    string mensaje = "Exito";
        //    return Json(new { mensaje = mensaje });
        //}

        [HttpPost]
        public JsonResult cambio_logica_guardado(int[] id_CR, int[] id_R)
        {
            var db_save = new Check035Entities();
            int counter = 0;
            try
            {
                foreach (int id_Cuestionario_Resultado in id_CR)
                {
                    int a = id_Cuestionario_Resultado;
                    int b = id_R[counter];
                    var result = db_save.ERGOS_Cuestionarios_Resultados_N01.SingleOrDefault(E => E.id_Cuestionario_Resultado == a);
                    result.id_respuesta = b;
                    counter = counter + 1;
                }
                db_save.SaveChanges();
            }
            catch (Exception ex) { throw ex; }
            string mensaje = "Exito";
            return Json(new { mensaje = mensaje });
        }

        //LINEAS FUERA DE cambio_logica_guardado

        //db.Database.ExecuteSqlCommand("EXECUTE Completing_survey @id_CT =" + id_CT);
        //db.SaveChanges();
        //db.Database.ExecuteSqlCommand("EXECUTE Answering_Survey @id_CR =" + a + ", @Respuesta = " + b);
        //db.SaveChanges();
        public JsonResult Completing_survey(int id_CT)
        {
            db.Database.ExecuteSqlCommand("EXECUTE Completing_survey @id_CT =" + id_CT);
            db.SaveChanges();

            string mensaje = "Exito";

            return Json(new { mensaje = mensaje });
        }

        public int flag_editar = 0;
        [Authorize]
        public ActionResult Encuesta(int id_CT, int id_C)
        {
            string UserName = User.Identity.Name;
            if (User.IsInRole("Final_Guest"))
            {
                int UserId = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_user).FirstOrDefault();
                ERGOS_Usuarios_N01 eRGOS_Usuarios_N01 = db.ERGOS_Usuarios_N01.Find(UserId);
                if (eRGOS_Usuarios_N01.id_cuestionario_trabajador != id_CT)
                {
                    return RedirectToAction("Login", "Home");
                }
            }


            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_empresa).FirstOrDefault();
            var result = (from Encuesta in db.ERGOS_Cuestionarios_Trabajador_N01
                          where Encuesta.id_cuestionario_trabajador == id_CT
                          select new { Encuesta.id_cuestionario_trabajador, Encuesta.ERGOS_Cuestionarios_N01.id_cuestionario }).FirstOrDefault();
            var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                        where R.id_cuestionario_trabajador == result.id_cuestionario_trabajador
                        select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

            flag_editar = 0;
            if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
            {
                flag_editar = 1;
            }

            if (flag_editar != 0)
            {

                return RedirectToAction("Edit","Encuestas", new { id = result.id_cuestionario_trabajador });

            }

            ViewBag.id_cuestionario = id_C;
            ViewBag.id_CT = id_CT;
            ViewBag.Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                               where CR.id_cuestionario_trabajador == id_CT
                               select new Respuestas { id_respuesta = CR.id_respuesta, id_pregunta = CR.id_pregunta }).ToArray();


            var datos_empleado = (from Encuesta in db.ERGOS_Cuestionarios_Trabajador_N01
                                  where Encuesta.id_cuestionario_trabajador == id_CT
                                  select new { Encuesta.id_trabajador, Encuesta.ERGOS_Empresas_N01.Razon_Social, Encuesta.Nombre }).FirstOrDefault();
            ViewBag.Empleado = datos_empleado.Nombre;
            ViewBag.Empresa = datos_empleado.Razon_Social;
            ViewBag.No_Emp = datos_empleado.id_trabajador;

            object datos = null;
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
                         });
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
                         });
            }
            //var datos = (from total in db.fn_Final_view_surveys(id_CT)
            //                      select new Surveys {
            //                          id_Cuestionario_Resultado = total.id_Cuestionario_Resultado,
            //                          id_cuestionario = id_C,
            //                          id_respuesta = total.id_respuesta,
            //                          No_Pregunta = total.id_pregunta,
            //                          Preguntas =  total.Preguntas });

            ViewBag.Survey = id_C;
            ViewBag.Survey_1 = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                join P in db.ERGOS_Preguntas_N01 on CR.id_pregunta equals P.id_pregunta
                                where CR.id_cuestionario_trabajador == id_CT && CR.id_encuesta == 1
                                group CR by new { P.No_Pregunta, P.Preguntas, CR.id_Cuestionario_Resultado, CR.id_respuesta, CR.id_encuesta } into X
                                select new Surveys { id_cuestionario = X.Key.id_encuesta, No_Pregunta = X.Key.No_Pregunta, Preguntas = X.Key.Preguntas, id_Cuestionario_Resultado = X.Key.id_Cuestionario_Resultado, id_respuesta = X.Key.id_respuesta }).OrderBy(x => x.No_Pregunta);

            // var datos2 = datos.Distinct(x => x.id_Cuestionario_Resultado);
            return View(datos);
        }
        public ActionResult Encuesta_Final_Guest()
        {
            string UserID = User.Identity.Name;
            int Flag = 0;
            int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserID select E.id_empresa).FirstOrDefault();
            int? id_CT = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserID select E.id_cuestionario_trabajador).FirstOrDefault();
            var usuario = (from E in db.ERGOS_Usuarios_N01 where E.id_empresa == id_empresa && E.id_rol == 6 && E.id_cuestionario_trabajador == id_CT select new { E.id_cuestionario_trabajador, E.ERGOS_Cuestionarios_Trabajador_N01.id_encuesta }).FirstOrDefault();

            var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                        where R.id_cuestionario_trabajador == usuario.id_cuestionario_trabajador
                        select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

            flag_editar = 0;
            if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
            {
                flag_editar = 1;
            }

            if (usuario != null)
            {

                Flag = 1;
                return Json(new { usuario, Flag, flag_editar }, JsonRequestBehavior.AllowGet);

            }
            else
            {

                return Json(false);
            }
        }
        public ActionResult Encuesta_Guest(string id_employee,DateTime? birthday ,string nombre ,string Empresa_Name, string Apellido)
        {


                int Flag = 0;
                string UserName = User.Identity.Name;
                int? id_empresa = (from E in db.ERGOS_Usuarios_N01 where E.User_Nombre == UserName select E.id_empresa).FirstOrDefault();
                var result = (from Encuesta in db.ERGOS_Cuestionarios_Trabajador_N01
                              where Encuesta.Fecha_Nacimiento == birthday /*&& Encuesta.Nombre.Contains(nombre) && Encuesta.Nombre.Contains(Apellido)*/
                              && Encuesta.id_trabajador == id_employee && Encuesta.id_empresa == id_empresa /*&& Encuesta.Email == Email*/
                              select new { Encuesta.id_cuestionario_trabajador, Encuesta.ERGOS_Cuestionarios_N01.id_cuestionario }).FirstOrDefault();
                var LL_E = (from R in db.ERGOS_Cuestionarios_Trabajador_N01
                            where R.id_cuestionario_trabajador == result.id_cuestionario_trabajador
                            select new { R.Rotacion_Turno, R.Tipo_Jornada, R.Nivel_Estudios, R.Estado_Civil, R.Nombre, R.Edad, R.Departamento, R.Ocupacion, R.Experiencia_puesto_actual, R.Experiencia_puesto_laboral, R.Sexo, R.Tipo_Personal, R.Tipo_puesto, R.Tipo_Contratacion }).FirstOrDefault();

                flag_editar = 0;
                if (LL_E.Nombre is null || LL_E.Sexo is null || LL_E.Edad is null || LL_E.Estado_Civil is null || LL_E.Nivel_Estudios is null || LL_E.Ocupacion is null || LL_E.Departamento is null
                    || LL_E.Tipo_puesto is null || LL_E.Tipo_Personal is null || LL_E.Tipo_Contratacion is null || LL_E.Tipo_Jornada is null || LL_E.Rotacion_Turno is null || LL_E.Experiencia_puesto_actual is null || LL_E.Experiencia_puesto_laboral is null)
                {
                    flag_editar = 1;
                }

                if (result != null)
                {

                    Flag = 1;
                    return Json(new { result, Flag, flag_editar }, JsonRequestBehavior.AllowGet);

                }
                else
                {

                    return Json(false);
                }

        }
         
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            if (eRGOS_Cuestionarios_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // GET: Trabajador_Resultados/Create
        public ActionResult Create()
        {
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador");
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas");
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta");
            return View();
        }

        // POST: Trabajador_Resultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Cuestionario_Resultado,id_cuestionario_trabajador,id_respuesta,id_pregunta")] ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Cuestionarios_Resultados_N01.Add(eRGOS_Cuestionarios_Resultados_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador", eRGOS_Cuestionarios_Resultados_N01.id_cuestionario_trabajador);
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas", eRGOS_Cuestionarios_Resultados_N01.id_pregunta);
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta", eRGOS_Cuestionarios_Resultados_N01.id_respuesta);
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // GET: Trabajador_Resultados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            if (eRGOS_Cuestionarios_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador", eRGOS_Cuestionarios_Resultados_N01.id_cuestionario_trabajador);
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas", eRGOS_Cuestionarios_Resultados_N01.id_pregunta);
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta", eRGOS_Cuestionarios_Resultados_N01.id_respuesta);
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // POST: Trabajador_Resultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Cuestionario_Resultado,id_cuestionario_trabajador,id_respuesta,id_pregunta")] ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Cuestionarios_Resultados_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador", eRGOS_Cuestionarios_Resultados_N01.id_cuestionario_trabajador);
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas", eRGOS_Cuestionarios_Resultados_N01.id_pregunta);
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta", eRGOS_Cuestionarios_Resultados_N01.id_respuesta);
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // GET: Trabajador_Resultados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            if (eRGOS_Cuestionarios_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // POST: Trabajador_Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            db.ERGOS_Cuestionarios_Resultados_N01.Remove(eRGOS_Cuestionarios_Resultados_N01);
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
