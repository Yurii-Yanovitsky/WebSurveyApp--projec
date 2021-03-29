using System.Text;

namespace WebLogic.Services
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var byted = Encoding.UTF8.GetBytes(password);
            var sha1 = System.Security.Cryptography.SHA1CryptoServiceProvider.Create();
            var hashedbytes = sha1.ComputeHash(byted);

            return Encoding.UTF8.GetString(hashedbytes);
        }

        public static bool IsCorrectPassword(User user, string password)
        {
            var hashedpassword = HashPassword(password);

            return hashedpassword == user.PasswordHash ? true : false;
        }
    }
}
