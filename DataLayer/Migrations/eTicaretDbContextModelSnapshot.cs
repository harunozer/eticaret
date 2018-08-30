﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(eTicaretDbContext))]
    partial class eTicaretDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Models.Cancel", b =>
                {
                    b.Property<int>("ID");

                    b.Property<int?>("CancelID");

                    b.Property<string>("CancelName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("CancelID");

                    b.HasIndex("CancelName")
                        .IsUnique();

                    b.ToTable("Cancel");

                    b.HasData(
                        new { ID = -2, CancelName = "Silindi" },
                        new { ID = -1, CancelName = "Sistemsel Kayıt" },
                        new { ID = 1, CancelName = "Pasif Kayıt" }
                    );
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CancelID");

                    b.Property<DateTime?>("CancelTime");

                    b.Property<int?>("CanceledBy");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Gsm");

                    b.Property<bool>("IsLogin");

                    b.Property<string>("LastLoginIP");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastLogoutTime");

                    b.Property<int>("LoginCount");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdateTime");

                    b.Property<int?>("UpdatedBy");

                    b.Property<int>("UserRoleID");

                    b.HasKey("ID");

                    b.HasIndex("CancelID");

                    b.HasIndex("CanceledBy");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EMail")
                        .IsUnique();

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserRoleID");

                    b.ToTable("User");

                    b.HasData(
                        new { ID = 1, CreateTime = new DateTime(2018, 8, 29, 0, 30, 44, 428, DateTimeKind.Local), CreatedBy = 1, EMail = "developer@harunozer.com", IsLogin = false, LoginCount = 0, Name = "Developer", Password = "12345", Surname = "Kullanıcı", UserRoleID = 0 },
                        new { ID = 2, CancelID = -1, CancelTime = new DateTime(2018, 8, 29, 0, 30, 44, 429, DateTimeKind.Local), CanceledBy = 1, CreateTime = new DateTime(2018, 8, 29, 0, 30, 44, 429, DateTimeKind.Local), CreatedBy = 1, EMail = "info@harunozer.com", IsLogin = false, LoginCount = 0, Name = "Site", Password = "s21/()d52^43^+!%&", Surname = "Kullanıcı", UserRoleID = -1 },
                        new { ID = 3, CreateTime = new DateTime(2018, 8, 29, 0, 30, 44, 429, DateTimeKind.Local), CreatedBy = 1, EMail = "admin@harunozer.com", IsLogin = false, LoginCount = 0, Name = "Admin", Password = "12345", Surname = "Kullanıcı", UserRoleID = 1 }
                    );
                });

            modelBuilder.Entity("DataLayer.Models.UserRole", b =>
                {
                    b.Property<int>("ID");

                    b.Property<int?>("CancelID");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("CancelID");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("UserRol");

                    b.HasData(
                        new { ID = -1, RoleName = "Site" },
                        new { ID = 0, RoleName = "Developer" },
                        new { ID = 1, RoleName = "Administrator" },
                        new { ID = 2, RoleName = "Admin" }
                    );
                });

            modelBuilder.Entity("DataLayer.Models.Cancel", b =>
                {
                    b.HasOne("DataLayer.Models.Cancel", "CancelObj")
                        .WithMany()
                        .HasForeignKey("CancelID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.HasOne("DataLayer.Models.Cancel", "Cancel")
                        .WithMany()
                        .HasForeignKey("CancelID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Models.User", "CanceledUser")
                        .WithMany()
                        .HasForeignKey("CanceledBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Models.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Models.User", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Models.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Models.UserRole", b =>
                {
                    b.HasOne("DataLayer.Models.Cancel", "CancelObj")
                        .WithMany()
                        .HasForeignKey("CancelID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
