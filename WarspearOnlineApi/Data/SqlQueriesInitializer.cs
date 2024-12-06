using WarspearOnlineApi.Enums.BaseRecordDB;
using WarspearOnlineApi.Models.Entity;
using WarspearOnlineApi.Models.Entity.Users;

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
MERGE {nameof(wo_AccessLevel)} as TARGET
USING (
	VALUES
	    ('{nameof(AccessLevelEnums.MainAdmin)}', '{AccessLevelEnums.MainAdmin}')
	   ,('{nameof(AccessLevelEnums.Admin)}', '{AccessLevelEnums.Admin}')
	   ,('{nameof(AccessLevelEnums.Moderator)}', '{AccessLevelEnums.Moderator}')
) as source ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_AccessLevel.AccessLevelName)})
on TARGET.{nameof(wo_AccessLevel.AccessLevelCode)} = source.{nameof(wo_AccessLevel.AccessLevelName)}
WHEN MATCHED and
   (TARGET.{nameof(wo_AccessLevel.AccessLevelName)} != source.{nameof(wo_AccessLevel.AccessLevelName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_AccessLevel.AccessLevelName)} = source.{nameof(wo_AccessLevel.AccessLevelName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_AccessLevel.AccessLevelName)})
    VALUES (source.{nameof(wo_AccessLevel.AccessLevelCode)}, source.{nameof(wo_AccessLevel.AccessLevelName)});


UPDATE record
SET {nameof(wo_AccessLevel.rf_ParentAccessLevel)} = parent.{nameof(wo_AccessLevel.AccessLevelID)}
FROM (
    VALUES
        ('{nameof(AccessLevelEnums.Admin)}', '{nameof(AccessLevelEnums.MainAdmin)}'),
        ('{nameof(AccessLevelEnums.Moderator)}', '{nameof(AccessLevelEnums.Admin)}')
) as subquery (recordCode, parentCode)
join {nameof(wo_AccessLevel)} as record on record.{nameof(wo_AccessLevel.AccessLevelCode)} = subquery.recordCode
join {nameof(wo_AccessLevel)} as parent on parent.{nameof(wo_AccessLevel.AccessLevelCode)} = subquery.parentCode
where record.{nameof(wo_AccessLevel.rf_ParentAccessLevel)} != parent.{nameof(wo_AccessLevel.AccessLevelID)}


MERGE {nameof(wo_Role)} AS TARGET
USING (
    VALUES
        ('{nameof(RoleEnums.AddPlayer)}', '{RoleEnums.AddPlayer}')
       ,('{nameof(RoleEnums.AddDrop)}', '{RoleEnums.AddDrop}')
       ,('{nameof(RoleEnums.AddGroup)}', '{RoleEnums.AddGroup}')
       ,('{nameof(RoleEnums.AddGuild)}', '{RoleEnums.AddGuild}')
       ,('{nameof(RoleEnums.AddObject)}', '{RoleEnums.AddObject}')
       ,('{nameof(RoleEnums.AddClass)}', '{RoleEnums.AddClass}')
) AS source ({nameof(wo_Role.RoleCode)}, {nameof(wo_Role.RoleName)})
ON TARGET.{nameof(wo_Role.RoleCode)} = source.{nameof(wo_Role.RoleCode)}
WHEN MATCHED AND
   (TARGET.{nameof(wo_Role.RoleName)} != source.{nameof(wo_Role.RoleName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_Role.RoleName)} = source.{nameof(wo_Role.RoleName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Role.RoleCode)}, {nameof(wo_Role.RoleName)})
    VALUES (source.{nameof(wo_Role.RoleCode)}, source.{nameof(wo_Role.RoleName)});


