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
    public partial class LoanStatusForm : Form
    {
        private int adminId;

        // ⚠️ 디자이너 오류 방지용 (실제 사용 안 함)
        public LoanStatusForm(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;

            // ComboBox 설정
            cboSearchType.Items.AddRange(new string[] { "도서명", "회원명", "도서관" });
            cboSearchType.SelectedIndex = 0;
            cboSearchType.DropDownStyle = ComboBoxStyle.DropDownList; // 콤보박스 수정 불가

            // DataGridView 설정
            dgvLoanStatus.AllowUserToAddRows = false;
            dgvLoanStatus.AllowUserToDeleteRows = false;
            dgvLoanStatus.AllowUserToOrderColumns = false;
            dgvLoanStatus.ReadOnly = true;
            dgvLoanStatus.AutoGenerateColumns = false;
            dgvLoanStatus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 검색창 엔터키 이벤트 연결
            this.AcceptButton = btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchLoans();
        }

        private void SearchLoans()
        {
            // ✅ 전역 DatabaseConfig.ConnectionString 사용
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string searchType = cboSearchType.SelectedItem.ToString();
                string keyword = txtSearchKeyword.Text.Trim();

                string query = @"
                    SELECT 
                        m.first_name,
                        b.title,
                        CASE WHEN b.is_ebook = 1 THEN 'O' ELSE 'X' END as is_ebook,
                        CASE WHEN b.is_ebook = 1 THEN '-' ELSE l.library_name END as library_name,
                        br.borrow_date,
                        br.due_date,
                        br.return_status
                    FROM Borrows br
                    JOIN Members m ON br.member_id = m.member_id
                    JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                    JOIN Books b ON lc.isbn = b.isbn
                    JOIN Libraries l ON lc.library_id = l.library_id
                    WHERE br.return_status != '반납됨' AND ";

                switch (searchType)
                {
                    case "도서명":
                        query += "b.title LIKE '%' + @keyword + '%'";
                        break;
                    case "회원명":
                        query += "m.first_name LIKE '%' + @keyword + '%'";
                        break;
                    case "도서관":
                        query += "l.library_name LIKE '%' + @keyword + '%'";
                        break;
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", keyword);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvLoanStatus.DataSource = dt;
                    }
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
