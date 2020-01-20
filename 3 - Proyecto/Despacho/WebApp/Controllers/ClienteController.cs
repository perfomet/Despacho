using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class ClienteController : Controller
    {
        #region Cliente

        public ActionResult Cliente()
        {
            return View();
        }

        #endregion

        #region Unidad Negocio

        public ActionResult UnidadNegocio()
        {
            return View();
        }

        #endregion

        #region Gerencia

        public ActionResult Gerencia()
        {
            return View();
        }

        #endregion
    }
}
