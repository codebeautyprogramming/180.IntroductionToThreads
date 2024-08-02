namespace CookBook.UI
{
    partial class AmountForm
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
            AmountNum = new NumericUpDown();
            OkBtn = new Button();
            CancelBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)AmountNum).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 24);
            label1.Name = "label1";
            label1.Size = new Size(144, 30);
            label1.TabIndex = 0;
            label1.Text = "Enter amount:";
            // 
            // AmountNum
            // 
            AmountNum.Location = new Point(31, 78);
            AmountNum.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AmountNum.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            AmountNum.Name = "AmountNum";
            AmountNum.Size = new Size(400, 35);
            AmountNum.TabIndex = 1;
            // 
            // OkBtn
            // 
            OkBtn.Location = new Point(31, 146);
            OkBtn.Name = "OkBtn";
            OkBtn.Size = new Size(178, 48);
            OkBtn.TabIndex = 2;
            OkBtn.Text = "Ok";
            OkBtn.UseVisualStyleBackColor = true;
            OkBtn.Click += OkBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Location = new Point(255, 146);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(176, 48);
            CancelBtn.TabIndex = 3;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // AmountForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(479, 228);
            Controls.Add(CancelBtn);
            Controls.Add(OkBtn);
            Controls.Add(AmountNum);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "AmountForm";
            Text = "AmountForm";
            ((System.ComponentModel.ISupportInitialize)AmountNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown AmountNum;
        private Button OkBtn;
        private Button CancelBtn;
    }
}