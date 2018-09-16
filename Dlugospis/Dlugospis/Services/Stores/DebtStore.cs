using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Dlugospis.Services.DatabaseConnectionService;
using Models.DataBase;
using Nito.Mvvm;
using Prism.Mvvm;
using SQLiteNetExtensionsAsync.Extensions;

namespace Dlugospis.Services.Stores
{
    public class DebtStore : BindableBase,IStore<Debt>
    { 
        private readonly IDatabaseConnectionService _databaseConnection;

        public DebtStore(IDatabaseConnectionService databaseConnection)
        {
            _databaseConnection = databaseConnection;
            InitializeTask = NotifyTask.Create(InitializeAsync);
        }

        private ObservableCollection<Debt> _debts;
        public ObservableCollection<Debt> Content
        {
            get { return _debts; }
            set { SetProperty(ref _debts, value); }
        }

        public NotifyTask InitializeTask { get; private set; }

        #region IStore<Debt> methods
        public async Task<Debt> GetAsync(int id)
        {
            return await _databaseConnection.DebtTable.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SeveAsync(Debt debt)
        {
            int status = 0;
            if (debt.Id != 0)
                status = await _databaseConnection.Database.UpdateAsync(debt);
            else
                status = await _databaseConnection.Database.InsertAsync(debt);
            if (status >= 0)
            {
                await WriteOperations.UpdateWithChildrenAsync(_databaseConnection.Database,debt);
                await UpdateDebtsAsync();
            }
            return status;
        }

        public async Task<int> DeleteAsync(Debt debt)
        {
            return await _databaseConnection.Database.DeleteAsync(debt);
        }

        private async Task UpdateDebtsAsync()
        {
            var debts = await ReadOperations.GetAllWithChildrenAsync<Debt>(_databaseConnection.Database);
            Content = new ObservableCollection<Debt>(debts);
        }
        #endregion

        private async Task InitializeAsync()
        {
            if (!_databaseConnection.InitializeTask.IsSuccessfullyCompleted)
                await _databaseConnection.InitializeTask.Task;

            await UpdateDebtsAsync();
        }
    }
}
