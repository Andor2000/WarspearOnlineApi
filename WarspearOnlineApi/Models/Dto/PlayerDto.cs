namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель игрока
    /// </summary>
    public class PlayerDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ник.
        /// </summary>
        public string Nick { get; set; } = string.Empty;

        /// <summary>
        /// Доля.
        /// </summary>
        public int Part { get; set; }

        /// <summary>
        /// Признак выплаты.
        /// </summary>
        public bool IsPaid { get; set; }
    }
}
