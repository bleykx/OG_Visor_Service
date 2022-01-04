using OG_Visor_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace OG_Visor_Service
{
    public class AppDbContext : DbContext
    {
        public DbSet<Farm> Farms { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Part> Parts{ get; set; }
        public DbSet<Rig> Rigs{ get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        //public DbSet<HiveIdentifier> HiveIdentifiers{ get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var FarmEntity = modelBuilder.Entity<Farm>();
            FarmEntity.HasKey(k => k.Id);
            FarmEntity.Property(p => p.Name).IsRequired();
            FarmEntity.HasMany(m => m.Rigs);

            var RigEntity = modelBuilder.Entity<Rig>();
            RigEntity.HasKey(k => k.Id);
            RigEntity.Property(p => p.Name).IsRequired();
            RigEntity.Property(p => p.Online).IsRequired();
            RigEntity.Property(p => p.IPv4).IsRequired();
            RigEntity.HasOne(o => o.Farm);
            RigEntity.HasMany(m => m.GPUs);


            RigEntity.HasOne(o => o.Farm);
            RigEntity.HasMany(m => m.GPUs);

            var GPUEntity = modelBuilder.Entity<GPU>();
            GPUEntity.HasKey(k => k.Id);
            GPUEntity.Property(p => p.Name).IsRequired();
            GPUEntity.Property(p => p.Brand).IsRequired();
            GPUEntity.Property(p => p.Model).IsRequired();
            GPUEntity.Property(p => p.Online).IsRequired();
            GPUEntity.HasOne(o => o.Rig);
            GPUEntity.HasMany(m => m.Parts);

            var OwnerEntity = modelBuilder.Entity<Owner>();
            OwnerEntity.HasKey(k => k.Id);
            OwnerEntity.Property(p => p.Name).IsRequired();
            OwnerEntity.HasMany(m => m.Wallets);

            var WalletEntity = modelBuilder.Entity<Wallet>();
            WalletEntity.HasKey(k => k.Id);
            WalletEntity.Property(p => p.Coin).IsRequired();
            WalletEntity.HasOne(o => o.Owner);

            var PartEntity = modelBuilder.Entity<Part>();
            PartEntity.HasKey(k => k.Id);
            PartEntity.Property(p => p.PercentPart).IsRequired();
            PartEntity.HasOne(o => o.GPU);
            PartEntity.HasOne(o => o.Owner);

            //var HiveIdentifierEntity = modelBuilder.Entity<HiveIdentifier>();
            //HiveIdentifierEntity.HasKey(k => k.Id);
            //HiveIdentifierEntity.Property(p => p.Key).IsRequired();
            //HiveIdentifierEntity.Property(p => p.Key).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    
    }
}
 