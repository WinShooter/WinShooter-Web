namespace WinShooter.Controllers
{
    using System.Web.Mvc;

    public class AccountController : Controller
    {
        /// <summary>
        /// GET: /Home/Index/{id}
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login()
        {
            return this.View();
        }
    }
}