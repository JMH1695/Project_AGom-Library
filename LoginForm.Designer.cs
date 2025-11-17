namespace AGomProject
{
    partial class LoginForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblGetEmail = new System.Windows.Forms.Label();
            this.lblGetPassword = new System.Windows.Forms.Label();
            this.btnFindEmail = new System.Windows.Forms.Button();
            this.lblFindEmail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindPassword = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lbLogoText1 = new System.Windows.Forms.Label();
            this.lbLogoText2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(129, 187);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(209, 21);
            this.txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(129, 238);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(209, 21);
            this.txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(364, 187);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(79, 72);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(313, 343);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(95, 23);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "회원가입";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblGetEmail
            // 
            this.lblGetEmail.AutoSize = true;
            this.lblGetEmail.Font = new System.Drawing.Font("휴먼둥근헤드라인", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblGetEmail.Location = new System.Drawing.Point(37, 189);
            this.lblGetEmail.Name = "lblGetEmail";
            this.lblGetEmail.Size = new System.Drawing.Size(93, 19);
            this.lblGetEmail.TabIndex = 4;
            this.lblGetEmail.Text = "이메일 : ";
            // 
            // lblGetPassword
            // 
            this.lblGetPassword.AutoSize = true;
            this.lblGetPassword.Font = new System.Drawing.Font("휴먼둥근헤드라인", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblGetPassword.Location = new System.Drawing.Point(18, 238);
            this.lblGetPassword.Name = "lblGetPassword";
            this.lblGetPassword.Size = new System.Drawing.Size(112, 19);
            this.lblGetPassword.TabIndex = 5;
            this.lblGetPassword.Text = "비밀번호 : ";
            // 
            // btnFindEmail
            // 
            this.btnFindEmail.Location = new System.Drawing.Point(313, 387);
            this.btnFindEmail.Name = "btnFindEmail";
            this.btnFindEmail.Size = new System.Drawing.Size(95, 23);
            this.btnFindEmail.TabIndex = 7;
            this.btnFindEmail.Text = "이메일 찾기";
            this.btnFindEmail.UseVisualStyleBackColor = true;
            this.btnFindEmail.Click += new System.EventHandler(this.btnFindEmail_Click);
            // 
            // lblFindEmail
            // 
            this.lblFindEmail.AutoSize = true;
            this.lblFindEmail.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFindEmail.Location = new System.Drawing.Point(44, 390);
            this.lblFindEmail.Name = "lblFindEmail";
            this.lblFindEmail.Size = new System.Drawing.Size(165, 16);
            this.lblFindEmail.TabIndex = 8;
            this.lblFindEmail.Text = "이메일을 잊으셨나요?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(44, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "비밀번호를 잊으셨나요?";
            // 
            // btnFindPassword
            // 
            this.btnFindPassword.Location = new System.Drawing.Point(313, 431);
            this.btnFindPassword.Name = "btnFindPassword";
            this.btnFindPassword.Size = new System.Drawing.Size(95, 23);
            this.btnFindPassword.TabIndex = 10;
            this.btnFindPassword.Text = "비밀번호 찾기";
            this.btnFindPassword.UseVisualStyleBackColor = true;
            this.btnFindPassword.Click += new System.EventHandler(this.btnFindPassword_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AGomProject.Properties.Resources.Green;
            this.pictureBox1.Location = new System.Drawing.Point(-8, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(482, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(45, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "처음 접속하셨나요?";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::AGomProject.Properties.Resources.Logo1;
            this.pbLogo.Location = new System.Drawing.Point(10, 10);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(120, 120);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 23;
            this.pbLogo.TabStop = false;
            // 
            // lbLogoText1
            // 
            this.lbLogoText1.AutoSize = true;
            this.lbLogoText1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbLogoText1.Location = new System.Drawing.Point(136, 18);
            this.lbLogoText1.Name = "lbLogoText1";
            this.lbLogoText1.Size = new System.Drawing.Size(222, 50);
            this.lbLogoText1.TabIndex = 24;
            this.lbLogoText1.Text = "AGOM ";
            // 
            // lbLogoText2
            // 
            this.lbLogoText2.AutoSize = true;
            this.lbLogoText2.Font = new System.Drawing.Font("휴먼둥근헤드라인", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbLogoText2.Location = new System.Drawing.Point(136, 70);
            this.lbLogoText2.Name = "lbLogoText2";
            this.lbLogoText2.Size = new System.Drawing.Size(277, 50);
            this.lbLogoText2.TabIndex = 25;
            this.lbLogoText2.Text = "Libraries";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 466);
            this.Controls.Add(this.lbLogoText2);
            this.Controls.Add(this.lbLogoText1);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnFindPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFindEmail);
            this.Controls.Add(this.btnFindEmail);
            this.Controls.Add(this.lblGetPassword);
            this.Controls.Add(this.lblGetEmail);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGOM Libraries 로그인";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblGetEmail;
        private System.Windows.Forms.Label lblGetPassword;
        private System.Windows.Forms.Button btnFindEmail;
        private System.Windows.Forms.Label lblFindEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbLogoText1;
        private System.Windows.Forms.Label lbLogoText2;
    }
}

