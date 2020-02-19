using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class GerenciaController : Controller
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
			return View("Gerencia", new Datos.Modelo.Gerencia());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.Gerencia gerencia)
		{
			bool exito = Datos.Datos.Gerencia.Crear(gerencia);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.Gerencia gerencia = Datos.Datos.Gerencia.ObtenerGerencia(id);

			return View("Gerencia", gerencia);
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.Gerencia gerencia)
		{
			bool exito = Datos.Datos.Gerencia.Modificar(gerencia);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult EstaActivo(int id)
		{
			bool exito = Datos.Datos.Gerencia.EstaActivo(id);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Listar(int id)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			if (id > 0)
			{
				return Json(Datos.Datos.Gerencia.ObtenerGerencia(id));
			}
			else
			{
				return Json(Datos.Datos.Gerencia.ObtenerGerencias(usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
			}
		}
	}
}