using Application.Actions.UserActions;
using Application.Config;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _respository;
        private readonly IMapper _mapper;
        private readonly IHasingService _hashing;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthController(IUserRepository repository, IMapper mapper, 
            IHasingService hashing, IJwtAuthManager jwtAuthManager)
        {
            _respository = repository;
            _mapper = mapper;
            _hashing = hashing;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(Authentication auth)
        {
            var user = _respository.GetByEmailOrUsername(auth.Username);

            if (user == null)
            {
                return Unauthorized(new { 
                    message = "Username or Password is wrong"
                });
            }

            if(!_hashing.ValidatePassword(auth.Password, user.Password))
            {
                return Unauthorized(new
                {
                    message = "Username or Password is wrong"
                });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var token = _jwtAuthManager.GenerateToken(user.Username, claims, DateTime.Now);
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;

            Response.Cookies.Append("token-access", token, cookieOptions);

            return Ok(new
            {
                message = "OKA"
            });
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public IActionResult Me()
        {
            var user = _respository.GetByEmailOrUsername(User.Identity.Name);
            
            return Ok(user);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Register(CreateUser user)
        {
            var userTest = _respository.GetByEmailOrUsername(user.Username);

            if(userTest != null)
            {
                return Conflict(new
                {
                    field = "username",
                    message = "This values is already taken"
                }) ;
            }

            userTest = _respository.GetByEmailOrUsername(user.Email);

            if (userTest != null)
            {
                return Conflict(new
                {
                    field = "email",
                    message = "This values is already taken"
                });
            }

            var userModel = _mapper.Map<User>(user);

            userModel.CreatedAt = DateTime.Now;
            userModel.Password = _hashing.HashPassword(userModel.Password);

            _respository.Add(userModel);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userModel.Username)
            };

            var token = _jwtAuthManager.GenerateToken(userModel.Username, claims, DateTime.Now);
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;

            Response.Cookies.Append("token-access", token, cookieOptions);

            return Ok(new
            {
                message = "Created"
            });
        }
    }
}
