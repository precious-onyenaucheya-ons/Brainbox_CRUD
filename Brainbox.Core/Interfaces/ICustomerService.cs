using Brainbox.Core.DTO;
using Brainbox.Domain.Models;

namespace Brainbox.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseDTO<LoginResponseDTO>> Login(LoginDTO model);
        Task<ResponseDTO<bool>> Register(RegisterDTO model);
    }
}