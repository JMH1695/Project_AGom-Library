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
    public partial class LibraryInventoryForm : Form
    {
        // ⚠️ 디자이너 오류 방지를 위해 남겨둠 (실제 사용 안 함)
        public LibraryInventoryForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgvLibraries.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLibrarySearch.Text))
            {
                MessageBox.Show("도서관명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ DatabaseConfig.ConnectionString 사용
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                string query = @"
                    SELECT library_id, library_name, library_address 
                    FROM Libraries 
                    WHERE library_name LIKE @searchText + '%'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchText", txtLibrarySearch.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvLibraries.DataSource = dt;
                }
            }
        }

        private bool IsValidIsbn(string isbn)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Books WHERE isbn = @isbn";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private int GetCurrentStock(int libraryId, string isbn)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT quantity_available 
                    FROM LibraryCollections 
                    WHERE library_id = @libraryId AND isbn = @isbn";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@libraryId", libraryId);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    object result = cmd.ExecuteScalar();
                    return result == null ? 0 : Convert.ToInt32(result);
                }
            }
        }

        private void UpdateStock(int libraryId, string isbn, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = @"
                    IF EXISTS (
                        SELECT 1 FROM LibraryCollections 
                        WHERE library_id = @libraryId AND isbn = @isbn
                    )
                    BEGIN
                        UPDATE LibraryCollections 
                        SET quantity_available = @quantity
                        WHERE library_id = @libraryId AND isbn = @isbn
                    END
                    ELSE
                    BEGIN
                        INSERT INTO LibraryCollections (library_id, isbn, quantity_available)
                        VALUES (@libraryId, @isbn, @quantity)
                    END";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@libraryId", libraryId);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            int libraryId = Convert.ToInt32(dgvLibraries.CurrentRow.Cells["library_id"].Value);
            string isbn = txtIsbn.Text.Trim();
            int addQuantity = Convert.ToInt32(txtQuantity.Text);

            int currentStock = GetCurrentStock(libraryId, isbn);
            int newStock = currentStock + addQuantity;

            UpdateStock(libraryId, isbn, newStock);
            MessageBox.Show($"재고가 추가되었습니다. 현재 재고: {newStock}권", "알림");
        }

        private void btnDecreaseStock_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            int libraryId = Convert.ToInt32(dgvLibraries.CurrentRow.Cells["library_id"].Value);
            string isbn = txtIsbn.Text.Trim();
            int decreaseQuantity = Convert.ToInt32(txtQuantity.Text);

            int currentStock = GetCurrentStock(libraryId, isbn);
            int newStock = currentStock - decreaseQuantity;

            if (newStock < 0)
            {
                MessageBox.Show("재고가 0 미만입니다. 정확한 값을 입력해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newStock == 0)
            {
                DialogResult result = MessageBox.Show(
                    "재고가 0이 됩니다. 계속하시겠습니까?",
                    "확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No) return;
            }

            UpdateStock(libraryId, isbn, newStock);
            MessageBox.Show($"재고가 감소되었습니다. 현재 재고: {newStock}권", "알림");
        }

        private bool ValidateInputs()
        {
            if (dgvLibraries.CurrentRow == null)
            {
                MessageBox.Show("도서관을 선택해주세요.", "알림");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIsbn.Text))
            {
                MessageBox.Show("ISBN을 입력해주세요.", "알림");
                return false;
            }

            if (!IsValidIsbn(txtIsbn.Text.Trim()))
            {
                MessageBox.Show("존재하지 않는 ISBN입니다.", "알림");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("수량을 입력해주세요.", "알림");
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("올바른 수량을 입력해주세요.", "알림");
                return false;
            }

            return true;
        }

        private void lblCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
