using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeServer.Server.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                    table.UniqueConstraint("AK_ExpenseCategories_CreatedBy_Order", x => new { x.CreatedBy, x.Order });
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoCategories", x => x.Id);
                    table.UniqueConstraint("AK_TodoCategories_CreatedBy_Order", x => new { x.CreatedBy, x.Order });
                    table.ForeignKey(
                        name: "FK_TodoCategories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoCategories_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.UniqueConstraint("AK_Expenses_ExpenseCategoryId_Order", x => new { x.ExpenseCategoryId, x.Order });
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTargets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseTargets_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseTargets_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseTargets_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    TodoCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.UniqueConstraint("AK_Todos_TodoCategoryId_Order", x => new { x.TodoCategoryId, x.Order });
                    table.ForeignKey(
                        name: "FK_Todos_TodoCategories_TodoCategoryId",
                        column: x => x.TodoCategoryId,
                        principalTable: "TodoCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todos_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todos_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTargetsHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseTargetId = table.Column<long>(type: "bigint", nullable: false),
                    Month = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    TargetHit = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTargetsHistory", x => x.Id);
                    table.UniqueConstraint("AK_ExpenseTargetsHistory_ExpenseTargetId_Month_Year", x => new { x.ExpenseTargetId, x.Month, x.Year });
                    table.ForeignKey(
                        name: "FK_ExpenseTargetsHistory_ExpenseTargets_ExpenseTargetId",
                        column: x => x.ExpenseTargetId,
                        principalTable: "ExpenseTargets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_Guid",
                table: "ExpenseCategories",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_UpdatedBy",
                table: "ExpenseCategories",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatedBy",
                table: "Expenses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Guid",
                table: "Expenses",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UpdatedBy",
                table: "Expenses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargets_CreatedBy",
                table: "ExpenseTargets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargets_ExpenseCategoryId",
                table: "ExpenseTargets",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargets_Guid",
                table: "ExpenseTargets",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargets_UpdatedBy",
                table: "ExpenseTargets",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTargetsHistory_Guid",
                table: "ExpenseTargetsHistory",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoCategories_Guid",
                table: "TodoCategories",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoCategories_UpdatedBy",
                table: "TodoCategories",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CreatedBy",
                table: "Todos",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Guid",
                table: "Todos",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UpdatedBy",
                table: "Todos",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Guid",
                table: "Users",
                column: "Guid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseTargetsHistory");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "ExpenseTargets");

            migrationBuilder.DropTable(
                name: "TodoCategories");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
