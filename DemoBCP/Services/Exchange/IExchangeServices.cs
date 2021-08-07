using DemoBCP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBCP.Services.Exchange
{
    public interface IExchangeServices
    {
        Currency GetCurrencyByAbb(string abb);
        List<Currency> GetAllCurrencies();
    }
}
