namespace NewspaperSellerSimulation
{
    partial class Form1
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
            this.BrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.outputGrid = new System.Windows.Forms.DataGridView();
            this.excessDeamndText = new System.Windows.Forms.Label();
            this.unsoldText = new System.Windows.Forms.Label();
            this.excessPaperNumber = new System.Windows.Forms.Label();
            this.unsoldNumberText = new System.Windows.Forms.Label();
            this.testText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(41, 117);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(147, 61);
            this.BrowseButton.TabIndex = 0;
            this.BrowseButton.TabStop = false;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(37, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a test case";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 61);
            this.button2.TabIndex = 2;
            this.button2.TabStop = false;
            this.button2.Text = "Run";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.runButton_click);
            // 
            // outputGrid
            // 
            this.outputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputGrid.Location = new System.Drawing.Point(258, 117);
            this.outputGrid.Name = "outputGrid";
            this.outputGrid.RowHeadersWidth = 51;
            this.outputGrid.RowTemplate.Height = 24;
            this.outputGrid.Size = new System.Drawing.Size(1178, 585);
            this.outputGrid.TabIndex = 13;
            this.outputGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // excessDeamndText
            // 
            this.excessDeamndText.AutoSize = true;
            this.excessDeamndText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.excessDeamndText.Location = new System.Drawing.Point(4, 366);
            this.excessDeamndText.Name = "excessDeamndText";
            this.excessDeamndText.Size = new System.Drawing.Size(203, 20);
            this.excessDeamndText.TabIndex = 14;
            this.excessDeamndText.Text = "total excess demand days";
            this.excessDeamndText.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // unsoldText
            // 
            this.unsoldText.AutoSize = true;
            this.unsoldText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.unsoldText.Location = new System.Drawing.Point(12, 479);
            this.unsoldText.Name = "unsoldText";
            this.unsoldText.Size = new System.Drawing.Size(182, 20);
            this.unsoldText.TabIndex = 15;
            this.unsoldText.Text = "total unsold paper days";
            // 
            // excessPaperNumber
            // 
            this.excessPaperNumber.AutoSize = true;
            this.excessPaperNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.excessPaperNumber.Location = new System.Drawing.Point(10, 401);
            this.excessPaperNumber.Name = "excessPaperNumber";
            this.excessPaperNumber.Size = new System.Drawing.Size(182, 20);
            this.excessPaperNumber.TabIndex = 16;
            this.excessPaperNumber.Text = "total unsold paper days";
            // 
            // unsoldNumberText
            // 
            this.unsoldNumberText.AutoSize = true;
            this.unsoldNumberText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.unsoldNumberText.Location = new System.Drawing.Point(12, 509);
            this.unsoldNumberText.Name = "unsoldNumberText";
            this.unsoldNumberText.Size = new System.Drawing.Size(182, 20);
            this.unsoldNumberText.TabIndex = 17;
            this.unsoldNumberText.Text = "total unsold paper days";
            // 
            // testText
            // 
            this.testText.AutoSize = true;
            this.testText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.testText.Location = new System.Drawing.Point(792, 748);
            this.testText.Name = "testText";
            this.testText.Size = new System.Drawing.Size(72, 24);
            this.testText.TabIndex = 18;
            this.testText.Text = "test text";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1506, 867);
            this.Controls.Add(this.testText);
            this.Controls.Add(this.unsoldNumberText);
            this.Controls.Add(this.excessPaperNumber);
            this.Controls.Add(this.unsoldText);
            this.Controls.Add(this.excessDeamndText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.outputGrid);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.Text = "News Paper Seller";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView outputGrid;
        private System.Windows.Forms.Label excessDeamndText;
        private System.Windows.Forms.Label unsoldText;
        private System.Windows.Forms.Label excessPaperNumber;
        private System.Windows.Forms.Label unsoldNumberText;
        private System.Windows.Forms.Label testText;
    }
}