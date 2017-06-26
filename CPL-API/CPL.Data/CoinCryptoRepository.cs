using CPL.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using CPL.Data.Utility;

namespace CPL.Data
{
    public class CoinCryptoRepository : ICoinCryptoRepository
    {
        public static string _coinMarketCapGetAll = ConfigurationManager.AppSettings["coinMarketCapApi"];

        public List<CryptoCoin> GetAllCryptoCoins()
        {
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(_coinMarketCapGetAll);

                return JSONUtility.Deserialize<List<CryptoCoin>>(json).ToList();
            }
        }
    }
}