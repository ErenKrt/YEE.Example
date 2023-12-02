using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Models.Services.Auth;

namespace YEE.Identity.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest data);
        Task<bool> Register(RegisterRequest data);
    }
}
