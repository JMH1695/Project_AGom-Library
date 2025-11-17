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

namespace AGomProject
{
    public partial class ChangePasswordForm : Form
    {
        // ✅ 개별 connectionString 제거 (디자이너 오류 방지용으로는 유지 가능하지만 실제 사용 안 함)
        private int userId;

        public ChangePasswordForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmNewPassword.Text;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("새 비밀번호를 입력해주세요.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            try
            {
                // ✅ 통합된 전역 연결 문자열 사용
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE AGomDB.dbo.Members SET password = @Password WHERE member_id = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("비밀번호가 성공적으로 변경되었습니다.");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("비밀번호 변경에 실패했습니다.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비밀번호 변경 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
