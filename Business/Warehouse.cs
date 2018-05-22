using System;

namespace Business
{
    public class Warehouse
    {
        //Instances variables

        public int Id { get; set; }
        public String Name { get; set; } // max 50 characters
        public String Address { get; set; }
        public int RowTotal { get; set; }
        public int ColumnTotal { get; set; }
        public int FloorTotal { get; set; }

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
                "WarehouseID: " + Id + "\n" +
                "Warehouse Name: " + Name + "\n" +
                "Warehouse Address: " + Address + "\n" +
                "Capacity: " + RowTotal * ColumnTotal * FloorTotal;

            return result;
        }
    }
}