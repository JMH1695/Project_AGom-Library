namespace AGomProject
{
    partial class BookBorrowTrendForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartBookTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBookSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cboTimePeriod = new System.Windows.Forms.ComboBox();
            this.lblCurrentBook = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvBookSearch = new System.Windows.Forms.DataGridView();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCancle = new System.Windows.Forms.Label();
            this.btnShowTrend = new System.Windows.Forms.Button();
            this.dgvBookAvailability = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cboReportTimePeriod = new System.Windows.Forms.ComboBox();
            this.btnCheckAvailability = new System.Windows.Forms.Button();
            this.BookTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LibraryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnavailableCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chartBookTrend)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookAvailability)).BeginInit();
            this.SuspendLayout();
            // 
            // chartBookTrend
            // 
            chartArea5.BackColor = System.Drawing.Color.OldLace;
            chartArea5.BorderColor = System.Drawing.Color.DarkOrange;
            chartArea5.Name = "ChartArea1";
            chartArea5.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(3)))), ((int)(((byte)(3)))), ((int)(((byte)(3)))));
            this.chartBookTrend.ChartAreas.Add(chartArea5);
            this.chartBookTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBookTrend.Location = new System.Drawing.Point(0, 0);
            this.chartBookTrend.Name = "chartBookTrend";
            this.chartBookTrend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.IsXValueIndexed = true;
            series5.Name = "Books";
            this.chartBookTrend.Series.Add(series5);
            this.chartBookTrend.Size = new System.Drawing.Size(1292, 447);
            this.chartBookTrend.TabIndex = 1;
            this.chartBookTrend.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chartBookTrend);
            this.panel1.Location = new System.Drawing.Point(41, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1292, 447);
            this.panel1.TabIndex = 2;
            // 
            // txtBookSearch
            // 
            this.txtBookSearch.Location = new System.Drawing.Point(90, 549);
            this.txtBookSearch.Name = "txtBookSearch";
            this.txtBookSearch.Size = new System.Drawing.Size(413, 21);
            this.txtBookSearch.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(529, 549);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 752);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "기간 설정";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(406, 752);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(150, 21);
            this.dtpEndDate.TabIndex = 7;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(250, 752);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(150, 21);
            this.dtpStartDate.TabIndex = 6;
            // 
            // cboTimePeriod
            // 
            this.cboTimePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimePeriod.FormattingEnabled = true;
            this.cboTimePeriod.Location = new System.Drawing.Point(90, 752);
            this.cboTimePeriod.Name = "cboTimePeriod";
            this.cboTimePeriod.Size = new System.Drawing.Size(154, 20);
            this.cboTimePeriod.TabIndex = 5;
            this.cboTimePeriod.SelectedIndexChanged += new System.EventHandler(this.cboTimePeriod_SelectedIndexChanged);
            // 
            // lblCurrentBook
            // 
            this.lblCurrentBook.AutoSize = true;
            this.lblCurrentBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrentBook.Location = new System.Drawing.Point(78, 26);
            this.lblCurrentBook.Name = "lblCurrentBook";
            this.lblCurrentBook.Size = new System.Drawing.Size(21, 29);
            this.lblCurrentBook.TabIndex = 10;
            this.lblCurrentBook.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 549);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "도서 검색";
            // 
            // dgvBookSearch
            // 
            this.dgvBookSearch.AllowUserToAddRows = false;
            this.dgvBookSearch.AllowUserToDeleteRows = false;
            this.dgvBookSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitle,
            this.colAuthor,
            this.colCategory});
            this.dgvBookSearch.Location = new System.Drawing.Point(15, 587);
            this.dgvBookSearch.MultiSelect = false;
            this.dgvBookSearch.Name = "dgvBookSearch";
            this.dgvBookSearch.ReadOnly = true;
            this.dgvBookSearch.RowTemplate.Height = 23;
            this.dgvBookSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookSearch.Size = new System.Drawing.Size(514, 150);
            this.dgvBookSearch.TabIndex = 12;
            this.dgvBookSearch.SelectionChanged += new System.EventHandler(this.dgvBookSearch_SelectionChanged);
            // 
            // colTitle
            // 
            this.colTitle.DataPropertyName = "title";
            this.colTitle.HeaderText = "도서명";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            this.colTitle.Width = 200;
            // 
            // colAuthor
            // 
            this.colAuthor.DataPropertyName = "author";
            this.colAuthor.HeaderText = "저자";
            this.colAuthor.Name = "colAuthor";
            this.colAuthor.ReadOnly = true;
            this.colAuthor.Width = 150;
            // 
            // colCategory
            // 
            this.colCategory.DataPropertyName = "category_name";
            this.colCategory.HeaderText = "카테고리";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 120;
            // 
            // lblCancle
            // 
            this.lblCancle.AutoSize = true;
            this.lblCancle.BackColor = System.Drawing.Color.Silver;
            this.lblCancle.Font = new System.Drawing.Font("함초롬돋움", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCancle.Location = new System.Drawing.Point(667, 795);
            this.lblCancle.Name = "lblCancle";
            this.lblCancle.Padding = new System.Windows.Forms.Padding(15);
            this.lblCancle.Size = new System.Drawing.Size(128, 57);
            this.lblCancle.TabIndex = 13;
            this.lblCancle.Text = "뒤로 가기";
            this.lblCancle.Click += new System.EventHandler(this.lblCancle_Click);
            // 
            // btnShowTrend
            // 
            this.btnShowTrend.Location = new System.Drawing.Point(535, 700);
            this.btnShowTrend.Name = "btnShowTrend";
            this.btnShowTrend.Size = new System.Drawing.Size(100, 37);
            this.btnShowTrend.TabIndex = 14;
            this.btnShowTrend.Text = "통계 보기";
            this.btnShowTrend.UseVisualStyleBackColor = true;
            this.btnShowTrend.Click += new System.EventHandler(this.btnShowTrend_Click);
            // 
            // dgvBookAvailability
            // 
            this.dgvBookAvailability.AllowUserToAddRows = false;
            this.dgvBookAvailability.AllowUserToDeleteRows = false;
            this.dgvBookAvailability.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookAvailability.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BookTitle,
            this.LibraryName,
            this.UnavailableCount});
            this.dgvBookAvailability.Location = new System.Drawing.Point(819, 587);
            this.dgvBookAvailability.MultiSelect = false;
            this.dgvBookAvailability.Name = "dgvBookAvailability";
            this.dgvBookAvailability.ReadOnly = true;
            this.dgvBookAvailability.RowTemplate.Height = 23;
            this.dgvBookAvailability.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookAvailability.Size = new System.Drawing.Size(384, 150);
            this.dgvBookAvailability.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("함초롬돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(815, 553);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "모랄빵";
            // 
            // cboReportTimePeriod
            // 
            this.cboReportTimePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportTimePeriod.FormattingEnabled = true;
            this.cboReportTimePeriod.Location = new System.Drawing.Point(875, 553);
            this.cboReportTimePeriod.Name = "cboReportTimePeriod";
            this.cboReportTimePeriod.Size = new System.Drawing.Size(154, 20);
            this.cboReportTimePeriod.TabIndex = 17;
            this.cboReportTimePeriod.SelectedIndexChanged += new System.EventHandler(this.cboReportTimePeriod_SelectedIndexChanged);
            // 
            // btnCheckAvailability
            // 
            this.btnCheckAvailability.Location = new System.Drawing.Point(1035, 550);
            this.btnCheckAvailability.Name = "btnCheckAvailability";
            this.btnCheckAvailability.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAvailability.TabIndex = 18;
            this.btnCheckAvailability.Text = "검색";
            this.btnCheckAvailability.UseVisualStyleBackColor = true;
            this.btnCheckAvailability.Click += new System.EventHandler(this.btnCheckAvailability_Click);
            // 
            // BookTitle
            // 
            this.BookTitle.DataPropertyName = "BookTitle";
            this.BookTitle.HeaderText = "도서명";
            this.BookTitle.Name = "BookTitle";
            this.BookTitle.ReadOnly = true;
            this.BookTitle.Width = 150;
            // 
            // LibraryName
            // 
            this.LibraryName.DataPropertyName = "LibraryName";
            this.LibraryName.HeaderText = "도서관 이름";
            this.LibraryName.Name = "LibraryName";
            this.LibraryName.ReadOnly = true;
            this.LibraryName.Width = 110;
            // 
            // UnavailableCount
            // 
            this.UnavailableCount.DataPropertyName = "UnavailableCount";
            this.UnavailableCount.HeaderText = "오링횟수";
            this.UnavailableCount.Name = "UnavailableCount";
            this.UnavailableCount.ReadOnly = true;
            this.UnavailableCount.Width = 80;
            // 
            // BookBorrowTrendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.btnCheckAvailability);
            this.Controls.Add(this.cboReportTimePeriod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvBookAvailability);
            this.Controls.Add(this.btnShowTrend);
            this.Controls.Add(this.lblCancle);
            this.Controls.Add(this.dgvBookSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrentBook);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.cboTimePeriod);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBookSearch);
            this.Controls.Add(this.panel1);
            this.Name = "BookBorrowTrendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BookBorrowTrendForm";
            ((System.ComponentModel.ISupportInitialize)(this.chartBookTrend)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookAvailability)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartBookTrend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBookSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cboTimePeriod;
        private System.Windows.Forms.Label lblCurrentBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvBookSearch;
        private System.Windows.Forms.Label lblCancle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.Button btnShowTrend;
        private System.Windows.Forms.DataGridView dgvBookAvailability;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboReportTimePeriod;
        private System.Windows.Forms.Button btnCheckAvailability;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn LibraryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnavailableCount;
    }
}