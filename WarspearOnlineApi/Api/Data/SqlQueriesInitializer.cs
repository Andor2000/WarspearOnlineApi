using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Models.Entity.Intersections;
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
	    ('{nameof(AccessLevelEnum.Moderator)}', '{AccessLevelEnum.Moderator}', '{AccessLevelEnum.LevelValue(nameof(AccessLevelEnum.Moderator))}')
	   ,('{nameof(AccessLevelEnum.Admin)}', '{AccessLevelEnum.Admin}', '{AccessLevelEnum.LevelValue(nameof(AccessLevelEnum.Admin))}')
	   ,('{nameof(AccessLevelEnum.AdminServer)}', '{AccessLevelEnum.AdminServer}', '{AccessLevelEnum.LevelValue(nameof(AccessLevelEnum.AdminServer))}')
	   ,('{nameof(AccessLevelEnum.MainAdmin)}', '{AccessLevelEnum.MainAdmin}', '{AccessLevelEnum.LevelValue(nameof(AccessLevelEnum.MainAdmin))}')
) as source ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_AccessLevel.AccessLevelName)}, {nameof(wo_AccessLevel.AccessLevelInt)})
on TARGET.{nameof(wo_AccessLevel.AccessLevelCode)} = source.{nameof(wo_AccessLevel.AccessLevelCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_AccessLevel.AccessLevelName)} != source.{nameof(wo_AccessLevel.AccessLevelName)} or
    TARGET.{nameof(wo_AccessLevel.AccessLevelInt)} != source.{nameof(wo_AccessLevel.AccessLevelInt)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_AccessLevel.AccessLevelName)} = source.{nameof(wo_AccessLevel.AccessLevelName)},
        TARGET.{nameof(wo_AccessLevel.AccessLevelInt)} = source.{nameof(wo_AccessLevel.AccessLevelInt)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_AccessLevel.AccessLevelName)}, {nameof(wo_AccessLevel.AccessLevelInt)})
    VALUES (source.{nameof(wo_AccessLevel.AccessLevelCode)}, source.{nameof(wo_AccessLevel.AccessLevelName)}, source.{nameof(wo_AccessLevel.AccessLevelInt)});


UPDATE record
SET {nameof(wo_AccessLevel.rf_ParentAccessLevelID)} = parent.{nameof(wo_AccessLevel.AccessLevelID)}
FROM (
    VALUES
        ('{nameof(AccessLevelEnum.Moderator)}', ''),
        ('{nameof(AccessLevelEnum.Admin)}', '{nameof(AccessLevelEnum.Moderator)}'),
        ('{nameof(AccessLevelEnum.AdminServer)}', '{nameof(AccessLevelEnum.Admin)}'),
        ('{nameof(AccessLevelEnum.MainAdmin)}', '{nameof(AccessLevelEnum.AdminServer)}')
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
       ,('{nameof(RoleEnum.AddUser)}', '{RoleEnum.AddUser}')
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
	al.{nameof(wo_AccessLevel.AccessLevelID)},
    role.{nameof(wo_Role.RoleID)}	
	from (
		VALUES
             ('{nameof(AccessLevelEnum.Moderator)}', '{nameof(RoleEnum.AddDeletePlayerInDrop)}')
            ,('{nameof(AccessLevelEnum.Moderator)}', '{nameof(RoleEnum.AddDrop)}')

            ,('{nameof(AccessLevelEnum.Admin)}', '{nameof(RoleEnum.AddUser)}')
            ,('{nameof(AccessLevelEnum.Admin)}', '{nameof(RoleEnum.DeleteDrop)}')

            ,('{nameof(AccessLevelEnum.AdminServer)}', '{nameof(RoleEnum.AddGuild)}')
            ,('{nameof(AccessLevelEnum.AdminServer)}', '{nameof(RoleEnum.AddDeleteGroup)}')

            ,('{nameof(AccessLevelEnum.MainAdmin)}', '{nameof(RoleEnum.AddObject)}')
            ,('{nameof(AccessLevelEnum.MainAdmin)}', '{nameof(RoleEnum.AddClass)}')

	) as source ({nameof(wo_AccessLevel.AccessLevelCode)}, {nameof(wo_Role.RoleCode)})
	join {nameof(wo_AccessLevel)} as al on source.{nameof(wo_AccessLevel.AccessLevelCode)} = al.{nameof(wo_AccessLevel.AccessLevelCode)}
	join {nameof(wo_Role)} as role on source.{nameof(wo_Role.RoleCode)} = role.{nameof(wo_Role.RoleCode)}
) AS source ({nameof(wo_AccessLevelRole.rf_AccessLevelID)}, {nameof(wo_AccessLevelRole.rf_RoleID)})
ON TARGET.{nameof(wo_AccessLevelRole.rf_AccessLevelID)} = source.{nameof(wo_AccessLevelRole.rf_AccessLevelID)}
    and TARGET.{nameof(wo_AccessLevelRole.rf_RoleID)} = source.{nameof(wo_AccessLevelRole.rf_RoleID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_AccessLevelRole.rf_AccessLevelID)}, {nameof(wo_AccessLevelRole.rf_RoleID)})
    VALUES (source.{nameof(wo_AccessLevelRole.rf_AccessLevelID)}, source.{nameof(wo_AccessLevelRole.rf_RoleID)});




