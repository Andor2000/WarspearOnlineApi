namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель связи дропа с игроком.
    /// </summary>
    public class DropPlayerDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Доля.
        /// </summary>
        public int Part { get; set; }

        /// <summary>
        /// Признак выплаты.
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Класс
        /// </summary>
        public PlayerDto Player { get; set; } = new PlayerDto();
    }
}
