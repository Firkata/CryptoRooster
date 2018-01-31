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
        string url = "https://api.coinmarketcap.com/v1/ticker/";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        List<Coin> coins =  new List<Coin>();
        ObservableCollection<Coin> favcoins;
        delegate void kur(object sender, EventArgs e);
        event kur shure;

        public MainPage()
        {
            favcoins = new ObservableCollection<Coin>();
            InitializeComponent();
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
                DisplayAlert("Connection Error", "Internet is turned off", "OK");
                return;
            }
        }

        private async void GetCoins()
        {
            //try
            //{
            //    var jsonContent = await client.GetStringAsync(url);
            //    coins = JsonConvert.DeserializeObject<List<Coin>>(jsonContent);
            //}
            //catch (Exception)
            //{
            //    await DisplayAlert("Connection Error", "Internet is turned off", "OK");
            //    return;
            //}

            var jsonContent = await client.GetStringAsync(url);
            coins = JsonConvert.DeserializeObject<List<Coin>>(jsonContent);

            if (favcoins != null || favcoins.Count != 0)
            {
                foreach(Coin coin in favcoins)
                {
                    if (coins.Contains(coin))
                    {
                        coins[coins.IndexOf(coin)].IsFavourite = true;
                    }
                }
            }

            coinslist.ItemsSource = coins;
            favcoins = new ObservableCollection<Coin>(coins.Where(c => c.IsFavourite).ToList());
        }

        async private void coinslist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            coinslist.SelectedItem = null;
            var coin = e.Item as Coin;
            await Navigation.PushModalAsync(new CoinDetailPage(coin));
        }

        private void coinslist_Refreshing(object sender, EventArgs e)
        {
            //GetCoins();
            ValidateConnection();
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
                    favcoins.Remove(coin);
                    coin.IsFavourite = false;
                }
                else
                {
                    button.Image = ImageSource.FromFile("heart.png") as FileImageSource;
                    favcoins.Add(coin);
                    coin.IsFavourite = true;
                }
            }
            catch
            {
                
            }
        }

        private void test_Clicked(object sender, EventArgs e)
        {
            List<Coin> coins = coinslist.ItemsSource as List<Coin>;
            //List<Coin> favcoins = coins.Where(c => c.IsFavourite).ToList();
            //await Navigation.PushModalAsync(new FavouriteCoinsPage(favcoins));
            //coinslist.ItemsSource = coins.Where(c => c.IsFavourite).ToList();
            coinslist.ItemsSource = favcoins;
        }

        private void test2_Clicked(object sender, EventArgs e)
        {
            coinslist.ItemsSource = coins;
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var label = sender as Label;
        //    label.IsEnabled = false;
        //}
        
    }
}
