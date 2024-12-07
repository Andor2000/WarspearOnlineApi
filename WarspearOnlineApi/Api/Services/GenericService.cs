using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services
{
    /// <summary>
    /// Сервис для получения общих данных.
    /// </summary>
    public class GenericService : BaseService
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
        public GenericService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Получение информации о сервере.
        /// </summary>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <returns>Сервер.</returns>
        public async Task<ServerDto> GetServer(int serverId)
        {
            serverId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор сервера");
            return await _context.wo_Server
                .Where(x => x.ServerID == serverId)
                .ProjectTo<ServerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Сервер");
        }

        /// <summary>
        /// Получение списка серверов.
        /// </summary>
        /// <param name="search">Поиск.</param>
        /// <returns>Список серверов.</returns>
        public async Task<ServerDto[]> GetServerList(string search)
        {
            return await _context.wo_Server
                .Where(x => x.ServerID > 0)
                .ProjectTo<ServerDto>(_mapper.ConfigurationProvider)
                .FilterByNameContains(search)
                .SortByName()
                .ToArrayAsync();
        }

        /// <summary>
        /// Получение информации о фракции.
        /// </summary>
        /// <param name="fractionId">Идентификатор фракции.</param>
        /// <returns>Фракция.</returns>
        public async Task<CodeNameBaseModel> GetFraction(int fractionId)
        {
            fractionId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор фракции");
            return await _context.wo_Fraction
                .Where(x => x.FractionID == fractionId)
                .ProjectTo<CodeNameBaseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Фракция");
        }

        /// <summary>
        /// Получение списка фракций.
        /// </summary>
        /// <param name="search">Поиск.</param>
        /// <returns>Список фракций.</returns>
        public async Task<CodeNameBaseModel[]> GetFractionList(string search)
        {
            return await _context.wo_Fraction
                .Where(x => x.FractionID > 0)
                .ProjectTo<CodeNameBaseModel>(_mapper.ConfigurationProvider)
                .FilterByNameContains(search)
                .SortByName()
                .ToArrayAsync();
        }

        /// <summary>
        /// Получение информации о классе.
        /// </summary>
        /// <param name="classId">Идентификатор класса.</param>
        /// <returns>Класс.</returns>
        public async Task<CodeNameBaseModel> GetClass(int classId)
        {
            classId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор класса");
            return await _context.wo_Class
                .Where(x => x.ClassID == classId)
                .ProjectTo<CodeNameBaseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Класс");
        }

        /// <summary>
        /// Получение списка классов.
        /// </summary>
        /// <param name="search">Строка поиска.</param>
        /// <returns>Список классов.</returns>
        public async Task<CodeNameBaseModel[]> GetClassList(string search)
        {
            return await _context.wo_Class
                .Where(x => x.ClassID > 0)
                .ProjectTo<CodeNameBaseModel>(_mapper.ConfigurationProvider)
                .FilterByNameContains(search)
                .SortByName()
                .ToArrayAsync();
        }
    }
}
