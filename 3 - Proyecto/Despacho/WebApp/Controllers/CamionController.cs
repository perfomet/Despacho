using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class CamionController : Controller
    {
        // GET: Camion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Camion/Details/5
        public ActionResult Details(string patente)
        {
            return View();
        }

        // GET: Camion/Create
        public ActionResult Create()
        {
          return View("/Camion/Modificar", new Datos.Modelo.Camion());
        }

    // POST: Camion/Create
    [HttpPost]
    public ActionResult Create(Datos.Modelo.Camion camion)
    {
      bool exito = Datos.Datos.Camion.Crear(camion);

      return Json(new { exito = exito });
    }
        // GET: Camion/Edit/5
        public ActionResult Edit(string id)
        {
          Datos.Modelo.Camion camion = Datos.Datos.Camion.ObtenerCamion(id);
          return View("/Camion/Modificar", camion);
        }

        // POST: Camion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Datos.Modelo.Camion camion)
        {
          bool exito = Datos.Datos.Camion.Modificar(camion);

          return Json(new { exito = exito });
        }

        // GET: Camion/Delete/5
        public ActionResult Delete(string id)
        {
          bool exito = Datos.Datos.Modelo.Camion.Eliminar(id);

          return Json(new { exito = exito });
        }

        // POST: Camion/Delete/5
        [HttpPost]
    public JsonResult Listar(string id)
    {
      if ((id == "")
      {
        return Json(Datos.Datos.Camion.ObtenerCamiones());
      }
      else
      {
        return Json(Datos.Datos.Camion.ObtenerCamion(id));
      }
    }
  }
}
