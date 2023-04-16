using AppoinmentScheduler.Data;
using Microsoft.EntityFrameworkCore;

namespace AppoinmentScheduler.Middleware
{
    public class Authentication
    {
        private readonly RequestDelegate _next;
        
        private readonly ApplicationDBContext _dbContext;

        public Authentication(RequestDelegate next, ApplicationDBContext context)
        {
            this._next = next;
            this._dbContext = context;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    Results.Unauthorized();
                    return;
                }

                var result = _dbContext.RootUsers.FromSqlRaw($"Select * from dbo.RootUsers Where Token='{token}'").ToList();
                Console.WriteLine(result);
                context.Items["User"] = result[0];
                await _next(context);
            }catch(Exception ex)
            {
                Results.BadRequest(ex);
                return;
            }
           
        }
    }
}
