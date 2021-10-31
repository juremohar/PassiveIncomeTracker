using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Helpers;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Services
{
    public class UserService : IUserService
    {
        private readonly DbApi _db;
        private readonly IJwtRepository _jwtRepository;

        public UserService
        (
            DbApi db,
            IJwtRepository jwtRepository
        ) 
        {
            _db = db;
            _jwtRepository = jwtRepository; 
        }

        public bool Register(UserRegisterModel model)
        {
            // TODO: add email validation, minimal password strength ...

            if (string.IsNullOrEmpty(model.Email))
                throw new ArgumentNullException(nameof(model.Email));

            if (string.IsNullOrEmpty(model.Password))
                throw new ArgumentNullException(nameof(model.Password));

            if (string.IsNullOrEmpty(model.PasswordRepeat))
                throw new ArgumentNullException(nameof(model.PasswordRepeat));

            var user = _db.Users.SingleOrDefault(x => x.Email == model.Email);
            if (user != null)
                throw new Exception("User with this email already exists");

            if (model.Password != model.PasswordRepeat)
                throw new Exception("Passwords do not match!");

            string salt = SecurityHelper.GenerateSalt(70);

            var row = new TUser
            {
                Email = model.Email,
                Password = SecurityHelper.HashPassword(model.Password, salt, 10101, 70),
                Salt = salt
            };

            _db.Add(row);
            _db.SaveChanges();

            return true;
        }

        public string Login(UserLoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
                throw new Exception("Missing param - email");

            if (string.IsNullOrEmpty(model.Password))
                throw new Exception("Missing param - password");

            var user = _db.Users.SingleOrDefault(x => x.Email == model.Email);

            if (user == null)
                throw new Exception("User with this email doesn't exist");

            string password = SecurityHelper.HashPassword(model.Password, user.Salt, 10101, 70);

            if (user.Password != password)
                throw new Exception("Password is not correct");

            var loggedIn = new LoggedInUserModel
            {
                IdUser = user.IdUser,
                Email = user.Email
            };

            var row = new TSession
            {
                IdUser = user.IdUser,
                Token = _jwtRepository.Encode(loggedIn)
            };

            _db.Add(row);
            _db.SaveChanges();

            return row.Token;
        }
    }
}
