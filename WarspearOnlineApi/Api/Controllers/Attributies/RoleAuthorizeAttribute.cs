using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Controllers.Attributies
{
    /// <summary>
    /// Атрибут проверки роли.
    /// </summary>
    public class RoleAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
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
            this._requiredRole = requiredRole;
        }

        /// <summary>
        /// Проверка роли.
        /// </summary>
        /// <param name="context">Контекст.</param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roles = await context.HttpContext.RequestServices
                .GetService<UserService>()
                .GetRoleCodes();

            var roleCodes = roles.Select(role => role.Code);

            if (!roleCodes.Contains(this._requiredRole))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}