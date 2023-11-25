using AutoMapper;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using Entities.SharedModels;
using MassTransit;

// ctrl + r + g
namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDAL _userDAL;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserManager(IUserDAL userDAL, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _userDAL = userDAL;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public IResult Login(LoginDTO loginDTO)
        {
            var result = BusinessRules.CheckLogic(CheckUserEmailNotExists(loginDTO.Email),
                CheckUserLoginAttempt(loginDTO.Email),
                CheckUserPassword(loginDTO.Email, loginDTO.Password));
            var user = _userDAL.Get(x => x.Email == loginDTO.Email);

            if (!result.Success)
            {
                user.LoginAttempt += 1;
                _userDAL.Update(user);
                return new ErrorResult();
            }


            var token = Token.TokenGenerator(user, "User");

            return new SuccessResult(token);
        }

        public IResult Register(RegisterDTO registerDTO)
        {
            var result = BusinessRules.CheckLogic(CheckUserEmailExists(registerDTO.Email),
                CheckUserPasswordConfirm(registerDTO.Password, registerDTO.ConfirmPassword));

            if (!result.Success)
                return new ErrorResult(result.Message);

            var map = _mapper.Map<User>(registerDTO);
            HashingHelper.HashPassword(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            map.PasswordSalt = passwordSalt;
            map.PasswordHash = passwordHash;
            map.Token = Guid.NewGuid().ToString();
            map.TokenExpiredDate = DateTime.Now.AddMinutes(30);

            _userDAL.Add(map);

            SendEmailCommand sendEmailCommand = new()
            {
                Lastname = map.Lastname,
                Firstname = map.Firstname,
                Token = map.Token,
                Email = map.Email
            };
            _publishEndpoint.Publish<SendEmailCommand>(sendEmailCommand);
            return new SuccessResult("Registerid Successfully");
        }

        public IResult VerifyEmail(string email, string verifyToken)
        {
            var user = _userDAL.Get(x => email == x.Email);
            if(user.Token == verifyToken)
            {
                if(DateTime.Compare(user.TokenExpiredDate, DateTime.Now) < 0)
                {
                    return new ErrorResult("Date Expired");
                }
                user.EmailConfirmed = true;
                _userDAL.Update(user);
                return new SuccessResult();
            }
            return new ErrorResult(" 1 dene message");
        }

        private IResult CheckUserEmailExists(string email)
        {
            var checkEmail = _userDAL.Get(x => x.Email == email);
            if (checkEmail != null)
                return new ErrorResult();

            return new SuccessResult();
        }
        private IResult CheckUserLoginAttempt(string email)
        {
            var user = _userDAL.Get(x => x.Email == email);

            if (user.LoginAttempt >= 2)
            {
                if (user.loginAttemptExpired <= DateTime.Now)
                {
                    user.loginAttemptExpired = DateTime.Now.AddMinutes(30);
                    _userDAL.Update(user);
                    return new ErrorResult();
                }
                return new ErrorResult();
            }

            return new SuccessResult();
        }
        private IResult CheckUserEmailNotExists(string email)
        {
            var checkEmail = _userDAL.Get(x => x.Email == email);
            if (checkEmail == null)
                return new ErrorResult();

            return new SuccessResult();
        }
        private IResult CheckUserPassword(string email, string password)
        {
            var checkEmail = _userDAL.Get(x => x.Email == email);
            bool checkPassword = HashingHelper.VerifyPassword(password, checkEmail.PasswordHash, checkEmail.PasswordSalt);
            if (!checkPassword)
                return new ErrorResult();

            return new SuccessResult();
        }
        private IResult CheckUserPasswordConfirm(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return new ErrorResult();
            return new SuccessResult();
        }
    }
}
