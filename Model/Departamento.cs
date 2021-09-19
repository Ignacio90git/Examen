using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class DepartamentoDTO
    {
        public long Id { get; set; }
        public long IdEmpresa { get; set; }
        public string Nombre { get; set; }

        public static DepartamentoDTO FromModel(Cat_Departamento model)
        {
            return new DepartamentoDTO()
            {
                Id = model.Id,
                IdEmpresa = model.IdEmpresa,
                Nombre = model.Nombre,
            };
        }

        public Cat_Departamento ToModel()
        {
            return new Cat_Departamento()
            {
                Id = Id,
                IdEmpresa = IdEmpresa,
                Nombre = Nombre,
            };
        }
    }
}