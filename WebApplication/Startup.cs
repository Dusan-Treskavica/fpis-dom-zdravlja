using BusinessLogic.Services;
using Common.Util;
using DataAccess.DB;
using DataAccess.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApplication.Profiles;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccess.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Common.Models;
using Common;
using System.Threading.Tasks;
using BusinessLogic.Services.Auth;

namespace WebApplication
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Get JWT Token Settings from JwtSettings.json file
            JwtSettings jwtSettings;
            jwtSettings = this.GetJwtSettings();
            // Create singleton of JwtSettings
            services.AddSingleton<JwtSettings>(jwtSettings);

            services.AddDbContext<FPISDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<IDBBroker, DBBroker>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FPISDBContext>()
                .AddDefaultTokenProviders();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextUtil, HttpContextUtil>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IKarticaZaEvidencijuService, KarticaZaEvidencijuService>();
            services.AddTransient<IAnalizaService, AnalizaService>();

            var mapperConfig = new AutoMapper.MapperConfiguration(conf => {
                conf.AddProfile(new AutoMapperProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                //options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = false;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200", "http://localhost:51767")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials(); 
                                  });
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(jwtSettings.JwtTokenExpiresIn))
                };
            });

            services.AddControllers()
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            this.PrepareDatabase(app, env);
        }

        private void PrepareDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
                var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
                
                this.AddDefaultUsersAsync(userManager, roleManager).Wait();
            }

        }

        private async Task<IdentityResult> AddDefaultUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser adminUser = new ApplicationUser
            {
                Email = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "dusanAdmin"
            };
            ApplicationUser readOnlyUser = new ApplicationUser
            {
                Email = "readonly@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "dusanRO"
            };
            if (await userManager.FindByNameAsync("dusanAdmin") == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Password.123");
            }

            if (await userManager.FindByNameAsync("dusanRO") == null)
            {
                var result = await userManager.CreateAsync(readOnlyUser, "Password.123");
            }

            if(!await roleManager.RoleExistsAsync(UserRoleConsts.ADMIN))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoleConsts.ADMIN));
            }
            if(!await roleManager.RoleExistsAsync(UserRoleConsts.READONLY))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoleConsts.READONLY));
            }

            var admin = await userManager.FindByNameAsync("dusanAdmin");
            if (await roleManager.RoleExistsAsync(UserRoleConsts.ADMIN) && admin != null)
            {
                var result = await userManager.AddToRoleAsync(admin, UserRoleConsts.ADMIN);
            }
            var readOnly = await userManager.FindByNameAsync("dusanRO");
            if (await roleManager.RoleExistsAsync(UserRoleConsts.READONLY) && readOnly != null)
            {
                var result = await userManager.AddToRoleAsync(readOnly, UserRoleConsts.READONLY);
            }
            return IdentityResult.Success;
        }

        public JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings();

            settings.Key = Configuration["JwtSettings:key"];
            settings.Audience = Configuration["JwtSettings:audience"];
            settings.Issuer = Configuration["JwtSettings:issuer"];
            settings.JwtTokenExpiresIn =
             Convert.ToInt32(
                Configuration["JwtSettings:jwtTokenExpiresIn"]);
            settings.RefreshTokenExpiresIn =
             Convert.ToInt32(
                Configuration["JwtSettings:refreshTokenExpiresIn"]);

            return settings;
        }
    }
}
