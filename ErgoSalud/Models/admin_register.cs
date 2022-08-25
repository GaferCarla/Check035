using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErgoSalud.Models
{
    public class admin_register
    {
        [Required]
        public string User_Nombre { get; set; }
        [Required]
        public string User_Password { get; set; }
        [Required]
        public string Razon_Social { get; set; }
        public string Telefono { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public string Contacto_Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }        
        public string Actividad_Principal { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fecha_Aplicacion { get; set; }
        [Required]
        public string id_evaluacion { get; set; }
    }
}