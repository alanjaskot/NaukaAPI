using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProsteAPI.Auth;
using ProsteAPI.Models;
using ProsteAPI.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private IJwtAuthenticationToken _token;

        public AuthController(IUserService service,
            IJwtAuthenticationToken token)
        {
            _service = service;
            _token = token;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenModel>> Login(string login, string password)
        {
            try
            {
                var serviceResponse = _service.GetUserByLoginAndPassword(login, password);
                if(serviceResponse.Object == null)
                {
                    return await Task.FromResult(NotFound(serviceResponse.Message));
                }
                else
                {
                    var token = _token.Authenticate(serviceResponse.Object.Id, serviceResponse.Object.Login);
                    if (token == null)
                        return await Task.FromResult(new TokenModel
                        {
                            Success = false,
                            Token = null,
                            Message = "token nie został utworzony"
                        });

                    return await Task.FromResult(new TokenModel
                    {
                        Success = true,
                        Token = token,
                        Message = null
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register(string username, string password)        
        {
            var user = new UserDTO
            {
                Id = Guid.NewGuid(),
                Login = username,
                Password = password
            };
            try
            {
                var serviceResponse = _service.AddUser(user);
                if (!serviceResponse.Success)
                    return await Task.FromResult(NotFound(serviceResponse.Message));
                else
                {
                    return await Task.FromResult(Ok($"Użytkownik {user.Login} został utworzony"));
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
