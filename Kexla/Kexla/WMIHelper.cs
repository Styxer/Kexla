using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class WMIHelper
    {
        public static string toString(object  obj) 
        {
            var sb = new StringBuilder();

            var propsNames = HelperFuncs.getSearchPropsNames(obj.GetType());
            var propValues = HelperFuncs.getSearchPropValues(obj);

            var propNamesAndValues = propsNames.Zip(propValues, (pName, pValue) => new { propName = pName, propValue = pValue });

            foreach (var item in propNamesAndValues)
            {
                var value = item.propValue is string
                ? item.propValue
                : string.Join(", ", (IEnumerable<object>)item.propValue);
                sb.AppendLine(item.propName + ": " + value);
                
            }

            return sb.ToString();
        }
    }
}
