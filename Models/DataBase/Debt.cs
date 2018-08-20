using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataBase
{
    public class Debt
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public Contact Giver { get; set; }
        //public Contact Recipient { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