MERGE {nameof(wo_Server)} AS TARGET
USING (
    VALUES
        ('{nameof(ServerEnum.RU_Topaz)}', '{ServerEnum.RU_Topaz}')
       ,('{nameof(ServerEnum.RU_Amber)}', '{ServerEnum.RU_Amber}')
       ,('{nameof(ServerEnum.RU_Ruby)}', '{ServerEnum.RU_Ruby}')
) AS source ({nameof(wo_Server.ServerCode)}, {nameof(wo_Server.ServerName)})
ON TARGET.{nameof(wo_Server.ServerCode)} = source.{nameof(wo_Server.ServerCode)}
WHEN MATCHED and
   (TARGET.{nameof(wo_Server.ServerName)} != source.{nameof(wo_Server.ServerName)})
THEN
    UPDATE SET
        TARGET.{nameof(wo_Server.ServerName)} = source.{nameof(wo_Server.ServerName)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Server.ServerCode)}, {nameof(wo_Server.ServerName)})
    VALUES (source.{nameof(wo_Server.ServerCode)}, source.{nameof(wo_Server.ServerName)});

MERGE {nameof(wo_Class)} AS TARGET
USING (
    select
    f.{nameof(wo_Fraction.FractionID)},
    source.{nameof(wo_Class.ClassCode)},
    source.{nameof(wo_Class.ClassName)}
    from (
    VALUES
        ('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Druid)}', '{ClassEnum.Druid}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.StrikingBlade)}', '{ClassEnum.StrikingBlade}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Ranger)}', '{ClassEnum.Ranger}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Guardian)}', '{ClassEnum.Guardian}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Hunter)}', '{ClassEnum.Hunter}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Paladin)}', '{ClassEnum.Paladin}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Priest)}', '{ClassEnum.Priest}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Mage)}', '{ClassEnum.Mage}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Seeker)}', '{ClassEnum.Seeker}')
       ,('{nameof(FractionEnum.Guardian)}', '{nameof(ClassEnum.Templar)}', '{ClassEnum.Templar}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Barbarian)}', '{ClassEnum.Barbarian}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Rogue)}', '{ClassEnum.Rogue}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Shaman)}', '{ClassEnum.Shaman}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Archer)}', '{ClassEnum.Archer}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Chieftain)}', '{ClassEnum.Chieftain}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Necromancer)}', '{ClassEnum.Necromancer}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Warlock)}', '{ClassEnum.Warlock}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.DeathKnight)}', '{ClassEnum.DeathKnight}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Spellcaster)}', '{ClassEnum.Spellcaster}')
       ,('{nameof(FractionEnum.Legion)}', '{nameof(ClassEnum.Reaper)}', '{ClassEnum.Reaper}')
    ) as source ({nameof(wo_Fraction.FractionCode)}, {nameof(wo_Class.ClassCode)}, {nameof(wo_Class.ClassName)})
    join {nameof(wo_Fraction)} as f on f.{nameof(wo_Fraction.FractionCode)} = source.{nameof(wo_Fraction.FractionCode)}
) AS source ({nameof(wo_Class.rf_FractionID)}, {nameof(wo_Class.ClassCode)}, {nameof(wo_Class.ClassName)})
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
        TARGET.{nameof(wo_Object.ObjectName)} = source.{nameof(wo_Object.ObjectName)},
        TARGET.{nameof(wo_Object.Image)} = source.{nameof(wo_Object.Image)},
        TARGET.{nameof(wo_Object.rf_ObjectTypeID)} = source.{nameof(wo_Object.rf_ObjectTypeID)}
