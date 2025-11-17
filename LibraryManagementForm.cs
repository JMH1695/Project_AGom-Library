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
    public partial class LibraryManagementForm : Form
    {
        private int adminId;
        // ⚠️ 디자이너 오류 방지용 (실제 사용 안 함)
        public LibraryManagementForm(int adminId)
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

        private void pbMemberManagement_Click(object sender, EventArgs e)
        {
            // ✅ 전역 DatabaseConfig 사용 (내부에서 DB 접근 시에도 동일하게 적용됨)
            MemberManagementForm memberManagementForm = new MemberManagementForm(adminId);
            memberManagementForm.ShowDialog();
        }

        private void pbLoanStatus_Click(object sender, EventArgs e)
        {
            // ✅ 전역 DatabaseConfig 사용
            LoanStatusForm loanStatusForm = new LoanStatusForm(adminId);
            loanStatusForm.ShowDialog();
        }

        private void lblCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
