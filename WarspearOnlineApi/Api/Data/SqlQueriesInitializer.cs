using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Data
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
	    ('{nameof(AccessLevelEnum.Moderator)}', '{AccessLevelEnum.Moderator}')
	   ,('{nameof(AccessLevelEnum.Admin)}', '{AccessLevelEnum.Admin}')
	   ,('{nameof(AccessLevelEnum.MainAdmin)}', '{AccessLevelEnum.MainAdmin}')
) as source ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_AccessLevel.AccessLevelName)})
on TARGET.{nameof(wo_AccessLevel.AccessLevelCode)} = source.{nameof(wo_AccessLevel.AccessLevelCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_AccessLevel.AccessLevelName)} != source.{nameof(wo_AccessLevel.AccessLevelName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_AccessLevel.AccessLevelName)} = source.{nameof(wo_AccessLevel.AccessLevelName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_AccessLevel.AccessLevelName)})
    VALUES (source.{nameof(wo_AccessLevel.AccessLevelCode)}, source.{nameof(wo_AccessLevel.AccessLevelName)});


UPDATE record
SET {nameof(wo_AccessLevel.rf_ParentAccessLevelID)} = parent.{nameof(wo_AccessLevel.AccessLevelID)}
FROM (
    VALUES
        ('{nameof(AccessLevelEnum.Moderator)}', ''),
        ('{nameof(AccessLevelEnum.MainAdmin)}', '{nameof(AccessLevelEnum.Admin)}'),
        ('{nameof(AccessLevelEnum.Admin)}', '{nameof(AccessLevelEnum.Moderator)}')
) as subquery (record{nameof(wo_AccessLevel.AccessLevelCode)}, parent{nameof(wo_AccessLevel.AccessLevelCode)})
join {nameof(wo_AccessLevel)} as record on record.{nameof(wo_AccessLevel.AccessLevelCode)} = subquery.record{nameof(wo_AccessLevel.AccessLevelCode)}
join {nameof(wo_AccessLevel)} as parent on parent.{nameof(wo_AccessLevel.AccessLevelCode)} = subquery.parent{nameof(wo_AccessLevel.AccessLevelCode)}
where record.{nameof(wo_AccessLevel.rf_ParentAccessLevelID)} != parent.{nameof(wo_AccessLevel.AccessLevelID)}


MERGE {nameof(wo_Role)} AS TARGET
USING (
    VALUES
        ('{nameof(RoleEnum.AddDeletePlayerInDrop)}', '{RoleEnum.AddDeletePlayerInDrop}')
       ,('{nameof(RoleEnum.AddDrop)}', '{RoleEnum.AddDrop}')
       ,('{nameof(RoleEnum.DeleteDrop)}', '{RoleEnum.DeleteDrop}')
       ,('{nameof(RoleEnum.AddDeleteGroup)}', '{RoleEnum.AddDeleteGroup}')
       ,('{nameof(RoleEnum.AddGuild)}', '{RoleEnum.AddGuild}')
       ,('{nameof(RoleEnum.AddObject)}', '{RoleEnum.AddObject}')
       ,('{nameof(RoleEnum.AddClass)}', '{RoleEnum.AddClass}')
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
			 ('{nameof(RoleEnum.AddDeletePlayerInDrop)}', '{nameof(AccessLevelEnum.Moderator)}')
			,('{nameof(RoleEnum.AddDrop)}', '{nameof(AccessLevelEnum.Moderator)}')
			,('{nameof(RoleEnum.DeleteDrop)}', '{nameof(AccessLevelEnum.Admin)}')
			,('{nameof(RoleEnum.AddGuild)}', '{nameof(AccessLevelEnum.Admin)}')
			,('{nameof(RoleEnum.AddDeleteGroup)}', '{nameof(AccessLevelEnum.MainAdmin)}')
			,('{nameof(RoleEnum.AddObject)}', '{nameof(AccessLevelEnum.MainAdmin)}')
			,('{nameof(RoleEnum.AddClass)}', '{nameof(AccessLevelEnum.MainAdmin)}')
	) as source ({nameof(wo_Role.RoleCode)}, {nameof(wo_AccessLevel.AccessLevelCode)})
	join {nameof(wo_Role)} as role on source.{nameof(wo_Role.RoleCode)} = role.{nameof(wo_Role.RoleCode)}
	join {nameof(wo_AccessLevel)} as al on source.{nameof(wo_AccessLevel.AccessLevelCode)} = al.{nameof(wo_AccessLevel.AccessLevelCode)}
) AS source ({nameof(wo_AccessLevelRole.rf_RoleID)}, {nameof(wo_AccessLevelRole.rf_AccessLevelID)})
ON TARGET.{nameof(wo_AccessLevelRole.rf_AccessLevelID)} = source.{nameof(wo_AccessLevelRole.rf_AccessLevelID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_AccessLevelRole.rf_RoleID)}, {nameof(wo_AccessLevelRole.rf_AccessLevelID)})
    VALUES (source.{nameof(wo_AccessLevelRole.rf_RoleID)}, source.{nameof(wo_AccessLevelRole.rf_AccessLevelID)});




