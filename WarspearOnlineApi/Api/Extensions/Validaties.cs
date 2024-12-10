using System.Runtime.CompilerServices;
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
        /// Валидация логина пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        public static string ValidateUserLogin(this string login)
        {
            return login.Trim()
                .ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан логин")
                .ThrowOnCondition(x => x.Contains(" "), "Логин не должен содержать пробелы")
                .ThrowOnCondition(x => x.Length < 5, "Логин должен содержать не менее 5 символов")
                .ThrowOnCondition(x => x.Length > SizeFieldEnum.UserLogin, $"Логин должен содержать не более {SizeFieldEnum.UserLogin} символов");
        }

        /// <summary>
        /// Валидация пароля пользователя.
        /// </summary>
        /// <param name="password">Пароль.</param>
        public static string ValidateUserPassword(this string password)
        {
            return password.Trim()
                .ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан пароль")
                .ThrowOnCondition(x => x.Length < 9, "Пароль должен содержать не менее 9 символов")
                .ThrowOnCondition(x => x.Length > SizeFieldEnum.UserPassword, $"Пароль должен содержать не более {SizeFieldEnum.UserPassword} символов");
        }

        /// <summary>
        /// Валидация имени пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public static string ValidateUserName(this string name)
        {
            return name.Trim()
                .ThrowOnCondition(x => x.Length < 3, "Имя пользователя должен содержать не менее 3 символов")
                .ThrowOnCondition(x => x.Length > SizeFieldEnum.UserName, $"Имя пользователя должен содержать не более {SizeFieldEnum.UserName} символов");

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
