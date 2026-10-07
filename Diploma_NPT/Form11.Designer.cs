namespace Diploma_NPT
{
    partial class formReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formReport));
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.goods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number_sales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.cbCategoryGoods = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnReportResult = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.pbPrint = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pbCart = new System.Windows.Forms.PictureBox();
            this.pbLanguageUS = new System.Windows.Forms.PictureBox();
            this.pbLanguageUA = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbMenu = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiHome = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPizza = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCombo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBurger = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSnack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrink = new System.Windows.Forms.ToolStripMenuItem();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLanguageUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLanguageUA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.msMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReport
            // 
            this.dgvReport.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.goods,
            this.number_sales,
            this.total_price});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReport.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReport.Location = new System.Drawing.Point(43, 168);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 24;
            this.dgvReport.Size = new System.Drawing.Size(1160, 643);
            this.dgvReport.TabIndex = 272;
            // 
            // goods
            // 
            this.goods.HeaderText = "Товар";
            this.goods.MinimumWidth = 6;
            this.goods.Name = "goods";
            this.goods.ReadOnly = true;
            this.goods.Width = 459;
            // 
            // number_sales
            // 
            this.number_sales.HeaderText = "К-ть продажів";
            this.number_sales.MinimumWidth = 6;
            this.number_sales.Name = "number_sales";
            this.number_sales.ReadOnly = true;
            this.number_sales.Width = 174;
            // 
            // total_price
            // 
            this.total_price.HeaderText = "Загальна ціна";
            this.total_price.MinimumWidth = 6;
            this.total_price.Name = "total_price";
            this.total_price.ReadOnly = true;
            this.total_price.Width = 185;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Purple;
            this.label7.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label7.Location = new System.Drawing.Point(1240, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(205, 28);
            this.label7.TabIndex = 271;
            this.label7.Text = "Категорія товарів:";
            // 
            // cbCategoryGoods
            // 
            this.cbCategoryGoods.FormattingEnabled = true;
            this.cbCategoryGoods.Location = new System.Drawing.Point(1245, 519);
            this.cbCategoryGoods.Name = "cbCategoryGoods";
            this.cbCategoryGoods.Size = new System.Drawing.Size(224, 24);
            this.cbCategoryGoods.TabIndex = 270;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Purple;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label1.Location = new System.Drawing.Point(1246, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 84);
            this.label1.TabIndex = 269;
            this.label1.Text = "Для перегляду\nпродажів за\nвказаний період.";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Purple;
            this.btnUpdate.Location = new System.Drawing.Point(1245, 664);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(224, 51);
            this.btnUpdate.TabIndex = 268;
            this.btnUpdate.Text = "Оновлення";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnReportResult
            // 
            this.btnReportResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.btnReportResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportResult.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReportResult.ForeColor = System.Drawing.Color.Purple;
            this.btnReportResult.Location = new System.Drawing.Point(1245, 598);
            this.btnReportResult.Name = "btnReportResult";
            this.btnReportResult.Size = new System.Drawing.Size(224, 51);
            this.btnReportResult.TabIndex = 267;
            this.btnReportResult.Text = "Результат";
            this.btnReportResult.UseVisualStyleBackColor = false;
            this.btnReportResult.Click += new System.EventHandler(this.btnReportResult_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(1245, 261);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 266;
            // 
            // pbPrint
            // 
            this.pbPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPrint.Image = ((System.Drawing.Image)(resources.GetObject("pbPrint.Image")));
            this.pbPrint.Location = new System.Drawing.Point(1313, 732);
            this.pbPrint.Name = "pbPrint";
            this.pbPrint.Size = new System.Drawing.Size(88, 79);
            this.pbPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPrint.TabIndex = 265;
            this.pbPrint.TabStop = false;
            this.pbPrint.Click += new System.EventHandler(this.pbPrint_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Purple;
            this.pictureBox4.Location = new System.Drawing.Point(25, 150);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1485, 688);
            this.pictureBox4.TabIndex = 264;
            this.pictureBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Purple;
            this.label3.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label3.Location = new System.Drawing.Point(525, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 28);
            this.label3.TabIndex = 263;
            this.label3.Text = "Замовити по телефону";
            // 
            // pbCart
            // 
            this.pbCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.pbCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCart.Image = global::Diploma_NPT.Properties.Resources._8_cart__3_;
            this.pbCart.Location = new System.Drawing.Point(1276, 21);
            this.pbCart.Name = "pbCart";
            this.pbCart.Size = new System.Drawing.Size(93, 75);
            this.pbCart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCart.TabIndex = 262;
            this.pbCart.TabStop = false;
            this.pbCart.Click += new System.EventHandler(this.pbCart_Click);
            // 
            // pbLanguageUS
            // 
            this.pbLanguageUS.BackColor = System.Drawing.Color.Purple;
            this.pbLanguageUS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLanguageUS.Image = global::Diploma_NPT.Properties.Resources._4_angl;
            this.pbLanguageUS.Location = new System.Drawing.Point(901, 18);
            this.pbLanguageUS.Name = "pbLanguageUS";
            this.pbLanguageUS.Size = new System.Drawing.Size(81, 80);
            this.pbLanguageUS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLanguageUS.TabIndex = 261;
            this.pbLanguageUS.TabStop = false;
            // 
            // pbLanguageUA
            // 
            this.pbLanguageUA.BackColor = System.Drawing.Color.Purple;
            this.pbLanguageUA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLanguageUA.Image = global::Diploma_NPT.Properties.Resources._3_ukr;
            this.pbLanguageUA.Location = new System.Drawing.Point(813, 18);
            this.pbLanguageUA.Name = "pbLanguageUA";
            this.pbLanguageUA.Size = new System.Drawing.Size(81, 80);
            this.pbLanguageUA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLanguageUA.TabIndex = 260;
            this.pbLanguageUA.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(1155, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 22);
            this.label6.TabIndex = 259;
            this.label6.Text = "0 грн";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(1043, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 22);
            this.label5.TabIndex = 258;
            this.label5.Text = "0 шт";
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.pictureBox10.Location = new System.Drawing.Point(1028, 20);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(344, 76);
            this.pictureBox10.TabIndex = 257;
            this.pictureBox10.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Purple;
            this.label4.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label4.Location = new System.Drawing.Point(301, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 34);
            this.label4.TabIndex = 256;
            this.label4.Text = "08:00-22:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Purple;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label2.Location = new System.Drawing.Point(553, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 34);
            this.label2.TabIndex = 255;
            this.label2.Text = "099 999 99 99";
            // 
            // pbMenu
            // 
            this.pbMenu.BackColor = System.Drawing.Color.Purple;
            this.pbMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMenu.Image = global::Diploma_NPT.Properties.Resources._5_menu;
            this.pbMenu.Location = new System.Drawing.Point(1407, 34);
            this.pbMenu.Name = "pbMenu";
            this.pbMenu.Size = new System.Drawing.Size(103, 49);
            this.pbMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMenu.TabIndex = 254;
            this.pbMenu.TabStop = false;
            this.pbMenu.Click += new System.EventHandler(this.pbMenu_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Purple;
            this.pictureBox6.Image = global::Diploma_NPT.Properties.Resources._2_clock;
            this.pictureBox6.Location = new System.Drawing.Point(239, 30);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(69, 62);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 253;
            this.pictureBox6.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Purple;
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLogo.Image = global::Diploma_NPT.Properties.Resources._1_logo;
            this.pbLogo.Location = new System.Drawing.Point(12, 10);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(232, 105);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 252;
            this.pbLogo.TabStop = false;
            this.pbLogo.Click += new System.EventHandler(this.pbLogo_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Purple;
            this.pictureBox3.Location = new System.Drawing.Point(-6, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1545, 118);
            this.pictureBox3.TabIndex = 251;
            this.pictureBox3.TabStop = false;
            // 
            // msMainMenu
            // 
            this.msMainMenu.AutoSize = false;
            this.msMainMenu.BackColor = System.Drawing.Color.Purple;
            this.msMainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.msMainMenu.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.msMainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHome,
            this.tsmiPizza,
            this.tsmiCombo,
            this.tsmiBurger,
            this.tsmiSnack,
            this.tsmiDrink});
            this.msMainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.msMainMenu.Location = new System.Drawing.Point(1379, 86);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(157, 332);
            this.msMainMenu.TabIndex = 274;
            this.msMainMenu.Visible = false;
            // 
            // tsmiHome
            // 
            this.tsmiHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiHome.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.tsmiHome.Name = "tsmiHome";
            this.tsmiHome.Size = new System.Drawing.Size(145, 35);
            this.tsmiHome.Text = "На головну";
            this.tsmiHome.Click += new System.EventHandler(this.tsmiHome_Click);
            // 
            // tsmiPizza
            // 
            this.tsmiPizza.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiPizza.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsmiPizza.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiPizza.Name = "tsmiPizza";
            this.tsmiPizza.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsmiPizza.Size = new System.Drawing.Size(145, 35);
            this.tsmiPizza.Text = "Піци";
            this.tsmiPizza.Click += new System.EventHandler(this.tsmiPizza_Click);
            // 
            // tsmiCombo
            // 
            this.tsmiCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiCombo.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiCombo.Name = "tsmiCombo";
            this.tsmiCombo.Size = new System.Drawing.Size(145, 35);
            this.tsmiCombo.Text = "Комбо";
            this.tsmiCombo.Click += new System.EventHandler(this.tsmiCombo_Click);
            // 
            // tsmiBurger
            // 
            this.tsmiBurger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiBurger.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiBurger.Name = "tsmiBurger";
            this.tsmiBurger.Size = new System.Drawing.Size(145, 35);
            this.tsmiBurger.Text = "Бургери";
            this.tsmiBurger.Click += new System.EventHandler(this.tsmiBurger_Click);
            // 
            // tsmiSnack
            // 
            this.tsmiSnack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiSnack.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiSnack.Name = "tsmiSnack";
            this.tsmiSnack.Size = new System.Drawing.Size(145, 35);
            this.tsmiSnack.Text = "Снеки";
            this.tsmiSnack.Click += new System.EventHandler(this.tsmiSnack_Click);
            // 
            // tsmiDrink
            // 
            this.tsmiDrink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiDrink.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiDrink.Name = "tsmiDrink";
            this.tsmiDrink.Size = new System.Drawing.Size(145, 35);
            this.tsmiDrink.Text = "Напої";
            this.tsmiDrink.Click += new System.EventHandler(this.tsmiDrink_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // formReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(1535, 903);
            this.Controls.Add(this.msMainMenu);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbCategoryGoods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnReportResult);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.pbPrint);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbCart);
            this.Controls.Add(this.pbLanguageUS);
            this.Controls.Add(this.pbLanguageUA);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbMenu);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.pictureBox3);
            this.Name = "formReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Звіт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formReport_FormClosing);
            this.Load += new System.EventHandler(this.formReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLanguageUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLanguageUA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCategoryGoods;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnReportResult;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.PictureBox pbPrint;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbCart;
        private System.Windows.Forms.PictureBox pbLanguageUS;
        private System.Windows.Forms.PictureBox pbLanguageUA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbMenu;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiHome;
        private System.Windows.Forms.ToolStripMenuItem tsmiPizza;
        private System.Windows.Forms.ToolStripMenuItem tsmiCombo;
        private System.Windows.Forms.ToolStripMenuItem tsmiBurger;
        private System.Windows.Forms.ToolStripMenuItem tsmiSnack;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrink;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridViewTextBoxColumn goods;
        private System.Windows.Forms.DataGridViewTextBoxColumn number_sales;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_price;
    }
}