using Microsoft.EntityFrameworkCore;
using PortalQuest.Domain.Entities.Core;

namespace PortalQuest.Persistence.Context;

public class PortalQuestDbContext : DbContext
{
    public PortalQuestDbContext(DbContextOptions<PortalQuestDbContext> options) : base(options)
    {
     
    }
	#region Core
	public DbSet<AbilityScore> AbilityScores { get; set; }
    public DbSet<AttackType> AttackTypes { get; set; }
    public DbSet<CastingTime>  CastingTime { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<DamageType> DamageTypes { get; set; }
    public DbSet<Duration> Durations { get; set; } 
    public DbSet<Effect> Effects { get; set; }
    public DbSet<MagicSchool> MagicSchools { get; set; }
    public DbSet<Domain.Entities.Core.Range> Ranges { get; set; }
    public DbSet<Spell> Spells { get; set; }
    public DbSet<Tag> Tags { get; set; }
	#endregion
}