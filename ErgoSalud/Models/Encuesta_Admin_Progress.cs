using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErgoSalud.Models
{
    public class Encuesta_Admin_Progress
    {
        public Nullable<int> Encuestas_Totales { get; set; }
        public Nullable<int> Encuestas_Contestadas { get; set; }
        public string Departamento { get; set; }
    }
}