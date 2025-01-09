using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services
{
    /// <summary>
    /// Сервис для получения общих данных.
    /// </summary>
    public class GenericService : UserBaseService
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="jwtTokenService">Сервис для работы с JWT токенами.</param>
        /// <param name="mapper">Маппер.</param>
        public GenericService(
            AppDbContext context,
            JwtTokenService jwtTokenService,
            IMapper mapper)
            : base(context, jwtTokenService)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение информации о сервере.
        /// </summary>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <returns>Сервер.</returns>
        public async Task<CodeNameBaseModel> GetServer(int serverId)
        {
            serverId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор сервера");
            return await this._context.wo_Server
                .Where(x => x.ServerID == serverId)
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Сервер");
        }

        /// <summary>
        /// Получение списка серверов.
        /// </summary>
        /// <param name="search">Поиск.</param>
        /// <returns>Список серверов.</returns>
        public async Task<CodeNameBaseModel[]> GetServerList(string search)
        {
            return await this._context.wo_Group
                .Select(x => x.rf_Server)
                .Where(x => x.ServerID > 0)
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
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
            return await this._context.wo_Fraction
                .Where(x => x.FractionID == fractionId)
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
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
            return await this._context.wo_Group
                .Select(x => x.rf_Fraction)
                .Where(x => x.FractionID > 0)
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
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
            return await this._context.wo_Class
                .Where(x => x.ClassID == classId)
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Класс");
        }

        /// <summary>
        /// Получение списка классов.
        /// </summary>
        /// <param name="search">Строка поиска.</param>
        /// <param name="fractionCode">Код фракции.</param>
        /// <returns>Список классов.</returns>
        public async Task<CodeNameBaseModel[]> GetClassList(string search, FractionType fractionCode)
        {
            return await this._context.wo_Class
                .Where(x => x.ClassID > 0 && x.rf_Fraction.FractionCode == fractionCode.ToString())
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
                .FilterByNameContains(search)
                .SortByName()
                .ToArrayAsync();
        }
    }
}
