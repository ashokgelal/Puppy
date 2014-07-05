#region usings

using System;
using System.IO;
using System.Reflection;

#endregion

namespace PuppyFramework.Helpers
{
    public static class IOHelpers
    {
        public static string CombineWithLocalAppDataPath(this string path, string rootDir = null, bool replaceWhitespacesWithUnderscores = true)
        {
            return Path.Combine(LocalAppDir(rootDir, replaceWhitespacesWithUnderscores), path);
        }

        public static string LocalAppDir(string rootDir = null, bool replaceWhitespacesWithUnderscores = true)
        {
            var company = rootDir ?? AssemblyHelpers.GetEntryAssemblyAttribute<AssemblyTitleAttribute>().Title;
            if (replaceWhitespacesWithUnderscores)
            {
                company = company.Replace(' ', '_');
            }
            var localRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localRoot, company);
        }
    }
}