namespace WinShooter.Api
{
    using System.Web;
    using System.Web.Http;

    using WinShooter.Logic.Authentication;

    public abstract class BaseApiController : ApiController
    {
        protected virtual CustomPrincipal Principal
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }
    }
}
