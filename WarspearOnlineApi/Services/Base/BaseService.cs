using WarspearOnlineApi.Data;

namespace WarspearOnlineApi.Services.Base
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
            _context = context;
        }
    }
}
