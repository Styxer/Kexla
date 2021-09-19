﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class WMIMethods
    {

        private static string _methodName;

        /// <summary>
        /// Executes WMI instance method with  parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classObj"></param>
        /// <param name="parameters">function paramaters</param>
        public static void ExecuteMethod(object classObj, params object[] parameters)
        {
            var inParams = new List<object>();
            foreach (var param in parameters)
            {
                inParams.Add(param);
            }


            var propsNames = HelperFuncs.getSearchPropsNames(classObj.GetType());
            var propValues = HelperFuncs.getSearchPropValues(classObj);
            StringBuilder builder = new StringBuilder();

            var propNamesAndValues = propsNames.Zip(propValues, (pn, pv) => new { propName = pn, propValue = pv }).ToList();


            for (int i = 0; i < propNamesAndValues.Count; i++)
            {
                builder.Append(propNamesAndValues[i].propName + " = '" + propNamesAndValues[i].propValue + "'");
                if (i != propNamesAndValues.Count - 1)
                {
                    builder.Append(" AND ");
                }
            }

            string quertyWhere = "WHERE " + builder.ToString();
            quertyWhere = quertyWhere.Replace(@"\", @"\\");


            var methodName = String.IsNullOrEmpty(_methodName) ? new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name : _methodName;
            string searchParams = "*";
            string rootNamespace = HelperFuncs.getNamespace(classObj.GetType());
            string className = HelperFuncs.getClassName(classObj.GetType());

            string searchQuery = String.Format("SELECT {0} FROM {1} {2}", searchParams, className, quertyWhere);

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

        }

       
        /// <summary>
        /// Executes WMI instance method with  parameters asynchronously
        /// </summary>
        /// <param name="classObj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task ExecuteMethodAsync(object classObj , params object[] parameters)
        {
            _methodName =  new System.Diagnostics.StackTrace().GetFrame(5).GetMethod().Name;
            await Task.Run(() => ExecuteMethod(classObj, parameters));
        }

       


    }
}
