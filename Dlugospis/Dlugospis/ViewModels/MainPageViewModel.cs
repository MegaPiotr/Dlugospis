using Dlugospis.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlugospis.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public DelegateCommand ShowDebtsCommand { get; private set; }
        public DelegateCommand AddDebtCommand { get; private set; }
        public DelegateCommand ShowFriendsCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
            ShowDebtsCommand = new DelegateCommand(ShowDebts);
            AddDebtCommand = new DelegateCommand(AddDebt);
            ShowFriendsCommand = new DelegateCommand(ShowFriends);
        }
        #region commnds
        private void ShowFriends()
        {
            _navigationService.NavigateAsync(nameof(ContactBookPage));
        }

        private void AddDebt()
        {
            _navigationService.NavigateAsync(nameof(NewDebtPage));
        }

        private void ShowDebts()
        {
            
        }
        #endregion
    }
}
