﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Extensions;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Entity;
using WarspearOnlineApi.Services.Base;

namespace WarspearOnlineApi.Services
{
    /// <summary>
    /// Сервис для работы со страницей дропа.
    /// </summary>
    public class DropService : BaseService
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
        public DropService(AppDbContext context,
            PlayerService playerService,
            IMapper mapper) : base(context)
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
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа.");

            var drop = await this._context.wo_Drop
                .Where(x => x.DropID == dropId)
                .ProjectTo<DropDto>(this._mapper.ConfigurationProvider)
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
            // добавить проверку пользователя
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
            // добавить проверку пользователя
            dto.Id.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа.");

            var entity = await this._context.wo_Drop
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
            var dropPlayers = await _playerService.GetCountPlayerByDropIds(dropIds);

            foreach (var dropPlayer in dropPlayers)
            {
                var drop = drops.First(x => x.Id == dropPlayer.DropId);
                drop.PlayersCount = dropPlayer.CountPlayer;
                drop.Part = drop.PlayersCount > 0 ? drop.Price / drop.PlayersCount : 0;
            }
        }

        /// <summary>
        /// Создание entity-модели.
        /// </summary>
        /// <param name="dto">Dto-модель.</param>
        /// <returns>Entity-модель.</returns>
        private async Task<wo_Drop> CreateDropEntity(DropDto dto)
        {
            var entity = new wo_Drop();
            (dto.Server?.Id).GetValueOrDefault().ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор сервера");
            entity.rf_ServerID = await this._context.wo_Server
                .Where(x => x.ServerID == dto.Server.Id)
                .Select(x => x.ServerID)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Сервер");

            return entity;
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

            entity.rf_GroupID = await this._context.wo_Group
                .Where(x => x.GroupID == dto.Group.Id)
                .Select(x => x.GroupID)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Группа");

            entity.rf_ObjectID = await this._context.wo_Object
                .Where(x => x.ObjectID == dto.Object.Id)
                .Select(x => x.ObjectID)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Объект");
        }

        /// <summary>
        /// Валидация.
        /// </summary>
        /// <param name="dto">Dto-модель.</param>
        private async Task ValidateSaving(wo_Drop entity, DropDto dto)
        {
            (dto.Object?.Id).GetValueOrDefault().ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор объекта");
            (dto.Group?.Id).GetValueOrDefault().ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор группы");
            (dto.Group?.Server.Id).GetValueOrDefault()
                .ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор сервера")
                .ThrowOnCondition(x => x != entity.rf_ServerID, "Попытка изменить сервер у дропа.");

            var serverId = await this._context.wo_Group
                .Where(x => x.GroupID == dto.Group.Id)
                .Select(x => x.rf_Server.ServerID)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Группа")
                .ThrowOnConditionAsync(x => x != dto.Group.Server.Id, "Группа не относится к указанному серверу");
        }
    }
}