using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    class WMIProps : Attribute
    {

        public string Name { get; set; }

        public WMIProps()
        {

        }


        public WMIProps(string name)
        {
            this.Name = name;
        }
    }
}
