using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diploma_NPT.formPizza;

namespace Diploma_NPT
{
    public partial class formSnacks : Form
    {
        public formSnacks()
        {
            InitializeComponent();
        }

        private void formSnacks_FormClosing(object sender, FormClosingEventArgs e)
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
            try { db.Execute<Goods>(file_db, "select id_goods, product_name, number, size, price, popularity, categories, description, images, type from goods where categories = 'Снеки'", ref goods); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            var controls = new Dictionary<PictureBox, System.Windows.Forms.Label>
            {
                { pbGrilledSausages, labGS },
                { pbFrenchFries, labFF },
                { pbOnionRings, labOR },
                { pbChickenNuggets, labCN },
                { pbNachoswithCheese, labNC },
                { pbBBQWings, labBBQW }
            };

            string GetToolTipText(System.Windows.Forms.Label label)
            {
                var product = goods.FirstOrDefault(g => g.Name == label.Text);

                if (product != null)
                {
                    return $"Кількість: {product.Number}" +
                           $"\nРозмір: {product.Size}" +
                           $"\nЦіна: {product.Price} грн" +
                           $"\nПопулярність: {product.Popularity}" +
                           $"\nОпис: {product.Description}";
                }

                return "Товар не знайдено";
            }

            foreach (var controlPair in controls) toolTip1.SetToolTip(controlPair.Key, GetToolTipText(controlPair.Value));
        }

        private void LoadSnackImages()
        {
            string current_path = Path.Combine(Directory.GetCurrentDirectory(), "Photo_cursach", "Snacks");

            if (!Directory.Exists(current_path))
            {
                MessageBox.Show("Директорія 'Photo_cursach/Snacks' не знайдена!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var pictureBoxMap = new Dictionary<PictureBox, string>
                {
                    { pbGrilledSausages, "Грильовані ковбаски" },
                    { pbFrenchFries, "Картопля фрі" },
                    { pbOnionRings, "Цибулеві кільця" },
                    { pbChickenNuggets, "Курячі нагетси" },
                    { pbNachoswithCheese, "Начос з сиром" },
                    { pbBBQWings, "Крильця BBQ" }
                };

                foreach (var pair in pictureBoxMap)
                {
                    PictureBox pb = pair.Key;
                    string snackName = pair.Value;

                    var snack = goods.FirstOrDefault(g => g.Name == snackName);
                    if (snack != null && !string.IsNullOrEmpty(snack.Image))
                    {
                        string imagePath = Path.Combine(current_path, snack.Image);

                        if (File.Exists(imagePath)) pb.Image = Image.FromFile(imagePath);

                        else
                        {
                            pb.Image = null;
                            MessageBox.Show($"Зображення для '{snackName}' не знайдено: {imagePath}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    else
                    {
                        pb.Image = null;
                        MessageBox.Show($"Зображення або товар для '{snackName}' не знайдено в базі даних!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні зображень: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void formSnacks_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadSnackImages();
            UpdateCartLabels();
        }

        private void AddSnackToCart(string snackName)
        {
            var selectedGoods = goods.FirstOrDefault(g => g.Name == snackName);

            if (selectedGoods != null)
            {
                var existingItem = CartManager.CartItems.FirstOrDefault(item => item.Name == selectedGoods.Name);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    CartManager.CartItems.Add(new CartItem(selectedGoods.Name, selectedGoods.Price, 1));
                }

                MessageBox.Show($"{selectedGoods.Name} додано до кошика!");
                UpdateCartLabels();
            }
            else
            {
                MessageBox.Show($"Товар не знайдено!");
            }
        }


        private void pbGrilledSausages_DoubleClick(object sender, EventArgs e)
        {
            AddSnackToCart("Грильовані ковбаски");
        }

        private void pbFrenchFries_DoubleClick(object sender, EventArgs e)
        {
            AddSnackToCart("Картопля фрі");
        }

        private void pbOnionRings_DoubleClick(object sender, EventArgs e)
        {
            AddSnackToCart("Цибулеві кільця");
        }

        private void pbChickenNuggets_DoubleClick(object sender, EventArgs e)
        {
            AddSnackToCart("Курячі нагетси");
        }

        private void pbNachoswithCheese_DoubleClick(object sender, EventArgs e)
        {
            AddSnackToCart("Начос з сиром");
        }

        private void pbBBQWings_DoubleClick(object sender, EventArgs e)
        {
            AddSnackToCart("Крильця BBQ");
        }
    }
}
