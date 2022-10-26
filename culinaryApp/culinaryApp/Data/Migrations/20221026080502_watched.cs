using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace culinaryApp.Data.Migrations
{
    public partial class watched : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedRecipe_Recipes_RecipeId",
                table: "WatchedRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedRecipe_Users_UserId",
                table: "WatchedRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchedRecipe",
                table: "WatchedRecipe");

            migrationBuilder.RenameTable(
                name: "WatchedRecipe",
                newName: "WatchedRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_WatchedRecipe_UserId",
                table: "WatchedRecipes",
                newName: "IX_WatchedRecipes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchedRecipes",
                table: "WatchedRecipes",
                columns: new[] { "RecipeId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedRecipes_Recipes_RecipeId",
                table: "WatchedRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedRecipes_Users_UserId",
                table: "WatchedRecipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedRecipes_Recipes_RecipeId",
                table: "WatchedRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedRecipes_Users_UserId",
                table: "WatchedRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchedRecipes",
                table: "WatchedRecipes");

            migrationBuilder.RenameTable(
                name: "WatchedRecipes",
                newName: "WatchedRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_WatchedRecipes_UserId",
                table: "WatchedRecipe",
                newName: "IX_WatchedRecipe_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchedRecipe",
                table: "WatchedRecipe",
                columns: new[] { "RecipeId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedRecipe_Recipes_RecipeId",
                table: "WatchedRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedRecipe_Users_UserId",
                table: "WatchedRecipe",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
