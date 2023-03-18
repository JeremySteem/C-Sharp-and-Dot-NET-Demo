#region Additional Namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarTEDSystem.BLL;
using StarTEDSystem.DAL;
#endregion

namespace StarTEDSystem
{
    public static class BackendExtensions
    {
        public static void BackendDependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<StarTEDContext>(options);

            services.AddTransient<ProgramCourseServices>((ServiceProvider) =>
            {
                //  get the dbcontext class that has been registered
                var context = ServiceProvider.GetService<StarTEDContext>();
                //  create an instance of the service class (BuidVersionServices) supplying
                //      the context reference to the service class
                //  return the service class instance
                return new ProgramCourseServices(context);
            }
             );
            services.AddTransient<ProgramServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<StarTEDContext>();
                return new ProgramServices(context);
            }
             );
            services.AddTransient<SchoolServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<StarTEDContext>();
                return new SchoolServices(context);
            }
            );
        }
    }
}
