using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErgoSalud.Models
{
    public class checklist
    {
        [Required]
        public string Q1 { get; set; }
        [Required]
        public string Q2 { get; set; }
        [Required]
        public string Q3 { get; set; }
        [Required]
        public string Q4 { get; set; }
        [Required]
        public string Q5 { get; set; }
        [Required]
        public string Q6 { get; set; }
        [Required]
        public string Q7 { get; set; }
        [Required]
        public string Q8 { get; set; }
        [Required]
        public string Q9 { get; set; }
        [Required]
        public string Q10 { get; set; }
        [Required]
        public string Email { get; set; }
    }
}