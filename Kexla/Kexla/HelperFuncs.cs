using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class HelperFuncs
    {
        public static string getClassName(Type t)
        {
            var att = t.GetCustomAttribute<WMIClass>(inherit: true);
            string className = String.Empty;
            if (att != null)
            {
                className = att.Name;
            }

            return className;

        }

        public static string getSearchProps(Type type)
        {
            var propsList = new List<string>();

            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                propsList.Add(getWMIPropName(propInfo));
            }

            return String.Join(" , ", propsList);

        }

        public static object getSearchObjec(ManagementObject manageObject, Type type)
        {
            var instance = Activator.CreateInstance(type);
            string propName = String.Empty;

            foreach (var propInfo in instance.GetType().GetProperties())
            {
                propName = getWMIPropName(propInfo);

                var propValue = manageObject.Properties[propName].Value;

                if (propValue == null)
                {
                    propInfo.SetValue(obj: instance, value: null);
                }
                else
                {
                    propInfo.SetValue(obj: instance, value: Convert.ChangeType(value: propValue, conversionType: propInfo.PropertyType));
                }
            }

            return instance;
        }

        private static string getWMIPropName(PropertyInfo propInfo)
        {
            string propName = String.Empty;

            WMIProps prop = propInfo.GetCustomAttribute<WMIProps>();
            if (prop == null)
                propName = propInfo.Name;
            else
                propName = propInfo.Name;

            return propName;
        }
    }
}
