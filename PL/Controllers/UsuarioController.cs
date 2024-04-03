using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            Dictionary<string, object> diccionario = BL.Usuario.GetAll();
            bool resultado = (bool)diccionario["Resultado"];
            if (resultado == true)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = (ML.Usuario)diccionario["Usuario"];
                return View(usuario);

            }
            else
            {
                string excepcion = (string)diccionario["Excepcion"];

            }
            return View();
        }



        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario != null)
            {
                Dictionary<string, object> result = BL.Usuario.GetById(IdUsuario.Value);
                bool resultado = (bool)result["Resultado"];

                if (resultado == true)
                {
                    usuario = (ML.Usuario)result["Usuario"];
                    return View(usuario);
                }
                
                
                else
                {
                    string exepcion = (string)result["Excepcion"];
                    return View(usuario);
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario > 0)
            {
                Dictionary<string, object> result = BL.Usuario.UpDate(usuario);
                bool resultado = (bool)result["Resultado"];

                if (resultado == true)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                Dictionary<string, object> result = BL.Usuario.Add(usuario);
                bool resultado = (bool)result["Resultado"];

                if (resultado == true)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int idUsuario)
        {
            Dictionary<string, object> result = BL.Usuario.Delete(idUsuario);
            bool resultado = (bool)result["Resultado"];

            if (resultado == true)
            {
                return View();
            }
            else
            {
                return View();
            }
        }


    }
}

