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
    
    public partial class ERGOS_Cuestionarios_N01
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ERGOS_Cuestionarios_N01()
        {
            this.ERGOS_Preguntas_N01 = new HashSet<ERGOS_Preguntas_N01>();
            this.ERGOS_Preguntas_S2_N02 = new HashSet<ERGOS_Preguntas_S2_N02>();
            this.ERGOS_Cuestionarios_Trabajador_N01 = new HashSet<ERGOS_Cuestionarios_Trabajador_N01>();
            this.ERGOS_Empresas_N01 = new HashSet<ERGOS_Empresas_N01>();
        }
    
        public int id_cuestionario { get; set; }
        public string Cuestionario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Preguntas_N01> ERGOS_Preguntas_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Preguntas_S2_N02> ERGOS_Preguntas_S2_N02 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Cuestionarios_Trabajador_N01> ERGOS_Cuestionarios_Trabajador_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Empresas_N01> ERGOS_Empresas_N01 { get; set; }
    }
}