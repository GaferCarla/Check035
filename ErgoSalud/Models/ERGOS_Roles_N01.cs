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
    
    public partial class ERGOS_Roles_N01
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ERGOS_Roles_N01()
        {
            this.ERGOS_Usuarios_N01 = new HashSet<ERGOS_Usuarios_N01>();
        }
    
        public int id_rol { get; set; }
        public string Nombre_Rol { get; set; }
        public string Descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Usuarios_N01> ERGOS_Usuarios_N01 { get; set; }
    }
}
