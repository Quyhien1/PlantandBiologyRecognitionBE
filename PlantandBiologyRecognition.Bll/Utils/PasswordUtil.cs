using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Utils
{
    public static class PasswordUtil
    {
        private const int HashingRound = 10;
        public static async Task<string> HashPassword(string rawPassword)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(rawPassword, workFactor: HashingRound));
        }

        public static async Task<bool> VerifyPassword(string rawPassword, string hashedPassword)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword));
        }
    }
}
