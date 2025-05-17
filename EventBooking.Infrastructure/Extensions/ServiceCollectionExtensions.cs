using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.IRepositories.IdentityRepositories;
using EventBooking.Infrastructure.Repositories;
using EventBooking.Infrastructure.Repositories.IdentityRepositories;
using EventBooking.Infrastructure.Services.Abstractions;
using EventBooking.Infrastructure.Services.implementation;
using EventBooking.Infrastructure.Settings;
using EventBooking.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EventBooking.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IEventBookingDbContext, EventBookingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BookingConnection")));

            #region Verification code validity settings
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5);
            });
            #endregion

            #region Identity Settings 
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<EventBookingDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region Configuration Settings
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));
            #endregion

            #region JwtAuthenticationSettings
            var jwtSection = configuration.GetSection($"{nameof(JWTSettings)}");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,

                    ValidIssuer = jwtSection.GetValue<string>($"{nameof(JWTSettings.Issuer)}"),
                    ValidateAudience = true,
                    ValidAudience = jwtSection.GetValue<string>($"{nameof(JWTSettings.Audience)}"),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSection.GetValue<string>($"{nameof(JWTSettings.Secret)}") ?? throw new InvalidOperationException("Secret key is missing")))
                };
            });
            #endregion

            #region Configure Swagger with JWT Authentication and able to read version correctly
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EventBooking API", Version = "v1" });

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type =  ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            #endregion



            #region DI Settings
            services.AddScoped<IEventBookingDbContext, EventBookingDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IJwtTokenRepository, JwtTokenRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<RoleManager<Role>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddHttpContextAccessor();
            #endregion

            return services;
        }
    }
}
