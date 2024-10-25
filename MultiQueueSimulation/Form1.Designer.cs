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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.dgvInterarrival = new System.Windows.Forms.DataGridView();
            this.serverBox = new System.Windows.Forms.ComboBox();
            this.runButton = new System.Windows.Forms.Button();
            this.outputGrid = new System.Windows.Forms.DataGridView();
            this.graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.graphBox = new System.Windows.Forms.ComboBox();
            this.graphLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterarrival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(537, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to our project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(652, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Import your test case:";
            // 
            // browseButton
            // 
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(689, 72);
            this.browseButton.Margin = new System.Windows.Forms.Padding(2);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(80, 31);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
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
            this.outputGrid.Location = new System.Drawing.Point(235, 453);
            this.outputGrid.Name = "outputGrid";
            this.outputGrid.Size = new System.Drawing.Size(1043, 351);
            this.outputGrid.TabIndex = 7;
            // 
            // graph
            // 
            chartArea5.Name = "ChartArea1";
            this.graph.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.graph.Legends.Add(legend5);
            this.graph.Location = new System.Drawing.Point(822, 138);
            this.graph.Name = "graph";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.graph.Series.Add(series5);
            this.graph.Size = new System.Drawing.Size(591, 300);
            this.graph.TabIndex = 8;
            this.graph.Text = "chart1";
            // 
            // graphBox
            // 
            this.graphBox.FormattingEnabled = true;
            this.graphBox.Location = new System.Drawing.Point(1295, 102);
            this.graphBox.Name = "graphBox";
            this.graphBox.Size = new System.Drawing.Size(121, 21);
            this.graphBox.TabIndex = 9;
            this.graphBox.SelectedIndexChanged += new System.EventHandler(this.graphBox_SelectedIndexChanged);
            // 
            // graphLabel
            // 
            this.graphLabel.AutoSize = true;
            this.graphLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphLabel.Location = new System.Drawing.Point(1184, 102);
            this.graphLabel.Name = "graphLabel";
            this.graphLabel.Size = new System.Drawing.Size(105, 20);
            this.graphLabel.TabIndex = 10;
            this.graphLabel.Text = "Select server:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1458, 825);
            this.Controls.Add(this.graphLabel);
            this.Controls.Add(this.graphBox);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.outputGrid);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.serverBox);
            this.Controls.Add(this.dgvInterarrival);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterarrival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.DataGridView dgvInterarrival;
        private System.Windows.Forms.ComboBox serverBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.DataGridView outputGrid;
        private System.Windows.Forms.DataVisualization.Charting.Chart graph;
        private System.Windows.Forms.ComboBox graphBox;
        private System.Windows.Forms.Label graphLabel;
    }
}

