using E_Grocery_Store.Common.CustomException;
using E_Grocery_Store.Models;
using E_Grocery_Store.Repository.AccountManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Grocery_Store.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo accountManagementRepo;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAccountRepo accountManagementRepo, ILogger<AccountController> logger)
        {
            this.accountManagementRepo = accountManagementRepo;
            this.logger = logger;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<Response>> SignUp(User user)
        {
            Response response;
            try
            {
                await accountManagementRepo.SignUp(user);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Sign up success"
                };
                return Ok(response);
            }
            catch (DuplicateMailIdException dex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = dex.Message
                };
                logger.LogError(dex.Message);
                return BadRequest(response);
            }
            catch (RequestException rex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = rex.Message
                };
                logger.LogError(rex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = "Sign up failed"
                };
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult<Response>> SignIn(SignInRequest credentials)
        {
            Response response;
            try
            {
                var user = await accountManagementRepo.SignIn(credentials);
                var claims = new List<Claim> {
                                                new Claim(type: ClaimTypes.Email, value: user.Email),
                                                new Claim(type: ClaimTypes.Name, value: user.Name),
                                                new Claim(type: ClaimTypes.Role, value: user.Role.Name)
                                              };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(50),
                    });
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Sign in success"
                };
                return Ok(response);

            }//Custom exception for empty requests
            catch (RequestException rex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = rex.Message
                };
                logger.LogError(rex.Message);
                return BadRequest(response);
            }//Custom exception for invalid credentials
            catch (InvalidCredentialException icx)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = icx.Message
                };
                logger.LogError(icx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("user")]
        public ActionResult GetUser()
        {
            try
            {
                var userClaims = User.Claims.Select(x => new UserClaim() { Type = x.Type, Value = x.Value }).ToList();
                return Ok(userClaims);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }

        }

        [HttpGet("signout")]
        public async Task SignOut()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
