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
      return View("/Prioridad/Modificar", new Datos.Modelo.Prioridad());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Prioridad prioridad)
    {
      bool exito = Datos.Datos.Prioridad.Crear(prioridad);

      return Json(new { exito = exito });
    }


    public ActionResult Edit(int id)
    {
      Datos.Modelo.Prioridad prioridad = Datos.Datos.Prioridad.ObtenerPrioridad(id);

      return View("/Prioridad/Modificar", id);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Prioridad prioridad)
    {
      bool exito = Datos.Datos.Prioridad.Modificar(prioridad);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.Prioridad.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int id)
    {
      if (id == 0)
      {
        return Json(Datos.Datos.Prioridad.ObtenerPrioridades());
      }
      else
      {
        return Json(Datos.Datos.Prioridad.ObtenerPrioridad(id));
      }
    }
  }
}