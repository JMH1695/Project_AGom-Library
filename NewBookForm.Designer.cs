namespace AGomProject
{
    partial class NewBookForm
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
            this.txtIsbn = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtNewLanguage = new System.Windows.Forms.TextBox();
            this.txtCoverImage = new System.Windows.Forms.TextBox();
            this.txtEbookUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.chkIsEbook = new System.Windows.Forms.CheckBox();
            this.btnImageBrowse = new System.Windows.Forms.Button();
            this.pbCoverPreview = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtNewCategory = new System.Windows.Forms.TextBox();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAddLanguage = new System.Windows.Forms.Button();
            this.rtxtDescription = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnEbookBrowse = new System.Windows.Forms.Button();
            this.btnAddBookInLibrary = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoverPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIsbn
            // 
            this.txtIsbn.Location = new System.Drawing.Point(169, 62);
            this.txtIsbn.Name = "txtIsbn";
            this.txtIsbn.Size = new System.Drawing.Size(293, 21);
            this.txtIsbn.TabIndex = 0;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(169, 99);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(293, 21);
            this.txtTitle.TabIndex = 1;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(169, 136);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(293, 21);
            this.txtAuthor.TabIndex = 2;
            // 
            // txtNewLanguage
            // 
            this.txtNewLanguage.Location = new System.Drawing.Point(169, 810);
            this.txtNewLanguage.Name = "txtNewLanguage";
            this.txtNewLanguage.Size = new System.Drawing.Size(175, 21);
            this.txtNewLanguage.TabIndex = 6;
            // 
            // txtCoverImage
            // 
            this.txtCoverImage.Location = new System.Drawing.Point(169, 289);
            this.txtCoverImage.Name = "txtCoverImage";
            this.txtCoverImage.Size = new System.Drawing.Size(446, 21);
            this.txtCoverImage.TabIndex = 8;
            this.txtCoverImage.TextChanged += new System.EventHandler(this.txtCoverImage_TextChanged);
            // 
            // txtEbookUrl
            // 
            this.txtEbookUrl.Location = new System.Drawing.Point(169, 326);
            this.txtEbookUrl.Name = "txtEbookUrl";
            this.txtEbookUrl.Size = new System.Drawing.Size(446, 21);
            this.txtEbookUrl.TabIndex = 9;
            this.txtEbookUrl.TextChanged += new System.EventHandler(this.txtEbookUrl_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "ISBN Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "도서 제목";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "저자";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "카테고리";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "언어";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "전자책 여부";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "표지 이미지 경로";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "전자책 URL";
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(169, 176);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(121, 20);
            this.cboCategory.TabIndex = 3;
            // 
            // chkIsEbook
            // 
            this.chkIsEbook.AutoSize = true;
            this.chkIsEbook.Location = new System.Drawing.Point(169, 254);
            this.chkIsEbook.Name = "chkIsEbook";
            this.chkIsEbook.Size = new System.Drawing.Size(15, 14);
            this.chkIsEbook.TabIndex = 7;
            this.chkIsEbook.UseVisualStyleBackColor = true;
            this.chkIsEbook.CheckedChanged += new System.EventHandler(this.chkIsEbook_CheckedChanged);
            // 
            // btnImageBrowse
            // 
            this.btnImageBrowse.Location = new System.Drawing.Point(621, 282);
            this.btnImageBrowse.Name = "btnImageBrowse";
            this.btnImageBrowse.Size = new System.Drawing.Size(96, 30);
            this.btnImageBrowse.TabIndex = 10;
            this.btnImageBrowse.Text = "이미지 검색";
            this.btnImageBrowse.UseVisualStyleBackColor = true;
            this.btnImageBrowse.Click += new System.EventHandler(this.btnImageBrowse_Click);
            // 
            // pbCoverPreview
            // 
            this.pbCoverPreview.Location = new System.Drawing.Point(844, 12);
            this.pbCoverPreview.Name = "pbCoverPreview";
            this.pbCoverPreview.Size = new System.Drawing.Size(373, 497);
            this.pbCoverPreview.TabIndex = 18;
            this.pbCoverPreview.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(639, 719);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(121, 34);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "도서 추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(639, 777);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 34);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "뒤로가기";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtNewCategory
            // 
            this.txtNewCategory.Location = new System.Drawing.Point(169, 772);
            this.txtNewCategory.Name = "txtNewCategory";
            this.txtNewCategory.Size = new System.Drawing.Size(175, 21);
            this.txtNewCategory.TabIndex = 12;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(361, 772);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(54, 23);
            this.btnAddCategory.TabIndex = 13;
            this.btnAddCategory.Text = "입력";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(50, 777);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "카테고리 추가 입력";
            // 
            // cboLanguage
            // 
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(169, 214);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(121, 20);
            this.cboLanguage.TabIndex = 101;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 813);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 12);
            this.label10.TabIndex = 102;
            this.label10.Text = "언어 추가 입력";
            // 
            // btnAddLanguage
            // 
            this.btnAddLanguage.Location = new System.Drawing.Point(361, 808);
            this.btnAddLanguage.Name = "btnAddLanguage";
            this.btnAddLanguage.Size = new System.Drawing.Size(54, 23);
            this.btnAddLanguage.TabIndex = 103;
            this.btnAddLanguage.Text = "입력";
            this.btnAddLanguage.UseVisualStyleBackColor = true;
            this.btnAddLanguage.Click += new System.EventHandler(this.btnAddLanguage_Click);
            // 
            // rtxtDescription
            // 
            this.rtxtDescription.Location = new System.Drawing.Point(844, 515);
            this.rtxtDescription.Name = "rtxtDescription";
            this.rtxtDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxtDescription.Size = new System.Drawing.Size(373, 296);
            this.rtxtDescription.TabIndex = 104;
            this.rtxtDescription.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(771, 518);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 12);
            this.label11.TabIndex = 105;
            this.label11.Text = "도서 설명";
            // 
            // btnEbookBrowse
            // 
            this.btnEbookBrowse.Location = new System.Drawing.Point(621, 321);
            this.btnEbookBrowse.Name = "btnEbookBrowse";
            this.btnEbookBrowse.Size = new System.Drawing.Size(96, 30);
            this.btnEbookBrowse.TabIndex = 106;
            this.btnEbookBrowse.Text = "URL 폴더";
            this.btnEbookBrowse.UseVisualStyleBackColor = true;
            this.btnEbookBrowse.Click += new System.EventHandler(this.btnEbookBrowse_Click);
            // 
            // btnAddBookInLibrary
            // 
            this.btnAddBookInLibrary.Location = new System.Drawing.Point(198, 661);
            this.btnAddBookInLibrary.Name = "btnAddBookInLibrary";
            this.btnAddBookInLibrary.Size = new System.Drawing.Size(121, 34);
            this.btnAddBookInLibrary.TabIndex = 107;
            this.btnAddBookInLibrary.Text = "도서관 재고 추가";
            this.btnAddBookInLibrary.UseVisualStyleBackColor = true;
            this.btnAddBookInLibrary.Click += new System.EventHandler(this.btnAddBookInLibrary_Click);
            // 
            // NewBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.btnAddBookInLibrary);
            this.Controls.Add(this.btnEbookBrowse);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rtxtDescription);
            this.Controls.Add(this.btnAddLanguage);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.txtNewCategory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pbCoverPreview);
            this.Controls.Add(this.btnImageBrowse);
            this.Controls.Add(this.chkIsEbook);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEbookUrl);
            this.Controls.Add(this.txtCoverImage);
            this.Controls.Add(this.txtNewLanguage);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtIsbn);
            this.Name = "NewBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewBookForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbCoverPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtNewLanguage;
        private System.Windows.Forms.TextBox txtCoverImage;
        private System.Windows.Forms.TextBox txtEbookUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.CheckBox chkIsEbook;
        private System.Windows.Forms.Button btnImageBrowse;
        private System.Windows.Forms.PictureBox pbCoverPreview;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtNewCategory;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAddLanguage;
        private System.Windows.Forms.RichTextBox rtxtDescription;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnEbookBrowse;
        private System.Windows.Forms.Button btnAddBookInLibrary;
    }
}