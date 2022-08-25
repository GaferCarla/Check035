using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using ErgoSalud.Helper;

namespace ErgoSalud.Models
{
    public class Upload_excel_records
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [FileType("xlsx, xls")]
        [Required(ErrorMessage = "Por favor seleccione el archivo excel a cargar.")]
        public HttpPostedFileBase File { get; set; }

        public string FilePath { get; set; }
        [Required(ErrorMessage = "Por favor asigne el centro de trabajo.")]
        public int id_centro_trabajo { get; set; }

        public DataSet DataSet { get; set; }
    }
}