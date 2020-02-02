using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{
  public class ClasificacionController : Controller
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
      return View("Clasificacion", new Datos.Modelo.Clasificacion());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.Clasificacion clasificacion)
    {
      bool exito = Datos.Datos.Clasificacion.Crear(clasificacion);

      return Json(new { exito = exito });
    }

    public ActionResult Edit(int id)
    {
      Datos.Modelo.Clasificacion clasificacion = Datos.Datos.Clasificacion.ObtenerClasificacion(id);

      return View("Clasificacion", clasificacion);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.Clasificacion clasificacion)
    {
      bool exito = Datos.Datos.Clasificacion.Modificar(clasificacion);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.Clasificacion.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int id)
      {
        if (id > 0)
        {
          return Json(Datos.Datos.Clasificacion.ObtenerClasificacion(id));
        }
        else
        {
          return Json(Datos.Datos.Clasificacion.ObtenerClasificaciones());
        }
      }
    }
  }

