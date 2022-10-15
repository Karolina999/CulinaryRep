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
            CreateMap<Planner, PlannerDto>();
            CreateMap<PlannerRecipe, PlannerRecipeDto>();
            CreateMap<ProductFromList, ProductFromListDto>();
            CreateMap<ProductFromPlanner, ProductFromPlannerDto>();
            CreateMap<ProductFromRecipe, ProductFromRecipeDto>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<ShoppingList, ShoppingListDto>();
            CreateMap<Step, StepDto>();
            CreateMap<UserComment, UserCommentDto>();
            CreateMap<User, UserDto>();
        }
    }
}
