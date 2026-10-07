using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Diploma_NPT
{
    public class OrderingInAnInstitution
    {
        private int iD_order;
        private DateTime date_of_order;
        private int id_employee;

        public int ID_order { get => iD_order; }
        public DateTime Date_of_order { get => date_of_order; }
        public int ID_employee { get => id_employee; }

        public OrderingInAnInstitution(int iD_order, DateTime date_of_order, int id_employee)
        {
            this.iD_order = iD_order;
            this.date_of_order = date_of_order;
            this.id_employee = id_employee;
        }

        public OrderingInAnInstitution(string info)
        {
            if (info != null)
            {
                string[] values = info.Split('|');

                if (info.Length >= 1)
                {
                    try { iD_order = Convert.ToInt32(values[0]); } catch (Exception ex) { iD_order = -1; }
                    try { date_of_order = Convert.ToDateTime(values[1]); } catch (Exception ex) { date_of_order = DateTime.Today; }
                    try { id_employee = Convert.ToInt32(values[2]); } catch (Exception ex) { id_employee = -1; }
                }
            }
        }
    }
}
