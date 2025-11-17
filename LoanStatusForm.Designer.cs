namespace AGomProject
{
    partial class LoanStatusForm
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.txtSearchKeyword = new System.Windows.Forms.TextBox();
            this.dgvLoanStatus = new System.Windows.Forms.DataGridView();
            this.colMemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBookTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsEbook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLibrary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBorrowDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(439, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(42, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboSearchType
            // 
            this.cboSearchType.FormattingEnabled = true;
            this.cboSearchType.Location = new System.Drawing.Point(35, 25);
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(170, 20);
            this.cboSearchType.TabIndex = 4;
            // 
            // txtSearchKeyword
            // 
            this.txtSearchKeyword.Location = new System.Drawing.Point(206, 25);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.Size = new System.Drawing.Size(234, 21);
            this.txtSearchKeyword.TabIndex = 3;
            // 
            // dgvLoanStatus
            // 
            this.dgvLoanStatus.AllowUserToAddRows = false;
            this.dgvLoanStatus.AllowUserToDeleteRows = false;
            this.dgvLoanStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoanStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMemberName,
            this.colBookTitle,
            this.colIsEbook,
            this.colLibrary,
            this.colBorrowDate,
            this.colDueDate,
            this.colStatus});
            this.dgvLoanStatus.Location = new System.Drawing.Point(35, 52);
            this.dgvLoanStatus.Name = "dgvLoanStatus";
            this.dgvLoanStatus.ReadOnly = true;
            this.dgvLoanStatus.RowTemplate.Height = 23;
            this.dgvLoanStatus.Size = new System.Drawing.Size(804, 677);
            this.dgvLoanStatus.TabIndex = 6;
            // 
            // colMemberName
            // 
            this.colMemberName.DataPropertyName = "first_name";
            this.colMemberName.HeaderText = "회원 이름";
            this.colMemberName.Name = "colMemberName";
            this.colMemberName.ReadOnly = true;
            this.colMemberName.Width = 80;
            // 
            // colBookTitle
            // 
            this.colBookTitle.DataPropertyName = "title";
            this.colBookTitle.HeaderText = "도서명";
            this.colBookTitle.Name = "colBookTitle";
            this.colBookTitle.ReadOnly = true;
            this.colBookTitle.Width = 140;
            // 
            // colIsEbook
            // 
            this.colIsEbook.DataPropertyName = "is_ebook";
            this.colIsEbook.HeaderText = "E-Book";
            this.colIsEbook.Name = "colIsEbook";
            this.colIsEbook.ReadOnly = true;
            this.colIsEbook.Width = 60;
            // 
            // colLibrary
            // 
            this.colLibrary.DataPropertyName = "library_name";
            this.colLibrary.HeaderText = "도서관";
            this.colLibrary.Name = "colLibrary";
            this.colLibrary.ReadOnly = true;
            // 
            // colBorrowDate
            // 
            this.colBorrowDate.DataPropertyName = "borrow_date";
            this.colBorrowDate.HeaderText = "대여일";
            this.colBorrowDate.Name = "colBorrowDate";
            this.colBorrowDate.ReadOnly = true;
            this.colBorrowDate.Width = 150;
            // 
            // colDueDate
            // 
            this.colDueDate.DataPropertyName = "due_date";
            this.colDueDate.HeaderText = "반납예정일";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.ReadOnly = true;
            this.colDueDate.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "return_status";
            this.colStatus.HeaderText = "상태";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 80;
            // 
            // btnCancle
            // 
            this.btnCancle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancle.Location = new System.Drawing.Point(659, 798);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(93, 29);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "뒤로가기";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // LoanStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.dgvLoanStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboSearchType);
            this.Controls.Add(this.txtSearchKeyword);
            this.Name = "LoanStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoanStatusForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboSearchType;
        private System.Windows.Forms.TextBox txtSearchKeyword;
        private System.Windows.Forms.DataGridView dgvLoanStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsEbook;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLibrary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBorrowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Button btnCancle;
    }
}