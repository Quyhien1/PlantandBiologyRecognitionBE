using Microsoft.AspNetCore.Authorization;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Models;

namespace PlantandBiologyRecognition.API.Validators
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params RoleName[] roleEnums)
        {
            var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}
