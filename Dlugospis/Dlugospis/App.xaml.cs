using Prism;
using Prism.Ioc;
using Dlugospis.ViewModels;
using Dlugospis.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Autofac;
using System;
using Dlugospis.Services;
using Dlugospis.Services.DatabaseConnectionService;
using Dlugospis.Services.MediaService;
using Dlugospis.Services.Stores;
using Models.DataBase;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Dlugospis
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage?createTab=NewDebtPage&createTab=DebtBookPage&createTab=ContactBookPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterServices(containerRegistry);
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<NewDebtPage>();
            containerRegistry.RegisterForNavigation<ContactBookPage>();
            containerRegistry.RegisterForNavigation<NewContactPage>();
            containerRegistry.RegisterForNavigation<ContactBookPage>();
            containerRegistry.RegisterForNavigation<DebtBookPage>();
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMediaService, MediaService>();
            containerRegistry.RegisterSingleton<IDatabaseConnectionService, DatabaseConnectionService>();
            containerRegistry.RegisterSingleton<IStore<Contact>,ContactStore>();
            containerRegistry.RegisterSingleton<IStore<Debt>, DebtStore>();
        }
    }
}
