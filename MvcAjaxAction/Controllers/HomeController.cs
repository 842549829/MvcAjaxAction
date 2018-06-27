using AjaxHandler;
using MvcAjaxAction.Models;
using System.Web.Mvc;

namespace MvcAjaxAction.Controllers
{
    public class HomeController : WebAjax
    {
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns>view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 有参Ationc
        /// </summary>
        /// <param name="paramters">paramters</param>
        /// <returns>json</returns>
        [JsAction]
        public ActionResult GetParameters(Paramters paramters)
        {
            return Json(new { ok = "ok" });
        }

        /// <summary>
        /// 无参Action
        /// </summary>
        /// <returns>json</returns>
        [JsAction]
        public ActionResult NoParameters()
        {
            return Json(new { ok = "ok" });
        }
    }
}