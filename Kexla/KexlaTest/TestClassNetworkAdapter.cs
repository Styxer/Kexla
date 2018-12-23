using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaTest
{

    [WMIClass("Win32_NetworkAdapter")]
    public class TestClassNetworkAdapter
    {
        [WMIProps("Name")]
        public string MACAddress { get; set; }

        public string Caption { get; set; }

        public override string ToString()
        {
            return "MACAddress " + MACAddress
                + " Caption" + Caption;
        }

    }
}
