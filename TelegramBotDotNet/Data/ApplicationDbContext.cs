using Microsoft.EntityFrameworkCore;
using TelegramBotDotNet.Data;
using TelegramBotDotNet.Entities;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<CityEntity> CityEntities { get; set; }
    public DbSet<ChatToCityEntity> ChatToCityEntities { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CityEntity>()
            .Property(t => t.City)
            .IsRequired();
        
        modelBuilder.Entity<ChatToCityEntity>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.HasSequence<long>("chat_to_city_id_seq")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.Entity<ChatToCityEntity>()
            .Property(e => e.Id)
            .HasDefaultValueSql("nextval('chat_to_city_id_seq')");
    }
}