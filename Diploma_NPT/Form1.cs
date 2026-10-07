using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diploma_NPT.formPizza;

namespace Diploma_NPT
{
    public partial class formHomePage : Form
    {
        public formHomePage()
        {
            InitializeComponent();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";

        private void TransitionFormPizza()
        {
            this.Hide();
            formPizza formPizza = new formPizza();
            formPizza.Show();
        }

        private void TransitionFormСombo()
        {
            this.Hide();
            formCombo formCombo = new formCombo();
            formCombo.Show();
        }
        
        private void TransitionFormBurger()
        {
            this.Hide();
            formBurgers formBurgers = new formBurgers();
            formBurgers.Show();
        }

        private void TransitionFormSnack()
        {
            this.Hide();
            formSnacks formSnacks = new formSnacks();
            formSnacks.Show();
        }

        private void TransitionFormDrink()
        {
            this.Hide();
            formDrinks formDrinks = new formDrinks();
            formDrinks.Show();
        }

        private void formHomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {
            if (msMainMenu.Visible == false) msMainMenu.Visible = true;
            else msMainMenu.Visible = false;
        }

        private void tmsiPizza_Click(object sender, EventArgs e)
        {
            TransitionFormPizza();
        }

        private void tsmiCombo_Click(object sender, EventArgs e)
        {
            TransitionFormСombo();
        }
        
        private void tsmiBurger_Click(object sender, EventArgs e)
        {
            TransitionFormBurger();
        }
        private void tsmiSnack_Click(object sender, EventArgs e)
        {
            TransitionFormSnack();
        }

        private void tsmiDrink_Click(object sender, EventArgs e)
        {
            TransitionFormDrink();
        }

        private void btnShares_Click(object sender, EventArgs e)
        {
            this.Hide();
            formShares formShares = new formShares();
            formShares.Show();
        }

        private void btnPizza_Click(object sender, EventArgs e)
        {
            TransitionFormPizza();
        }

        private void btnCombo_Click(object sender, EventArgs e)
        {
            TransitionFormСombo();
        }

        private void btnBurger_Click(object sender, EventArgs e)
        {
            TransitionFormBurger();
        }

        private void btnSnack_Click(object sender, EventArgs e)
        {
            TransitionFormSnack();
        }

        private void btnDrink_Click(object sender, EventArgs e)
        {
            TransitionFormDrink();
        }

        private void pbCart_Click(object sender, EventArgs e)
        {
            this.Hide();
            formCart formCart = new formCart();
            formCart.Show();
        }

        private void tsmiReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            formReport formReport = new formReport();
            formReport.Show();
        }

        private void UpdateReportButtonState()
        {
            if (UserSession.CurrentUser != null) tsmiReport.Enabled = UserSession.CurrentUser.Login == "admin" || UserSession.CurrentUser.Login == "manager";
            else tsmiReport.Enabled = false;
        }

        string[] fileImages;
        int currentIndex = 0;

        private void LoadCurrentImage()
        {
            string current_path = Directory.GetCurrentDirectory() + "\\Photo_cursach\\Advertising";

            if (Directory.Exists(current_path))
            {
                fileImages = Directory.GetFiles(current_path);
            }

            else
            {
                MessageBox.Show("Директорія 'Photo_cursach/Advertising' не знайдена!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fileImages == null || fileImages.Length == 0)
            {
                MessageBox.Show("У директорії немає зображень!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int index1 = (currentIndex) % fileImages.Length;
                int index2 = (currentIndex + 1) % fileImages.Length;
                int index3 = (currentIndex + 2) % fileImages.Length;

                if (File.Exists(fileImages[index1])) pb1Home.Image = Image.FromFile(fileImages[index1]);
                else pb1Home.Image = null;

                if (File.Exists(fileImages[index2])) pb2Home.Image = Image.FromFile(fileImages[index2]);
                else pb2Home.Image = null;

                if (File.Exists(fileImages[index3])) pb3Home.Image = Image.FromFile(fileImages[index3]);
                else pb3Home.Image = null;
            }

            catch
            {
                MessageBox.Show("Некоректне відображення фото!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (fileImages != null && fileImages.Length > 0)
            {
                currentIndex = (currentIndex - 1 + fileImages.Length) % fileImages.Length;
                LoadCurrentImage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (fileImages != null && fileImages.Length > 0)
            {
                currentIndex = (currentIndex + 1) % fileImages.Length;
                LoadCurrentImage();
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

        private void formHomePage_Load(object sender, EventArgs e)
        {
            UpdateReportButtonState();
            LoadCurrentImage();
            UpdateCartLabels();
        }

        private void formHomePage_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) UpdateReportButtonState();
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
