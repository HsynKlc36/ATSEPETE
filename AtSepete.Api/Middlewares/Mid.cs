namespace AtSepete.Api.Middlewares
{
    public class Mid
    {
        private readonly RequestDelegate _nextMiddleWare;
        public Mid(RequestDelegate next)
        {
            _nextMiddleWare = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            try
            {
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    var token = Guid.Parse(authHeader.Substring(6).Trim());
                }
                else if (context.Request.Path.Value.Contains("/api/Auth"))
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            await _nextMiddleWare(context);
        }
    }
}
