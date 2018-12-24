using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaTest
{

    [WMIClass(name: "Win32_NetworkAdapter")]
    public class TestClassNetworkAdapter
    {
        [WMIProps(name: "MACAddress")]
        public string MACAddress { get; set; }


        public string Caption { get; set; }

        [WMIIgnore]
        public string GUID { get; set; }

        public override string ToString()
        {
            return "MACAddress " + MACAddress
                + " Caption" + Caption
                + "GUID " + GUID;

        }

    }
}
