using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizApi.Auth;
using QuizApi.Helpers;
using QuizApi.Models;
using QuizData;


namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        #region properties

        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;

        #endregion

        #region ctor

        public AccountController(IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        #endregion

        #region api methods

        [AllowAnonymous]
        [HttpPost]
        [ActionName("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody]UserData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            return new OkObjectResult("Account created");
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Post([FromBody]UserLoginData credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identity = await GetClaimsIdentity(credentials.Username, credentials.Password);

            if (identity == null)
                return BadRequest("Invalid username or password.");

            var jwt = await GenerateJwt(identity, _jwtFactory, credentials.Username, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        [HttpPost]
        [ActionName("SignOut")]
        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            
            return new OkObjectResult("Sign Out successfully");
        }

        #endregion

        #region methods

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
                return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        #endregion

    }
}
