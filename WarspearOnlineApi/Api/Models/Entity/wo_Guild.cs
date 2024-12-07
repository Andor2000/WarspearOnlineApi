﻿using System.ComponentModel.DataAnnotations;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель гильдии.
    /// </summary>
    public class wo_Guild
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int GuildID { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        [MaxLength(10)]
        public string GuildName { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int rf_ServerID { get; set; }

        /// <summary>
        /// Сервер.
        /// </summary>
        public wo_Server rf_Server { get; set; }

        /// <summary>
        /// Идентификтаор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }

        /// <summary>
        /// Интерсекиция группы и гильдии.
        /// </summary>
        public ICollection<wo_GroupGuild> GroupGuilds { get; set; } = new List<wo_GroupGuild>();
    }
}