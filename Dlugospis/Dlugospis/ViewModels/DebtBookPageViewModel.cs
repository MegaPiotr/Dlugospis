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
	public class DebtBookPageViewModel : BindableBase, IInitializable
    {
        private readonly INavigationService _navigationService;

        public DebtBookPageViewModel(IStore<Debt> store, INavigationService navigationService)
        {
            Title = "Długi";
            DebtStore = store;
            _navigationService = navigationService;
            InitializeTask = NotifyTask.Create(InitializeAsync);
        }

        public IStore<Debt> DebtStore { get; }

        public NotifyTask InitializeTask { get; private set; }

        public string Title { get; set; }

        private async Task InitializeAsync()
        {
            if (!DebtStore.InitializeTask.IsSuccessfullyCompleted)
                await DebtStore.InitializeTask.Task;
        }
    }
}
