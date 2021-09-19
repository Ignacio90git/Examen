using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public long IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool? Activo { get; set; }

        public List<EmpresaDTO> EmpresaList { get; set; }

        public static UsuarioDTO FromModel(Cat_Usuarios model)
        {
            return new UsuarioDTO()
            {
                Id = model.Id,
                IdEmpresa = model.IdEmpresa,
                Nombre = model.Nombre,
                UserName = model.UserName,
                UserPassword = model.UserPassword,
                Activo = model.Activo,
            };
        }

        public Cat_Usuarios ToModel()
        {
            return new Cat_Usuarios()
            {
                Id = Id,
                IdEmpresa = IdEmpresa,
                Nombre = Nombre,
                UserName = UserName,
                UserPassword = UserPassword,
                Activo = Activo,
            };
        }
    }
}