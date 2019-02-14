using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaExamples
{
    [WMIClass(Name: "Win32_PerfFormattedData_PerfDisk_PhysicalDisk")]
    public class PhysicalDisk
    {
        public string Name { get; set; }
        public string DiskBytesPerSec { get; set; }


        public override string ToString()
        {
            return WMIHelper.toString(this);
        }

    }
}
