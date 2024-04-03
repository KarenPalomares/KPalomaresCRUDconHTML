using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static Dictionary<string, object> Add(ML.Usuario usuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Exception", "" }, { "Resultado", false } };
            try
            {
                using (DL.KpalomaresHtmlContext context = new DL.KpalomaresHtmlContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioAdd'{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}',{usuario.Edad},'{usuario.Email}','{usuario.Password}'");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Exepcion"] = ex.Message;
            }
            return diccionario;
        }

        public static Dictionary<string, object> Delete(int IdUsuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", "" }, { "Resultado", false } };
            try
            {
                using (DL.KpalomaresHtmlContext context = new DL.KpalomaresHtmlContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }

            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Exepcion"] = ex.Message;
            }

            return diccionario;
        }

        public static Dictionary<string, object> UpDate(ML.Usuario usuario)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", "" }, { "Resultado", false } };
            try
            {
                using (DL.KpalomaresHtmlContext context = new DL.KpalomaresHtmlContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioUpDate {usuario.IdUsuario}, '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}',{usuario.Edad},'{usuario.Email}','{usuario.Password}'");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }

            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", "" }, { "Resultado", false }, { "Usuario", usuario } };

            try
            {
                using (DL.KpalomaresHtmlContext context = new DL.KpalomaresHtmlContext())
                {
                    var objeto = (from user in context.Usuarios
                                  where user.IdUsuario == idUsuario
                                  select new
                                  {
                                      IdUsuario = user.IdUsuario,
                                      Nombre = user.Nombre,
                                      ApellidoPaterno = user.ApellidoPaterno,
                                      ApellidoMaterno = user.ApellidoMaterno,
                                      Edad = user.Edad,
                                      Email = user.Email,
                                      Password = user.Password
                                  }).SingleOrDefault();
                    if (objeto != null)
                    {
                        usuario.IdUsuario = objeto.IdUsuario;
                        usuario.Nombre = objeto.Nombre;
                        usuario.ApellidoPaterno = objeto.ApellidoPaterno;
                        usuario.ApellidoMaterno = objeto.ApellidoMaterno;
                        usuario.Edad = objeto.Edad.Value;
                        usuario.Email = objeto.Email;
                        usuario.Password= objeto.Password;

                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;

        }

        public static Dictionary<string, object> GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", "" }, { "Resultado", false }, { "Usuario", null } };
            try
            {
                using (DL.KpalomaresHtmlContext context = new DL.KpalomaresHtmlContext())
                {
                    var registros = (from user in context.Usuarios
                                     select new
                                     {
                                         IdUsuario = user.IdUsuario,
                                         Nombre = user.Nombre,
                                         ApellidoPaterno = user.ApellidoPaterno,
                                         ApellidoMaterno = user.ApellidoMaterno,
                                         Edad = user.Edad,
                                         Email = user.Email,
                                         Password = user.Password
                                     }).ToList();
                    if (registros.Count > 0)
                    {
                        usuario.Usuarios = new List<object>();
                        foreach (var registro in registros)
                        {
                            ML.Usuario users = new ML.Usuario();
                            users.IdUsuario = registro.IdUsuario;
                            users.Nombre = registro.Nombre;
                            users.ApellidoPaterno = registro.ApellidoPaterno;
                            users.ApellidoMaterno = registro.ApellidoMaterno;
                            users.Edad = registro.Edad.Value;
                            users.Email = registro.Email;
                            users.Password = registro.Password;

                            usuario.Usuarios.Add(users);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }


    }
}
