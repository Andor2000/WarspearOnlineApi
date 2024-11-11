namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель связи журнала дропа и игрока.
    /// </summary>
    public class DropJournalPlayerEntity : BaseModel
    {
        /// <summary>
        /// Идентификатор записи в журнале дропа.
        /// </summary>
        public int DropJournalId { get; set; }

        /// <summary>
        /// Идентификатор игрока.
        /// </summary>
        public int PlayerId { get; set; }
    }
}
