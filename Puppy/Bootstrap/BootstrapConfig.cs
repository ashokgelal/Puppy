namespace PuppyFramework.Bootstrap
{
    public class BootstrapConfig
    {
        #region Properties

        public bool EnableMenuService { get; set; }

        public bool EnableUpdaterService { get; set; }

        public bool RegisterDefaultPrismServices { get; set; }

        #endregion

        #region Constructors

        public BootstrapConfig()
        {
            RegisterDefaultPrismServices = true;
            EnableUpdaterService = false;
        }

        #endregion

        #region Methods

        public PuppyBootstrapper Boot()
        {
            var bootstrapper = new PuppyBootstrapper();
            bootstrapper.Run(this);
            return bootstrapper;
        }

        #endregion
    }
}
