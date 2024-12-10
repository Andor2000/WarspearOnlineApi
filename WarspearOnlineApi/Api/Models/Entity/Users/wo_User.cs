﻿using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Enums;

namespace WarspearOnlineApi.Api.Models.Entity.Users
{
    /// <summary>
    /// Entity-модель пользователя.
    /// </summary>
    public class wo_User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [MaxLength(SizeFieldEnum.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [MaxLength(SizeFieldEnum.UserLogin)]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [MaxLength(SizeFieldEnum.UserPassword)]
        public string Password { get; set; }

        /// <summary>
        /// Ранг уровня доступа.
        /// </summary>
        public int RangeAccessLevel { get; set; }

        /// <summary>
        /// Идентификатор уровня доступа.
        /// </summary>
        public int rf_AccessLevelID { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public wo_AccessLevel rf_AccessLevel { get; set; }
    }
}
