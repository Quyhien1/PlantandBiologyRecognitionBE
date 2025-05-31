using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PlantandBiologyRecognition.DAL.Payload.Respond
{
    public class CreateAccountRespond
    {
        public Guid Accountid { get; set; }
        public string Username { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public string Roleid { get; set; }

    }
}
