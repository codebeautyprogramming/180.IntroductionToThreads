using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IRecipeIngredientsRepository
    {
        public Task<List<RecipeIngredientWithNameAndAmount>> GetRecipeIngredients(int recipeId);
        public Task AddRecipeIngredient(RecipeIngredient recipeIngredient);
        public Task DeleteRecipeIngredient(int ingredientId, int recipeId);
        public Task EditRecipeIngredientAmount(RecipeIngredient recipeIngredient);

        public Task<List<RecipeIngredient>> GetAllRecipeIngredients();
       
    }
}
