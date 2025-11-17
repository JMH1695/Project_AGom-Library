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
    public partial class MemberManagementForm : Form
    {
        private int adminId;

        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)
        public MemberManagementForm(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;

            // ComboBox 초기화
            cboSearchType.Items.AddRange(new string[] { "이메일", "이름", "전화번호" });
            cboSearchType.SelectedIndex = 0;
            cboSearchType.DropDownStyle = ComboBoxStyle.DropDownList; // 수정 불가

            // DataGridView 설정
            dgvLoanHistory.AllowUserToAddRows = false;
            dgvLoanHistory.AllowUserToDeleteRows = false;
            dgvLoanHistory.AllowUserToOrderColumns = false;
            dgvLoanHistory.ReadOnly = true;
            dgvLoanHistory.AutoGenerateColumns = false;
            dgvLoanHistory.ColumnHeadersVisible = true;
            dgvLoanHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvMembers.ColumnHeadersVisible = true;
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.MultiSelect = false;
            dgvMembers.SelectionChanged += new EventHandler(dgvMembers_SelectionChanged);

            dgvFullHistory.AllowUserToAddRows = false;
            dgvFullHistory.AllowUserToDeleteRows = false;
            dgvFullHistory.AllowUserToOrderColumns = false;
            dgvFullHistory.ReadOnly = true;
            dgvFullHistory.AutoGenerateColumns = false;
            dgvFullHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.AcceptButton = btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMembers();
        }

        private void dgvMembers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow != null)
            {
                try
                {
                    int memberId = Convert.ToInt32(dgvMembers.CurrentRow.Cells["colMemberId"].Value);
                    LoadMemberLoanInfo(memberId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("회원 정보를 불러오는 중 오류가 발생했습니다: " + ex.Message);
                }
            }
        }

        private void SearchMembers()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 통합된 연결문자열 사용
            {
                conn.Open();
                string searchType = cboSearchType.SelectedItem.ToString();
                string keyword = txtSearchKeyword.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("검색어를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string query = @"
                    SELECT m.member_id, m.email, m.first_name, m.gender, m.birth_date, m.phone
                    FROM Members m
                    WHERE m.admin = 0 AND "; // 관리자 제외

                switch (searchType)
                {
                    case "이메일":
                        query += "m.email LIKE '%' + @keyword + '%'";
                        break;
                    case "이름":
                        query += "m.first_name LIKE '%' + @keyword + '%'";
                        break;
                    case "전화번호":
                        query += "m.phone LIKE '%' + @keyword";
                        break;
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", keyword);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("검색 결과가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dgvMembers.DataSource = dt;
                    }
                }
            }
        }

        private void LoadMemberLoanInfo(int memberId)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 통합된 연결문자열 사용
            {
                conn.Open();

                // 최근 대여 이력 (현재 대여 중 + 최근 반납)
                string recentHistoryQuery = @"
                    SELECT 
                        b.title,
                        CASE WHEN b.is_ebook = 1 THEN 'O' ELSE 'X' END as is_ebook,
                        CASE WHEN b.is_ebook = 1 THEN '-' ELSE l.library_name END as library_name,
                        br.borrow_date,
                        br.due_date,
                        br.return_date,
                        br.return_status
                    FROM Borrows br
                    JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                    JOIN Books b ON lc.isbn = b.isbn
                    JOIN Libraries l ON lc.library_id = l.library_id
                    WHERE br.member_id = @memberId
                    AND (br.return_status = '대여 중'
                         OR (br.return_status = '반납됨' 
                             AND br.return_date >= DATEADD(day, -7, GETDATE())))
                    ORDER BY br.borrow_date DESC";

                using (SqlCommand cmd = new SqlCommand(recentHistoryQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@memberId", memberId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvLoanHistory.DataSource = dt;
                    }
                }

                // 전체 대여 이력
                string fullHistoryQuery = @"
                    SELECT 
                        b.title,
                        CASE WHEN b.is_ebook = 1 THEN 'O' ELSE 'X' END as is_ebook,
                        CASE WHEN b.is_ebook = 1 THEN '-' ELSE l.library_name END as library_name,
                        br.borrow_date,
                        br.due_date,
                        br.return_date
                    FROM Borrows br
                    JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                    JOIN Books b ON lc.isbn = b.isbn
                    JOIN Libraries l ON lc.library_id = l.library_id
                    WHERE br.member_id = @memberId
                    ORDER BY br.borrow_date DESC";

                using (SqlCommand cmd = new SqlCommand(fullHistoryQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@memberId", memberId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvFullHistory.DataSource = dt;
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
