using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicPay.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTransactionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactionSchema_userSchema_UserSchemaId",
                table: "transactionSchema");

            migrationBuilder.DropForeignKey(
                name: "FK_transactionSchema_userSchema_UserSchemaId1",
                table: "transactionSchema");

            migrationBuilder.DropIndex(
                name: "IX_transactionSchema_UserSchemaId",
                table: "transactionSchema");

            migrationBuilder.DropIndex(
                name: "IX_transactionSchema_UserSchemaId1",
                table: "transactionSchema");

            migrationBuilder.DropColumn(
                name: "UserSchemaId",
                table: "transactionSchema");

            migrationBuilder.DropColumn(
                name: "UserSchemaId1",
                table: "transactionSchema");

            migrationBuilder.CreateIndex(
                name: "IX_transactionSchema_FkPayee",
                table: "transactionSchema",
                column: "FkPayee");

            migrationBuilder.CreateIndex(
                name: "IX_transactionSchema_FkPayer",
                table: "transactionSchema",
                column: "FkPayer");

            migrationBuilder.AddForeignKey(
                name: "FK_transactionSchema_userSchema_FkPayee",
                table: "transactionSchema",
                column: "FkPayee",
                principalTable: "userSchema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactionSchema_userSchema_FkPayer",
                table: "transactionSchema",
                column: "FkPayer",
                principalTable: "userSchema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactionSchema_userSchema_FkPayee",
                table: "transactionSchema");

            migrationBuilder.DropForeignKey(
                name: "FK_transactionSchema_userSchema_FkPayer",
                table: "transactionSchema");

            migrationBuilder.DropIndex(
                name: "IX_transactionSchema_FkPayee",
                table: "transactionSchema");

            migrationBuilder.DropIndex(
                name: "IX_transactionSchema_FkPayer",
                table: "transactionSchema");

            migrationBuilder.AddColumn<string>(
                name: "UserSchemaId",
                table: "transactionSchema",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSchemaId1",
                table: "transactionSchema",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactionSchema_UserSchemaId",
                table: "transactionSchema",
                column: "UserSchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_transactionSchema_UserSchemaId1",
                table: "transactionSchema",
                column: "UserSchemaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_transactionSchema_userSchema_UserSchemaId",
                table: "transactionSchema",
                column: "UserSchemaId",
                principalTable: "userSchema",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactionSchema_userSchema_UserSchemaId1",
                table: "transactionSchema",
                column: "UserSchemaId1",
                principalTable: "userSchema",
                principalColumn: "Id");
        }
    }
}
