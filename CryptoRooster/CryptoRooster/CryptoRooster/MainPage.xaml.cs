using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace CryptoRooster
{
    public partial class MainPage : ContentPage
    {
        string url = "https://api.coinmarketcap.com/v1/ticker/?start=0&limit=50";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        ObservableCollection<Coin> _coins;
        ObservableCollection<Coin> _favcoins;
        

        public MainPage()
        {
            _favcoins = new ObservableCollection<Coin>();
            InitializeComponent();
            allcoins.FontSize = favourites.FontSize + 2;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ValidateConnection();
        }

        private void ValidateConnection()
        {
            
            if (ConnectionHelper.CheckNetworkConnection())
            {
                GetCoins();
            }
            else
            {
                DisplayAlert("Connection Error", "No Internet connection", "Dismiss");
                return;
            }
        }

        private async void GetCoins()
        {
            var jsonContent = await client.GetStringAsync(url);
            var coins = JsonConvert.DeserializeObject<List<Coin>>(jsonContent);

            _coins = new ObservableCollection<Coin>(coins);

            coinslist.ItemsSource = _coins;
        }

        async private void coinslist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            coinslist.SelectedItem = null;
            var coin = e.Item as Coin;
            await Navigation.PushModalAsync(new CoinDetailPage(coin));
        }

        private void coinslist_Refreshing(object sender, EventArgs e)
        {
            ValidateConnection();
            coinslist.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                coinslist.ItemsSource = _coins.Where(c => c.Name.ToLower().Contains(e.NewTextValue.ToLowerInvariant()));
            }
            else
            {
                coinslist.ItemsSource = _coins;
            }
        }

        private void Heart_Clicked(object sender, EventArgs e)
        {
            try
            {
                var coin = (sender as Button).CommandParameter as Coin;
                if (coin.IsFavourite)
                {
                    coin.IsFavourite = false;
                    _coins.Where(c => c.Equals(coin.Name)).FirstOrDefault(c => c.IsFavourite = false);
                    _favcoins.Remove(coin);
                }
                else
                {
                    coin.IsFavourite = true;
                    _coins.Where(c => c.Equals(coin.Name)).FirstOrDefault(c => c.IsFavourite = true);
                    _favcoins.Add(coin);
                }
            }
            catch { }
        }

        private void Favourites_Clicked(object sender, EventArgs e)
        {
            favourites.TextColor = Color.White;
            allcoins.TextColor = Color.Gray;
            favourites.FontSize = favourites.FontSize + 2;
            allcoins.FontSize = allcoins.FontSize - 2;

            coinslist.ItemsSource = _favcoins;
        }

        private void Allcoins_Clicked(object sender, EventArgs e)
        {
            favourites.TextColor = Color.Gray;
            allcoins.TextColor = Color.White;
            favourites.FontSize = favourites.FontSize - 2;
            allcoins.FontSize = allcoins.FontSize + 2;
            
            coinslist.ItemsSource = _coins;
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var label = sender as Label;
        //    label.IsEnabled = false;
        //}

    }
}
