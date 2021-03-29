using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebLogic.Services
{
    public class UserService
    {
        private readonly SurveyDbContext _context;

        public UserService(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task ChangeUserNameAsync(User userModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userModel.Email);
            user.Name = userModel.Name;

            await _context.SaveChangesAsync();
        }

        public async Task<ChangePasswordResult> ChangePasswordAsync(int userId, string newPassword, string oldPassword)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                var isCorrect = PasswordHasher.IsCorrectPassword(user, oldPassword);
                if (isCorrect)
                {
                    var newHashedPassword = PasswordHasher.HashPassword(newPassword);
                    user.PasswordHash = newHashedPassword;

                    await _context.SaveChangesAsync();

                    return new ChangePasswordResult(true, null);
                }

                return new ChangePasswordResult(false, "The old password doesn't match!");
            }

            return new ChangePasswordResult(false, "User is not found!");
        }

        public class ChangePasswordResult
        {
            public bool IsSuccess { get; }
            public string Error { get; }
            public ChangePasswordResult(bool isSuccess, string error)
            {
                IsSuccess = isSuccess;
                Error = error;
            }
        }
    }
}
