using AutoMapper;
using culinaryApp.Dto;
using culinaryApp.Models;

namespace culinaryApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDto>();
        }
    }
}
