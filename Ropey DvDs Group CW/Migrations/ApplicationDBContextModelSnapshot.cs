﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ropey_DvDs_Group_CW.DBContext;

#nullable disable

namespace Ropey_DvDs_Group_CW.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.ActorModel", b =>
                {
                    b.Property<int>("ActorNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActorNumber"), 1L, 1);

                    b.Property<string>("ActorFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActorSurname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorNumber");

                    b.ToTable("ActorModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.CastMemberModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActorNumber")
                        .HasColumnType("int");

                    b.Property<int>("DVDNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActorNumber");

                    b.HasIndex("DVDNumber");

                    b.ToTable("CastMemberModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDCategoryModel", b =>
                {
                    b.Property<int>("CategoryNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryNumber"), 1L, 1);

                    b.Property<bool>("AgeRestricted")
                        .HasColumnType("bit");

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryNumber");

                    b.ToTable("DVDCategoryModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDCopyModel", b =>
                {
                    b.Property<int>("CopyNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CopyNumber"), 1L, 1);

                    b.Property<int>("DVDNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("datetime2");

                    b.HasKey("CopyNumber");

                    b.HasIndex("DVDNumber");

                    b.ToTable("DVDCopyModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDTitleModel", b =>
                {
                    b.Property<int>("DVDNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DVDNumber"), 1L, 1);

                    b.Property<int>("CategoryNumber")
                        .HasColumnType("int");

                    b.Property<string>("DVDTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateReleased")
                        .HasColumnType("datetime2");

                    b.Property<int>("PenaltyCharge")
                        .HasColumnType("int");

                    b.Property<int>("ProducerNumber")
                        .HasColumnType("int");

                    b.Property<int>("StandardCharge")
                        .HasColumnType("int");

                    b.Property<int>("StudioNumber")
                        .HasColumnType("int");

                    b.HasKey("DVDNumber");

                    b.HasIndex("CategoryNumber");

                    b.HasIndex("ProducerNumber");

                    b.HasIndex("StudioNumber");

                    b.ToTable("DVDTitleModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.LoanModel", b =>
                {
                    b.Property<int>("LoanNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanNumber"), 1L, 1);

                    b.Property<int>("CopyNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoanTypeNumber")
                        .HasColumnType("int");

                    b.Property<int>("MemberNumber")
                        .HasColumnType("int");

                    b.HasKey("LoanNumber");

                    b.HasIndex("CopyNumber");

                    b.HasIndex("LoanTypeNumber");

                    b.HasIndex("MemberNumber");

                    b.ToTable("LoanModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.LoanTypeModel", b =>
                {
                    b.Property<int>("LoanTypeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanTypeNumber"), 1L, 1);

                    b.Property<int>("LoanDuration")
                        .HasColumnType("int");

                    b.Property<string>("LoanType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanTypeNumber");

                    b.ToTable("LoanTypeModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.MemberModel", b =>
                {
                    b.Property<int>("MemberNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberNumber"), 1L, 1);

                    b.Property<string>("MemberAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MemberDateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MembershipCategoryNumber")
                        .HasColumnType("int");

                    b.HasKey("MemberNumber");

                    b.HasIndex("MembershipCategoryNumber");

                    b.ToTable("MemberModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.MembershipCategoryModel", b =>
                {
                    b.Property<int>("MembershipCategoryNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MembershipCategoryNumber"), 1L, 1);

                    b.Property<string>("MembershipCategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MembershipCategoryTotalLoan")
                        .HasColumnType("int");

                    b.HasKey("MembershipCategoryNumber");

                    b.ToTable("MembershipCategoryModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.ProducerModel", b =>
                {
                    b.Property<int>("ProducerNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProducerNumber"), 1L, 1);

                    b.Property<string>("ProducerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducerNumber");

                    b.ToTable("ProducerModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.StudioModel", b =>
                {
                    b.Property<int>("StudioNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudioNumber"), 1L, 1);

                    b.Property<string>("StudioName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudioNumber");

                    b.ToTable("StudioModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.CastMemberModel", b =>
                {
                    b.HasOne("Ropey_DvDs_Group_CW.Models.ActorModel", "ActorModel")
                        .WithMany("CastMembers")
                        .HasForeignKey("ActorNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ropey_DvDs_Group_CW.Models.DVDTitleModel", "DVDTitleModel")
                        .WithMany("CastMembers")
                        .HasForeignKey("DVDNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActorModel");

                    b.Navigation("DVDTitleModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDCopyModel", b =>
                {
                    b.HasOne("Ropey_DvDs_Group_CW.Models.DVDTitleModel", "DVDTitleModel")
                        .WithMany("DVDCopys")
                        .HasForeignKey("DVDNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DVDTitleModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDTitleModel", b =>
                {
                    b.HasOne("Ropey_DvDs_Group_CW.Models.DVDCategoryModel", "DVDCategoryModel")
                        .WithMany("DVDTitles")
                        .HasForeignKey("CategoryNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ropey_DvDs_Group_CW.Models.ProducerModel", "ProducerModel")
                        .WithMany("DVDTitles")
                        .HasForeignKey("ProducerNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ropey_DvDs_Group_CW.Models.StudioModel", "StudioModel")
                        .WithMany("DVDTitles")
                        .HasForeignKey("StudioNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DVDCategoryModel");

                    b.Navigation("ProducerModel");

                    b.Navigation("StudioModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.LoanModel", b =>
                {
                    b.HasOne("Ropey_DvDs_Group_CW.Models.DVDCopyModel", "DVDCopyModel")
                        .WithMany("Loans")
                        .HasForeignKey("CopyNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ropey_DvDs_Group_CW.Models.LoanTypeModel", "LoanTypeModel")
                        .WithMany("Loans")
                        .HasForeignKey("LoanTypeNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ropey_DvDs_Group_CW.Models.MemberModel", "MemberModel")
                        .WithMany("LoanModels")
                        .HasForeignKey("MemberNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DVDCopyModel");

                    b.Navigation("LoanTypeModel");

                    b.Navigation("MemberModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.MemberModel", b =>
                {
                    b.HasOne("Ropey_DvDs_Group_CW.Models.MembershipCategoryModel", "membershipCategoryModel")
                        .WithMany("Members")
                        .HasForeignKey("MembershipCategoryNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("membershipCategoryModel");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.ActorModel", b =>
                {
                    b.Navigation("CastMembers");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDCategoryModel", b =>
                {
                    b.Navigation("DVDTitles");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDCopyModel", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.DVDTitleModel", b =>
                {
                    b.Navigation("CastMembers");

                    b.Navigation("DVDCopys");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.LoanTypeModel", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.MemberModel", b =>
                {
                    b.Navigation("LoanModels");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.MembershipCategoryModel", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.ProducerModel", b =>
                {
                    b.Navigation("DVDTitles");
                });

            modelBuilder.Entity("Ropey_DvDs_Group_CW.Models.StudioModel", b =>
                {
                    b.Navigation("DVDTitles");
                });
#pragma warning restore 612, 618
        }
    }
}
