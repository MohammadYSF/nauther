﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nauther.Identity.Persistence.Data;

#nullable disable

namespace Nauther.Identity.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.GroupPermission", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermissions");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("093d581c-28bb-4700-83a2-429d21765ee6"),
                            DisplayName = "مشاهده دسترسی",
                            Name = "ViewPermission"
                        },
                        new
                        {
                            Id = new Guid("a40cda85-a303-46d7-8350-cbed507efb12"),
                            DisplayName = "ایجاد دسترسی",
                            Name = "AddPermission"
                        },
                        new
                        {
                            Id = new Guid("7c52899e-c7b1-4074-95fc-4257120b5b29"),
                            DisplayName = "ویرایش دسترسی",
                            Name = "EditPermission"
                        },
                        new
                        {
                            Id = new Guid("be05bebe-21e7-4d7f-964f-ce4030373ac6"),
                            DisplayName = "حذف دسترسی",
                            Name = "DeletePermission"
                        },
                        new
                        {
                            Id = new Guid("9a755e5d-4380-49ab-bae2-06480b8e9476"),
                            DisplayName = "مشاهده نقش",
                            Name = "ViewRole"
                        },
                        new
                        {
                            Id = new Guid("62ec4f35-44ca-4c63-82b9-689d2bfde0c6"),
                            DisplayName = "ویرایش نقش",
                            Name = "EditRole"
                        },
                        new
                        {
                            Id = new Guid("0f5c4756-d479-4029-a1c5-165f7385accb"),
                            DisplayName = "ایجاد نقش",
                            Name = "AddRole"
                        },
                        new
                        {
                            Id = new Guid("0010928a-3381-46aa-b9b7-c2db83dd0f82"),
                            DisplayName = "حذف نقش",
                            Name = "DeleteRole"
                        },
                        new
                        {
                            Id = new Guid("9a5a8465-cb5b-44eb-bbe5-16a463a03c37"),
                            DisplayName = "مشاهده ادمین",
                            Name = "ViewAdmin"
                        },
                        new
                        {
                            Id = new Guid("3180f21d-0785-4293-a8fe-281a533b768c"),
                            DisplayName = "حذف ادمین",
                            Name = "DeleteAdmin"
                        },
                        new
                        {
                            Id = new Guid("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33"),
                            DisplayName = "ایجاد ادمین",
                            Name = "AddAdmin"
                        },
                        new
                        {
                            Id = new Guid("8ca6c12d-a066-4859-a3a0-f786de32821c"),
                            DisplayName = "ویرایش ادمین",
                            Name = "EditAdmin"
                        });
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            DisplayName = "نقش ادمین",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("093d581c-28bb-4700-83a2-429d21765ee6")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("a40cda85-a303-46d7-8350-cbed507efb12")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("7c52899e-c7b1-4074-95fc-4257120b5b29")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("be05bebe-21e7-4d7f-964f-ce4030373ac6")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("9a755e5d-4380-49ab-bae2-06480b8e9476")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("62ec4f35-44ca-4c63-82b9-689d2bfde0c6")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("0f5c4756-d479-4029-a1c5-165f7385accb")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("0010928a-3381-46aa-b9b7-c2db83dd0f82")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("9a5a8465-cb5b-44eb-bbe5-16a463a03c37")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("3180f21d-0785-4293-a8fe-281a533b768c")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33")
                        },
                        new
                        {
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738"),
                            PermissionId = new Guid("8ca6c12d-a066-4859-a3a0-f786de32821c")
                        });
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "65f84f5e-38d6-410a-a9a1-7e1cdff64b33"
                        });
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserCredential", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("UserId");

                    b.ToTable("UserCredentials");

                    b.HasData(
                        new
                        {
                            UserId = "65f84f5e-38d6-410a-a9a1-7e1cdff64b33",
                            PasswordHash = "$argon2id$v=19$m=65536,t=3,p=1$vCyJ1Bp2Vbb2Yh0LVIhh+w$LXPvVkoFs0bqF3OmXb4hWUpPlBvjouUi2bw/S0q0oEc"
                        });
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserGroup", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserPermission", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "65f84f5e-38d6-410a-a9a1-7e1cdff64b33",
                            RoleId = new Guid("081e66fc-527d-47a7-941f-7af16e95e738")
                        });
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.GroupPermission", b =>
                {
                    b.HasOne("Nauther.Identity.Domain.Entities.Group", "Group")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nauther.Identity.Domain.Entities.Permission", "Permission")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("Nauther.Identity.Domain.Entities.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nauther.Identity.Domain.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserCredential", b =>
                {
                    b.HasOne("Nauther.Identity.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Nauther.Identity.Domain.Entities.UserCredential", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserGroup", b =>
                {
                    b.HasOne("Nauther.Identity.Domain.Entities.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nauther.Identity.Domain.Entities.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserPermission", b =>
                {
                    b.HasOne("Nauther.Identity.Domain.Entities.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nauther.Identity.Domain.Entities.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Nauther.Identity.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nauther.Identity.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.Group", b =>
                {
                    b.Navigation("GroupPermissions");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.Permission", b =>
                {
                    b.Navigation("GroupPermissions");

                    b.Navigation("RolePermissions");

                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Nauther.Identity.Domain.Entities.User", b =>
                {
                    b.Navigation("UserGroups");

                    b.Navigation("UserPermissions");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
