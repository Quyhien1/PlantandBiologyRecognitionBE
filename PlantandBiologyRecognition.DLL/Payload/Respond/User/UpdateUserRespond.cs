using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.User
{
    public class UpdateUserRespond
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
