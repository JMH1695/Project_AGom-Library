using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class BookListForm : Form
    {


        private int itemsPerPage = 8;
        private int currentPage = 1;
        private int totalPage = 1;

        private int memberId;
        private MainForm mainForm;

        private List<string> imagePaths = new List<string>();
        private List<string> titles = new List<string>();
        private List<string> authors = new List<string>();
        private List<string> genres = new List<string>();
        private List<string> isbns = new List<string>();

        public BookListForm(string mainFormText, int memberId, MainForm mainForm)
        {
            InitializeComponent();
            tbBookListSearch.Text = mainFormText;
            this.memberId = memberId;
            this.mainForm = mainForm;
            color_Input();

            this.Shown += new EventHandler(BookListForm_Shown);
            this.FormClosed += BookListForm_FormClosed;
        }

        private void BookListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mainForm != null)
            {
                mainForm.ClearSearchText();
            }
        }

        private void BookListForm_Shown(object sender, EventArgs e)
        {
            LoadImagesFromDatabase();
        }

        private void LoadImagesFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT B.title, B.author, C.category_name, B.isbn, B.cover_image
                    FROM Books B
                    LEFT JOIN Categories C ON B.category_id = C.category_id
                    WHERE B.title LIKE @title OR B.author LIKE @author";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", "%" + tbBookListSearch.Text + "%");
                    command.Parameters.AddWithValue("@author", "%" + tbBookListSearch.Text + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        titles.Clear();
                        authors.Clear();
                        genres.Clear();
                        isbns.Clear();
                        imagePaths.Clear();

                        while (reader.Read())
                        {
                            titles.Add(reader.GetString(0));
                            authors.Add(reader.GetString(1));
                            genres.Add(reader.GetString(2));
                            isbns.Add(reader.GetString(3));
                            imagePaths.Add(reader.GetString(4));
                        }
                    }
                }
            }

            imagePaths.Sort();
            totalPage = (int)Math.Ceiling((double)imagePaths.Count / itemsPerPage);
            LoadImagesForCurrentPage(titles, authors, genres);
            UpdatePageCount();
        }

        private void LoadImagesForCurrentPage(List<string> titles, List<string> authors, List<string> genres)
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage - 1, imagePaths.Count - 1);

            string[] pictureBoxNames = {
                "pbBookListBookImage1", "pbBookListBookImage2", "pbBookListBookImage3",
                "pbBookListBookImage4", "pbBookListBookImage5", "pbBookListBookImage6",
                "pbBookListBookImage7", "pbBookListBookImage8"
            };

            string[] nameLabelNames = {
                "lbBookListBookName1", "lbBookListBookName2", "lbBookListBookName3",
                "lbBookListBookName4", "lbBookListBookName5", "lbBookListBookName6",
                "lbBookListBookName7", "lbBookListBookName8"
            };

            string[] authorLabelNames = {
                "lbBookListBookAuthor1", "lbBookListBookAuthor2", "lbBookListBookAuthor3",
                "lbBookListBookAuthor4", "lbBookListBookAuthor5", "lbBookListBookAuthor6",
                "lbBookListBookAuthor7", "lbBookListBookAuthor8"
            };

            string[] genreLabelNames = {
                "lbBookListBookGenre1", "lbBookListBookGenre2", "lbBookListBookGenre3",
                "lbBookListBookGenre4", "lbBookListBookGenre5", "lbBookListBookGenre6",
                "lbBookListBookGenre7", "lbBookListBookGenre8"
            };

            for (int i = 0, j = startIndex; j <= endIndex; i++, j++)
            {
                string imagePath = imagePaths[j];
                PictureBox pictureBox = Controls.Find(pictureBoxNames[i], true).FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                {
                    pictureBox.Image = System.IO.File.Exists(imagePath) ? Image.FromFile(imagePath) : null;
                    pictureBox.Tag = isbns[j];
                }

                Label nameLabel = Controls.Find(nameLabelNames[i], true).FirstOrDefault() as Label;
                if (nameLabel != null) nameLabel.Text = titles[j];

                Label authorLabel = Controls.Find(authorLabelNames[i], true).FirstOrDefault() as Label;
                if (authorLabel != null) authorLabel.Text = authors[j];

                Label genreLabel = Controls.Find(genreLabelNames[i], true).FirstOrDefault() as Label;
                if (genreLabel != null) genreLabel.Text = genres[j];
            }

            for (int i = endIndex - startIndex + 1; i < itemsPerPage; i++)
            {
                ClearControl(pictureBoxNames[i]);
                ClearControl(nameLabelNames[i]);
                ClearControl(authorLabelNames[i]);
                ClearControl(genreLabelNames[i]);
            }
        }

        private void ClearControl(string controlName)
        {
            Control control = Controls.Find(controlName, true).FirstOrDefault();
            if (control is PictureBox pictureBox)
            {
                pictureBox.Image = null;
                pictureBox.Tag = null;
            }
            else if (control is Label label)
            {
                label.Text = string.Empty;
            }
        }

        private void UpdatePageCount()
        {
            int totalItems = imagePaths.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            currentPage = Math.Min(currentPage, totalPages);
            lbBookListPageCount.Text = $"[{currentPage}/{totalPages}]";
        }

        private void UpdatePageDisplay()
        {
            lbBookListPageCount.Text = $"[{currentPage}/{totalPage}]";
        }

        private void pbBookListSearchButton_Click(object sender, EventArgs e)
        {
            imagePaths.Clear();
            LoadImagesFromDatabase();
            currentPage = 1;
            LoadImagesForCurrentPage(titles, authors, genres);
            UpdatePageCount();
        }

        private void pbBookListBookImage_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox?.Tag == null)
                return;

            string isbn = clickedPictureBox.Tag.ToString();
            SelectBookForm selectBookForm = new SelectBookForm(isbn, memberId);
            selectBookForm.ShowDialog();
        }

        private void pbBookListBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void pbBookListSkipRight_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPage)
            {
                currentPage++;
                UpdatePageDisplay();
                LoadImagesForCurrentPage(titles, authors, genres);
            }
        }

        private void pbBookListSkipLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePageDisplay();
                LoadImagesForCurrentPage(titles, authors, genres);
            }
        }

        private void LoadImagesByCategory(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT B.title, B.author, C.category_name, B.isbn, B.cover_image
                    FROM Books B
                    LEFT JOIN Categories C ON B.category_id = C.category_id
                    WHERE (B.title LIKE @title OR B.author LIKE @author)
                      AND B.category_id = @categoryId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", "%" + tbBookListSearch.Text + "%");
                    command.Parameters.AddWithValue("@author", "%" + tbBookListSearch.Text + "%");
                    command.Parameters.AddWithValue("@categoryId", categoryId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        titles.Clear();
                        authors.Clear();
                        genres.Clear();
                        isbns.Clear();
                        imagePaths.Clear();

                        while (reader.Read())
                        {
                            titles.Add(reader.GetString(0));
                            authors.Add(reader.GetString(1));
                            genres.Add(reader.GetString(2));
                            isbns.Add(reader.GetString(3));
                            imagePaths.Add(reader.GetString(4));
                        }
                    }
                }
            }

            imagePaths.Sort();
            currentPage = 1;
            LoadImagesForCurrentPage(titles, authors, genres);
            UpdatePageCount();
        }

        private void pbBookListCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm(memberId);
            cartForm.ShowDialog();
        }

        private void pbBookListMyPage_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void color_Input()
        {
            pbBookListGenre1.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre2.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre3.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre4.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre5.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre6.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre7.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre8.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre9.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre10.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre11.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre12.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre13.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListGenre14.BackColor = Color.FromArgb(254, 255, 210);

            lbBookListGenre1.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre2.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre3.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre4.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre5.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre6.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre7.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre8.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre9.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre10.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre11.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre12.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre13.BackColor = Color.FromArgb(217, 217, 16);
            lbBookListGenre14.BackColor = Color.FromArgb(217, 217, 16);

            pbBookListBackButton.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListSkipLeft.BackColor = Color.FromArgb(254, 255, 210);
            pbBookListSkipRight.BackColor = Color.FromArgb(254, 255, 210);
            lbBookListPageCount.BackColor = Color.FromArgb(254, 255, 210);

            pbBookListCart.BackColor = Color.FromArgb(197, 249, 214);
            pbBookListMyPage.BackColor = Color.FromArgb(197, 249, 214);

            pbLogoBackGround.BackColor = Color.FromArgb(197, 249, 214);
            pbBookListLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText2.BackColor = Color.FromArgb(197, 249, 214);

            lbBookListBackButton.BackColor = Color.FromArgb(65, 158, 59);
        }

        // ============================
        // 🔽 누락된 이벤트 핸들러 추가 🔽
        // ============================
        private void lbLogoText1_Click(object sender, EventArgs e) { }
        private void lbLogoText2_Click(object sender, EventArgs e) { }
        private void pbBookListLogo_Click(object sender, EventArgs e) { }
        private void pbLogoBackGround_Click(object sender, EventArgs e) { }

        private void pbBookListGenre1_Click(object sender, EventArgs e) => LoadImagesByCategory(1);
        private void pbBookListGenre2_Click(object sender, EventArgs e) => LoadImagesByCategory(2);
        private void pbBookListGenre3_Click(object sender, EventArgs e) => LoadImagesByCategory(3);
        private void pbBookListGenre4_Click(object sender, EventArgs e) => LoadImagesByCategory(4);
        private void pbBookListGenre5_Click(object sender, EventArgs e) => LoadImagesByCategory(5);
        private void pbBookListGenre6_Click(object sender, EventArgs e) => LoadImagesByCategory(6);
        private void pbBookListGenre7_Click(object sender, EventArgs e) => LoadImagesByCategory(7);
        private void pbBookListGenre8_Click(object sender, EventArgs e) => LoadImagesByCategory(8);
        private void pbBookListGenre9_Click(object sender, EventArgs e) => LoadImagesByCategory(9);
        private void pbBookListGenre10_Click(object sender, EventArgs e) => LoadImagesByCategory(10);
        private void pbBookListGenre11_Click(object sender, EventArgs e) => LoadImagesByCategory(11);
        private void pbBookListGenre12_Click(object sender, EventArgs e) => LoadImagesByCategory(12);
        private void pbBookListGenre13_Click(object sender, EventArgs e) => LoadImagesByCategory(13);
        private void pbBookListGenre14_Click(object sender, EventArgs e) => LoadImagesByCategory(14);

    }
}
