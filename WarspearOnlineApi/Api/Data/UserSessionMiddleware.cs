using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Data
{
    /// <summary>
    /// Добавление пользователя в контекст.
    /// </summary>
    public class UserSessionMiddleware
    {
        /// <summary>
        /// Построитель запросов.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Сервис работы с токенами.
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="next">Построитель запросов.</param>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="jwtTokenService">Сервис работы с токенами.</param>
        public UserSessionMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory, JwtTokenService jwtTokenService)
        {
            this._next = next;
            this._scopeFactory = scopeFactory;
            this._jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Обработка запроса.
        /// </summary>
        /// <param name="httpContext">Контекст запроса.</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var userId = this._jwtTokenService.GetUserIdFromToken();

            using (var scope = this._scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var command = $"EXEC sp_set_session_context @key = 'UserID', @value = {userId}";
                await dbContext.Database.ExecuteSqlRawAsync(command);
            }

            await this._next(httpContext);
        }
    }
}
