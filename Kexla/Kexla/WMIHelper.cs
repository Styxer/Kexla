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
                sb.AppendLine(item.propName + ": " + item.propValue);
                
            }

            return sb.ToString();
        }
    }
}
