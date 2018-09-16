using Dlugospis.Services.Stores;
using Dlugospis.Views;
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
    public class ContactBookPageViewModel : BindableBase, IInitializable, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public ContactBookPageViewModel(IStore<Contact> store, INavigationService navigationService)
        {
            Title = "Kontakty";
            ContactStore = store;
            _navigationService = navigationService;
            InitializeTask = NotifyTask.Create(InitializeAsync);
            AddContactCommand = new AsyncCommand(AddContactAsync);
            SelectCommand = new DelegateCommand<Contact>(SelectItem);
        }

        public IStore<Contact> ContactStore { get; }

        public AsyncCommand AddContactCommand { get; private set; }

        public DelegateCommand<Contact> SelectCommand { get; private set; }

        public NotifyTask InitializeTask { get; private set; }

        public string Title { get; set; }

        private bool CanChoose { get; set; } = false;

        private bool CanEdit { get; set; } = true;

        private void SelectItem(Contact person)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("person", person);
            _navigationService.GoBackAsync(navParameters);
        }

        private async Task AddContactAsync(object arg)
        {
            await _navigationService.NavigateAsync(nameof(NewContactPage));
        }

        #region INavigationAware methods
        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("CanChoose"))
                CanChoose = (bool)parameters["CanChoose"];
            //todo fix binding context
            if (parameters.ContainsKey("CanEdit"))
                CanEdit = (bool)parameters["CanEdit"];
        }
        #endregion

        private async Task InitializeAsync()
        {
            if (!ContactStore.InitializeTask.IsSuccessfullyCompleted)
                await ContactStore.InitializeTask.Task;
        }
    }
}
