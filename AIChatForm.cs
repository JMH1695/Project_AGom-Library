using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class AIChatForm : Form
    {
        private static readonly string OPENROUTER_API_KEY =
            "Your_API_KEY";

        private readonly int memberId;

        // Singleton HttpClient — 요청 시마다 매번 new 하지 않음
        private static readonly HttpClient http = new HttpClient();

        private List<BookInfo> searchResults = new List<BookInfo>();
        private int currentPage = 1;
        private int totalPage = 1;
        private const int booksPerPage = 4;

        public AIChatForm(int memberId)
        {
            this.memberId = memberId;
            InitializeComponent();
            color_Input();

            http.DefaultRequestHeaders.Clear();
            Console.WriteLine("[INIT] AIChatForm 시작됨.");

            rtbAIChat.AppendText("🤖 AI 어시스턴트에 오신 걸 환영합니다.\n도서 추천이나 정보가 필요하시면 물어보세요!\n\n");

            pbLeft.Click += pbLeft_Click;
            pbRight.Click += pbRight_Click;

            pbBookImage1.Click += pbBookImage_Click;
            pbBookImage2.Click += pbBookImage_Click;
            pbBookImage3.Click += pbBookImage_Click;
            pbBookImage4.Click += pbBookImage_Click;
        }

        private async void rtbUserChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                string userMessage = rtbUserChat.Text.Trim();
                if (string.IsNullOrEmpty(userMessage)) return;

                AppendUserMessage(userMessage);
                rtbUserChat.Clear();

                string aiResponse = await GenerateRAGResponse(userMessage);
                aiResponse = CleanResponse(aiResponse);

                AppendAIMessage(aiResponse);
            }
        }

        private void AppendUserMessage(string msg)
        {
            rtbAIChat.SelectionColor = Color.Blue;
            rtbAIChat.AppendText($"👤 사용자: {msg}\n");
            rtbAIChat.SelectionColor = Color.Black;
        }

        private void AppendAIMessage(string msg)
        {
            rtbAIChat.SelectionColor = Color.DarkGreen;
            rtbAIChat.AppendText($"🤖 AI: {msg}\n\n");
            rtbAIChat.SelectionColor = Color.Black;
            rtbAIChat.ScrollToCaret();
        }

        // 특수문자 정리
        private string CleanResponse(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            string cleaned = Regex.Replace(text,
                @"[^\uAC00-\uD7A3\u3131-\u318E\u1100-\u11FFa-zA-Z0-9\s.,!?\""\-()']",
                "");
            return cleaned.Trim();
        }

        // =========================
        //   RAG 전체 프로세스
        // =========================
        private async Task<string> GenerateRAGResponse(string userQuestion)
        {
            try
            {
                Console.WriteLine($"[RAG] 원문 질문: {userQuestion}");

                // 1) Intent 추출
                var intent = await ExtractSearchIntentAsync(userQuestion);

                Console.WriteLine($"[RAG] Intent 추출 완료: {intent.Type}, {intent.Keyword}");

                // 2) 인사 감지 → DB 검색 스킵
                string kwLower = intent.Keyword.Trim().ToLower();

                string[] greetings = {
            "안녕하세요", "안녕", "안뇽", "ㅎㅇ", "하이",
            "반가워", "반갑습니다", "hello", "hi", "hey",
            "안녕하세요!", "안녕!", "하이!"
        };

                if (Array.Exists(greetings, g => g == kwLower))
                {
                    Console.WriteLine("[RAG] 인사 감지 → DB 검색 건너뜀");

                    return "안녕하세요! 무엇을 도와드릴까요?\n" +
                           "도서 추천이나 검색을 원하시면 책 제목이나 작가명을 말씀해 주세요 😊";
                }

                // 3) DB 검색
                var relatedBooks = SearchBooksFromDB(intent);
                Console.WriteLine($"[RAG] DB 검색 결과 {relatedBooks.Count}개");

                // 4) 책 이미지/페이지 표시
                DisplayBookResults(relatedBooks);

                // 5) 프롬프트 구성 후 2차 LLM 호출
                string prompt = BuildPrompt(userQuestion, relatedBooks);

                Console.WriteLine("=== LLM Answer 프롬프트 ===");
                Console.WriteLine(prompt);
                Console.WriteLine("===========================");

                string aiAnswer = await QueryOpenRouterAnswerAsync(prompt);

                Console.WriteLine("[RAG] 2차 LLM 응답 수신");
                Console.WriteLine(aiAnswer);

                return aiAnswer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
                return $"⚠️ 오류 발생: {ex.Message}";
            }
        }


        // ======================================================
        //    1차 LLM → Intent(type, keyword) 추출
        // ======================================================
        private async Task<SearchIntent> ExtractSearchIntentAsync(string userQuestion)
        {
            try
            {
                http.DefaultRequestHeaders.Clear();
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + OPENROUTER_API_KEY);
                http.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/minhyeok-AGom");
                http.DefaultRequestHeaders.Add("X-Title", "AGom Library Intent Extractor");

                var payload = new
                {
                    model = "meta-llama/llama-4-maverick:free",
                    messages = new[]
                    {
                new {
                    role = "system",
                    content =
@"당신은 도서 검색용 질의 분석기입니다.
반드시 아래 JSON 형식만 출력하세요:

{
  ""type"": ""author"" | ""title"" | ""category"" | ""isbn"" | ""any"",
  ""keyword"": ""핵심 단어만""
}

불필요한 조사/부사는 제거하십시오."
                },
                new { role = "user", content = userQuestion }
            },
                    max_tokens = 200,
                    temperature = 0.1
                };

                string json = JsonConvert.SerializeObject(payload);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine("[INTENT] 요청 전송");

                var res = await http.PostAsync("https://openrouter.ai/api/v1/chat/completions", body);
                string raw = await res.Content.ReadAsStringAsync();

                Console.WriteLine("[INTENT RAW]");
                Console.WriteLine(raw);

                dynamic root = JsonConvert.DeserializeObject(raw);
                string content = root?.choices?[0]?.message?.content?.ToString();

                if (string.IsNullOrWhiteSpace(content))
                    return new SearchIntent { Type = "any", Keyword = userQuestion };

                // JSON 부분만 추출
                int s = content.IndexOf('{');
                int e = content.LastIndexOf('}');
                if (s < 0 || e < 0)
                    return new SearchIntent { Type = "any", Keyword = userQuestion };

                string pureJson = content.Substring(s, e - s + 1);

                Console.WriteLine("[INTENT JSON]");
                Console.WriteLine(pureJson);

                JObject obj = JObject.Parse(pureJson);

                string type = obj["type"]?.ToString()?.Trim() ?? "any";
                string keyword = obj["keyword"]?.ToString()?.Trim();

                if (string.IsNullOrEmpty(keyword))
                    keyword = userQuestion;


                // ==============================================================
                // 🔥 "작가 관련 질문" 자동 보정 (가장 중요한 부분)
                // ==============================================================

                string lowerQ = userQuestion.ToLower();

                bool isAskingAuthor =
                    lowerQ.Contains("작가") ||
                    lowerQ.Contains("쓴 사람") ||
                    lowerQ.Contains("누가 썼") ||
                    lowerQ.Contains("저자");

                if (isAskingAuthor)
                {
                    Console.WriteLine("[INTENT FIX] 작가 질문 감지 → type=any로 변환");

                    // keyword가 비어있거나 userQuestion 그대로면 타이틀 추출 시도
                    if (string.IsNullOrWhiteSpace(keyword) || keyword == userQuestion)
                    {
                        // 특수문자 제거
                        string cleaned = Regex.Replace(userQuestion, @"[^a-zA-Z가-힣0-9 ]", "").Trim();

                        // '작가' 단어 제거 후 제목 부분만 남기기
                        string[] words = cleaned.Split(' ');
                        List<string> remain = new List<string>();

                        foreach (var w in words)
                            if (!w.Contains("작가"))
                                remain.Add(w);

                        keyword = string.Join(" ", remain).Trim();
                    }

                    type = "any";  // 검색범위 전체로 확장
                }

                // ==============================================================
                // 🔥 반환
                // ==============================================================

                return new SearchIntent
                {
                    Type = type,
                    Keyword = keyword
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("[INTENT ERROR] " + ex.Message);

                return new SearchIntent
                {
                    Type = "any",
                    Keyword = userQuestion
                };
            }
        }


        // ============================
        //   SQL 검색
        // ============================
        private List<BookInfo> SearchBooksFromDB(SearchIntent intent)
        {
            var result = new List<BookInfo>();

            if (intent == null || string.IsNullOrEmpty(intent.Keyword))
                return result;

            string kw = intent.Keyword.Trim();
            string t = intent.Type.ToLower();

            string where = "";

            switch (t)
            {
                case "author":
                    where = "b.author LIKE '%' + @kw + '%'";
                    break;

                case "title":
                    where = "b.title LIKE '%' + @kw + '%'";
                    break;

                case "category":
                    where = "c.category_name LIKE '%' + @kw + '%'";
                    break;

                case "isbn":
                    where = "b.isbn LIKE '%' + @kw + '%'";
                    break;

                default:
                    where = @"
(b.title LIKE '%' + @kw + '%'
OR b.author LIKE '%' + @kw + '%'
OR c.category_name LIKE '%' + @kw + '%'
OR bd.description LIKE '%' + @kw + '%'
)";
                    break;
            }


            string sql = $@"
SELECT TOP 50
    b.isbn,
    b.title,
    b.author,
    ISNULL(c.category_name, N'미분류') AS category_name,
    ISNULL(bd.description, N'') AS description,
    ISNULL(b.cover_image, '') AS cover_image
FROM Books b
LEFT JOIN Categories c ON b.category_id = c.category_id
LEFT JOIN BookDetails bd ON b.isbn = bd.isbn
WHERE {where}
ORDER BY b.title;";

            using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw", kw);

                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        result.Add(new BookInfo
                        {
                            ISBN = rd["isbn"].ToString(),
                            Title = rd["title"].ToString(),
                            Author = rd["author"].ToString(),
                            Category = rd["category_name"].ToString(),
                            Description = rd["description"].ToString(),
                            CoverImage = rd["cover_image"].ToString()
                        });
                    }
                }
            }
            return result;
        }

        // ============================
        //   페이지 표시
        // ============================
        private void DisplayBookResults(List<BookInfo> list)
        {
            searchResults = list;
            totalPage = Math.Max(1, (int)Math.Ceiling(list.Count / (double)booksPerPage));
            currentPage = 1;

            DisplayPage();
        }

        private void DisplayPage()
        {
            PictureBox[] boxes = { pbBookImage1, pbBookImage2, pbBookImage3, pbBookImage4 };
            foreach (var pb in boxes) pb.Image = null;

            if (searchResults.Count == 0)
            {
                lbCount.Text = "[0/0]";
                return;
            }

            int start = (currentPage - 1) * booksPerPage;
            int end = Math.Min(start + booksPerPage, searchResults.Count);

            for (int i = start; i < end; i++)
            {
                int slot = i - start;
                string file = searchResults[i].CoverImage;

                try
                {
                    if (!string.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            boxes[slot].Image = Image.FromStream(fs);
                        }
                    }
                }
                catch
                {
                    boxes[slot].Image = null;
                }
            }

            lbCount.Text = $"[{currentPage}/{totalPage}]";
            pbLeft.Enabled = currentPage > 1;
            pbRight.Enabled = currentPage < totalPage;
        }

        private void pbLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage();
            }
        }

        private void pbRight_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPage)
            {
                currentPage++;
                DisplayPage();
            }
        }

        // ============================
        //   2차 LLM → 최종 답변
        // ============================
        private async Task<string> QueryOpenRouterAnswerAsync(string prompt)
        {
            try
            {
                http.DefaultRequestHeaders.Clear();
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + OPENROUTER_API_KEY);

                var payload = new
                {
                    model = "meta-llama/llama-4-maverick:free",
                    messages = new[]
                    {
                        new {
                            role = "system",
                            content =
"당신은 AGom 도서관리 시스템의 AI 사서입니다. 제공된 도서 목록만 사용하세요."
                        },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 400,
                    temperature = 0.1
                };

                string json = JsonConvert.SerializeObject(payload);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await http.PostAsync("https://openrouter.ai/api/v1/chat/completions", body);
                string raw = await res.Content.ReadAsStringAsync();

                dynamic root = JsonConvert.DeserializeObject(raw);
                return root?.choices?[0]?.message?.content?.ToString() ?? "(응답 없음)";
            }
            catch (Exception ex)
            {
                return "⚠ API 오류: " + ex.Message;
            }
        }

        // ============================
        //   프롬프트 생성
        // ============================
        private string BuildPrompt(string question, List<BookInfo> books)
        {
            var sb = new StringBuilder();
            sb.AppendLine("📚 데이터베이스 검색 결과:");

            if (books.Count == 0)
            {
                sb.AppendLine("검색 결과가 없습니다.");
                return sb.ToString();
            }

            foreach (var b in books)
            {
                sb.AppendLine($"제목: {b.Title}");
                sb.AppendLine($"저자: {b.Author}");
                sb.AppendLine($"장르: {b.Category}");
                sb.AppendLine($"설명: {b.Description}");
                sb.AppendLine();
            }

            sb.AppendLine($"사용자 질문: {question}");

            return sb.ToString();
        }

        private void color_Input()
        {
            lbMyPageBackButton.BackColor = Color.FromArgb(65, 158, 59);
        }

        private void pbMyPageBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbBookImage_Click(object sender, EventArgs e)
        {
            PictureBox[] boxes = { pbBookImage1, pbBookImage2, pbBookImage3, pbBookImage4 };
            int idx = Array.IndexOf(boxes, sender);
            int index = (currentPage - 1) * booksPerPage + idx;

            if (index < 0 || index >= searchResults.Count)
                return;

            string isbn = searchResults[index].ISBN;

            SelectBookForm sb = new SelectBookForm(isbn, memberId);
            sb.ShowDialog();
        }

        private void lbCount_Click(object sender, EventArgs e) { }
        private void pbBookImage1_Click(object sender, EventArgs e) => pbBookImage_Click(sender, e);
        private void pbBookImage2_Click(object sender, EventArgs e) => pbBookImage_Click(sender, e);
        private void pbBookImage3_Click(object sender, EventArgs e) => pbBookImage_Click(sender, e);
        private void pbBookImage4_Click(object sender, EventArgs e) => pbBookImage_Click(sender, e);
    }

    // ============================
    //   DTO
    // ============================
    public class BookInfo
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
    }

    public class SearchIntent
    {
        public string Type { get; set; }
        public string Keyword { get; set; }
    }
}
