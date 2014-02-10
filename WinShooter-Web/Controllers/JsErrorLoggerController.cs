using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinShooter.Controllers
{
    public class JsErrorLoggerController : Controller
    {
        private ILog log = LogManager.GetLogger(typeof(JsErrorLoggerController));

        public ActionResult Index(string error, string url, string line, string column, string stacktrace)
        {
            log.ErrorFormat("Error in client side js. Error: {0}, Url: {1}, Line: {2}, Column: {3}, Stacktrace: {4}", 
                                error, 
                                url, 
                                line, 
                                column, 
                                stacktrace);
            return Json("Ok");
        }
	}
}