﻿using culinaryApp.Models;

namespace culinaryApp.Dto
{
    public class ProductFromRecipeGetDto
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
