using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class UnidadNegocioController : Controller
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
			return View("UnidadNegocio", new Datos.Modelo.UnidadNegocio());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.UnidadNegocio unidadnegocio)
		{
			bool exito = Datos.Datos.UnidadNegocio.Crear(unidadnegocio);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.UnidadNegocio unidadnegocio = Datos.Datos.UnidadNegocio.ObtenerUnidadNegocio(id);
			return View("UnidadNegocio", unidadnegocio);

		}

		[HttpPost]
		public JsonResult EstaActivo(int id)
		{
			bool exito = Datos.Datos.UnidadNegocio.EstaActivo(id);
			return Json(new { exito = exito });
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.UnidadNegocio unidadnegocio)
		{
			bool exito = Datos.Datos.UnidadNegocio.Modificar(unidadnegocio);

			return Json(new { exito = exito });
		}


		[HttpPost]
		public JsonResult Listar(int Id)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			if (Id > 0)
			{
				return Json(Datos.Datos.UnidadNegocio.ObtenerUnidadNegocio(Id));
			}
			else
			{
				return Json(Datos.Datos.UnidadNegocio.ObtenerUnidadesNegocio(usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
			}
		}
	}
}