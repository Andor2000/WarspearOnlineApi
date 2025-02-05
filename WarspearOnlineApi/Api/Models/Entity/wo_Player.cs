﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель игрока.
    /// </summary>
    [Index(nameof(Nick), IsUnique = false)] // Создаём индекс на поле Nick
    public class wo_Player
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Ник игрока.
        /// </summary>
        [MaxLength(20)]
        public string Nick { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int rf_ServerID { get; set; }

        /// <summary>
        /// Сервер.
        /// </summary>
        public wo_Server rf_Server { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }

        /// <summary>
        /// Идентификатор класса.
        /// </summary>
        public int rf_ClassID { get; set; }

        /// <summary>
        /// Класс.
        /// </summary>
        public wo_Class rf_Class { get; set; }

        /// <summary>
        /// Интерсекция дропа с игроками.
        /// </summary>
        public ICollection<wo_DropPlayer> DropPlayers { get; set; } = new List<wo_DropPlayer>();
    }
}
