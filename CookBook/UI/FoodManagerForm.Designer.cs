namespace CookBook.UI
{
    partial class FoodManagerForm
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
            LeftPanel = new Panel();
            CreateShoppingListBtn = new Button();
            RecipesLbx = new CustomUserControls.CustomListBox();
            UnavailableBtn = new Button();
            AvailableBtn = new Button();
            PrepareFoodBtn = new Button();
            RightPanel = new Panel();
            IngredientsLbx = new CustomUserControls.CustomListBox();
            RecipePicture = new PictureBox();
            TotalPriceLbl = new Label();
            TotalCaloriesLbl = new Label();
            DescriptionTxt = new RichTextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            LeftPanel.SuspendLayout();
            RightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RecipePicture).BeginInit();
            SuspendLayout();
            // 
            // LeftPanel
            // 
            LeftPanel.Controls.Add(CreateShoppingListBtn);
            LeftPanel.Controls.Add(RecipesLbx);
            LeftPanel.Controls.Add(UnavailableBtn);
            LeftPanel.Controls.Add(AvailableBtn);
            LeftPanel.Controls.Add(PrepareFoodBtn);
            LeftPanel.Dock = DockStyle.Left;
            LeftPanel.Location = new Point(0, 0);
            LeftPanel.Name = "LeftPanel";
            LeftPanel.Size = new Size(394, 622);
            LeftPanel.TabIndex = 0;
            // 
            // CreateShoppingListBtn
            // 
            CreateShoppingListBtn.Location = new Point(24, 542);
            CreateShoppingListBtn.Name = "CreateShoppingListBtn";
            CreateShoppingListBtn.Size = new Size(346, 55);
            CreateShoppingListBtn.TabIndex = 3;
            CreateShoppingListBtn.Text = "Create shopping list";
            CreateShoppingListBtn.UseVisualStyleBackColor = true;
            CreateShoppingListBtn.Click += CreateShoppingListBtn_Click;
            // 
            // RecipesLbx
            // 
            RecipesLbx.Location = new Point(24, 78);
            RecipesLbx.Name = "RecipesLbx";
            RecipesLbx.SelectedItem = null;
            RecipesLbx.Size = new Size(346, 433);
            RecipesLbx.TabIndex = 2;
            // 
            // UnavailableBtn
            // 
            UnavailableBtn.Location = new Point(200, 12);
            UnavailableBtn.Name = "UnavailableBtn";
            UnavailableBtn.Size = new Size(170, 47);
            UnavailableBtn.TabIndex = 1;
            UnavailableBtn.Text = "Unavailable";
            UnavailableBtn.UseVisualStyleBackColor = true;
            UnavailableBtn.Click += UnavailableBtn_Click;
            // 
            // AvailableBtn
            // 
            AvailableBtn.Location = new Point(24, 12);
            AvailableBtn.Name = "AvailableBtn";
            AvailableBtn.Size = new Size(170, 47);
            AvailableBtn.TabIndex = 0;
            AvailableBtn.Text = "Available";
            AvailableBtn.UseVisualStyleBackColor = true;
            AvailableBtn.Click += AvailableBtn_Click;
            // 
            // PrepareFoodBtn
            // 
            PrepareFoodBtn.Location = new Point(24, 542);
            PrepareFoodBtn.Name = "PrepareFoodBtn";
            PrepareFoodBtn.Size = new Size(346, 55);
            PrepareFoodBtn.TabIndex = 4;
            PrepareFoodBtn.Text = "Prepare food";
            PrepareFoodBtn.UseVisualStyleBackColor = true;
            PrepareFoodBtn.Click += PrepareFoodBtn_Click;
            // 
            // RightPanel
            // 
            RightPanel.Controls.Add(IngredientsLbx);
            RightPanel.Controls.Add(RecipePicture);
            RightPanel.Controls.Add(TotalPriceLbl);
            RightPanel.Controls.Add(TotalCaloriesLbl);
            RightPanel.Controls.Add(DescriptionTxt);
            RightPanel.Controls.Add(label3);
            RightPanel.Controls.Add(label2);
            RightPanel.Controls.Add(label1);
            RightPanel.Dock = DockStyle.Fill;
            RightPanel.Location = new Point(394, 0);
            RightPanel.Name = "RightPanel";
            RightPanel.Size = new Size(785, 622);
            RightPanel.TabIndex = 1;
            // 
            // IngredientsLbx
            // 
            IngredientsLbx.Location = new Point(20, 78);
            IngredientsLbx.Name = "IngredientsLbx";
            IngredientsLbx.SelectedItem = null;
            IngredientsLbx.Size = new Size(397, 268);
            IngredientsLbx.TabIndex = 12;
            // 
            // RecipePicture
            // 
            RecipePicture.Location = new Point(441, 363);
            RecipePicture.Name = "RecipePicture";
            RecipePicture.Size = new Size(317, 234);
            RecipePicture.TabIndex = 11;
            RecipePicture.TabStop = false;
            // 
            // TotalPriceLbl
            // 
            TotalPriceLbl.AutoSize = true;
            TotalPriceLbl.Location = new Point(560, 78);
            TotalPriceLbl.Name = "TotalPriceLbl";
            TotalPriceLbl.Size = new Size(21, 30);
            TotalPriceLbl.TabIndex = 10;
            TotalPriceLbl.Text = "/";
            // 
            // TotalCaloriesLbl
            // 
            TotalCaloriesLbl.AutoSize = true;
            TotalCaloriesLbl.Location = new Point(585, 29);
            TotalCaloriesLbl.Name = "TotalCaloriesLbl";
            TotalCaloriesLbl.Size = new Size(21, 30);
            TotalCaloriesLbl.TabIndex = 9;
            TotalCaloriesLbl.Text = "/";
            // 
            // DescriptionTxt
            // 
            DescriptionTxt.Location = new Point(20, 363);
            DescriptionTxt.Name = "DescriptionTxt";
            DescriptionTxt.Size = new Size(397, 234);
            DescriptionTxt.TabIndex = 8;
            DescriptionTxt.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(441, 78);
            label3.Name = "label3";
            label3.Size = new Size(113, 30);
            label3.TabIndex = 7;
            label3.Text = "Total price:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(441, 29);
            label2.Name = "label2";
            label2.Size = new Size(138, 30);
            label2.TabIndex = 6;
            label2.Text = "Total calories:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 29);
            label1.Name = "label1";
            label1.Size = new Size(117, 30);
            label1.TabIndex = 5;
            label1.Text = "Ingredients";
            // 
            // FoodManagerForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1179, 622);
            Controls.Add(RightPanel);
            Controls.Add(LeftPanel);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "FoodManagerForm";
            Text = "FoodManagerForm";
            Load += FoodManagerForm_Load;
            LeftPanel.ResumeLayout(false);
            RightPanel.ResumeLayout(false);
            RightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RecipePicture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel LeftPanel;
        private Panel RightPanel;
        private Button CreateShoppingListBtn;
        private CustomUserControls.CustomListBox RecipesLbx;
        private Button UnavailableBtn;
        private Button AvailableBtn;
        private Button PrepareFoodBtn;
        private PictureBox RecipePicture;
        private Label TotalPriceLbl;
        private Label TotalCaloriesLbl;
        private RichTextBox DescriptionTxt;
        private Label label3;
        private Label label2;
        private Label label1;
        private CustomUserControls.CustomListBox IngredientsLbx;
    }
}