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

        public DateTime InstallDate { get; set; }

        [WMIProps(name: "TimeOfLastReset")]
        public TimeSpan TimeOfLastResetTimeSpan { get; set; }
        [WMIProps(name: "TimeOfLastReset")]
        public DateTime TimeOfLastResetDateTime { get; set; }
        [WMIProps(name: "TimeOfLastReset")]
        public DateTimeOffset TimeOfLastResetDateTimeOffset { get; set; }

        public UInt16 AdapterTypeID { get; set; }
        public bool AutoSense { get; set; }

        public override string ToString()
        {
            return
                "MACAddress " + MACAddress
                + " Caption" + Caption
                + "GUID " + GUID
                 + " InstallDate " + InstallDate
            + " TimeOfLastResetTimeSpan " + TimeOfLastResetTimeSpan
             + " TimeOfLastResetDateTime " + TimeOfLastResetDateTime
             + " TimeOfLastResetDateTimeOffset " + TimeOfLastResetDateTimeOffset
             + " AdapterTypeID " + AdapterTypeID
             + " AutoSense " + AutoSense;

        }

    }
}
