using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErgoSalud.Models
{
    public class Respuestas
    {
        public Nullable<int> id_respuesta { get; set; }


        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION

        public Nullable<double> Dominio_1 { get; set; }
        public Nullable<double> Dominio_2 { get; set; }
        public Nullable<double> Dominio_3 { get; set; }
        public Nullable<double> Dominio_4 { get; set; }
        public Nullable<double> Dominio_5 { get; set; }
        public Nullable<double> Dominio_6 { get; set; }
        public Nullable<double> Dominio_7 { get; set; }
        public Nullable<double> Dominio_8 { get; set; }
        public Nullable<double> Dominio_9 { get; set; }
        public Nullable<double> Dominio_10 { get; set; }
        public Nullable<double> Categoria_1 { get; set; }
        public Nullable<double> Categoria_2 { get; set; }
        public Nullable<double> Categoria_3 { get; set; }
        public Nullable<double> Categoria_4 { get; set; }
        public Nullable<double> Categoria_5 { get; set; }
        public Nullable<int> Final { get; set; }
        public Nullable<int> id_cuestionario { get; set; }

        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION CALCULO GLOBAL ---   CATEGORIAS 

        public Nullable<int> Total_Encuestas { get; set; }
        public Nullable<int> SUMATORIA { get; set; }
        public Nullable<double> Categoria_1_G { get; set; }
        public Nullable<double> Categoria_2_G { get; set; }
        public Nullable<double> Categoria_3_G { get; set; }
        public Nullable<double> Categoria_4_G { get; set; }
        public Nullable<double> Categoria_5_G { get; set; }

        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION CALCULO GLOBAL ---   DOMINIOS 
        public Nullable<double> Dominio_1_G { get; set; }
        public Nullable<double> Dominio_2_G { get; set; }
        public Nullable<double> Dominio_3_G { get; set; }
        public Nullable<double> Dominio_4_G { get; set; }
        public Nullable<double> Dominio_5_G { get; set; }
        public Nullable<double> Dominio_6_G { get; set; }
        public Nullable<double> Dominio_7_G { get; set; }
        public Nullable<double> Dominio_8_G { get; set; }
        public Nullable<double> Dominio_9_G { get; set; }
        public Nullable<double> Dominio_10_G { get; set; }

        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION CALCULO GLOBAL ---   DIMENSIONES 
        public Nullable<double> Dimension_1_G { get; set; }
        public Nullable<double> Dimension_2_G { get; set; }
        public Nullable<double> Dimension_3_G { get; set; }
        public Nullable<double> Dimension_4_G { get; set; }
        public Nullable<double> Dimension_5_G { get; set; }
        public Nullable<double> Dimension_6_G { get; set; }
        public Nullable<double> Dimension_7_G { get; set; }
        public Nullable<double> Dimension_8_G { get; set; }
        public Nullable<double> Dimension_9_G { get; set; }
        public Nullable<double> Dimension_10_G { get; set; }
        public Nullable<double> Dimension_11_G { get; set; }
        public Nullable<double> Dimension_12_G { get; set; }
        public Nullable<double> Dimension_13_G { get; set; }
        public Nullable<double> Dimension_14_G { get; set; }
        public Nullable<double> Dimension_15_G { get; set; }
        public Nullable<double> Dimension_16_G { get; set; }
        public Nullable<double> Dimension_17_G { get; set; }
        public Nullable<double> Dimension_18_G { get; set; }
        public Nullable<double> Dimension_19_G { get; set; }
        public Nullable<double> Dimension_20_G { get; set; }
        public Nullable<double> Dimension_21_G { get; set; }
        public Nullable<double> Dimension_22_G { get; set; }
        public Nullable<double> Dimension_23_G { get; set; }
        public Nullable<double> Dimension_24_G { get; set; }
        public Nullable<double> Dimension_25_G { get; set; }


        // VARIABLE PARA Calificacion_General_Pregunta

        public Nullable<int> Calificacion_General_Pregunta { get; set; }
        public Nullable<int> id_pregunta { get; set; }

        // VARIABLE PARA Canalizacion
        public Nullable<int> Canalizado { get; set; }


    }
}