namespace Hubtel.Wallets.Api.Contracts.DataDtos
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class WalletsApiRoutes
        {
            public const string WalletsBase = Base + "/" + "agents";
            public const string ById = "{id}";
        }
    }
}
