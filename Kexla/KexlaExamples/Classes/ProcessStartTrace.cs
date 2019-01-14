using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaExamples
{
    [WMIClass(Name: "Win32_ThreadTrace")]
    public class ProcessStartTrace
    {
        [WMIProps(name: "ProcessID")]
        public UInt32 Process_ID { get; set; }
        public UInt32 ThreadID { get; set; }
        public UInt64 TIME_CREATED { get; set; }

        public override string ToString()
        {
            return HelperFuncs.toString(this);
        }
    }
}
