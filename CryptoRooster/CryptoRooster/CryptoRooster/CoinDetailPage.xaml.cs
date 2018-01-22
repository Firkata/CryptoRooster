using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoRooster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinDetailPage : ContentPage
    {
        public CoinDetailPage()
        {
            InitializeComponent();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}