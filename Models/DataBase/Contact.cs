using Prism.Mvvm;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataBase
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nick { get; set; }
        //public byte[] Photo { get; set; }
        public string BankNumber { get; set; }
        [Ignore]
        public string FullName => Name + " " + Surname + " (" + Nick + ")";
    }
}