WHEN NOT MATCHED THEN
    INSERT ({nameof(wo_Object.ObjectCode)}, {nameof(wo_Object.ObjectName)}, {nameof(wo_Object.Image)}, {nameof(wo_Object.rf_ObjectTypeID)})
    VALUES (source.{nameof(wo_Object.ObjectCode)}, source.{nameof(wo_Object.ObjectName)}, source.{nameof(wo_Object.Image)}, source.{nameof(wo_Object.rf_ObjectTypeID)});







MERGE wo_User AS TARGET
USING (
	select
	source.Login,
	source.Password,
	source.RangeAccessLevel,
	al.AccessLevelID,
	source.UserName,
	s.ServerID,
	f.FractionID
	from (
    VALUES
        ('admin', 'admin', '0', 'MainAdmin', 'Андрей', 'RU_Ruby', 'Guardian')
	) AS source (Login, Password, RangeAccessLevel, AccessLevelCode, UserName, ServerCode, FractionCode)
	join wo_AccessLevel as al on source.AccessLevelCode = al.AccessLevelCode
	join wo_Server as s on source.ServerCode = s.ServerCode
	join wo_Fraction as f on source.FractionCode = f.FractionCode
) AS source (Login, Password, RangeAccessLevel, rf_AccessLevelID, UserName, rf_ServerID, rf_FractionID)
ON TARGET.Login = source.Login
WHEN MATCHED and
   (TARGET.Password != source.Password or
    TARGET.RangeAccessLevel != source.RangeAccessLevel or
    TARGET.rf_AccessLevelID != source.rf_AccessLevelID or
    TARGET.UserName != source.UserName or
    TARGET.rf_ServerID != source.rf_ServerID or
    TARGET.rf_FractionID != source.rf_FractionID)
THEN
    UPDATE SET
        TARGET.Password = source.Password,
        TARGET.RangeAccessLevel = source.RangeAccessLevel,
        TARGET.rf_AccessLevelID = source.rf_AccessLevelID,
        TARGET.UserName = source.UserName,
        TARGET.rf_ServerID = source.rf_ServerID,
        TARGET.rf_FractionID = source.rf_FractionID
WHEN NOT MATCHED THEN
    INSERT (Login, Password, RangeAccessLevel, rf_AccessLevelID, UserName, rf_ServerID, rf_FractionID)
    VALUES (source.Login, source.Password, source.RangeAccessLevel, source.rf_AccessLevelID, source.UserName, source.rf_ServerID, source.rf_FractionID);

MERGE wo_Group AS TARGET
USING (
	select
	source.GroupName,
	s.ServerID,
	f.FractionID
	from (
    VALUES
        ('Альянс', 'RU_Ruby', 'Guardian')
	) AS source (GroupName, ServerCode, FractionCode)
	join wo_Server as s on source.ServerCode = s.ServerCode
	join wo_Fraction as f on source.FractionCode = f.FractionCode
) AS source (GroupName, rf_ServerID, rf_FractionID)
ON TARGET.GroupName = source.GroupName
WHEN MATCHED and
   (TARGET.rf_ServerID != source.rf_ServerID or
    TARGET.rf_FractionID != source.rf_FractionID)
