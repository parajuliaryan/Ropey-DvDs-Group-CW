using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ropey_DvDs_Group_CW.Controllers
{
    public class AdminDashboard : Controller
    {
        // GET: AdminDashboard
        [Authorize(Roles = "Manager,Assistant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult Index()
        {
            return View();
        }

       
    }
}
