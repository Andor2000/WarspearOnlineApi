using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services.Users
{
    /// <summary>
    /// Сервис для работы с авторизацией пользователей.
    /// </summary>
    public class UserAuthorizeService : BaseService
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
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="jwtTokenService">Сервис для работы с токенами.</param>
        public UserAuthorizeService(
            AppDbContext context,
            IMapper mapper,
            JwtTokenService jwtTokenService) : base(context)
        {
            this._mapper = mapper;
            this._jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Проверка существования логина.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Признак существования логина.</returns>
        public async Task<bool> CheckExistLoginAndFilledPassword(UserAuthorizeDto dto)
        {
            this.ValidateUserData(dto, true);

            var user = await this._context.wo_User.Select(x => new UserAuthorizeDto
            {
                Login = x.Login,
                Password = x.Password,
            }).FirstOrDefaultAsync()
            .ThrowNotFoundAsync(x => (x?.Login).IsNullOrDefault(), "Пользователь");

            return !user.Password.IsNullOrDefault();
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Модель авотризированного пользователя.</returns>
        public async Task<UserSuccessAuthorizeDto> SignIn(UserAuthorizeDto dto)
        {
            this.ValidateUserData(dto);

            var user = await this._context.wo_User
                .Where(x => x.Login == dto.Login && x.Password == dto.Password)
                .ProjectTo<UserSuccessAuthorizeDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            _jwtTokenService....

            return null;
        }


        /// <summary>
        /// Валидация данных.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <param name="isOnlyLogin">Признак валидации только логина.</param>
        private void ValidateUserData(UserAuthorizeDto dto, bool isOnlyLogin = false)
        {
            dto.Login.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан логин пользователя")
                .Trim()
                .ThrowOnCondition(x => x.Contains(" "), "Логин пользователя не должен содержать пробелы")
                .ThrowOnCondition(x => x.Length < 6, "Логин пользователя должен содержать не менее 6 символов")
                .ThrowOnCondition(x => x.Length > 40, "Логин пользователя должен содержать не более 40 символов");

            if (isOnlyLogin)
            {
                return;
            }
        }
    }
}
