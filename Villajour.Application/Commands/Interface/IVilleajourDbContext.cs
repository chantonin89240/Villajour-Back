using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.Interface
{
    public interface IVilleajourDbContext
    {
        DbSet<AnnouncementEntity> Announcements { get; }
        DbSet<AnnouncementTypeEntity> AnnouncementTypes { get; }
        DbSet<AppointmentEntity> Appointments { get; }
        DbSet<AppointmentTypeEntity> AppointmentTypes { get; }
        DbSet<ContactEntity> Contacts { get; }
        DbSet<ContactTypeEntity> ContactTypes { get; }
        DbSet<DocumentEntity> Documents { get; }
        DbSet<DocumentTypeEntity> DocumentTypes { get; }
        DbSet<EventEntity> Events { get; }
        DbSet<EventTypeEntity> EventTypes { get; }
        DbSet<FavoriteContentEntity> FavoritesContent { get; }
        DbSet<FavoriteMairieEntity> FavoritesMairie { get; }
        DbSet<MairieEntity> Mairies { get; }
        DbSet<ScheduleMairieEntity> ScheduleMairies { get; }
        DbSet<UserEntity> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
