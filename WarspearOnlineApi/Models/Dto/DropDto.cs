﻿using WarspearOnlineApi.Enums;

namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель дропа.
    /// </summary>
    public class DropDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата дропа.
        /// </summary>
        public DateTime Date { get; set; } = DefaultsDates.MinDate;

        /// <summary>
        /// Цена.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество игроков.
        /// </summary>
        public int PlayersCount { get; set; }

        /// <summary>
        /// Доля \ часть денег.
        /// </summary>
        public int Part { get; set; }

        /// <summary>
        /// Объект.
        /// </summary>
        public ObjectDto Object { get; set; } = new ObjectDto();

        /// <summary>
        /// Группа.
        /// </summary>
        public GroupDto Group { get; set; } = new GroupDto();

        /// <summary>
        /// Сервер.
        /// </summary>
        public ServerDto Server { get; set; } = new ServerDto();

    }
}