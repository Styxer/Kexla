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

            return String.Join(" , ", propsList.Where(x => !String.IsNullOrEmpty(x)));

        }

        public static object getSearchObjec(ManagementObject manageObject, Type type)
        {
            var instance = Activator.CreateInstance(type);
            string propName = String.Empty;

            foreach (var propInfo in instance.GetType().GetProperties())
            {
                propName = getWMIPropName(propInfo);

                if (!String.IsNullOrEmpty(propName))
                {
                    var propertyType = propInfo.PropertyType;
                    var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;

                    var propValue = manageObject.Properties[propName].Value;

                    if (propValue == null)
                    {
                        propInfo.SetValue(obj: instance, value: null);
                    }
                    else if (targetType == typeof(DateTime) || targetType == typeof(DateTimeOffset) | targetType == typeof(TimeSpan))
                    {
                        WMIProps prop = propInfo.GetCustomAttribute<WMIProps>();
                        var dateTime = ManagementDateTimeConverter.ToDateTime(propValue.ToString());

                        if (targetType == typeof(DateTime))
                        {
                            propInfo.SetValue(obj: instance, value: dateTime);
                        }
                        else if (targetType == typeof(DateTimeOffset))
                        {
                            propInfo.SetValue(obj: instance, value: new DateTimeOffset(dateTime));
                        }
                        else if (targetType == typeof(TimeSpan))
                        {
                            propInfo.SetValue(obj: instance, value: dateTime.TimeOfDay);
                        }
                    }
                    else
                    {
                        propInfo.SetValue(obj: instance, value: Convert.ChangeType(value: propValue, conversionType: propInfo.PropertyType));
                    }
                }
            }

            return instance;
        }

        private static string getWMIPropName(PropertyInfo propInfo)
        {
            string propName = String.Empty;

            if (checkForIgnorePors(propInfo))
            {
                WMIProps prop = propInfo.GetCustomAttribute<WMIProps>();
                if (prop == null)
                    propName = propInfo.Name;
                else
                    propName = prop.Name;
            }

            return propName;
        }

        private static bool checkForIgnorePors(PropertyInfo propInfo)
        {
            bool result = false;
            WMIIgnore ignoreProp = propInfo.GetCustomAttribute<WMIIgnore>();


            if (ignoreProp == null)
                result = true;

            return result;

        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}
