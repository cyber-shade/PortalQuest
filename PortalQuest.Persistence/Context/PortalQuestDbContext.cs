using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PortalQuest.Domain.Entities.Common;
using PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Persistence.Context;

public class PortalQuestDbContext : DbContext
{
    public PortalQuestDbContext(DbContextOptions<PortalQuestDbContext> options) : base(options)
    {
     
    }
	#region Core
    public DbSet<CastingTime>  CastingTime { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Duration> Durations { get; set; } 
    public DbSet<Effect> Effects { get; set; }
    public DbSet<Domain.Entities.Core.Range> Ranges { get; set; }
    public DbSet<Spell> Spells { get; set; }
    public DbSet<Tag> Tags { get; set; }
	#endregion
	#region OnModelCreating
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var entityTypes = modelBuilder.Model.GetEntityTypes()
	   .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity)) || t.ClrType == typeof(BaseEntity));
		foreach (var entityType in entityTypes)
		{
			modelBuilder.Entity(entityType.ClrType)
				.Property("Id")
				.ValueGeneratedNever()
				.IsRequired();
		}
		modelBuilder.Entity<Spell>()
			.Property(e => e.DamageDices)
			.HasColumnType("jsonb")
			.HasConversion(
				v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
				v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null))
			.Metadata.SetValueComparer(new ValueComparer<Dictionary<string, int>>(
				(c1, c2) => c1.SequenceEqual(c2),               // Compare dictionaries
				c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Hash code
				c => new Dictionary<string, int>(c)));          // Deep copy for snapshotting

		#region IsDeleted Global Query Filter
		modelBuilder.Entity<CastingTime>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Class>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Component>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Condition>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<DamageType>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Duration>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Effect>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Domain.Entities.Core.Range>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Spell>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Tag>().HasQueryFilter(x => !x.IsDeleted);
		#endregion
	}
	#endregion
}