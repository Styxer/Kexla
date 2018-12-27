using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kexla;


namespace KexlaTest
{
    [TestClass]
    public class SearchExamples
    {
        [TestMethod]

        public void ReadExample()
        {
            WMISearcher searcher = new WMISearcher(WMIClassTypes.CimV2);

            var result = searcher.Query<NetworkAdapter>();

            foreach (var item in result)
            {
                System.Diagnostics.Trace.WriteLine(item);
            }
        }
    }


}
