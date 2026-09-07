using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace SuperariLife.Application.PasswordServices
{
    public static class PasswordGenerator
    {
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string SpecialCharacters = "!@#$%^&*()-+";

        public static string Generate(int length = 12)
        {
            const string allCharacters = Uppercase + Lowercase + Numbers + SpecialCharacters;

            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                int index = RandomNumberGenerator.GetInt32(allCharacters.Length);
                password[i] = allCharacters[index];
            }

            return new string(password);
        }
    }
}
