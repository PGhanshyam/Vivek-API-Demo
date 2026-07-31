using Microsoft.Extensions.DependencyInjection;
using SuperariLife.Infrastructure.DBRepository.Auth;
using SuperariLife.Infrastructure.DBRepository.Coupon;
using SuperariLife.Infrastructure.DBRepository.CouponType;
using SuperariLife.Infrastructure.DBRepository.Role;
using SuperariLife.Infrastructure.DBRepository.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
        {
            services.AddScoped<
           IAuthRepository,
           AuthRepository
       >();


            services.AddScoped<
                IUserRepository,
                UserRepository
            >();

            services.AddScoped<
                IRoleRepository,
                RoleRepository
            >();

            services.AddScoped<
                ICouponRepository,
                CouponRepository
            >();

            services.AddScoped<
                ICouponTypeRepository,
                CouponTypeRepository
            >();

            return services;
        }
    }
}
