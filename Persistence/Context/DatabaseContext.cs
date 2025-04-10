using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class DatabaseContext (DbContextOptions<DatabaseContext> options): DbContext(options),IDatabaseContext
{
    #region Fluent api

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<User>().Property<DateTime?>("CreateDate");
        //modelBuilder.Entity<User>().Property<DateTime?>("UpdateDate");

        foreach(var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if(entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
            {
                modelBuilder.Entity<User>().Property<DateTime?>("CreateDate");
                modelBuilder.Entity<User>().Property<DateTime?>("UpdateDate");
                modelBuilder.Entity<User>().Property<DateTime?>("DeleteDate");
                modelBuilder.Entity<User>().Property<bool>("IsDeleted");
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var modifierEntries = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Modified ||
            p.State == EntityState.Added ||
            p.State == EntityState.Deleted);

        foreach(var item in modifierEntries)
        {
            var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

            var createDate = entityType?.FindProperty("CreateDate");
            var updateDate = entityType?.FindProperty("UpdateDate");
            var deleteDate = entityType?.FindProperty("DeleteDate");
            var isDeleted = entityType?.FindProperty("IsDeleted");

            if(item.State == EntityState.Added && createDate != null)
            {
                item.Property("CreateDate").CurrentValue = DateTime.Now;
            }

            if (item.State == EntityState.Modified && updateDate != null)
            {
                item.Property("UpdateDate").CurrentValue = DateTime.Now;
            }

            if (item.State == EntityState.Deleted && deleteDate != null && isDeleted != null)
            {
                item.Property("deleteDate").CurrentValue = DateTime.Now;
                item.Property("isDeleted").CurrentValue = true;
            }
        }

        return base.SaveChanges();
    }

    #endregion

    #region Dbsets

    #endregion
}