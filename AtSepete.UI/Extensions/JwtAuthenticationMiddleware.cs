//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//namespace AtSepete.UI.Extensions
//{
//    public class JwtAuthenticationMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public JwtAuthenticationMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
//        {
//            // Check if request is authenticated
//            if (!context.User.Identity.IsAuthenticated)
//            {
//                // Check if request contains JWT token
//                string token = context.Request.Headers["Authorization"];
//                if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
//                {
//                    token = token.Substring(7);

//                    var handler = new JwtSecurityTokenHandler();
//                    var validationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuerSigningKey = true,
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
//                        ValidateIssuer = false,
//                        ValidateAudience = false
//                    };
//                    try
//                    {
//                        var claimsPrincipal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
//                        context.User = claimsPrincipal;
//                    }
//                    catch (SecurityTokenException)
//                    {
//                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                        return;
//                    }
//                }
//                else
//                {
//                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                    return;
//                }
//            }

//            await _next(context);
//        }
//    }


//    public static class JwtAuthenticationMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<JwtAuthenticationMiddleware>();
//        }

//        public static void AddJwtAuthentication(this IServiceCollection services,IConfiguration configuration)
//        {
//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                    .AddJwtBearer(options =>
//                    {
//                        options.RequireHttpsMetadata = false;
//                        options.SaveToken = true;
//                        options.TokenValidationParameters = new ()
//                        {
//                            ValidateIssuerSigningKey = true,
//                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
//                            ValidateIssuer = false,
//                            ValidateAudience = false
//                        };
//                    });
//        }
//    }
//}
