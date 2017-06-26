using CPL.Models;
using System.Collections.Generic;
using CPL.Data;

namespace CPL.Core.Managers
{
    public class CoinManager
    {
        private readonly ICoinCryptoRepository _coinCryptoRepository;

        public CoinManager(ICoinCryptoRepository coinCryptoRepository)
        {
            _coinCryptoRepository = coinCryptoRepository;
        }

        public List<CryptoCoin> GetAllCryptoCoins()
        {
            //Do math here to fill the other properties
            return _coinCryptoRepository.GetAllCryptoCoins();
        }
    }
}