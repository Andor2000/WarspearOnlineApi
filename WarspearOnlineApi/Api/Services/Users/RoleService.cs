using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.SqlQueries;

namespace WarspearOnlineApi.Api.Services.Users
{
    /// <summary>
    /// Сервис для работы с ролями пользователей.
    /// </summary>
    public class RoleService : BaseService
    {
        /// <summary>
        /// Сервис для работы с токенами.
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="jwtTokenService">Сервис для работы с токенами.</param>
        /// <param name="mapper">Маппер.</param>
        public RoleService(
            AppDbContext context,
            JwtTokenService jwtTokenService,
            IMapper mapper) : base(context)
        {
            this._jwtTokenService = jwtTokenService;
            this._mapper = mapper;
        }
    }
}
