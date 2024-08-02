namespace CookBook.UI
{
    partial class RecipeIngredientsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            AddIngredientBtn = new Button();
            RemoveIngredientBtn = new Button();
            AllIngredientsCustomLbx = new CustomUserControls.CustomListBox();
            RecipeIngredientsCustomLbx = new CustomUserControls.CustomListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(85, 31);
            label1.Name = "label1";
            label1.Size = new Size(146, 30);
            label1.TabIndex = 0;
            label1.Text = "All ingredients";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(387, 31);
            label2.Name = "label2";
            label2.Size = new Size(183, 30);
            label2.TabIndex = 1;
            label2.Text = "Recipe ingredients";
            // 
            // AddIngredientBtn
            // 
            AddIngredientBtn.Location = new Point(30, 431);
            AddIngredientBtn.Name = "AddIngredientBtn";
            AddIngredientBtn.Size = new Size(274, 48);
            AddIngredientBtn.TabIndex = 4;
            AddIngredientBtn.Text = "Add ingredient";
            AddIngredientBtn.UseVisualStyleBackColor = true;
            AddIngredientBtn.Click += AddIngredientBtn_Click;
            // 
            // RemoveIngredientBtn
            // 
            RemoveIngredientBtn.Location = new Point(343, 431);
            RemoveIngredientBtn.Name = "RemoveIngredientBtn";
            RemoveIngredientBtn.Size = new Size(269, 48);
            RemoveIngredientBtn.TabIndex = 5;
            RemoveIngredientBtn.Text = "Remove ingredient";
            RemoveIngredientBtn.UseVisualStyleBackColor = true;
            RemoveIngredientBtn.Click += RemoveIngredientBtn_Click;
            // 
            // AllIngredientsCustomLbx
            // 
            AllIngredientsCustomLbx.Location = new Point(30, 79);
            AllIngredientsCustomLbx.Margin = new Padding(3, 4, 3, 4);
            AllIngredientsCustomLbx.Name = "AllIngredientsCustomLbx";
            AllIngredientsCustomLbx.SelectedItem = null;
            AllIngredientsCustomLbx.Size = new Size(274, 334);
            AllIngredientsCustomLbx.TabIndex = 6;
            // 
            // RecipeIngredientsCustomLbx
            // 
            RecipeIngredientsCustomLbx.Location = new Point(343, 79);
            RecipeIngredientsCustomLbx.Name = "RecipeIngredientsCustomLbx";
            RecipeIngredientsCustomLbx.SelectedItem = null;
            RecipeIngredientsCustomLbx.Size = new Size(269, 334);
            RecipeIngredientsCustomLbx.TabIndex = 7;
            // 
            // RecipeIngredientsForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(656, 521);
            Controls.Add(RecipeIngredientsCustomLbx);
            Controls.Add(AllIngredientsCustomLbx);
            Controls.Add(RemoveIngredientBtn);
            Controls.Add(AddIngredientBtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "RecipeIngredientsForm";
            Text = "RecipeIngredientsForm";
            Load += RecipeIngredientsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button AddIngredientBtn;
        private Button RemoveIngredientBtn;
        private CustomUserControls.CustomListBox AllIngredientsCustomLbx;
        private CustomUserControls.CustomListBox RecipeIngredientsCustomLbx;
    }
}