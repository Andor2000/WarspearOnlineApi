﻿using AutoMapper;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Mapper
{
    /// <summary>
    /// Конфигуратор моделей.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Конфигурация моделей.
        /// </summary>
        public MappingProfile()
        {
            // Общие.
            CreateMap<wo_Server, ServerDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.ServerID))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.ServerName));

            CreateMap<wo_Fraction, FractionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.FractionID))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.FractionName));

            CreateMap<wo_Class, ClassDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.ClassID))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.ClassName));

            CreateMap<wo_ObjectType, ObjectTypeDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.ObjectTypeID))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.ObjectTypeName));

            CreateMap<wo_Object, ObjectDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.ObjectID))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.ObjectName))
                .ForMember(x => x.Image, opt => opt.MapFrom(s => s.Image))
                .ForMember(x => x.ObjectType, opt => opt.MapFrom(s => s.rf_ObjectType));

            CreateMap<wo_Group, GroupDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.GroupID))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.GroupName))
                .ForMember(x => x.Server, opt => opt.MapFrom(s => s.rf_Server))
                .ForMember(x => x.Fraction, opt => opt.MapFrom(s => s.rf_Fraction));

            CreateMap<wo_Drop, DropDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.DropID))
                .ForMember(x => x.Date, opt => opt.MapFrom(s => s.Drop_Date))
                .ForMember(x => x.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(x => x.PlayersCount, opt => opt.Ignore())
                .ForMember(x => x.Part, opt => opt.Ignore())
                .ForMember(x => x.Object, opt => opt.MapFrom(s => s.rf_Object))
                .ForMember(x => x.Group, opt => opt.MapFrom(s => s.rf_Group))
                .ForMember(x => x.Server, opt => opt.MapFrom(s => s.rf_Server))
                .ForMember(x => x.Fraction, opt => opt.MapFrom(s => s.rf_Fraction));

            CreateMap<wo_Player, PlayerDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.PlayerID))
                .ForMember(x => x.Nick, opt => opt.MapFrom(s => s.Nick))
                .ForMember(x => x.Class, opt => opt.MapFrom(s => s.rf_Class))
                .ForMember(x => x.Fraction, opt => opt.MapFrom(s => s.rf_Fraction))
                .ForMember(x => x.Server, opt => opt.MapFrom(s => s.rf_Server));

            CreateMap<wo_DropPlayer, DropPlayerDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.DropPlayerID))
                .ForMember(x => x.Part, opt => opt.MapFrom(s => s.Part))
                .ForMember(x => x.IsPaid, opt => opt.MapFrom(s => s.IsPaid))
                .ForMember(x => x.Player, opt => opt.MapFrom(s => s.rf_Player));
        }
    }
}
