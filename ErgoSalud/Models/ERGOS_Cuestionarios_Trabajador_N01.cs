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
    
    public partial class ERGOS_Cuestionarios_Trabajador_N01
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ERGOS_Cuestionarios_Trabajador_N01()
        {
            this.ClimaLaboral_Cuestionario_Resultados_N01 = new HashSet<ClimaLaboral_Cuestionario_Resultados_N01>();
            this.E360_Cuestionario_Resultado_N01 = new HashSet<E360_Cuestionario_Resultado_N01>();
            this.ERGOS_Cuestionarios_Resultados_N01 = new HashSet<ERGOS_Cuestionarios_Resultados_N01>();
            this.ERGOS_Usuarios_N01 = new HashSet<ERGOS_Usuarios_N01>();
        }
    
        public int id_cuestionario_trabajador { get; set; }
        public string id_trabajador { get; set; }
        public Nullable<int> id_encuesta { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public Nullable<int> Sexo { get; set; }
        public Nullable<int> Edad { get; set; }
        public Nullable<int> Estado_Civil { get; set; }
        public Nullable<int> Nivel_Estudios { get; set; }
        public string Ocupacion { get; set; }
        public string Departamento { get; set; }
        public Nullable<int> Tipo_puesto { get; set; }
        public Nullable<int> Tipo_Contratacion { get; set; }
        public Nullable<int> Tipo_Jornada { get; set; }
        public Nullable<int> Rotacion_Turno { get; set; }
        public Nullable<int> Experiencia_puesto_actual { get; set; }
        public Nullable<int> Experiencia_puesto_laboral { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
        public Nullable<int> Tipo_Personal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<int> No_Empleado { get; set; }
        public Nullable<System.DateTime> Fecha_Nacimiento { get; set; }
        public string Email { get; set; }
        public Nullable<int> Mail_Status { get; set; }
        public Nullable<int> Survey_Status { get; set; }
        public Nullable<int> Canalizacion { get; set; }
        public Nullable<int> id_departamento { get; set; }
        public Nullable<int> id_Tipo_Personal { get; set; }
        public Nullable<int> id_centro_trabajo { get; set; }
        public string deleted_by { get; set; }
        public Nullable<int> Vendedor_Status { get; set; }
        public Nullable<int> Supervisor_Status { get; set; }
        public Nullable<int> E360_EBCW { get; set; }
        public Nullable<int> E360_STAE { get; set; }
        public string E360_EBCW_id_trabajador { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClimaLaboral_Cuestionario_Resultados_N01> ClimaLaboral_Cuestionario_Resultados_N01 { get; set; }
        public virtual DATA_Departamentos_N01 DATA_Departamentos_N01 { get; set; }
        public virtual DATA_Edades_N01 DATA_Edades_N01 { get; set; }
        public virtual DATA_Estado_Civil_N01 DATA_Estado_Civil_N01 { get; set; }
        public virtual DATA_Experiencia_puesto_N01 DATA_Experiencia_puesto_N01 { get; set; }
        public virtual DATA_Experiencia_puesto_N01 DATA_Experiencia_puesto_N011 { get; set; }
        public virtual DATA_Nivel_Estudios_N01 DATA_Nivel_Estudios_N01 { get; set; }
        public virtual DATA_Rotacion_Turno_N01 DATA_Rotacion_Turno_N01 { get; set; }
        public virtual DATA_Sexo_N01 DATA_Sexo_N01 { get; set; }
        public virtual DATA_Tipo_Contratacion_N01 DATA_Tipo_Contratacion_N01 { get; set; }
        public virtual DATA_Tipo_Jornada_N01 DATA_Tipo_Jornada_N01 { get; set; }
        public virtual DATA_Tipo_Personal_N01 DATA_Tipo_Personal_N01 { get; set; }
        public virtual DATA_Tipo_puesto_N01 DATA_Tipo_puesto_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<E360_Cuestionario_Resultado_N01> E360_Cuestionario_Resultado_N01 { get; set; }
        public virtual ERGOS_Centros_Trabajo_N01 ERGOS_Centros_Trabajo_N01 { get; set; }
        public virtual ERGOS_Cuestionarios_N01 ERGOS_Cuestionarios_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Cuestionarios_Resultados_N01> ERGOS_Cuestionarios_Resultados_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Usuarios_N01> ERGOS_Usuarios_N01 { get; set; }
        public virtual ERGOS_Empresas_N01 ERGOS_Empresas_N01 { get; set; }
    }
}
