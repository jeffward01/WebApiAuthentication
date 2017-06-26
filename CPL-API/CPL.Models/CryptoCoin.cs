using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CPL.Models
{
    [DataContract]
    public class CryptoCoin
    {
        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "symbol")]
        public string Symbol { get; set; }

        [DataMember(Name = "market_cap_usd")]
        public string MarketCap { get; set; }

        [DataMember(Name = "price_btc")]
        public string PriceInBtc { get; set; }

        [DataMember(Name = "price_usd")]
        public string PriceInUsd { get; set; }

        [DataMember(Name = "total_supply")]
        public string TotalSupply { get; set; }

        [DataMember(Name = "available_supply")]
        public string AvailableSupply { get; set; }

        [DataMember(Name = "24h_volume_usd")]
        public string Volume24hours { get; set; }

        [DataMember(Name = "percent_change_24h")]
        public string Change24hours { get; set; }

        [DataMember(Name = "percent_change_1h")]
        public string Change1h { get; set; }

        [DataMember(Name = "percent_change_7d")]
        public string Change7d { get; set; }

        [DataMember(Name = "last_updated")]
        public string LastUpdated { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

    }
}
