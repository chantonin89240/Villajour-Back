using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Persistence;

public class VillajourDbContext : DbContext, IVillajourDbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<MairieEntity> Mairies => Set<MairieEntity>();
    public DbSet<ScheduleMairieEntity> ScheduleMairies => Set<ScheduleMairieEntity>();
    public DbSet<AnnouncementEntity> Announcements => Set<AnnouncementEntity>();
    public DbSet<AnnouncementTypeEntity> AnnouncementTypes => Set<AnnouncementTypeEntity>();
    public DbSet<AppointmentEntity> Appointments => Set<AppointmentEntity>();
    public DbSet<AppointmentTypeEntity> AppointmentTypes => Set<AppointmentTypeEntity>();
    public DbSet<ContactEntity> Contacts => Set<ContactEntity>();
    public DbSet<ContactTypeEntity> ContactTypes => Set<ContactTypeEntity>();
    public DbSet<DocumentEntity> Documents => Set<DocumentEntity>();
    public DbSet<DocumentTypeEntity> DocumentTypes => Set<DocumentTypeEntity>();
    public DbSet<EventEntity> Events => Set<EventEntity>();
    public DbSet<EventTypeEntity> EventTypes => Set<EventTypeEntity>();
    public DbSet<FavoriteContentEntity> FavoritesContent => Set<FavoriteContentEntity>();
    public DbSet<FavoriteMairieEntity> FavoritesMairie => Set<FavoriteMairieEntity>();


    public VillajourDbContext(DbContextOptions<VillajourDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // configuration de Users
        builder.Entity<UserEntity>()
            .HasKey(i => i.Id);

        // configuration de Mairies
        builder.Entity<MairieEntity>()
            .HasKey(i => i.Id);

        // configuration de ScheduleMairies
        builder.Entity<ScheduleMairieEntity>()
            .HasOne<MairieEntity>()
            .WithMany()
            .HasForeignKey(p => p.MairieId);



        // configuration de AnnouncementType
        builder.Entity<AnnouncementTypeEntity>()
            .HasKey(i => i.Id);

        // configuration de Announcement
        builder.Entity<AnnouncementEntity>()
            .HasOne<MairieEntity>()
            .WithMany()
            .HasForeignKey(p => p.MairieId);

        builder.Entity<AnnouncementEntity>()
            .HasOne<AnnouncementTypeEntity>()
            .WithMany()
            .HasForeignKey(p => p.AnnouncementTypeId);



        // configuration de AppointmentType
        builder.Entity<AppointmentTypeEntity>()
            .HasKey(i => i.Id);

        // configuration de Appointment
        builder.Entity<AppointmentEntity>()
            .HasOne<MairieEntity>()
            .WithMany()
            .HasForeignKey(p => p.MairieId);

        builder.Entity<AppointmentEntity>()
              .HasOne<UserEntity>()
              .WithMany()
              .HasForeignKey(p => p.UserId);

        builder.Entity<AppointmentEntity>()
            .HasOne<AppointmentTypeEntity>()
            .WithMany()
            .HasForeignKey(p => p.AppointmentTypeId);



        // configuration de ContactType
        builder.Entity<ContactTypeEntity>()
            .HasKey(i => i.Id);

        // configuration de Contact
        builder.Entity<ContactEntity>()
            .HasOne<MairieEntity>()
            .WithMany()
            .HasForeignKey(p => p.MairieId);

        builder.Entity<ContactEntity>()
              .HasOne<UserEntity>()
              .WithMany()
              .HasForeignKey(p => p.UserId);

        builder.Entity<ContactEntity>()
            .HasOne<ContactTypeEntity>()
            .WithMany()
            .HasForeignKey(p => p.ContactTypeId);



        // configuration de DocumentType
        builder.Entity<DocumentTypeEntity>()
            .HasKey(i => i.Id);

        // configuration de Document
        builder.Entity<DocumentEntity>()
            .HasOne<MairieEntity>()
            .WithMany()
            .HasForeignKey(p => p.MairieId);

        builder.Entity<DocumentEntity>()
            .HasOne<DocumentTypeEntity>()
            .WithMany()
            .HasForeignKey(p => p.DocumentTypeId);



        // configuration de EventType
        builder.Entity<EventTypeEntity>()
            .HasKey(i => i.Id);

        // configuration de Event
        builder.Entity<EventEntity>()
            .HasOne<MairieEntity>()
            .WithMany()
            .HasForeignKey(p => p.MairieId);

        builder.Entity<EventEntity>()
            .HasOne<EventTypeEntity>()
            .WithMany()
            .HasForeignKey(p => p.EventTypeId);



        // configuration de FavoriteMairie
        builder.Entity<FavoriteMairieEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.Entity<FavoriteMairieEntity>()
           .HasOne<MairieEntity>()
           .WithMany()
           .HasForeignKey(p => p.MairieId);



        // configuration de FavoriteContent
        builder.Entity<FavoriteContentEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.Entity<FavoriteContentEntity>()
            .HasOne<AnnouncementEntity>()
            .WithMany()
            .HasForeignKey(p => p.AnnouncementId);

        builder.Entity<FavoriteContentEntity>()
            .HasOne<EventEntity>()
            .WithMany()
            .HasForeignKey(p => p.EventId);

        builder.Entity<FavoriteContentEntity>()
            .HasOne<DocumentEntity>()
            .WithMany()
            .HasForeignKey(p => p.DocumentId);


        base.OnModelCreating(builder);
    }
}
