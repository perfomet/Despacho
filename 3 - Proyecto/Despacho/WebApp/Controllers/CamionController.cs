using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{
  public class CamionController : Controller
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
      return View("/Camiones/Modificar", new Datos.Modelo.Camion());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Camion camion)
    {
      bool exito = Datos.Datos.Camion.Crear(camion);

      return Json(new { exito = exito });
    }

    public ActionResult Edit(string id)
    {
      Datos.Modelo.Camion camion = Datos.Datos.Camion.ObtenerCamion(id);

      return View("/Camion/Modificar", camion);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Camion camion)
    {
      bool exito = Datos.Datos.Camion.Modificar(camion);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Delete(string id)
    {
      bool exito = Datos.Datos.Camion.Eliminar(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(string patentecamion)
    {
      if (patentecamion == "")
      {
        return Json(Datos.Datos.Camion.ObtenerCamiones());
      }
      else
      {
        return Json(Datos.Datos.Camion.ObtenerCamion(patentecamion));
      }
    }
  }
}
