using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


        private PropertyInfo[] _PropertyInfos = null;

        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = this.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }

    }
}
