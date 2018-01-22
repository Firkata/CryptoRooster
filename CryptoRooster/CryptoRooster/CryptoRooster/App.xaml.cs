using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Xamarin.Forms;

namespace CryptoRooster
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CoinDetailPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
