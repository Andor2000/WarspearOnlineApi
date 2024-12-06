﻿namespace WarspearOnlineApi.Models.Entity
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
        public string ObjectName { get; set; }

        /// <summary>
        /// Изображение объекта.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Идентификатор типа объекта.
        /// </summary>
        public int rf_ObjectTypeID { get; set; }

        /// <summary>
        /// Тип объекта.
        /// </summary>
        public wo_ObjectType rf_ObjectType { get; set; } 

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();
    }
}
