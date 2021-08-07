using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBCP.ViewModels.Base
{
    public class InitializedEventArgs : EventArgs
    {
        public object Data { get; set; }
        public InitializedEventArgs()
        {
        }
        public InitializedEventArgs(object data)
        {
            Data = data;
        }
    }
}
