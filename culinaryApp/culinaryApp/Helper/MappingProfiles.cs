using AutoMapper;
using culinaryApp.Dto;
using culinaryApp.Models;

namespace culinaryApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<Planner, PlannerDto>();
            CreateMap<PlannerDto, Planner>();

            CreateMap<PlannerRecipe, PlannerRecipeDto>();
            CreateMap<PlannerRecipeDto, PlannerRecipe>();

            CreateMap<ProductFromList, ProductFromListDto>();
            CreateMap<ProductFromListDto, ProductFromList>();

            CreateMap<ProductFromPlanner, ProductFromPlannerDto>();
            CreateMap<ProductFromPlannerDto, ProductFromPlanner>();

            CreateMap<ProductFromRecipe, ProductFromRecipeDto>();
            CreateMap<ProductFromRecipeDto, ProductFromRecipe>();

            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();

            CreateMap<ShoppingList, ShoppingListDto>();
            CreateMap<ShoppingListDto, ShoppingList>();

            CreateMap<Step, StepDto>();
            CreateMap<StepDto, Step>();

            CreateMap<UserComment, UserCommentDto>();
            CreateMap<UserCommentDto, UserComment>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
