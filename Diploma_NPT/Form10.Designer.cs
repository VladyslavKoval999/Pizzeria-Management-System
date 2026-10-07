namespace Diploma_NPT
{
    partial class formCart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formCart));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.pbAdd = new System.Windows.Forms.PictureBox();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.goods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnContinueShopping = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
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
            this.lbPrice = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
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
            // pbDelete
            // 
            this.pbDelete.BackColor = System.Drawing.Color.Purple;
            this.pbDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDelete.Image = ((System.Drawing.Image)(resources.GetObject("pbDelete.Image")));
            this.pbDelete.Location = new System.Drawing.Point(378, 764);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(65, 62);
            this.pbDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDelete.TabIndex = 271;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // pbAdd
            // 
            this.pbAdd.BackColor = System.Drawing.Color.Purple;
            this.pbAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAdd.Image = ((System.Drawing.Image)(resources.GetObject("pbAdd.Image")));
            this.pbAdd.Location = new System.Drawing.Point(468, 764);
            this.pbAdd.Name = "pbAdd";
            this.pbAdd.Size = new System.Drawing.Size(65, 62);
            this.pbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAdd.TabIndex = 270;
            this.pbAdd.TabStop = false;
            this.pbAdd.Click += new System.EventHandler(this.pbAdd_Click);
            // 
            // dgvCart
            // 
            this.dgvCart.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvCart.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.goods,
            this.price,
            this.number,
            this.discount,
            this.total_price});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCart.Location = new System.Drawing.Point(136, 168);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.RowTemplate.Height = 24;
            this.dgvCart.Size = new System.Drawing.Size(1266, 527);
            this.dgvCart.TabIndex = 269;
            this.dgvCart.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellClick);
            this.dgvCart.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellValueChanged);
            // 
            // goods
            // 
            this.goods.HeaderText = "Товар";
            this.goods.MinimumWidth = 6;
            this.goods.Name = "goods";
            this.goods.ReadOnly = true;
            this.goods.Width = 335;
            // 
            // price
            // 
            this.price.HeaderText = "Ціна";
            this.price.MinimumWidth = 6;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            this.price.Width = 150;
            // 
            // number
            // 
            this.number.HeaderText = "Кількість";
            this.number.MinimumWidth = 6;
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 140;
            // 
            // discount
            // 
            this.discount.HeaderText = "Знижка";
            this.discount.MinimumWidth = 6;
            this.discount.Name = "discount";
            this.discount.ReadOnly = true;
            this.discount.Width = 125;
            // 
            // total_price
            // 
            this.total_price.HeaderText = "Загальна ціна";
            this.total_price.MinimumWidth = 6;
            this.total_price.Name = "total_price";
            this.total_price.ReadOnly = true;
            this.total_price.Width = 150;
            // 
            // btnContinueShopping
            // 
            this.btnContinueShopping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.btnContinueShopping.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinueShopping.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnContinueShopping.ForeColor = System.Drawing.Color.Purple;
            this.btnContinueShopping.Location = new System.Drawing.Point(559, 770);
            this.btnContinueShopping.Name = "btnContinueShopping";
            this.btnContinueShopping.Size = new System.Drawing.Size(413, 51);
            this.btnContinueShopping.TabIndex = 268;
            this.btnContinueShopping.Text = "Продовжити покупки";
            this.btnContinueShopping.UseVisualStyleBackColor = false;
            this.btnContinueShopping.Click += new System.EventHandler(this.btnContinueShopping_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Purple;
            this.label13.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label13.Location = new System.Drawing.Point(131, 720);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(204, 28);
            this.label13.TabIndex = 267;
            this.label13.Text = "Сума замовлення:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.ForeColor = System.Drawing.Color.Purple;
            this.btnClear.Location = new System.Drawing.Point(136, 770);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(213, 51);
            this.btnClear.TabIndex = 266;
            this.btnClear.Text = "Очистити";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.btnPlaceOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlaceOrder.ForeColor = System.Drawing.Color.Purple;
            this.btnPlaceOrder.Location = new System.Drawing.Point(989, 770);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(413, 51);
            this.btnPlaceOrder.TabIndex = 265;
            this.btnPlaceOrder.Text = "Оформити замовлення";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Purple;
            this.pictureBox4.Location = new System.Drawing.Point(82, 150);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1369, 688);
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
            this.pbCart.Image = global::Diploma_NPT.Properties.Resources._8_cart__1_;
            this.pbCart.Location = new System.Drawing.Point(1276, 21);
            this.pbCart.Name = "pbCart";
            this.pbCart.Size = new System.Drawing.Size(93, 75);
            this.pbCart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCart.TabIndex = 262;
            this.pbCart.TabStop = false;
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
            this.pbLogo.Image = global::Diploma_NPT.Properties.Resources._1_logo__1_;
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
            this.msMainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.msMainMenu.Size = new System.Drawing.Size(157, 332);
            this.msMainMenu.TabIndex = 275;
            this.msMainMenu.Visible = false;
            // 
            // tsmiHome
            // 
            this.tsmiHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiHome.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.tsmiHome.Name = "tsmiHome";
            this.tsmiHome.Size = new System.Drawing.Size(146, 35);
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
            this.tsmiPizza.Size = new System.Drawing.Size(146, 35);
            this.tsmiPizza.Text = "Піци";
            this.tsmiPizza.Click += new System.EventHandler(this.tsmiPizza_Click);
            // 
            // tsmiCombo
            // 
            this.tsmiCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiCombo.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiCombo.Name = "tsmiCombo";
            this.tsmiCombo.Size = new System.Drawing.Size(146, 35);
            this.tsmiCombo.Text = "Комбо";
            this.tsmiCombo.Click += new System.EventHandler(this.tsmiCombo_Click);
            // 
            // tsmiBurger
            // 
            this.tsmiBurger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiBurger.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiBurger.Name = "tsmiBurger";
            this.tsmiBurger.Size = new System.Drawing.Size(146, 35);
            this.tsmiBurger.Text = "Бургери";
            this.tsmiBurger.Click += new System.EventHandler(this.tsmiBurger_Click);
            // 
            // tsmiSnack
            // 
            this.tsmiSnack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiSnack.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiSnack.Name = "tsmiSnack";
            this.tsmiSnack.Size = new System.Drawing.Size(146, 35);
            this.tsmiSnack.Text = "Снеки";
            this.tsmiSnack.Click += new System.EventHandler(this.tsmiSnack_Click);
            // 
            // tsmiDrink
            // 
            this.tsmiDrink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.tsmiDrink.Margin = new System.Windows.Forms.Padding(0, 15, 5, 0);
            this.tsmiDrink.Name = "tsmiDrink";
            this.tsmiDrink.Size = new System.Drawing.Size(146, 35);
            this.tsmiDrink.Text = "Напої";
            this.tsmiDrink.Click += new System.EventHandler(this.tsmiDrink_Click);
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.BackColor = System.Drawing.Color.Purple;
            this.lbPrice.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.lbPrice.Location = new System.Drawing.Point(1239, 720);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(109, 28);
            this.lbPrice.TabIndex = 277;
            this.lbPrice.Text = "0000,00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Purple;
            this.label14.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.label14.Location = new System.Drawing.Point(1354, 720);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 28);
            this.label14.TabIndex = 276;
            this.label14.Text = "грн";
            // 
            // formCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(175)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(1535, 903);
            this.Controls.Add(this.lbPrice);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.msMainMenu);
            this.Controls.Add(this.pbDelete);
            this.Controls.Add(this.pbAdd);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnContinueShopping);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPlaceOrder);
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
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "formCart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Кошик";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formCart_FormClosing);
            this.Load += new System.EventHandler(this.formCart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
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

        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.PictureBox pbAdd;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Button btnContinueShopping;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPlaceOrder;
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
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn goods;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_price;
    }
}