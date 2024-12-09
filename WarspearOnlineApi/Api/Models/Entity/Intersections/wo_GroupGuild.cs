namespace WarspearOnlineApi.Api.Models.Entity.Intersections
{
    /// <summary>
    /// Entity-модель связи группы и гильдии.
    /// </summary>
    public class wo_GroupGuild
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int GroupGuildID { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int rf_GroupID { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public wo_Group rf_Group { get; set; }

        /// <summary>
        /// Идентификатор гильдии.
        /// </summary>
        public int rf_GuildID { get; set; }

        /// <summary>
        /// Гильдия.
        /// </summary>
        public wo_Guild rf_Guild { get; set; }
    }
}
