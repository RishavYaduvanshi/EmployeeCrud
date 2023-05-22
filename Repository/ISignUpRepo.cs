using EmployeeDetails.Dto;
using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface ISignUpRepo
    {
        public SignUp AddSignUp(SignUp signUp);
        public SignUp GetSignUpById(int id);
        public IEnumerable<SignUp> getAllSignUp();

        public bool ResetPassword(string NewPassword,string Email);
        public bool Validate(SignIn signIn);
        public bool EmailVerfied(SignIn signIn);

        public bool SetEmailVerification(string EmailVerification);

        public string GetEmailFromUserName(string UserName);
        public string GetUserNameFromEmail(string Email);
        public bool DuplicatedEmail(SignUp signUp);
        public bool DuplicatedUserName(SignUp signUp);



    }
}
