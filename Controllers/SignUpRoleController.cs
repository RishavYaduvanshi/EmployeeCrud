using EmployeeDetails.Dto;
using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using EmployeeDetails.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.RegularExpressions;

namespace EmployeeDetails.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SignUpRoleController : Controller
    {
        private readonly ISignUpRepo _signUpRepository;
        private readonly IRoleRepo _roleRepo;
        private readonly ISignupRolesRepo _signUproleRepo;
        private readonly Mail _mail;

        public SignUpRoleController(ISignUpRepo signUpRepository, IRoleRepo roleRepo, ISignupRolesRepo signupRolesRepo, Mail mail)
        {
            _signUpRepository = signUpRepository;
            _roleRepo = roleRepo;
            _signUproleRepo = signupRolesRepo;
            _mail = mail;
        }

        [HttpGet]

        public ActionResult<SignUp> GetSignUpById(int id)
        {
            SignUp signUp = _signUpRepository.GetSignUpById(id);
            if (signUp == null)
            {
                return BadRequest("User Not Found");
            }
            return signUp;
        }

        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllSignUp()
        {
            var signlist = _signUpRepository.getAllSignUp().ToList();

            foreach (var signUp in signlist)
            {
                signUp.SignupRoles = new List<SignupRole>();

                foreach (var role in _signUproleRepo.GetById(signUp.UserId))
                {
                    SignupRole sr = new SignupRole()
                    {
                        SignUpId = signUp.UserId,
                        RoleId = role,
                        Role = _roleRepo.GetRoleById(role)

                    };
                    signUp.SignupRoles.Add(sr);
                }
            }
            signlist.Reverse();
            return Json(signlist);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<SignUp> AddSignUp(SignUpDto signUpDto)
        {
            

            SignUp signUp = new SignUp()
            {
                UserId = signUpDto.UserId,
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                Password = signUpDto.Password,
                SignupRoles = new List<SignupRole>()
            };
            if (_signUpRepository.DuplicatedUserName(signUp))
                return BadRequest("username already exist");
            if (_signUpRepository.DuplicatedEmail(signUp))
                return BadRequest("Email already exist");

            if (signUpDto.RoleIds == null && signUpDto.UserName == "admin")
            {
                signUpDto.RoleIds = new List<int>();
                signUpDto.RoleIds.Add(1);
                signUp.IsEmailConfirmed = true;
            }
            else if (signUpDto.RoleIds == null)
            {
                signUpDto.RoleIds = new List<int>();
                signUpDto.RoleIds.Add(2);
            }

            _signUpRepository.AddSignUp(signUp);

            _mail.SendEmail(signUp.Email, 2);

            // from admin side
            if (signUpDto.RoleIds != null)
            {
                foreach (var role in signUpDto.RoleIds)
                {
                    SignupRole ep = new SignupRole()
                    {
                        SignUpId = signUp.UserId,
                        RoleId = role,
                        Role = _roleRepo.GetRoleById(role)
                    };
                    signUp.SignupRoles.Add(ep);
                    _signUproleRepo.AddSignupRole(ep);
                }
            }

            return signUp;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult SignIn(SignIn signIn)
        {
            if (signIn.Email == null)
            {
                signIn.Email = _signUpRepository.GetEmailFromUserName(signIn.Username);
            }
            if (signIn.Username == null)
            {
                signIn.Username = _signUpRepository.GetUserNameFromEmail(signIn.Email);
            }
            if (_signUpRepository.Validate(signIn))
            {
                

                string user = signIn.Username;
                string pass = signIn.Password;
                string email = signIn.Email;

                bool validate = _signUpRepository.EmailVerfied(signIn);
                if (!validate)
                {
                    return BadRequest("Email is not Verified");
                }
                string inputString = email + ":" + pass;
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(inputString);
                string base64EncodedString = Convert.ToBase64String(bytesToEncode);
                return Ok(new { Message = "Login Successful", Token = base64EncodedString, User = user ,Verified = validate});
            }
            else return BadRequest("UserName or Password does not match");
        }


        // Role 

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Role> AddRole(Role role)
        {
            _roleRepo.AddRole(role);
            return role;
        }

        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllRole()
        {
            return Json(_roleRepo.GetAllRole());
        }


       
        [Route("[action]")]
        public ActionResult ForgotPassword([FromQuery] string Email)
        {
            if (_mail.SendEmail(Email,1))
            {
                return Ok(new { Message = "Send Email Sucessfully" });
            }
            else
                return BadRequest("Something went wrong");
        }

        [Route("[action]")]
        public ActionResult ResetPassword([FromQuery] string NewPassword, [FromQuery] string Email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@gmail\.com$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(Email))
                Email = _signUpRepository.GetEmailFromUserName(Email);
            if (_signUpRepository.ResetPassword(NewPassword, Email))
                return Ok(new { message = "Password Reset Succesfully" });
            else
                return BadRequest("Something went wrong");
        }
        [Route("[action]")]
        public ActionResult EmailVerfiedDone([FromQuery] string Email)
        {
            
            if (_signUpRepository.SetEmailVerification(Email))
                return Ok(new { message = "Verified" });
            else 
                return BadRequest(new { message = "Email Does not exist" });
        }


    }
}