MERGE {nameof(wo_AccessLevelRole)} AS TARGET
USING (
    select
	role.{nameof(wo_Role.RoleID)},
	al.{nameof(wo_AccessLevel.AccessLevelID)}
	from (
		VALUES 
			 ('{nameof(RoleEnums.AddPlayer)}', '{nameof(AccessLevelEnums.Moderator)}')
			,('{nameof(RoleEnums.AddDrop)}', '{nameof(AccessLevelEnums.Moderator)}')
			,('{nameof(RoleEnums.AddGuild)}', '{nameof(AccessLevelEnums.Admin)}')
			,('{nameof(RoleEnums.AddGroup)}', '{nameof(AccessLevelEnums.MainAdmin)}')
			,('{nameof(RoleEnums.AddObject)}', '{nameof(AccessLevelEnums.MainAdmin)}')
			,('{nameof(RoleEnums.AddClass)}', '{nameof(AccessLevelEnums.MainAdmin)}')
	) as source (role, accessLevel)
	join {nameof(wo_Role)} as role on source.role = role.{nameof(wo_Role.RoleCode)}
	join {nameof(wo_AccessLevel)} as al on source.accessLevel = al.{nameof(wo_AccessLevel.AccessLevelCode)}
) AS source ({nameof(wo_AccessLevelRole.rf_RoleID)}, {nameof(wo_AccessLevelRole.rf_AccessLevelID)})
ON TARGET.{nameof(wo_AccessLevelRole.rf_AccessLevelID)} = source.{nameof(wo_AccessLevelRole.rf_AccessLevelID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_AccessLevelRole.rf_RoleID)}, {nameof(wo_AccessLevelRole.rf_AccessLevelID)})
    VALUES (source.{nameof(wo_AccessLevelRole.rf_RoleID)}, source.{nameof(wo_AccessLevelRole.rf_AccessLevelID)});




MERGE {nameof(wo_Server)} AS TARGET
USING (
    VALUES
        ('{nameof(ServerEnums.RU_Topaz)}')
       ,('{nameof(ServerEnums.RU_Amber)}')
       ,('{nameof(ServerEnums.RU_Ruby)}')
) AS source ({nameof(wo_Server.ServerCode)})
ON TARGET.{nameof(wo_Server.ServerCode)} = source.{nameof(wo_Server.ServerCode)}        
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Server.ServerCode)})
    VALUES (source.{nameof(wo_Server.ServerCode)});

MERGE {nameof(wo_Class)} AS TARGET
USING (
    VALUES
        ('{nameof(ClassEnums.Druid)}', '{nameof(ClassEnums.Druid)}')
       ,('{nameof(ClassEnums.StrikingBlade)}', '{nameof(ClassEnums.StrikingBlade)}')
       ,('{nameof(ClassEnums.Ranger)}', '{nameof(ClassEnums.Ranger)}')
       ,('{nameof(ClassEnums.Guardian)}', '{nameof(ClassEnums.Guardian)}')
       ,('{nameof(ClassEnums.Hunter)}', '{nameof(ClassEnums.Hunter)}')
       ,('{nameof(ClassEnums.Paladin)}', '{nameof(ClassEnums.Paladin)}')
       ,('{nameof(ClassEnums.Priest)}', '{nameof(ClassEnums.Priest)}')
       ,('{nameof(ClassEnums.Mage)}', '{nameof(ClassEnums.Mage)}')
       ,('{nameof(ClassEnums.Seeker)}', '{nameof(ClassEnums.Seeker)}')
       ,('{nameof(ClassEnums.Templar)}', '{nameof(ClassEnums.Templar)}')
       ,('{nameof(ClassEnums.Barbarian)}', '{nameof(ClassEnums.Barbarian)}')
       ,('{nameof(ClassEnums.Rogue)}', '{nameof(ClassEnums.Rogue)}')
       ,('{nameof(ClassEnums.Shaman)}', '{nameof(ClassEnums.Shaman)}')
       ,('{nameof(ClassEnums.Archer)}', '{nameof(ClassEnums.Archer)}')
       ,('{nameof(ClassEnums.Chieftain)}', '{nameof(ClassEnums.Chieftain)}')
       ,('{nameof(ClassEnums.Necromancer)}', '{nameof(ClassEnums.Necromancer)}')
       ,('{nameof(ClassEnums.Warlock)}', '{nameof(ClassEnums.Warlock)}')
       ,('{nameof(ClassEnums.DeathKnight)}', '{nameof(ClassEnums.DeathKnight)}')
       ,('{nameof(ClassEnums.Spellcaster)}', '{nameof(ClassEnums.Spellcaster)}')
       ,('{nameof(ClassEnums.Reaper)}', '{nameof(ClassEnums.Reaper)}')
) AS source ({nameof(wo_Class.ClassCode)}, {nameof(wo_Class.ClassName)})
ON TARGET.{nameof(wo_Class.ClassCode)} = source.{nameof(wo_Class.ClassCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_Class.ClassName)} != source.{nameof(wo_Class.ClassName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_Class.ClassName)} = source.{nameof(wo_Class.ClassName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Class.ClassCode)}, {nameof(wo_Class.ClassName)})
    VALUES (source.{nameof(wo_Class.ClassCode)}, source.{nameof(wo_Class.ClassName)});

