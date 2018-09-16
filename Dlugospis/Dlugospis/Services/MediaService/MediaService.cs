using Nito.Mvvm;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dlugospis.Services.MediaService
{
    public class MediaService : IMediaService
    {
        public MediaService()
        {
            InitializeTask = NotifyTask.Create(InitializeAsync);
        }

        public NotifyTask InitializeTask { get; private set; }

        public bool CanPickImage { get; private set; }

        public bool CanTakePhoto { get; private set; }

        private async Task InitializeAsync()
        {
            await CrossMedia.Current.Initialize();
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (storageStatus == PermissionStatus.Granted)
            {
                CanPickImage = true;
                if (cameraStatus == PermissionStatus.Granted)
                    CanTakePhoto = true;
            }
        }

        private async Task<bool> GetPermisions(params Permission[] permisons)
        {
            var results = await CrossPermissions.Current.RequestPermissionsAsync(permisons);
            foreach (var result in results)
                if (result.Value != PermissionStatus.Granted)
                    return false;
            return true;
        }

        public async Task<ImageSource> PickImageAsync()
        {
            if(!CanPickImage)
            {
                CanPickImage = await GetPermisions(Permission.Storage);
            }

            if (CanPickImage)
            {
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());
                ImageSource image = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                return image;
            }
            else
            {
                //await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }
            return null;
        }

        public async Task<ImageSource> TakePhotoAsync()
        {
            if (!CanTakePhoto)
            {
                CanTakePhoto = await GetPermisions(new[] { Permission.Storage, Permission.Camera });
            }

            if (CanTakePhoto)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
                ImageSource image = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                return image;
            }
            else
            {
                //await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }
            return null;
        }
    }
}
