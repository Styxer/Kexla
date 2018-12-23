using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class WMIClassTypes
    {
        private readonly String name;
        private readonly int value;


        public static readonly WMIClassTypes CimV2 = new WMIClassTypes(1, @"root\CimV2");



        private WMIClassTypes(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }

    }
}
