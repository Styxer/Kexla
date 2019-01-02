using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Kexla
{
    public class WMISearcher
    {
        #region ctors


        private static string RootNamespace = String.Empty;

        public WMISearcher(string _RootNamespace)
        {
            RootNamespace = _RootNamespace;
        }
        public WMISearcher()
        {

        }
        #endregion


        /// <summary>
        /// Runs a query against WMI. It will return all instances of the class corresponding to the WMI class set on the Type on IEnumerable.
        /// </summary>
        /// <typeparam name="T">The Type of IEnumerable that will be returned</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>()
        {
            var rootNamespace = String.IsNullOrEmpty(RootNamespace) ? @"root\CimV2" : RootNamespace;
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
