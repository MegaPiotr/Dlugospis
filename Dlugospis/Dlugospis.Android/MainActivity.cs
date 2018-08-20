using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Dlugospis.Services;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using SegmentedControl.FormsPlugin.Android;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dlugospis.Droid
{
    [Activity(Label = "Dlugospis", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SegmentedControlRenderer.Init();
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {

        }
    }
}

