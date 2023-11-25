using Core.Utilities.Results.Abstract;
using Entities.DTOs.UserDTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Register(RegisterDTO registerDTO);
        IResult Login(LoginDTO loginDTO);
        IResult VerifyEmail(string email, string verifyToken);
    }
}
