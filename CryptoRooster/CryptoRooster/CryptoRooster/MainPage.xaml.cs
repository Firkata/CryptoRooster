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
        string url = "https://api.coinmarketcap.com/v1/ticker/?start=0&limit=500";//"https://api.coinmarketcap.com/v1/ticker/";
        HttpClient client = new HttpClient(new NativeMessageHandler());
        ObservableCollection<Coin> coins =  new ObservableCollection<Coin>();
        ObservableCollection<Coin> favcoins;
        

        public MainPage()
        {
            favcoins = new ObservableCollection<Coin>();
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
            coins = JsonConvert.DeserializeObject<ObservableCollection<Coin>>(jsonContent);

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

        private void Heart_Clicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                var coin = button.CommandParameter as Coin;
                if (coin.IsFavourite)
                {
                    button.Image = ImageSource.FromFile("heart_empty.png") as FileImageSource;
                    coin.IsFavourite = false;
                    //coin.FavouriteImage = "heart_empty.png";
                    favcoins.Remove(coin);
                    //int index = coins.FindIndex(c => c.Name == coin.Name);
                    //coins[index].IsFavourite = false;
                    //coins[index].FavouriteImage = "heart_empty.png";
                }
                else
                {
                    button.Image = ImageSource.FromFile("heart.png") as FileImageSource;
                    coin.IsFavourite = true;
                    //coin.FavouriteImage = "heart.png";
                    favcoins.Add(coin);
                    //int index = coins.FindIndex(c => c.Name == coin.Name);
                    //coins[index].IsFavourite = true;
                    //coins[index].FavouriteImage = "heart.png";
                }
            }
            catch
            {
                
            }
        }

        private void Favourites_Clicked(object sender, EventArgs e)
        {
            favourites.TextColor = Color.White;
            allcoins.TextColor = Color.Gray;
            favourites.FontSize = favourites.FontSize + 2;
            allcoins.FontSize = allcoins.FontSize - 2;
            List<Coin> coins = coinslist.ItemsSource as List<Coin>;
            //List<Coin> favcoins = coins.Where(c => c.IsFavourite).ToList();
            //await Navigation.PushModalAsync(new FavouriteCoinsPage(favcoins));
            //coinslist.ItemsSource = coins.Where(c => c.IsFavourite).ToList();
            coinslist.ItemsSource = favcoins;
        }

        private void Allcoins_Clicked(object sender, EventArgs e)
        {
            favourites.TextColor = Color.Gray;
            allcoins.TextColor = Color.White;
            favourites.FontSize = favourites.FontSize - 2;
            allcoins.FontSize = allcoins.FontSize + 2;
            coinslist.ItemsSource = coins;
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var label = sender as Label;
        //    label.IsEnabled = false;
        //}
        
    }
}
