using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DomainModel.Models;

namespace DataAccessLayer.Repositories
{
    public class RecipeIngredientsRepository : IRecipeIngredientsRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string errorMessage)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);
        }
        public async Task<List<RecipeIngredientWithNameAndAmount>> GetRecipeIngredients(int recipeId)
        {
            try
            {
                string query = @$"select i.Name, ri.IngredientId, ri.Amount
                                  from Ingredients i join RecipeIngredients ri
                                  on i.Id=ri.IngredientId
                                  where ri.RecipeId={recipeId}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<RecipeIngredientWithNameAndAmount>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting recipe ingredients.";
                ErrorOccured(errorMessage);
                return new List<RecipeIngredientWithNameAndAmount>();
            }
        }

        public async Task AddRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            try
            {
                string query = @"insert into RecipeIngredients 
                (RecipeId, IngredientId, Amount) 
                values (@RecipeId, @IngredientId, @Amount)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, recipeIngredient);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while adding recipe ingredient.";
                ErrorOccured(errorMessage);
            }
        }

        public async Task DeleteRecipeIngredient(int ingredientId, int recipeId)
        {
            try
            {
                string query = @$"delete from RecipeIngredients where IngredientId={ingredientId} and RecipeId={recipeId}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while deleting recipe ingredient.";
                ErrorOccured(errorMessage);
            }
        }

        public async Task EditRecipeIngredientAmount(RecipeIngredient recipeIngredient)
        {
            try
            {
                string query = @"update RecipeIngredients
                             set Amount = @Amount
                             where IngredientId=@IngredientId and RecipeId=@RecipeId";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, recipeIngredient);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while editing recipe ingredient amount.";
                ErrorOccured(errorMessage);
            }

        }

        public async Task<List<RecipeIngredient>> GetAllRecipeIngredients()
        {
            try
            {
                string query = "select * from RecipeIngredients";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<RecipeIngredient>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error happened while getting all recipe ingredients.";
                ErrorOccured(errorMessage);
                return new List<RecipeIngredient>();
            }
        }

       
    }
}
