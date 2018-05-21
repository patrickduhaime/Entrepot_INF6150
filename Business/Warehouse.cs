using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    class Warehouse
    {
        //Instances variables

        private int Id { get; set; }
        private String Name { get; set; } // max 50 characters
        private String Address { get; set; }
        private int RowTotal { get; set; }
        private int ColumnTotal { get; set; }
        private int FloorTotal { get; set; }

        //Constructor
        public Warehouse(int Id, String Name, String Address, int RowTotal, int ColumnTotal, int FloorTotal)
        {
            this.Id = Id;
            this.Name = Name;
            this.Address = Address;
            this.RowTotal = RowTotal;
            this.ColumnTotal = ColumnTotal;
            this.FloorTotal = FloorTotal;
        }

        public virtual string ToString()
        {
            String result = 
                "WarehouseID: " + Id +"\n"+
                "Warehouse Name: " + Name + "\n" +
                "Warehouse Address: " + Address + "\n" +
                "Capacity: " + RowTotal*ColumnTotal*FloorTotal;

            return result;
        }

    }
}
