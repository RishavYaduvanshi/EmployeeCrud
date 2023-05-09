using EmployeeDetails.DB;
using EmployeeDetails.Dto;
using EmployeeDetails.Model;
using EmployeeDetails.Services;

namespace EmployeeDetails.Repository
{
    public class MySQLSignUpRepo : ISignUpRepo
    {
        private readonly EmployeeDbContext _context;

        public MySQLSignUpRepo(EmployeeDbContext context)
        {
            _context = context;
        }

        public bool DuplicatedUserName(SignUp signUp)
        {
            return _context.SignUps.Any(user =>  user.UserName.ToLower() == signUp.UserName.ToLower());
        }
        public bool DuplicatedEmail(SignUp signUp)
        { 
            return _context.SignUps.Any(user => user.Email == signUp.Email);
        }
        public SignUp AddSignUp(SignUp signUp)
        {
            _context.SignUps.Add(signUp);
            _context.SaveChanges();
            return signUp;
        }
        public IEnumerable<SignUp> getAllSignUp()
        {
           return _context.SignUps;
        }

        public SignUp GetSignUpById(int id)
        {
            return _context.SignUps.Find(id);
        }

        public bool Validate(SignIn signIn)
        {
        
            return _context.SignUps.Any(u => u.UserName== signIn.Username && u.Password == signIn.Password);
        }

        public bool EmailVerfied(SignIn signIn)
        {
            var ans = _context.SignUps
                .Where(u => u.UserName == signIn.Username)
                .Select(u => u.IsEmailConfirmed)
                .FirstOrDefault();
            if(ans == null)
                return false;
            return true;

        }

        public bool SetEmailVerification(string EmailVerification)
        {
            SignUp signUp = _context.SignUps.FirstOrDefault(e => e.Email == EmailVerification);
            if (signUp == null)
                return false;
            signUp.IsEmailConfirmed = true;
            _context.SaveChanges();
            return true;
        }

        public bool ResetPassword(string NewPassword, string Email)
        {
            var user = _context.SignUps.SingleOrDefault(u => u.Email == Email);
            if (user == null)
            {
                return false; // user not found
            }
            user.Password = NewPassword;
            _context.SaveChanges();
            return true;
        }
    }
}
