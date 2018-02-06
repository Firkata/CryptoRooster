﻿using Newtonsoft.Json;
using Xamarin.Forms;

namespace CryptoRooster
{
    public class Coin
    {
        private string imageUrl = string.Empty;
        private Image imageFavourite = new Image { Source = "heart.png" };
        private string favouriteImage = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("symbol")]
        //public string Symbol { get; set; }

        //[JsonProperty("rank")]
        //public string Rank { get; set; }

        [JsonProperty("price_usd")]
        public string PriceUsd { get; set; }

        //[JsonProperty("price_btc")]
        //public string PriceBtc { get; set; }

        //[JsonProperty("24h_volume_usd")]
        //public string VolumeUsd24H { get; set; }

        //[JsonProperty("market_cap_usd")]
        //public string MarketCap { get; set; }

        //[JsonProperty("available_supply")]
        //public string AvailableSupply { get; set; }

        //[JsonProperty("lototal_supply")]
        //public string TotalSupply { get; set; }

        //[JsonProperty("max_supply")]
        //public object MaxSupply { get; set; }

        //[JsonProperty("percent_change_1h")]
        //public string PercentChangeHour { get; set; }

        //[JsonProperty("percent_change_24h")]
        //public string PercentChangeDay { get; set; }

        //[JsonProperty("percent_change_7d")]
        //public string PercentChangeWeek { get; set; }

        //[JsonProperty("last_updated")]
        //public string LastUpdated { get; set; }

        public string ImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = "https://files.coinmarketcap.com/static/img/coins/64x64/" + Id + ".png";
                }
                return imageUrl;
            }
            set
            {
                imageUrl = value;
            }
        }
        
        public bool IsFavourite { get; set; }

        public string FavouriteImage
        {
            get
            {
                if (IsFavourite)
                {
                    return "heart.png";
                }
                else
                {
                    return "heart_empty.png";
                }
            }
            set
            {
                favouriteImage = value;
            }
        }
        public override bool Equals(object obj)
        {
            Coin coin = obj as Coin;
            if (coin == null)
                return false;
            else
                return Name.Equals(coin.Name);

        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