MERGE {nameof(wo_Fraction)} AS TARGET
USING (
    VALUES
        ('{nameof(FractionEnums.Guardian)}', '{nameof(FractionEnums.Guardian)}')
       ,('{nameof(FractionEnums.Legion)}', '{nameof(FractionEnums.Legion)}')
) AS source ({nameof(wo_Fraction.FractionCode)}, {nameof(wo_Fraction.FractionName)})
ON TARGET.{nameof(wo_Fraction.FractionCode)} = source.{nameof(wo_Fraction.FractionCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_Fraction.FractionName)} != source.{nameof(wo_Fraction.FractionName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_Fraction.FractionName)} = source.{nameof(wo_Fraction.FractionName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Fraction.FractionCode)}, {nameof(wo_Fraction.FractionName)})
    VALUES (source.{nameof(wo_Fraction.FractionCode)}, source.{nameof(wo_Fraction.FractionName)});

MERGE {nameof(wo_ClassFraction)} AS TARGET
USING (
	select
	class.{nameof(wo_Class.ClassID)},
	fraction.{nameof(wo_Fraction.FractionID)}
	from (
		VALUES
		    ('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Druid)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.StrikingBlade)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Ranger)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Guardian)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Hunter)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Paladin)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Priest)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Mage)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Seeker)}')
           ,('{nameof(FractionEnums.Guardian)}', '{nameof(ClassEnums.Templar)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Barbarian)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Rogue)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Shaman)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Archer)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Chieftain)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Necromancer)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Warlock)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.DeathKnight)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Spellcaster)}')
           ,('{nameof(FractionEnums.Legion)}', '{nameof(ClassEnums.Reaper)}')
	) as source ({nameof(wo_Fraction.FractionCode)}, {nameof(wo_Class.ClassCode)})
	join {nameof(wo_Fraction)} as fractoin on source.{nameof(wo_Fraction.FractionCode)} = fractoin.{nameof(wo_Fraction.FractionCode)}
	join {nameof(wo_Class)} as class on source.{nameof(wo_Class.ClassCode)} = class.{nameof(wo_Class.ClassCode)}
) AS source ({nameof(wo_ClassFraction.rf_ClassID)}, {nameof(wo_ClassFraction.rf_FractionID)})
ON TARGET.{nameof(wo_ClassFraction.rf_FractionID)} = source.{nameof(wo_ClassFraction.rf_FractionID)} and TARGET.{nameof(wo_ClassFraction.rf_ClassID)} = source.{nameof(wo_ClassFraction.rf_ClassID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_ClassFraction.rf_FractionID)}, {nameof(wo_ClassFraction.rf_ClassID)})
    VALUES (source.{nameof(wo_ClassFraction.rf_FractionID)}, source.{nameof(wo_ClassFraction.rf_ClassID)});


MERGE {nameof(wo_ObjectType)} AS TARGET
USING (
    VALUES
        ('{nameof(ObjectTypeEnums.Book)}', '{ObjectTypeEnums.Book}')
       ,('{nameof(ObjectTypeEnums.Costume)}', '{ObjectTypeEnums.Costume}')
) AS source ({nameof(wo_ObjectType.ObjectTypeCode)}, {nameof(wo_ObjectType.ObjectTypeName)})
ON TARGET.{nameof(wo_ObjectType.ObjectTypeCode)} = source.{nameof(wo_ObjectType.ObjectTypeCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_ObjectType.ObjectTypeName)} != source.{nameof(wo_ObjectType.ObjectTypeName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_ObjectType.ObjectTypeName)} = source.{nameof(wo_ObjectType.ObjectTypeName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_ObjectType.ObjectTypeCode)}, {nameof(wo_ObjectType.ObjectTypeName)})
    VALUES (source.{nameof(wo_ObjectType.ObjectTypeCode)}, source.{nameof(wo_ObjectType.ObjectTypeName)});


wo_Object

";
    }
}
