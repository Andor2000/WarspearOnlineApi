using WarspearOnlineApi.Enums.BaseRecordDB;

namespace WarspearOnlineApi.Data
{
    /// <summary>
    /// Sql-запросы для инициализации базы данных.
    /// </summary>
    public static class SqlQueriesInitializer
    {
        /// <summary>
        /// Sql-запросы для инициализации базы данных.
        /// </summary>
        public static readonly string CreateBaseRecords = $@"
MERGE wo_AccessLevel as TARGET
USING (
	VALUES
	    ('{nameof(AccessLevelEnums.MainAdmin)}', '{AccessLevelEnums.MainAdmin}')
	   ,('{nameof(AccessLevelEnums.Admin)}', '{AccessLevelEnums.Admin}')
	   ,('{nameof(AccessLevelEnums.Moderator)}', '{AccessLevelEnums.Moderator}')
) as source (AccessLevelCode, AccessLevelName)
on TARGET.AccessLevelCode = source.AccessLevelCode
WHEN MATCHED and
   (TARGET.AccessLevelName != source.AccessLevelName)
THEN
    UPDATE SET
        TARGET.AccessLevelName = source.AccessLevelName
WHEN NOT MATCHED THEN
    INSERT (AccessLevelCode, AccessLevelName)
    VALUES (source.AccessLevelCode, source.AccessLevelName);

UPDATE record
SET rf_ParentAccessLevelID = parent.AccessLevelID
FROM (
    VALUES
        ('{nameof(AccessLevelEnums.Admin)}', '{nameof(AccessLevelEnums.MainAdmin)}'),
        ('{nameof(AccessLevelEnums.Moderator)}', '{nameof(AccessLevelEnums.Admin)}')
) as subquery (recordCode, parentCode)
join wo_AccessLevel as record on record.AccessLevelCode = subquery.recordCode
join wo_AccessLevel as parent on parent.AccessLevelCode = subquery.parentCode
where record.rf_ParentAccessLevelID != parent.AccessLevelID

MERGE wo_Role AS TARGET
USING (
    VALUES
        ('{nameof(RoleEnums.AddPlayer)}', '{RoleEnums.AddPlayer}')
       ,('{nameof(RoleEnums.AddDrop)}', '{RoleEnums.AddDrop}')
       ,('{nameof(RoleEnums.AddGroup)}', '{RoleEnums.AddGroup}')
       ,('{nameof(RoleEnums.AddGuild)}', '{RoleEnums.AddGuild}')
       ,('{nameof(RoleEnums.AddObject)}', '{RoleEnums.AddObject}')
       ,('{nameof(RoleEnums.AddClass)}', '{RoleEnums.AddClass}')
) AS source (RoleCode, RoleName)
ON TARGET.RoleCode = source.RoleCode
WHEN MATCHED AND
   (TARGET.RoleName != source.RoleName)
THEN
    UPDATE SET
        TARGET.RoleName = source.RoleName
WHEN NOT MATCHED THEN
    INSERT (RoleCode, RoleName)
    VALUES (source.RoleCode, source.RoleName);

MERGE wo_AccessLevelRole AS TARGET
USING (
    select
	role.RoleID,
	al.AccessLevelID
	from (
		VALUES 
			 ('{nameof(RoleEnums.AddPlayer)}', '{nameof(AccessLevelEnums.Moderator)}')
			,('{nameof(RoleEnums.AddDrop)}', '{nameof(AccessLevelEnums.Moderator)}')
			,('{nameof(RoleEnums.AddGuild)}', '{nameof(AccessLevelEnums.Admin)}')
			,('{nameof(RoleEnums.AddGroup)}', '{nameof(AccessLevelEnums.MainAdmin)}')
			,('{nameof(RoleEnums.AddObject)}', '{nameof(AccessLevelEnums.MainAdmin)}')
			,('{nameof(RoleEnums.AddClass)}', '{nameof(AccessLevelEnums.MainAdmin)}')
	) as source (role, accessLevel)
	join wo_Role as role on source.role = role.RoleCode
	join wo_AccessLevel as al on source.accessLevel = al.AccessLevelCode
) AS source (rf_RoleID, rf_AccessLevelID)
ON TARGET.rf_AccessLevelID = source.rf_AccessLevelID
WHEN NOT MATCHED THEN
    INSERT (rf_AccessLevelID, rf_RoleID)
    VALUES (source.rf_AccessLevelID, source.rf_RoleID);


wo_Server
wo_Class
wo_Fraction
wo_Object
wo_ObjectType



";
    }
}
