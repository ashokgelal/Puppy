namespace PuppyFramework.Bootstrap
{
    public class BootstrapConfig
    {
        #region Properties

        public bool EnableMenuService { get; set; }

        public bool EnableSoftwareUpdater { get; set; }

        public bool RunWithDefaultConfig { get; set; }

        #endregion

        #region Constructors

        public BootstrapConfig()
        {
            RunWithDefaultConfig = true;
        }

        #endregion

        public void Boot()
        {
            new PuppyBootstrapper().Run(this);
        }
    }
}