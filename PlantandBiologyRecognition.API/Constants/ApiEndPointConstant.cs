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
        public static class UserRoles
        {
            public const string UserRolesEndPoint = "api/user-roles";
            public const string GetUserRoleByIdEndpoint = "api/user-roles/{roleId}";
            public const string GetUserRolesByUserIdEndpoint = "api/user-roles/user/{userId}";
            public const string DeleteUserRoleEndpoint = "api/user-roles/{roleId}";
            public const string UpdateUserRoleEndpoint = "api/user-roles/{roleId}";
        }
        public static class Feedbacks
        {
            public const string FeedbackEndpoint = ApiEndPoint + "/feedback";
            public const string CreateFeedback = FeedbackEndpoint + "/create";
            public const string UpdateFeedback = FeedbackEndpoint + "/update";
            public const string DeleteFeedback = FeedbackEndpoint + "/delete";
            public const string GetFeedbackById = FeedbackEndpoint + "/{id}";
            public const string GetAllFeedbacks = FeedbackEndpoint + "/all";
        }
        public static class Categories
        {
            public const string CategoryEndpoint = ApiEndPoint + "/category";
            public const string CreateCategory = CategoryEndpoint + "/create";
            public const string UpdateCategory = CategoryEndpoint + "/update";
            public const string DeleteCategory = CategoryEndpoint + "/delete";
            public const string GetCategoryById = CategoryEndpoint + "/{id}";
            public const string GetAllCategories = CategoryEndpoint + "/all";
        }
        public static class LearningTips
        {
            public const string LearningTipEndpoint = ApiEndPoint + "/learning-tip";
            public const string CreateLearningTip = LearningTipEndpoint + "/create";
            public const string UpdateLearningTip = LearningTipEndpoint + "/update";
            public const string DeleteLearningTip = LearningTipEndpoint + "/delete";
            public const string GetLearningTipById = LearningTipEndpoint + "/{id}";
            public const string GetAllLearningTips = LearningTipEndpoint + "/all";
        }
    }
}
