using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diploma_NPT.formPizza;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Diploma_NPT
{
    public partial class formCombo : Form
    {
        public formCombo()
        {
            InitializeComponent();
        }

        private void formCombo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";

        public List<Goods> goods = new List<Goods>();
        public List<Goods_combo> goods_combo = new List<Goods_combo>();
        public List<Combos> combos = new List<Combos>();

        void LoadData()
        {
            try { db.Execute<Goods_combo>(file_db, "select id_combo, name_combo, number, total_price from goods_combo", ref goods_combo); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { db.Execute<Goods>(file_db, "select id_goods, product_name, number, size, price, popularity, categories, description, images, type from goods", ref goods); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { db.Execute<Combos>(file_db, "select id_combos, id_combo, id_goods from combos", ref combos); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            var controls = new Dictionary<PictureBox, System.Windows.Forms.Label>
            {
                { pbBurgerCombo, labBC },
                { pbVegeterianHappiness, labVH },
                { pbClassicCombo, labCC },
                { pbSeafoodDelight, labSD },
                { pbPizzaCombo, labPC },
                { pbSnackCombo, labSC }
            };

            string GetToolTipText(System.Windows.Forms.Label label)
            {
                var product = goods_combo.FirstOrDefault(g => g.Name == label.Text);

                var description = from gc in goods_combo
                                  join c in combos on gc.ID equals c.Id_combo
                                  join g in goods on c.Id_goods equals g.ID
                                  where gc.Name == label.Text
                                  group new { gc, g } by new { gc.ID, gc.Name, gc.Number, gc.Total_price } into grouped
                                  select new
                                  {
                                      id_combo = grouped.Key.ID,
                                      product_names = string.Join(", ", grouped.Select(x => x.g.Name)),
                                      number = grouped.Key.Number,
                                      total_price = grouped.Key.Total_price
                                  };

                if (product != null)
                {
                    foreach (var item in description)
                    {
                        return $"Опис: {item.product_names}" +
                               $"\nКількість: {item.number}" +
                               $"\nЦіна: {item.total_price}";
                    }
                }

                return "Товар не знайдено";
            }

            foreach (var controlPair in controls) toolTip1.SetToolTip(controlPair.Key, GetToolTipText(controlPair.Value));
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

        private void AddComboToCart(string comboName)
        {
            List<Goods_combo> comboDetails = new List<Goods_combo>();
            string escapedComboName = comboName.Replace("'", "''");

            string query = $"select id_combo, name_combo, number, total_price from goods_combo where name_combo = '{escapedComboName}'";

            try
            {
                db.Execute<Goods_combo>(file_db, query, ref comboDetails);
                if (comboDetails.Count > 0)
                {
                    decimal comboPrice = comboDetails[0].Total_price;
                    var existingItem = CartManager.CartItems.FirstOrDefault(item => item.Name == comboName);

                    if (existingItem != null) existingItem.Quantity++;
                    else CartManager.CartItems.Add(new CartItem(comboName, comboPrice, 1));

                    MessageBox.Show($"{comboName} додано до кошика!");
                    UpdateCartLabels();
                }

                else MessageBox.Show("Товар не знайдено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при виконанні запиту до goods_combo: {ex.Message}\nЗапит: {query}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void formCombo_Load(object sender, EventArgs e)
        {
            LoadData();
            UpdateCartLabels();
        }

        private void pbBurgerCombo_DoubleClick(object sender, EventArgs e)
        {
            AddComboToCart("Бургер комбо");
        }

        private void pbVegeterianHappiness_DoubleClick(object sender, EventArgs e)
        {
            AddComboToCart("Вегетаріанське щастя");
        }

        private void pbClassicCombo_DoubleClick(object sender, EventArgs e)
        {
            AddComboToCart("Класичне комбо");
        }

        private void pbSeafoodDelight_DoubleClick(object sender, EventArgs e)
        {
            AddComboToCart("Морське задоволення");
        }

        private void pbPizzaCombo_DoubleClick(object sender, EventArgs e)
        {
            AddComboToCart("Піца комбо");
        }

        private void pbSnackCombo_DoubleClick(object sender, EventArgs e)
        {
            AddComboToCart("Снекове комбо");
        }
    }
}
