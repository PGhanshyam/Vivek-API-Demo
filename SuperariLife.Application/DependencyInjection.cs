using Microsoft.Extensions.DependencyInjection;
using SuperariLife.Application.Auth;
using SuperariLife.Application.Coupon;
using SuperariLife.Application.CouponType;
using SuperariLife.Application.JWTServices;
using SuperariLife.Application.Role;
using SuperariLife.Application.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
        {
            services.AddScoped<
                IAuthService,
                AuthService
            >();

            services.AddScoped<
                IJwtService,
                JwtService
            >();

            services.AddScoped<
                IUserService,
                UserService
            >();

            services.AddScoped<
                IRoleService,
                RoleService
            >();

            services.AddScoped<
                ICouponService,
                CouponService
            >();

            services.AddScoped<
                ICouponTypeService,
                CouponTypeService
            >();

            return services;
        }
    }
}
