using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace PlantandBiologyRecognition.DAL.Payload.Request.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? Avatar { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
