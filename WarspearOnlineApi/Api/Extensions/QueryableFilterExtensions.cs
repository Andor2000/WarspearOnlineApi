using System.Linq.Expressions;

namespace WarspearOnlineApi.Api.Extensions
{
    /// <summary>
    /// Расширения для работы с коллекцией IQueryable.
    /// </summary>
    public static class QueryableFilterExtensions
    {
        /// <summary>
        /// Метод расширения для условного применения фильтрации с использованием метода Where.
        /// </summary>
        /// <typeparam name="T">Тип сущности.</typeparam>
        /// <param name="query">Исходный запрос.</param>
        /// <param name="condition">Условие, при котором фильтрация будет применена.</param>
        /// <param name="predicate">Логика фильтрации, если условие истинно.</param>
        /// <param name="elsePredicate">Логика фильтрации, если условие ложно.</param>
        /// <returns>Измененный запрос, с применением фильтрации, в зависимости от условия.</returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> query,
            bool condition,
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, bool>>? elsePredicate = null)
        {
            if (!condition)
            {
                if (elsePredicate == null)
                {
                    return query;
                }

                return query.Where(elsePredicate);
            }

            return query.Where(predicate);
        }
    }
}
