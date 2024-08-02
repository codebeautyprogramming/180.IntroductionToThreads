using CookBook.CustomUserControls;
using CookBook.ViewModels;
using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookBook.UI
{
    public partial class RecipeIngredientsForm : Form
    {
        private readonly IRecipeIngredientsRepository _recipeIngredientsRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        public int RecipeId { get; set; }
        public RecipeIngredientsForm(IRecipeIngredientsRepository recipeIngredientsRepository, IIngredientsRepository ingredientsRepository)
        {
            InitializeComponent();
            _recipeIngredientsRepository = recipeIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        private void RecipeIngredientsForm_Load(object sender, EventArgs e)
        {
            RefreshRecipeIngredients();
            RefreshAllIngredients();
        }

        private async void RefreshRecipeIngredients()
        {
            List<RecipeIngredientWithNameAndAmount> results = await _recipeIngredientsRepository.GetRecipeIngredients(RecipeId);
            List<RecipeIngredientVM> recipeIngredients = new List<RecipeIngredientVM>();

            foreach (var ingredient in results)
            {
                recipeIngredients.Add(new RecipeIngredientVM(ingredient.IngredientId, ingredient.Name, ingredient.Amount));
            }

            List<ListBoxItemVM> customLbxItems= new List<ListBoxItemVM>();

            foreach(var ri in recipeIngredients)
            {
                customLbxItems.Add(new ListBoxItemVM(ri, ri.NameWithAmount));
            }

            RecipeIngredientsCustomLbx.SetDataSource(customLbxItems);
        }
        private async void RefreshAllIngredients()
        {
            //AllIngredientsLbx.DataSource = await _ingredientsRepository.GetIngredients();
            //AllIngredientsLbx.DisplayMember = "Name";

            List<Ingredient> allIngredients = await _ingredientsRepository.GetIngredients();

            List<ListBoxItemVM> itemsList =new List<ListBoxItemVM>();
            foreach(Ingredient i in allIngredients)
            {
                itemsList.Add(new ListBoxItemVM(i, i.Name));
            }

            AllIngredientsCustomLbx.SetDataSource(itemsList);

        }

        private async void AddIngredientBtn_Click(object sender, EventArgs e)
        {
            if (AllIngredientsCustomLbx.SelectedItem != null && AllIngredientsCustomLbx.SelectedItem.Item != null)
            {
                AmountForm amountForm = new AmountForm();
                if (amountForm.ShowDialog() == DialogResult.OK)
                {
                    Ingredient selectedIngredient = (Ingredient)AllIngredientsCustomLbx.SelectedItem.Item;

                    RecipeIngredient newRecipeIngredient = new RecipeIngredient(RecipeId, selectedIngredient.Id, amountForm.Amount);

                    bool isExistingIngredient = CheckIsExistingIngredient(selectedIngredient.Id, RecipeIngredientsCustomLbx.DataSource);

                    if (isExistingIngredient)
                        await _recipeIngredientsRepository.EditRecipeIngredientAmount(newRecipeIngredient);
                    else
                        await _recipeIngredientsRepository.AddRecipeIngredient(newRecipeIngredient);

                    RefreshRecipeIngredients();
                }
            }
        }

        private bool CheckIsExistingIngredient(int selectedIngredientId, List<ListBoxItemVM> dataSource)
        {
            foreach (ListBoxItemVM customLbxItem in dataSource) 
            { 
                RecipeIngredientVM ri = customLbxItem.Item as RecipeIngredientVM;
                if(ri.IngredientId == selectedIngredientId)
                    return true;
            }
            return false;
        }

        private async void RemoveIngredientBtn_Click(object sender, EventArgs e)
        {
            if(RecipeIngredientsCustomLbx.SelectedItem!=null)
            {
                RecipeIngredientVM ingredient = (RecipeIngredientVM)RecipeIngredientsCustomLbx.SelectedItem.Item;

                await _recipeIngredientsRepository.DeleteRecipeIngredient(ingredient.IngredientId, RecipeId);

                RefreshRecipeIngredients();
            }
        }
    }
}
