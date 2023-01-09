using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace culinaryApp.Data.Migrations
{
    public partial class RemoveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerRecipes");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Users",
                newName: "Photo");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerRecipe");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Users",
                newName: "ImageUrl");

            migrationBuilder.CreateTable(
                name: "PlannerRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannerId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false)
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
    }
}
