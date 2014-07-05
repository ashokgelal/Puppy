#region usings

using System;
using System.Linq;
using System.Reflection;

#endregion

namespace PuppyFramework.Helpers
{
    public static class AssemblyHelpers
    {
        public static T GetEntryAssemblyAttribute<T>() where T : Attribute
        {
            var assembly = Assembly.GetEntryAssembly();
            var attributes = assembly.GetCustomAttributes(true);
            return attributes.OfType<T>().FirstOrDefault();
        }

	// use this in tests
        public static void SetEntryAssembly(Assembly assembly = null)
        {
            var manager = new AppDomainManager();
            var entryAssemblyfield = manager.GetType().GetField("m_entryAssembly", BindingFlags.Instance | BindingFlags.NonPublic);
            if (entryAssemblyfield != null)
            {
                entryAssemblyfield.SetValue(manager, assembly ?? Assembly.GetCallingAssembly());
            }

            var domain = AppDomain.CurrentDomain;
            var domainManagerField = domain.GetType().GetField("_domainManager", BindingFlags.Instance | BindingFlags.NonPublic);
            if (domainManagerField != null)
            {
                domainManagerField.SetValue(domain, manager);
            }
        }
    }
}