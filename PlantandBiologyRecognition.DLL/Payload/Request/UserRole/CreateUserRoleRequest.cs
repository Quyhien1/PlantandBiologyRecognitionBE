using PlantandBiologyRecognition.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request.UserRole
{
    public class CreateUserRoleRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [EnumDataType(typeof(RoleName))]
        public RoleName RoleName { get; set; }
    }
}
