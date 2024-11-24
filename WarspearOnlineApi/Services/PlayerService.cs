using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Text.RegularExpressions;
using System.Threading.RateLimiting;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Extensions;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Entity;
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

            return result.Select(x => (x.DropId, x.CountPlayer)).ToArray();
        }

        /// <summary>
        /// Получить список игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Список игроков.</returns>
        public async Task<IEnumerable<PlayerDto>> GetPlayerByDropId(int dropId)
        {
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа.");

            return await this._context.wo_DropPlayer
                .Where(x => x.rf_DropID == dropId)
                .ProjectTo<PlayerDto>(this._mapper.ConfigurationProvider)
                .ToArrayAsync();
        }


        /// <summary>
        ///  Добавить игрока в список дропа.
        /// </summary>
        /// <param name="dto">Игрок.</param>
        /// <returns>Модель игрока.</returns>
        public async Task<PlayerDto> AddPlayerInDrop(PlayerDto dto, int dropId)
        {
            var drop = await this._context.wo_Drop
                .Where(x => x.DropID == dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа."))
                .Select(x => new
                {
                    x.DropID,
                    x.rf_ServerID,
                    x.rf_Group.rf_FractionID,
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.DropID).IsNullOrDefault(), "Дроп")
                .ThrowOnConditionAsync(x => x.rf_ServerID.IsNullOrDefault(), "У дропа не указан сервер")
                .ThrowOnConditionAsync(x => x.rf_FractionID.IsNullOrDefault(), "У дропа не указана фракция (или группа)"); 
            
            var player = dto.Id.IsNullOrDefault()
                ? await this.CreatePlayer(dto, drop!.rf_ServerID, drop!.rf_FractionID)
                : await this._context.wo_Player
                    .FirstOrDefaultAsync(x => x.PlayerID == dto.Id)
                    .ThrowIfNullAsync($"Игрок с идентификатором: {dto.Id}")
                    .ThrowOnConditionAsync(x => x.rf_ServerID != drop!.rf_ServerID, "Указаный игрок относится к другому серверу")
                    .ThrowOnConditionAsync(x => x.rf_FractionID != drop!.rf_FractionID, "Указанный игрок находится в противоположной фракции");



            return null;
        }

        /// <summary>
        /// Создать модель игрока.
        /// </summary>
        /// <param name="dto">Dto-модель игрока.</param>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <param name="fractionId">Идентификатор фракция.</param>
        /// <returns>Идентификатор игрока.</returns>
        private async Task<wo_Player> CreatePlayer(PlayerDto dto, int serverId, int fractionId)
        {
            dto.Nick = dto.Nick?.Trim() ?? string.Empty;
            dto.ThrowOnCondition(x => x.Nick.IsNullOrDefault(), "Не указан ник игрока")
                .ThrowOnCondition(x => x.Nick.Length < 3 || x.Nick.Length > 10, "Неверный размер ника игрока")
                .ThrowOnCondition(x => !Regex.IsMatch(x.Nick, @"^[a-zA-Z]+$"), "Ник должен содержать только английские буквы")
                .ThrowOnCondition(x => (x?.Class?.Id).IsNullOrDefault(), "Не указан класс игрока");

            var entity = new wo_Player
            {
                Nick = dto.Nick,
                rf_ClassID = dto.Class.Id,
                rf_FractionID = fractionId,
                rf_ServerID = serverId,
            };

            this._context.wo_Player.Add(entity);
            await this._context.SaveChangesAsync();

            return entity;
        }
    }
}
