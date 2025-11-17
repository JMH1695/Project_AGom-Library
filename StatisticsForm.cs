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
using System.Windows.Forms.DataVisualization.Charting;

namespace AGomProject
{
    public partial class StatisticsForm : Form
    {
        private int adminId;
        // ⚠️ 디자이너 오류 방지용 (실제 사용 X)
        private List<Label> selectedLabels = new List<Label>();

        public StatisticsForm(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;
            InitializeChart();
            LoadTimePeriods();
            LoadCategories();

            // 초기 상태 설정
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

            // 나이/성별 라벨 클릭 이벤트 등록
            lbl10M.Click += Label_Click;
            lbl10F.Click += Label_Click;
            lbl20M.Click += Label_Click;
            lbl20F.Click += Label_Click;
            lbl30M.Click += Label_Click;
            lbl30F.Click += Label_Click;
            lbl40M.Click += Label_Click;
            lbl40F.Click += Label_Click;
            lbl50M.Click += Label_Click;
            lbl50F.Click += Label_Click;
            lbl60M.Click += Label_Click;
            lbl60F.Click += Label_Click;
        }

        private void InitializeChart()
        {
            if (!chartBooks.ChartAreas.Any(ca => ca.Name == "ChartArea1"))
            {
                ChartArea chartArea = new ChartArea("ChartArea1");
                chartArea.AxisX.MajorGrid.LineWidth = 0;
                chartArea.AxisX.MinorGrid.LineWidth = 0;
                chartArea.AxisY.MajorGrid.LineWidth = 0;
                chartArea.AxisY.MinorGrid.LineWidth = 0;
                chartBooks.ChartAreas.Add(chartArea);
            }

            if (!chartBooks.Legends.Any(l => l.Name == "Legend1"))
            {
                Legend legend = new Legend("Legend1");
                chartBooks.Legends.Add(legend);
            }

            if (!chartBooks.Titles.Any(t => t.Text == ""))
            {
                Title title = new Title("");
                chartBooks.Titles.Add(title);
            }

            if (!chartBooks.Series.Any(s => s.Name == "Books"))
            {
                Series series = new Series
                {
                    Name = "Books",
                    ChartType = SeriesChartType.Column,
                    XValueType = ChartValueType.String,
                    YValueType = ChartValueType.Int32
                };
                chartBooks.Series.Add(series);
            }
        }

        private void LoadTimePeriods()
        {
            cboTimePeriod.Items.AddRange(new string[] { "1개월", "3개월", "6개월", "12개월", "직접 입력" });
            cboTimePeriod.SelectedIndex = 2;
        }

        private void btnLoadStatistics_Click(object sender, EventArgs e)
        {
            DateTime startDate;
            DateTime endDate = DateTime.Today;

            switch (cboTimePeriod.SelectedItem.ToString())
            {
                case "1개월":
                    startDate = endDate.AddMonths(-1);
                    break;
                case "3개월":
                    startDate = endDate.AddMonths(-3);
                    break;
                case "6개월":
                    startDate = endDate.AddMonths(-6);
                    break;
                case "12개월":
                    startDate = endDate.AddMonths(-12);
                    break;
                case "직접 입력":
                    startDate = dtpStartDate.Value;
                    endDate = dtpEndDate.Value;
                    break;
                default:
                    startDate = endDate.AddMonths(-1);
                    break;
            }

            LoadStatistics(startDate, endDate);
        }

        private void LoadStatistics(DateTime startDate, DateTime endDate)
        {
            string baseQuery = @"
                SELECT b.title, COUNT(*) AS borrow_count
                FROM Borrows br
                JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                JOIN Books b ON lc.isbn = b.isbn
                JOIN Categories c ON b.category_id = c.category_id
                JOIN Members m ON br.member_id = m.member_id
                WHERE br.borrow_date BETWEEN @startDate AND @endDate
                AND (@category = '전체' OR c.category_name = @category)";

            // 선택된 라벨이 있다면 조건 추가
            if (selectedLabels.Count > 0)
            {
                baseQuery += " AND (";
                List<string> conditions = new List<string>();

                foreach (Label lbl in selectedLabels)
                {
                    string age = lbl.Name.Substring(3, 2);
                    string gender = lbl.Name.EndsWith("M") ? "남자" : "여자";

                    string condition = $"(m.gender = '{gender}' AND DATEDIFF(YEAR, m.birth_date, GETDATE()) ";
                    if (age == "60")
                        condition += ">= 60)";
                    else
                        condition += $"BETWEEN {age} AND {int.Parse(age) + 9})";

                    conditions.Add(condition);
                }

                baseQuery += string.Join(" OR ", conditions) + ")";
            }

            baseQuery += @" GROUP BY b.title
                            ORDER BY borrow_count DESC";

            using (SqlConnection dbConnection = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
            {
                SqlCommand command = new SqlCommand(baseQuery, dbConnection);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.Parameters.AddWithValue("@category", cboCategoryFilter.SelectedItem.ToString());

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                chartBooks.Series["Books"].Points.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    string title = row["title"].ToString();
                    int borrowCount = Convert.ToInt32(row["borrow_count"]);
                    chartBooks.Series["Books"].Points.AddXY(title, borrowCount);
                }
                chartBooks.Invalidate();
            }
        }

        private void cboTimePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimePeriod.SelectedItem.ToString() == "직접 입력")
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
        }

        private void LoadCategories()
        {
            using (SqlConnection dbConnection = new SqlConnection(DatabaseConfig.ConnectionString)) // ✅ 변경됨
            {
                string query = "SELECT category_name FROM Categories ORDER BY category_name";
                SqlCommand command = new SqlCommand(query, dbConnection);
                dbConnection.Open();

                cboCategoryFilter.Items.Add("전체");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cboCategoryFilter.Items.Add(reader["category_name"].ToString());
                    }
                }

                cboCategoryFilter.SelectedIndex = 0;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;

            if (selectedLabels.Contains(clickedLabel))
            {
                selectedLabels.Remove(clickedLabel);
                clickedLabel.BackColor = SystemColors.Control;
            }
            else
            {
                selectedLabels.Add(clickedLabel);
                clickedLabel.BackColor = Color.LightBlue;
            }
        }

        private void lblCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblBookTrend_Click(object sender, EventArgs e)
        {
            BookBorrowTrendForm bookTrendForm = new BookBorrowTrendForm();
            bookTrendForm.ShowDialog();
        }
    }
}
