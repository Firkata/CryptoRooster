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
    public partial class FavouriteCoinsPage : ContentPage
    {
        public FavouriteCoinsPage(List<Coin> coins)
        {
            InitializeComponent();
            favcoinslist.ItemsSource = coins;
        }

        private void FavouriteButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}