﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarspearOnlineApi.Data;

#nullable disable

namespace WarspearOnlineApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Drop", b =>
                {
                    b.Property<int>("DropID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DropID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DropID"));

                    b.Property<DateTime>("Drop_Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .HasColumnName("Drop_Date");

                    b.Property<int>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("Price");

                    b.Property<int>("rf_GroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_GroupID");

                    b.Property<int>("rf_ObjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_ObjectID");

                    b.HasKey("DropID");

                    b.HasIndex("rf_GroupID");

                    b.HasIndex("rf_ObjectID");

                    b.ToTable("wo_Drop", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_DropPlayer", b =>
                {
                    b.Property<int>("DropPlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DropPlayerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DropPlayerID"));

                    b.Property<int>("rf_DropID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_DropID");

                    b.Property<int>("rf_PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_PlayerID");

                    b.HasKey("DropPlayerID");

                    b.HasIndex("rf_DropID");

                    b.HasIndex("rf_PlayerID");

                    b.ToTable("wo_DropPlayer", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Fraction", b =>
                {
                    b.Property<int>("FractionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FractionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FractionID"));

                    b.Property<string>("FractionName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("FractionName");

                    b.HasKey("FractionID");

                    b.ToTable("wo_Fraction", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Group", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GroupID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupID"));

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("GroupName");

                    b.HasKey("GroupID");

                    b.ToTable("wo_Group", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_GroupGuild", b =>
                {
                    b.Property<int>("GroupGuildID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GroupGuildID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupGuildID"));

                    b.Property<int>("rf_GroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_GroupID");

                    b.Property<int>("rf_GuildID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_GuildID");

                    b.HasKey("GroupGuildID");

                    b.HasIndex("rf_GroupID");

                    b.HasIndex("rf_GuildID");

                    b.ToTable("wo_GroupGuild", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Guild", b =>
                {
                    b.Property<int>("GuildID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GuildID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GuildID"));

                    b.Property<string>("GuildName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("GuildName");

                    b.Property<int>("rf_FractionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_FractionID");

                    b.Property<int>("rf_ServerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_ServerID");

                    b.HasKey("GuildID");

                    b.HasIndex("rf_FractionID");

                    b.HasIndex("rf_ServerID");

                    b.ToTable("wo_Guild", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Object", b =>
                {
                    b.Property<int>("ObjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ObjectID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObjectID"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("Image");

                    b.Property<string>("ObjectName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("ObjectName");

                    b.Property<int>("rf_ObjectTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_ObjectTypeID");

                    b.HasKey("ObjectID");

                    b.HasIndex("rf_ObjectTypeID");

                    b.ToTable("wo_Object", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_ObjectType", b =>
                {
                    b.Property<int>("ObjectTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ObjectTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObjectTypeID"));

                    b.Property<string>("ObjectTypeName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("ObjectTypeName");

                    b.HasKey("ObjectTypeID");

                    b.ToTable("wo_ObjectType", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PlayerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerID"));

                    b.Property<string>("Nick")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("Nick");

                    b.Property<int>("rf_FractionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_FractionID");

                    b.Property<int>("rf_ServerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("rf_ServerID");

                    b.HasKey("PlayerID");

                    b.HasIndex("rf_FractionID");

                    b.HasIndex("rf_ServerID");

                    b.ToTable("wo_Player", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Server", b =>
                {
                    b.Property<int>("ServerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ServerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServerID"));

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("")
                        .HasColumnName("ServerName");

                    b.HasKey("ServerID");

                    b.ToTable("wo_Server", (string)null);
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Drop", b =>
                {
                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Group", "rf_Group")
                        .WithMany("Drops")
                        .HasForeignKey("rf_GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Object", "rf_Object")
                        .WithMany("Drops")
                        .HasForeignKey("rf_ObjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("rf_Group");

                    b.Navigation("rf_Object");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_DropPlayer", b =>
                {
                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Drop", "rf_Drop")
                        .WithMany("DropPlayers")
                        .HasForeignKey("rf_DropID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Player", "rf_Player")
                        .WithMany("DropPlayers")
                        .HasForeignKey("rf_PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("rf_Drop");

                    b.Navigation("rf_Player");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_GroupGuild", b =>
                {
                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Group", "rf_Group")
                        .WithMany("GroupGuilds")
                        .HasForeignKey("rf_GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Guild", "rf_Guild")
                        .WithMany("GroupGuilds")
                        .HasForeignKey("rf_GuildID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("rf_Group");

                    b.Navigation("rf_Guild");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Guild", b =>
                {
                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Fraction", "rf_Fraction")
                        .WithMany("Guilds")
                        .HasForeignKey("rf_FractionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Server", "rf_Server")
                        .WithMany("Guilds")
                        .HasForeignKey("rf_ServerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("rf_Fraction");

                    b.Navigation("rf_Server");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Object", b =>
                {
                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_ObjectType", "rf_ObjectType")
                        .WithMany("Objects")
                        .HasForeignKey("rf_ObjectTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("rf_ObjectType");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Player", b =>
                {
                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Fraction", "rf_Fraction")
                        .WithMany("Players")
                        .HasForeignKey("rf_FractionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarspearOnlineApi.Models.Entity.wo_Server", "rf_Server")
                        .WithMany("Players")
                        .HasForeignKey("rf_ServerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("rf_Fraction");

                    b.Navigation("rf_Server");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Drop", b =>
                {
                    b.Navigation("DropPlayers");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Fraction", b =>
                {
                    b.Navigation("Guilds");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Group", b =>
                {
                    b.Navigation("Drops");

                    b.Navigation("GroupGuilds");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Guild", b =>
                {
                    b.Navigation("GroupGuilds");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Object", b =>
                {
                    b.Navigation("Drops");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_ObjectType", b =>
                {
                    b.Navigation("Objects");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Player", b =>
                {
                    b.Navigation("DropPlayers");
                });

            modelBuilder.Entity("WarspearOnlineApi.Models.Entity.wo_Server", b =>
                {
                    b.Navigation("Guilds");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
