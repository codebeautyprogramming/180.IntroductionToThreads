using CookBook.ViewModels;
using DataAccessLayer.Contracts;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Services
{
    public class FoodManagerCache
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly IRecipeIngredientsRepository _recipeIngredientsRepository;

        private List<Ingredient> _ingredients;
        private List<Recipe> _recipes;
        private List<RecipeIngredient> _recipeIngredients;

        public List<Recipe> AvailableRecipes;
        public List<Recipe> UnavailableRecipes;

        public FoodManagerCache(IIngredientsRepository ingredientsRepository, IRecipesRepository recipesRepository, IRecipeIngredientsRepository recipeIngredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
            _recipesRepository = recipesRepository;
            _recipeIngredientsRepository = recipeIngredientsRepository;

            _ingredientsRepository.OnSuccess += ShowSuccessMessage;
            _ingredientsRepository.OnError += ShowErrorMessage;
        }

        private void ShowErrorMessage(string errorMessage)
        {
    //Bad SoC - Service should not manipulate UI elements directly, UI Form should do that
            MessageBox.Show(errorMessage);
        }

        private void ShowSuccessMessage(string successMessage)
        {
    //Bad SoC - Service should not manipulate UI elements directly, UI Form should do that
            MessageBox.Show(successMessage);
        }

        public async Task RefreshData()
        {
            _ingredients = await _ingredientsRepository.GetIngredients();
            _recipeIngredients = await _recipeIngredientsRepository.GetAllRecipeIngredients();
            _recipes = await _recipesRepository.GetAllRecipes();

            ClassifyRecipes();
        }

        public void ClassifyRecipes()
        {
            AvailableRecipes = new List<Recipe>();
            UnavailableRecipes = new List<Recipe>();

            var groupedRecipesAndIngredients = _recipeIngredients.GroupBy(ri=>ri.RecipeId).ToList();

            foreach(var recipeGroup in groupedRecipesAndIngredients)
            {
                int recipeId= recipeGroup.Key;
                bool isRecipeAvailable = true;

                foreach(var ri in recipeGroup)
                {
                    Ingredient fi = _ingredients.FirstOrDefault(i => i.Id == ri.IngredientId);

                    if(fi==null || fi.Weight < ri.Amount) 
                    { 
                        isRecipeAvailable = false;
                        break;
                    }
                }

                Recipe recipeToAdd = _recipes.FirstOrDefault(r => r.Id == recipeId);

                if (isRecipeAvailable)
                    AvailableRecipes.Add(recipeToAdd);
                else
                    UnavailableRecipes.Add(recipeToAdd);

            }
        }

        public List<RecipeIngredientExtendedVM> GetIngredients (int selectedRecipeId)
        {
            List<RecipeIngredientExtendedVM> ingredientsToReturn = new List<RecipeIngredientExtendedVM>();

            var selectedRecipeIngredients = _recipeIngredients.GroupBy(ri => ri.RecipeId).FirstOrDefault(g => g.Key == selectedRecipeId);

            foreach(RecipeIngredient sri in selectedRecipeIngredients)
            {
                Ingredient fi = _ingredients.FirstOrDefault(i=>i.Id== sri.IngredientId);
              
                decimal missingAmount = Math.Max(0, sri.Amount - fi.Weight);

                ingredientsToReturn.Add(new RecipeIngredientExtendedVM(fi.Id,fi.Name,sri.Amount, missingAmount, fi.KcalPer100g, fi.PricePer100g));
            }

            return ingredientsToReturn;
        }

        public async Task PrepareFood(Recipe selectedRecipe)
        {
            List<RecipeIngredient> recipeIngredients = _recipeIngredients.Where(ri=>ri.RecipeId== selectedRecipe.Id).ToList();

            await _ingredientsRepository.UpdateAmounts(recipeIngredients);
        }
    }
}
