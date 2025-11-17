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
using System.IO;  // File.Exists 메서드를 사용하기 위한 네임스페이스 추가

namespace AGomProject
{
    public partial class CartForm : Form
    {
        private List<string> isbnList; // ISBN 배열
        private List<string> titleList; // 책 제목 배열
        private List<string> authorList; // 저자 배열
        private List<string> genreList; // 장르 배열
        private List<string> descriptionList; // 설명 배열
        private List<string> coverImageList; // 이미지 경로 배열
        private int memberId;

        private int currentPage = 1;  // 현재 페이지 번호
        private const int itemsPerPage = 3; // 한 페이지에 표시할 항목 수

        public CartForm(int memberId)
        {
            this.memberId = memberId;
            InitializeComponent();
            LoadCartData(memberId);

            this.Shown += new EventHandler(CartForm_Shown);

            color_Input();
        }

        private void CartForm_Shown(object sender, EventArgs e)
        {
            GetCartBookData(memberId);  // 폼이 표시될 때마다 이미지를 로드
        }

        // Cart 데이터를 Books, Categories, BookDetails와 조인하여 가져오는 메서드
        private List<Dictionary<string, string>> GetCartBookData(int memberId)
        {
            List<Dictionary<string, string>> cartData = new List<Dictionary<string, string>>();

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                // Cart와 Books, Categories, BookDetails를 조인하여 필요한 데이터를 가져오는 쿼리
                string query = @"
            SELECT 
                c.isbn,
                b.title,
                b.author,
                b.cover_image,
                cat.category_name,
                bd.description
            FROM Cart c
            INNER JOIN Books b ON c.isbn = b.isbn
            INNER JOIN Categories cat ON b.category_id = cat.category_id
            LEFT JOIN BookDetails bd ON b.isbn = bd.isbn
            WHERE c.member_id = @memberId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@memberId", memberId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // 각 행의 데이터를 Dictionary로 저장
                        var data = new Dictionary<string, string>
                {
                    { "isbn", reader["isbn"].ToString() },
                    { "title", reader["title"].ToString() },
                    { "author", reader["author"].ToString() },
                    { "cover_image", reader["cover_image"].ToString() },
                    { "category_name", reader["category_name"].ToString() },
                    { "description", reader["description"]?.ToString() ?? string.Empty }
                };
                        cartData.Add(data);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"데이터를 불러오는 중 오류가 발생했습니다: {ex.Message}");
                }
            }

            return cartData;
        }

        private void LoadCartData(int memberId)
        {
            // 데이터를 가져오는 메서드를 호출하고 반환된 데이터를 멤버 변수에 저장
            var cartData = GetCartBookData(memberId);

            // 데이터를 각 리스트로 분리
            isbnList = cartData.Select(data => data["isbn"]).ToList();
            titleList = cartData.Select(data => data["title"]).ToList();
            authorList = cartData.Select(data => data["author"]).ToList();
            genreList = cartData.Select(data => data["category_name"]).ToList();
            descriptionList = cartData.Select(data => data["description"]).ToList();
            coverImageList = cartData.Select(data => data["cover_image"]).ToList();

            UpdatePageCount();
            DisplayPageData();

            Console.WriteLine($"isbnList count: {isbnList.Count}");

        }

       

        private void UpdatePageCount()
        {
            // 전체 페이지 수 계산
            int totalItems = isbnList.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)itemsPerPage));
            lbCartPageCount.Text = $"[{currentPage}/{totalPages}]";
        }

        private void DisplayPageData()
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, isbnList.Count);

            for (int i = 1; i <= itemsPerPage; i++)
            {
                if (i <= endIndex - startIndex)
                {
                    int dataIndex = startIndex + i - 1;
                    Controls[$"lbCartBookName{i}"].Text = titleList[dataIndex];
                    Controls[$"lbCartBookAuthor{i}"].Text = authorList[dataIndex];
                    Controls[$"lbCartBookGenre{i}"].Text = genreList[dataIndex];
                    Controls[$"lbCartBookText{i}"].Text = descriptionList[dataIndex];

                    if (File.Exists(coverImageList[dataIndex]))
                    {
                        (Controls[$"pbCartBookImage{i}"] as PictureBox).Image = Image.FromFile(coverImageList[dataIndex]);
                    }
                    else
                    {
                        (Controls[$"pbCartBookImage{i}"] as PictureBox).Image = null;
                    }
                }
                else
                {
                    ClearControlData(i);
                }
            }
        }

        private void ClearControlData(int index)
        {
            Controls[$"lbCartBookName{index}"].Text = string.Empty;
            Controls[$"lbCartBookAuthor{index}"].Text = string.Empty;
            Controls[$"lbCartBookGenre{index}"].Text = string.Empty;
            Controls[$"lbCartBookText{index}"].Text = string.Empty;
            (Controls[$"pbCartBookImage{index}"] as PictureBox).Image = null;
        }



        // 이전 페이지 버튼 클릭
        private void pbCartSkipLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePageCount();
                DisplayPageData();
            }
        }

        private void pbCartSkipRight_Click(object sender, EventArgs e)
        {
            int totalItems = isbnList.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)itemsPerPage));

            Console.WriteLine($"Current Page: {currentPage}");
            Console.WriteLine($"Total Pages: {totalPages}");

            if (currentPage < totalPages)
            {
                currentPage++;
                Console.WriteLine($"Page incremented to: {currentPage}");
                UpdatePageCount();
                DisplayPageData();
            }
            else
            {
                Console.WriteLine("Already at the last page.");
            }
        }

        private void pbCartRentButton_Click(object sender, EventArgs e)
        {
            // RentalReservationForm을 열 때 isbnList를 전달
            RentalReservationForm rentalForm = new RentalReservationForm(isbnList,memberId);
            rentalForm.ShowDialog();  // 모달 대화상자로 열기
        }

        private void pbCartMyPage_Click(object sender, EventArgs e)
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
                            // 관리자인 경우 AdminForm 열기
                            AdminForm adminForm = new AdminForm(memberId);
                            adminForm.ShowDialog();
                        }
                        else
                        {
                            // 일반 사용자인 경우 MyPageForm 열기
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
            //Yellow

            pbBackGround1.BackColor = Color.FromArgb(254, 255, 210);
            pbBackGround2.BackColor = Color.FromArgb(254, 255, 210);
            pbCartBackButton.BackColor = Color.FromArgb(254, 255, 210);
            pbCartRentButton.BackColor = Color.FromArgb(254, 255, 210);
            pbCart_.BackColor = Color.FromArgb(254, 255, 210);
            lbCartPageCount.BackColor = Color.FromArgb(254, 255, 210);
            pbCartSkipLeft.BackColor = Color.FromArgb(254, 255, 210);
            pbCartSkipRight.BackColor = Color.FromArgb(254, 255, 210);

            //Green

            pbCartLogo.BackColor = Color.FromArgb(197, 249, 214);
            pictureBox1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText1.BackColor = Color.FromArgb(197, 249, 214);
            lbLogoText2.BackColor = Color.FromArgb(197, 249, 214);
            pbCartMyPage.BackColor = Color.FromArgb(197, 249, 214);

            //Green Button

            lbCartBackButton.BackColor = Color.FromArgb(65, 158, 59);

            //Yellow Button

            lbCartRentButton.BackColor = Color.FromArgb(217, 217, 16);
        }

        private void pbCartBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
