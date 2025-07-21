using Eunoia_UM_API.Helper;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;

public class JwtSlidingAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtSlidingAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            string jwtFromHeader = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userIdStr = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(jwtFromHeader) && int.TryParse(userIdStr, out int userId))
            {
                bool isValid = ValidateAndExtendJwt(userId, jwtFromHeader);

                if (!isValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token expired or invalid");
                    return;
                }
            }
        }

        await _next(context);
    }

    private bool ValidateAndExtendJwt(int userId, string jwt)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@iUserId", userId),
            new SqlParameter("@sJwtToken", jwt)
        };

        var dt = DBOperation.FillDataTable("USP_CheckAndExtendJWTToken", param);

        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "1")
        {
            return true; // valid and expiry extended
        }
        return false;
    }
}
