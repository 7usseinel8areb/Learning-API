namespace ITI_API_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        //Create Account           "Registration" => Post
        //Check account validation "Login" => Post
        [HttpPost("register")]//api/account/register
        public async Task<IActionResult> Registeration(RegisterUserDTO userDTO)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                };
                var result = await userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account created succesfully");
                }
                return BadRequest(result.Errors.FirstOrDefault().ToString());
            }
            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userDTO.UserName);
                if(user is not null)
                {
                    var result = await userManager.CheckPasswordAsync(user, userDTO.Password);
                    if (result)
                    {
                        //Claims of Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        //get role
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));


                        SigningCredentials signincred =
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        //Create token => JwtSecurityToken
                        //This class represent token not to create it
                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"],//Provider API
                            audience: configuration["JWT:ValidAudience"], //Consumer Angular, HTML, MVC .....
                            claims:claims,
                            expires:DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Token), // Create the token 
                            expiration = Token.ValidTo //Valid to an hour for example
                        });
                    }
                }
            }
            return Unauthorized();
        }
    }
}
