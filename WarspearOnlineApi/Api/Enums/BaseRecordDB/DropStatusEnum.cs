namespace WarspearOnlineApi.Api.Enums.BaseRecordDB
{
    /// <summary>
    /// Статусы дропа.
    /// </summary>
    public static class DropStatusEnum
    {
        /// <summary>
        /// Заполнение.
        /// </summary>
        public static string Filling = "Заполнение";

        /// <summary>
        /// Готово к выдаче.
        /// </summary>
        public static string ReadyForPickup = "Готово к выдаче";

        /// <summary>
        /// Закрыто.
        /// </summary>
        public static string Closed = "Закрыто";

        /// <summary>
        /// Получить код статуса.
        /// </summary>
        /// <param name="name">Название статуса.</param>
        /// <returns>Код статуса.</returns>
        public static string GetCode(string name)
        {
            return (name switch
            {
                nameof(Filling) => 1,
                nameof(ReadyForPickup) => 2,
                nameof(Closed) => 3,
                _ => throw new Exception("Неизвестный статус дропа.")
            }).ToString();
        }
    }
}
