using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    class WMIMethods
    {

        internal static T ExecuteMethod<T>(object obj, T parameters)
        {
            /*
             * WMIClassName
             * methodName
             * funcParams
             */
            //  var inParams = new object[1];
            var inParams = new List<object>();
            var data = parameters.ToString()
                .Replace("{", String.Empty)
                 .Replace("}", String.Empty)
                 .Split('=')
                 [1];


            var searchprops = HelperFuncs.getSearchPropsNames(obj.GetType());



            inParams.Add(data);
            var methodName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;
            string searchParams = "*";
            string rootNamespace = @"root\CimV2";
            string className = @"Win32_Printer";

            string searchQuery = String.Format("SELECT {0} FROM {1} Where  Name = ' Brother RJ-403011' OR DeviceID = 'Brother RJ-403011'", searchParams, className);

            using (var searcher = new ManagementObjectSearcher(rootNamespace, searchQuery))
            {
                using (var searcherData = searcher.Get())
                {
                    foreach (ManagementObject manageObject in searcherData)
                    {
                        manageObject.InvokeMethod(methodName, inParams.ToArray());

                    }
                }
            }



            return default(T);
        }
    }
}
