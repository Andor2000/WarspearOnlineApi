namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель группы.
    /// </summary>
    public class GroupDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Сервер.
        /// </summary>
        public ServerDto Server { get; set; } = new ServerDto();
    }
}
