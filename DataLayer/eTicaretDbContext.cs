using DataLayer.Models;
using HelperLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
namespace DataLayer
{
    public class eTicaretDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=eTicaret;Integrated Security=True;Connection Timeout=30");
            optionsBuilder.UseSqlServer(Consts.ConnectionString);
        }

        public void MiggrateDb()
        {
            this.Database.Migrate();
        }

        public DbSet<Cancel> Cancel { get; set; }
        public DbSet<UserRole> UserRol { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }

        public User CurrentUser { get; set; }
        public int CurrentUserID { get { return CurrentUser.ID; } }

        public int CurrentCustomerID { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity("DataLayer.Models.User", b =>
            {
                b.HasOne("DataLayer.Models.User", "CreatedUser")
                    .WithMany()
                    .HasForeignKey("CreatedBy")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity("DataLayer.Models.User", b =>
            {
                b.HasOne("DataLayer.Models.User", "UpdatedUser")
                    .WithMany()
                    .HasForeignKey("UpdatedBy")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity("DataLayer.Models.User", b =>
            {
                b.HasOne("DataLayer.Models.User", "CanceledUser")
                    .WithMany()
                    .HasForeignKey("CanceledBy")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.EMail })
                .IsUnique(true);

            modelBuilder.Entity<Cancel>()
                .HasIndex(p => new { p.CancelName })
                .IsUnique(true);

            modelBuilder.Entity<UserRole>()
                .HasIndex(p => new { p.RoleName })
                .IsUnique(true);

            modelBuilder.Entity<Customer>()
                .HasIndex(p => new { p.EMail })
                .IsUnique(true);

            modelBuilder.Entity<UserRole>().HasData(new UserRole[]
                {
                    new UserRole { ID = -1, RoleName = "Site"},
                    new UserRole { ID = 0, RoleName = "Developer" },
                    new UserRole { ID = 1, RoleName = "Administrator" },
                    new UserRole { ID = 2, RoleName = "Admin" }
                }
            );

            //Negatif ID olan kayıtlar sistemde görünmez
            modelBuilder.Entity<Cancel>().HasData(new Cancel[]
                {
                    new Cancel { ID = -2, CancelName = "Silindi"},
                    new Cancel { ID = -1, CancelName = "Sistemsel Kayıt"},
                    new Cancel { ID = 1, CancelName = "Pasif Kayıt"}
                }
            );

            //Site kullanıcısı pasif olur
            modelBuilder.Entity<User>().HasData(new User[]
                {
                    new User { ID = 1, CreateTime = DateTime.Now, CreatedBy = 1, Name="Developer", Surname="Kullanıcı",EMail = "developer@harunozer.com", Password = "12345", UserRoleID = 0},
                    new User { ID = 2, CreateTime = DateTime.Now, CreatedBy = 1, Name="Site", Surname="Kullanıcı",EMail = "info@harunozer.com", Password = "s21/()d52^43^+!%&",UserRoleID = -1, CancelID = -1, CanceledBy = 1, CancelTime = DateTime.Now},
                    new User { ID = 3, CreateTime = DateTime.Now, CreatedBy = 1, Name="Admin", Surname="Kullanıcı",EMail = "admin@harunozer.com", Password = "12345", UserRoleID = 1}

                }
            );


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                bool ProcessBaseModel = true;

                if (entry.Entity.GetType().GetProperty("ProcessBaseModel") != null)
                    ProcessBaseModel = (bool)entry.Entity.GetType().GetProperty("ProcessBaseModel").GetValue(entry.Entity, null);

                if (ProcessBaseModel)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (entry.Entity.GetType().GetProperty("CreateTime") != null)
                            entry.Property("CreateTime").CurrentValue = DateTime.Now;

                        if (entry.Entity.GetType().GetProperty("CreatedBy") != null)
                            entry.Property("CreatedBy").CurrentValue = CurrentUserID;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (entry.Entity.GetType().GetProperty("UpdateTime") != null)
                            entry.Property("UpdateTime").CurrentValue = DateTime.Now;

                        if (entry.Entity.GetType().GetProperty("UpdatedBy") != null)
                            entry.Property("UpdatedBy").CurrentValue = CurrentUserID;
                    }
                }

                if (entry.Entity.GetType().GetProperty("CancelID") != null)
                {
                    int? CancelIDOrginalValue = (int?)entry.Property("CancelID").OriginalValue;
                    int? CancelIDCurrentValue = (int?)entry.Property("CancelID").CurrentValue;

                    //Cancel durumu değiştiyse cancel alanlarını güncelle
                    if (CancelIDOrginalValue != CancelIDCurrentValue)
                    {
                        if ((int?)entry.Property("CancelID").CurrentValue != null)
                        {
                            if (entry.Entity.GetType().GetProperty("CancelTime") != null)
                                entry.Property("CancelTime").CurrentValue = DateTime.Now;

                            if (entry.Entity.GetType().GetProperty("CanceledBy") != null)
                                entry.Property("CanceledBy").CurrentValue = CurrentUserID;
                        }
                        else
                        {
                            if (entry.Entity.GetType().GetProperty("CancelTime") != null)
                                entry.Property("CancelTime").CurrentValue = null;

                            if (entry.Entity.GetType().GetProperty("CanceledBy") != null)
                                entry.Property("CanceledBy").CurrentValue = null;
                        }
                    }
                }
            }
            //TODO: ElasticSearch vb. ile değişiklikleri logla
            return base.SaveChanges();
        }
    }
}