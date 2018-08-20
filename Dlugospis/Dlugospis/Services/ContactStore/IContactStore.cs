using Models.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Dlugospis.Services.ContactStore
{
    public interface IContactStore: IInitializable
    {
        ObservableCollection<Contact> FilteredContacts { get; }

        Task FilterAsync(Func<Contact, bool> filter = null);
        Task<Contact> GetContactAsync(int id);
        Task<int> SeveContactAsync(Contact contact);
        Task<int> DeleteContactAsync(Contact contact);
     
    }
}
