using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class WMIClass : Attribute
    {

        public string Name { get; set; }
        public string Namespace { get; set; }

        public WMIClass()
        {

        }
        public WMIClass(string name)
        {
            Name = name;
        }
        public WMIClass(string name, string wmiNamespace)
        {
            Name = name;
            Namespace = wmiNamespace;
        }

    }
}
