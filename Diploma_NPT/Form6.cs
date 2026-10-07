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
    public partial class formBurgers : Form
    {
        public formBurgers()
        {
            InitializeComponent();
        }

        private void formBurgers_FormClosing(object sender, FormClosingEventArgs e)
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

        void LoadData()
        {
            try { db.Execute<Goods>(file_db, "select id_goods, product_name, number, size, price, popularity, categories, description, images, type from goods where categories = 'Бургери'", ref goods); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void ShowBurgers(ref List<Goods> temp_goods, ref DataGridView data)
        {
            dgvBurger.Rows.Clear();

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

            var uniquePopularity = temp_goods.Select(g => g.Popularity).Distinct().ToList();

            foreach (var popul in uniquePopularity) comboBox.Items.Add(popul);
        }

        private ImageList imageList;

        private void SetupListView()
        {
            lvBurger.View = View.LargeIcon;
            lvBurger.LargeImageList = new ImageList();
            imageList = lvBurger.LargeImageList;
            imageList.ImageSize = new Size(100, 80);
        }

        private void LoadProductsToListViewFiltration(List<Goods> filteredGoods = null)
        {
            var goodsToDisplay = filteredGoods ?? goods;

            lvBurger.Items.Clear();
            imageList.Images.Clear();

            int imageIndex = 0;

            foreach (var product in goodsToDisplay.Distinct())
            {
                string current_path = Path.Combine(Directory.GetCurrentDirectory(), "Photo_cursach", "Burgers", product.Image);

                if (File.Exists(current_path))
                {
                    using (Image img = Image.FromFile(current_path))
                    {
                        imageList.Images.Add(img);
                    }

                    ListViewItem item = new ListViewItem(product.Name, imageIndex);
                    lvBurger.Items.Add(item);

                    imageIndex++;
                }

                else MessageBox.Show($"Файл не знайдено: {product.Image}");
            }
        }

        private void formBurgers_Load(object sender, EventArgs e)
        {
            LoadData();
            ShowTypes(ref goods, ref cbTypeBurger);
            ShowPopularity(ref goods, ref cbPopularityBurger);
            ShowBurgers(ref goods, ref dgvBurger);
            SetupListView();
            LoadProductsToListViewFiltration();
            UpdateCartLabels();
        }

        private void btnSearchBurgers_Click(object sender, EventArgs e)
        {
            List<Goods> resultListGoods = goods.Select(g => g).ToList();

            if (tbSearch.Text.Trim() != String.Empty)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text)).Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський").Select(g => g).ToList();
            }

            if (rbSmallBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "маленький").Select(g => g).ToList();
            }

            if (rbMediumBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "середній").Select(g => g).ToList();
            }

            if (rbLargeBurger.Checked)
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

            if (cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Popularity == "3").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypeBurger.Text.Trim() == "Звичайний")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Звичайний").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypeBurger.Text.Trim() == "Вегетаріанський")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Вегетаріанський").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypeBurger.Text.Trim() == "Морський")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Морський").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbSmallBurger.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "маленький").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbMediumBurger.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "середній").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbLargeBurger.Checked)
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

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityBurger.Text.Trim() == "5")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "5").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityBurger.Text.Trim() == "4")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "4").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityBurger.Text.Trim() == "3")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbSmallBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbMediumBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbLargeBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "великий").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbSmallBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbMediumBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbLargeBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "великий").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbSmallBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbMediumBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbLargeBurger.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "великий").Select(g => g).ToList();
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypeBurger.Text.Trim() == "Звичайний")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайний").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypeBurger.Text.Trim() == "Вегетаріанський")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанський").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypeBurger.Text.Trim() == "Морський")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морський").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbSmallBurger.Checked)
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbMediumBurger.Checked)
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbLargeBurger.Checked)
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityBurger.Text.Trim() == "5")
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityBurger.Text.Trim() == "4")
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityBurger.Text.Trim() == "3")
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

            if (cbTypeBurger.Text.Trim() == "Звичайний" && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }




            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbSmallBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайний" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbMediumBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайний" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbLargeBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайний" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbSmallBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанський" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbMediumBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанський" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbLargeBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанський" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbSmallBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морський" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbMediumBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морський" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbLargeBurger.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морський" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Звичайний" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайний" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Вегетаріанський" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанський" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbSmallBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbMediumBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypeBurger.Text.Trim() == "Морський" && rbLargeBurger.Checked && cbPopularityBurger.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морський" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            ShowBurgers(ref resultListGoods, ref dgvBurger);
            LoadProductsToListViewFiltration(resultListGoods);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            goods.Clear();
            LoadData();
            ShowBurgers(ref goods, ref dgvBurger);
            LoadProductsToListViewFiltration();

            tbSearch.Text = string.Empty;
            tbPriceFrom.Text = string.Empty;
            tbPriceTo.Text = string.Empty;
            rbSmallBurger.Checked = false;
            rbMediumBurger.Checked = false;
            rbLargeBurger.Checked = false;

            cbTypeBurger.SelectedIndex = -1;
            cbTypeBurger.Text = string.Empty;
            cbTypeBurger.SelectedItem = null;

            cbPopularityBurger.SelectedIndex = -1;
            cbPopularityBurger.Text = string.Empty;
            cbPopularityBurger.SelectedItem = null;
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

        private void lvBurger_DoubleClick(object sender, EventArgs e)
        {
            if (lvBurger.SelectedItems.Count > 0)
            {
                var selectedItem = lvBurger.SelectedItems[0];

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