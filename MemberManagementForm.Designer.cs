namespace AGomProject
{
    partial class MemberManagementForm
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
            this.txtSearchKeyword = new System.Windows.Forms.TextBox();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemberId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLoanHistory = new System.Windows.Forms.DataGridView();
            this.colBookTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsEbook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLibrary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBorrowDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReturnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFullHistory = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFullHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearchKeyword
            // 
            this.txtSearchKeyword.Location = new System.Drawing.Point(193, 25);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.Size = new System.Drawing.Size(234, 21);
            this.txtSearchKeyword.TabIndex = 0;
            // 
            // cboSearchType
            // 
            this.cboSearchType.FormattingEnabled = true;
            this.cboSearchType.Location = new System.Drawing.Point(71, 25);
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(121, 20);
            this.cboSearchType.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(426, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(42, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvMembers
            // 
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEmail,
            this.colName,
            this.colMemberId,
            this.colGender,
            this.colBirthDate,
            this.colPhone});
            this.dgvMembers.Location = new System.Drawing.Point(71, 53);
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.RowTemplate.Height = 23;
            this.dgvMembers.Size = new System.Drawing.Size(674, 250);
            this.dgvMembers.TabIndex = 3;
            this.dgvMembers.SelectionChanged += new System.EventHandler(this.dgvMembers_SelectionChanged);
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "email";
            this.colEmail.HeaderText = "이메일";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            this.colEmail.Width = 120;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "first_name";
            this.colName.HeaderText = "이름";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 80;
            // 
            // colMemberId
            // 
            this.colMemberId.DataPropertyName = "member_id";
            this.colMemberId.HeaderText = "회원 ID";
            this.colMemberId.Name = "colMemberId";
            this.colMemberId.ReadOnly = true;
            this.colMemberId.Width = 80;
            // 
            // colGender
            // 
            this.colGender.DataPropertyName = "gender";
            this.colGender.HeaderText = "성별";
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            this.colGender.Width = 60;
            // 
            // colBirthDate
            // 
            this.colBirthDate.DataPropertyName = "birth_date";
            this.colBirthDate.HeaderText = "생년월일";
            this.colBirthDate.Name = "colBirthDate";
            this.colBirthDate.ReadOnly = true;
            this.colBirthDate.Width = 150;
            // 
            // colPhone
            // 
            this.colPhone.DataPropertyName = "phone";
            this.colPhone.HeaderText = "전화번호";
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            this.colPhone.Width = 140;
            // 
            // dgvLoanHistory
            // 
            this.dgvLoanHistory.AllowUserToAddRows = false;
            this.dgvLoanHistory.AllowUserToDeleteRows = false;
            this.dgvLoanHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoanHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBookTitle,
            this.colIsEbook,
            this.colLibrary,
            this.colBorrowDate,
            this.colDueDate,
            this.colReturnDate,
            this.colStatus});
            this.dgvLoanHistory.Location = new System.Drawing.Point(71, 309);
            this.dgvLoanHistory.Name = "dgvLoanHistory";
            this.dgvLoanHistory.ReadOnly = true;
            this.dgvLoanHistory.RowTemplate.Height = 23;
            this.dgvLoanHistory.Size = new System.Drawing.Size(874, 180);
            this.dgvLoanHistory.TabIndex = 7;
            // 
            // colBookTitle
            // 
            this.colBookTitle.DataPropertyName = "title";
            this.colBookTitle.HeaderText = "도서명";
            this.colBookTitle.Name = "colBookTitle";
            this.colBookTitle.ReadOnly = true;
            this.colBookTitle.Width = 150;
            // 
            // colIsEbook
            // 
            this.colIsEbook.DataPropertyName = "is_ebook";
            this.colIsEbook.HeaderText = "E-Book";
            this.colIsEbook.Name = "colIsEbook";
            this.colIsEbook.ReadOnly = true;
            this.colIsEbook.Width = 50;
            // 
            // colLibrary
            // 
            this.colLibrary.DataPropertyName = "library_name";
            this.colLibrary.HeaderText = "도서관";
            this.colLibrary.Name = "colLibrary";
            this.colLibrary.ReadOnly = true;
            this.colLibrary.Width = 120;
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
            // colReturnDate
            // 
            this.colReturnDate.DataPropertyName = "return_date";
            this.colReturnDate.HeaderText = "반납일";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.ReadOnly = true;
            this.colReturnDate.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "return_status";
            this.colStatus.HeaderText = "상태";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 60;
            // 
            // dgvFullHistory
            // 
            this.dgvFullHistory.AllowUserToAddRows = false;
            this.dgvFullHistory.AllowUserToDeleteRows = false;
            this.dgvFullHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFullHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgvFullHistory.Location = new System.Drawing.Point(71, 495);
            this.dgvFullHistory.Name = "dgvFullHistory";
            this.dgvFullHistory.ReadOnly = true;
            this.dgvFullHistory.RowTemplate.Height = 23;
            this.dgvFullHistory.Size = new System.Drawing.Size(874, 250);
            this.dgvFullHistory.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn1.HeaderText = "도서명";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "is_ebook";
            this.dataGridViewTextBoxColumn2.HeaderText = "E-Book";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "library_name";
            this.dataGridViewTextBoxColumn3.HeaderText = "도서관";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "borrow_date";
            this.dataGridViewTextBoxColumn4.HeaderText = "대여일";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "due_date";
            this.dataGridViewTextBoxColumn5.HeaderText = "반납예정일";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "return_date";
            this.dataGridViewTextBoxColumn6.HeaderText = "반납일";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // btnCancle
            // 
            this.btnCancle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancle.Location = new System.Drawing.Point(647, 802);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(107, 31);
            this.btnCancle.TabIndex = 9;
            this.btnCancle.Text = "뒤로가기";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // MemberManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.dgvFullHistory);
            this.Controls.Add(this.dgvLoanHistory);
            this.Controls.Add(this.dgvMembers);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboSearchType);
            this.Controls.Add(this.txtSearchKeyword);
            this.Name = "MemberManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MemberManagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFullHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchKeyword;
        private System.Windows.Forms.ComboBox cboSearchType;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.DataGridView dgvLoanHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsEbook;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLibrary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBorrowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReturnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridView dgvFullHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button btnCancle;
    }
}