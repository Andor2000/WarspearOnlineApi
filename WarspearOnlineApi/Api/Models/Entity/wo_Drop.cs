﻿using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель выпавшего дропа.
    /// </summary>
    public class wo_Drop
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int DropID { get; set; }

        /// <summary>
        /// Дата дропа.
        /// </summary>
        public DateTime Drop_Date { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public int rf_ObjectID { get; set; }

        /// <summary>
        /// Объект.
        /// </summary>
        public wo_Object rf_Object { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int rf_GroupID { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public wo_Group rf_Group { get; set; }

        /// <summary>
        /// Интерсекция дропа с игроками.
        /// </summary>
        public ICollection<wo_DropPlayer> DropPlayers { get; set; } = new List<wo_DropPlayer>();
    }
}
