namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель объекта.
    /// </summary>
    public class wo_Object
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ObjectID { get; set; }

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

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();
    }
}
