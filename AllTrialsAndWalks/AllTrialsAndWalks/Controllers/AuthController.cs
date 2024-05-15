using AllTrialsAndWalks.Models.DTO;
using AllTrialsAndWalks.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllTrialsAndWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //https://localhost:portnumber/api/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AddRegisterDto addRegisterDto)
        {
            var user = new IdentityUser
            {
                Email = addRegisterDto.Username,
                UserName = addRegisterDto.Username
            };

            var identityResult = await userManager.CreateAsync(user, addRegisterDto.Password);
            if (identityResult.Succeeded)
            {
                if(addRegisterDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(user, addRegisterDto.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("User registered successfully. Please login!");
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AddLoginDto addLogin)
        {
            var user = await userManager.FindByEmailAsync(addLogin.Username);

            if(user != null) 
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, addLogin.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var response = tokenRepository.CreateJWTToken(user, roles.ToList());
                        return Ok(response);
                    }
                }
            }
            return BadRequest("UserName or password incorrect");
        }


    }
}