MERGE {nameof(wo_Server)} AS TARGET
USING (
    VALUES
        ('{nameof(ServerEnum.RU_Topaz)}')
       ,('{nameof(ServerEnum.RU_Amber)}')
       ,('{nameof(ServerEnum.RU_Ruby)}')
) AS source ({nameof(wo_Server.ServerCode)})
ON TARGET.{nameof(wo_Server.ServerCode)} = source.{nameof(wo_Server.ServerCode)}        
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Server.ServerCode)})
    VALUES (source.{nameof(wo_Server.ServerCode)});

MERGE {nameof(wo_Class)} AS TARGET
USING (
    VALUES
        ('{nameof(ClassEnum.Druid)}', '{ClassEnum.Druid}')
       ,('{nameof(ClassEnum.StrikingBlade)}', '{ClassEnum.StrikingBlade}')
       ,('{nameof(ClassEnum.Ranger)}', '{ClassEnum.Ranger}')
       ,('{nameof(ClassEnum.Guardian)}', '{ClassEnum.Guardian}')
       ,('{nameof(ClassEnum.Hunter)}', '{ClassEnum.Hunter}')
       ,('{nameof(ClassEnum.Paladin)}', '{ClassEnum.Paladin}')
       ,('{nameof(ClassEnum.Priest)}', '{ClassEnum.Priest}')
       ,('{nameof(ClassEnum.Mage)}', '{ClassEnum.Mage}')
       ,('{nameof(ClassEnum.Seeker)}', '{ClassEnum.Seeker}')
       ,('{nameof(ClassEnum.Templar)}', '{ClassEnum.Templar}')
       ,('{nameof(ClassEnum.Barbarian)}', '{ClassEnum.Barbarian}')
       ,('{nameof(ClassEnum.Rogue)}', '{ClassEnum.Rogue}')
       ,('{nameof(ClassEnum.Shaman)}', '{ClassEnum.Shaman}')
       ,('{nameof(ClassEnum.Archer)}', '{ClassEnum.Archer}')
       ,('{nameof(ClassEnum.Chieftain)}', '{ClassEnum.Chieftain}')
       ,('{nameof(ClassEnum.Necromancer)}', '{ClassEnum.Necromancer}')
       ,('{nameof(ClassEnum.Warlock)}', '{ClassEnum.Warlock}')
       ,('{nameof(ClassEnum.DeathKnight)}', '{ClassEnum.DeathKnight}')
       ,('{nameof(ClassEnum.Spellcaster)}', '{ClassEnum.Spellcaster}')
       ,('{nameof(ClassEnum.Reaper)}', '{ClassEnum.Reaper}')
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
        ('{nameof(FractionEnum.Guardian)}', '{FractionEnum.Guardian}')
       ,('{nameof(FractionEnum.Legion)}', '{FractionEnum.Legion}')
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
		    ('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Druid)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.StrikingBlade)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Ranger)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Guardian)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Hunter)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Paladin)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Priest)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Mage)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Seeker)}')
           ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Templar)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Barbarian)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Rogue)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Shaman)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Archer)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Chieftain)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Necromancer)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Warlock)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.DeathKnight)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Spellcaster)}')
           ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Reaper)}')
	) as source ({nameof(wo_Fraction.FractionCode)}, {nameof(wo_Class.ClassCode)})
	join {nameof(wo_Fraction)} as fraction on source.{nameof(wo_Fraction.FractionCode)} = fraction.{nameof(wo_Fraction.FractionCode)}
	join {nameof(wo_Class)} as class on source.{nameof(wo_Class.ClassCode)} = class.{nameof(wo_Class.ClassCode)}
) AS source ({nameof(wo_ClassFraction.rf_ClassID)}, {nameof(wo_ClassFraction.rf_FractionID)})
ON TARGET.{nameof(wo_ClassFraction.rf_FractionID)} = source.{nameof(wo_ClassFraction.rf_FractionID)} and TARGET.{nameof(wo_ClassFraction.rf_ClassID)} = source.{nameof(wo_ClassFraction.rf_ClassID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_ClassFraction.rf_FractionID)}, {nameof(wo_ClassFraction.rf_ClassID)})
    VALUES (source.{nameof(wo_ClassFraction.rf_FractionID)}, source.{nameof(wo_ClassFraction.rf_ClassID)});


