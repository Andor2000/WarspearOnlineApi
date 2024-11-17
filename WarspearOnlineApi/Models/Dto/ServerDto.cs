namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель сервера.
    /// </summary>
    public class ServerDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название сервера.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
