using Dlugospis.Services.ContactStore;
using Dlugospis.Views;
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
	public class ContactBookPageViewModel : BindableBase,IInitializable
	{
        private readonly INavigationService _navigationService;
        public DelegateCommand AddContactCommand { get; private set; }
        public IContactStore ContactStore { get; }

        public NotifyTask NotifyTask { get; private set; }
        public string Title { get; set; }
        public ContactBookPageViewModel(IContactStore store, INavigationService navigationService)
        {
            Title = "kupa";
            ContactStore = store;
            _navigationService = navigationService;
            NotifyTask = NotifyTask.Create(InitializeAsync);
            AddContactCommand = new DelegateCommand(AddContact);
        }

        private async Task InitializeAsync()
        {
            if (!ContactStore.NotifyTask.IsSuccessfullyCompleted)
                await ContactStore.NotifyTask.Task;
        }

        private void AddContact()
        {
            _navigationService.NavigateAsync(nameof(NewContactPage));
        }
    }
}
