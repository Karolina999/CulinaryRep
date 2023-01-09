using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace culinaryApp.Data.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlannerRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannerId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    MealType = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannerRecipes_Planners_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "Planners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlannerRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannerRecipes_PlannerId",
                table: "PlannerRecipes",
                column: "PlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannerRecipes_RecipeId",
                table: "PlannerRecipes",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerRecipes");
        }
    }
}
