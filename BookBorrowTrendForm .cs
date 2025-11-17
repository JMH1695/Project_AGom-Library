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
    public partial class BookBorrowTrendForm : Form
    {
        private int adminId;
        private string selectedBookTitle = null; // 선택된 도서의 ISBN 저장

        public BookBorrowTrendForm()
        {
            InitializeComponent();
            dgvBookSearch.AutoGenerateColumns = false;
            InitializeChart();
            LoadTimePeriods();
            InitializeReportComponents();

            // 초기 상태 설정
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void InitializeChart()
        {
            if (!chartBookTrend.ChartAreas.Any(ca => ca.Name == "ChartArea1"))
            {
                ChartArea chartArea = new ChartArea("ChartArea1");
                chartArea.AxisX.MajorGrid.LineWidth = 0;
                chartArea.AxisX.MinorGrid.LineWidth = 0;
                chartArea.AxisY.MajorGrid.LineWidth = 0;
                chartArea.AxisY.MinorGrid.LineWidth = 0;
                chartBookTrend.ChartAreas.Add(chartArea);
            }

            if (!chartBookTrend.Legends.Any(l => l.Name == "Legend1"))
            {
                Legend legend = new Legend("Legend1");
                chartBookTrend.Legends.Add(legend);
            }

            if (!chartBookTrend.Titles.Any(t => t.Text == "Book Borrow Trend"))
            {
                Title title = new Title("Book Borrow Trend");
                chartBookTrend.Titles.Add(title);
            }

            if (!chartBookTrend.Series.Any(s => s.Name == "BookTrend"))
            {
                Series series = new Series
                {
                    Name = "BookTrend",
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.DateTime,
                    YValueType = ChartValueType.Int32,
                    BorderWidth = 3,
                    Color = System.Drawing.Color.Blue
                };
                chartBookTrend.Series.Add(series);
            }
        }

        private void LoadTimePeriods()
        {
            cboTimePeriod.Items.AddRange(new string[] { "1개월", "3개월", "6개월", "12개월", "직접 입력" });
            cboTimePeriod.SelectedIndex = 2;
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

        private (DateTime startDate, DateTime endDate) GetSelectedPeriod()
        {
            DateTime endDate = DateTime.Today;
            DateTime startDate;

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

            return (startDate, endDate);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookSearch.Text))
            {
                MessageBox.Show("도서명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string searchQuery = @"
                SELECT DISTINCT 
                    b.title,
                    b.author,
                    c.category_name
                FROM Books b
                JOIN Categories c ON b.category_id = c.category_id
                WHERE b.title LIKE @searchText + '%'
                ORDER BY b.title";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(searchQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@searchText", txtBookSearch.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvBookSearch.DataSource = dt;
                }
            }
        }

        private void LoadBookTrendData(string title, DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT 
                    br.borrow_date,
                    COUNT(*) as borrow_count
                FROM Borrows br
                JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
                JOIN Books b ON lc.isbn = b.isbn
                WHERE b.title = @title AND br.borrow_date BETWEEN @startDate AND @endDate
                GROUP BY br.borrow_date
                ORDER BY br.borrow_date";

            using (SqlConnection dbConnection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                dbConnection.Open();

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<DateTime> dateRange = new List<DateTime>();
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    dateRange.Add(date);
                }

                chartBookTrend.Series["BookTrend"].Points.Clear();

                foreach (DateTime date in dateRange)
                {
                    DataRow[] rows = dataTable.Select($"borrow_date = '{date:yyyy-MM-dd}'");

                    int borrowCount = 0;
                    if (rows.Length > 0)
                    {
                        borrowCount = Convert.ToInt32(rows[0]["borrow_count"]);
                    }

                    chartBookTrend.Series["BookTrend"].Points.AddXY(date, borrowCount);
                }

                chartBookTrend.Invalidate();
            }
        }

        private void lblCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvBookSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBookSearch.CurrentRow != null)
            {
                selectedBookTitle = dgvBookSearch.CurrentRow.Cells[0].Value?.ToString();
                btnShowTrend.Enabled = true;
            }
        }

        private void btnShowTrend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedBookTitle))
            {
                var (startDate, endDate) = GetSelectedPeriod();
                LoadBookTrendData(selectedBookTitle, startDate, endDate);
            }
            else
            {
                MessageBox.Show("먼저 책을 선택하세요.");
            }
        }

        private void InitializeReportComponents()
        {
            cboReportTimePeriod.Items.AddRange(new string[] { "1개월", "3개월", "6개월", "12개월" });
            cboReportTimePeriod.SelectedIndex = 2;
        }

        private (DateTime startDate, DateTime endDate) GetReportSelectedPeriod()
        {
            DateTime endDate = DateTime.Today;
            DateTime startDate;

            switch (cboReportTimePeriod.SelectedItem.ToString())
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
                default:
                    startDate = endDate.AddMonths(-1);
                    break;
            }

            return (startDate, endDate);
        }

        private void cboReportTimePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = cboReportTimePeriod.SelectedItem != null;
            dtpEndDate.Enabled = cboReportTimePeriod.SelectedItem != null;
        }

        private void btnCheckAvailability_Click(object sender, EventArgs e)
        {
            var (startDate, endDate) = GetReportSelectedPeriod();

            string query = @"
                SELECT 
                    b.title AS BookTitle,
                    l.library_name AS LibraryName,
                    COUNT(*) AS UnavailableCount
                FROM BookStockHistory bsh
                JOIN LibraryCollections lc ON bsh.library_collection_id = lc.library_collection_id
                JOIN Books b ON lc.isbn = b.isbn
                JOIN Libraries l ON lc.library_id = l.library_id
                WHERE bsh.quantity = 0 
                  AND bsh.record_date BETWEEN @startDate AND @endDate
                GROUP BY b.title, l.library_name
                ORDER BY UnavailableCount DESC";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable resultTable = new DataTable();
                adapter.Fill(resultTable);

                dgvBookAvailability.DataSource = resultTable;
            }
        }
    }
}
