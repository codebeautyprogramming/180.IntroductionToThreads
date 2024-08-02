using CookBook.Helpers;
using CookBook.Services;
using CookBook.ViewModels;
using DomainModel.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookBook.UI
{
    public enum RecipeAvailability { Available, Unavailable };
    public partial class FoodManagerForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        FoodManagerCache _foodManagerCache;
        public FoodManagerForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _foodManagerCache = _serviceProvider.GetRequiredService<FoodManagerCache>();
            RecipesLbx.OnSelectedItemChanged += OnSelectedRecipeChanged;
        }

        private void OnSelectedRecipeChanged(ListBoxItemVM selectedItem)
        {
            Recipe selectedRecipe = (Recipe)selectedItem.Item;
            var ingredients = _foodManagerCache.GetIngredients(selectedRecipe.Id);
            List<ListBoxItemVM> dataSource = new List<ListBoxItemVM>();

            decimal totalKcal = 0;
            decimal totalPrice = 0;

            foreach (RecipeIngredientExtendedVM ingredient in ingredients)
            {
                ListBoxItemVM item = new ListBoxItemVM(ingredient, ingredient.NameWithMissingAmount);
                dataSource.Add(item);

                totalKcal += (ingredient.KcalPer100g / 100) * ingredient.Amount;
                totalPrice += (ingredient.PricePer100g / 100) * ingredient.Amount;
            }
            IngredientsLbx.SetDataSource(dataSource);

            DescriptionTxt.Text = selectedRecipe.Description;
            if (selectedRecipe.Image != null)
                RecipePicture.Image = ImageHelper.ConvertFromDbImage(selectedRecipe.Image);
            else
                RecipePicture.Image = ImageHelper.PlaceholderImage;

            TotalCaloriesLbl.Text = totalKcal.ToString();
            TotalPriceLbl.Text = totalPrice.ToString();
        }

        private async void FoodManagerForm_Load(object sender, EventArgs e)
        {
            RecipePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            await _foodManagerCache.RefreshData();
            RefreshRecipesLbx(RecipeAvailability.Available);
        }

        private void AvailableBtn_Click(object sender, EventArgs e)
        {
            RefreshRecipesLbx(RecipeAvailability.Available);
        }

        private void UnavailableBtn_Click(object sender, EventArgs e)
        {
            RefreshRecipesLbx(RecipeAvailability.Unavailable);
        }

        private void RefreshRecipesLbx(RecipeAvailability recipeAvailability)
        {
            List<ListBoxItemVM> dataSource = new List<ListBoxItemVM>();
            List<Recipe> recipes = new List<Recipe>();
            if (recipeAvailability == RecipeAvailability.Available)
            {
                recipes = _foodManagerCache.AvailableRecipes;
                PrepareFoodBtn.Visible = true;
                CreateShoppingListBtn.Visible = false;
            }
            else if (recipeAvailability == RecipeAvailability.Unavailable)
            {
                recipes = _foodManagerCache.UnavailableRecipes;
                PrepareFoodBtn.Visible = false;
                CreateShoppingListBtn.Visible = true;
            }

            foreach (Recipe recipe in recipes)
            {
                ListBoxItemVM item = new ListBoxItemVM(recipe, recipe.Name);
                dataSource.Add(item);
            }
            RecipesLbx.SetDataSource(dataSource);
        }

        private async void PrepareFoodBtn_Click(object sender, EventArgs e)
        {
            if (RecipesLbx.SelectedItem == null)
                return;

            Recipe selectedRecipe = (Recipe)RecipesLbx.SelectedItem.Item;

            await _foodManagerCache.PrepareFood(selectedRecipe);

            await _foodManagerCache.RefreshData();
            RefreshRecipesLbx(RecipeAvailability.Available);
        }

        private void CreateShoppingListBtn_Click(object sender, EventArgs e)
        {
            if (!_foodManagerCache.UnavailableRecipes.Any())
            {
                MessageBox.Show("There are no unavailable recipes!");
                return;
            }

            string shoppingList = "";

            foreach(Recipe recipe in _foodManagerCache.UnavailableRecipes) 
            {
                shoppingList += $"Missing ingredients for {recipe.Name}\n";

                var recipeIngredients = _foodManagerCache.GetIngredients(recipe.Id);

                foreach(var ingredient in recipeIngredients)
                {
                    if (ingredient.MissingAmount != 0)
                        shoppingList += $"{ingredient.Name} {ingredient.MissingAmount}g \n";
                }

                shoppingList += "\n";
            }

            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "ShoppingList.txt");

                using(StreamWriter sw = new StreamWriter(filePath)) 
                {
                    sw.Write(shoppingList);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error while creating shopping list.");
            }
        }
    }
}
