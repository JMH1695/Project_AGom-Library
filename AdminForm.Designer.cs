namespace AGomProject
{
    partial class AdminForm
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
            this.pbEditBook = new System.Windows.Forms.PictureBox();
            this.pbNewBook = new System.Windows.Forms.PictureBox();
            this.pbStatistics = new System.Windows.Forms.PictureBox();
            this.pbLibraryManage = new System.Windows.Forms.PictureBox();
            this.lbEditBook = new System.Windows.Forms.Label();
            this.lbNewBook = new System.Windows.Forms.Label();
            this.lbStatistics = new System.Windows.Forms.Label();
            this.lbLibraryManage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lblCancle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLibraryManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbEditBook
            // 
            this.pbEditBook.BackColor = System.Drawing.Color.White;
            this.pbEditBook.Location = new System.Drawing.Point(-1, 129);
            this.pbEditBook.Name = "pbEditBook";
            this.pbEditBook.Size = new System.Drawing.Size(280, 200);
            this.pbEditBook.TabIndex = 0;
            this.pbEditBook.TabStop = false;
            this.pbEditBook.Tag = "";
            this.pbEditBook.Click += new System.EventHandler(this.pbEditBook_Click);
            // 
            // pbNewBook
            // 
            this.pbNewBook.BackColor = System.Drawing.Color.White;
            this.pbNewBook.Location = new System.Drawing.Point(278, 129);
            this.pbNewBook.Name = "pbNewBook";
            this.pbNewBook.Size = new System.Drawing.Size(280, 200);
            this.pbNewBook.TabIndex = 1;
            this.pbNewBook.TabStop = false;
            this.pbNewBook.Click += new System.EventHandler(this.pbNewBook_Click);
            // 
            // pbStatistics
            // 
            this.pbStatistics.BackColor = System.Drawing.Color.White;
            this.pbStatistics.Location = new System.Drawing.Point(-1, 328);
            this.pbStatistics.Name = "pbStatistics";
            this.pbStatistics.Size = new System.Drawing.Size(280, 200);
            this.pbStatistics.TabIndex = 2;
            this.pbStatistics.TabStop = false;
            this.pbStatistics.Click += new System.EventHandler(this.pbStatistics_Click);
            // 
            // pbLibraryManage
            // 
            this.pbLibraryManage.BackColor = System.Drawing.Color.White;
            this.pbLibraryManage.Location = new System.Drawing.Point(278, 328);
            this.pbLibraryManage.Name = "pbLibraryManage";
            this.pbLibraryManage.Size = new System.Drawing.Size(280, 200);
            this.pbLibraryManage.TabIndex = 3;
            this.pbLibraryManage.TabStop = false;
            this.pbLibraryManage.Click += new System.EventHandler(this.pbLibraryManage_Click);
            // 
            // lbEditBook
            // 
            this.lbEditBook.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbEditBook.Location = new System.Drawing.Point(9, 133);
            this.lbEditBook.Name = "lbEditBook";
            this.lbEditBook.Size = new System.Drawing.Size(263, 192);
            this.lbEditBook.TabIndex = 4;
            this.lbEditBook.Text = "기존 도서 수정";
            this.lbEditBook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEditBook.Click += new System.EventHandler(this.pbEditBook_Click);
            // 
            // lbNewBook
            // 
            this.lbNewBook.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbNewBook.Location = new System.Drawing.Point(285, 133);
            this.lbNewBook.Name = "lbNewBook";
            this.lbNewBook.Size = new System.Drawing.Size(251, 192);
            this.lbNewBook.TabIndex = 5;
            this.lbNewBook.Text = "신규 도서 기입";
            this.lbNewBook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNewBook.Click += new System.EventHandler(this.pbNewBook_Click);
            // 
            // lbStatistics
            // 
            this.lbStatistics.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbStatistics.Location = new System.Drawing.Point(10, 332);
            this.lbStatistics.Name = "lbStatistics";
            this.lbStatistics.Size = new System.Drawing.Size(262, 186);
            this.lbStatistics.TabIndex = 6;
            this.lbStatistics.Text = "대여 통계";
            this.lbStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbStatistics.Click += new System.EventHandler(this.pbStatistics_Click);
            // 
            // lbLibraryManage
            // 
            this.lbLibraryManage.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbLibraryManage.Location = new System.Drawing.Point(285, 332);
            this.lbLibraryManage.Name = "lbLibraryManage";
            this.lbLibraryManage.Size = new System.Drawing.Size(251, 186);
            this.lbLibraryManage.TabIndex = 7;
            this.lbLibraryManage.Text = "대여 관리";
            this.lbLibraryManage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLibraryManage.Click += new System.EventHandler(this.pbLibraryManage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Image = global::AGomProject.Properties.Resources.Green;
            this.pictureBox1.Location = new System.Drawing.Point(-5, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(550, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.White;
            this.pbLogo.Image = global::AGomProject.Properties.Resources.Logo1;
            this.pbLogo.Location = new System.Drawing.Point(12, 9);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 100);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 12;
            this.pbLogo.TabStop = false;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.BackColor = System.Drawing.Color.White;
            this.lbTitle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTitle.Location = new System.Drawing.Point(184, 47);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(191, 34);
            this.lbTitle.TabIndex = 17;
            this.lbTitle.Text = "관리자 모드";
            // 
            // lblCancle
            // 
            this.lblCancle.BackColor = System.Drawing.Color.White;
            this.lblCancle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCancle.Location = new System.Drawing.Point(462, 11);
            this.lblCancle.Name = "lblCancle";
            this.lblCancle.Padding = new System.Windows.Forms.Padding(15);
            this.lblCancle.Size = new System.Drawing.Size(70, 70);
            this.lblCancle.TabIndex = 18;
            this.lblCancle.Text = "돌아가기";
            this.lblCancle.Click += new System.EventHandler(this.lblCancle_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 527);
            this.Controls.Add(this.lblCancle);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lbLibraryManage);
            this.Controls.Add(this.lbStatistics);
            this.Controls.Add(this.lbNewBook);
            this.Controls.Add(this.lbEditBook);
            this.Controls.Add(this.pbLibraryManage);
            this.Controls.Add(this.pbStatistics);
            this.Controls.Add(this.pbNewBook);
            this.Controls.Add(this.pbEditBook);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbEditBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLibraryManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEditBook;
        private System.Windows.Forms.PictureBox pbNewBook;
        private System.Windows.Forms.PictureBox pbStatistics;
        private System.Windows.Forms.PictureBox pbLibraryManage;
        private System.Windows.Forms.Label lbEditBook;
        private System.Windows.Forms.Label lbNewBook;
        private System.Windows.Forms.Label lbStatistics;
        private System.Windows.Forms.Label lbLibraryManage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lblCancle;
    }
}