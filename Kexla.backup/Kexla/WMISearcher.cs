using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading;
using Kexla.Classes;


namespace Kexla
{
    public class WMISearcher
    {       
        public ManagementScope Scope { get; set; }

        #region Ctor's
        /// <summary>
        /// Creates a WMISearcher object targeting the CimV2 scope. Default credentials are used.
        /// </summary>
        public WMISearcher()
        {
            Scope = new ManagementScope(@"root\CimV2")
            {
                Options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate
                }
            };
        }

        /// <summary>
        /// Creates a WMISearcher object targeting the desired scope scope. Default credentials are used.
        /// </summary>
        /// <param name="scope">WMI namespace</param>
        public WMISearcher(string scope)
        {
            Scope = new ManagementScope(scope)
            {
                Options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate
                }
            };
        }
        /// <summary>
        /// Creates a WMIHelper object targeting the desired scope on the specified hostname with optional authentication level(Default lvl is Default)
        /// Beware that in order to make WMI calls work, 
        /// the user running the application must have the corresponding privileges on the client machine. 
        /// Otherwise it will throw an 'Access Denied' exception.
        /// </summary>
        /// <param name="scope">WMI namespace</param>
        /// <param name="hostname">Client machine</param>
        /// <param name="auth">Athentication level</param>
        public WMISearcher(string scope, string hostname, AuthenticationLevel auth = AuthenticationLevel.Default)
        {
            Scope = new ManagementScope(String.Format("\\\\{0}\\{1}", hostname, scope))
            {
                Options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = auth
                }
            };
        }

        /// <summary>
        /// Creates a WMIHelper object targeting the desired scope on the specified hostname with a domain to use when authorizing WMI calls on the client machine.
        /// Beware that in order to make WMI calls work, the user running the application must have the corresponding privileges on the client machine. Otherwise it will throw an 'Access Denied' exception.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="hostname"></param>
        /// <param name="domain"></param>
        /// <param name="auth">Athentication level</param>
        public WMISearcher(string scope, string hostname, string domain, AuthenticationLevel auth = AuthenticationLevel.Default)
        {
            Scope = new ManagementScope(String.Format("\\\\{0}\\{1}", hostname, scope))
            {
                Options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = auth,
                    Authority = $"ntlmdomain:{domain}"
                }
            };
        }

          /// <summary>
        /// Creates a WMIHelper object targeting the desired scope on the specified hostname with specified credentials.
        /// </summary>
        /// <param name="scope">WMI namespace</param>
        /// <param name="hostname">Client machine</param>
        /// <param name="username">Username that will make the WMI connection</param>
        /// <param name="password">The username´s password</param>
        /// <param name="auth">Athentication level</param>
        public WMISearcher(string scope, string hostname, string username, string password, AuthenticationLevel auth = AuthenticationLevel.Default)
        {
            Scope = new ManagementScope(String.Format("\\\\{0}\\{1}", hostname, scope))
            {
                Options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = auth,
                    Username = username,
                    Password = password
                }
            };
        }
        #endregion
             

        /// <summary>
        /// Runs a query against WMI. It will return all instances of the class corresponding to the WMI class set on the Type on IEnumerable.
        /// </summary>
        /// <typeparam name="T">The Type of IEnumerable that will be returned</typeparam>
        /// <returns></returns>     
        public IEnumerable<T> Query<T>(string searchQuery = "")
        {

            searchQuery = String.IsNullOrEmpty(searchQuery) ? HelperFuncs.BuildQuery<T>() : searchQuery;


            using (var searcher = new ManagementObjectSearcher(Scope, new ObjectQuery(searchQuery)))
            {

                using (var searcherData = searcher.Get())
                {
                    
                    foreach (ManagementObject obj in searcherData)
                    {                        
                        yield return (T)HelperFuncs.getSearchObjects(obj, typeof(T));                      
                                              

                    }
                }
            }       
        }

        public ILookup<string, WMIKeyValues> GetPrimaryKeyValues<T>()
        {           
           
            var classNmae = HelperFuncs.getClassName(typeof(T));

            var list = new List<WMIKeyValues>();                    


            using (var wmiObject = new ManagementClass(Scope.Path.NamespacePath, classNmae, options: null))
            {
                foreach (ManagementObject c in wmiObject.GetInstances())
                {
                    foreach (PropertyData p in c.Properties)
                    {
                        foreach (QualifierData q in p.Qualifiers)
                        {
                            if (q.Name.Equals("key"))
                            {
                                list.Add(new WMIKeyValues(p.Name, c.GetPropertyValue(p.Name)));
                            }
                        }
                    }
                } 
            }

            ILookup<string, WMIKeyValues> byWMIKey = list.ToLookup(x => x.Key);

            return byWMIKey;
          
        }

        public async Task<ILookup<string, WMIKeyValues>> GetPrimaryKeyValuesAsync<T>()
        {
            return await Task.Run(() => GetPrimaryKeyValues<T>());
        }


        /// <summary>
        /// Runs a async query against WMI. It will return all instances of the class corresponding to the WMI class set on the Type on IEnumerable.
        /// </summary>
        /// <typeparam name="T">The Type of IEnumerable that will be returned</typeparam>
        /// <returns></returns>
        public  async Task<IEnumerable<T>> QueryAsAsync<T>()
        {            
            return  await  Task.Run(() =>  Query<T>());           
            
        }


    }
}
