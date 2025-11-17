namespace AGomProject
{
    partial class LibraryManagementForm
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
            this.pbLoanStatus = new System.Windows.Forms.PictureBox();
            this.pbMemberManagement = new System.Windows.Forms.PictureBox();
            this.lbMemberManagement = new System.Windows.Forms.Label();
            this.lbLoanStatus = new System.Windows.Forms.Label();
            this.lblCancle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoanStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMemberManagement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLoanStatus
            // 
            this.pbLoanStatus.BackColor = System.Drawing.Color.White;
            this.pbLoanStatus.Location = new System.Drawing.Point(-1, 129);
            this.pbLoanStatus.Name = "pbLoanStatus";
            this.pbLoanStatus.Size = new System.Drawing.Size(300, 385);
            this.pbLoanStatus.TabIndex = 1;
            this.pbLoanStatus.TabStop = false;
            this.pbLoanStatus.Click += new System.EventHandler(this.pbLoanStatus_Click);
            // 
            // pbMemberManagement
            // 
            this.pbMemberManagement.BackColor = System.Drawing.Color.White;
            this.pbMemberManagement.Location = new System.Drawing.Point(298, 129);
            this.pbMemberManagement.Name = "pbMemberManagement";
            this.pbMemberManagement.Size = new System.Drawing.Size(300, 385);
            this.pbMemberManagement.TabIndex = 0;
            this.pbMemberManagement.TabStop = false;
            this.pbMemberManagement.Click += new System.EventHandler(this.pbMemberManagement_Click);
            // 
            // lbMemberManagement
            // 
            this.lbMemberManagement.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbMemberManagement.Location = new System.Drawing.Point(308, 140);
            this.lbMemberManagement.Name = "lbMemberManagement";
            this.lbMemberManagement.Size = new System.Drawing.Size(272, 364);
            this.lbMemberManagement.TabIndex = 2;
            this.lbMemberManagement.Text = "회원 관리";
            this.lbMemberManagement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbMemberManagement.Click += new System.EventHandler(this.pbMemberManagement_Click);
            // 
            // lbLoanStatus
            // 
            this.lbLoanStatus.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbLoanStatus.Location = new System.Drawing.Point(8, 140);
            this.lbLoanStatus.Name = "lbLoanStatus";
            this.lbLoanStatus.Size = new System.Drawing.Size(280, 364);
            this.lbLoanStatus.TabIndex = 3;
            this.lbLoanStatus.Text = "도서 대여 현황";
            this.lbLoanStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLoanStatus.Click += new System.EventHandler(this.pbLoanStatus_Click);
            // 
            // lblCancle
            // 
            this.lblCancle.BackColor = System.Drawing.Color.White;
            this.lblCancle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCancle.Location = new System.Drawing.Point(510, 9);
            this.lblCancle.Name = "lblCancle";
            this.lblCancle.Padding = new System.Windows.Forms.Padding(15);
            this.lblCancle.Size = new System.Drawing.Size(70, 70);
            this.lblCancle.TabIndex = 10;
            this.lblCancle.Text = "돌아가기";
            this.lblCancle.Click += new System.EventHandler(this.lblCancle_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Image = global::AGomProject.Properties.Resources.Green;
            this.pictureBox1.Location = new System.Drawing.Point(-8, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.White;
            this.pbLogo.Image = global::AGomProject.Properties.Resources.Logo1;
            this.pbLogo.Location = new System.Drawing.Point(12, 12);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 100);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 12;
            this.pbLogo.TabStop = false;
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.White;
            this.lbTitle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTitle.Location = new System.Drawing.Point(171, 36);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(264, 52);
            this.lbTitle.TabIndex = 18;
            this.lbTitle.Text = "도서 관리 모드";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LibraryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 513);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lblCancle);
            this.Controls.Add(this.lbLoanStatus);
            this.Controls.Add(this.lbMemberManagement);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbLoanStatus);
            this.Controls.Add(this.pbMemberManagement);
            this.Name = "LibraryManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LibraryManagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbLoanStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMemberManagement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLoanStatus;
        private System.Windows.Forms.PictureBox pbMemberManagement;
        private System.Windows.Forms.Label lbMemberManagement;
        private System.Windows.Forms.Label lbLoanStatus;
        private System.Windows.Forms.Label lblCancle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbTitle;
    }
}