using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Color_Separation
{
    public class BindingEx : Binding
    {
        public BindingEx()
        {
            //UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        }
        public BindingEx(string path) : base(path)
        {
            //UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        }
    }
}