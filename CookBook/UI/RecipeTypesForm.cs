using CookBook.ViewModels;
using DataAccessLayer.Contracts;
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
    public partial class RecipeTypesForm : Form
    {
        private readonly IRecipeTypesRepository _recipeTypesRepository;
        public RecipeTypesForm(IRecipeTypesRepository recipeTypesRepository)
        {
            InitializeComponent();
            _recipeTypesRepository = recipeTypesRepository;
            _recipeTypesRepository.OnError += message=> MessageBox.Show(message);
        }

        private void RecipeTypesForm_Load(object sender, EventArgs e)
        {
            RefreshRecipeTypesList();
        }

        private async void RefreshRecipeTypesList()
        {
            List<RecipeType> recipeTypes = await _recipeTypesRepository.GetRecipeTypes();

            List<ListBoxItemVM> customLbxItems = new List<ListBoxItemVM>();

            foreach (var t in recipeTypes)
            {
                customLbxItems.Add(new ListBoxItemVM(t, t.Name));
            }
            RecipeTypesCustomLbx.SetDataSource(customLbxItems);
        }

        private async void AddRecipeTypeBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewTypeTxt.Text))
                return;

            RecipeType newRecipeType = new RecipeType(NewTypeTxt.Text);

            await _recipeTypesRepository.AddRecipeType(newRecipeType);
            RefreshRecipeTypesList();
            NewTypeTxt.Text = "";
        }
    }
}
