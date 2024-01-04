using identityproduct_app.Config;
using identityproduct_app.Domain.Dto.Create;
using identityproduct_app.Domain.Dto.Read;
using identityproduct_app.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace identityproduct_app.Domain.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(IOptions<JwtOptions> jwtOptions, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserRegisterResponse> UserRegister(UserRegisterRequest userRegister)
        {
            var identityUser = new IdentityUser()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(identityUser, userRegister.Password);
            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }
            var response = new UserRegisterResponse(result.Succeeded);
            if(!result.Succeeded && result.Errors.Count() > 0)
            {
                response.AddErrors(result.Errors.Select(r => r.Description));
            }
            return response;
        }

        public async Task<UserLoginResponse> UserLogin(UserLoginRequest userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
            //terceiro argumento se persiste, quarto argumento se deve travar a conta caso erre.

            if (result.Succeeded)
            {
                return await GenerateToken(userLogin.Email);
            }

            var response = new UserLoginResponse(result.Succeeded);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    response.AddError("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    response.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    response.AddError("É necessário confirmar o login no seu segundo fator de autenticação.");
                else
                    response.AddError("Usuário ou senha estão incorretos");
            }
            return response;
        }

        private async Task<UserLoginResponse> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var tokenClaims = await GetClaims(user);
            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserLoginResponse
                (
                    success: true,
                    token: token,
                    expirationDate: expirationDate
                );

        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }
            return claims;
        }
    }
}
