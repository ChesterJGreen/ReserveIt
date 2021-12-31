using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System;
using ReserveIt.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using ReserveIt.Managers;

namespace ReserveIt.Config
{
    public static class JwtTokenConfig
    {
        public static void ConfigureJwtAuthentication(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        public static void ConfigureJwtBearerTokens(JwtBearerOptions options,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            options.Events = new JwtBearerEvents
            {
                // after the token has been validated as being signed by our private key and the data extracted
                // .. the event callback below will run to allow us to perform additional validation of the token
                // .. such as making sure the user still exists (for a paid subscription service, perhaps we would
                // .. validate subscription status)
                OnTokenValidated = AuthenticateJsonToken
            };

            // TODO: only for development
            options.RequireHttpsMetadata = false;
            options.IncludeErrorDetails = true;

            options.SaveToken = true;

            //get our private key used to sign the tokens of our app settings
            var appSettings = configuration.Get<AppConfigModel>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.AuthTokenSettings.Secret);

            // configure our token validation to check that the token was signed by our key
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
        /// <summary>
        /// perform additional validation on a valid, signed token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task AuthenticateJsonToken(TokenValidatedContext context)
        {
            //get the user service from the DI container
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

            int userId;
            // if the identity is not an int (aka not hte user id) or the user does not exist anymore, fail the token validation
            if (!Int32.TryParse(context.Principal.Identity.Name, out userId) || userService.GetById(userId) == null)
                    {
                //return unauthorized if user no longer exists
                context.Fail("unauthorized");
            }
            return Task.CompletedTask;
            
        }
    }
}