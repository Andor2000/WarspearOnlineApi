namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель объекта.
    /// </summary>
    public class ObjectEntity : BaseModel
    {
        /// <summary>
        /// Название объекта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Изображение объекта.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Идентификатор типа объекта.
        /// </summary>
        public int ObjectTypeId { get; set; }
    }
}
