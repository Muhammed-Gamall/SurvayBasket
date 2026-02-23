

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SurvayBasket2026
{
    public static class Dependancies
    {
        public static IServiceCollection AddDependancies(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddControllers();
           services.AddOpenApi();
            services.AddDbContext(configuration);
            services.AddValidation();
            services.AddAuthentication(configuration);
            services.AddCORS(configuration);

            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
           
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionstring));

            return services;
        }
        private static IServiceCollection AddCORS(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(allowedOrigins!)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return services;
        }
        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtProvider, JwtProvider>();

            // option pattern
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            var JwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>(); 

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(u =>
                {
                    u.SaveToken = true;
                    u.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtSettings!.key)),
                        ValidIssuer   = JwtSettings.issuer,
                        ValidAudience = JwtSettings.audience
                    };
                });
            return services;
        }

    }
    }
