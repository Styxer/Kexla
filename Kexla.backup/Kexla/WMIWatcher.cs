using Kexla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Kexla
{
    public class WMIWatcher
    {
        ManagementEventWatcher watcher;

        public delegate void WMIEventHandler(object sender, WMIEventArgs e);
        public event WMIEventHandler WMIEventArrived;

        private string _scope;
        private string _query;
        private Type _type;



        public WMIWatcher(string scope, Type type)
        {
            _scope = scope;
            _type = type;

            string className = HelperFuncs.getClassName(type);
       
          
            if (TestIfEventWatchable(scope, className))
            {
                _query = $"SELECT * FROM {className}";
                CreateAndStartWatcher(); 
            }
            else
            {
                throw new Exception("Class is not an event class.");
            }
        }

        private void CreateAndStartWatcher()
        {
            watcher = new ManagementEventWatcher(_scope, _query);
            watcher.EventArrived += Watcher_EventArrived;
            watcher.Start();
        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            object obj = HelperFuncs.getSearchObjects(e.NewEvent, _type);
            WMIEventArrived(this, new WMIEventArgs { Object = obj });
        }


        public void StopWatcher()
        {
            watcher.Stop();
        }

        private bool TestIfEventWatchable(string classNamespace, string className)
        {
            bool result = false;
           using ( ManagementObjectSearcher searcher =
                     new ManagementObjectSearcher(
                     new ManagementScope(
                     classNamespace),
                     new WqlObjectQuery(
                     String.Format("SELECT * FROM meta_class WHERE __Class = '{0}'", className)),
                     null))
            {
                foreach (ManagementClass wmiClass in searcher.Get())
                {
                    {
                        if (wmiClass.Derivation.Contains("__Event"))
                        {
                            result = true;
                            break;

                        }

                    }

                }
            }

            return result;

        }
        
    }

    public class WMIEventArgs : EventArgs
    {
        public object Object { get; set; }
    }



}
