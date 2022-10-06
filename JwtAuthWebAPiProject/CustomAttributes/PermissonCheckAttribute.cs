using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace JwtAuthWebAPiProject.CustomAttributes
{
    public class PermissonCheckAttribute : TypeFilterAttribute
    {
        public PermissonCheckAttribute(string claimType, string claimValue) : base(typeof(PermissionCheckFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
        
    }
    public class PermissionCheckFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;

        public PermissionCheckFilter(Claim claim,IUserRepository userRepository,IMemoryCache memoryCache)
        {
            _claim = claim;
           _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var permissionName = _claim.Value;
            var permissionNames=_claim.Value.Split(',').ToList();
            //_memoryCache.TryGetValue("UserEmail",out string userEmail);
            var permissions = _memoryCache.TryGetValue("Permissions", out List<Permisson> userPermissions);

            //var hasClaim = pers.Any(p=>p.Name== permissionName);
            var hasClaim = false;
            if (permissionNames != null)
            {
                foreach (var per in permissionNames)
                {
                    hasClaim = userPermissions.Any(p => p.Name == per);
                    if (hasClaim)
                        break;
                }
            }
            
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
