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


        /// <summary>
        /// initiate the watcher bases on WMIClass, throws an exception if class in not watchable
        /// </summary>
        /// <param name="scope">Desired scopre</param>
        /// <param name="type">Type of object that will initiate the watch</param>
        public WMIWatcher(string scope, Type type)
        {
            _scope = scope;
            _type = type;

            string className = HelperFuncs.getClassName(type);     
                      
            _query = String.Format("SELECT * FROM {0}", className);
            createAndStartWatcher(); 
           
        }

        /// <summary>
        ///  Create a WMI Event Watcher
        /// </summary>
        private void createAndStartWatcher()
        {
            watcher = new ManagementEventWatcher(_scope, _query);
            watcher.EventArrived += Watcher_EventArrived;
            watcher.Start();
        }

        /// <summary>
        /// read the reciver parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            object obj = HelperFuncs.getSearchObjects(e.NewEvent, _type);
            WMIEventArrived(this, new WMIEventArgs { Object = obj });
        }


        public void stopWatcher()
        {
            watcher.Stop();
        }

      
        
    }

    public class WMIEventArgs : EventArgs
    {
        public object Object { get; set; }
    }



}
