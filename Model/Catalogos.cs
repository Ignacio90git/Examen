using ApiComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Model
{
    public class Catalogos
    {

        public static List<DepartamentoDTO> Departamento(string ApiName, bool Filtro, string Url, string Method)
        {
            var lista = Generic<DepartamentoDTO>(ApiName, Url, Method);
            if (Filtro)
                lista.Add(new DepartamentoDTO
                {
                    Id = 0,
                    Nombre = "Todos"
                });
            lista = lista.OrderBy(item => item.Id).ToList();
            return lista;
        }

        public static List<EmpresaDTO> Empresa(string ApiName, bool Filtro, string Url, string Method)
        {
            var lista = Generic<EmpresaDTO>(ApiName, Url, Method);
            if (Filtro)
                lista.Add(new EmpresaDTO
                {
                    Id = 0,
                    Nombre = "Todas"
                });
            lista = lista.OrderBy(item => item.Id).ToList();
            return lista;
        }

        public static List<GeneroDTO> Genero(string ApiName, bool Filtro, string Url, string Method)
        {
            var lista = Generic<GeneroDTO>(ApiName, Url, Method);
            if (Filtro)
                lista.Add(new GeneroDTO
                {
                    Id = 0,
                    Genero = "Todas"
                });
            lista = lista.OrderBy(item => item.Id).ToList();
            return lista;
        }

        private static List<T> Generic<T>(string endpointCatalogo, string url, string Method)
        {
            var apiResponse = RestUtility.CallService<GenericResponse>($"{url}/api/{endpointCatalogo}", string.Empty, null, Method) as GenericResponse;
            if (apiResponse == null) throw new Exception("Error");
            if (!apiResponse.Success) return new List<T>();
            return apiResponse.Data.JToObject<List<T>>();
        }
    }
}
