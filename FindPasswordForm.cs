using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AGomProject.RegisterForm;

namespace AGomProject
{
    public partial class FindPasswordForm : Form
    {
        // ✅ 디자이너 오류 방지용으로만 유지 (실제 사용 안 함)

        private int userId = -1;

        public FindPasswordForm()
        {
            InitializeComponent();
            LoadSecurityQuestions();  // 폼 로드 시 보안 질문 목록 로드
            this.AcceptButton = btnVerify;

            color_Input();
        }

        private void color_Input()
        {
            pbLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbTitle.BackColor = Color.FromArgb(197, 249, 214);
            btnCancel.BackColor = Color.FromArgb(197, 249, 214);
        }

        private void LoadSecurityQuestions()
        {
            try
            {
                // ✅ 전역 설정 사용
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT question_id, question_text FROM AGomDB.dbo.SecurityQuestions";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
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
                }

                if (cboSecurityQuestion.Items.Count > 0)
                    cboSecurityQuestion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"보안 질문을 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            string email = txtFindPwEmail.Text.Trim();
            string name = txtFindPwName.Text.Trim();
            string answer = txtSecurityAnswer.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(answer) || cboSecurityQuestion.SelectedItem == null)
            {
                MessageBox.Show("모든 필드를 입력해주세요.");
                return;
            }

            try
            {
                // ✅ 전역 DB 연결 문자열 사용
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT member_id 
                        FROM AGomDB.dbo.Members 
                        WHERE email = @Email 
                        AND first_name = @Name 
                        AND security_question_id = @QuestionId
                        AND security_answer = @Answer
                        AND admin = 0";  // 관리자 계정 제외

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@QuestionId",
                            ((SecurityQuestion)cboSecurityQuestion.SelectedItem).QuestionId);
                        cmd.Parameters.AddWithValue("@Answer", answer);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            userId = Convert.ToInt32(result);
                            ChangePasswordForm changePasswordForm = new ChangePasswordForm(userId);
                            this.Hide();
                            if (changePasswordForm.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show("비밀번호가 성공적으로 변경되었습니다.");
                                this.Close();
                            }
                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("입력하신 정보가 일치하지 않습니다.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검증 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        public class SecurityQuestion
        {
            public int QuestionId { get; set; }
            public string QuestionText { get; set; }

            public override string ToString()
            {
                return QuestionText;
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
