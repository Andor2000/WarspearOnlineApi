using WarspearOnlineApi.Models.BaseModels;

namespace WarspearOnlineApi.Models.Dto
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
