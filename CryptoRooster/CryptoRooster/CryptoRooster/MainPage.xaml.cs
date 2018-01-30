using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace CryptoRooster
{
    public partial class MainPage : ContentPage
    {
        string url = "https://api.coinmarketcap.com/v1/ticker/";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        List<Coin> coins =  new List<Coin>();

        public MainPage()
        {
            InitializeComponent();
            GetCoins();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void GetCoins()
        {
            var jsonContent = await client.GetStringAsync(url);
            coins = JsonConvert.DeserializeObject<List<Coin>>(jsonContent);
            coinslist.ItemsSource = coins;
        }

        async private void coinslist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            coinslist.SelectedItem = null;
            var coin = e.Item as Coin;
            await Navigation.PushModalAsync(new CoinDetailPage(coin));
        }

        private void coinslist_Refreshing(object sender, EventArgs e)
        {
            GetCoins();
            coinslist.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                coinslist.ItemsSource = coins.Where(c => c.Name.ToLower().Contains(e.NewTextValue.ToLowerInvariant()));
            }
            else
            {
                coinslist.ItemsSource = coins;
            }
        }

        private void FavouriteButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                var coin = button.CommandParameter as Coin;
                if (coin.IsFavourite)
                {
                    button.Image = ImageSource.FromFile("heart_empty.png") as FileImageSource;
                    coin.IsFavourite = false;
                }
                else
                {
                    button.Image = ImageSource.FromFile("heart.png") as FileImageSource;
                    coin.IsFavourite = true;
                }
            }
            catch
            {
                
            }
        }

        async private void test_Clicked(object sender, EventArgs e)
        {
            List<Coin> coins = coinslist.ItemsSource as List<Coin>;
            //List<Coin> favcoins = coins.Where(c => c.IsFavourite).ToList();
            //await Navigation.PushModalAsync(new FavouriteCoinsPage(favcoins));
            coinslist.ItemsSource = coins.Where(c => c.IsFavourite).ToList();
        }

        private void test2_Clicked(object sender, EventArgs e)
        {
            coinslist.ItemsSource = coins;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            label.IsEnabled = false;
        }
        
    }
}
