using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services
{
    /// <summary>
    /// Сервис для работы со страницей дропа.
    /// </summary>
    public class DropService : UserBaseService
    {
        /// <summary>
        /// Сервис для работы с игроками.
        /// </summary>
        private readonly PlayerService _playerService;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="playerService">Сервис для работы с игроками.</param>
        /// <param name="mapper">Маппер.</param>
        public DropService(AppDbContext context,
            PlayerService playerService,
            JwtTokenService jwtTokenService,
            IMapper mapper) : base(context, jwtTokenService)
        {
            this._playerService = playerService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение информации о дропе.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Дроп.</returns>
        public async Task<DropDto> GetDrop(int dropId)
        {
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа");
            var drop = await this._context.wo_Drop
                .Where(x => x.DropID == dropId)
                .ProjectTo<DropDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Дроп");

            await this.SetPlayerCountAndPart(drop);
            return drop;
        }

        /// <summary>
        /// Создание новой записи дропа.
        /// </summary>
        /// <param name="dto">Dto-модель дропа.</param>
        /// <returns>Дроп.</returns>
        public async Task<DropDto> AddDrop(DropDto dto)
        {
            var entity = await this.CreateDropEntity(dto);
            await this.MapToEntity(entity, dto);

            this._context.wo_Drop.Add(entity);
            await this._context.SaveChangesAsync();

            return await this.GetDrop(entity.DropID);
        }

        /// <summary>
        /// Редактирование дропа.
        /// </summary>
        /// <param name="dto">Dto-модель дропа.</param>
        /// <returns>Дроп.</returns>
        public async Task<DropDto> EditDrop(DropDto dto)
        {
            dto.Id.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа");

            var entity = await this._context.wo_Drop
                .Include(x => x.rf_Group)
                .FirstOrDefaultAsync(x => x.DropID == dto.Id)
                .ThrowIfNullAsync("Дроп");

            await this.MapToEntity(entity, dto);
            await this._context.SaveChangesAsync();

            return await this.GetDrop(entity.DropID);
        }

        /// <summary>
        /// Проставление количетсва игроков и размер доли.
        /// </summary>
        /// <param name="drops">Список дропа.</param>
        public async Task SetPlayerCountAndPart(params DropDto[] drops)
        {
            if (drops.IsNullOrDefault())
            {
                return;
            }

            var dropIds = drops.Select(x => x.Id).Where(x => x > 0);
            var dropPlayers = await this._playerService.GetCountPlayerByDropIds(dropIds);

            foreach (var dropPlayer in dropPlayers)
            {
                var drop = drops.First(x => x.Id == dropPlayer.DropId);
                drop.PlayersCount = dropPlayer.CountPlayer;
                drop.Part = drop.PlayersCount > 0
                    ? drop.Price / drop.PlayersCount
                    : drop.Price;
            }
        }

        /// <summary>
        /// Удаление дропа по идентификатору.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Строка.</returns>
        public async Task<string> Delete(int dropId)
        {
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа");
            var entity = await this._context.wo_Drop
                .Where(x => x.DropID == dropId)
                .Select(x =>new
                {
                    x.DropID,
                    x.rf_GroupID
                })
                .FirstOrDefaultAsync()
                .ThrowOnConditionAsync(x => (x?.DropID).IsNullOrDefault(), "Дроп");
            await this.CheckUserHasGroupAsync(entity.rf_GroupID);

            this._context.wo_Drop.Remove(new wo_Drop { DropID = entity.DropID });
            await this._context.SaveChangesAsync();
            return "Дроп удален";
        }

        /// <summary>
        /// Создание entity-модели.
        /// </summary>
        /// <param name="dto">Dto-модель.</param>
        /// <returns>Entity-модель.</returns>
        private async Task<wo_Drop> CreateDropEntity(DropDto dto)
        {
            dto.Group?.Id.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор группы");
            await this.CheckUserHasGroupAsync(dto.Group.Id);

            var group = await this._context.wo_Group.FirstOrDefaultAsync(x => x.GroupID == dto.Group.Id)
                .ThrowOnConditionAsync(x => (x?.GroupID).IsNullOrDefault(), "Группа");

            return new wo_Drop() { rf_Group = group };
        }

        /// <summary>
        /// Маппинг dto модели в entity.
        /// </summary>
        /// <param name="entity">Entity-модель.</param>
        /// <param name="dto">Dto-модель</param>
        private async Task MapToEntity(wo_Drop entity, DropDto dto)
        {
            await this.ValidateSaving(entity, dto);

            entity.Drop_Date = dto.Date;
            entity.Price = dto.Price;

            entity.rf_ObjectID = await this._context.wo_Object
                .Where(x => x.ObjectID == dto.Object.Id)
                .Select(x => x.ObjectID)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Объект");
        }

        /// <summary>
        /// Валидация.
        /// </summary>
        /// <param name="entity">Entity-модель.</param>
        /// <param name="dto">Dto-модель.</param>
        private async Task ValidateSaving(wo_Drop entity, DropDto dto)
        {
            (dto.Object?.Id).ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор объекта");

            dto.Group.ThrowOnCondition(x => x.Id.IsNullOrDefault(), "Не указан идентификатор группы")
                .Server.ThrowOnCondition(x => (x?.Id).IsNullOrDefault(), "Не указан идентификатор сервера")
                .Id.ThrowOnCondition(x => x != entity.rf_Group.rf_ServerID, "Попытка изменить сервер у дропа");

            await this._context.wo_Group.Where(x => x.GroupID == dto.Group.Id).Select(x => x.rf_ServerID).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Группа")
                .ThrowOnConditionAsync(x => x != dto.Group.Server.Id, "Группа не относится к указанному серверу");
        }
    }
}