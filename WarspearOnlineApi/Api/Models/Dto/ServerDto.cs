using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto
{
    /// <summary>
    /// Dto-модель сервера.
    /// </summary>
    public class ServerDto : CodeNameBaseModel
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }
    }
}
