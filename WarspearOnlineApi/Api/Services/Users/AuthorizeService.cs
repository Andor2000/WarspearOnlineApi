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
            dto.Login = dto.Login.ValidateUserLogin();

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
        public async Task<UserSessionDto> SignIn(AuthorizeDto dto)
        {
            dto.Login = dto.Login.ValidateUserLogin();
            dto.Password = dto.Password.ValidateUserPassword();
            return await this.GetUserByLoginAndPassword(dto.Login, dto.Password);
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="dto">Модель регистрации.</param>
        /// <returns>Модель зарегистрированного пользователя.</returns>
        public async Task<UserSessionDto> Registration(AuthorizeDto dto)
        {
            dto.Login = dto.Login.ValidateUserLogin();
            dto.Password = dto.Password.ValidateUserPassword();
            dto.Name = dto.Name.ValidateUserName();

            var userDb = await this._context.wo_User.FirstOrDefaultAsync(x => x.Login == dto.Login)
                .ThrowIfNullAsync("Пользователь")
                .ThrowOnConditionAsync(x => x.Password.IsNullOrDefault(), "Пользователь уже зарегистрирован");

            userDb.UserName = dto.Name;
            userDb.Password = dto.Password;
            await this._context.SaveChangesAsync();

            return await this.GetUserByLoginAndPassword(dto.Login, dto.Password);
        }

        /// <summary>
        /// Получить пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Пользователь.</returns>
        private async Task<UserSessionDto> GetUserByLoginAndPassword(string login, string password)
        {
            var user = await this._context.wo_User
                .Where(x => x.Login == login && x.Password == password)
                .ProjectTo<UserSessionDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNullAsync("Пользователь");

            this._jwtTokenService.GenerateToken(user);
            user.Roles = await this._roleService.GetRoleCodes(user.Token);

            return user;
        }
    }
}
