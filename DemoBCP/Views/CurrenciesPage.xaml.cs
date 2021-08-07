using DemoBCP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoBCP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Data", "Data")]
    public partial class CurrenciesPage : ContentPage
    {
        public string Data
        {
            set
            {
                (BindingContext as BaseViewModel).Data = value;
            }
        }
        public CurrenciesPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Yield();
        }
    }
}