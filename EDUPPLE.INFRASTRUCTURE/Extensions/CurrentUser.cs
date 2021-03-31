using Microsoft.AspNetCore.Http;

namespace EDUPPLE.INFRASTRUCTURE.Extensions
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
        }
        public bool IsAuthenticated => _httpContext.User.Identity.IsAuthenticated;
        public string UserName => _httpContext.User.Identity.Name;
    }
}
