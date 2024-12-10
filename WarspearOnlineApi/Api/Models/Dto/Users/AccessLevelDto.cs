using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    /// Dto-модель уровня доступа.
    /// </summary>
    public class AccessLevelDto : CodeNameBaseModel
    {
        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public int Level { get; set; }
    }
}
