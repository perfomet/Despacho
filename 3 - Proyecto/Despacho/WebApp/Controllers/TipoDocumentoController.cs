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
      return View("/Cliente/Modificar", new Datos.Modelo.Cliente());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Cliente cliente)
    {
      bool exito = Datos.Datos.Cliente.Crear(cliente);

      return Json(new { exito = exito });
    }

    public ActionResult Edit(int id)
    {
      Datos.Modelo.Cliente cliente = Datos.Datos.Cliente.ObtenerCliente(id);

      return View("/Cliente/Modificar", cliente);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Cliente cliente)
    {
      bool exito = Datos.Datos.Cliente.Modificar(cliente);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Delete(int id)
    {
      bool exito = Datos.Datos.TipoDocumento.Eliminar(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int tipodocumentoId)
    {
      if (tipodocumentoId > 0)
      {
        return Json(Datos.Datos.TipoDocumento.ObtenerTipoDocumento(tipodocumentoId));
      }
      else
      {
        return Json(Datos.Datos.TipoDocumento.ObtenerTiposdeDocumentos());
      }
    }
  }
}