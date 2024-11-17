namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель типа объекта.
    /// </summary>
    public class ObjectTypeDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название типа объекта.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
