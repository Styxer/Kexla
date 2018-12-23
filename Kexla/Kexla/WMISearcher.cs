using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class WMISearcher
    {

        private WMIClassTypes classType;

        public WMISearcher(WMIClassTypes classType)
        {
            this.classType = classType;
        }

        public IEnumerable<T> Query<T>()
        {
            string rootNamespace = classType.ToString();
            var results = new List<T>();

            string classNmae = HelperFuncs.getClassName(typeof(T));

            string searchprops = HelperFuncs.getSearchProps(typeof(T));

            string searchQuery = String.Format("SELECT {0} FROM {1} ", searchprops, classNmae);


            using (var searcher = new ManagementObjectSearcher(rootNamespace, searchQuery))
            {
                using (var searcherData = searcher.Get())
                {
                    foreach (ManagementObject obj in searcherData)
                    {

                        var searchItem = (T)HelperFuncs.getSearchObjec(obj, typeof(T));

                        results.Add(searchItem);


                    }
                }
            }


            return results;
        }
    }
}
