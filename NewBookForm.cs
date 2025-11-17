using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace AGomProject
{
    public partial class NewBookForm : Form
    {
        private int adminId;

        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)

        public NewBookForm(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;
            LoadCategories();
            LoadLanguages();

            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            txtEbookUrl.Enabled = chkIsEbook.Checked;
        }

        // 카테고리 목록 로드
        private void LoadCategories()
        {
            try
            {
                cboCategory.Items.Clear();
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    conn.Open();
                    string query = "SELECT category_id, category_name FROM Categories ORDER BY category_id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboCategory.Items.Add(new CategoryItem
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            });
                        }
                    }
                }

                if (cboCategory.Items.Count > 0)
                    cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"카테고리 목록을 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        // 언어 목록 로드
        private void LoadLanguages()
        {
            try
            {
                cboLanguage.Items.Clear();
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    conn.Open();
                    string query = "SELECT language_id, language_name FROM Languages ORDER BY language_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboLanguage.Items.Add(new LanguageItem
                            {
                                LanguageId = reader.GetInt32(0),
                                LanguageName = reader.GetString(1)
                            });
                        }
                    }
                }
                if (cboLanguage.Items.Count > 0)
                    cboLanguage.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"언어 목록을 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
                AddNewBook();
        }

        private class CategoryItem
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public override string ToString() => CategoryName;
        }

        private class LanguageItem
        {
            public int LanguageId { get; set; }
            public string LanguageName { get; set; }
            public override string ToString() => LanguageName;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtIsbn.Text)) { MessageBox.Show("ISBN을 입력해주세요."); return false; }
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) { MessageBox.Show("도서 제목을 입력해주세요."); return false; }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text)) { MessageBox.Show("저자를 입력해주세요."); return false; }
            if (cboCategory.SelectedItem == null) { MessageBox.Show("카테고리를 선택해주세요."); return false; }
            if (string.IsNullOrWhiteSpace(txtCoverImage.Text)) { MessageBox.Show("표지 이미지를 선택해주세요."); return false; }
            if (chkIsEbook.Checked && string.IsNullOrWhiteSpace(txtEbookUrl.Text))
            {
                MessageBox.Show("전자책 URL을 입력해주세요.");
                return false;
            }
            return true;
        }

        private void AddNewBook()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // ISBN 중복 체크
                        string checkQuery = "SELECT COUNT(*) FROM Books WHERE isbn = @ISBN";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@ISBN", txtIsbn.Text);
                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show("이미 존재하는 ISBN입니다.");
                                transaction.Rollback();
                                return;
                            }
                        }

                        string coverImagePath = string.IsNullOrEmpty(txtCoverImage.Text)
                            ? "default_cover_image_path"
                            : Path.Combine("BookCovers", txtIsbn.Text, $"cover{Path.GetExtension(txtCoverImage.Text)}");

                        string insertQuery = @"
                            INSERT INTO Books (isbn, title, author, category_id, language_id, is_ebook, cover_image, ebook_url)
                            VALUES (@ISBN, @Title, @Author, @CategoryId, @LanguageId, @IsEbook, @CoverImage, @EbookUrl)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ISBN", txtIsbn.Text);
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                            cmd.Parameters.AddWithValue("@CategoryId", ((CategoryItem)cboCategory.SelectedItem).CategoryId);
                            cmd.Parameters.AddWithValue("@LanguageId",
                                cboLanguage.SelectedIndex == -1 ? 1 : ((LanguageItem)cboLanguage.SelectedItem).LanguageId);
                            cmd.Parameters.AddWithValue("@IsEbook", chkIsEbook.Checked);
                            cmd.Parameters.AddWithValue("@CoverImage", coverImagePath);
                            cmd.Parameters.AddWithValue("@EbookUrl",
                                string.IsNullOrWhiteSpace(txtEbookUrl.Text) ? DBNull.Value : (object)Path.Combine("EBooks", txtIsbn.Text));
                            cmd.ExecuteNonQuery();
                        }

                        // 도서 설명 추가
                        if (!string.IsNullOrWhiteSpace(rtxtDescription.Text))
                        {
                            string insertDetailsQuery = @"
                                INSERT INTO BookDetails (isbn, description, last_updated)
                                VALUES (@ISBN, @Description, GETDATE())";
                            using (SqlCommand cmd = new SqlCommand(insertDetailsQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ISBN", txtIsbn.Text);
                                cmd.Parameters.AddWithValue("@Description", rtxtDescription.Text);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 도서관에 실물 도서 등록
                        if (!chkIsEbook.Checked)
                        {
                            List<int> libraryIds = new List<int>();
                            string libraryQuery = "SELECT library_id FROM Libraries";
                            using (SqlCommand libCmd = new SqlCommand(libraryQuery, conn, transaction))
                            using (SqlDataReader reader = libCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                    libraryIds.Add(reader.GetInt32(0));
                            }

                            if (libraryIds.Count == 0)
                            {
                                MessageBox.Show("등록된 도서관이 없습니다.");
                                transaction.Rollback();
                                return;
                            }

                            string insertLibraryCollectionQuery = @"
                                INSERT INTO LibraryCollections (library_id, isbn, quantity_available)
                                VALUES (@LibraryId, @ISBN, @QuantityAvailable)";
                            foreach (int libraryId in libraryIds)
                            {
                                using (SqlCommand cmd = new SqlCommand(insertLibraryCollectionQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@LibraryId", libraryId);
                                    cmd.Parameters.AddWithValue("@ISBN", txtIsbn.Text);
                                    cmd.Parameters.AddWithValue("@QuantityAvailable", 5);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 커버 이미지 복사
                        if (!string.IsNullOrEmpty(txtCoverImage.Text))
                        {
                            string coverPath = Path.Combine(Application.StartupPath, "BookCovers", txtIsbn.Text);
                            Directory.CreateDirectory(coverPath);
                            string fileName = $"cover{Path.GetExtension(txtCoverImage.Text)}";
                            string destPath = Path.Combine(coverPath, fileName);
                            File.Copy(txtCoverImage.Text, destPath, true);
                        }

                        transaction.Commit();
                        MessageBox.Show("도서가 성공적으로 추가되었습니다!");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"도서 추가 중 오류 발생: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string newCategory = txtNewCategory.Text.Trim();
            if (string.IsNullOrEmpty(newCategory))
            {
                MessageBox.Show("새로운 카테고리 이름을 입력해주세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Categories WHERE category_name = @CategoryName";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CategoryName", newCategory);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("이미 존재하는 카테고리입니다.");
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Categories (category_name) VALUES (@CategoryName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", newCategory);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("새로운 카테고리가 추가되었습니다.");
                        txtNewCategory.Clear();
                        LoadCategories();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"카테고리 추가 중 오류: {ex.Message}");
            }
        }

        private void btnImageBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtCoverImage.Text = ofd.FileName;
                    LoadCoverImage();
                }
            }
        }

        private void txtCoverImage_TextChanged(object sender, EventArgs e) => LoadCoverImage();

        private void LoadCoverImage()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCoverImage.Text) && File.Exists(txtCoverImage.Text))
                {
                    using (var img = Image.FromFile(txtCoverImage.Text))
                    {
                        pbCoverPreview.Image?.Dispose();
                        pbCoverPreview.Image = new Bitmap(img);
                        pbCoverPreview.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 중 오류: {ex.Message}");
            }
        }

        private void btnAddLanguage_Click(object sender, EventArgs e)
        {
            string newLanguage = txtNewLanguage.Text.Trim();
            if (string.IsNullOrEmpty(newLanguage))
            {
                MessageBox.Show("새로운 언어 이름을 입력해주세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Languages WHERE language_name = @LanguageName";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@LanguageName", newLanguage);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("이미 존재하는 언어입니다.");
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Languages (language_name) VALUES (@LanguageName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageName", newLanguage);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("새로운 언어가 추가되었습니다.");
                        txtNewLanguage.Clear();
                        LoadLanguages();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"언어 추가 중 오류: {ex.Message}");
            }
        }

        private void btnEbookBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "전자책 이미지가 있는 폴더를 선택하세요.";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtEbookUrl.Text = fbd.SelectedPath;
                    chkIsEbook.Checked = true;
                }
            }
        }

        private void txtEbookUrl_TextChanged(object sender, EventArgs e)
        {
            chkIsEbook.Checked = !string.IsNullOrEmpty(txtEbookUrl.Text);
        }

        private void chkIsEbook_CheckedChanged(object sender, EventArgs e)
        {
            txtEbookUrl.Enabled = chkIsEbook.Checked;
            if (!chkIsEbook.Checked)
                txtEbookUrl.Clear();
        }

        private void btnAddBookInLibrary_Click(object sender, EventArgs e)
        {
            LibraryInventoryForm inventoryForm = new LibraryInventoryForm();
            inventoryForm.Show();
        }
    }
}
