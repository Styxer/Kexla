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

        public static string getClassName(Type type)
        {
            var attribute = type.GetCustomAttribute<WMIClass>(inherit: false);            
            string className = String.Empty;
            if (attribute != null)
            {
                className = attribute.Name;
            }

            return className;

        }

        public static string getNamespace(Type type)
        {
            var attribute = type.GetCustomAttribute<WMIClass>(inherit: false);          
            string classNameSpace = String.Empty;
            if (attribute.Namespace != null)
            {
                classNameSpace = attribute.Namespace.ToString();
            }

            return classNameSpace;
        }

        public static List<string> getSearchPropsNames(Type type)
        {
            var propsList = new List<string>();

            foreach (var propInfo in type.GetProperties())
            {
                WMIIgnore ignoreProp = propInfo.GetCustomAttribute<WMIIgnore>(inherit: false);

                if (ignoreProp == null)
                {
                    string prop = getWMIPropName(propInfo);
                    if (!String.IsNullOrEmpty(prop))
                    {
                        propsList.Add(getWMIPropName(propInfo));
                    }
                }
            }

            return propsList;
        }

        public static List<object> getSearchPropValues(object obj)
        {
            var propsValues = new List<object>();
            foreach (var propInfo in obj.GetType().GetProperties())
            {
                WMIIgnore ignoreProp = propInfo.GetCustomAttribute<WMIIgnore>(inherit: false);
                if (ignoreProp == null)
                {
                    var value = propInfo.GetValue(obj) ?? "null";
                    propsValues.Add(value); 
                }
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
                    else if (targetType == typeof(DateTime) || targetType == typeof(DateTimeOffset) || targetType == typeof(TimeSpan))
                    {
                        
                        WMIProps prop = propInfo.GetCustomAttribute<WMIProps>(inherit: false);
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
                WMIProps prop = propInfo.GetCustomAttribute<WMIProps>(inherit: false);
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
            WMIIgnore ignoreProp = propInfo.GetCustomAttribute<WMIIgnore>(inherit: false);


            if (ignoreProp == null)
                result = true;

            return result;

        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }


        //public static string myToString(object obj)
        //{
       
    }
}
