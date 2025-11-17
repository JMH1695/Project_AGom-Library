using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class SelectBookForm : Form
    {
        private string isbn;
        private int memberId;

        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)

        public SelectBookForm(string isbn, int memberId)
        {
            this.isbn = isbn;
            this.memberId = memberId;
            InitializeComponent();
            LoadBookDetails(isbn);
            LoadMemberDetails(memberId);
            color_Input();
        }

        private void color_Input()
        {
            // Yellow
            pbSelectBookAddtoCartButton.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookNameTitle.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookName.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookAuthorTitle.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookAuthor.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookGenreTitle.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookGenre.BackColor = Color.FromArgb(254, 255, 210);
            lbSelectBookTextTitle.BackColor = Color.FromArgb(254, 255, 210);

            // Green
            pbSelectLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText2.BackColor = Color.FromArgb(197, 249, 214);
            pbSelectCart.BackColor = Color.FromArgb(197, 249, 214);
            pbSelectMyPage.BackColor = Color.FromArgb(197, 249, 214);

            // Buttons
            lbSelectBookAddtoCartButton.BackColor = Color.FromArgb(217, 217, 16);
            lbSelectBackButton.BackColor = Color.FromArgb(65, 158, 59);
        }

        public void LoadBookDetails(string isbn)
        {
            string query = @"
                SELECT b.title, b.author, b.category_id, c.category_name, bd.description, b.cover_image
                FROM Books b
                JOIN Categories c ON b.category_id = c.category_id
                LEFT JOIN BookDetails bd ON b.isbn = bd.isbn
                WHERE b.isbn = @isbn";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@isbn", isbn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lbSelectBookName.Text = reader["title"].ToString();
                    lbSelectBookAuthor.Text = reader["author"].ToString();
                    lbSelectBookGenre.Text = reader["category_name"].ToString();
                    lbSelectBookText.Text = reader["description"].ToString();

                    string imagePath = reader["cover_image"].ToString();
                    if (File.Exists(imagePath))
                        pbSelectBookImage.Image = Image.FromFile(imagePath);
                    else
                        pbSelectBookImage.Image = null;
                }
            }
        }

        public void LoadMemberDetails(int memberId)
        {
            string query = "SELECT * FROM Members WHERE member_id = @memberId";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@memberId", memberId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
            }
        }

        private void pbSelectBookAddtoCartButton_Click(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(*) FROM Cart WHERE member_id = @memberId AND isbn = @isbn";
            string insertQuery = "INSERT INTO Cart (member_id, isbn) OUTPUT INSERTED.cart_id VALUES (@memberId, @isbn)";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
            {
                conn.Open();

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@memberId", memberId);
                checkCmd.Parameters.AddWithValue("@isbn", isbn);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("이미 추가한 책입니다.");
                }
                else
                {
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@memberId", memberId);
                    insertCmd.Parameters.AddWithValue("@isbn", isbn);

                    int cartId = (int)insertCmd.ExecuteScalar();
                    MessageBox.Show("책이 장바구니에 추가되었습니다.");
                }
            }
        }

        private void pbSelectCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm(memberId);
            cartForm.ShowDialog();
        }

        private void pbSelectMyPage_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
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

        private void pbSelectBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbSelectBookImage_Click(object sender, EventArgs e)
        {
        }
    }
}
