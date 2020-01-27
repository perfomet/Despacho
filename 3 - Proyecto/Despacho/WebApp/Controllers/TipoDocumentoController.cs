using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
      return View("/TipoDocumento/Modificar", new Datos.Modelo.TipoDocumento());
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

      return View("/TipoDocumento/Modificar", tipodocumento);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.TipoDocumento tipodocumento)
    {
      bool exito = Datos.Datos.TipoDocumento.Modificar(tipodocumento);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Delete(int id)
    {
      bool exito = Datos.Datos.TipoDocumento.Eliminar(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int Id)
    {
      if (Id > 0)
      {
        return Json(Datos.Datos.TipoDocumento.ObtenerTipoDocumento(Id));
      }
      else
      {
        return Json(Datos.Datos.TipoDocumento.ObtenerTiposdeDocumentos());
      }
    }
  }
}