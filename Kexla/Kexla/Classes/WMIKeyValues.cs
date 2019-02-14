using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kexla.Classes
{
    public class WMIKeyValues
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public WMIKeyValues(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

    }
}
