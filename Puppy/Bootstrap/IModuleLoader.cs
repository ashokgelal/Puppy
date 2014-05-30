namespace PuppyFramework.Bootstrap
{
    /// <summary>
    /// Interface for a class that determines whether a module with the given
    /// attribute should be created and initialized or not. One project should
    /// have one and only one implementor of this interface.
    /// </summary>
    public interface IModuleLoader
    {
        #region Methods

        /// <summary>
        /// Check to see if a module with <see cref="PuppyModuleExportAttribute"/> can be created and initialized.
        /// </summary>
        /// <param name="exportAttribute">PuppyModule attribute of a module</param>
        /// <returns>True if a module can be loaded, otherwise False</returns>
        bool ShouldLoadModuleWithAttribute(PuppyModuleExportAttribute exportAttribute);

        /// <summary>
        /// Check to see if a module without <see cref="PuppyModuleExportAttribute"/> is allowed to be created and initialized.
        /// </summary>
        /// <returns></returns>
        bool ShouldAllowNonPuppyModule();

        #endregion
    }
}
