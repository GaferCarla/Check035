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
    
    public partial class DATA_Tipo_puesto_N01
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DATA_Tipo_puesto_N01()
        {
            this.ERGOS_Cuestionarios_Trabajador_N01 = new HashSet<ERGOS_Cuestionarios_Trabajador_N01>();
        }
    
        public int id_Tipo_puesto { get; set; }
        public string Tipo_puesto { get; set; }
        public string Comments { get; set; }
        public Nullable<int> Extra { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Cuestionarios_Trabajador_N01> ERGOS_Cuestionarios_Trabajador_N01 { get; set; }
    }
}
