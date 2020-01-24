﻿using System;
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
      return View("/EstadoEquipo/Modificar", new Datos.Modelo.EstadoEquipo());
    }

    [HttpPost]
    public ActionResult Create(Datos.Modelo.EstadoEquipo estadoequipo)
    {
      bool exito = Datos.Datos.EstadoEquipo.Crear(estadoequipo);

      return Json(new { exito = exito });
    }

    public ActionResult Edit(int id)
    {
      Datos.Modelo.EstadoEquipo estadoequipo = Datos.Datos.EstadoEquipo.ObtenerEstadoEquipo(id);

      return View("/EstadoEquipo/Modificar", estadoequipo);
    }

    [HttpPost]
    public ActionResult Edit(Datos.Modelo.EstadoEquipo estadoequipo)
    {
      bool exito = Datos.Datos.EstadoEquipo.Modificar(estadoequipo);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Delete(int id)
    {
      bool exito = Datos.Datos.EstadoEquipo.Eliminar(id);

      return Json(new { exito = exito });
    }

    [HttpPost]
    public JsonResult Listar(int estadoequipoId)
    {
      if (estadoequipoId > 0)
      {
        return Json(Datos.Datos.EstadoEquipo.ObtenerEstadoEquipo(estadoequipoId));
      }
      else
      {
        return Json(Datos.Datos.EstadoEquipo.ObtenerEstadosEquipos());
      }
    }
  }
}