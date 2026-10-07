using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diploma_NPT.formPizza;

namespace Diploma_NPT
{
    public partial class formDrinks : Form
    {
        public formDrinks()
        {
            InitializeComponent();
        }

        private void formDrinks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";
        public List<Goods> goods = new List<Goods>();

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

        private void pbCart_Click(object sender, EventArgs e)
        {
            this.Hide();
            formCart formCart = new formCart();
            formCart.Show();
        }

        void LoadData()
        {
            try { db.Execute<Goods>(file_db, "select id_goods, product_name, number, size, price, popularity, categories, description, images, type from goods where categories = 'Напої'", ref goods); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void ShowDrinks(ref List<Goods> temp_goods, ref DataGridView data)
        {
            dgvDrinks.Rows.Clear();

            if (goods.Count > 0)
            {
                foreach (Goods g in temp_goods)
                {
                    data.Rows.Add(g.Name, g.Type, g.Size, g.Price.ToString(), g.Popularity, g.Description);
                }
            }
        }

        void ShowTypes(ref List<Goods> temp_goods, ref ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var uniqueTypes = temp_goods.Select(g => g.Type).Distinct().ToList();

            foreach (var type in uniqueTypes) comboBox.Items.Add(type);
        }

        void ShowPopularity(ref List<Goods> temp_goods, ref ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var uniquePopularity = temp_goods.Select(g => g.Popularity).Distinct().OrderByDescending(g => g).ToList();

            foreach (var popul in uniquePopularity) comboBox.Items.Add(popul);
        }

        private ImageList imageList;

        private void SetupListView()
        {
            lvDrink.View = View.LargeIcon;
            lvDrink.LargeImageList = new ImageList();
            imageList = lvDrink.LargeImageList;
            imageList.ImageSize = new Size(108, 98);
        }

        private void LoadProductsToListViewFiltration(List<Goods> filteredGoods = null)
        {
            var goodsToDisplay = filteredGoods ?? goods;

            lvDrink.Items.Clear();
            imageList.Images.Clear();

            int imageIndex = 0;

            foreach (var product in goodsToDisplay.Distinct())
            {
                string current_path = Path.Combine(Directory.GetCurrentDirectory(), "Photo_cursach", "Drinks", product.Image);

                if (File.Exists(current_path))
                {
                    using (Image img = Image.FromFile(current_path))
                    {
                        imageList.Images.Add(img);
                    }

                    ListViewItem item = new ListViewItem(product.Name, imageIndex);
                    lvDrink.Items.Add(item);

                    imageIndex++;
                }

                else MessageBox.Show($"Файл не знайдено: {product.Image}");
            }
        }

        private void formDrinks_Load(object sender, EventArgs e)
        {
            LoadData();
            ShowTypes(ref goods, ref cbTypeDrink);
            ShowPopularity(ref goods, ref cbPopularityDrink);
            ShowDrinks(ref goods, ref dgvDrinks);
            SetupListView();
            LoadProductsToListViewFiltration();
            UpdateCartLabels();
        }

        private void btnSearchDrinks_Click(object sender, EventArgs e)
        {
            List<Goods> resultListGoods = goods.Select(g => g).ToList();

            if (tbSearch.Text.Trim() != String.Empty)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text)).Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай").Select(g => g).ToList();
            }

            if (rbSmallDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "маленький").Select(g => g).ToList();
            }

            if (rbMediumDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "середній").Select(g => g).ToList();
            }

            if (rbLargeDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "великий").Select(g => g).ToList();
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => (g.Price >= priceFrom && g.Price <= priceTo)).OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Popularity == "3").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypeDrink.Text.Trim() == "Сік")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Сік").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypeDrink.Text.Trim() == "Кава")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Кава").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypeDrink.Text.Trim() == "Чай")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Чай").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbSmallDrink.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "маленький").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbMediumDrink.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "середній").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbLargeDrink.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "великий").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                string text = tbSearch.Text.Trim();

                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Name.Contains(text)).OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityDrink.Text.Trim() == "5")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "5").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityDrink.Text.Trim() == "4")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "4").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityDrink.Text.Trim() == "3")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbSmallDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbMediumDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbLargeDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "великий").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbSmallDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbMediumDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbLargeDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "великий").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbSmallDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbMediumDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbLargeDrink.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "великий").Select(g => g).ToList();
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypeDrink.Text.Trim() == "Сік")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Сік").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypeDrink.Text.Trim() == "Кава")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Кава").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypeDrink.Text.Trim() == "Чай")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Чай").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbSmallDrink.Checked)
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbMediumDrink.Checked)
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbLargeDrink.Checked)
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityDrink.Text.Trim() == "5")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => (g.Price >= priceFrom && g.Price <= priceTo && g.Popularity == "5")).OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityDrink.Text.Trim() == "4")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Popularity == "4").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityDrink.Text.Trim() == "3")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Popularity == "3").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbSmallDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Сік" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbMediumDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Сік" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbLargeDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Сік" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbSmallDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Кава" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbMediumDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Кава" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbLargeDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Кава" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbSmallDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Чай" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbMediumDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Чай" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbLargeDrink.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Чай" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Сік" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Сік" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Кава" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Кава" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbSmallDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbMediumDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeDrink.Text.Trim() == "Чай" && rbLargeDrink.Checked && cbPopularityDrink.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Чай" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            ShowDrinks(ref resultListGoods, ref dgvDrinks);
            LoadProductsToListViewFiltration(resultListGoods);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            goods.Clear();
            LoadData();
            ShowDrinks(ref goods, ref dgvDrinks);
            LoadProductsToListViewFiltration();

            tbSearch.Text = string.Empty;
            tbPriceFrom.Text = string.Empty;
            tbPriceTo.Text = string.Empty;
            rbSmallDrink.Checked = false;
            rbMediumDrink.Checked = false;
            rbLargeDrink.Checked = false;

            cbTypeDrink.SelectedIndex = -1;
            cbTypeDrink.Text = string.Empty;
            cbTypeDrink.SelectedItem = null;

            cbPopularityDrink.SelectedIndex = -1;
            cbPopularityDrink.Text = string.Empty;
            cbPopularityDrink.SelectedItem = null;
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

        private void lvDrink_DoubleClick(object sender, EventArgs e)
        {
            if (lvDrink.SelectedItems.Count > 0)
            {
                var selectedItem = lvDrink.SelectedItems[0];

                int selectedGoodsId = goods.Where(g => g.Name == selectedItem.Text).Select(g => g.ID).FirstOrDefault();

                var selectedGoods = goods.FirstOrDefault(g => g.ID == selectedGoodsId);

                if (selectedGoods != null)
                {
                    var existingItem = CartManager.CartItems.FirstOrDefault(item => item.Name == selectedGoods.Name);

                    if (existingItem != null) existingItem.Quantity++;

                    else CartManager.CartItems.Add(new CartItem(selectedGoods.Name, selectedGoods.Price, 1));

                    MessageBox.Show($"{selectedGoods.Name} додано до кошика!");
                    UpdateCartLabels();
                }

                else
                {
                    MessageBox.Show("Товар не знайдено!");
                }
            }
        }
    }
}