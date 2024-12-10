using System.Text.RegularExpressions;
using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Dto.Users;

namespace WarspearOnlineApi.Api.Extensions
{
    /// <summary>
    /// Валидация данных.
    /// </summary>
    public static class Validaties
    {
        /// <summary>
        /// Валидация данных пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="isCheckPassword">Признак валидации только логина.</param>
        public static void ValidateUserAuthorize(this AuthorizeDto user, bool isCheckPassword = false)
        {
            user.Login.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан логин пользователя")
                .Trim()
                .ThrowOnCondition(x => x.Contains(" "), "Логин пользователя не должен содержать пробелы")
                .ThrowOnCondition(x => x.Length < 5, "Логин пользователя должен содержать не менее 5 символов")
                .ThrowOnCondition(x => x.Length > SizeFieldEnum.UserLogin, $"Логин пользователя должен содержать не более {SizeFieldEnum.UserLogin} символов");

            if (isCheckPassword)
            {
                user.Password.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан пароль пользователя")
                    .ThrowOnCondition(x => x.Length < 9, "Пароль пользователя должен содержать не менее 9 символов")
                    .ThrowOnCondition(x => x.Length > SizeFieldEnum.UserPassword, $"Пароль пользователя должен содержать не более {SizeFieldEnum.UserPassword} символов");
            }
        }

        /// <summary>
        /// Валидация данных игрока.
        /// </summary>
        /// <param name="player">Игрок.</param>
        public static void ValidatePlayer(this PlayerDto player)
        {
            player.ThrowOnCondition(x => x.Nick.IsNullOrDefault(), "Не указан ник игрока")
            .ThrowOnCondition(x => x.Nick.Length < 3 || x.Nick.Length > 10, "Неверный размер ника игрока")
            .ThrowOnCondition(x => !Regex.IsMatch(x.Nick, @"^[a-zA-Z]+$"), "Ник должен содержать только английские буквы")
            .ThrowOnCondition(x => (x?.Class?.Id).IsNullOrDefault(), "Не указан класс игрока");
        }
    }
}
