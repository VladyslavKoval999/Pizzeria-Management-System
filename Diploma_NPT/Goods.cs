using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_NPT
{
    public class Goods
    {
        private int iD;
        private string name;
        private int number;
        private string size;
        private decimal price;
        private string popularity;
        private string categories;
        private string description;
        private string image;
        private string type;

        public int ID { get => iD; }
        public string Name { get => name; }
        public int Number { get => number; }
        public string Size { get => size; }
        public decimal Price { get => price; }
        public string Popularity { get => popularity; }
        public string Category { get => categories; }
        public string Description { get => description; }
        public string Image { get => image; }

        public string Type { get => type; }

        public Goods(int iD, string name, int number, string size, decimal price, string popularity, string categories, string description, string image, string type)
        {
            this.iD = iD;
            this.name = name;
            this.number = number;
            this.size = size;
            this.price = price;
            this.popularity = popularity;
            this.categories = categories;
            this.description = description;
            this.image = image;
            this.type = type;
        }

        public Goods(string info)
        {
            if (info != null)
            {
                string[] values = info.Split('|');

                if (info.Length >= 4)
                {
                    try { iD = Convert.ToInt32(values[0]); } catch (Exception ex) { iD = -1; }
                    try { name = values[1]; } catch (Exception ex) { name = ""; }
                    try { number = Convert.ToInt32(values[2]); } catch (Exception ex) { number = 0; }
                    try { size = values[3]; } catch (Exception ex) { size = ""; }
                    try { price = Convert.ToDecimal(values[4]); } catch (Exception ex) { price = 0; }

                    if (info.Length >= 6)
                    {
                        try { popularity = values[5]; } catch (Exception ex) { popularity = ""; }
                        try { categories = values[6]; } catch (Exception ex) { categories = ""; }
                        try { description = values[7]; } catch (Exception ex) { description = ""; }
                        try { image = values[8]; } catch (Exception ex) { image = ""; }
                        try { type = values[9]; } catch (Exception ex) { type = ""; }
                    }
                }
            }
        }
    }
}
