using Dlugospis.Services.DatabaseConnectionService;
using Models.DataBase;
using Nito.Mvvm;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dlugospis.Services.Stores
{
    public class ContactStore : BindableBase, IStore<Contact>
    {
        
        private readonly IDatabaseConnectionService _databaseConnection;
  
        public ContactStore(IDatabaseConnectionService databaseConnection)
        {
            _databaseConnection = databaseConnection;
            InitializeTask = NotifyTask.Create(InitializeAsync);
        }

        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Content
        {
            get { return _contacts; }
            set { SetProperty(ref _contacts, value); }
        }

        public NotifyTask InitializeTask { get; private set; }

        #region IStore<Contact> methods
        public async Task<Contact> GetAsync(int id)
        {
            return await _databaseConnection.ContactTable.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SeveAsync(Contact contact)
        {
            int status = 0;
            if (contact.Id != 0)
                status= await _databaseConnection.Database.UpdateAsync(contact);
            else
                status = await _databaseConnection.Database.InsertAsync(contact);
            if (status >= 0)
                await UpdateContactsAsync();
            return status;
        }

        public async Task<int> DeleteAsync(Contact contact)
        {
            return await _databaseConnection.Database.DeleteAsync(contact);
        }

        private async Task UpdateContactsAsync()
        {
            var contacts = await _databaseConnection.ContactTable.ToListAsync();
            Content = new ObservableCollection<Contact>(contacts);
        }
        #endregion

        private async Task InitializeAsync()
        {
            if (!_databaseConnection.InitializeTask.IsSuccessfullyCompleted)
                await _databaseConnection.InitializeTask.Task;

            await UpdateContactsAsync();
        }
    }
}
