
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


//===============================================================
    #region Manuel
    //---------------------------------------------------------------
    // Using JwtBearer to create Token 
    // Claim by ipAddress
    //
    // *********************************
    // Last Updated : 20240117 by Jakkit
    // *********************************
    #endregion
    //===============================================================
namespace JackMenuMaster.Services
{
    // Class for appsettings.Services.json
    public class JwtBearerSetting
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? LifeTimeSeconds { get; set; }
    }


    public class JwtBearer
    {
        public string Issuer = "";
        public string Audience = "";
        public double LifeTimeSeconds = 0;
        public string SecretKey = ""; // Auto-Gen on initial
        public JwtBearer()
        {
            var jwtBearerSetting = new AppSettingReader().LoadClass<JwtBearerSetting>("appsettings.services.json", "JwtBearerSetting");

            Issuer = jwtBearerSetting.Issuer ?? "";
            Audience = jwtBearerSetting.Audience ?? "";
            LifeTimeSeconds = double.Parse(jwtBearerSetting.LifeTimeSeconds ?? "0");
            SecretKey = GenerateSecretKey(256);
        }
        private static string GenerateSecretKey(int lengthInBits)
        {
            try
            {

                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: GenerateSecretKey Request: {lengthInBits}");
                //-------------------------------------------------------------------------------

                if (lengthInBits % 8 != 0)
                {
                    throw new ArgumentException("Key length must be a multiple of 8 bits.");
                }

                int byteLength = lengthInBits / 8;
                byte[] keyBytes = new byte[byteLength];
                new Random().NextBytes(keyBytes);
                var resp = Convert.ToBase64String(keyBytes);

                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: GenerateSecretKey Respond: {resp}");
                //-------------------------------------------------------------------------------
                return resp;
            }catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: GenerateSecretKey Respond fail: {ex.ToString()}");
                //-------------------------------------------------------------------------------
                return "";
            }
        }

        public string GenerateToken(string ipAddress) // 3600 Sec = 1 hour
        {
            try
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: Request Token : {ipAddress}");
                //-------------------------------------------------------------------------------

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim("ipAddress", ipAddress)
                };

                var token = new JwtSecurityToken(
                    issuer: Issuer,
                    audience: Audience,
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(LifeTimeSeconds),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: Request Token Successful : {token}");
                //-------------------------------------------------------------------------------
                return (tokenString);
            }
            catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: Request Token Fail : {ex.ToString()}");
                //-------------------------------------------------------------------------------
                return ("");
            }
        }

        public bool IsAuthorize(string token, string ipAddress)
        {
            try
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: IsAuthorize Request");
                
                var Clime = ReadToken(token);
                if ( Clime == ipAddress)
                {
                    // Log --------------------------------------------------------------------------
                    GlobalData.Log.Write($"JwtBearer: IsAuthorize Respond: User authorized");
                    //-------------------------------------------------------------------------------
                    return true;

                }
                else
                {
                    // Log --------------------------------------------------------------------------
                    GlobalData.Log.Write($"JwtBearer: IsAuthorize Respond: User is not authorized, wrong Ip Address");
                    //-------------------------------------------------------------------------------
                    return false;
                }

            }
            catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: IsAuthorize Respond fail: {ex.ToString()}");
                //-------------------------------------------------------------------------------
                return false;
            }
        }


        public string  ReadToken(string jwtToken)
        {
            try
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: ReadToken {jwtToken}");

                string secretKey = SecretKey;
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                // Token validation parameters
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience
                };

                // Validate and decode the token
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);

                // Extract claims from the principal
                var claims = claimsPrincipal.Claims;

                // Filter the claims to find the one with type "ipAddress"
                var claims_Filter = claims.FirstOrDefault(c => c.Type == "ipAddress");

                if (claims_Filter != null)
                {
                    // Extract the desired values
                    var type = claims_Filter.Type;
                    var value = claims_Filter.Value;
                    // Log --------------------------------------------------------------------------
                    GlobalData.Log.Write($"JwtBearer: Value type={type}, value={value}");

                    // Return the extracted values
                    return value;
                }
                else
                {
                    // Log --------------------------------------------------------------------------
                    GlobalData.Log.Write($"JwtBearer: claim not found");
                    return "";
                }
            }
            catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"JwtBearer: Token validation failed, Error = {ex.Message}");
                return "";

            }
        }

    }
}



//===============================================================
#region Add-On
//---------------------------------------------------------------
// Add ConditionalAuthorizeAttribute to Main project inorder to use 
//        [ConditionalAuthorize(true)] 
// to Authorize in API
//
// ConditionalAuthorizeAttribute.cs
// *********************************
// Last Updated : 20240117 by Jakkit
// *********************************
#endregion
//===============================================================

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TrainingCenter.API
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
//    public class ConditionalAuthorizeAttribute : Attribute, IAuthorizationFilter
//    {
//        private readonly bool isEnabled;

//        public ConditionalAuthorizeAttribute(bool isEnabled)
//        {
//            this.isEnabled = isEnabled;
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            if (isEnabled)
//            {
//                var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

//                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//                // Get the IP address from the incoming request, ensuring RemoteIpAddress is not null
//                var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();


//                //var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, context, requirement);

//                if (!GlobalData.JwtToken.IsAuthorize(token ?? "", ipAddress ?? ""))
//                {
//                    context.Result = new ForbidResult();
//                }
//            }
//        }
//    }
//}
