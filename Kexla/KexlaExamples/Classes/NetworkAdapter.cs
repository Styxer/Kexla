using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaExamples
{
    [WMIClass(Name: "Win32_NetworkAdapter")]
    public class NetworkAdapter
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
            return HelperFuncs.toString(this);
        }





    }
}
