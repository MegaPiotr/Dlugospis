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

namespace Dlugospis.Services.ContactStore
{
    public class ContactStore : BindableBase, IContactStore
    {
        private ObservableCollection<Contact> _filteredContacts;
        private readonly IDatabaseConnectionService _databaseConnection;
  
        public ContactStore(IDatabaseConnectionService databaseConnection)
        {
            _databaseConnection = databaseConnection;
            NotifyTask = NotifyTask.Create(InitializeAsync);
        }

        public ObservableCollection<Contact> FilteredContacts
        {
            get { return _filteredContacts; }
            set { SetProperty(ref _filteredContacts, value); }
        }

        public NotifyTask NotifyTask { get; private set; }

        public async Task FilterAsync(Func<Contact, bool> filter = null)
        {
            List<Contact> filtered;
            if (filter == null)
                filtered = await _databaseConnection.ContactTable.ToListAsync();
            else
                filtered = await _databaseConnection.ContactTable.Where((c) => filter.Invoke(c)).ToListAsync();
            FilteredContacts = new ObservableCollection<Contact>(filtered);
        }
        public async Task<Contact> GetContactAsync(int id)
        {
            return await _databaseConnection.ContactTable.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> SeveContactAsync(Contact contact)
        {
            if (contact.Id != 0)
            {
                return await _databaseConnection.Database.UpdateAsync(contact);
            }
            else
                return await _databaseConnection.Database.InsertAsync(contact);
        }
        public async Task<int> DeleteContactAsync(Contact contact)
        {
            return await _databaseConnection.Database.DeleteAsync(contact);
        }

        private async Task InitializeAsync()
        {
            if (!_databaseConnection.NotifyTask.IsSuccessfullyCompleted)
                await _databaseConnection.NotifyTask.Task;
            int count = await _databaseConnection.ContactTable.CountAsync();

            var contacts = await _databaseConnection.ContactTable.ToListAsync();
            FilteredContacts = new ObservableCollection<Contact>(contacts);
        }
    }
}
