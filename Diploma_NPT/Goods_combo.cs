using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_NPT
{
    public class Goods_combo
    {
        private int iD;
        private string name;
        private int number;
        private decimal total_price;

        public int ID { get => iD; }
        public string Name { get => name; }
        public int Number { get => number; }
        public decimal Total_price { get => total_price; }

        public Goods_combo(int iD, string name, string products_name, int number, decimal total_price)
        {
            this.iD = iD;
            this.name = name;
            this.number = number;
            this.total_price = total_price;
        }

        public Goods_combo(string info)
        {
            if (info != null)
            {
                string[] values = info.Split('|');

                if (info.Length >= 4)
                {
                    try { iD = Convert.ToInt32(values[0]); } catch (Exception ex) { iD = -1; }
                    try { name = values[1]; } catch (Exception ex) { name = ""; }
                    try { number = Convert.ToInt32(values[2]); } catch (Exception ex) { number = 0; }
                    try { total_price = Convert.ToDecimal(values[3]); } catch (Exception ex) { total_price = 0; }
                }
            }
        }
    }
}