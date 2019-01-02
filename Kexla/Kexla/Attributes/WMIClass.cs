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
        public string Searchkey { get; set; }

        public WMIClass()
        {

        }
        public WMIClass(string Name)
        {
            this.Name = Name;
        }

        public WMIClass(string Name, string Namespace)
        {
            this.Name = Name;
            this.Namespace = Namespace;
        }

        public WMIClass(string Name, string Namespace, string SearchKey)
        {
            this.Name = Name;
            this.Namespace = Namespace;
            this.Searchkey = Searchkey;
        }


    }
}
