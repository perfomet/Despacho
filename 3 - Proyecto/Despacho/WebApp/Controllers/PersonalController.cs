
using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class PersonalController : Controller
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
			return View("Personal", new Datos.Modelo.Personal());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.Personal personal)
		{
			bool exito = Datos.Datos.Personal.Crear(personal);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.Personal personal = Datos.Datos.Personal.ObtenerPersonal(id);
			return View("Personal", personal);

		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.Personal personal)
		{
			bool exito = Datos.Datos.Personal.Modificar(personal);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult EstaActivo(int id)
		{
			bool exito = Datos.Datos.Personal.EstaActivo(id);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Listar(int id)
		{
			if (id > 0)
			{
				return Json(Datos.Datos.Personal.ObtenerPersonal(id));
			}
			else
			{
				return Json(Datos.Datos.Personal.ObtenerPersonal());
			}
		}
	}
}