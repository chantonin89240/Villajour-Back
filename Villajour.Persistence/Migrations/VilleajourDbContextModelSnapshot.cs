﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Villajour.Persistence;

#nullable disable

namespace Villajour.Persistence.Migrations
{
    [DbContext(typeof(VilleajourDbContext))]
    partial class VilleajourDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Villajour.Domain.Common.AnnouncementEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnnouncementTypeId")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(5);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementTypeId");

                    b.HasIndex("MairieId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("Villajour.Domain.Common.AnnouncementTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("AnnouncementTypes");
                });

            modelBuilder.Entity("Villajour.Domain.Common.AppointmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentTypeId")
                        .HasColumnType("int")
                        .HasColumnOrder(6);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(2);

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(7);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(5);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(8);

                    b.HasKey("Id");

                    b.HasIndex("AppointmentTypeId");

                    b.HasIndex("MairieId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Villajour.Domain.Common.AppointmentTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("AppointmentTypes");
                });

            modelBuilder.Entity("Villajour.Domain.Common.ContactEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactTypeId")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(5);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(6);

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("MairieId");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Villajour.Domain.Common.ContactTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("ContactTypes");
                });

            modelBuilder.Entity("Villajour.Domain.Common.DocumentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<byte[]>("Document")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnOrder(4);

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int")
                        .HasColumnOrder(5);

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(6);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("MairieId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Villajour.Domain.Common.DocumentTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("Villajour.Domain.Common.EventEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(2);

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int")
                        .HasColumnOrder(6);

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(7);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("MairieId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Villajour.Domain.Common.EventTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("Villajour.Domain.Common.FavoriteContentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnnouncementId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int?>("DocumentId")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<int?>("EventId")
                        .HasColumnType("int")
                        .HasColumnOrder(3);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoritesContent");
                });

            modelBuilder.Entity("Villajour.Domain.Common.FavoriteMairieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.HasIndex("MairieId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoritesMairie");
                });

            modelBuilder.Entity("Villajour.Domain.Common.MairieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<string>("Siret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.ToTable("Mairies");
                });

            modelBuilder.Entity("Villajour.Domain.Common.ScheduleMairieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnOrder(1);

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time")
                        .HasColumnOrder(3);

                    b.Property<Guid>("MairieId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(4);

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("MairieId");

                    b.ToTable("ScheduleMairies");
                });

            modelBuilder.Entity("Villajour.Domain.Common.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(1);

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Villajour.Domain.Common.AnnouncementEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.AnnouncementTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("AnnouncementTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.AppointmentEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.AppointmentTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("AppointmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.ContactEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.ContactTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("ContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.DocumentEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.DocumentTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.EventEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.EventTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.FavoriteContentEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.AnnouncementEntity", null)
                        .WithMany()
                        .HasForeignKey("AnnouncementId");

                    b.HasOne("Villajour.Domain.Common.DocumentEntity", null)
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.HasOne("Villajour.Domain.Common.EventEntity", null)
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("Villajour.Domain.Common.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.FavoriteMairieEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Villajour.Domain.Common.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Villajour.Domain.Common.ScheduleMairieEntity", b =>
                {
                    b.HasOne("Villajour.Domain.Common.MairieEntity", null)
                        .WithMany()
                        .HasForeignKey("MairieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
