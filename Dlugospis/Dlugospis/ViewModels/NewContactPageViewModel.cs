using Dlugospis.Services.Stores;
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
        private readonly IStore<Contact> _contactStore;

        private readonly INavigationService _navigationService;
        
        public NewContactPageViewModel(IStore<Contact> store, INavigationService navigationService)
        {
            _contactStore = store;
            _navigationService = navigationService;
            AddContactCommand = new AsyncCommand(AddContact);
            Contact = new Contact();
        }

        public AsyncCommand AddContactCommand { get; private set; }

        public Contact Contact { get; set; }

        private async Task AddContact()
        {
            await _contactStore.SeveAsync(Contact);
            await _navigationService.GoBackAsync();
        }
    }
}
