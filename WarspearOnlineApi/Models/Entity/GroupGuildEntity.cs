namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель связи группы и гильдии.
    /// </summary>
    public class GroupGuildEntity : BaseModel
    {
        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Идентификатор гильдии.
        /// </summary>
        public int GuildId { get; set; }
    }
}
