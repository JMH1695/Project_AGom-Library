using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class MapForm : Form
    {
        private List<string> isbnList;
        private int memberId;

        private const string NAVER_MAP_CLIENT_ID = "q0ji1uzqd2";

        public MapForm(List<string> isbnList, int memberId)
        {
            InitializeComponent();
            this.isbnList = isbnList ?? new List<string>();
            this.memberId = memberId;
            this.Load += MapForm_Load;
        }

        private async void MapForm_Load(object sender, EventArgs e)
        {
            LoadLibraryData();

            // WebView2 초기화
            await webView21.EnsureCoreWebView2Async(null);

            // 표 크기 자동 조정
            dataGridViewLibraries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewLibraries.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewLibraries.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // 행 더블클릭 시 지도 표시
            dataGridViewLibraries.CellDoubleClick += DataGridViewLibraries_CellDoubleClick;
        }

        private void LoadLibraryData()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    L.library_id,
                    L.library_name,
                    L.library_address,
                    L.latitude,
                    L.longitude,
                    ISNULL(SUM(LC.quantity_available), 0) AS quantity_available
                FROM Libraries L
                LEFT JOIN LibraryCollections LC ON L.library_id = LC.library_id
                AND LC.isbn IN (" + string.Join(",", isbnList.ConvertAll(x => "'" + x + "'")) + @")
                WHERE L.library_name NOT LIKE '%온라인 도서관%' 
                GROUP BY L.library_id, L.library_name, L.library_address, L.latitude, L.longitude
                ORDER BY L.library_name;";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewLibraries.DataSource = dt;
            }

            // 컬럼명 표시 설정
            if (dataGridViewLibraries.Columns.Contains("library_id"))
                dataGridViewLibraries.Columns["library_id"].Visible = false;
            if (dataGridViewLibraries.Columns.Contains("library_name"))
                dataGridViewLibraries.Columns["library_name"].HeaderText = "도서관 이름";
            if (dataGridViewLibraries.Columns.Contains("library_address"))
                dataGridViewLibraries.Columns["library_address"].HeaderText = "주소";
            if (dataGridViewLibraries.Columns.Contains("latitude"))
                dataGridViewLibraries.Columns["latitude"].HeaderText = "위도";
            if (dataGridViewLibraries.Columns.Contains("longitude"))
                dataGridViewLibraries.Columns["longitude"].HeaderText = "경도";
            if (dataGridViewLibraries.Columns.Contains("quantity_available"))
                dataGridViewLibraries.Columns["quantity_available"].HeaderText = "남은 도서 수";

            // 버튼 컬럼 추가
            if (!dataGridViewLibraries.Columns.Contains("BorrowButton"))
            {
                DataGridViewButtonColumn borrowButton = new DataGridViewButtonColumn
                {
                    HeaderText = "대여예약",
                    Name = "BorrowButton",
                    Text = "대여예약",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewLibraries.Columns.Add(borrowButton);
            }

            if (!dataGridViewLibraries.Columns.Contains("MapButton"))
            {
                DataGridViewButtonColumn mapButton = new DataGridViewButtonColumn
                {
                    HeaderText = "위치보기",
                    Name = "MapButton",
                    Text = "위치보기",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewLibraries.Columns.Add(mapButton);
            }

            // 이벤트 중복 방지
            dataGridViewLibraries.CellClick -= DataGridViewLibraries_CellClick;
            dataGridViewLibraries.CellClick += DataGridViewLibraries_CellClick;
        }

        private void DataGridViewLibraries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewLibraries.Rows[e.RowIndex];

            // 📘 대여 버튼
            if (dataGridViewLibraries.Columns[e.ColumnIndex].Name == "BorrowButton")
            {
                int libraryId = Convert.ToInt32(row.Cells["library_id"].Value);
                string libraryName = row.Cells["library_name"].Value.ToString();
                int available = Convert.ToInt32(row.Cells["quantity_available"].Value);

                if (available <= 0)
                {
                    MessageBox.Show("해당 도서관에 대여 가능한 책이 없습니다.");
                    return;
                }

                DialogResult result = MessageBox.Show($"[{libraryName}]에서 책을 대여하시겠습니까?", "확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    BorrowBook(libraryId);
            }
            // 📍 지도 보기
            else if (dataGridViewLibraries.Columns[e.ColumnIndex].Name == "MapButton")
            {
                string name = row.Cells["library_name"].Value.ToString();
                string address = row.Cells["library_address"].Value.ToString();

                double lat = 0, lng = 0;
                if (row.Cells["latitude"].Value != DBNull.Value)
                    double.TryParse(row.Cells["latitude"].Value.ToString(), out lat);
                if (row.Cells["longitude"].Value != DBNull.Value)
                    double.TryParse(row.Cells["longitude"].Value.ToString(), out lng);

                ShowMap(lat, lng, name, address);
            }
        }

        private void BorrowBook(int libraryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // library_collection_id 조회
                    string findQuery = @"
                        SELECT TOP 1 library_collection_id
                        FROM LibraryCollections
                        WHERE library_id = @libraryId AND isbn = @isbn";
                    SqlCommand findCmd = new SqlCommand(findQuery, conn);
                    findCmd.Parameters.AddWithValue("@libraryId", libraryId);
                    findCmd.Parameters.AddWithValue("@isbn", isbnList[0]);
                    object result = findCmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("해당 도서가 도서관에 없습니다.");
                        return;
                    }

                    int collectionId = Convert.ToInt32(result);

                    // Borrows 삽입
                    string insertBorrow = @"
                        INSERT INTO Borrows (member_id, library_collection_id, borrow_date, due_date, return_status)
                        VALUES (@memberId, @collectionId, GETDATE(), DATEADD(DAY, 7, GETDATE()), '대여 중')";
                    SqlCommand insCmd = new SqlCommand(insertBorrow, conn);
                    insCmd.Parameters.AddWithValue("@memberId", memberId);
                    insCmd.Parameters.AddWithValue("@collectionId", collectionId);
                    insCmd.ExecuteNonQuery();

                    // 수량 감소
                    string updateQty = @"
                        UPDATE LibraryCollections
                        SET quantity_available = quantity_available - 1
                        WHERE library_collection_id = @collectionId";
                    SqlCommand updCmd = new SqlCommand(updateQty, conn);
                    updCmd.Parameters.AddWithValue("@collectionId", collectionId);
                    updCmd.ExecuteNonQuery();

                    MessageBox.Show("도서 대여가 완료되었습니다!");
                    this.Close(); // ✅ 대여 완료 시 폼 닫기
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"대여 중 오류 발생: {ex.Message}");
            }
        }

        private void DataGridViewLibraries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewLibraries.Rows[e.RowIndex];
            if (row.Cells["latitude"].Value == DBNull.Value || row.Cells["longitude"].Value == DBNull.Value)
            {
                MessageBox.Show("해당 도서관의 위치 정보가 없습니다.");
                return;
            }

            double lat = Convert.ToDouble(row.Cells["latitude"].Value);
            double lng = Convert.ToDouble(row.Cells["longitude"].Value);
            string name = row.Cells["library_name"].Value.ToString();

            ShowMap(lat, lng, name);
        }

        private async void ShowMap(double lat, double lng, string name, string address = "")
        {
            await webView21.EnsureCoreWebView2Async(null);

            if (!string.IsNullOrWhiteSpace(address))
            {
                string searchUrl = $"https://map.naver.com/v5/search/{Uri.EscapeDataString(address)}";
                webView21.CoreWebView2.Navigate(searchUrl);
            }
            else
            {
                string coordUrl = $"https://map.naver.com/v5/?c={lng},{lat},15,0,0,0,dh";
                webView21.CoreWebView2.Navigate(coordUrl);
            }
        }
    }
}
