using WarspearOnlineApi.Models.BaseModels;

namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель сервера.
    /// </summary>
    public class ServerDto : BaseModel
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }
    }
}
