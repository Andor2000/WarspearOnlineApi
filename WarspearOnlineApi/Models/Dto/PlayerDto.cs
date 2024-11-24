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
        /// Класс
        /// </summary>
        public ClassDto Class { get; set; } = new ClassDto();
    }
}
