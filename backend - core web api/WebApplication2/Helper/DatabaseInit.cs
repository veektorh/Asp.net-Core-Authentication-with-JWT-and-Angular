using Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApplication2.Helper
{
    public class DatabaseInit
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            try
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                    if (!context.Students.Any())
                    {
                        context.Students.Add(new Student() { Name = "victor", Position = "dev", Address = "lekki-epe" });
                        context.Students.Add(new Student() { Name = "wale", Position = "support", Address = "lekki-epe" });
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
