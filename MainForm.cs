using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AGomProject
{
    public partial class MainForm : Form
    {
        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)

        private int memberId; // 현재 로그인한 사용자의 ID
        private List<BookData> bookList = new List<BookData>();

        public MainForm(int memberId)
        {
            InitializeComponent();
            this.memberId = memberId;

            LoadRandomBooks();
            UpdateUI();
            color_input();

            // Enter 키로 검색 버튼 작동
            tbMainSearch.KeyDown += TbMainSearch_KeyDown;
        }

        private void TbMainSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pbMainSearchButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void LoadRandomBooks()
        {
            bookList.Clear();

            string query = @"
                WITH RandomBooks AS (
                    SELECT TOP 4 b.isbn, b.cover_image, b.title, b.author, b.category_id, 
                                 c.category_name, bd.description
                    FROM Books b
                    JOIN Categories c ON b.category_id = c.category_id
                    LEFT JOIN BookDetails bd ON b.isbn = bd.isbn
                    ORDER BY NEWID()
                )
                SELECT * FROM RandomBooks";

            // ✅ 전역 DatabaseConfig 사용
            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string coverImagePath = reader["cover_image"].ToString();
                        string fullPath = ResolveCoverImagePath(coverImagePath);

                        bookList.Add(new BookData
                        {
                            Isbn = reader["isbn"].ToString(),
                            CoverImage = fullPath,
                            Title = reader["title"].ToString(),
                            Author = reader["author"].ToString(),
                            CategoryName = reader["category_name"].ToString(),
                            Description = reader["description"].ToString()
                        });
                    }
                }
            }
        }

        private string ResolveCoverImagePath(string coverImagePath)
        {
            string defaultCoverPathPng = Path.Combine(Application.StartupPath, "BookCovers", "default", "default_cover.png");
            string defaultCoverPathJpg = Path.Combine(Application.StartupPath, "BookCovers", "default", "default_cover.jpg");

            string defaultCoverPath = File.Exists(defaultCoverPathPng) ? defaultCoverPathPng : defaultCoverPathJpg;

            if (string.IsNullOrEmpty(coverImagePath))
                return defaultCoverPath;

            string fullPath = Path.Combine(Application.StartupPath, coverImagePath);
            return File.Exists(fullPath) ? fullPath : defaultCoverPath;
        }

        private void UpdateUI()
        {
            if (bookList.Count > 0)
            {
                LoadImage(pbReCmdBooksBig, bookList[0].CoverImage);
                lbReCmdBooksName.Text = bookList[0].Title;
                lbReCmdBooksAuthor.Text = bookList[0].Author;
                lbReCmdBooksGenre.Text = bookList[0].CategoryName;
                rtbReCmdBooksText.Text = bookList[0].Description;

                LoadImage(pbReCmdBooksSmall1, bookList.Count > 1 ? bookList[1].CoverImage : null);
                LoadImage(pbReCmdBooksSmall2, bookList.Count > 2 ? bookList[2].CoverImage : null);
                LoadImage(pbReCmdBooksSmall3, bookList.Count > 3 ? bookList[3].CoverImage : null);
            }
        }

        private void LoadImage(PictureBox pictureBox, string imagePath)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    pictureBox.Image = new Bitmap(stream);
                }
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        private void pbReCmdLeftButton_Click(object sender, EventArgs e)
        {
            if (bookList.Count > 0)
            {
                var temp = bookList[0];
                bookList.RemoveAt(0);
                bookList.Add(temp);
                UpdateUI();
            }
        }

        private void pbReCmdRightButton_Click(object sender, EventArgs e)
        {
            if (bookList.Count > 0)
            {
                var temp = bookList[bookList.Count - 1];
                bookList.RemoveAt(bookList.Count - 1);
                bookList.Insert(0, temp);
                UpdateUI();
            }
        }

        private void pbReCmdBooksBig_Click(object sender, EventArgs e) => OpenSelectBookForm(0);
        private void pbReCmdBooksSmall1_Click(object sender, EventArgs e) => OpenSelectBookForm(1);
        private void pbReCmdBooksSmall2_Click(object sender, EventArgs e) => OpenSelectBookForm(2);
        private void pbReCmdBooksSmall3_Click(object sender, EventArgs e) => OpenSelectBookForm(3);

        private void OpenSelectBookForm(int index)
        {
            if (index < bookList.Count)
            {
                SelectBookForm selectBookForm = new SelectBookForm(bookList[index].Isbn, memberId);
                selectBookForm.ShowDialog();
            }
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
                        cmd.Parameters.AddWithValue("@MemberId", memberId);
                        bool isAdmin = (bool)cmd.ExecuteScalar();

                        if (isAdmin)
                        {
                            AdminForm adminForm = new AdminForm(memberId);
                            adminForm.ShowDialog();
                        }
                        else
                        {
                            MyPageForm myPageForm = new MyPageForm(memberId);
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

        private void pbMainCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm(memberId);
            cartForm.ShowDialog();
        }

        public void ClearSearchText() => tbMainSearch.Text = string.Empty;

        private void pbMainSearchButton_Click(object sender, EventArgs e)
        {
            string mainFormText = tbMainSearch.Text;
            BookListForm bookListForm = new BookListForm(mainFormText, memberId, this);
            bookListForm.ShowDialog();
        }

        private void color_input()
        {
            pbMainCart.BackColor = Color.FromArgb(197, 249, 214);
            pbMainMyPage.BackColor = Color.FromArgb(197, 249, 214);
            pbMainLogo.BackColor = Color.FromArgb(197, 249, 214);
            pbLogoBackGround.BackColor = Color.FromArgb(197, 249, 214);
            lbMainLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbMainLogoText2.BackColor = Color.FromArgb(197, 249, 214);

            pbAILOGO.BackColor = Color.FromArgb(217, 217, 16);
            label2.BackColor = Color.FromArgb(217, 217, 16);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AIChatForm aIChatForm = new AIChatForm(memberId);
            aIChatForm.ShowDialog();
        }
    }

    public class BookData
    {
        public string Isbn { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
