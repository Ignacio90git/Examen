using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class EmpresaDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }

        public static EmpresaDTO FromModel(Cat_Empresa model)
        {
            return new EmpresaDTO()
            {
                Id = model.Id,
                Nombre = model.Nombre,
            };
        }

        public Cat_Empresa ToModel()
        {
            return new Cat_Empresa()
            {
                Id = Id,
                Nombre = Nombre,
            };
        }
    }
}