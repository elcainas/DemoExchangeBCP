using DemoBCP.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoBCP.Services.Exchange
{
    public class ExchangeServices : IExchangeServices
    {
        private static List<Currency> _currencies;
        public ExchangeServices()
        {
            var stringValues = GetStringFromFile();
            _currencies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Currency>>(stringValues);
        }


        public List<Currency> GetAllCurrencies()
        {
            return new List<Currency>(_currencies);
        }

        public Currency GetCurrencyByAbb(string abb)
        {
            var currency = _currencies.FirstOrDefault(x => x.NameAbb == abb);
            return currency;
        }
        private static string GetStringFromFile()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ExchangeServices)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("DemoBCP.Resources.Currencies.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
    }
}
