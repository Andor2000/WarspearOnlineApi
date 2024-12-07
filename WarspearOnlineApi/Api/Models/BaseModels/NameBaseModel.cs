using WarspearOnlineApi.Api.Interfaces.Base;

namespace WarspearOnlineApi.Api.Models.BaseModels
{
    /// <summary>
    /// Базовая модель и наименованием.
    /// </summary>
    public class NameBaseModel : BaseModel, IName
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
