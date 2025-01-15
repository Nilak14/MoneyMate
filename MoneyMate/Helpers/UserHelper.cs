using MoneyMate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyMate.Helpers
{
    public class UserHelper
    {
        private static string userFilePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");

        public static UserModel getSession()
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

        public static void  RegisterUser(UserModel user)
        {
            var json = JsonSerializer.Serialize(user);
             File.WriteAllText(userFilePath,json);
        }

         

    }
}
