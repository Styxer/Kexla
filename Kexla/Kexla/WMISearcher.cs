using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    class WMISearcher
    {

        private WMIClassTypes classType;

        public WMISearcher(WMIClassTypes classType)
        {
            this.classType = classType;
        }

        public IEnumerable<T> Query<T>()
        {
            var rootNamespace = classType.ToString();
            var results = new List<T>();

            var classNmae = HelperFuncs.getClassName(typeof(T));

            var searchprops = String.Join(" , ", HelperFuncs.getSearchPropsNames(typeof(T)));


            var searchQuery = String.Format("SELECT {0} FROM {1} ", searchprops, classNmae);


            using (var searcher = new ManagementObjectSearcher(rootNamespace, searchQuery))
            {
                using (var searcherData = searcher.Get())
                {
                    foreach (ManagementObject obj in searcherData)
                    {

                        var searchItem = (T)HelperFuncs.getSearchObjects(obj, typeof(T));

                        results.Add(searchItem);


                    }
                }
            }


            return results;
        }


    }
}
