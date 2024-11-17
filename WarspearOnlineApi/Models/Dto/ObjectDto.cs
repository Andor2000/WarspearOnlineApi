namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель объекта.
    /// </summary>
    public class ObjectDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название объекта.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Изображение объекта.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Тип объекта.
        /// </summary>
        public ObjectTypeDto ObjectType { get; set; } = new ObjectTypeDto();
    }
}
