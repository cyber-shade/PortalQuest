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
    public DbSet<Effect> Effects { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Spell> Spells { get; set; }
	public DbSet<Log> Logs { get; set; }
	#endregion
	#region OnModelCreating
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Ignore<BaseEntity>();
		var entityTypes = modelBuilder.Model.GetEntityTypes()
			.Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity)));
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
		#region Spell owned value objects
		modelBuilder.Entity<Spell>(spell =>
		{
			spell.OwnsOne(s => s.Range, r =>
			{
				r.Property(x => x.Type).HasColumnName("Range_Type");
				r.Property(x => x.DistanceType).HasColumnName("Range_DistanceType");
				r.Property(x => x.Amount).HasColumnName("Range_Amount");
			});

			spell.OwnsMany(s => s.Durations, d =>
			{
				d.ToTable("SpellDurations");
				d.WithOwner().HasForeignKey("SpellId");
				d.Property<Guid>("Id");
				d.HasKey("Id");

				d.OwnsOne(x => x.Time);

				d.Property(x => x.Ends)
					.HasConversion(
						v => string.Join(',', v),
						v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
			});

			spell.OwnsMany(s => s.CastingTimes, t =>
			{
				t.ToTable("SpellCastingTimes");
				t.WithOwner().HasForeignKey("SpellId");
				t.Property<Guid>("Id");
				t.HasKey("Id");
			});
		});
		#endregion

		#region IsDeleted Global Query Filter
		modelBuilder.Entity<Class>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Effect>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Book>().HasQueryFilter(x => !x.IsDeleted);
		modelBuilder.Entity<Spell>().HasQueryFilter(x => !x.IsDeleted);
		#endregion
	}
	#endregion
	#region SaveChanges
	// specify DateTimeKind UTC
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries()
			.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
		{
			var dateTimeProperties = entry.Entity.GetType().GetProperties()
				.Where(p => p.PropertyType == typeof(DateTime) ||
							p.PropertyType == typeof(DateTime?));

			foreach (var property in dateTimeProperties)
			{
				var currentValue = (DateTime?)property.GetValue(entry.Entity);

				if (currentValue.HasValue)
				{
					if (currentValue.Value.Kind == DateTimeKind.Unspecified ||
						currentValue.Value.Kind == DateTimeKind.Local)
					{
						property.SetValue(entry.Entity, currentValue.Value.ToUniversalTime());
					}
				}
			}
		}
		return base.SaveChangesAsync(cancellationToken);
	}
	#endregion
}