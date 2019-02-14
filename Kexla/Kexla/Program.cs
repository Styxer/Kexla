using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Kexla
{
    class Program
    {
        static void Main(string[] args)
        {
            //   WMISearcher searcher = new WMISearcher(@"root\CimV2");
             var searcher = new WMISearcher();
            //var keyValues =  searcher.getKeyValues<PnPEntity>();
            //foreach (var item in keyValues)
            //{
            //    //Console.WriteLine(item.Key);
            //    foreach (var order in item)
            //    {
            //        Console.WriteLine("    PnPEntity key: {0} is PnPEntity value: {1}", order.Key, order.Value);
            //    }
          //  }
            // var result = searcher.Query<PnPEntity>();//.Where(x => x.Name.Contains("com"));

             var entries = searcher.Query<PnPEntity>();
             foreach (var item in entries)
             {
                 Console.WriteLine(item);
             }
          
          
            Console.ReadKey();
        }

      

        static void watcher_WMIEventArrived(object sender, WMIEventArgs e)
        {
            var SystemTrace = (ProcessStartTrace)e.Object;
            Console.WriteLine("watcher_WMIEventArrived");
            Console.WriteLine(SystemTrace.ToString());
        }
       
    }

    [WMIClass(Name: "Win32_PnPSignedDriver")]
    public class PnPSignedDriver 
    {
        public string ClassGuid { get; set; }
        public string CompatID { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public string FriendlyName { get; set; }


        public override string ToString()
        {
            return WMIHelper.toString(this);
        }

    }

    [WMIClass(Name: "Win32_PnPEntity")]
    public class PnPEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] HardwareID { get; set; }


        public override string ToString()
        {
            return WMIHelper.toString(this);
        }

    }

    [WMIClass(Name: "Win32_ThreadTrace")]
    public class ProcessStartTrace
    {
       [WMIProps(name: "ProcessID")]
       public UInt32 Process_ID { get; set; }
       public UInt32 ThreadID { get; set; }
       public UInt64 TIME_CREATED { get; set; }

       public override string ToString()
       {
           return WMIHelper.toString(this);
       }

    }

    [WMIClass(Name: "Win32_PerfFormattedData_PerfDisk_PhysicalDisk")]
    public class PhysicalDisk
    {
        public string Name { get; set; }
        public string DiskBytesPerSec { get; set; }


        public override string ToString()
        {
            return WMIHelper.toString(this);
        }

    }

    [WMIClass(Name: "WmiMonitorBrightness", Namespace: @"Root\wmi")]
    public class WmiMonitorBrightness
    {
        public bool Active { get; set; }
        public string InstanceName { get; set; }
        public UInt32 Levels { get; set; }

        public override string ToString()
        {
            return WMIHelper.toString(this);
        }

    }

    [WMIClass(Name: "WmiMonitorBrightnessMethods", Namespace: @"Root\wmi")]
    public class WmiMonitorBrightnessMethods
    {
        public bool Active { get; set; }
        public string InstanceName { get; set; }


        public void WmiSetBrightness(UInt32 Timeout, byte Brightness)
        {
            WMIMethods.ExecuteMethod(this, Timeout, Brightness);
        }

     

        public override string ToString()
        {
            return WMIHelper.toString(this);
        }



    }

    [WMIClass(Name: "Win32_Printer", Namespace: @"root\CimV2")]
    public class Printer
    {
        public string DeviceID { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }

        //public void RenamePrinter(string newName)
        //{
        //    WMIMethods.ExecuteMethod(this, newName);

        //}

        public async Task RenamePrinter(string newName)
        {
            await WMIMethods.ExecuteMethodAsync(this, newName);
        }


    }

    [WMIClass(Name: "Win32_NetworkLoginProfile ", Namespace: @"root\CimV2")]
    public class NetworkLoginProfile
    {
        public DateTime LastLogoff { get; set; }
        public DateTime LastLogon { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return WMIHelper.toString(this);
        }


    }


    [WMIClass(Name: "Win32_NetworkAdapter")]
    public class NetworkAdapter
    {
        [WMIProps(name: "MACAddress")]
        public string MACAddress { get; set; }

        public string Caption { get; set; }

        [WMIIgnore]
        public string GUID { get; set; }

        public DateTime InstallDate { get; set; }

        [WMIProps(name: "TimeOfLastReset")]
        public TimeSpan TimeOfLastResetTimeSpan { get; set; }
        [WMIProps(name: "TimeOfLastReset")]
        public DateTime TimeOfLastResetDateTime { get; set; }
        [WMIProps(name: "TimeOfLastReset")]
        public DateTimeOffset TimeOfLastResetDateTimeOffset { get; set; }

        public UInt16 AdapterTypeID { get; set; }
        public bool AutoSense { get; set; }


        public override string ToString()
        {
            return WMIHelper.toString(this);
        }






    }

   




}
