using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class AdminForm : Form
    {
        private int adminId;  // 관리자 ID 저장 변수

        public AdminForm(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;

            color_Input();
        }

        private void color_Input()
        {
            pbLogo.BackColor = Color.FromArgb(197, 249, 214);
            lbTitle.BackColor = Color.FromArgb(197, 249, 214);
        }

        // 기존도서수정 클릭 이벤트
        private void pbEditBook_Click(object sender, EventArgs e)
        {
            EditBookForm editBookForm = new EditBookForm(adminId);
            editBookForm.ShowDialog();
        }

        // 신규도서추가 클릭 이벤트
        private void pbNewBook_Click(object sender, EventArgs e)
        {
            NewBookForm newBookForm = new NewBookForm(adminId);
            newBookForm.ShowDialog();
        }

        private void pbStatistics_Click(object sender, EventArgs e)
        {
            StatisticsForm statisticsForm = new StatisticsForm(adminId);
            statisticsForm.ShowDialog();
        }

        private void pbLibraryManage_Click(object sender, EventArgs e)
        {
            LibraryManagementForm libraryManagementForm = new LibraryManagementForm(adminId);
            libraryManagementForm.ShowDialog();
        }

        private void lblCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
