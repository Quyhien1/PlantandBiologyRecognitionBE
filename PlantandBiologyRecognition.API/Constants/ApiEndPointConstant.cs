namespace PlantandBiologyRecognition.API.Constants
{
    public static class ApiEndPointConstant
    {
        public const string RootEndPoint = "/api";
        public const string Apiversion = "/v1";
        public const string ApiEndPoint = RootEndPoint + Apiversion;
        public static class Account
        {
            public const string AccountEndPoint = ApiEndPoint + "/account";
            public const string CreateAccount = AccountEndPoint + "/create-account";
        }
    }

}
