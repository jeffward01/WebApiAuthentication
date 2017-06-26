using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CPL.Models.Dto
{
    [DataContract]
    public class PreLaunchToken
    {
        [DataMember(Name = "coinName")]
        public string CoinName { get; set; }

        [DataMember(Name = "coinTicker")]
        public string CoinTicker { get; set; }

        [DataMember(Name = "coinMarketCapUrl")]
        public string CoinMarketCapUrl { get; set; }

        [DataMember(Name = "coinPrice_Usd")]
        public string CoinPrice_Usd { get; set; }

        [DataMember(Name = "coinPrice_Btc")]
        public string CoinPrice_Btc { get; set; }

        [DataMember(Name = "Markets")]
        public List<Market> Markets { get; set; }
    }
}