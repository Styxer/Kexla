using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading;
using Kexla.Classes;


namespace Kexla
{
    public class WMISearcher
    {
        #region ctors       

        private static string _rootNamespace = String.Empty;

        public WMISearcher(string RootNamespace)
        {
            _rootNamespace = RootNamespace;
        }
        public WMISearcher()
        {
            _rootNamespace = String.IsNullOrEmpty(_rootNamespace) ? @"root\CimV2" : _rootNamespace; 
        }
        #endregion


        /// <summary>
        /// Runs a query against WMI. It will return all instances of the class corresponding to the WMI class set on the Type on IEnumerable.
        /// </summary>
        /// <typeparam name="T">The Type of IEnumerable that will be returned</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>()
        {

            
            var results = new List<T>();

            var classNmae = HelperFuncs.getClassName(typeof(T));

            var searchprops = String.Join(" , ", HelperFuncs.getSearchPropsNames(typeof(T)));


            var searchQuery = String.Format("SELECT {0} FROM {1} ", searchprops, classNmae);


            using (var searcher = new ManagementObjectSearcher(_rootNamespace, searchQuery))
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


        public ILookup<string, WMIKeyValues> getPrimaryKeyValues<T>()
        {           
           
            var classNmae = HelperFuncs.getClassName(typeof(T));

            var list = new List<WMIKeyValues>();                    


            using (var wmiObject = new ManagementClass(_rootNamespace, classNmae, null))
            {
                foreach (ManagementObject c in wmiObject.GetInstances())
                {
                    foreach (PropertyData p in c.Properties)
                    {
                        foreach (QualifierData q in p.Qualifiers)
                        {
                            if (q.Name.Equals("key"))
                            {
                                list.Add(new WMIKeyValues(p.Name, c.GetPropertyValue(p.Name)));
                            }
                        }
                    }
                } 
            }

            ILookup<string, WMIKeyValues> byWMIKey = list.ToLookup(x => x.Key);

            return byWMIKey;
          
        }

        public async Task<ILookup<string, WMIKeyValues>> getPrimaryKeyValuesAsync<T>()
        {
            return await Task.Run(() => getPrimaryKeyValues<T>());
        }


        /// <summary>
        /// Runs a async query against WMI. It will return all instances of the class corresponding to the WMI class set on the Type on IEnumerable.
        /// </summary>
        /// <typeparam name="T">The Type of IEnumerable that will be returned</typeparam>
        /// <returns></returns>
        public  async Task<IEnumerable<T>> QueryAsAsync<T>()
        {            
            return  await  Task.Run(() =>  Query<T>());
           
            
        }


    }
}
