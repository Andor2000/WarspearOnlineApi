using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Services.Users;
using WarspearOnlineApi.Api.Extensions;

namespace WarspearOnlineApi.Api.Controllers.Attributies
{
    public class RoleAuthorizeAttribute : Attribute
    {
        /// <summary>
        /// Роль.
        /// </summary>
        private readonly string _requiredRole;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="requiredRole">Роль.</param>
        public RoleAuthorizeAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        /// <summary>
        /// Проверка роли.
        /// </summary>
        /// <param name="context">Контекст.</param>
        public async Task OnAuthorization(AuthorizationFilterContext context)
        {
            var roleService = context.HttpContext.RequestServices.GetService<RoleService>();
            var jwtTokenService = context.HttpContext.RequestServices.GetService<JwtTokenService>();

            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var username = jwtTokenService.GetUsernameFromToken()
                .ThrowNotFound(x => x.IsNullOrDefault(), "Пользователь");

            var roles = await roleService.GetRoleCodes(username);
            var roleCodes = roles.Select(role => role.Code);

            if (!roleCodes.Contains(_requiredRole))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}