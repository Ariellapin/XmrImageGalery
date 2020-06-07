using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageGalery.Services;
using ImageGalery.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace ImageGalery
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=20a7dc53-c143-46d9-b72e-32c86a9891e6;" +
                  "uwp=20a7dc53-c143-46d9-b72e-32c86a9891e6;" +
                  "ios=20a7dc53-c143-46d9-b72e-32c86a9891e6",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
