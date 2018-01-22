using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoRooster
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            coinslist.ItemsSource = GetCoins();
        }
        private IEnumerable<Coin> GetCoins(string searchText = null)
        {
            //TODO: Call to a remote service CMC
            var coins =  new List<Coin>
            {
                new Coin {Name = "Bitcoin", PriceUsd="11000", ImageUrl="http://lorempixel.com/100/100/people/1"},
                new Coin {Name = "Ethereum", PriceUsd="1000", ImageUrl="http://lorempixel.com/100/100/people/2"}
            };

            if (string.IsNullOrWhiteSpace(searchText))
                return coins;
            return coins.Where(c => c.Name.ToLower().Contains(searchText.ToLowerInvariant()));
        }

        private void coinslist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            coinslist.SelectedItem = null;
        }

        async private void coinslist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //TODO: Navigation to next page
            var coin = e.Item as Coin;
            await Navigation.PushAsync(new CoinDetailPage(coin));
        }

        private void coinslist_Refreshing(object sender, EventArgs e)
        {
            coinslist.ItemsSource = GetCoins();
            coinslist.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            coinslist.ItemsSource = GetCoins(e.NewTextValue);
        }
    }
}
