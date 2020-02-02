using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class TipoDocumentoController : Controller
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
      return View("TipoDocumento", new Datos.Modelo.TipoDocumento());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.TipoDocumento tipodocumento)
    {
      bool exito = Datos.Datos.TipoDocumento.Crear(tipodocumento);

      return Json(new { exito = exito });
    }

    public ActionResult Edit(int id)
    {
      Datos.Modelo.TipoDocumento tipodocumento = Datos.Datos.TipoDocumento.ObtenerTipoDocumento(id);

      return View("TipoDocumento", tipodocumento);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.TipoDocumento tipodocumento)
    {
      bool exito = Datos.Datos.TipoDocumento.Modificar(tipodocumento);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.TipoDocumento.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int tipodocumentoid)
    {
      if (tipodocumentoid > 0)
      {
        return Json(Datos.Datos.TipoDocumento.ObtenerTipoDocumento(tipodocumentoid));
      }
      else
      {
        return Json(Datos.Datos.TipoDocumento.ObtenerTiposdeDocumentos());
      }
    }
  }
}