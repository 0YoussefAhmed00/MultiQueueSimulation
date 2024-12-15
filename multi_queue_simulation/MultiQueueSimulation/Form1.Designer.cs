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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.serverPerformance = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simulationPerformance = new System.Windows.Forms.DataGridView();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterarrival)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverPerformance)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationPerformance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(716, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(493, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to our project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(869, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Import your test case:";
            // 
            // browseButton
            // 
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(900, 93);
            this.browseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(107, 38);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // dgvInterarrival
            // 
            this.dgvInterarrival.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvInterarrival.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInterarrival.Location = new System.Drawing.Point(64, 74);
            this.dgvInterarrival.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvInterarrival.Name = "dgvInterarrival";
            this.dgvInterarrival.RowHeadersWidth = 51;
            this.dgvInterarrival.RowTemplate.Height = 24;
            this.dgvInterarrival.Size = new System.Drawing.Size(927, 295);
            this.dgvInterarrival.TabIndex = 4;
            // 
            // serverBox
            // 
            this.serverBox.BackColor = System.Drawing.SystemColors.Window;
            this.serverBox.FormattingEnabled = true;
            this.serverBox.Location = new System.Drawing.Point(484, 25);
            this.serverBox.Margin = new System.Windows.Forms.Padding(4);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(197, 24);
            this.serverBox.TabIndex = 5;
            this.serverBox.SelectedIndexChanged += new System.EventHandler(this.serverBox_SelectedIndexChanged);
            // 
            // runButton
            // 
            this.runButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.Location = new System.Drawing.Point(903, 137);
            this.runButton.Margin = new System.Windows.Forms.Padding(4);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(100, 28);
            this.runButton.TabIndex = 6;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // outputGrid
            // 
            this.outputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputGrid.Location = new System.Drawing.Point(64, 443);
            this.outputGrid.Margin = new System.Windows.Forms.Padding(4);
            this.outputGrid.Name = "outputGrid";
            this.outputGrid.RowHeadersWidth = 51;
            this.outputGrid.Size = new System.Drawing.Size(1808, 618);
            this.outputGrid.TabIndex = 7;
            // 
            // graph
            // 
            chartArea1.Name = "ChartArea1";
            this.graph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graph.Legends.Add(legend1);
            this.graph.Location = new System.Drawing.Point(64, 1170);
            this.graph.Margin = new System.Windows.Forms.Padding(4);
            this.graph.Name = "graph";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.graph.Series.Add(series1);
            this.graph.Size = new System.Drawing.Size(1368, 498);
            this.graph.TabIndex = 8;
            this.graph.Text = "chart1";
            // 
            // graphBox
            // 
            this.graphBox.FormattingEnabled = true;
            this.graphBox.Location = new System.Drawing.Point(937, 1115);
            this.graphBox.Margin = new System.Windows.Forms.Padding(4);
            this.graphBox.Name = "graphBox";
            this.graphBox.Size = new System.Drawing.Size(160, 24);
            this.graphBox.TabIndex = 9;
            this.graphBox.SelectedIndexChanged += new System.EventHandler(this.graphBox_SelectedIndexChanged);
            // 
            // graphLabel
            // 
            this.graphLabel.AutoSize = true;
            this.graphLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphLabel.Location = new System.Drawing.Point(789, 1115);
            this.graphLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.graphLabel.Name = "graphLabel";
            this.graphLabel.Size = new System.Drawing.Size(132, 25);
            this.graphLabel.TabIndex = 10;
            this.graphLabel.Text = "Select server:";
            // 
            // serverPerformance
            // 
            this.serverPerformance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serverPerformance.Location = new System.Drawing.Point(1469, 1268);
            this.serverPerformance.Margin = new System.Windows.Forms.Padding(4);
            this.serverPerformance.Name = "serverPerformance";
            this.serverPerformance.RowHeadersWidth = 51;
            this.serverPerformance.Size = new System.Drawing.Size(403, 326);
            this.serverPerformance.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simulationPerformance);
            this.panel1.Controls.Add(this.graphLabel);
            this.panel1.Controls.Add(this.graphBox);
            this.panel1.Controls.Add(this.serverPerformance);
            this.panel1.Controls.Add(this.outputGrid);
            this.panel1.Controls.Add(this.graph);
            this.panel1.Controls.Add(this.dgvInterarrival);
            this.panel1.Controls.Add(this.serverBox);
            this.panel1.Location = new System.Drawing.Point(16, 169);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1933, 735);
            this.panel1.TabIndex = 13;
            // 
            // simulationPerformance
            // 
            this.simulationPerformance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.simulationPerformance.Location = new System.Drawing.Point(1107, 74);
            this.simulationPerformance.Margin = new System.Windows.Forms.Padding(4);
            this.simulationPerformance.Name = "simulationPerformance";
            this.simulationPerformance.RowHeadersWidth = 51;
            this.simulationPerformance.Size = new System.Drawing.Size(765, 295);
            this.simulationPerformance.TabIndex = 13;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(1123, 106);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(188, 20);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Stop At max No. customers";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(1123, 133);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(133, 20);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Stop At max Time";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(561, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Servers No.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(555, 143);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 18;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(748, 140);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(748, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Selection Methods";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1924, 918);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterarrival)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverPerformance)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationPerformance)).EndInit();
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
        private System.Windows.Forms.DataGridView serverPerformance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView simulationPerformance;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
    }
}

