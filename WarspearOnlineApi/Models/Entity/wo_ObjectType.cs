namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель типа объекта.
    /// </summary>
    public class wo_ObjectType
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ObjectTypeID { get; set; }

        /// <summary>
        /// Название типа объекта.
        /// </summary>
        public string ObjectTypeName { get; set; }

        /// <summary>
        /// Объекты.
        /// </summary>
        public ICollection<wo_Object> Objects { get; set; } = new List<wo_Object>();
    }
}
