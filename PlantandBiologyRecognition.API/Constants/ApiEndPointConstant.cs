namespace PlantandBiologyRecognition.API.Constants
{
    public static class ApiEndPointConstant
    {
        public const string RootEndPoint = "/api";
        public const string Apiversion = "/v1";
        public const string ApiEndPoint = RootEndPoint + Apiversion;
        public static class Users
        {
            public const string UsersEndPoint = ApiEndPoint + "/users";
            public const string CreateUserEndpoint = UsersEndPoint + "/create-user";
            public const string GetUserByIdEndpoint = UsersEndPoint + "/get-user-by-id/{userId}";
            public const string DeleteUserEndpoint = UsersEndPoint + "/delete-user/{userId}";
            public const string UpdateUserEndpoint = UsersEndPoint + "/update-user/{userId}";
        }
        public static class Feedbacks
        {
            public const string FeedbackEndpoint = ApiEndPoint + "/feedback";
            public const string CreateFeedback = FeedbackEndpoint + "/create-feedback";
        }
    }

}
