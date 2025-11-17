using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class RentalReservationForm : Form
    {
        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)
        private int currentUserId;
        private List<string> isbnList;

        public RentalReservationForm(List<string> isbnList, int memberId)
        {
            InitializeComponent();
            color_Input();
            this.isbnList = isbnList;
            this.currentUserId = memberId;

            lblRentOrReser.Text = "예약목록";
            LoadBooks(false);
        }

        private void RentalReservationForm_Load(object sender, EventArgs e)
        {
            CreateLibrariesTable();
        }

        private void PbBooks_Click(object sender, EventArgs e)
        {
            LoadBooks(false);
            lblRentOrReser.Text = "예약목록";
        }

        private void PbEbooks_Click(object sender, EventArgs e)
        {
            LoadBooks(true);
            lblRentOrReser.Text = "대여목록";
        }

        private void pbAllBorrow_Click(object sender, EventArgs e)
        {
            List<string> ebookList = new List<string>();
            List<string> physicalBookList = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    string query = @"
                        SELECT b.isbn, b.is_ebook 
                        FROM Cart c 
                        JOIN Books b ON c.isbn = b.isbn 
                        WHERE c.member_id = @userId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", currentUserId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string bookIsbn = reader["isbn"].ToString();
                        bool isEbook = reader["is_ebook"] != DBNull.Value && Convert.ToBoolean(reader["is_ebook"]);

                        if (isEbook)
                            ebookList.Add(bookIsbn);
                        else
                            physicalBookList.Add(bookIsbn);
                    }
                }

                foreach (var ebookIsbn in ebookList)
                    BorrowBook(ebookIsbn);

                if (physicalBookList.Count > 0)
                {
                    MapForm mapForm = new MapForm(physicalBookList, currentUserId);
                    mapForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("대여할 일반 도서가 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}");
            }
        }

        private void pbAllCancel_Click(object sender, EventArgs e)
        {
            if (currentUserId == 0)
            {
                MessageBox.Show("로그인 후 전체 대여 취소를 진행해 주세요.");
                return;
            }

            DialogResult result = MessageBox.Show("모든 대여를 취소하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                    {
                        string query = "DELETE FROM Cart WHERE member_id = @userId";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@userId", currentUserId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("모든 대여가 취소되었습니다.");
                    LoadBooks(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"대여 취소 중 오류 발생: {ex.Message}");
                }
            }
        }

        private void LoadBooks(bool isEbook)
        {
            pRentalLists.Controls.Clear();
            pRentalLists.AutoScroll = true;

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
            {
                string query = @"
                    SELECT b.title, b.author, b.cover_image, b.isbn 
                    FROM Cart c 
                    JOIN Books b ON c.isbn = b.isbn
                    WHERE c.member_id = @userId AND b.is_ebook = @isEbook";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                cmd.Parameters.AddWithValue("@isEbook", isEbook ? 1 : 0);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int yOffset = 10;

                while (reader.Read())
                {
                    Panel bookPanel = new Panel
                    {
                        Size = new Size(980, 213),
                        Location = new Point(5, yOffset),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    PictureBox pbBookImage = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(122, 179),
                        Location = new Point(10, 15)
                    };

                    try
                    {
                        pbBookImage.Image = Image.FromFile(reader["cover_image"].ToString());
                    }
                    catch
                    {
                        pbBookImage.Image = null;
                    }

                    Label lblTitleN = new Label { Text = "도서 제목: ", Location = new Point(267, 30), AutoSize = true };
                    Label lblTitleR = new Label { Text = reader["title"].ToString(), Location = new Point(382, 30), AutoSize = true };
                    Label lblAuthorN = new Label { Text = "저자: ", Location = new Point(267, 70), AutoSize = true };
                    Label lblAuthor = new Label { Text = reader["author"].ToString(), Location = new Point(382, 70), AutoSize = true };
                    Label lblRentalN = new Label { Text = "대여 날짜 : ", Location = new Point(267, 110), AutoSize = true };
                    DateTimePicker dtpBorrowDate = new DateTimePicker { Value = DateTime.Now, Location = new Point(382, 110) };
                    Label lblReturnN = new Label { Text = "반납 날짜 : ", Location = new Point(267, 150), AutoSize = true };
                    DateTimePicker dtpReturnDate = new DateTimePicker { Value = DateTime.Now.AddDays(7), Location = new Point(382, 150) };

                    string bookIsbn = reader["isbn"].ToString();

                    // 📗 대여 버튼 (패널)
                    Panel pbBorrow = new Panel
                    {
                        BackColor = Color.LightGreen,
                        Size = new Size(100, 40),
                        Location = new Point(690, 150),
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand
                    };

                    // 대여 버튼 텍스트 라벨
                    Label lblBorrowText = new Label
                    {
                        Text = "📗 대여",
                        ForeColor = Color.White,
                        Font = new Font("맑은 고딕", 12, FontStyle.Bold),
                        BackColor = Color.Transparent,
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    pbBorrow.Controls.Add(lblBorrowText);

                    // ❌ 취소 버튼 (패널)
                    Panel pbCancel = new Panel
                    {
                        BackColor = Color.OrangeRed,
                        Size = new Size(100, 40),
                        Location = new Point(820, 150),
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand
                    };

                    // 취소 버튼 텍스트 라벨
                    Label lblCancelText = new Label
                    {
                        Text = "❌ 취소",
                        ForeColor = Color.White,
                        Font = new Font("맑은 고딕", 12, FontStyle.Bold),
                        BackColor = Color.Transparent,
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    pbCancel.Controls.Add(lblCancelText);

                    // 🔗 클릭 이벤트 (한 번씩만!)
                    pbBorrow.Click += (s, e) => BorrowBook(bookIsbn);
                    lblBorrowText.Click += (s, e) => BorrowBook(bookIsbn);        // 라벨 눌러도 동작
                    pbCancel.Click += (s, e) => CancelReservation(bookIsbn);
                    lblCancelText.Click += (s, e) => CancelReservation(bookIsbn); // 라벨 눌러도 동작

                    // 컨트롤 추가
                    bookPanel.Controls.AddRange(new Control[]
                    {
                        pbBookImage, lblTitleN, lblTitleR, lblAuthorN, lblAuthor,
                        lblRentalN, dtpBorrowDate, lblReturnN, dtpReturnDate,
                        pbBorrow, pbCancel
                    });

                    pRentalLists.Controls.Add(bookPanel);

                    yOffset += 220;
                }
            }
        }

        private void BorrowBook(string isbn)
        {
            if (currentUserId == 0)
            {
                MessageBox.Show("로그인 후 대여를 진행해 주세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    conn.Open();

                    string query = "SELECT is_ebook FROM Books WHERE isbn = @isbn";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@isbn", isbn);

                    object ebookValue = cmd.ExecuteScalar();
                    bool isEbook = false;

                    if (ebookValue != DBNull.Value)
                    {
                        if (ebookValue is bool) isEbook = (bool)ebookValue;
                        else if (ebookValue is int) isEbook = ((int)ebookValue) == 1;
                        else if (ebookValue is byte) isEbook = ((byte)ebookValue) == 1;
                        else isEbook = ebookValue.ToString() == "1" || ebookValue.ToString().ToLower() == "true";
                    }

                    if (isEbook)
                    {
                        int libraryId = 0;
                        int libraryCollectionId = 0;

                        string findLibraryQuery = "SELECT library_id FROM Libraries WHERE library_name = '온라인 도서관'";
                        SqlCommand findLibraryCmd = new SqlCommand(findLibraryQuery, conn);
                        object libIdObj = findLibraryCmd.ExecuteScalar();

                        if (libIdObj == null)
                        {
                            string insertLibraryQuery = "INSERT INTO Libraries (library_name, library_address) VALUES ('온라인 도서관', 'E-BOOK 전용')";
                            SqlCommand insertLibraryCmd = new SqlCommand(insertLibraryQuery, conn);
                            insertLibraryCmd.ExecuteNonQuery();

                            findLibraryCmd = new SqlCommand(findLibraryQuery, conn);
                            libIdObj = findLibraryCmd.ExecuteScalar();
                        }
                        libraryId = Convert.ToInt32(libIdObj);

                        string findCollectionQuery = "SELECT library_collection_id FROM LibraryCollections WHERE isbn = @isbn AND library_id = @libId";
                        SqlCommand findCollectionCmd = new SqlCommand(findCollectionQuery, conn);
                        findCollectionCmd.Parameters.AddWithValue("@isbn", isbn);
                        findCollectionCmd.Parameters.AddWithValue("@libId", libraryId);
                        object collectionObj = findCollectionCmd.ExecuteScalar();

                        if (collectionObj == null)
                        {
                            string insertCollectionQuery = @"
                                INSERT INTO LibraryCollections (library_id, isbn, quantity_available)
                                VALUES (@libId, @isbn, 9999)";
                            SqlCommand insertCollectionCmd = new SqlCommand(insertCollectionQuery, conn);
                            insertCollectionCmd.Parameters.AddWithValue("@libId", libraryId);
                            insertCollectionCmd.Parameters.AddWithValue("@isbn", isbn);
                            insertCollectionCmd.ExecuteNonQuery();

                            collectionObj = findCollectionCmd.ExecuteScalar();
                        }
                        libraryCollectionId = Convert.ToInt32(collectionObj);

                        string checkQuery = @"
                            SELECT COUNT(*) 
                            FROM Borrows 
                            WHERE member_id = @memberId 
                            AND library_collection_id = @collectionId 
                            AND return_status = '대여 중'";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@memberId", currentUserId);
                        checkCmd.Parameters.AddWithValue("@collectionId", libraryCollectionId);
                        int alreadyBorrowed = (int)checkCmd.ExecuteScalar();

                        if (alreadyBorrowed > 0)
                        {
                            MessageBox.Show("이미 대여 중인 전자책입니다.");
                            return;
                        }

                        string insertBorrowQuery = @"
                            INSERT INTO Borrows (member_id, library_collection_id, borrow_date, due_date, return_status)
                            VALUES (@memberId, @collectionId, @borrowDate, @dueDate, '대여 중')";
                        SqlCommand insertBorrowCmd = new SqlCommand(insertBorrowQuery, conn);
                        insertBorrowCmd.Parameters.AddWithValue("@memberId", currentUserId);
                        insertBorrowCmd.Parameters.AddWithValue("@collectionId", libraryCollectionId);
                        insertBorrowCmd.Parameters.AddWithValue("@borrowDate", DateTime.Now);
                        insertBorrowCmd.Parameters.AddWithValue("@dueDate", DateTime.Now.AddDays(7));
                        insertBorrowCmd.ExecuteNonQuery();

                        MessageBox.Show($"전자책 대여가 완료되었습니다: {isbn}");
                    }
                    else
                    {
                        MapForm mapForm = new MapForm(new List<string> { isbn }, currentUserId);
                        mapForm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"대여 중 오류 발생: {ex.Message}");
            }
        }

        private void CancelReservation(string isbn)
        {
            if (currentUserId == 0)
            {
                MessageBox.Show("로그인 후 예약 취소를 진행해 주세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    string query = "DELETE FROM Cart WHERE member_id = @userId AND isbn = @isbn";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", currentUserId);
                    cmd.Parameters.AddWithValue("@isbn", isbn);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"예약 취소되었습니다: {isbn}");
                LoadBooks(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"예약 취소 중 오류 발생: {ex.Message}");
            }
        }

        private void CreateLibrariesTable()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Libraries')
                        BEGIN
                            CREATE TABLE Libraries (
                                library_id INT PRIMARY KEY IDENTITY(1,1),
                                library_name NVARCHAR(255) NOT NULL,
                                library_address NVARCHAR(255) NOT NULL,
                                latitude DECIMAL(9,6),
                                longitude DECIMAL(9,6)
                            )
                        END";

                    string insertDataQuery = @"
                        IF NOT EXISTS (SELECT * FROM Libraries)
                        BEGIN
                            INSERT INTO Libraries (library_name, library_address, latitude, longitude)
                            VALUES 
                                ('E-Book Library', '온라인 전자도서관', NULL, NULL),
                                ('서울 중앙 도서관', '서울특별시 중구', 37.5665, 126.9780),
                                ('부산 도서관', '부산광역시 해운대구', 35.1634, 129.1600),
                                ('대구 도서관', '대구광역시 중구', 35.8714, 128.6010),
                                ('인천 도서관', '인천광역시 남동구', 37.4563, 126.7052),
                                ('광주 도서관', '광주광역시 북구', 35.1595, 126.8526),
                                ('대전 도서관', '대전광역시 서구', 36.3504, 127.3845),
                                ('울산 도서관', '울산광역시 남구', 35.5383, 129.3114),
                                ('경기도 도서관', '경기도 수원시', 37.2632, 127.0286),
                                ('충청북도 도서관', '충청북도 청주시', 36.6354, 127.4890),
                                ('전라남도 도서관', '전라남도 목포시', 34.8102, 126.3920)
                        END";

                    conn.Open();
                    new SqlCommand(createTableQuery, conn).ExecuteNonQuery();
                    new SqlCommand(insertDataQuery, conn).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        private void color_Input()
        {
            pbMainMyPage.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText2.BackColor = Color.FromArgb(197, 249, 214);
            lbMyPageBackButton.BackColor = Color.FromArgb(65, 158, 59);
            pbBookListLogo.BackColor = Color.FromArgb(197, 249, 214);
        }

        private void pbMainMyPage_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ 전역 DB 연결
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT admin FROM Members WHERE member_id = @MemberId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", currentUserId);
                        bool isAdmin = (bool)cmd.ExecuteScalar();

                        if (isAdmin)
                        {
                            AdminForm adminForm = new AdminForm(currentUserId);
                            adminForm.ShowDialog();
                        }
                        else
                        {
                            MyPageForm myPageForm = new MyPageForm(currentUserId);
                            myPageForm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류가 발생했습니다: {ex.Message}");
            }
        }

        private void pbMyPageBackButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbBookListLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
