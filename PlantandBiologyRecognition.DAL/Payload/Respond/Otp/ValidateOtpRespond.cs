using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.Otp
{
    public class ValidateOtpRespond
    {
        public bool Success { get; set; }
        public int AttemptsLeft { get; set; }
    }
}
