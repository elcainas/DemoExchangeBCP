using DemoBCP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBCP.Models
{
    public class CurrencyWrapper : BaseViewModel
    {
        private Currency _currencyInput;
        private Currency _currencyOutput;

        public Currency CurrencyInput
        {
            get => _currencyInput;
            set
            {
                SetProperty(ref _currencyInput, value);

                if (value == null || CurrencyOutput == null)
                    return;

                OnPropertyChanged(nameof(GetBuyingRate));
                OnPropertyChanged(nameof(GetSalesRate));
            }
        }
        public Currency CurrencyOutput
        {
            get => _currencyOutput;
            set
            {
                SetProperty(ref _currencyOutput, value);
                if (value == null || CurrencyInput == null)
                    return;
                OnPropertyChanged(nameof(GetBuyingRate));
                OnPropertyChanged(nameof(GetSalesRate));
            }
        }
        public CurrencyWrapper(Currency currencyInput, Currency currencyOutPut)
        {
            CurrencyInput = currencyInput;
            CurrencyOutput = currencyOutPut;
        }

        //public double GetBuyingRate { get => Math.Round(CurrencyInput.BuyingRateDollar / CurrencyOutput.SellingRateDollar, 5); }
        //public double GetSalesRate { get => Math.Round(CurrencyInput.SellingRateDollar / CurrencyOutput.BuyingRateDollar, 5); }
        public double GetBuyingRate { get => Math.Round(CurrencyOutput.SellingRateDollar / CurrencyInput.BuyingRateDollar, 5); }
        public double GetSalesRate { get => Math.Round(CurrencyOutput.BuyingRateDollar / CurrencyInput.SellingRateDollar, 5); }
        public double GetBuyingRateInverse { get => Math.Round(CurrencyInput.BuyingRateDollar / CurrencyOutput.SellingRateDollar, 5); }

    }
}
