using ApiComm;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utils;

namespace Examen.Controllers
{
    public class AdminController : Controller
    {
        #region EMPLEADOS
        public ActionResult Empleados()
        {
            var model = new EmpleadoDTO();
            Task.WaitAll(new List<Task>
            {
                Task.Factory.StartNew(() =>
                {
                    model.DepartamentoList = Catalogos.Departamento("departamento", true, "http://localhost:51571", "GET");
                    model.EmpresaList = Catalogos.Empresa("empresa", true, "http://localhost:51571", "GET");
                    model.GeneroList = Catalogos.Genero("genero", false, "http://localhost:51571", "GET");
                }),
            }.ToArray());
            return View("Empleados", model);
        }

        public async Task<ActionResult> BuscarEmpleados(EmpleadoDTO model)
        {
            var response = new GenericResponse();
            try
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>($"http://localhost:51571//api/read", string.Empty, model, "POST", null) as GenericResponse;
                if (apiResponse == null) throw new Exception("Error");
                response = apiResponse;
                if (apiResponse.Success) apiResponse.Data = apiResponse.Data.JToObject<List<VEmpeladosWebDTO>>();
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                response.Success = false;
                response.Message = message;
                response.Data = null;
            }
            return response.ToJsonResult();
        }

        public ActionResult DetalleEmpleado(long Id)
        {
            var model = new EmpleadoDTO
            {
                DepartamentoList = new List<DepartamentoDTO>(),
                EmpresaList = new List<EmpresaDTO>()
            };
            if (Id > 0)
            {
                var apiResponse = RestUtility.CallService<GenericResponse>($"http://localhost:51571//api/detail", string.Empty, null, "GET", new { Id }) as GenericResponse;
                if (apiResponse == null) throw new Exception("Error");
                if (!apiResponse.Success) return apiResponse.ToJsonResult();
                model = apiResponse.Data.JToObject<EmpleadoDTO>();

            }
            Task.WaitAll(new List<Task>
            {
                Task.Factory.StartNew(() =>
                {
                    model.DepartamentoList = Catalogos.Departamento("departamento", false, "http://localhost:51571", "GET");
                    model.EmpresaList = Catalogos.Empresa("empresa", false, "http://localhost:51571", "GET");
                    model.GeneroList = Catalogos.Genero("genero", false, "http://localhost:51571", "GET");
                }),
            }.ToArray());

            return View("DetalleEmpleado", model);
        }

        public async Task<ActionResult> EliminarEmpleado(long Id)
        {
            var response = new GenericResponse();
            try
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>($"http://localhost:51571/api/delete", string.Empty, null, "GET", new { Id }) as GenericResponse;
                if (apiResponse == null)
                {
                    throw new Exception("Error");
                }
                response = apiResponse;
            }
            catch (Exception ex)
            {
                var message = "";
                response.Success = false;
                response.Message = message;
                response.Data = null;
            }

            return response.ToJsonResult();
        }


        public async Task<ActionResult> CrearEmpleado(EmpleadoDTO model)
        {
            var response = new GenericResponse();
            try
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>(model.Id <= 0 ? $"http://localhost:51571/api/create" : $"http://localhost:51571/api/update",
                    string.Empty, model, "POST", null) as GenericResponse;
                if (apiResponse == null)
                {
                    throw new Exception("Error");
                }
                response = apiResponse;
            }
            catch (Exception ex)
            {
                var message = "";
                response.Success = false;
                response.Message = message;
                response.Data = null;
            }

            return response.ToJsonResult();
        }

        #endregion

        #region USUARIO
        public ActionResult Usuarios()
        {
            var model = new UsuarioDTO();
            Task.WaitAll(new List<Task>
            {
                Task.Factory.StartNew(() =>
                {
                    model.EmpresaList = Catalogos.Empresa("empresa", true, "http://localhost:51571", "GET");
                }),
            }.ToArray());
            return View("Usuarios", model);
        }

        public async Task<ActionResult> BuscarUsuario(UsuarioDTO model)
        {
            var response = new GenericResponse();
            try
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>($"http://localhost:51571//api/readUser", string.Empty, model, "POST", null) as GenericResponse;
                if (apiResponse == null) throw new Exception("Error");
                response = apiResponse;
                if (apiResponse.Success) apiResponse.Data = apiResponse.Data.JToObject<List<UsuarioDTO>>();
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                response.Success = false;
                response.Message = message;
                response.Data = null;
            }
            return response.ToJsonResult();
        }

        public ActionResult DetalleUsuario(long Id)
        {
            var model = new UsuarioDTO
            {
                EmpresaList = new List<EmpresaDTO>()
            };
            if (Id > 0)
            {
                var apiResponse = RestUtility.CallService<GenericResponse>($"http://localhost:51571//api/detailUser", string.Empty, null, "GET", new { Id }) as GenericResponse;
                if (apiResponse == null) throw new Exception("Error");
                if (!apiResponse.Success) return apiResponse.ToJsonResult();
                model = apiResponse.Data.JToObject<UsuarioDTO>();

            }
            Task.WaitAll(new List<Task>
            {
                Task.Factory.StartNew(() =>
                {
                    model.EmpresaList = Catalogos.Empresa("empresa", false, "http://localhost:51571", "GET");
                }),
            }.ToArray());

            return View("DetalleUsuario", model);
        }

        public async Task<ActionResult> EliminarUsuario(long Id)
        {
            var response = new GenericResponse();
            try
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>($"http://localhost:51571/api/deleteUser", string.Empty, null, "GET", new { Id }) as GenericResponse;
                if (apiResponse == null)
                {
                    throw new Exception("Error");
                }
                response = apiResponse;
            }
            catch (Exception ex)
            {
                var message = "";
                response.Success = false;
                response.Message = message;
                response.Data = null;
            }

            return response.ToJsonResult();
        }

        public async Task<ActionResult> CrearUsuario(UsuarioDTO model)
        {
            var response = new GenericResponse();
            try
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>(model.Id <= 0 ? $"http://localhost:51571/api/createUser" : $"http://localhost:51571/api/updateUser",
                    string.Empty, model, "POST", null) as GenericResponse;
                if (apiResponse == null)
                {
                    throw new Exception("Error");
                }
                response = apiResponse;
            }
            catch (Exception ex)
            {
                var message = "";
                response.Success = false;
                response.Message = message;
                response.Data = null;
            }

            return response.ToJsonResult();
        }
        #endregion

    }
}