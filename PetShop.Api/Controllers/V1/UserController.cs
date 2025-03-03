﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.Application.DTO;
using PetShop.Application.Services.Interfaces;
using PetShop.Core;
using PetShop.Core.Audit;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IAuditHelper _audit;
        private readonly IAuditService _auditService;

        public UserController(IUsersService usersService, IAuditHelper audit, IAuditService auditService)
        {
            _usersService = usersService;
            _audit = audit;
            _auditService = auditService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(string RegitrationNumber, string password)
        {
            try
            {
                var response = await _usersService.Authenticate(RegitrationNumber, password);

                if(response.Data == null)
                {
                    await RegisterLog("PetShop", $"Login Fail - {response.Errors}", new { response.Success });
                    return UnprocessableEntity(response.Errors);
                }
                
                string[] res = response.Data.Split('|');

                await RegisterLog("PetShop", $"effected Login - {res[1]}", new { response.Success });
                return Ok(new { jwt_token = res[0] });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserDto users, string code)
        {
            try
            {
                var response = await _usersService.CreateUser(users, code);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Register User Fail - {users}", new { response.Success, response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Register Sucess - {users}", new { response.Success });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetAll( int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _usersService.GetAll(pageIndex, pageSize);

                if (!response.Success)
                {

                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get done -  ", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetById/{userId}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetById(int userId)
        {
            {
                try
                {
                    var response = await _usersService.GetById(userId);

                    if (!response.Success)
                    {
                        return UnprocessableEntity(response.Errors);
                    }

                    return Ok(response.Data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);


                }
            }
        }

        [HttpGet("GetByRegistrationNumber/{registrationNumber}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetByRegistrationNumber(string registrationNumber)
        {
            {
                try
                {
                    var response = await _usersService.GetByRegistrationNumber(registrationNumber);

                    if (!response.Success)
                    {
                        return UnprocessableEntity(response.Errors);
                    }

                    return Ok(response.Data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);


                }
            }
        }

        [HttpGet("GetByEmail/{email}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetById(string email)
        {
            {
                try
                {
                    var response = await _usersService.GetByEmail(email);

                    if (!response.Success)
                    {
                        return UnprocessableEntity(response.Errors);
                    }

                    return Ok(response.Data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);


                }
            }
        }

        [HttpGet("GetByPhoneNumber/{phonenumber}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetByPhoneNumber(string phonenumber, int pageIndex = 1, int pageSize = 10)
        {
            {
                try
                {
                    var response = await _usersService.GetByPhoneNumber(phonenumber, pageIndex, pageSize);

                    if (!response.Success)
                    {
                        return UnprocessableEntity(response.Errors);
                    }

                    return Ok(response.Data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);


                }
            }
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var users = await _usersService.DeleteUser(userId);
                if (!users)
                {
                    await RegisterLog("PetShop", $"Delete User Fail - {userId}");
                    return UnprocessableEntity(users);
                }

                await RegisterLog("PetShop", $"Delete User - {userId}");
                return Ok();
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Delete User  Fail - {userId}", new { ex.Message });
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{code}/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string code, int userId, UserDto userDto)
        {

            try
            {
                var response = await _usersService.UpdateUser(userId, userDto, code);
                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Update User Fail - {userDto}", new { response.Success, response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Updete User Sucess - {userDto}", new { response.Success });
                return Ok(response.Success);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Update User Fail - {userDto}", new { ex.Message });
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int userId, string password, string newPassword)
        {

            try
            {
                var response = await _usersService.UpdateUser(userId, password, newPassword);
                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Update User Password Fail - {userId}", new { response.Success, response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Updete User Password Sucess - {userId}", new { response.Success });
                return Ok(response.Success);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Update User Password Fail - {userId}", new { ex.Message });
                return BadRequest(ex.Message);
            }

        }

        protected AuditModel LogAudit(string module, string description, string model)
        {
            return _audit.RegisterLog(HttpContext, module, description, model);
        }
        private async Task RegisterLog(string module, string description, object objectModel = null)
        {
            try
            {
                var modelJson = string.Empty;

                //convert object in json
                if (objectModel != null) modelJson = JsonConvert.SerializeObject(objectModel);

                var log = LogAudit(module, description, modelJson);
                await _auditService.RegisterLog(log);
            }
            catch
            {
                throw;
            }
        }
    }
}
