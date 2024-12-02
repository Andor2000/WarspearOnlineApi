using WarspearOnlineApi.Interfaces.Base;

namespace WarspearOnlineApi.Models.BaseModels
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
