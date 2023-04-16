using AppoinmentScheduler.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppoinmentScheduler.Middleware
{
    public class Authentication
    {
        private readonly RequestDelegate _next;
       

        public Authentication(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDBContext _dbContext)
        {
            try
            {
                var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
                Console.WriteLine(token);
                if (token == null)
                {
                    Console.WriteLine("haii");
                    context.Items["User"] = null;
                    await _next(context);
                }
                var result = _dbContext.RootUsers.FromSqlRaw($"Select * from dbo.RootUsers Where Token='{token}'").ToList();
                Console.WriteLine(result);
                context.Items["User"] = result[0].Id;
                await _next(context);
            }catch(Exception ex)
            {
               Console.WriteLine(ex.ToString());
                
            }
           
        }
    }
}
