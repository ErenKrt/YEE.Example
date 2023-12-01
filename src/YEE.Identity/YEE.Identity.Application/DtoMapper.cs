using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Models.DTOs;
using YEE.Identity.Core.Entities;

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
