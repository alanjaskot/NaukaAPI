using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<Response<List<UserDTO>>>> GetAllUsers()
        {
            try
            {
                var result = _service.GetAllUsers();
                if (!result.Success)
                    return await Task.FromResult(NotFound(result));
                else
                {
                    return await Task.FromResult(result);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("ModifyUser")]
        public async Task<ActionResult<Response<UserDTO>>> ModifyUser([FromBody]UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _service.UpdateUser(user);
                    if (!result.Success)
                        return await Task.FromResult(NotFound(result));
                    else
                        return await Task.FromResult(Ok(result));
                }
                else
                    return await Task.FromResult(BadRequest());
                
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult<Response<string>>> ChangePassword(Guid userId, string oldPassword, 
            string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
                return await Task.FromResult(NotFound(new Response<string>
                {
                    Success = false,
                    Message = "Podane hasła się nie zgadzają"
                }));
            try
            {
                var user = _service.GetUserById(userId);
                if (!user.Success)
                {
                    return await Task.FromResult(NotFound(user));
                }
                else
                {
                    if(user.Object.Password != oldPassword)
                    {
                        return await Task.FromResult(NotFound(new Response<string>
                        {
                            Success = false,
                            Message = "podane hasło jest nieprawidłowe"
                        }));
                    }
                    else
                    {
                        var result = _service.ChangeUserPassword(userId, newPassword);
                        if (result.Success)
                            return await Task.FromResult(Ok(result));
                        else
                            return await Task.FromResult(NotFound(result));
                        
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<Response<Guid>>> DeleteUser([FromBody]UserDTO user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _service.DeleteUser(user);
                    if (result.Success)
                        return await Task.FromResult(Ok(result));
                    else
                        return await Task.FromResult(NotFound(result));
                }
                catch
                {
                    throw;
                }
            }
            else
                return await Task.FromResult(BadRequest());
        }
    }
}
