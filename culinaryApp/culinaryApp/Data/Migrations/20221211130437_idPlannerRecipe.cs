using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace culinaryApp.Data.Migrations
{
    public partial class idPlannerRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planners_Users_UserId",
                table: "Planners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFromPlanners_Ingredients_IngredientId",
                table: "ProductFromPlanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlannerRecipes",
                table: "PlannerRecipes");

            migrationBuilder.AlterColumn<string>(
                name: "MealType",
                table: "ProductFromPlanners",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "ProductFromPlanners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Planners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PlannerRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlannerRecipes",
                table: "PlannerRecipes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlannerRecipes_PlannerId",
                table: "PlannerRecipes",
                column: "PlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planners_Users_UserId",
                table: "Planners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFromPlanners_Ingredients_IngredientId",
                table: "ProductFromPlanners",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planners_Users_UserId",
                table: "Planners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFromPlanners_Ingredients_IngredientId",
                table: "ProductFromPlanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlannerRecipes",
                table: "PlannerRecipes");

            migrationBuilder.DropIndex(
                name: "IX_PlannerRecipes_PlannerId",
                table: "PlannerRecipes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlannerRecipes");

            migrationBuilder.AlterColumn<int>(
                name: "MealType",
                table: "ProductFromPlanners",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "ProductFromPlanners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Planners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlannerRecipes",
                table: "PlannerRecipes",
                columns: new[] { "PlannerId", "RecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Planners_Users_UserId",
                table: "Planners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFromPlanners_Ingredients_IngredientId",
                table: "ProductFromPlanners",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }
    }
}
