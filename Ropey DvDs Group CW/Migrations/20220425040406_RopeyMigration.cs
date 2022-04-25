using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ropey_DvDs_Group_CW.Migrations
{
    public partial class RopeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorModel",
                columns: table => new
                {
                    ActorNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActorSurname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorModel", x => x.ActorNumber);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DVDCategoryModel",
                columns: table => new
                {
                    CategoryNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDCategoryModel", x => x.CategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "LoanTypeModel",
                columns: table => new
                {
                    LoanTypeNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypeModel", x => x.LoanTypeNumber);
                });

            migrationBuilder.CreateTable(
                name: "MembershipCategoryModel",
                columns: table => new
                {
                    MembershipCategoryNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipCategoryTotalLoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCategoryModel", x => x.MembershipCategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "ProducerModel",
                columns: table => new
                {
                    ProducerNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducerModel", x => x.ProducerNumber);
                });

            migrationBuilder.CreateTable(
                name: "StudioModel",
                columns: table => new
                {
                    StudioNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudioName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioModel", x => x.StudioNumber);
                });

            migrationBuilder.CreateTable(
                name: "UserRegisterModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegisterModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberModel",
                columns: table => new
                {
                    MemberNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipCategoryNumber = table.Column<int>(type: "int", nullable: false),
                    MemberLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberModel", x => x.MemberNumber);
                    table.ForeignKey(
                        name: "FK_MemberModel_MembershipCategoryModel_MembershipCategoryNumber",
                        column: x => x.MembershipCategoryNumber,
                        principalTable: "MembershipCategoryModel",
                        principalColumn: "MembershipCategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DVDTitleModel",
                columns: table => new
                {
                    DVDNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryNumber = table.Column<int>(type: "int", nullable: false),
                    StudioNumber = table.Column<int>(type: "int", nullable: false),
                    ProducerNumber = table.Column<int>(type: "int", nullable: false),
                    DVDTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReleased = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StandardCharge = table.Column<int>(type: "int", nullable: false),
                    PenaltyCharge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDTitleModel", x => x.DVDNumber);
                    table.ForeignKey(
                        name: "FK_DVDTitleModel_DVDCategoryModel_CategoryNumber",
                        column: x => x.CategoryNumber,
                        principalTable: "DVDCategoryModel",
                        principalColumn: "CategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DVDTitleModel_ProducerModel_ProducerNumber",
                        column: x => x.ProducerNumber,
                        principalTable: "ProducerModel",
                        principalColumn: "ProducerNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DVDTitleModel_StudioModel_StudioNumber",
                        column: x => x.StudioNumber,
                        principalTable: "StudioModel",
                        principalColumn: "StudioNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CastMemberModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DVDNumber = table.Column<int>(type: "int", nullable: false),
                    ActorNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMemberModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastMemberModel_ActorModel_ActorNumber",
                        column: x => x.ActorNumber,
                        principalTable: "ActorModel",
                        principalColumn: "ActorNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMemberModel_DVDTitleModel_DVDNumber",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitleModel",
                        principalColumn: "DVDNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DVDCopyModel",
                columns: table => new
                {
                    CopyNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DVDNumber = table.Column<int>(type: "int", nullable: false),
                    DatePurchased = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDCopyModel", x => x.CopyNumber);
                    table.ForeignKey(
                        name: "FK_DVDCopyModel_DVDTitleModel_DVDNumber",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitleModel",
                        principalColumn: "DVDNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanModel",
                columns: table => new
                {
                    LoanNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanTypeNumber = table.Column<int>(type: "int", nullable: false),
                    CopyNumber = table.Column<int>(type: "int", nullable: false),
                    MemberNumber = table.Column<int>(type: "int", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturned = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanModel", x => x.LoanNumber);
                    table.ForeignKey(
                        name: "FK_LoanModel_DVDCopyModel_CopyNumber",
                        column: x => x.CopyNumber,
                        principalTable: "DVDCopyModel",
                        principalColumn: "CopyNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanModel_LoanTypeModel_LoanTypeNumber",
                        column: x => x.LoanTypeNumber,
                        principalTable: "LoanTypeModel",
                        principalColumn: "LoanTypeNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanModel_MemberModel_MemberNumber",
                        column: x => x.MemberNumber,
                        principalTable: "MemberModel",
                        principalColumn: "MemberNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CastMemberModel_ActorNumber",
                table: "CastMemberModel",
                column: "ActorNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CastMemberModel_DVDNumber",
                table: "CastMemberModel",
                column: "DVDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDCopyModel_DVDNumber",
                table: "DVDCopyModel",
                column: "DVDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitleModel_CategoryNumber",
                table: "DVDTitleModel",
                column: "CategoryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitleModel_ProducerNumber",
                table: "DVDTitleModel",
                column: "ProducerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitleModel_StudioNumber",
                table: "DVDTitleModel",
                column: "StudioNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LoanModel_CopyNumber",
                table: "LoanModel",
                column: "CopyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LoanModel_LoanTypeNumber",
                table: "LoanModel",
                column: "LoanTypeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LoanModel_MemberNumber",
                table: "LoanModel",
                column: "MemberNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MemberModel_MembershipCategoryNumber",
                table: "MemberModel",
                column: "MembershipCategoryNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CastMemberModel");

            migrationBuilder.DropTable(
                name: "LoanModel");

            migrationBuilder.DropTable(
                name: "UserRegisterModel");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ActorModel");

            migrationBuilder.DropTable(
                name: "DVDCopyModel");

            migrationBuilder.DropTable(
                name: "LoanTypeModel");

            migrationBuilder.DropTable(
                name: "MemberModel");

            migrationBuilder.DropTable(
                name: "DVDTitleModel");

            migrationBuilder.DropTable(
                name: "MembershipCategoryModel");

            migrationBuilder.DropTable(
                name: "DVDCategoryModel");

            migrationBuilder.DropTable(
                name: "ProducerModel");

            migrationBuilder.DropTable(
                name: "StudioModel");
        }
    }
}
