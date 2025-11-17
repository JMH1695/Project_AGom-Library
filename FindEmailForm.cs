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
    public partial class FindEmailForm : Form
    {
        // ✅ 실제 사용 안 하지만 디자이너 오류 방지용으로 남겨둠

        public FindEmailForm()
        {
            InitializeComponent();
            this.AcceptButton = btnFindEmail;

            color_Input();
        }

        private void color_Input()
        {
            pbLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbTitle.BackColor = Color.FromArgb(197, 249, 214);
            btnCancel.BackColor = Color.FromArgb(197, 249, 214);
        }

        private void btnFindEmail_Click_1(object sender, EventArgs e)
        {
            string name = txtFindName.Text.Trim();
            string phone = txtFindPhone.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("모든 필드를 입력해주세요.");
                return;
            }

            try
            {
                // ✅ 전역 DatabaseConfig.ConnectionString 사용
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT email 
                        FROM AGomDB.dbo.Members 
                        WHERE first_name = @Name 
                        AND phone = @Phone 
                        AND admin = 0";  // 관리자 계정 제외

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show($"찾으시는 이메일은 {result.ToString()} 입니다.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("일치하는 정보를 찾을 수 없습니다.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이메일 찾기 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
