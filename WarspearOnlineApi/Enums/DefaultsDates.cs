namespace WarspearOnlineApi.Enums
{
    /// <summary>
    /// Значения по умолчанию для дат.
    /// </summary>
    public static class DefaultsDates
    {
        /// <summary>
        /// Минимальное значение по умолчанию для даты.
        /// </summary>
        public static DateTime MinDate => new DateTime(1900, 1, 1);

        /// <summary>
        /// Максимальное значение по умолчанию для даты.
        /// </summary>
        public static DateTime MaxDate => new DateTime(2222, 1, 1);
    }
}
