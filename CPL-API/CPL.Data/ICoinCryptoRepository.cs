using System.Collections.Generic;
using CPL.Models;

namespace CPL.Data
{
    public interface ICoinCryptoRepository
    {
        List<CryptoCoin> GetAllCryptoCoins();
    }
}