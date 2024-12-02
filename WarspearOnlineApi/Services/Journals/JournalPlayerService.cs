using AutoMapper;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Services.Base;

namespace WarspearOnlineApi.Services.Journals
{
    /// <summary>
    /// Сервис для работы с журналом игроков.
    /// </summary>
    public class JournalPlayerService : BaseService
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="mapper">Маппер.</param>
        public JournalPlayerService(AppDbContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

    }
}
