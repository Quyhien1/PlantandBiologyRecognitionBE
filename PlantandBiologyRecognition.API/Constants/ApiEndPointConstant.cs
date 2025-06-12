namespace PlantandBiologyRecognition.API.Constants
{
    public static class ApiEndPointConstant
    {
        public const string RootEndPoint = "/api";
        public const string Apiversion = "/v1";
        public const string ApiEndPoint = RootEndPoint + Apiversion;
        public static class User
        {
            public const string UserEndPoint = ApiEndPoint + "/user";
            public const string CreateUser = UserEndPoint + "/create-user";
        }
        public static class Feedbacks
        {
            public const string FeedbackEndpoint = ApiEndPoint + "/feedback";
            public const string CreateFeedback = FeedbackEndpoint + "/create-feedback";
        }
    }

}
