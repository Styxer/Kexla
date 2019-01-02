using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KexlaExamples
{
    [WMIClass(Name: "Win32_NetworkLoginProfile ", Namespace: @"root\CimV2")]
    public class NetworkLoginProfile
    {
        public DateTime LastLogoff { get; set; }
        public DateTime LastLogon { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return HelperFuncs.toString(this);
        }

    }
}
