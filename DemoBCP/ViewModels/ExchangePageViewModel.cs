using DemoBCP.Models;
using DemoBCP.Services.Exchange;
using DemoBCP.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoBCP.ViewModels
{
    public class ExchangePageViewModel : BaseViewModel
    {
        private readonly IExchangeServices _exchangeServices;

        private CurrencyWrapper _currencyWrapper;
        public CurrencyWrapper CurrencyWrapper { get => _currencyWrapper; set => SetProperty(ref _currencyWrapper, value); }

        private double _inputValue;
        public double InputValue { get => _inputValue; set => SetProperty(ref _inputValue, value); }

        private double _outputValue;
        public double OutputValue { get => _outputValue; set => SetProperty(ref _outputValue, value); }

        public ICommand _swapCurrencyCommand;
        public ICommand SwapCurrencyCommand => _swapCurrencyCommand ??= new Command(SwapCurrency);

        public ICommand _changeCurrencyFromCommand;
        public ICommand ChangeCurrencyFromCommand => _changeCurrencyFromCommand ??= new Command(async () => await ChangeCurrency(true));

        public ICommand _changeCurrencyToCommand;
        public ICommand ChangeCurrencyToCommand => _changeCurrencyToCommand ??= new Command(async () => await ChangeCurrency(false));

        public ICommand _inputValueChangeCommand;
        public ICommand InputValueChangeCommand => _inputValueChangeCommand ??= new Command<string>((x) => InputValueChange(x));


        public ExchangePageViewModel(IExchangeServices exchangeServices)
        {
            _exchangeServices = exchangeServices;
            SubscribeToCurrencies();
        }
        private async Task ChangeCurrency(bool changeInput)
        {
            var args = new ChangeCurrencyArgs()
            {
                CurrencyWrapper = CurrencyWrapper,
                ChangeInput = changeInput,

            };

            var stringjson = JsonConvert.SerializeObject(args);
            stringjson = System.Net.WebUtility.UrlEncode(stringjson);
            await Shell.Current.GoToAsync($"CurrenciesPage?Data={stringjson}");
        }


        private void SwapCurrency()
        {
            CurrencyWrapper = new CurrencyWrapper(CurrencyWrapper.CurrencyOutput, CurrencyWrapper.CurrencyInput);
            UpdateOutputValue();
        }
        private void InputValueChange(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                OutputValue = 0;
                InputValue = 0;
                return;
            }
            UpdateOutputValue();
        }

        private void UpdateOutputValue()
        {
            OutputValue = InputValue / CurrencyWrapper.GetBuyingRate;
        }

        private void SubscribeToCurrencies()
        {
            MessagingCenter.Instance.Subscribe<CurrenciesPageViewModel, CurrencyWrapper>(this, "GetCurrency", (sender, arg) =>
            {
                CurrencyWrapper = arg;
                UpdateOutputValue();
            });
        }
        public override void OnInitializedAsync(InitializedEventArgs e)
        {
            var CurrencyFrom = _exchangeServices.GetCurrencyByAbb("PEN");
            var CurrencyTo = _exchangeServices.GetCurrencyByAbb("USD");
            CurrencyWrapper = new CurrencyWrapper(CurrencyFrom, CurrencyTo);
        }
    }
}
