using DemoBCP.ViewModels;
using DemoBCP.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DemoBCP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CurrenciesPage), typeof(CurrenciesPage));
        }

      
    }
}
