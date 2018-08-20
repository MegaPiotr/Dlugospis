using Models.DataBase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlugospis.Services.DatabaseConnectionService
{
    public interface IDatabaseConnectionService: IInitializable
    {
        SQLiteAsyncConnection Database { get; }
        AsyncTableQuery<Contact> ContactTable { get; }
        AsyncTableQuery<Debt> DebtTable { get; }
    }
}
