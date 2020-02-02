
using System.Web.Mvc;

namespace Despacho.Controllers
{
  public class Enlaces : Controller
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
      return View("Enlaces", new Datos.Modelo.Enlaces());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Enlaces enlaces)
    {
      bool exito = Datos.Datos.Enlaces.Crear(enlaces);

      return Json(new { exito = exito });
    }

    public ActionResult Edit(int id)
    {
      Datos.Modelo.Enlaces enlaces = Datos.Datos.Enlaces.ObtenerEnlace(id);
      return View("Enlaces", enlaces);

    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Enlaces enlaces)
    {
      bool exito = Datos.Datos.Enlaces.Modificar(enlaces);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.Enlaces.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int id)
    {
      if (id > 0)
      {
        return Json(Datos.Datos.Enlaces.ObtenerEnlace(id));
      }
      else
      {
        return Json(Datos.Datos.Enlaces.ObtenerEnlaces());
      }
    }
  }
}