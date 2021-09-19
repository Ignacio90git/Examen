
using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Model
{
    public class EmpleadoDTO
    {
        public long Id { get; set; }
        public long IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public short IdGenero { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool? Activo { get; set; }

        public List<EmpresaDTO> EmpresaList { get; set; }
        public List<DepartamentoDTO> DepartamentoList { get; set; }
        public List<GeneroDTO> GeneroList { get; set; }

        public static EmpleadoDTO FromModel(Cat_Empleados model)
        {
            return new EmpleadoDTO()
            {
                Id = model.Id,
                Nombre = model.Nombre,
                FechaNacimiento = model.FechaNacimiento,
                Correo = model.Correo,
                IdGenero = model.IdGenero,
                Telefono = model.Telefono,
                Celular = model.Celular,
                FechaIngreso = model.FechaIngreso,
                Activo = model.Activo,
                IdDepartamento = model.IdDepartamento,
            };
        }

        public Cat_Empleados ToModel()
        {
            return new Cat_Empleados()
            {
                Id = Id,
                Nombre = Nombre,
                FechaNacimiento = FechaNacimiento,
                Correo = Correo,
                IdGenero = IdGenero,
                Telefono = Telefono,
                Celular = Celular,
                FechaIngreso = FechaIngreso,
                Activo = Activo,
                IdDepartamento = IdDepartamento,
            };
        }
    }
}