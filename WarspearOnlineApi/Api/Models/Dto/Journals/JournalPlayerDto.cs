namespace WarspearOnlineApi.Api.Models.Dto.Journals
{
    /// <summary>
    /// Dto-модель журнала игроков.
    /// </summary>
    public class JournalPlayerDto
    {
        /// <summary>
        /// Игрок.
        /// </summary>
        public PlayerDto Player { get; set; } = new PlayerDto();

        /// <summary>
        /// Количество участий.
        /// </summary>
        public int ParticipationCount { get; set; }

        /// <summary>
        /// Выплачено.
        /// </summary>
        public int PaidOut { get; set; }

        /// <summary>
        /// Не выплачено.
        /// </summary>
        public int NotPaid { get; set; }

        /// <summary>
        /// Не выплачено и дроп закрыт.
        /// </summary>
        public int NotPaidClosed { get; set; }
    }
}
