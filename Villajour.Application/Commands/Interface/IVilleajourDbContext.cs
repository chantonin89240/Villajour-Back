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
        DbSet<UserEntity> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
