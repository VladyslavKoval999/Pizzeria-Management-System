using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diploma_NPT.formPizza;

namespace Diploma_NPT
{
    public partial class formReport : Form
    {
        public formReport()
        {
            InitializeComponent();
            dgvReport.MultiSelect = false;
        }
        
        private void formReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";

        List<Goods> globalgoods = new List<Goods>();
        List<OrderingInAnInstitution> orderings = new List<OrderingInAnInstitution>();
        List<DishesToOrder> dishes = new List<DishesToOrder>();

        private void LoadData()
        {
            globalgoods.Clear();
            orderings.Clear();
            dishes.Clear();

            try
            {
                db.Execute<Goods>(file_db, "SELECT id_goods, product_name, number, size, price, popularity, categories, description, images, type FROM goods", ref globalgoods);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження товарів: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                db.Execute<OrderingInAnInstitution>(file_db, "SELECT id_order, date_of_order, id_employee FROM ordering_in_an_institution", ref orderings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження замовлень: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                db.Execute<DishesToOrder>(file_db, "SELECT id_meal, id_order, id_order_online, id_goods, number, price_for_one FROM dishes_to_order", ref dishes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження страв: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            var categories = globalgoods.Select(g => g.Category).Distinct().ToList();
            cbCategoryGoods.Items.Clear();
            cbCategoryGoods.Items.Add("Усі категорії");
            cbCategoryGoods.Items.AddRange(categories.ToArray());
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
            formHomePage formHomePage = new formHomePage();
            formHomePage.Show();
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {
            if (msMainMenu.Visible == false) msMainMenu.Visible = true;
            else msMainMenu.Visible = false;
        }

        private void tsmiHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            formHomePage formHomePage = new formHomePage();
            formHomePage.Show();
        }

        private void tsmiPizza_Click(object sender, EventArgs e)
        {
            this.Hide();
            formPizza formPizza = new formPizza();
            formPizza.Show();
        }

        private void tsmiCombo_Click(object sender, EventArgs e)
        {
            this.Hide();
            formCombo formCombo = new formCombo();
            formCombo.Show();
        }

        private void tsmiBurger_Click(object sender, EventArgs e)
        {
            this.Hide();
            formBurgers formBurgers = new formBurgers();
            formBurgers.Show();
        }

        private void tsmiSnack_Click(object sender, EventArgs e)
        {
            this.Hide();
            formSnacks formSnacks = new formSnacks();
            formSnacks.Show();
        }

        private void tsmiDrink_Click(object sender, EventArgs e)
        {
            this.Hide();
            formDrinks formDrinks = new formDrinks();
            formDrinks.Show();
        }

        private void pbCart_Click(object sender, EventArgs e)
        {
            this.Hide();
            formCart formCart = new formCart();
            formCart.Show();
        }

        private void ApplyCoffeePromotion()
        {
            int coffeeCount = 0;

            foreach (var item in CartManager.CartItems)
            {
                List<Goods> drinkGoods = new List<Goods>();

                db.Execute<Goods>(file_db, "SELECT * FROM goods WHERE product_name = '" + item.Name + "' AND categories = 'Напої' AND type = 'Кава'", ref drinkGoods);

                if (drinkGoods.Count > 0) coffeeCount += item.Quantity;
            }

            var diabloGift = CartManager.CartItems.FirstOrDefault(item => item.Name == "Діабло (подарунок)");

            if (coffeeCount >= 2)
            {
                if (diabloGift == null)
                {
                    List<Goods> diabloPizzas = new List<Goods>();

                    db.Execute<Goods>(file_db, "SELECT * FROM goods WHERE product_name = 'Діабло' AND categories = 'Піца'", ref diabloPizzas);

                    if (diabloPizzas.Count > 0)
                    {
                        CartManager.CartItems.Add(new CartItem { Name = "Діабло (подарунок)", Price = 0m, Quantity = 1 });
                    }
                }
            }

            else if (diabloGift != null)
            {
                CartManager.CartItems.Remove(diabloGift);
            }
        }

        private decimal CalculateTotalPriceWithDiscount()
        {
            ApplyCoffeePromotion();

            decimal totalPrice = 0m;

            foreach (var item in CartManager.CartItems)
            {
                List<Goods_combo> comboDetails = new List<Goods_combo>();

                db.Execute<Goods_combo>(file_db, $"select id_combo, name_combo, number, total_price from goods_combo where name_combo = '{item.Name}'", ref comboDetails);

                if (comboDetails.Count > 0) totalPrice += comboDetails[0].Total_price * item.Quantity;

                else
                {
                    List<Goods> goods = new List<Goods>();
                    db.Execute<Goods>(file_db, $"SELECT id_goods, product_name, number, size, price, popularity, categories, description, images, type FROM goods WHERE product_name = '{item.Name.Replace(" (подарунок)", "")}'", ref goods);

                    if (goods.Count > 0 && goods[0].Category == "Піца" && !item.Name.Contains("(подарунок)"))
                    {
                        decimal discountedPrice = goods[0].Price * 0.85m;
                        totalPrice += discountedPrice * item.Quantity;
                    }

                    else if (item.Name == "Діабло (подарунок)") totalPrice += 0m;

                    else if (goods.Count > 0) totalPrice += goods[0].Price * item.Quantity;
                }
            }

            return totalPrice;
        }

        private void UpdateCartLabels()
        {
            int totalQuantity = CartManager.CartItems.Sum(item => item.Quantity);
            decimal totalPrice = CalculateTotalPriceWithDiscount();

            label5.Text = $"{totalQuantity} шт";
            label6.Text = $"{totalPrice:F2} грн";
        }

        private void formReport_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCategories();
            UpdateCartLabels();
            monthCalendar1.SetDate(DateTime.Today);
        }
        
        private void btnReportResult_Click(object sender, EventArgs e)
        {
            dgvReport.Rows.Clear();

            DateTime startDate = monthCalendar1.SelectionStart;
            DateTime endDate = monthCalendar1.SelectionEnd;

            string selectedCategory = cbCategoryGoods.SelectedItem?.ToString();

            var filteredOrders = (from o in orderings
                                  where o.Date_of_order >= startDate && o.Date_of_order <= endDate
                                  select o.ID_order).ToList();

            var filteredDishes = (from d in dishes
                                  join o in orderings on d.ID_order equals o.ID_order
                                  where o.Date_of_order >= startDate && o.Date_of_order <= endDate
                                  group d by d.ID_goods into g
                                  select new
                                  {
                                      GoodsId = g.Key,
                                      TotalQuantity = g.Sum(d => d.Number),
                                      TotalPrice = g.Sum(d => d.Price)
                                  }).ToList();

            HashSet<string> addedProducts = new HashSet<string>();

            foreach (var dish in filteredDishes)
            {
                var good = globalgoods.FirstOrDefault(g => g.ID == dish.GoodsId);
                if (good != null)
                {
                    if (selectedCategory == "Усі категорії" || good.Category == selectedCategory)
                    {
                        string productName = good.Name;
                        if (productName == "Діабло" && dish.TotalPrice == 0)
                        {
                            productName = "Діабло (подарунок)";
                        }

                        string uniqueKey = $"{productName}_{dish.GoodsId}";
                        if (!addedProducts.Contains(uniqueKey))
                        {
                            dgvReport.Rows.Add(productName, dish.TotalQuantity, dish.TotalPrice.ToString("F2"));
                            addedProducts.Add(uniqueKey);
                        }
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
            LoadCategories();
            dgvReport.Rows.Clear();
            monthCalendar1.SetDate(DateTime.Today);
            cbCategoryGoods.SelectedIndex = -1;
            cbCategoryGoods.Text = "";
            MessageBox.Show("Дані успішно оновлено.", "Оновлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pbPrint_Click(object sender, EventArgs e)
        {
            bool hasData = false;

            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells[0].Value != null && !string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                {
                    hasData = true;
                    break;
                }
            }

            if (!hasData)
            {
                MessageBox.Show("Звіт порожній. Оберіть певний період та категорію товару для звітності.");
                return;
            }

            try
            {
                CalculateTotalPages();

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument1;
                printPreviewDialog.ShowDialog();

                dgvReport.Rows.Clear();
                monthCalendar1.SetDate(monthCalendar1.TodayDate);
                cbCategoryGoods.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Помилка друку", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int currentPrintRow = 0;
        private int pageNumber = 1;
        private int totalPages;

        private void CalculateTotalPages()
        {
            const int paperHeight = 1100;
            const int margin = 25;
            const int headerHeight = 200;
            const int footerHeight = 100;
            int itemsPerPage = (paperHeight - headerHeight - footerHeight - margin * 2) / 25;
            totalPages = (int)Math.Ceiling((double)dgvReport.Rows.Count / itemsPerPage);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                const int paperWidth = 500;
                const int paperHeight = 1100;
                const int margin = 25;
                const int headerHeight = 200;
                const int footerHeight = 100;
                const string fontName = "Arial";
                const int fontSize = 11;
                const FontStyle fontStyle = FontStyle.Regular;

                using (Font font = new Font(fontName, fontSize, fontStyle))
                {
                    using (StringFormat stringFormat = new StringFormat())
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        DrawHeader(e.Graphics, font, fontStyle, stringFormat, margin, headerHeight, paperWidth, paperHeight);

                        DrawBody(e.Graphics, font, stringFormat, margin, headerHeight, footerHeight, paperHeight, e);

                        DrawFooter(e.Graphics, font, stringFormat, margin, footerHeight, paperWidth, paperHeight, pageNumber);
                    }
                }
            }

            if (e.HasMorePages) pageNumber++;

            else
            {
                pageNumber = 1;
                currentPrintRow = 0;
            }
        }

        private void DrawHeader(Graphics graphics, Font font, FontStyle fs, StringFormat stringFormat, int margin, int headerHeight, int paperWidth, int paperHeight)
        {
            Bitmap logo = Properties.Resources._1_logo;
            graphics.DrawImage(logo, margin, margin, 200, 113);

            string headerText = $"Звіт продаж ({cbCategoryGoods.Text.ToLower()})";
            string headerText1 = DateTime.Now.ToString();
            string headerText2 = $"Період: {monthCalendar1.SelectionStart.Date.ToString()} - {monthCalendar1.SelectionEnd.Date.ToString()}";

            int textY = margin + 20;
            int lineHeight = 20;

            using (StringFormat centerFormat = new StringFormat())
            {
                centerFormat.Alignment = StringAlignment.Center;
                centerFormat.LineAlignment = StringAlignment.Center;

                using (Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold))
                {
                    SizeF headerText2Size = graphics.MeasureString(headerText2, font);
                    float headerText2X = margin + 208;
                    float centerX = headerText2X + (headerText2Size.Width / 2);

                    float headerTextWidth = graphics.MeasureString(headerText, boldFont).Width;
                    float headerTextX = centerX - (headerTextWidth / 2);

                    graphics.DrawString(headerText, boldFont, Brushes.Black, new RectangleF(headerTextX, textY + 15, headerTextWidth, headerHeight), centerFormat);
                }

                graphics.DrawString(headerText1, font, Brushes.Black, new RectangleF(margin + 315, textY + lineHeight + 20, paperWidth - margin * 2, headerHeight), stringFormat);
                graphics.DrawString(headerText2, font, Brushes.Black, new RectangleF(margin + 215, textY + lineHeight * 2 + 25, paperWidth - margin * 2, headerHeight), stringFormat);
            }
        }

        private void DrawBody(Graphics graphics, Font font, StringFormat stringFormat, int margin, int headerHeight, int footerHeight, int paperHeight, PrintPageEventArgs e)
        {
            int startY = headerHeight + margin;
            int itemsPerPage = (paperHeight - headerHeight - footerHeight - margin * 2) / 25;

            DrawColumnNames(graphics, font, stringFormat, margin, startY);

            startY += 25;

            for (int i = currentPrintRow; i < dgvReport.Rows.Count; i++)
            {
                DataGridViewRow row = dgvReport.Rows[i];

                if (row.Cells["goods"].Value != null)
                {
                    DrawRowData(graphics, font, stringFormat, margin, startY, row);

                    startY += 25;

                    if (startY > paperHeight - footerHeight - margin)
                    {
                        e.HasMorePages = true;
                        currentPrintRow = i + 1;
                        return;
                    }
                }
            }

            e.HasMorePages = false;
            currentPrintRow = 0;
        }

        private void DrawColumnNames(Graphics graphics, Font font, StringFormat stringFormat, int margin, int startY)
        {
            Font boldFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);

            string goods = "Товар";
            string number_sales = "К-ть продажів (шт.)";
            string total_price = "Загальна ціна (грн.)";

            Pen pen = new Pen(Brushes.Black, 2);
            graphics.DrawLine(pen, margin + 60, startY + 22, margin + 720, startY + 22);

            graphics.DrawString(goods, boldFont, Brushes.Black, new RectangleF(margin + 130, startY + 20, 500, 25), stringFormat);
            graphics.DrawString(number_sales, boldFont, Brushes.Black, new RectangleF(margin + 315, startY + 20, 170, 25), stringFormat);
            graphics.DrawString(total_price, boldFont, Brushes.Black, new RectangleF(margin + 495, startY + 20, 240, 25), stringFormat);

            graphics.DrawLine(pen, margin + 60, startY + 45, margin + 720, startY + 45);
        }

        private void DrawRowData(Graphics graphics, Font font, StringFormat stringFormat, int margin, int startY, DataGridViewRow row)
        {
            string goods = row.Cells["goods"].Value.ToString();
            string number_sales = row.Cells["number_sales"].Value.ToString();
            string total_price = row.Cells["total_price"].Value.ToString();

            graphics.DrawString(goods, font, Brushes.Black, new RectangleF(margin + 130, startY + 20, 500, 25), stringFormat);
            graphics.DrawString(number_sales, font, Brushes.Black, new RectangleF(margin + 315, startY + 20, 170, 25), stringFormat);
            graphics.DrawString(total_price, font, Brushes.Black, new RectangleF(margin + 495, startY + 20, 240, 25), stringFormat);
        }

        private void DrawFooter(Graphics graphics, Font font, StringFormat stringFormat, int margin, int footerHeight, int paperWidth, int paperHeight, int pageNumber)
        {
            decimal total_sum = 0;

            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (row.Cells["total_price"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["total_price"].Value.ToString(), out decimal price))
                    {
                        total_sum += price;
                    }
                }
            }
            string footerTextSum = $"Загальна сума: {total_sum}";

            Pen pen = new Pen(Brushes.Black, 2);

            int tableTop = 200 + margin;
            int itemsPerPage = (paperHeight - 200 - footerHeight - margin * 2) / 25;
            int rowsOnCurrentPage;

            if (pageNumber == totalPages) rowsOnCurrentPage = dgvReport.Rows.Count - currentPrintRow;
            else rowsOnCurrentPage = itemsPerPage;

            if (rowsOnCurrentPage < 0 || rowsOnCurrentPage > itemsPerPage)
            {
                rowsOnCurrentPage = dgvReport.Rows.Count - (itemsPerPage * (pageNumber - 1));

                if (rowsOnCurrentPage > itemsPerPage) rowsOnCurrentPage = itemsPerPage;
                if (rowsOnCurrentPage < 0) rowsOnCurrentPage = 0;
            }

            int tableHeight = (rowsOnCurrentPage * 25) + 25;
            int tableBottom = tableTop + tableHeight;
            int textY = tableBottom + 15;

            if (pageNumber == totalPages)
            {
                graphics.DrawString(footerTextSum, font, Brushes.Black, new RectangleF(margin + 575, textY - 5, paperWidth - margin * 2, footerHeight), stringFormat);
            }

            string footerText = "*** Роздруківка списку продажів ***";
            graphics.DrawString(footerText, font, Brushes.Black, new RectangleF(margin, paperHeight - footerHeight - margin + 80, paperWidth - margin * 2, footerHeight), stringFormat);

            string pageNumberText = $"Page {pageNumber}";
            graphics.DrawString(pageNumberText, font, Brushes.Black, new RectangleF(margin, paperHeight - footerHeight / 2 - margin + 80, paperWidth - margin * 2, footerHeight / 2), stringFormat);
        }
    }
}
