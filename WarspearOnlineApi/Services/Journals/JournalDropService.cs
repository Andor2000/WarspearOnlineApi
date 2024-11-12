using WarspearOnlineApi.Data;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Filters;
using WarspearOnlineApi.Services.Base;

namespace WarspearOnlineApi.Services.Journals
{
    public class JournalDropService : BaseService
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        public JournalDropService(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить журнал дропа.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Журнал дропа.</returns>
        public async Task<IEnumerable<JournalDropDto>> GetJournalDrop(DropFilter filter)
        {


            return null;
        }
    }
}
