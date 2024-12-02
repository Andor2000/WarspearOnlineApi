using WarspearOnlineApi.Interfaces.Base;

namespace WarspearOnlineApi.Models.BaseModels
{
    /// <summary>
    /// Базовая модель.
    /// </summary>
    public class BaseModel : IIdentifier
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; } = 0;
    }
}
