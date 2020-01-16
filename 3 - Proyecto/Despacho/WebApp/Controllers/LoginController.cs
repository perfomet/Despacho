using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObtenerUsuario(string usuario)
        {
            Datos.Modelo.Usuario user = Datos.Datos.Usuario.ObtenerUsuario(usuario);
            user.Password = "";

            return Json(user);
        }

        [HttpPost]
        public JsonResult Ingresar(string usuario, string clave)
        {
            Datos.Modelo.Usuario user = Datos.Datos.Usuario.ObtenerUsuario(usuario);

            if (user.Password != null)
            {
                if (user.Password.Equals(clave))
                {
                    return Json(new { exito = true, usuario = user });
                }
            }

            return Json(new { exito = false });
        }
    }
}