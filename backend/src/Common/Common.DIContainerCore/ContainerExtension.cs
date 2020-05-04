using Common.Services;
using Common.Services.Infrastructure;
using Common.DataAccess.EFCore;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Common.Services.Infrastructure.Repositories;
using Common.DataAccess.EFCore.Repositories;

namespace Common.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();

            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IUserPhotoRepository, UserPhotoRepository>();
            services.AddTransient<IUserService, UserService<User>>();
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IIdentityUserRepository<User>, IdentityUserRepository>();
            services.AddTransient<IRoleRepository<Role>, RoleRepository>();
            services.AddTransient<IUserRoleRepository<UserRole>, UserRoleRepository>();
            services.AddTransient<IUserClaimRepository<UserClaim>, UserClaimRepository>();

            #region Repository

            services.AddTransient<IHotspotResultRepository, HotspotResultRepository>();

            #endregion

            #region Service

            services.AddTransient<IHotspotResultService, HotspotResultService>();

            #endregion
        }
    }
}
