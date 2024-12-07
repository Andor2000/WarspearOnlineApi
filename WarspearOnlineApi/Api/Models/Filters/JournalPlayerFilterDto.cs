namespace WarspearOnlineApi.Api.Models.Filters
{
    /// <summary>
    /// Dto-модель фильтра для получения журнала игроков.
    /// </summary>
    public class JournalPlayerFilterDto : BaseFilterDto
    {
        /// <summary>
        /// Ник игрока.
        /// </summary>
        public string Nick { get; set; }
    }
}
