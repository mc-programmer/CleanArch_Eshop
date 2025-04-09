using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Interfaces.Contexts;

public interface IDatabaseContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
}