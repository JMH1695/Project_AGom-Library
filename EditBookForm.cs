using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace AGomProject
{
    public partial class EditBookForm : Form
    {
        private int adminId;

        public EditBookForm(int adminId)
        {
            InitializeComponent();
            string defaultCoverDir = Path.Combine(Application.StartupPath, "BookCovers", "default");
            string defaultCoverPathPng = Path.Combine(defaultCoverDir, "default_cover.png");
            string defaultCoverPathJpg = Path.Combine(defaultCoverDir, "default_cover.jpg");

            if (!Directory.Exists(defaultCoverDir))
            {
                Directory.CreateDirectory(defaultCoverDir);
            }

            // 둘 다 없을 경우에만 생성
            if (!File.Exists(defaultCoverPathPng) && !File.Exists(defaultCoverPathJpg))
            {
                using (Bitmap bmp = new Bitmap(200, 300))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightGray);
                    using (Font font = new Font("Arial", 12))
                    {
                        string text = "No Cover";
                        SizeF textSize = g.MeasureString(text, font);
                        g.DrawString(text, font, Brushes.Black,
                            (200 - textSize.Width) / 2,
                            (300 - textSize.Height) / 2);
                    }
                    bmp.Save(defaultCoverPathJpg, ImageFormat.Jpeg);  // jpg로 저장
                }
            }

            this.adminId = adminId;
            LoadCategories();  // 카테고리 목록 로드
            LoadLanguages();
            InitializeDataGridView();  // DataGridView 초기 설정
            cboSelectCategory.DropDownStyle = ComboBoxStyle.DropDownList; //장르
            cboSelectLanguage.DropDownStyle = ComboBoxStyle.DropDownList; //언어
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            chkIsEbook.Enabled = false;  // 전자책 여부 변경 불가능

            this.AcceptButton = btnSearch;
            // EbookUrl 초기 상태 설정
            txtEbookUrl.Enabled = chkIsEbook.Checked;
        }



        private void LoadCategories()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT category_id, category_name FROM Categories ORDER BY category_id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 두 콤보박스 모두 초기화
                            cboCategory.Items.Clear();          // 도서 카테고리 선택용
                            cboSelectCategory.Items.Clear();    // 카테고리 관리용

                            while (reader.Read())
                            {
                                CategoryItem item = new CategoryItem
                                {
                                    CategoryId = reader.GetInt32(0),
                                    CategoryName = reader.GetString(1)
                                };
                                cboCategory.Items.Add(item);
                                cboSelectCategory.Items.Add(item);
                            }
                        }
                    }

                    // 각 콤보박스에 기본 선택 설정
                    if (cboCategory.Items.Count > 0)
                        cboCategory.SelectedIndex = 0;
                    if (cboSelectCategory.Items.Count > 0)
                        cboSelectCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"카테고리 목록을 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }



        // ComboBox 선택 변경 이벤트
        private void cboSelectCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectCategory.SelectedItem != null) // 선택된 항목이 있는지 확인
            {
                // 선택된 항목을 CategoryItem 타입으로 형변환
                CategoryItem selectedCategory = (CategoryItem)cboSelectCategory.SelectedItem;
                // 선택된 카테고리의 이름을 텍스트박스에 표시
                txtEditCategory.Text = selectedCategory.CategoryName;
            }
        }


        // 수정 버튼 클릭
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (cboSelectCategory.SelectedItem == null)
            {
                MessageBox.Show("수정할 카테고리를 선택해주세요.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditCategory.Text))
            {
                MessageBox.Show("카테고리 이름을 입력해주세요.");
                return;
            }

            try
            {
                CategoryItem selectedCategory = (CategoryItem)cboSelectCategory.SelectedItem;

                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Categories SET category_name = @NewName WHERE category_id = @CategoryId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewName", txtEditCategory.Text.Trim());
                        cmd.Parameters.AddWithValue("@CategoryId", selectedCategory.CategoryId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("카테고리가 수정되었습니다!");
                LoadCategories();  // ComboBox 목록 새로고침
                txtEditCategory.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"카테고리 수정 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        //추가 버튼 클릭
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string newCategory = txtEditCategory.Text.Trim();

            if (string.IsNullOrEmpty(newCategory))
            {
                MessageBox.Show("새로운 카테고리 이름을 입력해주세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // 카테고리 중복 검사
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

                    // 새 카테고리 추가
                    string insertQuery = "INSERT INTO Categories (category_name) VALUES (@CategoryName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", newCategory);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("새로운 카테고리가 추가되었습니다.");
                        txtEditCategory.Clear();
                        LoadCategories();  // 카테고리 목록 새로고침
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"카테고리 추가 중 오류가 발생했습니다: {ex.Message}");
            }
        }


        // 삭제 버튼 클릭
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (cboSelectCategory.SelectedItem == null)
            {
                MessageBox.Show("삭제할 카테고리를 선택해주세요.");
                return;
            }

            CategoryItem selectedCategory = (CategoryItem)cboSelectCategory.SelectedItem;

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // 해당 카테고리를 사용하는 도서가 있는지 확인
                    string checkQuery = "SELECT COUNT(*) FROM Books WHERE category_id = @CategoryId";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", selectedCategory.CategoryId);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show($"'{selectedCategory.CategoryName}' 카테고리는 현재 {count}개의 도서에서 사용 중이므로 삭제할 수 없습니다.");
                            return;
                        }
                    }

                    // 삭제 확인
                    if (MessageBox.Show($"'{selectedCategory.CategoryName}' 카테고리를 삭제하시겠습니까?",
                        "카테고리 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM Categories WHERE category_id = @CategoryId";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@CategoryId", selectedCategory.CategoryId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("카테고리가 삭제되었습니다.");
                        LoadCategories();  // ComboBox 목록 새로고침
                        txtEditCategory.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"카테고리 삭제 중 오류가 발생했습니다: {ex.Message}");
            }
        }








        private void LoadLanguages()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT language_id, language_name FROM Languages ORDER BY language_id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cboLanguage.Items.Clear(); //언어 선택용
                            cboSelectLanguage.Items.Clear();
                            while (reader.Read())
                            {
                                LanguageItem item = new LanguageItem
                                {
                                    LanguageId = reader.GetInt32(0),
                                    LanguageName = reader.GetString(1)
                                };
                                cboLanguage.Items.Add(item);
                                cboSelectLanguage.Items.Add(item);
                            }


                        }
                    }

                    if (cboSelectLanguage.Items.Count > 0)
                        cboSelectLanguage.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"언어 목록을 불러오는 중 오류가 발생했습니다: {ex.Message}");
            }
        }





        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("검색어를 입력해주세요.");
                return;
            }

            SearchBooks(searchText);
        }

        private void SearchBooks(string searchText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT b.isbn AS ISBN, b.title AS Title, b.author AS Author, c.category_name AS CategoryName
                        FROM Books b 
                        JOIN Categories c ON b.category_id = c.category_id
                        WHERE ";

                    if (rdoIsbn.Checked)
                        query += "b.isbn LIKE @SearchText + '%'";
                    else
                        query += "b.title LIKE '%' + @SearchText + '%'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", searchText);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBooks.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"도서 검색 중 오류가 발생했습니다: {ex.Message}");
            }
        }



        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow != null)
            {
                try
                {
                    string isbn = dgvBooks.CurrentRow.Cells["colIsbn"].Value?.ToString();  // colIsbn으로 수정

                    using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();
                        string query = @"
                                SELECT b.*, c.category_id, l.language_id, bd.description
                                FROM Books b 
                                JOIN Categories c ON b.category_id = c.category_id 
                                JOIN Languages l ON b.language_id = l.language_id
                                LEFT JOIN BookDetails bd ON b.isbn = bd.isbn
                                WHERE b.isbn = @ISBN";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ISBN", isbn);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // 각 컨트롤에 데이터 표시
                                    txtIsbn.Text = reader["isbn"].ToString();
                                    txtTitle.Text = reader["title"].ToString();
                                    txtAuthor.Text = reader["author"].ToString();

                                    chkIsEbook.Checked = (bool)reader["is_ebook"];
                                    txtCoverImage.Text = reader["cover_image"].ToString();
                                    txtEbookUrl.Text = reader["ebook_url"]?.ToString();

                                    // 카테고리 콤보박스 선택
                                    int categoryId = (int)reader["category_id"];
                                    foreach (CategoryItem item in cboCategory.Items)
                                    {
                                        if (item.CategoryId == categoryId)
                                        {
                                            cboCategory.SelectedItem = item;
                                            break;
                                        }
                                    }
                                    // 언어 콤보박스 선택
                                    int languageId = (int)reader["language_id"];
                                    foreach (LanguageItem item in cboLanguage.Items)
                                    {
                                        if (item.LanguageId == languageId)
                                        {
                                            cboLanguage.SelectedItem = item;
                                            break;
                                        }
                                    }

                                    // 표지 이미지 표시

                                    string coverImagePath = reader["cover_image"].ToString();
                                    string defaultCoverPathPng = Path.Combine(Application.StartupPath, "BookCovers", "default", "default_cover.png");
                                    string defaultCoverPathJpg = Path.Combine(Application.StartupPath, "BookCovers", "default", "default_cover.jpg");
                                    string fullPath;

                                    // 기본 이미지 경로 설정
                                    string defaultCoverPath;
                                    if (File.Exists(defaultCoverPathPng))
                                        defaultCoverPath = defaultCoverPathPng;
                                    else
                                        defaultCoverPath = defaultCoverPathJpg;

                                    // 실제 사용할 이미지 경로 결정
                                    if (string.IsNullOrEmpty(coverImagePath))
                                    {
                                        fullPath = defaultCoverPath;
                                    }
                                    else
                                    {
                                        fullPath = Path.Combine(Application.StartupPath, coverImagePath);
                                        if (!File.Exists(fullPath))
                                        {
                                            fullPath = defaultCoverPath;
                                        }
                                    }

                                    try
                                    {
                                        if (pbCoverPreview.Image != null)
                                        {
                                            var oldImage = pbCoverPreview.Image;
                                            pbCoverPreview.Image = null;
                                            oldImage.Dispose();
                                        }

                                        using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                                        {
                                            pbCoverPreview.Image = new Bitmap(stream);
                                        }
                                        pbCoverPreview.SizeMode = PictureBoxSizeMode.Zoom;
                                    }
                                    catch
                                    {
                                        if (pbCoverPreview.Image != null)
                                        {
                                            pbCoverPreview.Image.Dispose();
                                            pbCoverPreview.Image = null;
                                        }
                                    }


                                    int descriptionOrdinal = reader.GetOrdinal("description");
                                    bool isNull = reader.IsDBNull(descriptionOrdinal);
                                    string description = isNull ? "" : reader.GetString(descriptionOrdinal);
                                    rtxtDescription.Text = description;

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"도서 정보를 불러오는 중 오류가 발생했습니다: {ex.Message}");
                }
            }
        }

        // CategoryItem 클래스 (NewBookForm과 동일)
        private class CategoryItem
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

            public override string ToString()
            {
                return CategoryName;
            }
        }


        private class LanguageItem
        {
            public int LanguageId { get; set; }
            public string LanguageName { get; set; }

            public override string ToString()
            {
                return LanguageName;
            }
        }


        // DataGridView 초기화 메서드 수정
        private void InitializeDataGridView()
        {
            // 도서 목록 DataGridView 설정
            //dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.AllowUserToAddRows = false;  // 사용자가 직접 행 추가 불가
            dgvBooks.ReadOnly = true;  // 더블클릭으로 수정 불가능
            dgvBooks.ColumnHeadersVisible = true; //헤더보이기
            dgvBooks.Refresh();  // 추가

        }






        private bool ValidateInputs()
        {
            // 필수 입력값 검증
            if (string.IsNullOrWhiteSpace(txtIsbn.Text) ||
                string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                cboCategory.SelectedIndex == -1 ||
                cboLanguage.SelectedIndex == -1)
            {
                MessageBox.Show("모든 필수 입력값을 입력해주세요.");
                return false;
            }

            // 전자책인 경우 URL 필수 입력
            // 전자책 처리
            // 전자책인 경우 검증
            if (chkIsEbook.Checked)
            {
                string fullPath = Path.Combine(Application.StartupPath, txtEbookUrl.Text);
                MessageBox.Show($"확인할 경로: {fullPath}"); // 디버깅용

                // 새로운 경로가 선택된 경우
                if (!txtEbookUrl.Text.StartsWith("EBooks"))
                {
                    string[] imageFiles = Directory.GetFiles(txtEbookUrl.Text, "*.*")
                        .Where(file => file.ToLower().EndsWith(".jpg") ||
                                      file.ToLower().EndsWith(".jpeg") ||
                                      file.ToLower().EndsWith(".png"))
                        .OrderBy(file => file)
                        .ToArray();

                    if (!imageFiles.Any())
                    {
                        MessageBox.Show("전자책 이미지가 없습니다.");
                        return false;
                    }
                }
                else  // 기존 전자책 경로인 경우
                {
                    string[] existingImages = Directory.GetFiles(fullPath, "*.*")
                        .Where(file => file.ToLower().EndsWith(".jpg") ||
                                      file.ToLower().EndsWith(".jpeg") ||
                                      file.ToLower().EndsWith(".png"))
                        .ToArray();

                    MessageBox.Show($"찾은 이미지 수: {existingImages.Length}"); // 디버깅용

                    if (!existingImages.Any())
                    {
                        MessageBox.Show("기존 전자책 이미지를 찾을 수 없습니다.");
                        return false;
                    }
                }
            }

            return true;
        }



        private void UpdateBook()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // 트랜잭션 시작
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // 도서 정보 업데이트
                        string updateQuery = @"
                    UPDATE Books
                    SET title = @Title, 
                        author = @Author, 
                        category_id = @CategoryId, 
                        language_id = @LanguageId, 
                        is_ebook = @IsEbook, 
                        cover_image = @CoverImage, 
                        ebook_url = @EbookUrl
                    WHERE isbn = @ISBN";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ISBN", txtIsbn.Text);
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                            cmd.Parameters.AddWithValue("@CategoryId", ((CategoryItem)cboCategory.SelectedItem).CategoryId);
                            cmd.Parameters.AddWithValue("@LanguageId", ((LanguageItem)cboLanguage.SelectedItem).LanguageId);
                            cmd.Parameters.AddWithValue("@IsEbook", chkIsEbook.Checked);
                            // cover_image 파라미터 설정 부분 수정
                            if (string.IsNullOrEmpty(txtCoverImage.Text))
                            {
                                cmd.Parameters.AddWithValue("@CoverImage", Path.Combine("BookCovers", "default", "default_cover.png"));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CoverImage", Path.Combine("BookCovers", txtIsbn.Text, $"cover{Path.GetExtension(txtCoverImage.Text)}"));
                            }
                            cmd.Parameters.AddWithValue("@EbookUrl", string.IsNullOrWhiteSpace(txtEbookUrl.Text) ? DBNull.Value : (object)Path.Combine("EBooks", txtIsbn.Text));


                            cmd.ExecuteNonQuery();
                        }

                        // 도서 설명 처리 (업데이트 또는 추가)
                        string upsertDetailsQuery = @"
                            IF EXISTS (SELECT 1 FROM BookDetails WHERE isbn = @ISBN)
                            BEGIN
                                IF @Description IS NULL OR @Description = ''
                                BEGIN
                                    DELETE FROM BookDetails WHERE isbn = @ISBN
                                END
                                ELSE
                                BEGIN
                                    UPDATE BookDetails 
                                    SET description = @Description, last_updated = GETDATE()
                                    WHERE isbn = @ISBN
                                END
                            END
                            ELSE
                            BEGIN
                                IF @Description IS NOT NULL AND @Description <> ''
                                BEGIN
                                    INSERT INTO BookDetails (isbn, description, last_updated)
                                    VALUES (@ISBN, @Description, GETDATE())
                                END
                            END";
                        using (SqlCommand cmd = new SqlCommand(upsertDetailsQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ISBN", txtIsbn.Text);
                            cmd.Parameters.AddWithValue("@Description",
                                string.IsNullOrWhiteSpace(rtxtDescription.Text) ? (object)DBNull.Value : rtxtDescription.Text);
                            cmd.ExecuteNonQuery();
                        }

                        // 전자책 이미지 복사 및 검증
                        // 전자책 처리
                        if (chkIsEbook.Checked)
                        {
                            string isbn = txtIsbn.Text;
                            string ebookRelativePath = Path.Combine("EBooks", isbn);

                            // txtEbookUrl의 텍스트가 변경된 경우에만 새로운 이미지를 복사
                            if (!txtEbookUrl.Text.StartsWith("EBooks") || txtEbookUrl.Text.Contains("default"))
                            {
                                string ebookPath = Path.Combine(Application.StartupPath, ebookRelativePath);
                                Directory.CreateDirectory(ebookPath);

                                // 숫자 정렬을 위해 OrderBy 수정
                                string[] imageFiles = Directory.GetFiles(txtEbookUrl.Text, "*.*")
                                    .Where(file => file.ToLower().EndsWith(".jpg") ||
                                                  file.ToLower().EndsWith(".jpeg") ||
                                                  file.ToLower().EndsWith(".png"))
                                    .OrderBy(file =>
                                    {
                                        string fileName = Path.GetFileNameWithoutExtension(file);
                                        if (int.TryParse(fileName, out int number))
                                            return number;
                                        return 0;
                                    })
                                    .ToArray();

                                // 최소 한 장의 이미지가 있는지 확인
                                if (!imageFiles.Any())
                                {
                                    MessageBox.Show("전자책 이미지가 없습니다.");
                                    return;
                                }

                                // 새 이미지 파일 복사
                                for (int i = 0; i < imageFiles.Length; i++)
                                {
                                    string destFileName = $"page_{(i + 1):D3}{Path.GetExtension(imageFiles[i])}";
                                    string destPath = Path.Combine(ebookPath, destFileName);
                                    File.Copy(imageFiles[i], destPath, true);
                                }

                                var bookInfo = new
                                {
                                    isbn = isbn,
                                    title = txtTitle.Text,
                                    author = txtAuthor.Text,
                                    total_pages = imageFiles.Length
                                };

                                string jsonPath = Path.Combine(ebookPath, "info.json");
                                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(bookInfo, JsonFormatting.Indented));
                            }
                        }

                        // 커버 이미지 복사
                        if (!string.IsNullOrEmpty(txtCoverImage.Text))
                        {
                            // 새로운 이미지가 선택된 경우에만 복사 진행
                            if (!txtCoverImage.Text.StartsWith("BookCovers"))
                            {
                                string coverPath = Path.Combine(Application.StartupPath, "BookCovers", txtIsbn.Text);
                                Directory.CreateDirectory(coverPath);

                                string fileName = $"cover{Path.GetExtension(txtCoverImage.Text)}";
                                string destPath = Path.Combine(coverPath, fileName);

                                using (FileStream sourceStream = new FileStream(txtCoverImage.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    using (FileStream destinationStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                                    {
                                        sourceStream.CopyTo(destinationStream);
                                    }
                                }
                            }
                        }

                        // 모든 작업이 성공하면 트랜잭션 커밋
                        transaction.Commit();
                        MessageBox.Show("도서가 성공적으로 업데이트되었습니다!");
                    }
                    catch (Exception)
                    {
                        // 오류 발생 시 트랜잭션 롤백
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"도서 업데이트 중 오류가 발생했습니다: {ex.Message}");
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                UpdateBook();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void LoadCoverImage()
        {
            if (!string.IsNullOrEmpty(txtCoverImage.Text) && File.Exists(txtCoverImage.Text))
            {
                try
                {
                    using (FileStream fs = new FileStream(txtCoverImage.Text, FileMode.Open, FileAccess.Read))
                    {
                        // PictureBox에 이미지를 로드합니다.
                        pbCoverPreview.Image = Image.FromStream(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"커버 이미지를 불러오는 중 오류가 발생했습니다: {ex.Message}");
                }
            }
            else
            {
                pbCoverPreview.Image = null;
            }
        }


        private void btnEbookBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "전자책 이미지가 있는 폴더를 선택하세요.";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string isbn = txtIsbn.Text;
                    if (string.IsNullOrEmpty(isbn))
                    {
                        MessageBox.Show("먼저 ISBN을 입력해주세요.");
                        return;
                    }

                    // 선택한 폴더 경로를 텍스트 박스에 설정
                    txtEbookUrl.Text = fbd.SelectedPath;
                    chkIsEbook.Checked = true;
                }
            }
        }

        private void cboSelectLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectLanguage.SelectedItem != null)
            {
                LanguageItem selectedLanguage = (LanguageItem)cboSelectLanguage.SelectedItem;
                txtEditLanguage.Text = selectedLanguage.LanguageName;
            }
        }

        private void btnEditLanguage_Click(object sender, EventArgs e)
        {
            if (cboSelectLanguage.SelectedItem == null)
            {
                MessageBox.Show("수정할 언어를 선택해주세요.");
                return;
            }

            try
            {
                string newName = txtEditLanguage.Text.Trim();
                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("새로운 언어 이름을 입력해주세요.");
                    return;
                }

                LanguageItem selectedLanguage = (LanguageItem)cboSelectLanguage.SelectedItem;

                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // 중복 검사 (자기 자신은 제외)
                    string checkQuery = "SELECT COUNT(*) FROM Languages WHERE language_name = @LanguageName AND language_id != @LanguageId";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageName", newName);
                        cmd.Parameters.AddWithValue("@LanguageId", selectedLanguage.LanguageId);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("이미 존재하는 언어입니다.");
                            return;
                        }
                    }

                    // 언어 수정
                    string updateQuery = "UPDATE Languages SET language_name = @LanguageName WHERE language_id = @LanguageId";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageName", newName);
                        cmd.Parameters.AddWithValue("@LanguageId", selectedLanguage.LanguageId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("언어가 수정되었습니다.");
                    LoadLanguages();  // 콤보박스 새로고침
                    txtEditLanguage.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"언어 수정 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnAddLanguage_Click(object sender, EventArgs e)
        {
            try
            {
                string newLanguage = txtEditLanguage.Text.Trim();
                if (string.IsNullOrEmpty(newLanguage))
                {
                    MessageBox.Show("새로운 언어 이름을 입력해주세요.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // 중복 검사
                    string checkQuery = "SELECT COUNT(*) FROM Languages WHERE language_name = @LanguageName";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageName", newLanguage);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("이미 존재하는 언어입니다.");
                            return;
                        }
                    }

                    // 새 언어 추가
                    string insertQuery = "INSERT INTO Languages (language_name) VALUES (@LanguageName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageName", newLanguage);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("새로운 언어가 추가되었습니다.");
                    LoadLanguages();  // 콤보박스 새로고침
                    txtEditLanguage.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"언어 추가 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnDeleteLanguage_Click(object sender, EventArgs e)
        {
            if (cboSelectLanguage.SelectedItem == null)
            {
                MessageBox.Show("삭제할 언어를 선택해주세요.");
                return;
            }

            LanguageItem selectedLanguage = (LanguageItem)cboSelectLanguage.SelectedItem;

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // 해당 언어를 사용하는 책이 있는지 확인
                    string checkQuery = "SELECT COUNT(*) FROM Books WHERE language_id = @LanguageId";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LanguageId", selectedLanguage.LanguageId);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show($"'{selectedLanguage.LanguageName}' 언어는 현재 {count}개의 도서에서 사용 중이므로 삭제할 수 없습니다.");
                            return;
                        }
                    }

                    // 삭제 확인
                    if (MessageBox.Show($"'{selectedLanguage.LanguageName}' 언어를 삭제하시겠습니까?",
                        "언어 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM Languages WHERE language_id = @LanguageId";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@LanguageId", selectedLanguage.LanguageId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("언어가 삭제되었습니다.");
                        LoadLanguages();  // 콤보박스 새로고침
                        txtEditLanguage.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"언어 삭제 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void chkIsEbook_CheckedChanged(object sender, EventArgs e)
        {
            txtEbookUrl.Enabled = chkIsEbook.Checked;
            if (!chkIsEbook.Checked)
            {
                txtEbookUrl.Clear();  // 체크 해제시 텍스트 지우기
            }
        }

        private void btnPreviewEbook_Click(object sender, EventArgs e)
        {
            if (chkIsEbook.Checked && !string.IsNullOrEmpty(txtIsbn.Text))
            {
                string ebookPath = Path.Combine(Application.StartupPath, "EBooks", txtIsbn.Text);
                // 다양한 이미지 형식 검사
                if (Directory.Exists(ebookPath) &&
                    Directory.GetFiles(ebookPath, "*.*")
                    .Where(file => file.ToLower().EndsWith(".jpg") ||
                                  file.ToLower().EndsWith(".jpeg") ||
                                  file.ToLower().EndsWith(".png"))
                    .Any())
                {
                    EbookViewerForm viewer = new EbookViewerForm(txtIsbn.Text);
                    viewer.ShowDialog();
                }
                else
                {
                    MessageBox.Show("전자책 파일을 찾을 수 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("전자책이 아니거나 ISBN이 없습니다.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //삭제 코드 만들까 말까
        }
    }
}
