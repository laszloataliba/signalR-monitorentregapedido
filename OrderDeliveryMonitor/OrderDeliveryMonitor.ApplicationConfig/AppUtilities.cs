namespace OrderDeliveryMonitor.ApplicationConfig
{
    public static class AppUtilities
    {
        public const string RELOAD_AWAITING_CONTAINER = "ReloadAwaitingContainer";        
        public const string LOAD_AWAITING_CONTAINER = "LoadAwaitingContainer";
        public const string LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS = "LoadAwaitingContainerForCustomers";
        public const string LOAD_PREPARING_CONTAINER = "LoadPreparingContainer";
        public const string LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS = "LoadPreparingContainerForCustomers";
        public const string LOAD_FINISHED_CONTAINER = "LoadFinishedContainer";
        public const string LOAD_FINISHED_CONTAINER_FOR_CUSTOMER = "LoadFinishedContainerForCustomers";

        public const string RELOADAWAITINGCONTAINER_METHOD = "ReloadAwaitingContainer";
        public const string FROMAWAITINGTOPREPARING_METHOD = "FromAwaitingToPreparing";
        public const string FROMPREPARINGTOFINISHED_METHOD = "FromPreparingToFinished";
        public const string HIDEFINISHEDORDERBYTIMEOUT_METHOD = "HideFinishedOrderByTimeOut";

        public const string AWAITING_CONTAINER = "AwaitingContainer";
        public const string PREPARING_CONTAINER = "PreparingContainer";
        public const string FINISHED_CONTAINER = "FinishedContainer";

    }
}
