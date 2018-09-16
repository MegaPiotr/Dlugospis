using Dlugospis.Services;
using Dlugospis.Services.MediaService;
using Dlugospis.Services.Stores;
using Dlugospis.Views;
using Models.DataBase;
using Models.Enums;
using Nito.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation;
using Validation.Rules;
using Xamarin.Forms;

namespace Dlugospis.ViewModels
{
    public class NewDebtPageViewModel : BindableBase, INavigationAware
    {
        private readonly IMediaService _mediaService;

        private readonly IStore<Debt> _debtStore;

        private readonly INavigationService _navigationService;

        public NewDebtPageViewModel(IMediaService mediaService, IStore<Debt> debtStore, INavigationService navigationService)
        {
            Title = "Nowy dług";
            _mediaService = mediaService;
            _debtStore = debtStore;
            _navigationService = navigationService;
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            GetPhotoCommand = new AsyncCommand(GetPhotoAsync);
            AcceptCommand = new AsyncCommand(AddDebtAsync);
            TakeFromContactBookCommand = new AsyncCommand(TakeFromContactBook);
        }

        public AsyncCommand TakeFromContactBookCommand { get; private set; }

        public AsyncCommand TakePhotoCommand { get; private set; }

        public AsyncCommand GetPhotoCommand { get; private set; }

        public AsyncCommand AcceptCommand { get; private set; }

        public string Title { get; set; }

        private Contact _person;
        public Contact Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private OwnerRole _ownerRole;
        public OwnerRole OwnerRole
        {
            get { return _ownerRole; }
            set { SetProperty(ref _ownerRole, value); }
        }

        private SubjectType _subjectType;
        public SubjectType SubjectType
        {
            get { return _subjectType; }
            set { SetProperty(ref _subjectType, value); }
        }

        private string _money;
        public string Money
        {
            get { return _money; }
            set { SetProperty(ref _money, value); }
        }

        private bool _isMoneyValid;
        public bool IsMoneyValid
        {
            get { return _isMoneyValid; }
            set { SetProperty(ref _isMoneyValid, value); }
        }

        public List<IValidationRule<string>> Rules => new List<IValidationRule<string>>
        {
            new IsNotNullOrEmptyRule<string>{ValidationMessage = "Wartość jest wymagana."},
            new IsPriceRule<string>{ValidationMessage = "Błędny format danych."},
            new IsInRangeRule<string>(0.01, 9999.99){ValidationMessage = "Wartość poza dozwolonym zakresem."}
        };

        private async Task TakeFromContactBook()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("CanChoose", true);
            navParameters.Add("CanEdit", false);
            await _navigationService.NavigateAsync(nameof(ContactBookPage), navParameters);
        }

        private async Task AddDebtAsync()
        {
            Debt debt = new Debt() { OwnerRole = OwnerRole, Person = Person };
            if (SubjectType == SubjectType.Money)
                debt.Money = Double.Parse(Money);
            else
            {
                debt.Description = Description;
                // todo add image
            }
            await _debtStore.SeveAsync(debt);
        }

        private async Task TakePhotoAsync()
        {
            ImageSource image = await _mediaService.TakePhotoAsync();
            Device.BeginInvokeOnMainThread(() =>
            {
                Image = image;
            });
        }

        private async Task GetPhotoAsync()
        {
            var image = await _mediaService.PickImageAsync();
            Device.BeginInvokeOnMainThread(() =>
            {
                Image = image;
            });
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
            if (parameters.ContainsKey("person"))
                Person = (Contact)parameters["person"];
        }
        #endregion
    }
}
