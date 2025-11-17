using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace AGomProject
{
    public partial class MyPageForm : Form
    {
        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)

        private int memberId;
        private List<BorrowedBook> borrowedBooks; // 대여 중인 책 목록
        private string defaultImagePath = "Images/default_cover.png"; // 기본 이미지 경로

        // 페이지 관련 변수
        private int pageNumber = 1;
        private const int booksPerPage = 3;

        public MyPageForm(int memberId)
        {
            InitializeComponent();
            this.memberId = memberId;
            this.Shown += MyPageForm_Shown;
            color_Input();
        }

        private void MyPageForm_Shown(object sender, EventArgs e)
        {
            LoadMemberInfo();
            borrowedBooks = LoadBorrowedBooks(pageNumber, booksPerPage);

            if (borrowedBooks.Any())
                DisplayPage(pageNumber);
            else
                MessageBox.Show("대여 중인 책이 없습니다.");
        }

        // 대여 중인 책 정보 모델 클래스
        public class BorrowedBook
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string CoverImage { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public int BorrowId { get; set; }
            public int LibraryCollectionId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }

        }
        // 📚 대여 중인 책 데이터 로드 (e북 + 실물 모두)
        private List<BorrowedBook> LoadBorrowedBooks(int currentPage, int booksPerPage)
        {
            var borrowedBooks = new List<BorrowedBook>();

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    string query = @"
                    SELECT 
                        br.borrow_id,
                        lc.library_collection_id,
                        bc.isbn,
                        bc.title,
                        bc.author,
                        bc.cover_image,
                        bc.is_ebook,
                        ct.category_name,
                        bd.description,
                        br.borrow_date,          -- 🆕 대여일
                        br.due_date              -- 🆕 반납예정일
                    FROM Borrows br
                    JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                    JOIN Books bc ON lc.isbn = bc.isbn
                    LEFT JOIN Categories ct ON bc.category_id = ct.category_id
                    LEFT JOIN BookDetails bd ON bc.isbn = bd.isbn
                    WHERE br.member_id = @memberId 
                              AND (br.return_status LIKE N'%대여%')
                    ORDER BY bc.title
                    OFFSET @Offset ROWS FETCH NEXT @BooksPerPage ROWS ONLY;";


                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@memberId", memberId);
                    command.Parameters.AddWithValue("@Offset", (currentPage - 1) * booksPerPage);
                    command.Parameters.AddWithValue("@BooksPerPage", booksPerPage);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            borrowedBooks.Add(new BorrowedBook
                            {
                                ISBN = reader["isbn"].ToString(),
                                Title = reader["title"].ToString(),
                                Author = reader["author"].ToString(),
                                CoverImage = reader["cover_image"].ToString(),
                                CategoryName = reader["category_name"].ToString(),
                                Description = reader["description"].ToString(),
                                BorrowId = Convert.ToInt32(reader["borrow_id"]),
                                LibraryCollectionId = Convert.ToInt32(reader["library_collection_id"]),
                                BorrowDate = reader["borrow_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["borrow_date"]),
                                DueDate = reader["due_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["due_date"]),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"대여 중인 책 데이터를 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }

            return borrowedBooks;
        }



        // 👤 회원 정보 로드
        private void LoadMemberInfo()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 통합된 연결문자열 사용
                {
                    string query = "SELECT first_name FROM Members WHERE member_id = @memberId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@memberId", memberId);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                        lbMyPageWelcome.Text = $"{result}님의 마이페이지";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"회원 정보를 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        // 페이지 표시
        // 페이지 표시
        private void DisplayPage(int pageNumber)
        {
            // 1️⃣ 완전 초기화
            ClearPage();

            // 2️⃣ 데이터 로드
            borrowedBooks = LoadBorrowedBooks(pageNumber, booksPerPage);
            if (!borrowedBooks.Any())
            {
                lbMyPagePageCount.Text = "0/0";
                MessageBox.Show("대여 중인 책이 없습니다.");
                return;
            }

            // ✅ labels 배열 정의
            var labels = new[] {
        new { Title = lbMyPageBookName1, Author = lbMyPageBookAuthor1, Genre = lbMyPageBookGenr1, Text = lbMyPageBookText1, Image = pbMyPageBookImage1, RentButton = pbMyPageRentButton1, RentLabel = lbMyPageRentButton1, RCButton = pbMyPageRCButton1 },
        new { Title = lbMyPageBookName2, Author = lbMyPageBookAuthor2, Genre = lbMyPageBookGenr2, Text = lbMyPageBookText2, Image = pbMyPageBookImage2, RentButton = pbMyPageRentButton2, RentLabel = lbMyPageRentButton2, RCButton = pbMyPageRCButton2 },
        new { Title = lbMyPageBookName3, Author = lbMyPageBookAuthor3, Genre = lbMyPageBookGenr3, Text = lbMyPageBookText3, Image = pbMyPageBookImage3, RentButton = pbMyPageRentButton3, RentLabel = lbMyPageRentButton3, RCButton = pbMyPageRCButton3 }
    };

            // ✅ 날짜 라벨 배열 정의
            var dateLabels = new[]
            {
        new { Start = lbDateS1, End = lbDateE1 },
        new { Start = lbDateS2, End = lbDateE2 },
        new { Start = lbDateS3, End = lbDateE3 }
    };

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                for (int i = 0; i < labels.Length; i++)
                {
                    var cancelLabel = GetCancelLabelByIndex(i + 1);

                    if (i < borrowedBooks.Count)
                    {
                        // ✅ 정상 데이터 출력
                        var book = borrowedBooks[i];
                        labels[i].Title.Text = book.Title;
                        labels[i].Author.Text = book.Author;
                        labels[i].Genre.Text = book.CategoryName;
                        labels[i].Text.Text = book.Description;
                        labels[i].Image.ImageLocation = File.Exists(book.CoverImage) ? book.CoverImage : defaultImagePath;

                        // 🆕 대여일 / 반납예정일 표시
                        dateLabels[i].Start.Text = book.BorrowDate == DateTime.MinValue
                            ? "-"
                            : book.BorrowDate.ToString("yyyy-MM-dd");

                        dateLabels[i].End.Text = book.DueDate == DateTime.MinValue
                            ? "-"
                            : book.DueDate.ToString("yyyy-MM-dd");

                        // ✅ E북 여부 확인
                        string query = "SELECT is_ebook FROM Books WHERE isbn = @isbn";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@isbn", book.ISBN);
                        object result = cmd.ExecuteScalar();
                        bool isEbook = result != null && result != DBNull.Value && Convert.ToBoolean(result);

                        labels[i].RentLabel.Text = isEbook ? "E북읽기" : "지도보기";
                        cancelLabel.Text = isEbook ? "대여취소" : "예약취소";

                        // 활성화
                        labels[i].RentButton.Enabled = true;
                        labels[i].RCButton.Enabled = true;
                        cancelLabel.Enabled = true;

                        // ✅ 태그 연결
                        labels[i].RentButton.Tag = new { ISBN = book.ISBN, IsEbook = isEbook, Row = i, LibraryCollectionId = book.LibraryCollectionId };
                        labels[i].RCButton.Tag = new { ISBN = book.ISBN, BorrowId = book.BorrowId, Row = i, LibraryCollectionId = book.LibraryCollectionId };

                        // ✅ 이벤트 중복 제거 후 재연결
                        labels[i].RentButton.Click -= pbMyPageRentButton_Click;
                        labels[i].RentButton.Click += pbMyPageRentButton_Click;

                        labels[i].RCButton.Click -= pbMyPageRCButton_Click;
                        labels[i].RCButton.Click += pbMyPageRCButton_Click;
                    }
                    else
                    {
                        // 비어있는 슬롯 처리
                        labels[i].Title.Text = "";
                        labels[i].Author.Text = "";
                        labels[i].Genre.Text = "";
                        labels[i].Text.Text = "";
                        labels[i].Image.ImageLocation = null;
                        labels[i].RentLabel.Text = "―";
                        cancelLabel.Text = "―";

                        // 🆕 날짜 초기화
                        dateLabels[i].Start.Text = "-";
                        dateLabels[i].End.Text = "-";

                        labels[i].RentButton.Enabled = false;
                        labels[i].RCButton.Enabled = false;
                        cancelLabel.Enabled = false;

                        labels[i].RentButton.Tag = null;
                        labels[i].RCButton.Tag = null;

                        labels[i].RentButton.Click -= pbMyPageRentButton_Click;
                        labels[i].RCButton.Click -= pbMyPageRCButton_Click;
                    }
                }
            }

            // 페이지 수 갱신
            int totalCount = GetTotalBorrowCount();
            int totalPages = (int)Math.Ceiling(totalCount / (double)booksPerPage);
            lbMyPagePageCount.Text = $"{pageNumber}/{(totalPages == 0 ? 1 : totalPages)}";
        }



        private Label GetCancelLabelByIndex(int index)
        {
            switch (index)
            {
                case 1: return label1;
                case 2: return label2;
                case 3: return label3;
                default: return null;
            }
        }

        private int GetTotalBorrowCount()
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT COUNT(*)
                FROM Borrows br
                JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                JOIN Books bc ON lc.isbn = bc.isbn
                WHERE br.member_id = @memberId
                  AND (br.return_status LIKE N'%대여%')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@memberId", memberId);
                    count = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"도서 개수를 불러오는 중 오류: {ex.Message}");
            }
            return count;
        }


        // UI 초기화
        private void ClearPage()
        {
            // 텍스트 및 이미지 초기화
            lbMyPageBookName1.Text = lbMyPageBookAuthor1.Text = lbMyPageBookGenr1.Text = lbMyPageBookText1.Text = "";
            lbMyPageBookName2.Text = lbMyPageBookAuthor2.Text = lbMyPageBookGenr2.Text = lbMyPageBookText2.Text = "";
            lbMyPageBookName3.Text = lbMyPageBookAuthor3.Text = lbMyPageBookGenr3.Text = lbMyPageBookText3.Text = "";

            pbMyPageBookImage1.ImageLocation = pbMyPageBookImage2.ImageLocation = pbMyPageBookImage3.ImageLocation = null;

            // 버튼 텍스트 초기화
            lbMyPageRentButton1.Text = lbMyPageRentButton2.Text = lbMyPageRentButton3.Text = "―";
            label3.Text = label2.Text = label1.Text = "―";

            // 버튼 완전 비활성화
            pbMyPageRentButton1.Enabled = pbMyPageRentButton2.Enabled = pbMyPageRentButton3.Enabled = false;
            pbMyPageRCButton1.Enabled = pbMyPageRCButton2.Enabled = pbMyPageRCButton3.Enabled = false;

            // 이전 이벤트 핸들러 완전 제거
            //pbMyPageRentButton1.Click -= pbMyPageRentButton_Click;
            //pbMyPageRentButton2.Click -= pbMyPageRentButton_Click;
            //pbMyPageRentButton3.Click -= pbMyPageRentButton_Click;

            pbMyPageRCButton1.Click -= pbMyPageRCButton_Click;
            pbMyPageRCButton2.Click -= pbMyPageRCButton_Click;
            pbMyPageRCButton3.Click -= pbMyPageRCButton_Click;

            // 태그 제거
            pbMyPageRentButton1.Tag = pbMyPageRentButton2.Tag = pbMyPageRentButton3.Tag = null;
            pbMyPageRCButton1.Tag = pbMyPageRCButton2.Tag = pbMyPageRCButton3.Tag = null;
        }




        // 페이지 이동 버튼
        private void pbMyPageSkipRight_Click(object sender, EventArgs e)
        {
            int totalCount = GetTotalBorrowCount(); // 전체 개수
            int totalPages = (int)Math.Ceiling(totalCount / (double)booksPerPage);

            if (pageNumber < totalPages)
            {
                pageNumber++;
                DisplayPage(pageNumber);
            }
        }

        private void pbMyPageSkipLeft_Click(object sender, EventArgs e)
        {
            if (pageNumber > 1)
            {
                pageNumber--;
                DisplayPage(pageNumber);
            }
        }


        // 대여 버튼 클릭
        private void pbMyPageRentButton_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clickedButton)
            {
                if (clickedButton.Tag == null)
                {
                    MessageBox.Show("선택된 도서가 없습니다.");
                    return;
                }

                var tag = clickedButton.Tag;
                string isbn = (string)tag.GetType().GetProperty("ISBN").GetValue(tag, null);
                bool isEbook = (bool)tag.GetType().GetProperty("IsEbook").GetValue(tag, null);

                try
                {
                    // ✅ Borrows 테이블에 실제 대여 중인지 재확인
                    using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();
                        string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM Borrows br
                    JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                    WHERE lc.isbn = @isbn AND br.member_id = @memberId";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@isbn", isbn);
                        checkCmd.Parameters.AddWithValue("@memberId", memberId);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("해당 도서의 대여 정보를 찾을 수 없습니다.");
                            return;
                        }
                    }

                    // ✅ E북 or 지도보기 실행
                    if (isEbook)
                    {
                        var ebookViewerForm = new EbookViewerForm(isbn);
                        ebookViewerForm.ShowDialog();
                    }
                    else
                    {
                        int libraryCollectionId = (int)tag.GetType().GetProperty("LibraryCollectionId").GetValue(tag, null);
                        var bookMapForm = new BookMapForm(isbn, memberId, libraryCollectionId);
                        bookMapForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"도서 열람 중 오류 발생: {ex.Message}");
                }
            }
        }


        private void pbMyPageBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbMyPageInfoEdit_Click(object sender, EventArgs e)
        {
            MemberInfoEditForm memberInfoEditForm = new MemberInfoEditForm(memberId);
            memberInfoEditForm.ShowDialog();
        }

        private void color_Input()
        {
            //Yellow
            lbMyPageBookName1.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookName3.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookAuthor1.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookAuthor3.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookGenr1.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookGenr3.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookText1.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageBookText3.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPagePageCount.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPagePageCount.BackColor = Color.FromArgb(254, 255, 210);

            //Green
            pbMyPageLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText2.BackColor = Color.FromArgb(197, 249, 214);
            lbMyPageWelcome.BackColor = Color.FromArgb(197, 249, 214);
            pbMyPageInfoEdit.BackColor = Color.FromArgb(197, 249, 214);

            //Yellow Button
            lbMyPageInfoEdit.BackColor = Color.FromArgb(217, 217, 16);
            pbMyPageRentButton1.BackColor = Color.FromArgb(254, 255, 210);
            pbMyPageRentButton3.BackColor = Color.FromArgb(254, 255, 210);
            lbMyPageRentButton1.BackColor = Color.FromArgb(217, 217, 16);
            lbMyPageRentButton2.BackColor = Color.FromArgb(217, 217, 16);
            lbMyPageRentButton3.BackColor = Color.FromArgb(217, 217, 16);

            //Green Button
            lbMyPageBackButton.BackColor = Color.FromArgb(65, 158, 59);
            pbMyPageRCButton1.BackColor = Color.FromArgb(254, 255, 210);
            pbMyPageRCButton3.BackColor = Color.FromArgb(254, 255, 210);
            label3.BackColor = Color.FromArgb(65, 158, 59);
            label2.BackColor = Color.FromArgb(65, 158, 59);
            label1.BackColor = Color.FromArgb(65, 158, 59);

            lbDNS1.BackColor = Color.FromArgb(254, 255, 210);
            lbDateS1.BackColor = Color.FromArgb(254, 255, 210);
            lbDNE1.BackColor = Color.FromArgb(254, 255, 210);
            lbDateE1.BackColor = Color.FromArgb(254, 255, 210);

            lbDNS3.BackColor = Color.FromArgb(254, 255, 210);
            lbDateS3.BackColor = Color.FromArgb(254, 255, 210);
            lbDNE3.BackColor = Color.FromArgb(254, 255, 210);
            lbDateE3.BackColor = Color.FromArgb(254, 255, 210);

        }
        private void pbMyPageRCButton_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clickedButton && clickedButton.Tag != null)
            {
                dynamic tag = clickedButton.Tag;
                string selectedIsbn = tag.ISBN;

                bool isEbook = false;
                string actionText = "";

                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();

                        // E북 여부 확인
                        string query = "SELECT is_ebook FROM Books WHERE isbn = @isbn";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@isbn", selectedIsbn);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                            isEbook = Convert.ToBoolean(result);

                        actionText = isEbook ? "대여취소" : "예약취소";

                        DialogResult confirm = MessageBox.Show($"{actionText}를 진행하시겠습니까?",
                            "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirm == DialogResult.Yes)
                        {
                            string deleteQuery = @"
        DELETE FROM Borrows
        WHERE borrow_id = @borrowId AND member_id = @memberId";

                            SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                            deleteCmd.Parameters.AddWithValue("@borrowId", tag.BorrowId);
                            deleteCmd.Parameters.AddWithValue("@memberId", memberId);

                            int rows = deleteCmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show($"{actionText}가 완료되었습니다.");
                                borrowedBooks = LoadBorrowedBooks(pageNumber, booksPerPage);
                                DisplayPage(pageNumber);  // 전체 다시 표시
                            }
                            else
                            {
                                MessageBox.Show("취소할 도서를 찾을 수 없습니다.");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"취소 중 오류 발생: {ex.Message}");
                }
            }
        }


        private void ClearBookSlot(int index)
        {
            Label[] titleLabels = { lbMyPageBookName1, lbMyPageBookName2, lbMyPageBookName3 };
            Label[] authorLabels = { lbMyPageBookAuthor1, lbMyPageBookAuthor2, lbMyPageBookAuthor3 };
            Label[] genreLabels = { lbMyPageBookGenr1, lbMyPageBookGenr2, lbMyPageBookGenr3 };
            Label[] textLabels = { lbMyPageBookText1, lbMyPageBookText2, lbMyPageBookText3 };
            PictureBox[] imageBoxes = { pbMyPageBookImage1, pbMyPageBookImage2, pbMyPageBookImage3 };
            Label[] rentLabels = { lbMyPageRentButton1, lbMyPageRentButton2, lbMyPageRentButton3 };
            Label[] cancelLabels = { label3, label2, label1 };
            PictureBox[] rentButtons = { pbMyPageRentButton1, pbMyPageRentButton2, pbMyPageRentButton3 };
            PictureBox[] rcButtons = { pbMyPageRCButton1, pbMyPageRCButton2, pbMyPageRCButton3 };

            int i = index; // 0, 1, 2

            titleLabels[i].Text = "";
            authorLabels[i].Text = "";
            genreLabels[i].Text = "";
            textLabels[i].Text = "";
            imageBoxes[i].ImageLocation = null;
            rentLabels[i].Text = "―";
            cancelLabels[i].Text = "―";

            rentButtons[i].Enabled = false;
            rcButtons[i].Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pbMyPageRCButton_Click(pbMyPageRCButton1, e);
        }
        private void label2_Click(object sender, EventArgs e)
        {
            pbMyPageRCButton_Click(pbMyPageRCButton2, e);
        }
        private void label3_Click(object sender, EventArgs e)
        {
            pbMyPageRCButton_Click(pbMyPageRCButton3, e);
        }

        private void lbMyPageBookName1_Click(object sender, EventArgs e)
        {

        }

        private void lbDNS1_Click(object sender, EventArgs e)
        {

        }
    }
}
