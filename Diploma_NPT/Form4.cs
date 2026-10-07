using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Diploma_NPT
{
    public partial class formPizza : Form
    {
        public formPizza()
        {
            InitializeComponent();
        }

        private void formPizza_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";
        public List<Goods> goods = new List<Goods>();

        public static class CartManager
        {
            public static List<CartItem> CartItems { get; set; } = new List<CartItem>();
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

        void LoadData()
        {
            try { db.Execute<Goods>(file_db, "select id_goods, product_name, number, size, price, popularity, categories, description, images, type from goods where categories = 'Піца'", ref goods); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void ShowPizzas(ref List<Goods> temp_goods, ref DataGridView data)
        {
            dgvPizza.Rows.Clear();

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
            lvPizza.View = View.LargeIcon;
            lvPizza.LargeImageList = new ImageList();
            imageList = lvPizza.LargeImageList;
            imageList.ImageSize = new Size(105, 105);
        }

        private void LoadProductsToListViewFiltration(List<Goods> filteredGoods = null)
        {
            var goodsToDisplay = filteredGoods ?? goods;

            lvPizza.Items.Clear();
            imageList.Images.Clear();

            int imageIndex = 0;

            foreach (var product in goodsToDisplay.Distinct())
            {
                string current_path = Path.Combine(Directory.GetCurrentDirectory(), "Photo_cursach", "Pizzas", product.Image);

                if (File.Exists(current_path))
                {
                    using (Image img = Image.FromFile(current_path))
                    {
                        imageList.Images.Add(img);
                    }

                    ListViewItem item = new ListViewItem(product.Name, imageIndex);
                    lvPizza.Items.Add(item);

                    imageIndex++;
                }

                else MessageBox.Show($"Файл не знайдено: {product.Image}");
            }
        }

        private void formPizza_Load(object sender, EventArgs e)
        {
            LoadData();
            ShowTypes(ref goods, ref cbTypePizza);
            ShowPopularity(ref goods, ref cbPopularityPizza);
            ShowPizzas(ref goods, ref dgvPizza);
            SetupListView();
            LoadProductsToListViewFiltration();
            UpdateCartLabels();
        }

        private void btnSearchPizzas_Click(object sender, EventArgs e)
        {
            List<Goods> resultListGoods = goods.Select(g => g).ToList();

            if (tbSearch.Text.Trim() != String.Empty)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text)).Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська").Select(g => g).ToList();
            }

            if (rbSmallPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "маленький").Select(g => g).ToList();
            }

            if (rbMediumPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Size == "середній").Select(g => g).ToList();
            }

            if (rbLargePizza.Checked)
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

            if (cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Popularity == "3").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypePizza.Text.Trim() == "Звичайна")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Звичайна").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypePizza.Text.Trim() == "Вегетаріанська")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Вегетаріанська").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbTypePizza.Text.Trim() == "Морська")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Type == "Морська").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbSmallPizza.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "маленький").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbMediumPizza.Checked)
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Size == "середній").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && rbLargePizza.Checked)
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

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityPizza.Text.Trim() == "5")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "5").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityPizza.Text.Trim() == "4")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "4").Select(g => g).ToList();
            }

            if (tbSearch.Text.Trim() != String.Empty && cbPopularityPizza.Text.Trim() == "3")
            {
                string text = tbSearch.Text.Trim();

                resultListGoods = goods.Where(g => g.Name.Contains(text) && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbSmallPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbMediumPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbLargePizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "великий").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbSmallPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbMediumPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbLargePizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "великий").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbSmallPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "маленький").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbMediumPizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "середній").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbLargePizza.Checked)
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "великий").Select(g => g).ToList();
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypePizza.Text.Trim() == "Звичайна")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайна").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypePizza.Text.Trim() == "Вегетаріанська")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанська").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbTypePizza.Text.Trim() == "Морська")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морська").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbSmallPizza.Checked)
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

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbMediumPizza.Checked)
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

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && rbLargePizza.Checked)
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

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityPizza.Text.Trim() == "5")
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityPizza.Text.Trim() == "4")
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

            if (tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "" && cbPopularityPizza.Text.Trim() == "3")
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

            if (cbTypePizza.Text.Trim() == "Звичайна" && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbSmallPizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайна" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbMediumPizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайна" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbLargePizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Звичайна" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbSmallPizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанська" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbMediumPizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанська" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbLargePizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Вегетаріанська" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbSmallPizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морська" && g.Size == "маленький").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbMediumPizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морська" && g.Size == "середній").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbLargePizza.Checked && tbPriceFrom.Text.Trim() != "" && tbPriceTo.Text.Trim() != "")
            {
                if (decimal.TryParse(tbPriceFrom.Text.Trim(), out decimal priceFrom) && decimal.TryParse(tbPriceTo.Text.Trim(), out decimal priceTo))
                {
                    decimal priceMin = goods.OrderBy(g => g.Price).Select(g => g.Price).FirstOrDefault();
                    decimal priceMax = goods.OrderByDescending(g => g.Price).Select(g => g.Price).FirstOrDefault();

                    if (priceFrom >= priceMin && priceTo <= priceMax)
                    {
                        resultListGoods = goods.Where(g => g.Price >= priceFrom && g.Price <= priceTo && g.Type == "Морська" && g.Size == "великий").OrderBy(g => g.Price).ToList();
                    }

                    else MessageBox.Show($"Будь ласка, введіть значення ціни в діапазоні від {priceMin} до {priceMax}", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else MessageBox.Show("Будь ласка, введіть коректні числові значення для діапазону цін", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "маленький" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "середній" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "5")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "великий" && g.Popularity == "5").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "маленький" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "середній" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "4")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "великий" && g.Popularity == "4").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Звичайна" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Звичайна" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Вегетаріанська" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Вегетаріанська" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbSmallPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "маленький" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbMediumPizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "середній" && g.Popularity == "3").Select(g => g).ToList();
            }

            if (cbTypePizza.Text.Trim() == "Морська" && rbLargePizza.Checked && cbPopularityPizza.Text.Trim() == "3")
            {
                resultListGoods = goods.Where(g => g.Type == "Морська" && g.Size == "великий" && g.Popularity == "3").Select(g => g).ToList();
            }

            ShowPizzas(ref resultListGoods, ref dgvPizza);
            LoadProductsToListViewFiltration(resultListGoods);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            goods.Clear();
            LoadData();
            ShowPizzas(ref goods, ref dgvPizza);
            LoadProductsToListViewFiltration();

            tbSearch.Text = string.Empty;
            tbPriceFrom.Text = string.Empty;
            tbPriceTo.Text = string.Empty;
            rbSmallPizza.Checked = false;
            rbMediumPizza.Checked = false;
            rbLargePizza.Checked = false;

            cbTypePizza.SelectedIndex = -1;
            cbTypePizza.Text = string.Empty;
            cbTypePizza.SelectedItem = null;

            cbPopularityPizza.SelectedIndex = -1;
            cbPopularityPizza.Text = string.Empty;
            cbPopularityPizza.SelectedItem = null;
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

        private void lvPizza_DoubleClick(object sender, EventArgs e)
        {
            if (lvPizza.SelectedItems.Count > 0)
            {
                var selectedItem = lvPizza.SelectedItems[0];

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