using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Models.Services.Auth;
using YEE.Identity.Application.Models.Services.Users.DTOs;
using YEE.Identity.Core.Entities.Users;

namespace YEE.Identity.Application
{
    public class DtoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, User>();
            config.NewConfig<User, UserDTO>();
        }
    }
}
