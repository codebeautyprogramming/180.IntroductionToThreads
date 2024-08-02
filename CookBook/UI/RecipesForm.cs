using CookBook.Helpers;
using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using DataAccessLayer.Repositories;
using DomainModel.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookBook.UI
{
    public partial class RecipesForm : Form
    {
        private readonly IRecipeTypesRepository _recipeTypesRepository;
        private readonly IRecipesRepository _recipesRepository;
        private readonly IServiceProvider _serviceProvider;

        private bool _isUserImageAdded = false;
        private int _recipeToEditId;
        private List<RecipeWithType> _recipesCache;

        public RecipesForm(IRecipeTypesRepository recipeTypesRepository, IServiceProvider serviceProvider, IRecipesRepository recipesRepository)
        {
            InitializeComponent();
            _recipeTypesRepository = recipeTypesRepository;
            _recipesRepository = recipesRepository;
            _serviceProvider = serviceProvider;
            _recipesRepository.OnError += message => MessageBox.Show(message);
        }

        private async Task RefreshRecipeTypes()
        {
            RecipeType previouslySelectedFilter = (RecipeType)RecipesFilterCbx.SelectedItem;
            RecipeType previouslySelectedRecipeType = (RecipeType)RecipeTypesCbx.SelectedItem;

            List<RecipeType> recipeTypes = await _recipeTypesRepository.GetRecipeTypes();

            RecipeTypesCbx.DataSource = recipeTypes;
            RecipeTypesCbx.DisplayMember = "Name";

            List<RecipeType> filterList = new List<RecipeType>();
            filterList.Add(new RecipeType("All recipe types", 0));
            filterList.AddRange(recipeTypes);

            RecipesFilterCbx.DataSource = filterList;
            RecipesFilterCbx.DisplayMember = "Name";

            if (previouslySelectedFilter != null && previouslySelectedFilter.Id != 0)
            {
                int indexToSelect = FindRecipeTypeIndex(previouslySelectedFilter.Id);
                RecipesFilterCbx.SelectedIndex = indexToSelect + 1;
            }

            if (previouslySelectedRecipeType != null && previouslySelectedRecipeType.Id != 0)
            {
                int indexToSelect = FindRecipeTypeIndex(previouslySelectedRecipeType.Id);
                RecipeTypesCbx.SelectedIndex = indexToSelect;
            }
        }
        private async void RefreshRecipesCache()
        {
            _recipesCache = await _recipesRepository.GetRecipes();

            FilterGridData();
        }

        private void FilterGridData()
        {
            RecipeType selectedType = (RecipeType)RecipesFilterCbx.SelectedItem;

            if (selectedType.Id == 0)
                RecipesGrid.DataSource = _recipesCache;
            else
                RecipesGrid.DataSource = _recipesCache.Where(r => r.RecipeTypeId == selectedType.Id).ToList();
        }

        private async void RecipesForm_Load(object sender, EventArgs e)
        {
            CustomizeGridAppearance();
            await RefreshRecipeTypes();
            RefreshRecipesCache();
            RecipePictureBox.Image = ImageHelper.PlaceholderImage;
            RecipePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            EditRecipeButton.Visible = false;
        }

        private void AddRecipeTypeBtn_Click(object sender, EventArgs e)
        {
            RecipeTypesForm form = _serviceProvider.GetRequiredService<RecipeTypesForm>();
            form.FormClosed += (sender, e) => RefreshRecipeTypes();
            form.ShowDialog();
        }

        private async void AddRecipeBtn_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            byte[] image = null;
            if (_isUserImageAdded)
                image = ImageHelper.ConvertToDbImage(RecipePictureBox.ImageLocation);
            int recipeTypeId = ((RecipeType)RecipeTypesCbx.SelectedItem).Id;
            Recipe newRecipe = new Recipe(NameTxt.Text, DescriptionTxt.Text, image, recipeTypeId);

            await _recipesRepository.AddRecipe(newRecipe);
            ClearAllFields();
            RefreshRecipesCache();
        }

        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;
            DescriptionTxt.Text = string.Empty;
            RecipePictureBox.ImageLocation = string.Empty;
            RecipePictureBox.Image = ImageHelper.PlaceholderImage;
            _isUserImageAdded = false;
        }

        private bool IsValid()
        {
            bool isValid = true;
            string message = "";

            if (string.IsNullOrEmpty(NameTxt.Text))
            {
                isValid = false;
                message += "Please enter name.\n\n";
            }
            if (string.IsNullOrEmpty(DescriptionTxt.Text))
            {
                isValid = false;
                message += "Please enter description.\n\n";
            }

            if (!isValid)
                MessageBox.Show(message, "Form not valid!");

            return isValid;
        }

        private void RecipePictureBox_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Please select an image",
                Filter = "PNG|*.png|JPG|*.jpg|JPEG|*.jpeg",
                Multiselect = false
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RecipePictureBox.ImageLocation = openFileDialog.FileName;
                    _isUserImageAdded = true;
                }
            }
        }

        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            EditRecipeButton.Visible = false;
        }

        private void CustomizeGridAppearance()
        {
            RecipesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            RecipesGrid.AutoGenerateColumns = false;

            DataGridViewColumn[] columns = new DataGridViewColumn[7];
            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name" };
            columns[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "Description", HeaderText = "Description" };
            columns[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "Type", HeaderText = "Type" };
            columns[4] = new DataGridViewButtonColumn()
            {
                Text = "Edit",
                Name = "EditBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };
            columns[5] = new DataGridViewButtonColumn()
            {
                Text = "Delete",
                Name = "DeleteBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };
            columns[6] = new DataGridViewButtonColumn()
            {
                Text = "Ingredients",
                Name = "IngredientsBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };

            RecipesGrid.RowHeadersVisible = false;
            RecipesGrid.Columns.Clear();
            RecipesGrid.Columns.AddRange(columns);
        }

        private async void RecipesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && RecipesGrid.CurrentCell is DataGridViewButtonCell)
            {
                RecipeWithType clickedRecipe = (RecipeWithType)RecipesGrid.Rows[e.RowIndex].DataBoundItem;

                if (RecipesGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _recipesRepository.DeleteRecipe(clickedRecipe.Id);
                    RefreshRecipesCache();
                }
                else if (RecipesGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedRecipe);
                    EditRecipeButton.Visible = true;
                    _recipeToEditId = clickedRecipe.Id;
                }
                else if (RecipesGrid.CurrentCell.OwningColumn.Name == "IngredientsBtn")
                {
                    RecipeIngredientsForm form = _serviceProvider.GetRequiredService<RecipeIngredientsForm>();
                    form.RecipeId = clickedRecipe.Id;
                    form.ShowDialog();
                }
            }
        }

        private void FillFormForEdit(RecipeWithType clickedRecipe)
        {
            NameTxt.Text = clickedRecipe.Name;
            DescriptionTxt.Text = clickedRecipe.Description;
            if (clickedRecipe.Image != null)
                RecipePictureBox.Image = ImageHelper.ConvertFromDbImage(clickedRecipe.Image);
            else
                RecipePictureBox.Image = ImageHelper.PlaceholderImage;

            RecipeTypesCbx.SelectedIndex = FindRecipeTypeIndex(clickedRecipe.RecipeTypeId);
        }

        private int FindRecipeTypeIndex(int recipeTypeId)
        {
            List<RecipeType> allRecipeTypes = (List<RecipeType>)RecipeTypesCbx.DataSource;

            RecipeType matchingRecipeType = allRecipeTypes.FirstOrDefault(rt => rt.Id == recipeTypeId);

            int index = RecipeTypesCbx.Items.IndexOf(matchingRecipeType);
            return index;
        }

        private async void EditRecipeButton_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            byte[] image = null;
            if (_isUserImageAdded)
                image = ImageHelper.ConvertToDbImage(RecipePictureBox.ImageLocation);
            int recipeTypeId = ((RecipeType)RecipeTypesCbx.SelectedItem).Id;
            Recipe recipe = new Recipe(NameTxt.Text, DescriptionTxt.Text, image, recipeTypeId, _recipeToEditId);

            await _recipesRepository.EditRecipe(recipe);
            ClearAllFields();
            RefreshRecipesCache();
            EditRecipeButton.Visible = false;
        }

        private void RecipesFilterCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGridData();
        }

    }
}
