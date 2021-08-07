using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using DemoBCP.Models;
using DemoBCP.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoBCP.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        private event EventHandler<InitializedEventArgs> _initialized;
        public event EventHandler<InitializedEventArgs> Initialized
        {
            add { _initialized += value; }
            remove { _initialized -= value; }
        }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public object Data { get; set; }
        public BaseViewModel()
        {
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        public virtual async Task GoTo(string route)
        {
            await Task.FromResult(false);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public virtual void OnInitializedAsync(InitializedEventArgs e)
        {
            _initialized?.Invoke(this, e);
            //await Task.FromResult(false);
        }
    }
}
