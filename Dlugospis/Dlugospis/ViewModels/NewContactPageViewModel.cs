using Dlugospis.Services.ContactStore;
using Models.DataBase;
using Nito.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dlugospis.ViewModels
{
    public class NewContactPageViewModel : BindableBase
    {
        public AsyncCommand AddContactCommand { get; private set; }
        private readonly IContactStore _contactStore;
        private readonly INavigationService _navigationService;
        public Contact Contact
        {
            get; set;
        }
        public NewContactPageViewModel(IContactStore store, INavigationService navigationService)
        {
            _contactStore = store;
            _navigationService = navigationService;
            AddContactCommand = new AsyncCommand(AddContact);
            Contact = new Contact();
        }

        private async Task AddContact()
        {
            try
            {
                await _contactStore.SeveContactAsync(Contact);
            }catch(Exception ex)
            {

            }
            await _navigationService.GoBackAsync();
        }
    }
}
