using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicPay.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userSchema",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSchema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transactionSchema",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FkPayer = table.Column<string>(type: "text", nullable: false),
                    FkPayee = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserSchemaId = table.Column<string>(type: "text", nullable: true),
                    UserSchemaId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionSchema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactionSchema_userSchema_UserSchemaId",
                        column: x => x.UserSchemaId,
                        principalTable: "userSchema",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transactionSchema_userSchema_UserSchemaId1",
                        column: x => x.UserSchemaId1,
                        principalTable: "userSchema",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactionSchema_UserSchemaId",
                table: "transactionSchema",
                column: "UserSchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_transactionSchema_UserSchemaId1",
                table: "transactionSchema",
                column: "UserSchemaId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactionSchema");

            migrationBuilder.DropTable(
                name: "userSchema");
        }
    }
}
