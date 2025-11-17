using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AGomProject
{
    public partial class RegisterForm : Form
    {
        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)

        public RegisterForm()
        {
            InitializeComponent();
            InitializeGenderComboBox();
            LoadSecurityQuestions();


            txtPassword.PasswordChar = '●';
            txtConfirmPassword.PasswordChar = '●';
            this.AcceptButton = btnRegister;

            color_Input();
        }

        // 🎨 색상 설정
        private void color_Input()
        {
            pbLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbTitle.BackColor = Color.FromArgb(197, 249, 214);
            btnCancel.BackColor = Color.FromArgb(197, 249, 214);
        }

        // 👤 성별 콤보박스 초기화
        private void InitializeGenderComboBox()
        {
            cboGender.Items.Add("남자");
            cboGender.Items.Add("여자");
        }

        // 🔐 보안 질문 목록 불러오기
        private void LoadSecurityQuestions()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT question_id, question_text FROM AGomDB.dbo.SecurityQuestions";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cboSecurityQuestion.Items.Clear();
                        while (reader.Read())
                        {
                            cboSecurityQuestion.Items.Add(new SecurityQuestion
                            {
                                QuestionId = reader.GetInt32(0),
                                QuestionText = reader.GetString(1)
                            });
                        }
                    }
                }

                if (cboSecurityQuestion.Items.Count > 0)
                    cboSecurityQuestion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"보안 질문을 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        // 🧾 회원가입 버튼 클릭
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // ✅ 필수 입력값 검증
            if (string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtSecurityAnswer.Text))
            {
                MessageBox.Show("필수 항목을 모두 입력해주세요.");
                return;
            }

            // ✅ 이메일 형식 검증
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("올바른 이메일 형식이 아닙니다.");
                return;
            }

            // ✅ 비밀번호 일치 확인
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // ✅ 이메일 중복 검사 (Trim + 소문자 변환)
                    string checkQuery = "SELECT COUNT(*) FROM AGomDB.dbo.Members WHERE LOWER(email) = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim().ToLower());
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("이미 등록된 이메일입니다.");
                            return;
                        }
                    }

                    // ✅ 회원 등록
                    string insertQuery = @"
                        INSERT INTO AGomDB.dbo.Members 
                        (password, first_name, gender, birth_date, email, phone, admin, security_question_id, security_answer)
                        VALUES 
                        (@Password, @FirstName, @Gender, @BirthDate, @Email, @Phone, 0, @QuestionId, @SecurityAnswer)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@FirstName", txtName.Text);
                        cmd.Parameters.AddWithValue("@Gender",
                            cboGender.SelectedItem != null ? cboGender.SelectedItem.ToString() : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BirthDate", dtpBirthDate.Value);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim().ToLower());
                        cmd.Parameters.AddWithValue("@Phone",
                            string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text);
                        cmd.Parameters.AddWithValue("@QuestionId",
                            ((SecurityQuestion)cboSecurityQuestion.SelectedItem).QuestionId);
                        cmd.Parameters.AddWithValue("@SecurityAnswer", txtSecurityAnswer.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("회원가입이 완료되었습니다!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"회원가입 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        // ✉️ 이메일 유효성 검사
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // 🔒 보안 질문 모델 클래스
        public class SecurityQuestion
        {
            public int QuestionId { get; set; }
            public string QuestionText { get; set; }
            public override string ToString() => QuestionText;
        }

        // ❌ 취소 버튼
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