MERGE {nameof(wo_ObjectType)} AS TARGET
USING (
    VALUES
        ('{nameof(ObjectTypeEnum.Book)}', '{ObjectTypeEnum.Book}')
       ,('{nameof(ObjectTypeEnum.Costume)}', '{ObjectTypeEnum.Costume}')
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


MERGE {nameof(wo_Object)} AS TARGET
USING (
	select
	source.{nameof(wo_Object.ObjectCode)},
	source.{nameof(wo_Object.ObjectName)},
	source.{nameof(wo_Object.Image)},
	objectType.{nameof(wo_ObjectType.ObjectTypeID)}
	from (
		VALUES
        ('{nameof(DropEnum.vyaz_krit_dd)}',  '{DropEnum.vyaz_krit_dd}',  '{DropEnum.GetImage(nameof(DropEnum.vyaz_krit_dd))}',  '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.vyaz_krit_hil)}', '{DropEnum.vyaz_krit_hil}', '{DropEnum.GetImage(nameof(DropEnum.vyaz_krit_hil))}', '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.inj_fiz)}',       '{DropEnum.inj_fiz}',       '{DropEnum.GetImage(nameof(DropEnum.inj_fiz))}',       '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.inj_mag)}',       '{DropEnum.inj_mag}',       '{DropEnum.GetImage(nameof(DropEnum.inj_mag))}',       '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.ork)}',           '{DropEnum.ork}',           '{DropEnum.GetImage(nameof(DropEnum.ork))}',           '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.sprut)}',         '{DropEnum.sprut}',         '{DropEnum.GetImage(nameof(DropEnum.sprut))}',         '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.jija_db)}',       '{DropEnum.jija_db}',       '{DropEnum.GetImage(nameof(DropEnum.jija_db))}',       '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.jija_bb)}',       '{DropEnum.jija_bb}',       '{DropEnum.GetImage(nameof(DropEnum.jija_bb))}',       '{nameof(ObjectTypeEnum.Book)}')
       ,('{nameof(DropEnum.demon)}',         '{DropEnum.demon}',         '{DropEnum.GetImage(nameof(DropEnum.demon))}',         '{nameof(ObjectTypeEnum.Book)}')
	) as source ({nameof(wo_Object.ObjectCode)}, {nameof(wo_Object.ObjectName)}, {nameof(wo_Object.Image)}, {nameof(wo_ObjectType.ObjectTypeCode)})
	join {nameof(wo_ObjectType)} as objectType on source.{nameof(wo_ObjectType.ObjectTypeCode)} = objectType.{nameof(wo_ObjectType.ObjectTypeCode)}
) AS source ({nameof(wo_Object.ObjectCode)}, {nameof(wo_Object.ObjectName)}, {nameof(wo_Object.Image)}, {nameof(wo_Object.rf_ObjectTypeID)})
ON TARGET.{nameof(wo_Object.ObjectCode)} = source.{nameof(wo_Object.ObjectCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_Object.ObjectName)} != source.{nameof(wo_Object.ObjectName)} or
    TARGET.{nameof(wo_Object.rf_ObjectTypeID)}!= source.{nameof(wo_Object.rf_ObjectTypeID)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_Object.ObjectName)}= source.{nameof(wo_Object.ObjectName)},
        TARGET.{nameof(wo_Object.Image)}= source.{nameof(wo_Object.Image)},
        TARGET.{nameof(wo_Object.rf_ObjectTypeID)} = source.{nameof(wo_Object.rf_ObjectTypeID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Object.ObjectCode)}, {nameof(wo_Object.ObjectName)}, {nameof(wo_Object.Image)}, {nameof(wo_Object.rf_ObjectTypeID)})
    VALUES (source.{nameof(wo_Object.ObjectCode)}, source.{nameof(wo_Object.ObjectName)}, source.{nameof(wo_Object.Image)}, source.{nameof(wo_Object.rf_ObjectTypeID)});";
    }
}
