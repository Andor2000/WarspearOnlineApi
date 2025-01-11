using WarspearOnlineApi.Api.Interfaces.Base;
using WarspearOnlineApi.Api.Models.Filters;

namespace WarspearOnlineApi.Api.Extensions
{
    /// <summary>
    /// Расширения для работы с коллекцией IQueryable.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Расширение для фильтрации элементов в IQueryable на основе вхождения подстроки в свойство Name.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции, который должен реализовывать интерфейс IName.</typeparam>
        /// <param name="queryable">Коллекция IQueryable, которую нужно отфильтровать.</param>
        /// <param name="searchQuery">Строка для поиска. Если строка пуста или равна null, возвращается исходная коллекция.</param>
        /// <returns>Отфильтрованная коллекция, содержащая только элементы, где свойство Name содержит searchQuery.</returns>
        public static IQueryable<T> FilterByNameContains<T>(this IQueryable<T> queryable, string searchQuery)
            where T : IName
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
        public static IQueryable<T> SortByName<T>(this IQueryable<T> queryable)
            where T : IName
        {
            return queryable.OrderBy(x => x.Name);
        }

        /// <summary>
        /// Применяет пагинацию (Skip и Take) к запросу.
        /// </summary>
        /// <typeparam name="T">Тип элементов в запросе.</typeparam>
        /// <param name="query">Исходный запрос.</param>
        /// <param name="filter">Объект фильтра, содержащий параметры Skip и Take.</param>
        /// <param name="defaultTake">Значение по умолчанию для Take, если оно не задано в фильтре или задано как 0. По умолчанию равно 20.</param>
        /// <returns>Запрос с примененными Skip и Take.</returns>
        public static IQueryable<T> SkipTake<T>(this IQueryable<T> query, BaseFilterDto filter, int defaultTake = 50)
        {
            filter.ThrowIfNull("Фильтр не может быть null");

            var take = filter.Take > 0 ? filter.Take : defaultTake;
            var skip = filter.Skip < 0 ? 0 : filter.Skip;

            return query.Skip(skip).Take(take);
        }
    }
}
