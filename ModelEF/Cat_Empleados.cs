//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cat_Empleados
    {
        public long Id { get; set; }
        public long IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public short IdGenero { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual Cat_Departamento Cat_Departamento { get; set; }
        public virtual Cat_Genero Cat_Genero { get; set; }
    }
}