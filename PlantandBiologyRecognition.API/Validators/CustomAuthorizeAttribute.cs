using Microsoft.AspNetCore.Authorization;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Models;
using System.Linq;

namespace PlantandBiologyRecognition.API.Validators
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params RoleName[] roleEnums)
        {
            var allowedRolesAsString = roleEnums.Select(x => x.ToString());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}
