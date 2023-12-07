using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> Register(RegisterDTO registerDTO);
        IResult Login(LoginDTO loginDTO);
        IResult VerifyEmail(string email, string verifyToken);
        IDataResult<User> GetUser(int userId);
    }
}
