using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebLogic.Services
{
    public class AccountService
    {
        private readonly SurveyDbContext _context;

        public AccountService(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task<RegistResult> RegistSuccessAsync(RegisterModel model)
        {
            var userAlreadyExists = await _context.Users.AnyAsync(u => u.Email == model.Email);

            if (userAlreadyExists)
            {
                return new RegistResult(false, "Login is already in use");
            }
            else
            {
                var user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = PasswordHasher.HashPassword(model.Password),
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new RegistResult(true, null);
            }
        }

        public async Task<LoginResult> LoginSuccessAsync(LoginModel loginModel)
        {

            var userOrNull = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (userOrNull is { } user)
            {
                var isCorrect = PasswordHasher.IsCorrectPassword(user, loginModel.Password);
                if (isCorrect)
                {

                    return new LoginResult(true, user);
                }
            }

            return new LoginResult(false, null);
        }

    }

    public class RegistResult
    {
        public RegistResult(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }

    public class LoginResult
    {
        public LoginResult(bool isSuccess, User user)
        {
            IsSuccess = isSuccess;
            User = user;
        }

        public bool IsSuccess { get; set; }
        public User User { get; set; }

    }
}
