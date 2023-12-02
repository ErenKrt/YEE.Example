using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Helpers;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Entities;
using YEE.Identity.Core.Helpers.Interfaces;
using YEE.Identity.DataAccess.EntityFramework.Interfaces;

namespace YEE.Identity.Application.Services.Impl
{
    public class AuthService : IAuthService
    {
        private IGenericRepository<User> _userRepository;
        private readonly AppSettings _appSettings;
        private readonly IHasher _hasher;
        private readonly ICustomMapper _customMapper;
        public AuthService(IGenericRepository<User> userRepository, IOptions<AppSettings> appSettings, IHasher hasher, ICustomMapper mapper)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _hasher = hasher;
            _customMapper = mapper;
        }

        public async Task<LoginResponse> Login(LoginRequest data)
        {

            var user = await _userRepository.GetAll()
                .Include(x=>x.Permissions)
                .ThenInclude(x=>x.Permission)
                .FirstOrDefaultAsync(x => x.Email == data.Email && x.Password == _hasher.Hash(data.Password));
            if (user is null)
            {
                throw new InfoException("Email adresinizi veya şifrenizi yanlış girdiniz. Tekrar deneyiniz !");
            }

            var expDate = DateTime.Now.AddDays(30);

            var token = JWTBearer.CreateToken(
               signingKey: _appSettings.JWTKey,
               expireAt: expDate,
               privileges: u =>
               {
                   u.Claims.Add(new("ID", user.ID.ToString()));
                   u.Permissions.AddRange(user.Permissions.Select(x=>x.Permission.Name));
               }
           );

            return new()
            {
                AccessToken = token,
                ExpireDate = expDate
            };

        }

        public async Task<bool> Register(RegisterRequest data)
        {
            var user = await _userRepository.GetAll().AnyAsync(x => x.Email == data.Email);
            if (user is true)
            {
                throw new InfoException("EMail adresiniz veya telefon numaranız başka bir hesap tarafından kullanılıyor !");
            }

            var newUser = _customMapper.Map<User>(data);
            newUser.Password = _hasher.Hash(data.Password);
            await _userRepository.InsertAsync(newUser);

            return true;
        }
    }
}
