using WarspearOnlineApi.Api.Data;

namespace WarspearOnlineApi.Api.Services.Base
{
    /// <summary>
    /// Базовый сервис.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Контекст данных.
        /// </summary>
        protected readonly AppDbContext _context;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        protected BaseService(AppDbContext context)
        {
            this._context = context;
        }
    }
}
