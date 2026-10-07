using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_NPT
{
    public class Combos
    {
        private int iD;
        private int id_combo;
        private int id_goods;

        public int ID { get => iD; }
        public int Id_combo { get => id_combo; }
        public int Id_goods { get => id_goods; }

        public Combos(int iD, int id_combo, int id_goods)
        {
            this.iD = iD;
            this.id_combo = id_combo;
            this.id_goods = id_goods;
        }

        public Combos(string info)
        {
            if (info != null)
            {
                string[] values = info.Split('|');

                if (info.Length >= 1)
                {
                    try { iD = Convert.ToInt32(values[0]); } catch (Exception ex) { iD = -1; }
                    try { id_combo = Convert.ToInt32(values[1]); } catch (Exception ex) { id_combo = -1; }
                    try { id_goods = Convert.ToInt32(values[2]); } catch (Exception ex) { id_goods = -1; }
                }
            }
        }
    }
}
