﻿using WarspearOnlineApi.Models.BaseModels;

namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель объекта.
    /// </summary>
    public class ObjectDto : CodeNameBaseModel
    {
        /// <summary>
        /// Изображение объекта.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Тип объекта.
        /// </summary>
        public CodeNameBaseModel ObjectType { get; set; } = new CodeNameBaseModel();
    }
}
