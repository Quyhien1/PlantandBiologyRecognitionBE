using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.User
{
    public class CreateUserRespond
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
