namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель интерсекции класса и фракции.
    /// </summary>
    public class wo_ClassFraction
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ClassFractionID { get; set; }

        /// <summary>
        /// Идентификатор класса.
        /// </summary>
        public int rf_ClassID { get; set; }

        /// <summary>
        /// Класс.
        /// </summary>
        public wo_Class rf_Class { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }
    }
}
