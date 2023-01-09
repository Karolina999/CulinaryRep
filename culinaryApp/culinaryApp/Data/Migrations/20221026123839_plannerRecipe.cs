using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace culinaryApp.Data.Migrations
{
    public partial class plannerRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannerRecipe_Planners_PlannerId",
                table: "PlannerRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannerRecipe_Recipes_RecipeId",
                table: "PlannerRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlannerRecipe",
                table: "PlannerRecipe");

            migrationBuilder.RenameTable(
                name: "PlannerRecipe",
                newName: "PlannerRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_PlannerRecipe_RecipeId",
                table: "PlannerRecipes",
                newName: "IX_PlannerRecipes_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlannerRecipes",
                table: "PlannerRecipes",
                columns: new[] { "PlannerId", "RecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlannerRecipes_Planners_PlannerId",
                table: "PlannerRecipes",
                column: "PlannerId",
                principalTable: "Planners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannerRecipes_Recipes_RecipeId",
                table: "PlannerRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannerRecipes_Planners_PlannerId",
                table: "PlannerRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannerRecipes_Recipes_RecipeId",
                table: "PlannerRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlannerRecipes",
                table: "PlannerRecipes");

            migrationBuilder.RenameTable(
                name: "PlannerRecipes",
                newName: "PlannerRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_PlannerRecipes_RecipeId",
                table: "PlannerRecipe",
                newName: "IX_PlannerRecipe_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlannerRecipe",
                table: "PlannerRecipe",
                columns: new[] { "PlannerId", "RecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlannerRecipe_Planners_PlannerId",
                table: "PlannerRecipe",
                column: "PlannerId",
                principalTable: "Planners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannerRecipe_Recipes_RecipeId",
                table: "PlannerRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
