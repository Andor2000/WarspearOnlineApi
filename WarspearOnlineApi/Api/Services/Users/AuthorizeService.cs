using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services.Users
{
    /// <summary>
    /// Сервис для работы с авторизацией пользователей.
    /// </summary>
    public class AuthorizeService : BaseService
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Сервис для работы с токенами.
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Сервис для работы с ролями.
        /// </summary>
        private readonly RoleService _roleService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="jwtTokenService">Сервис для работы с токенами.</param>
        public AuthorizeService(
            AppDbContext context,
            IMapper mapper,
            JwtTokenService jwtTokenService,
            RoleService roleService) : base(context)
        {
            this._mapper = mapper;
            this._jwtTokenService = jwtTokenService;
            this._roleService = roleService;
        }

        /// <summary>
        /// Проверка существования логина.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Признак существования логина.</returns>
        public async Task<bool> CheckExistLoginAndFilledPassword(AuthorizeDto dto)
        {
            dto.ValidateUserAuthorize();

            var user = await this._context.wo_User
                .Where(x => x.UserId > 0)
                .Where(x => x.Login == dto.Login)
                .ProjectTo<AuthorizeDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.Login).IsNullOrDefault(), "Пользователь");

            return !user.Password.IsNullOrDefault();
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Модель авотризированного пользователя.</returns>
        public async Task<SuccessAuthorizeDto> SignIn(AuthorizeDto dto)
        {
            dto.ValidateUserAuthorize(true);

            var user = await this._context.wo_User
                .Where(x => x.Login == dto.Login && x.Password == dto.Password)
                .ProjectTo<SuccessAuthorizeDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Пользователь");

            this._jwtTokenService.GenerateToken(user);
            user.User.Roles = await this._roleService.GetRoleCodes(user.Token);

            return user;
        }

        public async Task<SuccessAuthorizeDto> Registration(AuthorizeDto dto)
        {
            dto.ValidateUserAuthorize(true);

            var user = await this._context.wo_User
                .Where(x => x.Login == dto.Login && x.Password == dto.Password)
                .ProjectTo<SuccessAuthorizeDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowOnConditionAsync(x => x == null, "Указан неверный логин или пароль");
        }
    }
}
