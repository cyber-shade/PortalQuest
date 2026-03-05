using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Persistence.Context;

public class PortalQuestDbContext : DbContext
{
    public PortalQuestDbContext(DbContextOptions<PortalQuestDbContext> options) : base(options)
    {
     
    }
	#region Core
    public DbSet<Class> Classes { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Duration> Durations { get; set; }
    public DbSet<Domain.Entities.Core.Range> Ranges { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Spell> Spells { get; set; }
	public DbSet<Time> Times { get; set; }
	public DbSet<Log> Logs { get; set; }
	#endregion
	#region OnModelCreating
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var entityTypes = modelBuilder.Model.GetEntityTypes()
			.Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity)) || t.ClrType == typeof(BaseEntity));
		foreach (var entityType in entityTypes)
		{
			var clrType = entityType.ClrType;

			modelBuilder.Entity(clrType)
				.HasKey(nameof(BaseEntity.Id));

			modelBuilder.Entity(clrType)
				.Property("Id")
				.ValueGeneratedNever()
				.IsRequired();
		}
		modelBuilder.Entity<BaseCoreEntity>()
			.Property(e => e.Content)
			.HasColumnType("jsonb");

		#region IsDeleted Global Query Filter
		modelBuilder.Entity<Class>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Condition>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Duration>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Domain.Entities.Core.Range>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Source>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Spell>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Time>().HasQueryFilter(x => !x.IsDeleted);
		#endregion
	}
	#endregion
}