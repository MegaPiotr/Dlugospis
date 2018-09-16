using Models.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataBase
{
    public class Debt
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Contact))]
        public int PersonId { get; set; }
        public OwnerRole OwnerRole { get;set;}
        public string Description { get; set; }
        public double Money { get; set; }
        public string Image { get; set; }

        [OneToOne]
        public Contact Person { get; set; }
    }
}
