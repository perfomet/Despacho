using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class EstadoEquipoController : Controller
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
      return View("EstadoEquipo", new Datos.Modelo.EstadoEquipo());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.EstadoEquipo estadoequipo, List<Datos.Modelo.BinToEstadoEquipo> bins)
    {
      int id = Datos.Datos.EstadoEquipo.Crear(estadoequipo);

      if(id > 0)
      {
        bins.ForEach((bin) =>
        {
          bin.Estadoequipoid = id;
        });

        Datos.Datos.BinToEstadoEquipo.Eliminar(id);
        Datos.Datos.BinToEstadoEquipo.Crear(bins);
      }

      return Json(new { exito = (id > 0) });
    }

    public ActionResult Edit(int id)
    {
      Datos.Modelo.EstadoEquipo estadoequipo = Datos.Datos.EstadoEquipo.ObtenerEstadoEquipo(id);
      return View("EstadoEquipo", estadoequipo);
      
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.EstadoEquipo estadoequipo, List<Datos.Modelo.BinToEstadoEquipo> bins)
    {
      bool exito = Datos.Datos.EstadoEquipo.Modificar(estadoequipo);

      Datos.Datos.BinToEstadoEquipo.Eliminar(estadoequipo.Estadoequipoid);
      Datos.Datos.BinToEstadoEquipo.Crear(bins);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult EstaActivo(int id)
    {
      bool exito = Datos.Datos.EstadoEquipo.EstaActivo(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int id)
    {
      if (id > 0)
      {
        return Json(Datos.Datos.EstadoEquipo.ObtenerEstadoEquipo(id));
      }
      else
      {
        return Json(Datos.Datos.EstadoEquipo.ObtenerEstadosEquipos());
      }
    }
  }
}