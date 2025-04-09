using Application.Interfaces.Contexts;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class DatabaseContext (DbContextOptions<DatabaseContext> options): DbContext(options),IDatabaseContext
{
    #region Dbsets

    public DbSet<User> Users { get; set; }

    #endregion
}