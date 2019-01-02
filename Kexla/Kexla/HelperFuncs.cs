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

        public static string getNamespace(Type t)
        {
            var att = t.GetCustomAttribute<WMIClass>(true);
            string classNameSpace = String.Empty;

            if (att.Namespace != null)
            {
                classNameSpace = att.Namespace.ToString();
            }

            return classNameSpace;
        }

        public static List<string> getSearchPropsNames(Type type)
        {
            var propsList = new List<string>();

            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                string prop = getWMIPropName(propInfo);
                if (!String.IsNullOrEmpty(prop))
                {
                    propsList.Add(getWMIPropName(propInfo));
                }
            }

            return propsList;
        }

        public static List<object> getSearchPropValues(object obj)
        {
            var propsValues = new List<object>();
            foreach (var info in obj.GetType().GetProperties())
            {
                var value = info.GetValue(obj) ?? "null";
                propsValues.Add(value);
            }

            return propsValues;
        }

        public static object getSearchObjects(ManagementBaseObject manageObject, Type type)
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


        public static string toString(object obj)
        {
            var sb = new StringBuilder();

            var propsNames = HelperFuncs.getSearchPropsNames(obj.GetType());
            var propValues = HelperFuncs.getSearchPropValues(obj);

            var propNamesAndValues = propsNames.Zip(propValues, (n, w) => new { propName = n, propValue = w });

            foreach (var item in propNamesAndValues)
            {
                sb.AppendLine(item.propName + ": " + item.propValue);
            }

            return sb.ToString();
        }
    }
}
