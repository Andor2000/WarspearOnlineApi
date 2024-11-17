using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Extensions;
using WarspearOnlineApi.Services.Base;

namespace WarspearOnlineApi.Services
{
    /// <summary>
    /// Сервис для работы с игроками.
    /// </summary>
    public class PlayerService : BaseService
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
        public PlayerService(AppDbContext context, IMapper mapper) : base(context)
        { 
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение количества игроков претендующих на долю с дропа.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Количетсво игроков.</returns>
        public async Task<int> GetCountPlayerByDropId(int dropId)
        {
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа.");
            return await this._context.wo_DropPlayer.CountAsync(x => x.rf_DropID == dropId);
        }


        /// <summary>
        /// Получение количества игроков претендующих на долю с дропа.
        /// </summary>
        /// <param name="dropIds">Список идентификаторов дропа.</param>
        /// <returns>Картеж количетсва игроков.</returns>
        public async Task<(int DropId, int CountPlayer)[]> GetCountPlayerByDropIds(IEnumerable<int> dropIds)
        {
            if (dropIds.IsNullOrDefault())
            {
                return Array.Empty<(int DropId, int CountPlayer)>();
            }

            var result = await this._context.wo_DropPlayer
                .Where(x => dropIds.Contains(x.rf_DropID))
                .GroupBy(x => x.rf_DropID)
                .Select(x => new { DropId = x.Key, CountPlayer = x.Count() })
                .ToArrayAsync();

            return result
                .Select(x => (x.DropId, x.CountPlayer))
                .ToArray();
        }
    }
}
