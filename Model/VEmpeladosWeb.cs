using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VEmpeladosWebDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public short IdGenero { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public DateTime FechaIngreso { get; set; }
        public long IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public string NombreEmpresa { get; set; }

        public static VEmpeladosWebDTO FromModel(VEmpeladosWeb model)
        {
            return new VEmpeladosWebDTO()
            {
                Id = model.Id,
                Nombre = model.Nombre,
                FechaNacimiento = model.FechaNacimiento,
                IdGenero = model.IdGenero,
                Genero = model.Genero,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Celular = model.Celular,
                FechaIngreso = model.FechaIngreso,
                IdDepartamento = model.IdDepartamento,
                NombreDepartamento = model.NombreDepartamento,
                NombreEmpresa = model.NombreEmpresa
            };
        }

        public VEmpeladosWeb ToModel()
        {
            return new VEmpeladosWeb()
            {
                Id = Id,
                Nombre = Nombre,
                FechaNacimiento = FechaNacimiento,
                IdGenero = IdGenero,
                Genero = Genero,
                Correo = Correo,
                Telefono = Telefono,
                Celular = Celular,
                FechaIngreso = FechaIngreso,
                IdDepartamento = IdDepartamento,
                NombreDepartamento = NombreDepartamento,
                NombreEmpresa = NombreEmpresa
            };
        }

    }
}
