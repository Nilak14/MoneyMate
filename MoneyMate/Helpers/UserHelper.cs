using MoneyMate.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MoneyMate.Helpers
{
    public class UserHelper
    {
        private static string userFilePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");

        // Helper method to hash password
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash bytes to string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // Helper method to verify password
        private static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == hashedPassword;
        }

        public static UserModel GetSession()
        {
            if (File.Exists(userFilePath))
            {
                var json = File.ReadAllText(userFilePath);
                return JsonSerializer.Deserialize<UserModel>(json) ?? new UserModel();
            }
            else
            {
                return new UserModel();
            }
        }

        public static void RegisterUser(UserModel user)
        {
            // Hash the password before saving
            user.password = HashPassword(user.password);

            var json = JsonSerializer.Serialize(user);
            File.WriteAllText(userFilePath, json);
        }

        public static bool ChangePassword(string currentPassword, string newPassword)
        {
            try
            {
                // Read current user data
                var user = GetSession();

                // Verify current password
                if (!VerifyPassword(currentPassword, user.password))
                {
                    return false; // Current password is incorrect
                }

                // Update with new hashed password
                user.password = HashPassword(newPassword);

                // Save updated user data
                var json = JsonSerializer.Serialize(user);
                File.WriteAllText(userFilePath, json);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        public static bool ValidateLogin(string username, string password)
        {
            try
            {
                var user = GetSession();
                return user.username == username && VerifyPassword(password, user.password);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}