THEN
    UPDATE SET
        TARGET.rf_ServerID = source.rf_ServerID,
        TARGET.rf_FractionID = source.rf_FractionID
WHEN NOT MATCHED THEN
    INSERT (GroupName, rf_ServerID, rf_FractionID)
    VALUES (source.GroupName, source.rf_ServerID, source.rf_FractionID);



MERGE wo_Drop AS TARGET
USING (
	select
	source.Price,
	o.ObjectID,
	g.GroupID
	from (
    VALUES
        ('7500000', 'vyaz_krit_dd', 'Альянс')
	) AS source (Price, ObjectCode, GroupName)
	join wo_Object as o on source.objectCode = o.objectCode
	join wo_Group as g on source.GroupName = g.GroupName
) AS source (Price, rf_ObjectID, rf_GroupID)
ON TARGET.Price = source.Price
WHEN MATCHED and
   (TARGET.rf_ObjectID != source.rf_ObjectID or
    TARGET.rf_GroupID != source.rf_GroupID)
THEN
    UPDATE SET
        TARGET.rf_ObjectID = source.rf_ObjectID,
        TARGET.rf_GroupID = source.rf_GroupID
WHEN NOT MATCHED THEN
    INSERT (Price, Drop_Date, rf_ObjectID, rf_GroupID)
    VALUES (source.Price, getdate(), source.rf_ObjectID, source.rf_GroupID);


MERGE wo_Player AS TARGET
USING (
	select
	source.Nick,
	s.ServerID,
	f.FractionID,
	c.ClassID
	from (
    VALUES
        ('Karkarich', 'RU_Ruby', 'Guardian', 'Seeker')
	) AS source (Nick, ServerCode, FractionCode, ClassCode)
	join wo_Server as s on source.ServerCode = s.ServerCode
	join wo_Fraction as f on source.FractionCode = f.FractionCode
	join wo_Class as c on c.ClassCode = source.ClassCode
) AS source (Nick, rf_ServerID, rf_FractionID, rf_ClassID)
ON TARGET.Nick = source.Nick
WHEN MATCHED and
   (TARGET.rf_ServerID != source.rf_ServerID or
    TARGET.rf_FractionID != source.rf_FractionID or
	TARGET.rf_ClassID != source.rf_ClassID)
THEN
    UPDATE SET
        TARGET.rf_ServerID = source.rf_ServerID,
        TARGET.rf_FractionID = source.rf_FractionID,
        TARGET.rf_ClassID = source.rf_ClassID
WHEN NOT MATCHED THEN
    INSERT (Nick, rf_ServerID, rf_FractionID, rf_ClassID)
    VALUES (source.Nick, source.rf_ServerID, source.rf_FractionID, source.rf_ClassID);


MERGE wo_DropPlayer AS TARGET
USING (
	select
	'1',
	'0',
	(select top(1) DropID from wo_Drop where DropID > 0),
	(select PlayerID from wo_Player where Nick = 'Karkarich')
) AS source (Part, IsPaid, rf_DropID, rf_PlayerID)
ON TARGET.rf_DropID = source.rf_DropID and TARGET.rf_PlayerID = source.rf_PlayerID
WHEN MATCHED and
   (TARGET.Part != source.Part or
    TARGET.IsPaid != source.IsPaid)
THEN
    UPDATE SET
        TARGET.Part = source.Part,
        TARGET.IsPaid = source.IsPaid
WHEN NOT MATCHED THEN
    INSERT (Part, IsPaid, rf_DropID, rf_PlayerID)
    VALUES (source.Part, source.IsPaid, source.rf_DropID, source.rf_PlayerID);
";
    }
}
