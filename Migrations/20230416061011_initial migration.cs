using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppoinmentScheduler.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RootUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IamSlug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appoinments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartRange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndRange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Intreval = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOrganizationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appoinments_RootUsers_CreatedOrganizationId",
                        column: x => x.CreatedOrganizationId,
                        principalTable: "RootUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_RootUsers_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "RootUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppoinmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    SlotTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlotUserById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_Appoinments_AppoinmentId",
                        column: x => x.AppoinmentId,
                        principalTable: "Appoinments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Slots_Users_SlotUserById",
                        column: x => x.SlotUserById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_CreatedOrganizationId",
                table: "Appoinments",
                column: "CreatedOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_AppoinmentId",
                table: "Slots",
                column: "AppoinmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_SlotUserById",
                table: "Slots",
                column: "SlotUserById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Appoinments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RootUsers");
        }
    }
}
