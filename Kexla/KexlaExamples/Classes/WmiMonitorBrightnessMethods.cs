using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaExamples
{
    [WMIClass(Name: "WmiMonitorBrightnessMethods", Namespace: @"Root\wmi")]
    public class WmiMonitorBrightnessMethods
    {
        public bool Active { get; set; }
        public string InstanceName { get; set; }


        public void WmiSetBrightness(UInt32 Timeout, byte Brightness)
        {
            WMIMethods.ExecuteMethod(this, Timeout, Brightness);

        }

        public override string ToString()
        {
            return WMIHelper.toString(this);
        }



    }
}
