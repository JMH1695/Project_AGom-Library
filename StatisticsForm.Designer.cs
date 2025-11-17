namespace AGomProject
{
    partial class StatisticsForm
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
            this.cboTimePeriod = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadStatistics = new System.Windows.Forms.Button();
            this.lblCancle = new System.Windows.Forms.Label();
            this.cboCategoryFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelChart = new System.Windows.Forms.Panel();
            this.chartBooks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl60F = new System.Windows.Forms.Label();
            this.lbl60M = new System.Windows.Forms.Label();
            this.lbl50F = new System.Windows.Forms.Label();
            this.lbl50M = new System.Windows.Forms.Label();
            this.lbl40F = new System.Windows.Forms.Label();
            this.lbl40M = new System.Windows.Forms.Label();
            this.lbl30F = new System.Windows.Forms.Label();
            this.lbl30M = new System.Windows.Forms.Label();
            this.lbl20F = new System.Windows.Forms.Label();
            this.lbl20M = new System.Windows.Forms.Label();
            this.lbl10F = new System.Windows.Forms.Label();
            this.lbl10M = new System.Windows.Forms.Label();
            this.lblBookTrend = new System.Windows.Forms.Label();
            this.panelChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBooks)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTimePeriod
            // 
            this.cboTimePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimePeriod.FormattingEnabled = true;
            this.cboTimePeriod.Location = new System.Drawing.Point(90, 752);
            this.cboTimePeriod.Name = "cboTimePeriod";
            this.cboTimePeriod.Size = new System.Drawing.Size(154, 20);
            this.cboTimePeriod.TabIndex = 0;
            this.cboTimePeriod.SelectedIndexChanged += new System.EventHandler(this.cboTimePeriod_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(250, 752);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(150, 21);
            this.dtpStartDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(406, 752);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(150, 21);
            this.dtpEndDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 752);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "기간 설정";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "인기 도서 TOP 10";
            // 
            // btnLoadStatistics
            // 
            this.btnLoadStatistics.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadStatistics.Location = new System.Drawing.Point(942, 732);
            this.btnLoadStatistics.Name = "btnLoadStatistics";
            this.btnLoadStatistics.Size = new System.Drawing.Size(75, 23);
            this.btnLoadStatistics.TabIndex = 7;
            this.btnLoadStatistics.Text = "검색";
            this.btnLoadStatistics.UseVisualStyleBackColor = true;
            this.btnLoadStatistics.Click += new System.EventHandler(this.btnLoadStatistics_Click);
            // 
            // lblCancle
            // 
            this.lblCancle.AutoSize = true;
            this.lblCancle.BackColor = System.Drawing.Color.Silver;
            this.lblCancle.Font = new System.Drawing.Font("함초롬돋움", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCancle.Location = new System.Drawing.Point(670, 794);
            this.lblCancle.Name = "lblCancle";
            this.lblCancle.Padding = new System.Windows.Forms.Padding(15);
            this.lblCancle.Size = new System.Drawing.Size(128, 57);
            this.lblCancle.TabIndex = 9;
            this.lblCancle.Text = "뒤로 가기";
            this.lblCancle.Click += new System.EventHandler(this.lblCancle_Click);
            // 
            // cboCategoryFilter
            // 
            this.cboCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryFilter.FormattingEnabled = true;
            this.cboCategoryFilter.Location = new System.Drawing.Point(90, 803);
            this.cboCategoryFilter.Name = "cboCategoryFilter";
            this.cboCategoryFilter.Size = new System.Drawing.Size(154, 20);
            this.cboCategoryFilter.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(12, 806);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "장르 선택";
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.chartBooks);
            this.panelChart.Location = new System.Drawing.Point(17, 67);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(1000, 647);
            this.panelChart.TabIndex = 6;
            // 
            // chartBooks
            // 
            chartArea1.BackColor = System.Drawing.Color.OldLace;
            chartArea1.BorderColor = System.Drawing.Color.DarkOrange;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(3)))), ((int)(((byte)(3)))), ((int)(((byte)(3)))));
            this.chartBooks.ChartAreas.Add(chartArea1);
            this.chartBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBooks.Location = new System.Drawing.Point(0, 0);
            this.chartBooks.Name = "chartBooks";
            this.chartBooks.Size = new System.Drawing.Size(1000, 647);
            this.chartBooks.TabIndex = 0;
            this.chartBooks.Text = "chart1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(1148, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 29);
            this.label4.TabIndex = 12;
            this.label4.Text = "상세 통계";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbl60F, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbl60M, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbl50F, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl50M, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl40F, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl40M, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl30F, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl30M, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl20F, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl20M, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl10F, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl10M, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1096, 245);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 361);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // lbl60F
            // 
            this.lbl60F.AutoSize = true;
            this.lbl60F.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl60F.Font = new System.Drawing.Font("휴먼둥근헤드라인", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl60F.Location = new System.Drawing.Point(102, 300);
            this.lbl60F.Name = "lbl60F";
            this.lbl60F.Size = new System.Drawing.Size(95, 61);
            this.lbl60F.TabIndex = 11;
            this.lbl60F.Text = "60대 이상 여성";
            this.lbl60F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl60M
            // 
            this.lbl60M.AutoSize = true;
            this.lbl60M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl60M.Font = new System.Drawing.Font("휴먼둥근헤드라인", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl60M.Location = new System.Drawing.Point(3, 300);
            this.lbl60M.Name = "lbl60M";
            this.lbl60M.Size = new System.Drawing.Size(93, 61);
            this.lbl60M.TabIndex = 10;
            this.lbl60M.Text = "60대 이상 남성";
            this.lbl60M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl50F
            // 
            this.lbl50F.AutoSize = true;
            this.lbl50F.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl50F.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl50F.Location = new System.Drawing.Point(102, 240);
            this.lbl50F.Name = "lbl50F";
            this.lbl50F.Size = new System.Drawing.Size(95, 60);
            this.lbl50F.TabIndex = 9;
            this.lbl50F.Text = "50대 여성";
            this.lbl50F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl50M
            // 
            this.lbl50M.AutoSize = true;
            this.lbl50M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl50M.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl50M.Location = new System.Drawing.Point(3, 240);
            this.lbl50M.Name = "lbl50M";
            this.lbl50M.Size = new System.Drawing.Size(93, 60);
            this.lbl50M.TabIndex = 8;
            this.lbl50M.Text = "50대 남성";
            this.lbl50M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl40F
            // 
            this.lbl40F.AutoSize = true;
            this.lbl40F.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl40F.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl40F.Location = new System.Drawing.Point(102, 180);
            this.lbl40F.Name = "lbl40F";
            this.lbl40F.Size = new System.Drawing.Size(95, 60);
            this.lbl40F.TabIndex = 7;
            this.lbl40F.Text = "40대 여성";
            this.lbl40F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl40M
            // 
            this.lbl40M.AutoSize = true;
            this.lbl40M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl40M.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl40M.Location = new System.Drawing.Point(3, 180);
            this.lbl40M.Name = "lbl40M";
            this.lbl40M.Size = new System.Drawing.Size(93, 60);
            this.lbl40M.TabIndex = 6;
            this.lbl40M.Text = "40대 남성";
            this.lbl40M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl30F
            // 
            this.lbl30F.AutoSize = true;
            this.lbl30F.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl30F.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl30F.Location = new System.Drawing.Point(102, 120);
            this.lbl30F.Name = "lbl30F";
            this.lbl30F.Size = new System.Drawing.Size(95, 60);
            this.lbl30F.TabIndex = 5;
            this.lbl30F.Text = "30대 여성";
            this.lbl30F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl30M
            // 
            this.lbl30M.AutoSize = true;
            this.lbl30M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl30M.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl30M.Location = new System.Drawing.Point(3, 120);
            this.lbl30M.Name = "lbl30M";
            this.lbl30M.Size = new System.Drawing.Size(93, 60);
            this.lbl30M.TabIndex = 4;
            this.lbl30M.Text = "30대 남성";
            this.lbl30M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl20F
            // 
            this.lbl20F.AutoSize = true;
            this.lbl20F.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl20F.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl20F.Location = new System.Drawing.Point(102, 60);
            this.lbl20F.Name = "lbl20F";
            this.lbl20F.Size = new System.Drawing.Size(95, 60);
            this.lbl20F.TabIndex = 3;
            this.lbl20F.Text = "20대 여성";
            this.lbl20F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl20M
            // 
            this.lbl20M.AutoSize = true;
            this.lbl20M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl20M.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl20M.Location = new System.Drawing.Point(3, 60);
            this.lbl20M.Name = "lbl20M";
            this.lbl20M.Size = new System.Drawing.Size(93, 60);
            this.lbl20M.TabIndex = 2;
            this.lbl20M.Text = "20대 남성";
            this.lbl20M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl10F
            // 
            this.lbl10F.AutoSize = true;
            this.lbl10F.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl10F.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl10F.Location = new System.Drawing.Point(102, 0);
            this.lbl10F.Name = "lbl10F";
            this.lbl10F.Size = new System.Drawing.Size(95, 60);
            this.lbl10F.TabIndex = 1;
            this.lbl10F.Text = "10대 여성";
            this.lbl10F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl10M
            // 
            this.lbl10M.AutoSize = true;
            this.lbl10M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl10M.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl10M.Location = new System.Drawing.Point(3, 0);
            this.lbl10M.Name = "lbl10M";
            this.lbl10M.Size = new System.Drawing.Size(93, 60);
            this.lbl10M.TabIndex = 0;
            this.lbl10M.Text = "10대 남성";
            this.lbl10M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBookTrend
            // 
            this.lblBookTrend.AutoSize = true;
            this.lblBookTrend.BackColor = System.Drawing.Color.Silver;
            this.lblBookTrend.Font = new System.Drawing.Font("함초롬돋움", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblBookTrend.Location = new System.Drawing.Point(1195, 25);
            this.lblBookTrend.Name = "lblBookTrend";
            this.lblBookTrend.Padding = new System.Windows.Forms.Padding(15);
            this.lblBookTrend.Size = new System.Drawing.Size(154, 57);
            this.lblBookTrend.TabIndex = 14;
            this.lblBookTrend.Text = "각 도서 통계";
            this.lblBookTrend.Click += new System.EventHandler(this.lblBookTrend_Click);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.lblBookTrend);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCategoryFilter);
            this.Controls.Add(this.lblCancle);
            this.Controls.Add(this.btnLoadStatistics);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cboTimePeriod);
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StatisticsForm";
            this.panelChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBooks)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTimePeriod;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadStatistics;
        private System.Windows.Forms.Label lblCancle;
        private System.Windows.Forms.ComboBox cboCategoryFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBooks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl10M;
        private System.Windows.Forms.Label lbl60F;
        private System.Windows.Forms.Label lbl60M;
        private System.Windows.Forms.Label lbl50F;
        private System.Windows.Forms.Label lbl50M;
        private System.Windows.Forms.Label lbl40F;
        private System.Windows.Forms.Label lbl40M;
        private System.Windows.Forms.Label lbl30F;
        private System.Windows.Forms.Label lbl30M;
        private System.Windows.Forms.Label lbl20F;
        private System.Windows.Forms.Label lbl20M;
        private System.Windows.Forms.Label lbl10F;
        private System.Windows.Forms.Label lblBookTrend;
    }
}