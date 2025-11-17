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
    public partial class LoginForm : Form
    {
        // ⚠️ master DB 연결은 DatabaseConfig에 포함되지 않음 (시스템용)

        private string masterConnectionString ="Server=DESKTOP-Your\\SQLEXPRESS01;Database=master;Trusted_Connection=True;";



        // ✅ 실제 프로젝트용 DB는 DatabaseConfig에서 관리
        private string dbConnectionString = DatabaseConfig.ConnectionString;

        public LoginForm()
        {
            InitializeComponent();
            CreateDatabaseIfNotExists();
            this.AcceptButton = btnLogin;
            txtPassword.PasswordChar = '●';
            color_Input();
        }

        private void color_Input()
        {
            pbLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText2.BackColor = Color.FromArgb(197, 249, 214);
        }

        private void CreateDatabaseIfNotExists()
        {
            try
            {
                using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
                {
                    masterConnection.Open();

                    SqlCommand checkDatabase = new SqlCommand(
                        "SELECT database_id FROM sys.databases WHERE Name = 'AGomDB'",
                        masterConnection);

                    object result = checkDatabase.ExecuteScalar();

                    if (result == null)
                    {
                        SqlCommand createDatabase = new SqlCommand("CREATE DATABASE AGomDB", masterConnection);
                        createDatabase.ExecuteNonQuery();
                        MessageBox.Show("데이터베이스가 생성되었습니다.");

                        CreateTables(); // 새 DB 테이블 자동 생성
                    }
                    else
                    {
                        // 존재 시 자동 테이블 확인
                        CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터베이스 생성 중 오류 발생: " + ex.Message);
            }
        }

        private void CreateTables()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(dbConnectionString))
                {
                    dbConnection.Open();

                    string createSecurityQuestionsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SecurityQuestions')
                    BEGIN
                        CREATE TABLE SecurityQuestions (
                            question_id INT PRIMARY KEY IDENTITY(1,1),
                            question_text NVARCHAR(255) NOT NULL
                        );
                        INSERT INTO SecurityQuestions (question_text)
                        VALUES 
                            (N'귀하의 출신 초등학교 이름은 무엇입니까?'),
                            (N'귀하가 태어난 곳의 이름은 무엇입니까?'),
                            (N'귀하의 어머니의 성함은 무엇입니까?'),
                            (N'귀하가 가장 좋아하는 음식은 무엇입니까?'),
                            (N'귀하의 첫 번째 애완동물의 이름은 무엇입니까?'),
                            (N'귀하의 보물 제 1호는 무엇입니까?');
                    END";

                    string createLibrariesTable = @"
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

                    string createCategoriesTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
                    BEGIN
                        CREATE TABLE Categories (
                            category_id INT PRIMARY KEY IDENTITY(1,1),
                            category_name NVARCHAR(255) NOT NULL
                        )
                    END";

                    string createLanguagesTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Languages')
                    BEGIN
                        CREATE TABLE Languages (
                            language_id INT PRIMARY KEY IDENTITY(1,1),
                            language_name NVARCHAR(50) NOT NULL UNIQUE
                        )
                    END";

                    string createBooksTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Books')
                    BEGIN
                        CREATE TABLE Books (
                            isbn VARCHAR(20) PRIMARY KEY,
                            author VARCHAR(50) NOT NULL,
                            category_id INT NOT NULL,
                            language_id INT NULL,
                            title NVARCHAR(255) NOT NULL,
                            is_ebook BIT DEFAULT 1,
                            cover_image VARCHAR(255) NULL,
                            ebook_url NVARCHAR(255) NULL,
                            FOREIGN KEY (category_id) REFERENCES Categories(category_id),
                            FOREIGN KEY (language_id) REFERENCES Languages(language_id)
                        )
                    END";

                    string createBookDetailsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BookDetails')
                    BEGIN
                        CREATE TABLE BookDetails (
                            detail_id INT PRIMARY KEY IDENTITY(1,1),
                            isbn VARCHAR(20) NOT NULL,
                            description NVARCHAR(MAX),
                            last_updated DATETIME DEFAULT GETDATE(),
                            FOREIGN KEY (isbn) REFERENCES Books(isbn) ON DELETE CASCADE
                        )
                    END";

                    string createLibraryCollectionsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LibraryCollections')
                    BEGIN
                        CREATE TABLE LibraryCollections (
                            library_collection_id INT PRIMARY KEY IDENTITY(1,1),
                            library_id INT NOT NULL,
                            isbn VARCHAR(20) NOT NULL,
                            quantity_available INT NULL DEFAULT 0,
                            FOREIGN KEY (library_id) REFERENCES Libraries(library_id),
                            FOREIGN KEY (isbn) REFERENCES Books(isbn)
                        )
                    END";

                    string createBookStockHistoryTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BookStockHistory')
                    BEGIN
                        CREATE TABLE BookStockHistory (
                            history_id INT PRIMARY KEY IDENTITY(1,1),
                            library_collection_id INT,
                            isbn VARCHAR(20),
                            library_id INT,
                            quantity INT,
                            record_date DATETIME DEFAULT GETDATE(),
                            FOREIGN KEY (library_collection_id) REFERENCES LibraryCollections(library_collection_id),
                            FOREIGN KEY (isbn) REFERENCES Books(isbn),
                            FOREIGN KEY (library_id) REFERENCES Libraries(library_id)
                        )
                    END";

                    string createMembersTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Members')
                    BEGIN
                        CREATE TABLE Members (
                            member_id INT PRIMARY KEY IDENTITY(1,1),
                            password NVARCHAR(255) NOT NULL,
                            first_name NVARCHAR(50) NOT NULL,
                            gender NVARCHAR(10),
                            birth_date DATE,
                            email NVARCHAR(255) NOT NULL UNIQUE,
                            phone NVARCHAR(20),
                            admin BIT NOT NULL DEFAULT 0,
                            security_question_id INT,
                            security_answer NVARCHAR(255),
                            FOREIGN KEY (security_question_id) REFERENCES SecurityQuestions(question_id)
                        )
                    END";

                    string createBorrowsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Borrows')
                    BEGIN
                        CREATE TABLE Borrows (
                            borrow_id INT PRIMARY KEY IDENTITY(1,1),
                            member_id INT NOT NULL,
                            library_collection_id INT NOT NULL,
                            borrow_date DATE NOT NULL,
                            due_date DATE NOT NULL,
                            return_date DATE NULL,
                            return_status NVARCHAR(10) CHECK (return_status IN ('대여 중', '반납됨', '연체')) NOT NULL,
                            FOREIGN KEY (member_id) REFERENCES Members(member_id),
                            FOREIGN KEY (library_collection_id) REFERENCES LibraryCollections(library_collection_id)
                        )
                    END";

                    string createCartTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cart')
                    BEGIN
                        CREATE TABLE Cart (
                            cart_id INT PRIMARY KEY IDENTITY(1,1),
                            member_id INT NOT NULL,
                            isbn VARCHAR(20) NOT NULL,
                            borrow_date DATE DEFAULT GETDATE(),
                            return_date DATE DEFAULT DATEADD(DAY, 7, GETDATE()),
                            FOREIGN KEY (member_id) REFERENCES Members(member_id),
                            FOREIGN KEY (isbn) REFERENCES Books(isbn)
                        )
                    END";

                    using (SqlCommand cmd = new SqlCommand("", dbConnection))
                    {
                        string[] scripts = {
                            createSecurityQuestionsTable, createLibrariesTable, createCategoriesTable,
                            createLanguagesTable, createBooksTable, createBookDetailsTable,
                            createLibraryCollectionsTable, createBookStockHistoryTable,
                            createMembersTable, createBorrowsTable, createCartTable
                        };

                        foreach (string script in scripts)
                        {
                            cmd.CommandText = script;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("DB 구조 확인 완료.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("테이블 생성 중 오류 발생: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("이메일과 비밀번호를 입력해주세요.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnectionString))
                {
                    conn.Open();
                    string query = "SELECT member_id, admin FROM Members WHERE email = @Email AND password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int memberId = reader.GetInt32(0);
                                bool isAdmin = reader.GetBoolean(1);

                                MessageBox.Show("로그인 성공!");
                                MainForm mainForm = new MainForm(memberId);
                                this.Hide();
                                mainForm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("이메일 또는 비밀번호가 올바르지 않습니다.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로그인 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("회원가입이 완료되었습니다. 로그인해주세요.");
            }
        }

        private void btnFindEmail_Click(object sender, EventArgs e)
        {
            FindEmailForm findEmailForm = new FindEmailForm();
            findEmailForm.ShowDialog();
        }

        private void btnFindPassword_Click(object sender, EventArgs e)
        {
            FindPasswordForm findPasswordForm = new FindPasswordForm();
            findPasswordForm.ShowDialog();
        }
    }
}
