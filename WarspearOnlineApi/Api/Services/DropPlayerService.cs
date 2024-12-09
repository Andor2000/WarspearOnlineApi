﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Entity.Intersections;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services
{
    /// <summary>
    /// Сервис для работы с интерсекцией дропа и игрока.
    /// </summary>
    public class DropPlayerService : BaseService
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
        public DropPlayerService(
            AppDbContext context,
            PlayerService playerService,
            IMapper mapper) : base(context)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Список игроков.</returns>
        public async Task<DropPlayerDto[]> GetPlayerByDropId(int dropId)
        {
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа");
            return await GetPlayersByPredicate(x => x.rf_DropID == dropId);
        }

        /// <summary>
        /// Получить связь игрока с дропом по идентификатору.
        /// </summary>
        /// <param name="dropPlayerId">Идентификатор связи.</param>
        /// <returns>Связь игрока с дропом.</returns>
        public async Task<DropPlayerDto> GetDropPlayerById(int dropPlayerId)
        {
            dropPlayerId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор связи игрока и дропа");
            return (await GetPlayersByPredicate(x => x.DropPlayerID == dropPlayerId))
                .FirstOrDefault()
                .ThrowIfNull("Связь игрока с дропом");
        }

        /// <summary>
        /// Получение количества игроков претендующих на долю с дропа.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Количетсво игроков.</returns>
        public async Task<int> GetCountPlayerByDropId(int dropId)
        {
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа.");
            return await _context.wo_DropPlayer.CountAsync(x => x.rf_DropID == dropId);
        }

        /// <summary>
        /// Получение количества игроков претендующих на долю с дропа.
        /// </summary>
        /// <param name="dropIds">Список идентификаторов дропа.</param>
        /// <returns>Картеж количетсва игроков.</returns>
        public async Task<IEnumerable<(int DropId, int CountPlayer)>> GetCountPlayerByDropIds(IEnumerable<int> dropIds)
        {
            if (dropIds.IsNullOrDefault())
            {
                return Array.Empty<(int DropId, int CountPlayer)>();
            }

            return (await _context.wo_DropPlayer
                .Where(x => dropIds.Contains(x.rf_DropID))
                .GroupBy(x => x.rf_DropID)
                .Select(x => new { DropId = x.Key, CountPlayer = x.Count() })
                .ToArrayAsync())
                .Select(x => (x.DropId, x.CountPlayer));
        }

        /// <summary>
        /// Добавить игрока в список дропа.
        /// </summary>
        /// <param name="dto">Игрок.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель игрока.</returns>
        public async Task<DropPlayerDto> Add(DropPlayerDto dto, int dropId)
        {
            var entity = await MapToEntity(dto, dropId)
                .ThrowOnConditionAsync(x => x.DropPlayerID > 0, "Игрок уже находится в списоке");

            await _context.wo_DropPlayer.AddAsync(entity);
            await _context.SaveChangesAsync();

            return (await GetPlayersByPredicate(x => x.DropPlayerID == entity.DropPlayerID))
                .FirstOrDefault()
                .ThrowIfNull("Связь игрока с дропом");
        }

        /// <summary>
        /// Добавить игрока в список дропа.
        /// </summary>
        /// <param name="dto">Игрок.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель игрока.</returns>
        public async Task<DropPlayerDto> Update(DropPlayerDto dto, int dropId)
        {
            dto.Id.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор связи игрока и дропа");
            var entity = await MapToEntity(dto, dropId);

            _context.wo_DropPlayer.Update(entity);
            await _context.SaveChangesAsync();

            return await GetDropPlayerById(entity.DropPlayerID);
        }

        /// <summary>
        /// Удалить игрока из списка дропа.
        /// </summary>
        /// <param name="dropPlayerId">Идентификатор связи игрока и дропа.</param>
        /// <returns>Строка.</returns>
        public async Task<string> Delete(int dropPlayerId)
        {
            dropPlayerId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор связи игрока и дропа");
            var entityId = await _context.wo_DropPlayer
                .Where(x => x.DropPlayerID == dropPlayerId)
                .Select(x => x.DropPlayerID)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Связь игрока с дропом");

            _context.wo_DropPlayer.Remove(new wo_DropPlayer { DropPlayerID = entityId });
            await _context.SaveChangesAsync();
            return "Связь игрока с дропом удалена.";
        }

        /// <summary>
        /// Получить список игроков по предикату.
        /// </summary>
        /// <param name="predicate">Предикат.</param>
        /// <returns>Список игроков.</returns>
        private async Task<DropPlayerDto[]> GetPlayersByPredicate(Expression<Func<wo_DropPlayer, bool>> predicate)
        {
            return await _context.wo_DropPlayer
                .Where(x => x.DropPlayerID > 0 && x.rf_PlayerID > 0 && x.rf_DropID > 0)
                .Where(predicate)
                .ProjectTo<DropPlayerDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        /// <summary>
        /// Маппинг dto модели в entity.
        /// </summary>
        /// <param name="dto">Dto-модель.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Entity-модель.</returns>
        private async Task<wo_DropPlayer> MapToEntity(DropPlayerDto dto, int dropId)
        {
            dto.ThrowOnCondition(x => x.Part.IsNullOrDefault(), "Не указана доля игрока.");
            dropId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор дропа.");

            var drop = await GetDrop(dropId);
            var playerId = await GetOrCreatePlayerId(dto.Player, drop.ServerID, drop.FractionID);
            var entity = await GetOrCreateDropPlayerAsync(dto.Id, drop.DropID, playerId);

            entity.Part = dto.Part;
            entity.IsPaid = dto.IsPaid;

            return entity;
        }

        /// <summary>
        /// Получить дроп по идентификатору.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель дропа.</returns>
        private async Task<(int DropID, int ServerID, int FractionID)> GetDrop(int dropId)
        {
            var drop = await _context.wo_Drop
                .Where(x => x.DropID == dropId)
                .Select(x => new { x.DropID, x.rf_ServerID, x.rf_FractionID })
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x?.DropID == null, "Дроп")
                .ThrowOnConditionAsync(x => (x?.rf_ServerID).IsNullOrDefault(), "У дропа не указан сервер")
                .ThrowOnConditionAsync(x => (x?.rf_FractionID).IsNullOrDefault(), "У дропа не указана фракция");

            return (drop.DropID, drop.rf_ServerID, drop.rf_FractionID);
        }

        /// <summary>
        /// Получить идентификатор игрока или создать нового, если его нет.
        /// </summary>
        /// <param name="playerDto">Информация о игроке.</param>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <param name="fractionId">Идентификатор фракции.</param>
        /// <returns>Идентификатор игрока.</returns>
        private async Task<int> GetOrCreatePlayerId(PlayerDto playerDto, int serverId, int fractionId)
        {
            if ((playerDto?.Id).IsNullOrDefault())
            {
                return await _playerService.CreatePlayer(playerDto, serverId, fractionId);
            }

            var player = await _context.wo_Player
                .Where(x => x.PlayerID == playerDto.Id)
                .Select(x => new { x.PlayerID, x.rf_ServerID, x.rf_FractionID })
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync($"Игрок с идентификатором: {playerDto.Id}")
                .ThrowOnConditionAsync(x => x.rf_ServerID != serverId, "Игрок не относится к данному серверу")
                .ThrowOnConditionAsync(x => x.rf_FractionID != fractionId, "Игрок не относится к данной фракции");

            return player.PlayerID;
        }

        /// <summary>
        /// Получить или создать запись о дропе для игрока.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <param name="playerId">Идентификатор игрока.</param>
        /// <returns>Запись о дропе для игрока.</returns>
        private async Task<wo_DropPlayer> GetOrCreateDropPlayerAsync(int dropPlayerId, int dropId, int playerId)
        {
            if (!dropPlayerId.IsNullOrDefault())
            {
                return await _context.wo_DropPlayer.FirstOrDefaultAsync(x => x.DropPlayerID == dropPlayerId)
                    .ThrowOnConditionAsync(x => (x?.DropPlayerID).IsNullOrDefault(), "Не найдена запись о связи игрока и дропа");
            }

            return await _context.wo_DropPlayer.FirstOrDefaultAsync(x => x.rf_DropID == dropId && x.rf_PlayerID == playerId)
                ?? new wo_DropPlayer
                {
                    rf_DropID = dropId,
                    rf_PlayerID = playerId,
                };
        }
    }
}
