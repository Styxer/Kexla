using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaExamples
{
    [WMIClass(Name: "WmiMonitorBrightness", Namespace: @"Root\wmi")]
    public class WmiMonitorBrightness
    {
        public bool Active { get; set; }
        public string InstanceName { get; set; }
        public UInt32 Levels { get; set; }

        public override string ToString()
        {
            return HelperFuncs.toString(this);
        }

    }
}
