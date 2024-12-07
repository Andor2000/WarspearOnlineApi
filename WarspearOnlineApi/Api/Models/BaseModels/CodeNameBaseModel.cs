using WarspearOnlineApi.Api.Interfaces.Base;

namespace WarspearOnlineApi.Api.Models.BaseModels
{
    public class CodeNameBaseModel : NameBaseModel, ICode
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }
    }
}
