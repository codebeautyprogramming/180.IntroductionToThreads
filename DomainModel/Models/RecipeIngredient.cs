using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public decimal Amount { get; set; }

        public RecipeIngredient() { }

        public RecipeIngredient(int recipeId, int ingredientId, decimal amount)
        {
            RecipeId=recipeId;
            IngredientId=ingredientId;
            Amount=amount;
        }
    }
}
