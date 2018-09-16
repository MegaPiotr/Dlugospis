using Models.DataBase;
using Nito.Mvvm;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dlugospis.Services.DatabaseConnectionService
{
    public class DatabaseConnectionService : IDatabaseConnectionService
    {
        
        private const string databaseFile = "DlugospisSQLite.db3";

        public DatabaseConnectionService()
        {
            InitializeTask = NotifyTask.Create(CreateDatabaseAsync);
        }

        private SQLiteAsyncConnection database;
        public SQLiteAsyncConnection Database => database;

        public AsyncTableQuery<Contact> ContactTable => database.Table<Contact>();

        public AsyncTableQuery<Debt> DebtTable => database.Table<Debt>();

        public NotifyTask InitializeTask { get; private set; }

        private async Task CreateDatabaseAsync()
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseFile);
            database = new SQLiteAsyncConnection(dbpath);
            await database.CreateTableAsync<Debt>();
            await database.CreateTableAsync<Contact>();
        }
    }
}
