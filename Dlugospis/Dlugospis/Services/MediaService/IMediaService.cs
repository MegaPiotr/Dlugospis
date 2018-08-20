using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dlugospis.Services.MediaService
{
    public interface IMediaService: IInitializable
    {
        Task<ImageSource> TakePhotoAsync();
        Task<ImageSource> PickImageAsync();
    }
}
