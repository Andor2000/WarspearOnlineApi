using WarspearOnlineApi.Interfaces.Base;

namespace WarspearOnlineApi.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Расширение для фильтрации элементов в IQueryable на основе вхождения подстроки в свойство Name.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции, который должен реализовывать интерфейс IName.</typeparam>
        /// <param name="queryable">Коллекция IQueryable, которую нужно отфильтровать.</param>
        /// <param name="searchQuery">Строка для поиска. Если строка пуста или равна null, возвращается исходная коллекция.</param>
        /// <returns>Отфильтрованная коллекция, содержащая только элементы, где свойство Name содержит searchQuery.</returns>
        public static IQueryable<T> FilterByNameContains<T>(this IQueryable<T> queryable, string searchQuery) where T : IName
        {
            if (searchQuery.IsNullOrDefault())
            {
                return queryable;
            }

            return queryable.Where(x => x.Name.Contains(searchQuery));
        }
        /// <summary>
        /// Расширение для сортировки элементов IQueryable по свойству Name в алфавитном порядке.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции, который должен реализовывать интерфейс IName.</typeparam>
        /// <param name="queryable">Коллекция IQueryable, которую нужно отсортировать.</param>
        /// <returns>Отсортированная коллекция.</returns>
        public static IQueryable<T> SortByName<T>(this IQueryable<T> queryable) where T : IName
        {
            return queryable.OrderBy(x => x.Name);
        }
    }
}
