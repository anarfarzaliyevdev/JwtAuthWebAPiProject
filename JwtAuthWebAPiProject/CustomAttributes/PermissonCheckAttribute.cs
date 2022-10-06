using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Security.Claims;

namespace JwtAuthWebAPiProject.CustomAttributes
{
    
    public class PermissonCheckAttribute : TypeFilterAttribute
    {
        public PermissonCheckAttribute(string claimValue) : base(typeof(PermissionCheckFilter))
        {
            Arguments = new object[] { new Claim("",claimValue) };
        }
        
    }
    public class PermissionCheckFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public PermissionCheckFilter(Claim claim,IUserRepository userRepository,IMemoryCache memoryCache,IHttpContextAccessor httpContextAccessor)
        {
            _claim = claim;
           _userRepository = userRepository;
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
            
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            
            //get user from context
            var userEmail = context.HttpContext.User.Claims.FirstOrDefault(c=>c.Type=="Email").Value;
            // check is cahced or not, if not then cache
            var isUserPermissionsCached = _memoryCache.TryGetValue(userEmail, out List<Permisson> userPermissions);
            if (!isUserPermissionsCached)
            {
                var userPermissionsFromDb = _userRepository.GetUserAsync(userEmail).Result.Permissions;
                _memoryCache.Set(userEmail, userPermissionsFromDb);
                userPermissions = userPermissionsFromDb;
            }

            var permissionNames=_claim.Value.Split(',').ToList();
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
