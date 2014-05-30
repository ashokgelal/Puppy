#region Usings

using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;

#endregion

namespace PuppyFramework.Bootstrap
{
    ///  <summary>
    ///  Custom attribute for all of Puppy's prism modules. This adds the
    ///  ability to decorate a module with a GUID and the main application later 
    ///  can decide whether to initialize this module or not.
    ///  This attribute can be applied to an implementor of <see cref="IModule"/> interface
    ///  only once and can be only be applied to a class.
    ///  </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PuppyModuleExportAttribute : ModuleExportAttribute
    {
        #region Properties

        public string Guid { get; set; }

        #endregion

        #region Constructors

        public PuppyModuleExportAttribute(Type moduleType)
            : base(moduleType)
        {
        }

        public PuppyModuleExportAttribute(string moduleName, Type moduleType)
            : base(moduleName, moduleType)
        {
        }

        #endregion
    }
}
