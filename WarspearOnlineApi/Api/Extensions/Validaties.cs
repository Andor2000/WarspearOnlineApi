﻿using System.Text.RegularExpressions;
using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Models.Dto;

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
            player.Nick = player.Nick.Trim();
            player.ThrowOnCondition(x => x.Nick.IsNullOrDefault(), "Не указан ник игрока")
            .ThrowOnCondition(x => x.Nick.Length < 3 || x.Nick.Length > 10, "Неверный размер ника игрока")
            .ThrowOnCondition(x => !Regex.IsMatch(x.Nick, @"^[a-zA-Z]+$"), "Ник должен содержать только английские буквы")
            .ThrowOnCondition(x => (x?.Class?.Id).IsNullOrDefault(), "Не указан класс игрока");
        }

        /// <summary>
        /// Валидания наименования группы.
        /// </summary>
        /// <param name="groupName">Наименование группы.</param>
        /// <returns>Наименование группы.</returns>
        public static string ValidateGroupName(this string groupName)
        {
            return groupName.Trim()
                .ThrowOnCondition(x => x.IsNullOrDefault(), "Не указано наименование группы")
                .ThrowOnCondition(x => x.Length < 3, "Наименование группы должно содержать не менее 3 символов")
                .ThrowOnCondition(x => x.Length > SizeFieldEnum.GroupName, $"Наименование группы должно содержать не более {SizeFieldEnum.GroupName} символов");
        }

        /// <summary>
        /// Валидания наименования гильдии.
        /// </summary>
        /// <param name="groupName">Наименование гильдии.</param>
        /// <returns>Наименование гильдии.</returns>
        public static string ValidateGuildName(this string guildName)
        {
            return guildName.Trim()
                .ThrowOnCondition(x => x.IsNullOrDefault(), "Не указано наименование гильдии")
                .ThrowOnCondition(x => x.Length < 3, "Наименованиее гильдии должно содержать не менее 3 символов")
                .ThrowOnCondition(x => x.Length > SizeFieldEnum.GroupName, $"Наименование гильдии должно содержать не более {SizeFieldEnum.GuildName} символов");
        }
    }
}
