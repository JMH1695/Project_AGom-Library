using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AGomProject
{
    public partial class EbookViewerForm : Form
    {
        private string ebookPath;
        private string coverPath;
        private int currentSpread = 0;  // 0: 표지, 1: 1-2페이지, 2: 3-4페이지...
        private int totalPages;
        private PictureBox pbLeftPage;
        private PictureBox pbRightPage;
        private Label lblPageInfo;
        private List<string> allPages;  // 표지와 모든 페이지를 포함하는 리스트

        public EbookViewerForm(string isbn)
        {
            InitializeComponent();

            // default E-book 경로 설정 및 확인
            string defaultEbookPath = Path.Combine(Application.StartupPath, "EBooks", "default");
            if (!Directory.Exists(defaultEbookPath))
            {
                Directory.CreateDirectory(defaultEbookPath);
                using (Bitmap bmp = new Bitmap(800, 1000))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    using (Font font = new Font("Arial", 20))
                    {
                        string text = "No E-Book Content Available";
                        SizeF textSize = g.MeasureString(text, font);
                        g.DrawString(text, font, Brushes.Black,
                            (800 - textSize.Width) / 2,
                            (1000 - textSize.Height) / 2);
                    }
                    bmp.Save(Path.Combine(defaultEbookPath, "page_001.jpg"), ImageFormat.Jpeg);
                }
            }

            // 실제 경로 설정
            ebookPath = Path.Combine(Application.StartupPath, "EBooks", isbn);
            coverPath = Path.Combine(Application.StartupPath, "BookCovers", isbn, "cover" + GetCoverExtension(isbn));

            // E-book 내용이 없으면 default 사용
            if (!Directory.Exists(ebookPath) || !Directory.GetFiles(ebookPath, "page_*").Any())
            {
                ebookPath = defaultEbookPath;
            }

            // 모든 페이지 리스트 생성 (표지 + 내용 페이지들)
            allPages = new List<string>();
            if (File.Exists(coverPath))
            {
                allPages.Add(coverPath);
            }

            // 내용 페이지들 추가 (정렬된 상태로)
            var contentPages = Directory.GetFiles(ebookPath, "page_*")
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                              file.ToLower().EndsWith(".jpeg") ||
                              file.ToLower().EndsWith(".png"))
                .OrderBy(file => file)
                .ToList();

            allPages.AddRange(contentPages);
            totalPages = allPages.Count;
            InitializeViewer();
            LoadSpread(currentSpread);
        }

        private string GetCoverExtension(string isbn)
        {
            // 가능한 확장자들을 순차적으로 확인
            string[] possibleExtensions = { ".png", ".jpg", ".jpeg" };
            foreach (var ext in possibleExtensions)
            {
                string testPath = Path.Combine(Application.StartupPath, "BookCovers", isbn, "cover" + ext);
                if (File.Exists(testPath))
                {
                    return ext;
                }
            }
            return ".png"; // 기본값
        }

        private void InitializeViewer()
        {
            this.Size = new Size(1200, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            pbLeftPage = new PictureBox
            {
                Size = new Size(500, 500),
                Location = new Point(50, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand
            };
            pbLeftPage.Click += PbLeftPage_Click;

            pbRightPage = new PictureBox
            {
                Size = new Size(500, 500),
                Location = new Point(600, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand
            };
            pbRightPage.Click += PbRightPage_Click;

            lblPageInfo = new Label
            {
                AutoSize = true,
                Location = new Point(550, 525),
                Font = new Font(this.Font.FontFamily, 12)
            };

            this.Controls.AddRange(new Control[] { pbLeftPage, pbRightPage, lblPageInfo });
        }

        private void LoadSpread(int spreadIndex)
        {
            if (pbLeftPage.Image != null)
            {
                pbLeftPage.Image.Dispose();
                pbLeftPage.Image = null;
            }
            if (pbRightPage.Image != null)
            {
                pbRightPage.Image.Dispose();
                pbRightPage.Image = null;
            }

            // 표지 (첫 번째 spread)
            if (spreadIndex == 0)
            {
                pbLeftPage.Visible = false;
                pbRightPage.Visible = true;

                if (allPages.Count > 0)
                {
                    using (var stream = new FileStream(allPages[0], FileMode.Open, FileAccess.Read))
                    {
                        pbRightPage.Image = new Bitmap(stream);
                    }
                }
                lblPageInfo.Text = "표지";
            }
            else
            {
                pbLeftPage.Visible = true;
                pbRightPage.Visible = true;

                // 실제 페이지 인덱스 계산 (표지 다음부터 시작)
                int leftPageIndex = (spreadIndex * 2) - 1;
                int rightPageIndex = leftPageIndex + 1;

                // 좌측 페이지 로드
                if (leftPageIndex < totalPages)
                {
                    using (var stream = new FileStream(allPages[leftPageIndex], FileMode.Open, FileAccess.Read))
                    {
                        pbLeftPage.Image = new Bitmap(stream);
                    }
                }

                // 우측 페이지 로드
                if (rightPageIndex < totalPages)
                {
                    using (var stream = new FileStream(allPages[rightPageIndex], FileMode.Open, FileAccess.Read))
                    {
                        pbRightPage.Image = new Bitmap(stream);
                    }
                }

                // 페이지 번호 표시 (표지는 페이지 번호에서 제외)
                int leftDisplayPage = leftPageIndex;
                int rightDisplayPage = Math.Min(rightPageIndex, totalPages - 1);
                lblPageInfo.Text = $"{leftDisplayPage}-{rightDisplayPage}/{totalPages - 1}";
            }
        }

        private void PbLeftPage_Click(object sender, EventArgs e)
        {
            if (currentSpread > 0)
            {
                currentSpread--;
                LoadSpread(currentSpread);
            }
        }

        private void PbRightPage_Click(object sender, EventArgs e)
        {
            int maxSpread = (totalPages + 1) / 2;  // 표지를 포함한 최대 spread 수
            if (currentSpread < maxSpread - 1)  // 마지막 spread 이전인 경우
            {
                currentSpread++;
                LoadSpread(currentSpread);
            }
            else if (currentSpread == maxSpread - 1)  // 마지막 spread인 경우
            {
                MessageBox.Show("마지막 페이지입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (pbLeftPage.Image != null)
            {
                pbLeftPage.Image.Dispose();
            }
            if (pbRightPage.Image != null)
            {
                pbRightPage.Image.Dispose();
            }
        }
    }
}