using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class BookMapForm : Form
    {
        private string isbn;
        private int memberId;
        private int libraryCollectionId;

        public BookMapForm(string isbn, int memberId, int libraryCollectionId)
        {
            InitializeComponent();
            this.isbn = isbn;
            this.memberId = memberId;
            this.libraryCollectionId = libraryCollectionId;

            this.Shown += BookMapForm_Shown; // ✅ Shown 이벤트로 이동
        }

        private async void BookMapForm_Shown(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);
            LoadLibraryMap();
        }

        private void LoadLibraryMap()
        {
            string address = null;
            string libraryName = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    L.library_name, 
                    L.library_address
                FROM Libraries L
                JOIN LibraryCollections LC ON L.library_id = LC.library_id
                WHERE LC.library_collection_id = @libraryCollectionId;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@libraryCollectionId", libraryCollectionId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        libraryName = reader["library_name"].ToString();
                        address = reader["library_address"].ToString();
                    }
                }

                if (string.IsNullOrEmpty(libraryName))
                {
                    MessageBox.Show("도서관 정보를 찾을 수 없습니다.");
                    return;
                }

                // ✅ 지도와 검색결과가 같이 나오는 PC용 URL로 변경
                string searchKeyword = $"{libraryName} {address}";
                string searchUrl = $"https://map.naver.com/v5/search/{Uri.EscapeDataString(searchKeyword)}";

                webView21.CoreWebView2.Navigate(searchUrl);

                MessageBox.Show($"📍 {libraryName}\n주소: {address}", "도서 위치 정보");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"지도 정보를 불러오는 중 오류가 발생했습니다:\n{ex.Message}");
            }
        }
    }
}
