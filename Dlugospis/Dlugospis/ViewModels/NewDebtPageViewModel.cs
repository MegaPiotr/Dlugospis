using Dlugospis.Services;
using Dlugospis.Services.MediaService;
using Models.Enums;
using Nito.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation;
using Validation.Rules;
using Xamarin.Forms;

namespace Dlugospis.ViewModels
{
    public class NewDebtPageViewModel : BindableBase
    {
        private string _money;
        private bool _isMoneyValid;
        public AsyncCommand TakePhotoCommand { get; private set; }
        public AsyncCommand GetPhotoCommand { get; private set; }
        private readonly IMediaService _mediaService;

        public NewDebtPageViewModel(IMediaService mediaService)
        {
            _mediaService = mediaService;
            TakePhotoCommand = new AsyncCommand(TakePhoto);
            GetPhotoCommand = new AsyncCommand(GetPhoto);
        }

        private async Task TakePhoto()
        {
            ImageSource image= await _mediaService.TakePhotoAsync();
            Device.BeginInvokeOnMainThread(() =>
                {
                    Image = image;
                });
        }
        private async Task GetPhoto()
        {
            var image = await _mediaService.PickImageAsync();
            Device.BeginInvokeOnMainThread(() =>
            {
                Image = image;
            });
        }
        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
        private UserType _userType;
        public UserType UserType
        {
            get { return _userType; }
            set { SetProperty(ref _userType, value); }
        }
        private SubjectType _subjectType;
        public SubjectType SubjectType
        {
            get { return _subjectType; }
            set { SetProperty(ref _subjectType, value); }
        }
        public string Money
        {
            get { return _money; }
            set { SetProperty(ref _money, value); }
        }
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
    }
}
