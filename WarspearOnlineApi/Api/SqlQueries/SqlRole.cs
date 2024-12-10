using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.SqlQueries
{
    /// <summary>
    /// Запросы для работы с ролями.
    /// </summary>
    public static class SqlRole
    {
        /// <summary>
        /// Получить роли по уровню доступа.
        /// </summary>
        public static readonly string GetRolesByAccessLevel = $@"
with RecursiveAccessLevel as (
    -- Начальный уровень (текущий уровень доступа пользователя)
    select
	al.{nameof(wo_AccessLevel.AccessLevelID)},
	al.{nameof(wo_AccessLevel.rf_ParentAccessLevelID)}
    from {nameof(wo_AccessLevel)} as al
    where {nameof(wo_AccessLevel.AccessLevelID)} = @AccessLevelID
		
    union all

    -- Рекурсивно находим родителей
    select
	al.{nameof(wo_AccessLevel.AccessLevelID)},
	al.{nameof(wo_AccessLevel.rf_ParentAccessLevelID)}
    from {nameof(wo_AccessLevel)} as al
    join RecursiveAccessLevel as ral on al.{nameof(wo_AccessLevel.AccessLevelID)} = ral.{nameof(wo_AccessLevel.rf_ParentAccessLevelID)}
	where al.{nameof(wo_AccessLevel.AccessLevelID)} > 0
)

select distinct alr.{nameof(wo_AccessLevelRole.rf_RoleID)}
from RecursiveAccessLevel as ral
join {nameof(wo_AccessLevelRole)} as alr on ral.{nameof(wo_AccessLevel.AccessLevelID)} = alr.{nameof(wo_AccessLevelRole.rf_AccessLevelID)}
where alr.{nameof(wo_AccessLevelRole.rf_RoleID)} > 0";
    }
}
