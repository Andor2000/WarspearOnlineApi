using WarspearOnlineApi.Api.Interfaces.Base;

namespace WarspearOnlineApi.Api.Models.BaseModels
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
