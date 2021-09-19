using Model;
using ModelEF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Utils;

namespace API.Examen.Controllers
{
    public class AdminController : ApiController
    {
        #region EMPLEADOS
        [HttpPost]
        [Route("api/read")]
        public GenericResponse EmployyeRead(EmpleadoDTO model)//long IdEmpresa, long IdDepartamento, string Nombre = null)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var listaObj = new List<VEmpeladosWeb>();
                    var Nombre = model.Nombre == null ? "" : model.Nombre;
                    listaObj = context.VEmpeladosWeb.Where(item =>
                        (string.IsNullOrEmpty(Nombre) || item.Nombre.Contains(Nombre))
                       && (model.IdDepartamento == 0 || item.IdDepartamento == model.IdDepartamento)
                   ).ToList();

                    mResponse.Data = listaObj.AsEnumerable().Select(VEmpeladosWebDTO.FromModel).OrderByDescending(item => item.Nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = ex.ToString();
                mResponse.Data = null;
            }

            return mResponse;
        }

        [HttpGet]
        [Route("api/detail")]
        public GenericResponse EmployyeDetail(long Id)
        {
            var mResponse = new GenericResponse();
            try
            {
                EmpleadoDTO empleado;
                using (var context = new ExamenEntities())
                {
                    var listaEntities = context.Cat_Empleados.Find(Id);
                    if (listaEntities == null) throw new Exception("Error");
                    empleado = EmpleadoDTO.FromModel(listaEntities);
                }

                mResponse.Data = empleado;
            }
            catch (Exception ex)
            {
                //log
                mResponse.Success = false;
                mResponse.Message = ex.ToString();
                mResponse.Data = null;
            }

            return mResponse;
        }

