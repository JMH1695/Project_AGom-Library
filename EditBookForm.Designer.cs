namespace AGomProject
{
    partial class EditBookForm
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
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnImageBrowse = new System.Windows.Forms.Button();
            this.chkIsEbook = new System.Windows.Forms.CheckBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEbookUrl = new System.Windows.Forms.TextBox();
            this.txtCoverImage = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtIsbn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.rdoIsbn = new System.Windows.Forms.RadioButton();
            this.rdoTitle = new System.Windows.Forms.RadioButton();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.colIsbn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnEditCategory = new System.Windows.Forms.Button();
            this.cboSelectCategory = new System.Windows.Forms.ComboBox();
            this.txtEditCategory = new System.Windows.Forms.TextBox();
            this.txtEditLanguage = new System.Windows.Forms.TextBox();
            this.cboSelectLanguage = new System.Windows.Forms.ComboBox();
            this.btnEditLanguage = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnDeleteLanguage = new System.Windows.Forms.Button();
            this.btnAddLanguage = new System.Windows.Forms.Button();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.btnEbookBrowse = new System.Windows.Forms.Button();
            this.rtxtDescription = new System.Windows.Forms.RichTextBox();
            this.pbCoverPreview = new System.Windows.Forms.PictureBox();
            this.btnPreviewEbook = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoverPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(109, 784);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(54, 23);
            this.btnAddCategory.TabIndex = 45;
            this.btnAddCategory.Text = "추가";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(620, 747);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(121, 34);
            this.btnUpdate.TabIndex = 43;
            this.btnUpdate.Text = "업데이트";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnImageBrowse
            // 
            this.btnImageBrowse.Location = new System.Drawing.Point(689, 477);
            this.btnImageBrowse.Name = "btnImageBrowse";
            this.btnImageBrowse.Size = new System.Drawing.Size(96, 30);
            this.btnImageBrowse.TabIndex = 42;
            this.btnImageBrowse.Text = "이미지 검색";
            this.btnImageBrowse.UseVisualStyleBackColor = true;
            this.btnImageBrowse.Click += new System.EventHandler(this.btnImageBrowse_Click);
            // 
            // chkIsEbook
            // 
            this.chkIsEbook.AutoSize = true;
            this.chkIsEbook.Location = new System.Drawing.Point(228, 448);
            this.chkIsEbook.Name = "chkIsEbook";
            this.chkIsEbook.Size = new System.Drawing.Size(15, 14);
            this.chkIsEbook.TabIndex = 39;
            this.chkIsEbook.UseVisualStyleBackColor = true;
            this.chkIsEbook.CheckedChanged += new System.EventHandler(this.chkIsEbook_CheckedChanged);
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(228, 373);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(143, 20);
            this.cboCategory.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 523);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 12);
            this.label8.TabIndex = 51;
            this.label8.Text = "전자책 URL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 486);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 12);
            this.label7.TabIndex = 50;
            this.label7.Text = "표지 이미지 경로";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 448);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "전자책 여부";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 373);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "카테고리";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "도서 제목";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "ISBN Number";
            // 
            // txtEbookUrl
            // 
            this.txtEbookUrl.Location = new System.Drawing.Point(228, 520);
            this.txtEbookUrl.Name = "txtEbookUrl";
            this.txtEbookUrl.Size = new System.Drawing.Size(446, 21);
            this.txtEbookUrl.TabIndex = 41;
            // 
            // txtCoverImage
            // 
            this.txtCoverImage.Location = new System.Drawing.Point(228, 483);
            this.txtCoverImage.Name = "txtCoverImage";
            this.txtCoverImage.Size = new System.Drawing.Size(446, 21);
            this.txtCoverImage.TabIndex = 40;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(228, 333);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(293, 21);
            this.txtAuthor.TabIndex = 36;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(228, 296);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(293, 21);
            this.txtTitle.TabIndex = 35;
            // 
            // txtIsbn
            // 
            this.txtIsbn.Location = new System.Drawing.Point(228, 259);
            this.txtIsbn.Name = "txtIsbn";
            this.txtIsbn.Size = new System.Drawing.Size(293, 21);
            this.txtIsbn.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 409);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 54;
            this.label5.Text = "언어";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 53;
            this.label3.Text = "저자";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(228, 37);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(446, 21);
            this.txtSearch.TabIndex = 55;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(672, 36);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 23);
            this.btnSearch.TabIndex = 56;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(48, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 12);
            this.label10.TabIndex = 57;
            this.label10.Text = "책 검색";
            // 
            // rdoIsbn
            // 
            this.rdoIsbn.AutoSize = true;
            this.rdoIsbn.Location = new System.Drawing.Point(113, 30);
            this.rdoIsbn.Name = "rdoIsbn";
            this.rdoIsbn.Size = new System.Drawing.Size(79, 16);
            this.rdoIsbn.TabIndex = 58;
            this.rdoIsbn.TabStop = true;
            this.rdoIsbn.Text = "ISBN 으로";
            this.rdoIsbn.UseVisualStyleBackColor = true;
            // 
            // rdoTitle
            // 
            this.rdoTitle.AutoSize = true;
            this.rdoTitle.Location = new System.Drawing.Point(113, 54);
            this.rdoTitle.Name = "rdoTitle";
            this.rdoTitle.Size = new System.Drawing.Size(71, 16);
            this.rdoTitle.TabIndex = 59;
            this.rdoTitle.TabStop = true;
            this.rdoTitle.Text = "제목으로";
            this.rdoTitle.UseVisualStyleBackColor = true;
            // 
            // dgvBooks
            // 
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.ColumnHeadersHeight = 20;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIsbn,
            this.colTitle,
            this.colAuthor,
            this.colCategory});
            this.dgvBooks.Location = new System.Drawing.Point(51, 90);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowTemplate.Height = 23;
            this.dgvBooks.Size = new System.Drawing.Size(680, 146);
            this.dgvBooks.TabIndex = 60;
            this.dgvBooks.SelectionChanged += new System.EventHandler(this.dgvBooks_SelectionChanged);
            // 
            // colIsbn
            // 
            this.colIsbn.DataPropertyName = "ISBN";
            this.colIsbn.HeaderText = "ISBN";
            this.colIsbn.Name = "colIsbn";
            this.colIsbn.ReadOnly = true;
            this.colIsbn.Width = 150;
            // 
            // colTitle
            // 
            this.colTitle.DataPropertyName = "Title";
            this.colTitle.HeaderText = "제목";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            this.colTitle.Width = 220;
            // 
            // colAuthor
            // 
            this.colAuthor.DataPropertyName = "Author";
            this.colAuthor.HeaderText = "저자";
            this.colAuthor.Name = "colAuthor";
            this.colAuthor.ReadOnly = true;
            this.colAuthor.Width = 150;
            // 
            // colCategory
            // 
            this.colCategory.DataPropertyName = "CategoryName";
            this.colCategory.HeaderText = "카테고리";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 120;
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Location = new System.Drawing.Point(171, 784);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(54, 23);
            this.btnDeleteCategory.TabIndex = 63;
            this.btnDeleteCategory.Text = "삭제";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(610, 262);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(121, 34);
            this.btnDelete.TabIndex = 65;
            this.btnDelete.Text = "도서 비활성화";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(620, 804);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 34);
            this.btnCancel.TabIndex = 101;
            this.btnCancel.Text = "뒤로가기";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(63, 664);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 19);
            this.label11.TabIndex = 103;
            this.label11.Text = "카테고리 수정";
            // 
            // btnEditCategory
            // 
            this.btnEditCategory.Location = new System.Drawing.Point(45, 784);
            this.btnEditCategory.Name = "btnEditCategory";
            this.btnEditCategory.Size = new System.Drawing.Size(54, 23);
            this.btnEditCategory.TabIndex = 104;
            this.btnEditCategory.Text = "수정";
            this.btnEditCategory.UseVisualStyleBackColor = true;
            this.btnEditCategory.Click += new System.EventHandler(this.btnEditCategory_Click);
            // 
            // cboSelectCategory
            // 
            this.cboSelectCategory.FormattingEnabled = true;
            this.cboSelectCategory.Location = new System.Drawing.Point(45, 718);
            this.cboSelectCategory.Name = "cboSelectCategory";
            this.cboSelectCategory.Size = new System.Drawing.Size(180, 20);
            this.cboSelectCategory.TabIndex = 105;
            this.cboSelectCategory.SelectedIndexChanged += new System.EventHandler(this.cboSelectCategory_SelectedIndexChanged);
            // 
            // txtEditCategory
            // 
            this.txtEditCategory.Location = new System.Drawing.Point(45, 750);
            this.txtEditCategory.Name = "txtEditCategory";
            this.txtEditCategory.Size = new System.Drawing.Size(180, 21);
            this.txtEditCategory.TabIndex = 106;
            // 
            // txtEditLanguage
            // 
            this.txtEditLanguage.Location = new System.Drawing.Point(325, 750);
            this.txtEditLanguage.Name = "txtEditLanguage";
            this.txtEditLanguage.Size = new System.Drawing.Size(180, 21);
            this.txtEditLanguage.TabIndex = 112;
            // 
            // cboSelectLanguage
            // 
            this.cboSelectLanguage.FormattingEnabled = true;
            this.cboSelectLanguage.Location = new System.Drawing.Point(325, 718);
            this.cboSelectLanguage.Name = "cboSelectLanguage";
            this.cboSelectLanguage.Size = new System.Drawing.Size(180, 20);
            this.cboSelectLanguage.TabIndex = 111;
            this.cboSelectLanguage.SelectedIndexChanged += new System.EventHandler(this.cboSelectLanguage_SelectedIndexChanged);
            // 
            // btnEditLanguage
            // 
            this.btnEditLanguage.Location = new System.Drawing.Point(325, 784);
            this.btnEditLanguage.Name = "btnEditLanguage";
            this.btnEditLanguage.Size = new System.Drawing.Size(54, 23);
            this.btnEditLanguage.TabIndex = 110;
            this.btnEditLanguage.Text = "수정";
            this.btnEditLanguage.UseVisualStyleBackColor = true;
            this.btnEditLanguage.Click += new System.EventHandler(this.btnEditLanguage_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(365, 664);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 19);
            this.label9.TabIndex = 109;
            this.label9.Text = "언어 수정";
            // 
            // btnDeleteLanguage
            // 
            this.btnDeleteLanguage.Location = new System.Drawing.Point(451, 784);
            this.btnDeleteLanguage.Name = "btnDeleteLanguage";
            this.btnDeleteLanguage.Size = new System.Drawing.Size(54, 23);
            this.btnDeleteLanguage.TabIndex = 108;
            this.btnDeleteLanguage.Text = "삭제";
            this.btnDeleteLanguage.UseVisualStyleBackColor = true;
            this.btnDeleteLanguage.Click += new System.EventHandler(this.btnDeleteLanguage_Click);
            // 
            // btnAddLanguage
            // 
            this.btnAddLanguage.Location = new System.Drawing.Point(389, 784);
            this.btnAddLanguage.Name = "btnAddLanguage";
            this.btnAddLanguage.Size = new System.Drawing.Size(54, 23);
            this.btnAddLanguage.TabIndex = 107;
            this.btnAddLanguage.Text = "추가";
            this.btnAddLanguage.UseVisualStyleBackColor = true;
            this.btnAddLanguage.Click += new System.EventHandler(this.btnAddLanguage_Click);
            // 
            // cboLanguage
            // 
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(228, 409);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(121, 20);
            this.cboLanguage.TabIndex = 113;
            // 
            // btnEbookBrowse
            // 
            this.btnEbookBrowse.Location = new System.Drawing.Point(689, 516);
            this.btnEbookBrowse.Name = "btnEbookBrowse";
            this.btnEbookBrowse.Size = new System.Drawing.Size(96, 30);
            this.btnEbookBrowse.TabIndex = 114;
            this.btnEbookBrowse.Text = "URL 폴더";
            this.btnEbookBrowse.UseVisualStyleBackColor = true;
            this.btnEbookBrowse.Click += new System.EventHandler(this.btnEbookBrowse_Click);
            // 
            // rtxtDescription
            // 
            this.rtxtDescription.Location = new System.Drawing.Point(914, 542);
            this.rtxtDescription.Name = "rtxtDescription";
            this.rtxtDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxtDescription.Size = new System.Drawing.Size(373, 296);
            this.rtxtDescription.TabIndex = 115;
            this.rtxtDescription.Text = "";
            // 
            // pbCoverPreview
            // 
            this.pbCoverPreview.Location = new System.Drawing.Point(914, 36);
            this.pbCoverPreview.Name = "pbCoverPreview";
            this.pbCoverPreview.Size = new System.Drawing.Size(373, 497);
            this.pbCoverPreview.TabIndex = 116;
            this.pbCoverPreview.TabStop = false;
            // 
            // btnPreviewEbook
            // 
            this.btnPreviewEbook.Location = new System.Drawing.Point(620, 689);
            this.btnPreviewEbook.Name = "btnPreviewEbook";
            this.btnPreviewEbook.Size = new System.Drawing.Size(121, 34);
            this.btnPreviewEbook.TabIndex = 117;
            this.btnPreviewEbook.Text = "미리보기";
            this.btnPreviewEbook.UseVisualStyleBackColor = true;
            this.btnPreviewEbook.Click += new System.EventHandler(this.btnPreviewEbook_Click);
            // 
            // EditBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.btnPreviewEbook);
            this.Controls.Add(this.pbCoverPreview);
            this.Controls.Add(this.rtxtDescription);
            this.Controls.Add(this.btnEbookBrowse);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.txtEditLanguage);
            this.Controls.Add(this.cboSelectLanguage);
            this.Controls.Add(this.btnEditLanguage);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnDeleteLanguage);
            this.Controls.Add(this.btnAddLanguage);
            this.Controls.Add(this.txtEditCategory);
            this.Controls.Add(this.cboSelectCategory);
            this.Controls.Add(this.btnEditCategory);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDeleteCategory);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.rdoTitle);
            this.Controls.Add(this.rdoIsbn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnImageBrowse);
            this.Controls.Add(this.chkIsEbook);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEbookUrl);
            this.Controls.Add(this.txtCoverImage);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtIsbn);
            this.Name = "EditBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditBookForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoverPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnImageBrowse;
        private System.Windows.Forms.CheckBox chkIsEbook;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEbookUrl;
        private System.Windows.Forms.TextBox txtCoverImage;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rdoIsbn;
        private System.Windows.Forms.RadioButton rdoTitle;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsbn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnEditCategory;
        private System.Windows.Forms.ComboBox cboSelectCategory;
        private System.Windows.Forms.TextBox txtEditCategory;
        private System.Windows.Forms.TextBox txtEditLanguage;
        private System.Windows.Forms.ComboBox cboSelectLanguage;
        private System.Windows.Forms.Button btnEditLanguage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDeleteLanguage;
        private System.Windows.Forms.Button btnAddLanguage;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Button btnEbookBrowse;
        private System.Windows.Forms.RichTextBox rtxtDescription;
        private System.Windows.Forms.PictureBox pbCoverPreview;
        private System.Windows.Forms.Button btnPreviewEbook;
    }
}