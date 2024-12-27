using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Models.Entity.Users;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class UserService : BaseService
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
        public UserService(
            AppDbContext context,
            IMapper mapper,
            JwtTokenService jwtTokenService) : base(context)
        {
            this._mapper = mapper;
            this._jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Добавить пользователя.
        /// </summary>
        /// <param name="dto">Dto-модель для создания пользователя.</param>
        /// <returns>Сообщение о успешном добавлении пользователя.</returns>
        public async Task<UserDto> AddUser(SavingUserDto dto)
        {
            dto.AccessLevelId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор уровня доступа");
            dto.Login = dto.Login.ValidateUserLogin();

            await this._context.wo_User.AnyAsync(x => x.UserId > 0 && x.Login == dto.Login)
                .ThrowOnConditionAsync(x => x, "Пользователь с таким логином уже существует");

            var accessLevel = await this._context.wo_AccessLevel
                .Where(x => x.AccessLevelID == dto.AccessLevelId)
                .Select(x => x.AccessLevelInt)
                .FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => x.IsNullOrDefault(), "Уровень доступа");

            var userId = this._jwtTokenService.GetUserIdFromToken();
            var user = await this._context.wo_User
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.UserId,
                    x.RangeAccessLevel,
                    x.rf_ServerID,
                    x.rf_FractionID,
                    x.rf_AccessLevel.AccessLevelInt
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.UserId).IsNullOrDefault(), "Пользователь")
                .ThrowOnConditionAsync(x => x.AccessLevelInt < accessLevel, "Вы не можете дать право доступа выше собственного");

            var newUser = new wo_User()
            {
                Login = dto.Login,
                RangeAccessLevel = user.RangeAccessLevel + 1, // как-то поумному нужно делать
                rf_ServerID = user.rf_ServerID,
                rf_FractionID = user.rf_FractionID,
                rf_AccessLevelID = dto.AccessLevelId,
            };

            await this._context.wo_User.AddAsync(newUser);
            await this._context.SaveChangesAsync();

            return await GetUserById(newUser.UserId);
        }

        /// <summary>
        /// Обновление уровня доступа пользователя.
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> UpdateAccessLevel(SavingUserDto dto)
        {
            var accessLevelInt = (await this._context.wo_AccessLevel.AsNoTracking()
                .Where(x => x.AccessLevelID == dto.AccessLevelId)
                .Select(x => new { x.AccessLevelInt })
                .FirstOrDefaultAsync()
                .ThrowIfNull("Уровень доступа"))
                .AccessLevelInt;

            var userHeaderId = this._jwtTokenService.GetUserIdFromToken();
            var userHeader = await this._context.wo_User
                .FirstOrDefaultAsync(x => x.UserId == userHeaderId)
                .ThrowNotFoundAsync(x => (x?.UserId).IsNullOrDefault(), "Пользователь")
                .ThrowOnConditionAsync(x => x.rf_AccessLevel.AccessLevelInt < accessLevelInt, "Вы не можете дать право доступа выше собственного");

            dto.UserId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор пользователя");
            var user = await this._context.wo_User
                .FirstOrDefaultAsync(x => x.UserId == dto.UserId)
                .ThrowIfNull("Пользователь")
                .ThrowOnConditionAsync(x => x.rf_ServerID != userHeader.rf_ServerID, "У вас нет прав на обновление пользователя на указанный сервер")
                .ThrowOnConditionAsync(x => x.rf_FractionID != userHeader.rf_FractionID, "У вас нет прав на обновление пользователя в указанной фракции")
                .ThrowOnConditionAsync(x => x.RangeAccessLevel >= userHeader.RangeAccessLevel, "Ваш ранг меньше чем у редактируемого пользователя. У вас нет прав на обновление пользователя");

            user.rf_AccessLevelID = dto.AccessLevelId;
            user.RangeAccessLevel = userHeader.RangeAccessLevel + 1;
            await this._context.SaveChangesAsync();

            return await GetUserById(user.UserId);
        }

        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>пользователь.</returns>
        private async Task<UserDto> GetUserById(int userId)
        {
            return await this._context.wo_User
                .Where(x => x.UserId == userId)
                .ProjectTo<UserDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                .ThrowIfNull("Пользователь");
        }
    }
}
