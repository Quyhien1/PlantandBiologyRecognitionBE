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
            public const string UserRolesEndPoint = ApiEndPoint + "/user-roles";
            public const string GetUserRoleByIdEndpoint = UserRolesEndPoint + "/{roleId}";
            public const string GetUserRolesByUserIdEndpoint = UserRolesEndPoint + "/user/{userId}";
            public const string DeleteUserRoleEndpoint = UserRolesEndPoint + "/{roleId}";
            public const string UpdateUserRoleEndpoint = UserRolesEndPoint + "/{roleId}";
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
        public static class Samples
        {
            public const string SampleEndpoint = ApiEndPoint + "/sample";
            public const string CreateSample = SampleEndpoint + "/create";
            public const string UpdateSample = SampleEndpoint + "/update";
            public const string DeleteSample = SampleEndpoint + "/delete";
            public const string GetSampleById = SampleEndpoint + "/{id}";
            public const string GetAllSamples = SampleEndpoint + "/all";
        }
        public static class SampleDetails
        {
            public const string SampleDetailEndpoint = ApiEndPoint + "/sample-detail";
            public const string CreateSampleDetail = SampleDetailEndpoint + "/create";
            public const string UpdateSampleDetail = SampleDetailEndpoint + "/update";
            public const string DeleteSampleDetail = SampleDetailEndpoint + "/delete";
            public const string GetSampleDetailById = SampleDetailEndpoint + "/{id}";
            public const string GetAllSampleDetails = SampleDetailEndpoint + "/all";
        }
        public static class SampleImages
        {
            public const string SampleImageEndpoint = ApiEndPoint + "/sample-image";
            public const string CreateSampleImage = SampleImageEndpoint + "/create";
            public const string UpdateSampleImage = SampleImageEndpoint + "/update";
            public const string DeleteSampleImage = SampleImageEndpoint + "/delete";
            public const string GetSampleImageById = SampleImageEndpoint + "/{id}";
            public const string GetAllSampleImages = SampleImageEndpoint + "/all";
        }
        public static class SavedSamples
        {
            public const string SavedSampleEndpoint = ApiEndPoint + "/saved-sample";
            public const string CreateSavedSample = SavedSampleEndpoint + "/create";
            public const string UpdateSavedSample = SavedSampleEndpoint + "/update";
            public const string DeleteSavedSample = SavedSampleEndpoint + "/delete";
            public const string GetSavedSampleById = SavedSampleEndpoint + "/{id}";
            public const string GetAllSavedSamples = SavedSampleEndpoint + "/all";
        }
    }
}
