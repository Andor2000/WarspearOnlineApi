using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto.Admin
{
    /// <summary>
    /// Модель данных администратора.
    /// </summary>
    public class UserAdminDto : BaseModel
    {
        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int FractionId { get; set; }

        /// <summary>
        /// Код уровня доступа.
        /// </summary>
        public string AccessLevelCode { get; set; } = string.Empty;
    }
}
