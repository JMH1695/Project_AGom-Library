namespace AGomProject
{
    partial class LibraryInventoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvLibraries = new System.Windows.Forms.DataGridView();
            this.library_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.library_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.library_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLibrarySearch = new System.Windows.Forms.TextBox();
            this.txtIsbn = new System.Windows.Forms.TextBox();
            this.btnAddStock = new System.Windows.Forms.Button();
            this.btnDecreaseStock = new System.Windows.Forms.Button();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCancle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibraries)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLibraries
            // 
            this.dgvLibraries.AllowUserToAddRows = false;
            this.dgvLibraries.AllowUserToDeleteRows = false;
            this.dgvLibraries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLibraries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.library_id,
            this.library_name,
            this.library_address});
            this.dgvLibraries.Location = new System.Drawing.Point(89, 86);
            this.dgvLibraries.Name = "dgvLibraries";
            this.dgvLibraries.ReadOnly = true;
            this.dgvLibraries.RowTemplate.Height = 23;
            this.dgvLibraries.Size = new System.Drawing.Size(496, 150);
            this.dgvLibraries.TabIndex = 0;
            // 
            // library_id
            // 
            this.library_id.DataPropertyName = "library_id";
            this.library_id.HeaderText = "도서관 ID";
            this.library_id.Name = "library_id";
            this.library_id.ReadOnly = true;
            this.library_id.Width = 80;
            // 
            // library_name
            // 
            this.library_name.DataPropertyName = "library_name";
            this.library_name.HeaderText = "도서관 이름";
            this.library_name.Name = "library_name";
            this.library_name.ReadOnly = true;
            this.library_name.Width = 150;
            // 
            // library_address
            // 
            this.library_address.DataPropertyName = "library_address";
            this.library_address.HeaderText = "주소";
            this.library_address.Name = "library_address";
            this.library_address.ReadOnly = true;
            this.library_address.Width = 220;
            // 
            // txtLibrarySearch
            // 
            this.txtLibrarySearch.Location = new System.Drawing.Point(89, 45);
            this.txtLibrarySearch.Name = "txtLibrarySearch";
            this.txtLibrarySearch.Size = new System.Drawing.Size(240, 21);
            this.txtLibrarySearch.TabIndex = 1;
            // 
            // txtIsbn
            // 
            this.txtIsbn.Location = new System.Drawing.Point(89, 242);
            this.txtIsbn.Name = "txtIsbn";
            this.txtIsbn.Size = new System.Drawing.Size(240, 21);
            this.txtIsbn.TabIndex = 2;
            // 
            // btnAddStock
            // 
            this.btnAddStock.Location = new System.Drawing.Point(172, 302);
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(75, 23);
            this.btnAddStock.TabIndex = 3;
            this.btnAddStock.Text = "수량 추가";
            this.btnAddStock.UseVisualStyleBackColor = true;
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // btnDecreaseStock
            // 
            this.btnDecreaseStock.Location = new System.Drawing.Point(254, 302);
            this.btnDecreaseStock.Name = "btnDecreaseStock";
            this.btnDecreaseStock.Size = new System.Drawing.Size(75, 23);
            this.btnDecreaseStock.TabIndex = 4;
            this.btnDecreaseStock.Text = "수량 감소";
            this.btnDecreaseStock.UseVisualStyleBackColor = true;
            this.btnDecreaseStock.Click += new System.EventHandler(this.btnDecreaseStock_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(89, 302);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(77, 21);
            this.txtQuantity.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(341, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCancle
            // 
            this.lblCancle.AutoSize = true;
            this.lblCancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblCancle.Font = new System.Drawing.Font("휴먼둥근헤드라인", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCancle.Location = new System.Drawing.Point(381, 387);
            this.lblCancle.Name = "lblCancle";
            this.lblCancle.Padding = new System.Windows.Forms.Padding(10);
            this.lblCancle.Size = new System.Drawing.Size(125, 42);
            this.lblCancle.TabIndex = 7;
            this.lblCancle.Text = "뒤로 가기";
            this.lblCancle.Click += new System.EventHandler(this.lblCancle_Click);
            // 
            // LibraryInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.lblCancle);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.btnDecreaseStock);
            this.Controls.Add(this.btnAddStock);
            this.Controls.Add(this.txtIsbn);
            this.Controls.Add(this.txtLibrarySearch);
            this.Controls.Add(this.dgvLibraries);
            this.Name = "LibraryInventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LibraryInventoryForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibraries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLibraries;
        private System.Windows.Forms.TextBox txtLibrarySearch;
        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.Button btnDecreaseStock;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCancle;
        private System.Windows.Forms.DataGridViewTextBoxColumn library_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn library_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn library_address;
    }
}