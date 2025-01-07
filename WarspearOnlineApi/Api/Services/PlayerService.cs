using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services
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

            return result.Select(x => (x.DropId, x.CountPlayer)).ToArray();
        }

        /// <summary>
        /// Создать модель игрока.
        /// </summary>
        /// <param name="dto">Dto-модель игрока.</param>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <param name="fractionId">Идентификатор фракции.</param>
        /// <returns>Идентификатор игрока.</returns>
        public async Task<int> CreatePlayer(PlayerDto dto, int serverId, int fractionId)
        {
            dto.ValidatePlayer();
            serverId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан сервер");

            var playerId = await this._context.wo_Player
                .Where(x => x.Nick == dto.Nick && x.rf_ServerID == serverId)
                .Select(x => x.PlayerID)
                .FirstOrDefaultAsync();

            if (playerId.IsNullOrDefault())
            {
                var entity = new wo_Player
                {
                    Nick = dto.Nick,
                    rf_ClassID = dto.Class.Id,
                    rf_FractionID = fractionId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указана фракция"),
                    rf_ServerID = serverId,
                };

                this._context.wo_Player.Add(entity);
                await this._context.SaveChangesAsync();
                playerId = entity.PlayerID;
            }

            return playerId;
        }
    }
}
