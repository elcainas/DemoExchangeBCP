using DemoBCP.Models;
using DemoBCP.Services.Exchange;
using DemoBCP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoBCP.ViewModels
{
    public class CurrenciesPageViewModel : BaseViewModel
    {
        private readonly IExchangeServices _exchangeServices;
        private ChangeCurrencyArgs _changeCurrencyArgs;
        private List<Currency> _currencies;

        public List<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }
        private List<CurrencyWrapper> _currencyWrappers;
        public List<CurrencyWrapper> CurrencyWrappers
        {
            get { return _currencyWrappers; }
            set { SetProperty(ref _currencyWrappers, value); }
        }

        public ICommand _selectedCurrencyCommand;
        public ICommand SelectedCurrencyCommand => _selectedCurrencyCommand ??= new Command<CurrencyWrapper>(async (x) => await SelectedCurrency(x));
        public CurrenciesPageViewModel(IExchangeServices exchangeServices)
        {
            _exchangeServices = exchangeServices;
        }
        public override void OnInitializedAsync(InitializedEventArgs e)
        {
            Currencies = _exchangeServices.GetAllCurrencies();
            if (e.Data == null)
                return;

            var data = System.Net.WebUtility.UrlDecode(e.Data.ToString());
            _changeCurrencyArgs = Newtonsoft.Json.JsonConvert.DeserializeObject<ChangeCurrencyArgs>(data);
            FilterCurrencies(_changeCurrencyArgs);
            CreateWrappers(_changeCurrencyArgs);
        }

        private void CreateWrappers(ChangeCurrencyArgs args)
        {
            if (args.ChangeInput)
                CurrencyWrappers = Currencies.Select(x => new CurrencyWrapper(x, args.CurrencyWrapper.CurrencyOutput)).ToList();
            else
                CurrencyWrappers = Currencies.Select(x => new CurrencyWrapper(x, args.CurrencyWrapper.CurrencyInput)).ToList();
        }

        private void FilterCurrencies(ChangeCurrencyArgs args)
        {
            var currencyInput = Currencies.FirstOrDefault(x => x.NameAbb == args.CurrencyWrapper.CurrencyInput.NameAbb);
            var currencyOutput = Currencies.FirstOrDefault(x => x.NameAbb == args.CurrencyWrapper.CurrencyOutput.NameAbb);
            Currencies.Remove(currencyInput);
            Currencies.Remove(currencyOutput);
        }

        private async Task SelectedCurrency(CurrencyWrapper item)
        {
            var wrapper = _changeCurrencyArgs.CurrencyWrapper;
            if (_changeCurrencyArgs.ChangeInput)
                wrapper.CurrencyInput = item.CurrencyInput;
            else
                wrapper.CurrencyOutput = item.CurrencyInput;

            MessagingCenter.Instance.Send(this, "GetCurrency", wrapper);
            await Shell.Current.Navigation.PopModalAsync();
        }

    }
}
