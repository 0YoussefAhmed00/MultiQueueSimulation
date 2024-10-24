namespace MultiQueueSimulation
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvInterarrival = new System.Windows.Forms.DataGridView();
            this.serverBox = new System.Windows.Forms.ComboBox();
            this.runButton = new System.Windows.Forms.Button();
            this.outputGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterarrival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(574, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to our project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(309, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Import your test case:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(347, 76);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvInterarrival
            // 
            this.dgvInterarrival.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvInterarrival.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInterarrival.Location = new System.Drawing.Point(36, 138);
            this.dgvInterarrival.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInterarrival.Name = "dgvInterarrival";
            this.dgvInterarrival.RowHeadersWidth = 51;
            this.dgvInterarrival.RowTemplate.Height = 24;
            this.dgvInterarrival.Size = new System.Drawing.Size(695, 240);
            this.dgvInterarrival.TabIndex = 4;
            // 
            // serverBox
            // 
            this.serverBox.BackColor = System.Drawing.SystemColors.Window;
            this.serverBox.FormattingEnabled = true;
            this.serverBox.Location = new System.Drawing.Point(312, 112);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(149, 21);
            this.serverBox.TabIndex = 5;
            this.serverBox.SelectedIndexChanged += new System.EventHandler(this.serverBox_SelectedIndexChanged);
            // 
            // runButton
            // 
            this.runButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.Location = new System.Drawing.Point(347, 397);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 6;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // outputGrid
            // 
            this.outputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputGrid.Location = new System.Drawing.Point(36, 452);
            this.outputGrid.Name = "outputGrid";
            this.outputGrid.Size = new System.Drawing.Size(1385, 351);
            this.outputGrid.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1458, 825);
            this.Controls.Add(this.outputGrid);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.serverBox);
            this.Controls.Add(this.dgvInterarrival);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterarrival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvInterarrival;
        private System.Windows.Forms.ComboBox serverBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.DataGridView outputGrid;
    }
}

