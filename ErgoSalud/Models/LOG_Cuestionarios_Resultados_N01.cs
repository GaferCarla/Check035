//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErgoSalud.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOG_Cuestionarios_Resultados_N01
    {
        public int id_log { get; set; }
        public Nullable<int> id_Cuestionario_Resultado { get; set; }
        public Nullable<int> id_cuestionario_trabajador { get; set; }
        public Nullable<int> id_respuesta { get; set; }
        public Nullable<int> id_pregunta { get; set; }
        public Nullable<int> id_encuesta { get; set; }
        public Nullable<int> Calificacion { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
    }
}
