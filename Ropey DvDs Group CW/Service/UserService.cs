namespace Ropey_DvDs_Group_CW.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor httpContext)
        {
            _contextAccessor = httpContext;
        }

        public string GetUser()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }

        public bool isAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
