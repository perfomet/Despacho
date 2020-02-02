using System.Web.Mvc;

namespace Despacho.Controllers
{
  public class PerfilController : Controller
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
      return View("Perfil", new Datos.Modelo.Perfil());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Perfil perfil)
    {
      bool exito = Datos.Datos.Perfil.Crear(perfil);

      return Json(new { exito = exito });
    }


    public ActionResult Edit(int id)
    {
      Datos.Modelo.Perfil perfil = Datos.Datos.Perfil.ObtenerPerfil(id);

      return View("Perfil", perfil);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Perfil perfil)
    {
      bool exito = Datos.Datos.Perfil.Modificar(perfil);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.Perfil.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int id)
    {
      if (id == 0)
      {
        return Json(Datos.Datos.Perfil.ObtenerPerfiles());
      }
      else
      {
        return Json(Datos.Datos.Perfil.ObtenerPerfil(id));
      }
    }
  }
}