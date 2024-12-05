using WarspearOnlineApi.Interfaces.Base;

namespace WarspearOnlineApi.Models.BaseModels
{
    public class CodeNameBaseModel : NameBaseModel, ICode
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }
    }
}
