namespace WarspearOnlineApi.Api.Extensions
{
    /// <summary>
    /// Обработка исключений.
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Делегат следующего обработчика.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="next">Делегат следующего обработчика.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Обработка исключений.
        /// </summary>
        /// <param name="context">Контекст.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обработка исключений.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="exception">Исключение.</param>
        /// <returns>Таск.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Или другой код ошибки, если нужно

            var response = new
            {
                message = exception.Message // Вывод только сообщения
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
