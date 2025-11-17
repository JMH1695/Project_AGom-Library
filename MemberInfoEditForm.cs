using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class MemberInfoEditForm : Form
    {
        private int memberId;
        private bool EditMode = false;

        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)
        public MemberInfoEditForm(int memberId)
        {
            this.memberId = memberId;
            InitializeComponent();
            LoadMemberData();

            tbEditPasswd.PasswordChar = '●';
            tbEditPasswdCheck.PasswordChar = '●';

            color_Input();
        }

        private void color_Input()
        {
            pbLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbTitle.BackColor = Color.FromArgb(197, 249, 214);
            lbEditSaveEditButton.BackColor = Color.FromArgb(217, 217, 16);
            lbEditBackButton.BackColor = Color.FromArgb(65, 158, 59);
        }

        private void LoadMemberData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 통합된 연결문자열 사용
            {
                string query = @"
                    SELECT first_name, email, password, phone, birth_date, gender
                    FROM Members
                    WHERE member_id = @memberId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@memberId", memberId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            tbEditName.Text = reader["first_name"].ToString();
                            tbEditEmail.Text = reader["email"].ToString();
                            tbEditPasswd.Text = reader["password"].ToString();
                            tbEditPasswdCheck.Text = reader["password"].ToString();
                            tbEditPhoneNb.Text = reader["phone"].ToString();

                            if (DateTime.TryParse(reader["birth_date"].ToString(), out DateTime birthDate))
                            {
                                dtpEditBirthDay.Value = birthDate;
                            }

                            cbEditGender.Text = reader["gender"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("회원 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pbEditSaveEditButton_Click(object sender, EventArgs e)
        {
            if (!EditMode)
            {
                EditMode = true;

                tbEditName.ReadOnly = false;
                tbEditPasswd.ReadOnly = false;
                tbEditPasswdCheck.ReadOnly = false;
                tbEditPhoneNb.ReadOnly = false;
                dtpEditBirthDay.Enabled = true;
                cbEditGender.Enabled = true;

                lbEditSaveEditButton.Text = "저장하기";
            }
            else
            {
                if (tbEditPasswd.Text != tbEditPasswdCheck.Text)
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.");
                    return;
                }

                EditMode = false;
                tbEditName.ReadOnly = true;
                tbEditPasswd.ReadOnly = true;
                tbEditPasswdCheck.ReadOnly = true;
                tbEditPhoneNb.ReadOnly = true;
                dtpEditBirthDay.Enabled = false;
                cbEditGender.Enabled = false;

                UpdateMemberData();
                lbEditSaveEditButton.Text = "수정하기";
            }
        }

        private void UpdateMemberData()
        {
            string firstName = tbEditName.Text;
            string email = tbEditEmail.Text;
            string password = tbEditPasswd.Text;
            string phone = tbEditPhoneNb.Text;
            DateTime birthDate = dtpEditBirthDay.Value;
            string gender = cbEditGender.SelectedItem?.ToString() ?? "";

            string query = @"
                UPDATE Members
                SET first_name = @firstName, 
                    email = @Email, 
                    password = @password, 
                    phone = @phone, 
                    birth_date = @birthDate, 
                    gender = @gender
                WHERE member_id = @memberId";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 통합된 연결문자열 사용
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@birthDate", birthDate);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@memberId", memberId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("회원 정보가 성공적으로 업데이트되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("회원 정보 업데이트에 실패했습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"업데이트 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pbEditBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
