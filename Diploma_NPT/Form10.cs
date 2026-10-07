using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Diploma_NPT.formPizza;

namespace Diploma_NPT
{
    public partial class formCart : Form
    {
        public formCart()
        {
            InitializeComponent();
            dgvCart.MultiSelect = false;
        }
        
        private void formCart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";

        List<Goods> globalgoods = new List<Goods>();
        List<Goods_combo> goods_combo = new List<Goods_combo>();
        List<Combos> combos = new List<Combos>();
        List<OrderingInAnInstitution> orderings = new List<OrderingInAnInstitution>();
        List<DishesToOrder> dishes = new List<DishesToOrder>();

        void LoadData()
        {
            try { db.Execute<Goods>(file_db, "select id_goods, product_name, number, size, price, popularity, categories, description, images, type from goods", ref globalgoods); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { db.Execute<Goods_combo>(file_db, "select id_combo, name_combo, number, total_price from goods_combo", ref goods_combo); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { db.Execute<Combos>(file_db, "select id_combos, id_combo, id_goods from combos", ref combos); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { db.Execute<OrderingInAnInstitution>(file_db, "select id_order, date_of_order, id_employee from ordering_in_an_institution", ref orderings); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { db.Execute<DishesToOrder>(file_db, "select id_meal, id_order, id_order_online, id_goods, number, price_for_one from dishes_to_order", ref dishes); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvCart.Rows.Clear();
            lbPrice.Text = "0000,00";
            CartManager.CartItems.Clear();
            selectedCartItem = null;
            LoadCartItems();
            UpdateCartLabels();
        }

        private void btnContinueShopping_Click(object sender, EventArgs e)
        {
            this.Hide();
            formHomePage formHomePage = new formHomePage();
            formHomePage.Show();
        }

        private void LoadCartItems()
        {
            dgvCart.Rows.Clear();

            foreach (var item in CartManager.CartItems)
            {
                decimal itemPrice = item.Price;
                decimal itemTotalBeforeDiscount = item.Price * item.Quantity;
                decimal itemDiscount = 0m;
                decimal itemTotalAfterDiscount = itemTotalBeforeDiscount;

                List<Goods_combo> comboDetails = new List<Goods_combo>();

                string comboQuery = $"SELECT id_combo, name_combo, id_goods, number, total_price FROM goods_combo WHERE name_combo = '{item.Name}' LIMIT 1";

                try
                {
                    db.Execute<Goods_combo>(file_db, comboQuery, ref comboDetails);

                    if (comboDetails.Count > 0)
                    {
                        itemPrice = comboDetails[0].Total_price;
                        itemTotalBeforeDiscount = itemPrice * item.Quantity;
                        itemTotalAfterDiscount = itemTotalBeforeDiscount;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка запиту до goods_combo: {ex.Message}\nЗапит: {comboQuery}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                List<Goods> goods = new List<Goods>();

                string goodsQuery = $"SELECT id_goods, product_name, number, size, price, popularity, categories, description, images, type FROM goods WHERE product_name = '{item.Name.Replace(" (подарунок)", "")}'";

                try
                {
                    db.Execute<Goods>(file_db, goodsQuery, ref goods);

                    if (goods.Count > 0 && goods[0].Category == "Піца" && !item.Name.Contains("(подарунок)"))
                    {
                        itemPrice = goods[0].Price;
                        itemTotalBeforeDiscount = itemPrice * item.Quantity;
                        itemDiscount = itemPrice * 0.15m * item.Quantity;
                        itemTotalAfterDiscount = itemTotalBeforeDiscount - itemDiscount;
                    }

                    else if (item.Name == "Діабло (подарунок)")
                    {
                        itemPrice = 0m;
                        itemTotalBeforeDiscount = 0m;
                        itemDiscount = 0m;
                        itemTotalAfterDiscount = 0m;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка запиту до goods: {ex.Message}\nЗапит: {goodsQuery}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                dgvCart.Rows.Add(item.Name, itemPrice.ToString("F2"), item.Quantity, itemDiscount.ToString("F2"), itemTotalAfterDiscount.ToString("F2"));
            }
        }


        private void ApplyCoffeePromotion()
        {
            int coffeeCount = 0;

            foreach (var item in CartManager.CartItems)
            {
                List<Goods> drinkGoods = new List<Goods>();
                db.Execute<Goods>(file_db, "SELECT * FROM goods WHERE product_name = '" + item.Name + "' AND categories = 'Напої' AND type = 'Кава'", ref drinkGoods);

                if (drinkGoods.Count > 0)
                    coffeeCount += item.Quantity;
            }

            var diabloGift = CartManager.CartItems.FirstOrDefault(item => item.Name == "Діабло (подарунок)");
            var diablo = globalgoods.FirstOrDefault(g => g.Name == "Діабло");

            Dictionary<string, int> itemQuantities = new Dictionary<string, int>();
            foreach (var item in CartManager.CartItems)
            {
                string itemName = item.Name == "Діабло (подарунок)" ? "Діабло" : item.Name;
                var stockGood = globalgoods.FirstOrDefault(g => g.Name == itemName);
                if (stockGood != null)
                {
                    if (!itemQuantities.ContainsKey(stockGood.Name))
                        itemQuantities[stockGood.Name] = 0;
                    itemQuantities[stockGood.Name] += item.Quantity;
                }
                else
                {
                    var combo = goods_combo.FirstOrDefault(gc => gc.Name == itemName);
                    if (combo != null)
                    {
                        var comboItems = from c in combos
                                         join g in globalgoods on c.Id_goods equals g.ID
                                         where c.Id_combo == combo.ID
                                         select g;
                        foreach (var comboItem in comboItems)
                        {
                            if (!itemQuantities.ContainsKey(comboItem.Name))
                                itemQuantities[comboItem.Name] = 0;
                            itemQuantities[comboItem.Name] += item.Quantity;
                        }
                    }
                }
            }

            if (coffeeCount >= 2 && diablo != null)
            {
                int currentDiabloQuantity = itemQuantities.ContainsKey("Діабло") ? itemQuantities["Діабло"] : 0;
                if (diabloGift == null)
                {
                    if (currentDiabloQuantity >= diablo.Number)
                    {
                        MessageBox.Show("Додано 'Діабло (подарунок)'. Кількість звичайної 'Діабло' зменшено на 1 через ліміт запасів.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var regularDiablo = CartManager.CartItems.FirstOrDefault(item => item.Name == "Діабло");
                        if (regularDiablo != null && regularDiablo.Quantity > 0)
                        {
                            regularDiablo.Quantity--;
                            if (regularDiablo.Quantity == 0)
                                CartManager.CartItems.Remove(regularDiablo);
                            CartManager.CartItems.Add(new CartItem { Name = "Діабло (подарунок)", Price = 0m, Quantity = 1 });
                        }
                    }
                    else
                    {
                        CartManager.CartItems.Add(new CartItem { Name = "Діабло (подарунок)", Price = 0m, Quantity = 1 });
                    }
                }
                else
                {
                    diabloGift.Quantity = 1;
                }
            }
            else if (diabloGift != null)
            {
                CartManager.CartItems.Remove(diabloGift);
            }

            LoadCartItems();
            UpdateCartLabels();
        }

        private decimal CalculateTotalPriceWithDiscount()
        {
            decimal totalPrice = 0m;

            foreach (var item in CartManager.CartItems)
            {
                List<Goods_combo> comboDetails = new List<Goods_combo>();
                db.Execute<Goods_combo>(file_db, $"select id_combo, name_combo, number, total_price from goods_combo where name_combo = '{item.Name}'", ref comboDetails);

                if (comboDetails.Count > 0)
                {
                    totalPrice += comboDetails[0].Total_price * item.Quantity;
                }
                else
                {
                    List<Goods> goods = new List<Goods>();
                    db.Execute<Goods>(file_db, $"SELECT id_goods, product_name, number, size, price, popularity, categories, description, images, type FROM goods WHERE product_name = '{item.Name.Replace(" (подарунок)", "")}'", ref goods);

                    if (goods.Count > 0 && goods[0].Category == "Піца" && !item.Name.Contains("(подарунок)"))
                    {
                        decimal discountedPrice = goods[0].Price * 0.85m;
                        totalPrice += discountedPrice * item.Quantity;
                    }
                    else if (item.Name == "Діабло (подарунок)")
                    {
                        totalPrice += 0m;
                    }
                    else if (goods.Count > 0)
                    {
                        totalPrice += goods[0].Price * item.Quantity;
                    }
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
            lbPrice.Text = totalPrice == 0 ? "0000,00" : totalPrice.ToString("F2");
        }

        private void UpdateTotalPrice()
        {
            decimal total = CalculateTotalPriceWithDiscount();
            lbPrice.Text = total == 0 ? "0000,00" : total.ToString("F2");
        }

        private void formCart_Load(object sender, EventArgs e)
        {
            LoadCartItems();
            UpdateTotalPrice();
            UpdateCartLabels();
            LoadData();
        }

        private CartItem selectedCartItem = null;

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string name = dgvCart.Rows[e.RowIndex].Cells[0].Value?.ToString();

                if (!string.IsNullOrEmpty(name))
                {
                    selectedCartItem = CartManager.CartItems.FirstOrDefault(item => item.Name == name);

                    if (selectedCartItem != null)
                    {
                        dgvCart.ClearSelection();
                        dgvCart.Rows[e.RowIndex].Selected = true;
                        dgvCart.CurrentCell = dgvCart.Rows[e.RowIndex].Cells[0];
                    }
                }
            }
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            if (selectedCartItem == null)
            {
                MessageBox.Show("Будь ласка, виберіть товар у кошику, натиснувши на його назву.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = -1;
            if (dgvCart.SelectedRows.Count > 0)
                selectedIndex = dgvCart.SelectedRows[0].Index;

            if (selectedCartItem.Name == "Діабло (подарунок)")
            {
                MessageBox.Show("Неможливо додати більше подарункової піци 'Діабло'.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Dictionary<string, int> itemQuantities = new Dictionary<string, int>();
            foreach (var item in CartManager.CartItems)
            {
                string itemName = item.Name == "Діабло (подарунок)" ? "Діабло" : item.Name;
                var stockGood = globalgoods.FirstOrDefault(g => g.Name == itemName);
                if (stockGood != null)
                {
                    if (!itemQuantities.ContainsKey(stockGood.Name))
                        itemQuantities[stockGood.Name] = 0;
                    itemQuantities[stockGood.Name] += item.Quantity;
                }
                else
                {
                    var combo = goods_combo.FirstOrDefault(gc => gc.Name == itemName);
                    if (combo != null)
                    {
                        var comboItems = from c in combos
                                         join g in globalgoods on c.Id_goods equals g.ID
                                         where c.Id_combo == combo.ID
                                         select g;
                        foreach (var comboItem in comboItems)
                        {
                            if (!itemQuantities.ContainsKey(comboItem.Name))
                                itemQuantities[comboItem.Name] = 0;
                            itemQuantities[comboItem.Name] += item.Quantity;
                        }
                    }
                }
            }

            var selectedGood = globalgoods.FirstOrDefault(g => g.Name == selectedCartItem.Name);
            if (selectedGood != null)
            {
                int currentQuantity = itemQuantities.ContainsKey(selectedGood.Name) ? itemQuantities[selectedGood.Name] : 0;
                if (currentQuantity + 1 > selectedGood.Number)
                {
                    MessageBox.Show($"Товар '{selectedCartItem.Name}' скінчився. Доступно лише {selectedGood.Number} шт.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                var combo = goods_combo.FirstOrDefault(gc => gc.Name == selectedCartItem.Name);
                if (combo != null)
                {
                    var comboItems = from c in combos
                                     join g in globalgoods on c.Id_goods equals g.ID
                                     where c.Id_combo == combo.ID
                                     select g;

                    foreach (var item in comboItems)
                    {
                        int currentQuantity = itemQuantities.ContainsKey(item.Name) ? itemQuantities[item.Name] : 0;
                        if (currentQuantity + 1 > item.Number)
                        {
                            MessageBox.Show($"Товар '{item.Name}' у комбо '{selectedCartItem.Name}' скінчився. Доступно лише {item.Number} шт.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (combo.Number <= selectedCartItem.Quantity)
                    {
                        MessageBox.Show($"Комбо '{selectedCartItem.Name}' скінчилося. Доступно лише {combo.Number} шт.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Товар або комбо '{selectedCartItem.Name}' не знайдено в базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            selectedCartItem.Quantity++;
            ApplyCoffeePromotion();
            LoadCartItems();

            if (selectedIndex >= 0 && selectedIndex < dgvCart.Rows.Count)
            {
                dgvCart.ClearSelection();
                dgvCart.Rows[selectedIndex].Selected = true;
                dgvCart.CurrentCell = dgvCart.Rows[selectedIndex].Cells[0];
                dgvCart.FirstDisplayedScrollingRowIndex = selectedIndex;
            }
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (selectedCartItem == null)
            {
                MessageBox.Show("Будь ласка, виберіть товар у кошику, натиснувши на його назву.");
                return;
            }

            int selectedIndex = -1;
            if (dgvCart.SelectedRows.Count > 0)
                selectedIndex = dgvCart.SelectedRows[0].Index;

            if (!(selectedCartItem.Name == "Діабло (подарунок)"))
            {
                selectedCartItem.Quantity--;
                if (selectedCartItem.Quantity <= 0)
                {
                    CartManager.CartItems.Remove(selectedCartItem);
                    selectedCartItem = null;
                }
            }

            ApplyCoffeePromotion();
            LoadCartItems();

            if (selectedIndex >= 0 && selectedIndex < dgvCart.Rows.Count)
            {
                dgvCart.ClearSelection();
                dgvCart.Rows[selectedIndex].Selected = true;
                dgvCart.CurrentCell = dgvCart.Rows[selectedIndex].Cells[0];
                dgvCart.FirstDisplayedScrollingRowIndex = selectedIndex;
            }
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                var row = dgvCart.Rows[e.RowIndex];
                string name = row.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(name))
                    return;

                int newQuantity;
                if (int.TryParse(row.Cells[2].Value?.ToString(), out newQuantity) && newQuantity > 0)
                {
                    var cartItem = CartManager.CartItems.FirstOrDefault(item => item.Name == name);
                    if (cartItem != null)
                    {
                        if (name == "Діабло (подарунок)")
                        {
                            MessageBox.Show("Неможливо змінити кількість подарункової піци 'Діабло'.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCartItems();
                            return;
                        }

                        Dictionary<string, int> itemQuantities = new Dictionary<string, int>();
                        foreach (var item in CartManager.CartItems)
                        {
                            if (item.Name == name)
                                continue;
                            string itemName = item.Name == "Діабло (подарунок)" ? "Діабло" : item.Name;
                            var stockGood = globalgoods.FirstOrDefault(g => g.Name == itemName);
                            if (stockGood != null)
                            {
                                if (!itemQuantities.ContainsKey(stockGood.Name))
                                    itemQuantities[stockGood.Name] = 0;
                                itemQuantities[stockGood.Name] += item.Quantity;
                            }
                            else
                            {
                                var combo = goods_combo.FirstOrDefault(gc => gc.Name == itemName);
                                if (combo != null)
                                {
                                    var comboItems = from c in combos
                                                     join g in globalgoods on c.Id_goods equals g.ID
                                                     where c.Id_combo == combo.ID
                                                     select g;
                                    foreach (var comboItem in comboItems)
                                    {
                                        if (!itemQuantities.ContainsKey(comboItem.Name))
                                            itemQuantities[comboItem.Name] = 0;
                                        itemQuantities[comboItem.Name] += item.Quantity;
                                    }
                                }
                            }
                        }

                        string checkName = name == "Діабло (подарунок)" ? "Діабло" : name;
                        var selectedGood = globalgoods.FirstOrDefault(g => g.Name == checkName);
                        if (selectedGood != null)
                        {
                            int currentQuantity = itemQuantities.ContainsKey(selectedGood.Name) ? itemQuantities[selectedGood.Name] : 0;
                            if (currentQuantity + newQuantity > selectedGood.Number)
                            {
                                MessageBox.Show($"Товару '{checkName}' залишилося лише {selectedGood.Number} шт.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCartItems();
                                return;
                            }
                        }
                        else
                        {
                            var combo = goods_combo.FirstOrDefault(gc => gc.Name == name);
                            if (combo != null)
                            {
                                var comboItems = from c in combos
                                                 join g in globalgoods on c.Id_goods equals g.ID
                                                 where c.Id_combo == combo.ID
                                                 select g;

                                foreach (var item in comboItems)
                                {
                                    int currentQuantity = itemQuantities.ContainsKey(item.Name) ? itemQuantities[item.Name] : 0;
                                    if (currentQuantity + newQuantity > item.Number)
                                    {
                                        MessageBox.Show($"Товару '{item.Name}' у комбо '{name}' залишилося лише {item.Number} шт.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        LoadCartItems();
                                        return;
                                    }
                                }

                                if (combo.Number < newQuantity)
                                {
                                    MessageBox.Show($"Комбо '{name}' залишилося лише {combo.Number} шт.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadCartItems();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Товар або комбо '{name}' не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                LoadCartItems();
                                return;
                            }
                        }

                        cartItem.Quantity = newQuantity;
                        ApplyCoffeePromotion();
                        LoadCartItems();
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть коректну кількість (ціле число більше 0).", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCartItems();
                }
            }
        }

        private int InsertOrdering(DateTime orderDate, int employeeId, string file_db, ClassDataBase db)
        {
            string query = "INSERT INTO ordering_in_an_institution (date_of_order, id_employee) VALUES ('" + orderDate.ToString("dd.MM.yyyy") + "', " + employeeId + "); " +
                           "SELECT last_insert_rowid();";

            try
            {
                using (var connection = new SqliteConnection($"Data Source={file_db};"))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(query, connection))
                    {
                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при вставці замовлення: {ex.Message}");
                return -1;
            }
        }

        private void InsertDishes(int orderId, DataGridView dgv, string file_db, ClassDataBase db)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["goods"].Value != null && !row.IsNewRow)
                {
                    string productName = row.Cells["goods"].Value.ToString().Replace("'", "''");
                    int number = Convert.ToInt32(row.Cells["number"].Value);
                    decimal total_price = Convert.ToDecimal(row.Cells["total_price"].Value);

                    string priceFormatted = total_price.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    if (!(globalgoods.Where(g => g.Name == productName).FirstOrDefault() == null))
                    {
                        string query_g = $"insert into dishes_to_order (id_order, id_order_online, id_goods, number, price_for_one) " +
                                       $"values ({orderId}, null, (select id_goods from goods where product_name = '{productName}'), {number}, {priceFormatted})";

                        try
                        {
                            int rowsAffected = db.ExecuteNonQuery(file_db, query_g);

                            if (rowsAffected == 0) MessageBox.Show($"Не вдалося додати страву '{productName}' до замовлення.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при вставці страви '{productName}': {ex.Message}");
                        }
                    }

                    else if (productName == "Діабло (подарунок)")
                    {
                        string query_g = $"insert into dishes_to_order (id_order, id_order_online, id_goods, number, price_for_one) " +
                                       $"values ({orderId}, null, (select id_goods from goods where product_name = 'Діабло'), 1, 0)";

                        try
                        {
                            int rowsAffected = db.ExecuteNonQuery(file_db, query_g);

                            if (rowsAffected == 0) MessageBox.Show($"Не вдалося додати страву 'Діабло' до замовлення.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при вставці страви 'Діабло': {ex.Message}");
                        }
                    }

                    else
                    {
                        var comboItems = from gc in goods_combo
                                         join c in combos on gc.ID equals c.Id_combo
                                         join g in globalgoods on c.Id_goods equals g.ID
                                         where gc.Name == productName
                                         select new { g.ID, s = g.Price - (g.Price * (decimal)0.1) };

                        if (comboItems.Any())
                        {
                            foreach (var item in comboItems)
                            {
                                string priceForm = item.s.ToString(System.Globalization.CultureInfo.InvariantCulture);

                                string query_c = $"insert into dishes_to_order (id_order, id_order_online, id_goods, number, price_for_one) " +
                                                $"VALUES ({orderId}, NULL, {item.ID}, {number}, {priceForm})";

                                try
                                {
                                    int rowsAffected = db.ExecuteNonQuery(file_db, query_c);

                                    if (rowsAffected == 0) MessageBox.Show($"Не вдалося додати страву '{productName}' до замовлення.");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Помилка при вставці страви '{productName}': {ex.Message}");
                                }
                            }
                        }

                        else MessageBox.Show($"Комбо '{productName}' не містить товарів у таблиці goods_combo або combos.");
                    }
                }
            }
        }

        private void UpdateNumberGoods()
        {
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["goods"].Value != null && !row.IsNewRow)
                {
                    string productName = row.Cells["goods"].Value.ToString().Replace("'", "''");
                    int number = Convert.ToInt32(row.Cells["number"].Value);

                    if (!(globalgoods.Where(g => g.Name == productName).FirstOrDefault() == null))
                    {
                        string updateGoodsQ = $"UPDATE goods SET number = number - {number} WHERE product_name = '{productName}'";

                        try
                        {
                            int rowsAffeGoods = db.ExecuteNonQuery(file_db, updateGoodsQ);

                            if (rowsAffeGoods == 0) MessageBox.Show($"Не вдалося оновити кількість для товару з product_name = '{productName}'.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при оновленні goods для product_name = '{productName}': {ex.Message}");
                        }

                        var goodsItems = from gc in goods_combo
                                         join c in combos on gc.ID equals c.Id_combo
                                         join g in globalgoods on c.Id_goods equals g.ID
                                         where g.Name == productName
                                         select new
                                         {
                                             ComboId = gc.ID,
                                             ComboName = gc.Name,
                                             ComboNumber = gc.Number,
                                             GoodsNumber = g.Number
                                         };


                        foreach (var item in goodsItems)
                        {
                            if (item.ComboNumber == item.GoodsNumber)
                            {
                                string comboToUpd = $"update goods_combo set number = number - {number} where name_combo = '{item.ComboName}'";

                                try
                                {
                                    int rowsAffeGoodsCombo = db.ExecuteNonQuery(file_db, comboToUpd);

                                    if (rowsAffeGoodsCombo == 0) MessageBox.Show($"Не вдалося оновити кількість для товару з name_combo = '{item.ComboName}'.");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Помилка при оновленні goods_combo для name_combo = '{item.ComboName}': {ex.Message}");
                                }
                            }
                        }
                    }

                    else if (productName == "Діабло (подарунок)")
                    {
                        string updateGoodsQ = $"UPDATE goods SET number = number - 1 WHERE product_name = 'Діабло'";

                        try
                        {
                            int rowsAffeGoods = db.ExecuteNonQuery(file_db, updateGoodsQ);

                            if (rowsAffeGoods == 0) MessageBox.Show($"Не вдалося оновити кількість для товару з product_name = 'Діабло'.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при оновленні goods для product_name = 'Діабло': {ex.Message}");
                        }
                    }

                    else
                    {
                        var comboItems = (from gc in goods_combo
                                          join c in combos on gc.ID equals c.Id_combo
                                          join g in globalgoods on c.Id_goods equals g.ID
                                          where gc.Name == productName
                                          select g.ID).ToList();

                        var comboToUpdate = goods_combo.FirstOrDefault(gc => gc.Name == productName);

                        if (comboToUpdate != null)
                        {
                            string updateGoodsComboQuery = $"UPDATE goods_combo SET number = number - {number} WHERE name_combo = '{productName}'";

                            try
                            {
                                int rowsAffectedCombo = db.ExecuteNonQuery(file_db, updateGoodsComboQuery);

                                if (rowsAffectedCombo == 0) MessageBox.Show($"Не вдалося оновити кількість для комбо '{productName}'.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Помилка при оновленні goods_combo для '{productName}': {ex.Message}");
                            }
                        }

                        else MessageBox.Show($"Комбо '{productName}' не знайдено в таблиці goods_combo.");

                        if (comboItems.Any())
                        {
                            foreach (var idGoods in comboItems)
                            {
                                var goodToUpdate = globalgoods.FirstOrDefault(g => g.ID == idGoods);

                                if (goodToUpdate != null)
                                {
                                    string updateGoodsQuery = $"UPDATE goods SET number = number - {number} WHERE id_goods = {idGoods}";

                                    try
                                    {
                                        int rowsAffectedGoods = db.ExecuteNonQuery(file_db, updateGoodsQuery);

                                        if (rowsAffectedGoods == 0) MessageBox.Show($"Не вдалося оновити кількість для товару з id_goods = {idGoods}.");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Помилка при оновленні goods для id_goods = {idGoods}: {ex.Message}");
                                    }
                                }

                                else MessageBox.Show($"Товар з id_goods = {idGoods} не знайдено в таблиці goods.");
                            }
                        }

                        else MessageBox.Show($"Комбо '{productName}' не містить товарів у таблиці combos.");
                    }
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (CartManager.CartItems.Count == 0)
            {
                MessageBox.Show("Кошик порожній. Додайте товари перед оформленням замовлення.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                bool canPlaceOrder = true;
                List<string> outOfStockMessages = new List<string>();

                ApplyCoffeePromotion();
                LoadCartItems();

                Dictionary<string, int> itemQuantities = new Dictionary<string, int>();
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.Cells["goods"].Value != null && !row.IsNewRow)
                    {
                        string productName = row.Cells["goods"].Value.ToString().Replace("'", "''");
                        int requestedQuantity = Convert.ToInt32(row.Cells["number"].Value);

                        string stockName = productName == "Діабло (подарунок)" ? "Діабло" : productName;

                        var stockGood = globalgoods.FirstOrDefault(g => g.Name == stockName);
                        if (stockGood != null)
                        {
                            if (!itemQuantities.ContainsKey(stockGood.Name))
                                itemQuantities[stockGood.Name] = 0;
                            itemQuantities[stockGood.Name] += requestedQuantity;
                        }
                        else
                        {
                            var combo = goods_combo.FirstOrDefault(gc => gc.Name == productName);
                            if (combo != null)
                            {
                                var comboItems = from c in combos
                                                 join g in globalgoods on c.Id_goods equals g.ID
                                                 where c.Id_combo == combo.ID
                                                 select g;

                                foreach (var item in comboItems)
                                {
                                    if (!itemQuantities.ContainsKey(item.Name))
                                        itemQuantities[item.Name] = 0;
                                    itemQuantities[item.Name] += requestedQuantity;
                                }

                                if (combo.Number < requestedQuantity)
                                {
                                    outOfStockMessages.Add($"Комбо '{productName}' залишилося лише {combo.Number} шт. Ви замовили {requestedQuantity} шт.");
                                    canPlaceOrder = false;
                                }
                            }
                            else
                            {
                                outOfStockMessages.Add($"Товар або комбо '{productName}' не знайдено в базі даних.");
                                canPlaceOrder = false;
                            }
                        }
                    }
                }

                foreach (var kvp in itemQuantities)
                {
                    string itemName = kvp.Key;
                    int totalQuantity = kvp.Value;
                    var stockGood = globalgoods.FirstOrDefault(g => g.Name == itemName);
                    if (stockGood != null)
                    {
                        if (totalQuantity > stockGood.Number)
                        {
                            outOfStockMessages.Add($"Загальна кількість товару '{itemName}' (включаючи комбо та подарунки) становить {totalQuantity} шт., але в наявності лише {stockGood.Number} шт.");
                            canPlaceOrder = false;
                        }
                    }
                    else
                    {
                        outOfStockMessages.Add($"Товар '{itemName}' не знайдено в базі даних.");
                        canPlaceOrder = false;
                    }
                }

                if (!canPlaceOrder)
                {
                    MessageBox.Show(string.Join("\n", outOfStockMessages), "Недостатня кількість товару", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    var diabloGift = CartManager.CartItems.FirstOrDefault(item => item.Name == "Діабло (подарунок)");
                    if (diabloGift != null)
                    {
                        CartManager.CartItems.Remove(diabloGift);
                        ApplyCoffeePromotion();
                    }
                    return;
                }

                int orderId = InsertOrdering(DateTime.Today, 5, file_db, db);

                if (orderId == -1)
                {
                    MessageBox.Show("Не вдалося створити замовлення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InsertDishes(orderId, dgvCart, file_db, db);
                UpdateNumberGoods();
                LoadCartItems();
                UpdateCartLabels();

                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintReceipt);

                string suggestedFileName = $"Receipt{DateTime.Now:yyyyMMdd}";
                pd.DocumentName = suggestedFileName;

                this.Activate();
                MessageBox.Show($"Будь ласка, у діалозі збереження введіть назву файла як '{suggestedFileName}'.", "Інструкція", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PrintDialog printDialog = new PrintDialog
                {
                    Document = pd,
                    AllowSomePages = true
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    pd.Print();
                    btnClear_Click(null, null);
                    this.Activate();
                    MessageBox.Show(this, "Замовлення оформлено! Чек надруковано та збережено як PDF.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnContinueShopping_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при створенні чека: {ex.Message}\nДеталі: {ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Times New Roman", 18, FontStyle.Bold);
            Font subtitleFont = new Font("Times New Roman", 16, FontStyle.Bold);
            Font regularFont = new Font("Arial", 10);
            Font italicFont = new Font("Arial", 9, FontStyle.Italic);
            float yPos = 10;
            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float pageWidth = e.PageBounds.Width;

            Bitmap logo = Properties.Resources._1_logo;
            float logoWidth = 200;
            float logoHeight = 113;
            float logoX = (pageWidth - logoWidth) / 2;
            g.DrawImage(logo, logoX, yPos, logoWidth, logoHeight);
            yPos += logoHeight + 10;

            string title = "Чек замовлення";
            float titleWidth = g.MeasureString(title, subtitleFont).Width;
            g.DrawString(title, subtitleFont, Brushes.Black, (pageWidth - titleWidth) / 2, yPos);
            yPos += subtitleFont.GetHeight(g) + 10;

            string greeting = "Дякуємо за покупку в нашій піцерії!";
            float greetingWidth = g.MeasureString(greeting, italicFont).Width;
            g.DrawString(greeting, italicFont, Brushes.Black, (pageWidth - greetingWidth) / 2, yPos);
            yPos += italicFont.GetHeight(g) + 15;

            string dateTime = $"Дата і час: {DateTime.Now:dd.MM.yyyy HH:mm:ss}";
            g.DrawString(dateTime, regularFont, Brushes.Black, leftMargin, yPos);
            yPos += regularFont.GetHeight(g) + 10;

            g.DrawString(new string('=', 70), regularFont, Brushes.Black, leftMargin, yPos);
            yPos += regularFont.GetHeight(g) + 5;

            g.DrawString("Назва", regularFont, Brushes.Black, leftMargin, yPos);
            g.DrawString("Ціна (грн)", regularFont, Brushes.Black, leftMargin + 200, yPos);
            g.DrawString("Кількість", regularFont, Brushes.Black, leftMargin + 300, yPos);
            g.DrawString("Знижка (грн)", regularFont, Brushes.Black, leftMargin + 400, yPos);
            g.DrawString("Сума (грн)", regularFont, Brushes.Black, leftMargin + 500, yPos);
            yPos += regularFont.GetHeight(g) + 5;

            g.DrawString(new string('=', 70), regularFont, Brushes.Black, leftMargin, yPos);
            yPos += regularFont.GetHeight(g) + 5;

            decimal totalBeforeDiscount = 0m;
            decimal totalAfterDiscount = 0m;
            decimal totalDiscount = 0m;
            bool hasDiscount = false;
            bool hasFreeDiablo = false;

            foreach (var item in CartManager.CartItems)
            {
                decimal itemPrice = item.Price;
                decimal itemTotalBeforeDiscount = item.Price * item.Quantity;
                decimal itemTotalAfterDiscount = itemTotalBeforeDiscount;
                decimal itemDiscount = 0m;

                List<Goods_combo> comboDetails = new List<Goods_combo>();
                db.Execute<Goods_combo>(file_db, $"SELECT id_combo, name_combo, id_goods, number, total_price FROM goods_combo WHERE UPPER(TRIM(name_combo)) = UPPER(TRIM('{item.Name}')) LIMIT 1", ref comboDetails);
                if (comboDetails.Count > 0)
                {
                    itemPrice = comboDetails[0].Total_price;
                    itemTotalBeforeDiscount = itemPrice * item.Quantity;
                    itemTotalAfterDiscount = itemTotalBeforeDiscount;
                }
                else
                {
                    List<Goods> goods = new List<Goods>();
                    db.Execute<Goods>(file_db, $"SELECT id_goods, product_name, number, size, price, popularity, categories, description, images, type FROM goods WHERE product_name = '{item.Name.Replace(" (подарунок)", "")}'", ref goods);
                    if (goods.Count > 0 && goods[0].Category == "Піца" && !item.Name.Contains("(подарунок)"))
                    {
                        itemPrice = goods[0].Price;
                        itemTotalBeforeDiscount = itemPrice * item.Quantity;
                        itemDiscount = itemPrice * 0.15m * item.Quantity;
                        itemTotalAfterDiscount = itemTotalBeforeDiscount - itemDiscount;
                        hasDiscount = true;
                    }
                    else if (item.Name == "Діабло (подарунок)")
                    {
                        itemPrice = 0m;
                        itemTotalBeforeDiscount = 0m;
                        itemDiscount = 0m;
                        itemTotalAfterDiscount = 0m;
                        hasFreeDiablo = true;
                    }
                }

                totalBeforeDiscount += itemTotalBeforeDiscount;
                totalAfterDiscount += itemTotalAfterDiscount;
                totalDiscount += itemDiscount;

                g.DrawString(item.Name, regularFont, Brushes.Black, leftMargin, yPos);
                g.DrawString(itemPrice.ToString("F2"), regularFont, Brushes.Black, leftMargin + 200, yPos);
                g.DrawString(item.Quantity.ToString(), regularFont, Brushes.Black, leftMargin + 300, yPos);
                g.DrawString(itemDiscount.ToString("F2"), regularFont, Brushes.Black, leftMargin + 400, yPos);
                g.DrawString(itemTotalAfterDiscount.ToString("F2"), regularFont, Brushes.Black, leftMargin + 500, yPos);
                yPos += regularFont.GetHeight(g) + 10;
            }

            g.DrawString(new string('=', 70), regularFont, Brushes.Black, leftMargin, yPos);
            yPos += regularFont.GetHeight(g) + 5;

            yPos += regularFont.GetHeight(g);

            string totalText = $"Сума без знижки: {totalBeforeDiscount:F2} грн";
            float totalWidth = g.MeasureString(totalText, regularFont).Width;
            g.DrawString(totalText, regularFont, Brushes.Black, rightMargin - totalWidth, yPos);
            yPos += regularFont.GetHeight(g) + 3;

            string discountText = $"Знижка: {totalDiscount:F2} грн";
            float discountWidth = g.MeasureString(discountText, regularFont).Width;
            g.DrawString(discountText, regularFont, Brushes.Black, rightMargin - discountWidth, yPos);
            yPos += regularFont.GetHeight(g) + 3;

            string amountToPayText = $"До сплати: {totalAfterDiscount:F2} грн";
            float amountToPayWidth = g.MeasureString(amountToPayText, regularFont).Width;
            g.DrawString(amountToPayText, regularFont, Brushes.Black, rightMargin - amountToPayWidth, yPos);
            yPos += regularFont.GetHeight(g) + 3;

            if (hasDiscount || hasFreeDiablo)
            {
                string discountReason = "";
                if (hasDiscount) discountReason += "15% на піцу";
                if (hasFreeDiablo) discountReason += (hasDiscount ? ", " : "") + "Діабло як подарунок (2+ кави)";
                float discountReasonWidth = g.MeasureString(discountReason, regularFont).Width;
                g.DrawString(discountReason, regularFont, Brushes.Black, rightMargin - discountReasonWidth, yPos);
                yPos += regularFont.GetHeight(g) + 15;
            }
            else
            {
                yPos += regularFont.GetHeight(g) + 15;
            }

            string slogan = "Смачного! Чекаємо на вас знову! Любов до піци починається тут!";
            float sloganWidth = g.MeasureString(slogan, italicFont).Width;
            g.DrawString(slogan, italicFont, Brushes.Black, (pageWidth - sloganWidth) / 2, yPos);
            yPos += italicFont.GetHeight(g) + 5;
        }

    }
}