        [HttpPost]
        [Route("api/create")]
        public GenericResponse EmployeeCreate(EmpleadoDTO empleDto)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var exists = context.Cat_Empleados.Any(entityItem =>
                        entityItem.Nombre.Equals(empleDto.Nombre, StringComparison.OrdinalIgnoreCase));
                    if (exists)
                        throw new Exception($"El empleado con nombre {empleDto.Nombre} ya se encuentra registrado");
                    var empleado = empleDto.ToModel();
                    empleado.Activo = true;
                    empleado.FechaIngreso = DateTime.Now;
                    context.Cat_Empleados.Add(empleado);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.SaveChanges();
                            transaction.Commit();
                            mResponse.Message = "Ok";
                            mResponse.Data = empleado.Id;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }

        [HttpPost]
        [Route("api/update")]
        public GenericResponse EmployeeUpdate(EmpleadoDTO empleDto)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var existente = context.Cat_Empleados.Find(empleDto.Id);
                    var dtoBefore = EmpleadoDTO.FromModel(existente);
                    var dtoAfter = empleDto;
                    if (existente == null) throw new Exception("Ya existe");
                    var updated = empleDto.ToModel();
                    updated.Activo = true;
                    context.Entry(existente).CurrentValues.SetValues(updated);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.SaveChanges();
                            transaction.Commit();
                            mResponse.Message = "Se actualizo correcto";
                            mResponse.Data = existente.Id;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }

        [HttpGet]
        [Route("api/delete")]
        public GenericResponse EmployeeDelete(long Id)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var existente = context.Cat_Empleados.Find(Id);
                    var dtoBefore = EmpleadoDTO.FromModel(existente);
                    var dtoAfter = existente;
                    if (existente == null) throw new Exception("Ya existe");
                    var updated = existente;
                    context.Entry(existente).CurrentValues.SetValues(updated);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            updated.Activo = false;
                            context.SaveChanges();
                            transaction.Commit();
                            mResponse.Message = "Se actualizo correcto";
                            mResponse.Data = existente.Id;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }
        #endregion

        #region CATALOGOS
        [HttpGet]
        [Route("api/departamento")]
        public GenericResponse Departamento()
        {
            string cnstr = ConfigurationManager.ConnectionStrings["ExamenEntities"].ToString();
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    mResponse.Data = null;
                    var listaObj = context.Cat_Departamento.Where(w => w.Nombre != "").ToList();
                    if (listaObj != null)
                        mResponse.Data = listaObj.AsEnumerable().Select(DepartamentoDTO.FromModel).OrderByDescending(item => item.Nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = ex.ToString();
                mResponse.Data = null;
            }

            return mResponse;
        }

        [HttpGet]
        [Route("api/empresa")]
        public GenericResponse Empresa()
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    mResponse.Data = null;
                    var listaObj = context.Cat_Empresa.ToList();
                    if (listaObj != null)
                        mResponse.Data = listaObj.AsEnumerable().Select(EmpresaDTO.FromModel).OrderByDescending(item => item.Nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = ex.ToString();
                mResponse.Data = null;
            }

            return mResponse;
        }

        [HttpGet]
        [Route("api/genero")]
        public GenericResponse Genero()
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    mResponse.Data = null;
                    var listaObj = context.Cat_Genero.ToList();
                    if (listaObj != null)
                        mResponse.Data = listaObj.AsEnumerable().Select(GeneroDTO.FromModel).OrderByDescending(item => item.Genero).ToList();
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = ex.ToString();
                mResponse.Data = null;
            }

            return mResponse;
        }
        #endregion

        #region USUARIO
        [HttpPost]
        [Route("api/createUser")]
        public GenericResponse UserCreate(UsuarioDTO userDTO)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var exists = context.Cat_Usuarios.Any(entityItem =>
                        entityItem.Nombre.Equals(userDTO.UserName, StringComparison.OrdinalIgnoreCase));
                    if (exists)
                        throw new Exception($"El empleado con nombre {userDTO.UserName} ya se encuentra registrado");
                    var userEntity = userDTO.ToModel();
                    userEntity.Activo = true;
                    context.Cat_Usuarios.Add(userEntity);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.SaveChanges();
                            transaction.Commit();
                            mResponse.Message = "Ok";
                            mResponse.Data = userEntity.Id;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }

        [HttpPost]
        [Route("api/readUser")]
        public GenericResponse UserRead(UsuarioDTO model)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var listaObj = new List<Cat_Usuarios>();
                    var Nombre = model.Nombre == null ? "" : model.Nombre;
                    if (model.IdEmpresa == 0)
                        listaObj = context.Cat_Usuarios.Where(w => w.Nombre.Contains(Nombre) && w.IdEmpresa > model.IdEmpresa && w.Activo == true).ToList();
                    else
                        listaObj = context.Cat_Usuarios.Where(w => w.Nombre.Contains(Nombre) && w.IdEmpresa == model.IdEmpresa && w.Activo == true).ToList();

                    mResponse.Data = listaObj.AsEnumerable().Select(UsuarioDTO.FromModel).OrderBy(item => item.Nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = ex.ToString();
                mResponse.Data = null;
            }

            return mResponse;
        }

        [HttpGet]
        [Route("api/detailUser")]
        public GenericResponse UserDetail(long Id)
        {
            var mResponse = new GenericResponse();
            try
            {
                UsuarioDTO usuario;
                using (var context = new ExamenEntities())
                {
                    var listaEntities = context.Cat_Usuarios.Find(Id);
                    if (listaEntities == null) throw new Exception("Error");
                    usuario = UsuarioDTO.FromModel(listaEntities);
                }

                mResponse.Data = usuario;
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }

            return mResponse;
        }

        [HttpPost]
        [Route("api/updateUser")]
        public GenericResponse UserUpdate(UsuarioDTO model)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var existente = context.Cat_Usuarios.Find(model.Id);
                    var dtoBefore = UsuarioDTO.FromModel(existente);
                    var dtoAfter = model;
                    if (existente == null) throw new Exception("Ya existe");
                    var update = model.ToModel();
                    update.Activo = true;
                    context.Entry(existente).CurrentValues.SetValues(update);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.SaveChanges();
                            transaction.Commit();
                            mResponse.Message = "Se actualizo correcto";
                            mResponse.Data = existente.Id;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }

        [HttpGet]
        [Route("api/deleteUser")]
        public GenericResponse UserDelete(long Id)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var existente = context.Cat_Usuarios.Find(Id);
                    var dtoBefore = UsuarioDTO.FromModel(existente);
                    var dtoAfter = existente;
                    if (existente == null) throw new Exception("Ya existe");
                    var updated = existente;
                    context.Entry(existente).CurrentValues.SetValues(updated);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            updated.Activo = false;
                            context.SaveChanges();
                            transaction.Commit();
                            mResponse.Message = "Se actualizo correcto";
                            mResponse.Data = existente.Id;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }
        #endregion

        #region LOGIN
        [HttpPost]
        [Route("api/validation")]
        public GenericResponse Validation(UsuarioDTO userDTO)
        {
            var mResponse = new GenericResponse();
            try
            {
                using (var context = new ExamenEntities())
                {
                    var exists = context.Cat_Usuarios.FirstOrDefault(f => f.UserName == userDTO.UserName && f.UserPassword == userDTO.UserPassword);
                    if (exists != null)
                    {
                        mResponse.Data = null;
                        mResponse.Success = true;
                    }
                    else
                        throw new Exception($"El usuario {userDTO.UserName} no esta registrado");
                }
            }
            catch (Exception ex)
            {
                mResponse.Success = false;
                mResponse.Message = "";
                mResponse.Data = null;
            }
            return mResponse;
        }

        #endregion
    }
}