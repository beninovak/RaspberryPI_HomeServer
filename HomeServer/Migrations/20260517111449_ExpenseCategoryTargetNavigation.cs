using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeServer.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseCategoryTargetNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExpenseTargets_ExpenseCategoryId",
                table: "ExpenseTargets");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExpenseTargets",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargets_ExpenseCategoryId",
                table: "ExpenseTargets",
                column: "ExpenseCategoryId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExpenseTargets_ExpenseCategoryId",
                table: "ExpenseTargets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExpenseTargets");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargets_ExpenseCategoryId",
                table: "ExpenseTargets",
                column: "ExpenseCategoryId");
        }
    }
}
