namespace WarspearOnlineApi.Api.Extensions
{
    /// <summary>
    /// Расширения объектов.
    /// </summary>
    public static class ThrowExtensions
    {
        /// <summary>
        /// Проверка объекта переданным условием.<br/>
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="obj">Объект.</param>
        /// <param name="predicate">Условие вызова исключения.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Сам объект.</returns>
        public static T ThrowOnCondition<T>(this T obj, Func<T, bool> predicate, string message)
        {
            if (predicate.Invoke(obj))
            {
                throw new Exception(message + ".");
            }

            return obj!;
        }

        /// <summary>
        /// Проверка асинхронно получаемого объекта переданным условием.<br/>
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="awaitableObj">Асинхронно получаемый объект.</param>
        /// <param name="predicate">Условие вызова исключения.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Сам объект.</returns>
        public static async Task<T> ThrowOnConditionAsync<T>(this Task<T> awaitableObj, Func<T, bool> predicate, string message)
        {
            return (await awaitableObj).ThrowOnCondition(predicate, message);
        }

        /// <summary>
        /// Проверка объекта переданным условием.<br/>
        /// При выполнении условия, вызывает исключение вида "Объект не найден: name".
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="obj">Объект.</param>
        /// <param name="predicate">Условие вызова исключения.</param>
        /// <param name="name">Наименование объекта.</param>
        /// <returns>Сам объект.</returns>
        public static T ThrowNotFound<T>(this T obj, Func<T, bool> predicate, string name)
        {
            return obj.ThrowOnCondition(predicate, $"Объект не найден: {name}");
        }

        /// <summary>
        /// Проверка асинхронно получаемого объекта переданным условием.<br/>
        /// При выполнении условия, вызывает исключение вида "Объект не найден: name".
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="awaitableObj">Асинхронно получаемый объект.</param>
        /// <param name="predicate">Условие вызова исключения.</param>
        /// <param name="name">Наименование объекта.</param>
        /// <returns>Сам объект.</returns>
        public static async Task<T> ThrowNotFoundAsync<T>(this Task<T> awaitableObj, Func<T, bool> predicate, string name)
        {
            return (await awaitableObj).ThrowNotFound(predicate, name);
        }

        /// <summary>
        /// Проверка объекта на null.<br/>
        /// При выполнении условия, вызывает исключение вида "Объект не найден: name".
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="obj">Объект.</param>
        /// <param name="name">Наименование объекта.</param>
        /// <returns>Сам объект.</returns>
        public static T ThrowIfNull<T>(this T? obj, string name)
            where T : class
        {
            return obj.ThrowNotFound(x => x == null, name);
        }

        /// <summary>
        /// Проверка объекта на null.<br/>
        /// При выполнении условия, вызывает исключение вида "Объект не найден: name".
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="awaitableObj">Асинхронно получаемый объект.</param>
        /// <param name="name">Наименование объекта.</param>
        /// <returns>Сам объект.</returns>
        public static async Task<T> ThrowIfNullAsync<T>(this Task<T?> awaitableObj, string name)
            where T : class
        {
            return (await awaitableObj).ThrowIfNull(name);
        }
    }
}
