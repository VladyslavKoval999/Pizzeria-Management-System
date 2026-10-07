using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_NPT
{
    public class DishesToOrder
    {
        private int iD;
        private int id_order;
        private int id_order_online;
        private int id_goods;
        private int number;
        private decimal price;

        public int ID { get => iD; }
        public int ID_order { get => id_order; }
        public int ID_order_online { get => id_order_online; }
        public int ID_goods { get => id_goods; }
        public int Number { get => number; }
        public decimal Price { get => price; }

        public DishesToOrder(int iD, int id_order, int id_order_online, int id_goods, int number, decimal price)
        {
            this.iD = iD;
            this.id_order = id_order;
            this.id_order_online = id_order_online;
            this.id_goods = id_goods;
            this.number = number;
            this.price = price;
        }

        public DishesToOrder(string info)
        {
            if (info != null)
            {
                string[] values = info.Split('|');

                if (info.Length >= 4)
                {
                    try { iD = Convert.ToInt32(values[0]); } catch (Exception ex) { iD = -1; }
                    try { id_order = Convert.ToInt32(values[1]); } catch (Exception ex) { id_order = -1; }
                    try { id_order_online = Convert.ToInt32(values[2]); } catch (Exception ex) { id_order_online = 0; }
                    try { id_goods = Convert.ToInt32(values[3]); } catch (Exception ex) { id_goods = -1; }
                    try { number = Convert.ToInt32(values[4]); } catch (Exception ex) { number = 0; }
                    try { price = Convert.ToDecimal(values[5], CultureInfo.InvariantCulture); } catch (Exception ex) { price = 0; }
                }
            }
        }
    }
}
