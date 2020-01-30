using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class UsuarioController : Controller
    {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Details(int id)
    {
      return View();
    }

    public ActionResult Create()
    {
      return View("Usuario", new Datos.Modelo.Usuario());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Usuario usuario)
    {
      bool exito = Datos.Datos.Usuario.Crear(usuario);

      return Json(new { exito = exito });
    }

   
    public ActionResult Edit(int id)
    {
      Datos.Modelo.Usuario usuario = Datos.Datos.Usuario.ObtenerUsuario(id);

      return View("Usuario", usuario);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Usuario usuario)
    {
      bool exito = Datos.Datos.Usuario.Modificar(usuario);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.Usuario.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int id)
    {
      if (id == 0)
      {
        return Json(Datos.Datos.Usuario.ObtenerUsuarios());
      }
      else
      {
        return Json(Datos.Datos.Usuario.ObtenerUsuario(id));
      }
    }
  }
}