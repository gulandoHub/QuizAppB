using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizRepository;
using QuizService;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using QuizApi.Auth;
using QuizApi.Helpers;
using QuizApi.Models;
using QuizData;


namespace QuizApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration config)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = config;
        }
            
        public void ConfigureServices(IServiceCollection services)
        {
            #region mvc
            
            services.AddMvc();
            
            #endregion
            
            #region cash
            
            services.AddMemoryCache();
            
            #endregion

            #region database

            var conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddScoped<IDbContext>(provider => provider.GetService<QuizDBContext>())
                .AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));

            var identityConString = Configuration["ConnectionStrings:IdentityConnection"];
            services.AddScoped<IIdentityDBContext>(provider => provider.GetService<QuizIdentityDBContext>())
                .AddDbContext<QuizIdentityDBContext>(options =>
                    options.UseSqlServer(identityConString, b => b.MigrationsAssembly("Quiz.Api")));

            //services.AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));
            //.AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));

            #endregion

            #region mapping

            services.AddAutoMapper();

            //manual add mapping
            /* var mappingConfig = new MapperConfiguration(mc =>
             {
                 mc.AddProfile(new MappingProfile());
             });

             var mapper = mappingConfig.CreateMapper();
             services.AddSingleton(mapper);*/

            #endregion

            #region authentication part

            services.AddSingleton<IJwtFactory, JwtFactory>();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var signingKey = new SymmetricSecurityKey(key);

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = System.TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.ApiAccess));
            });

            // add identity
            services
                .AddIdentity<AppUser, IdentityRole>(o =>
                    {
                        // configure identity options
                        o.Password.RequireDigit = false;
                        o.Password.RequireLowercase = false;
                        o.Password.RequireUppercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequiredLength = 6;
                    })
                .AddEntityFrameworkStores<QuizIdentityDBContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region generic repository and service

            //add generic repository and service
            services.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
            services.AddScoped(typeof(IElasticSearchRepository<>), typeof(ElasticSearchRepository<>));
            
            services.AddScoped(typeof(IService<>),typeof(Service<>));
            services.AddScoped(typeof(ISearchService<>),typeof(SearchService<>));
            
            #endregion
            
            #region services
            
            //add services
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IAnswerTypeService, AnswerTypeService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionTypeService, QuestionTypeService>();
            services.AddScoped<IQuizService, QuizService.QuizService>();
            services.AddScoped<IQuizThemeService, QuizThemeService>();
            services.AddScoped<IExamTypeService, ExamTypeService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRightService, RightService>();
            services.AddScoped<IRoleService, RoleService>();
            
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRightService, UserRightService>();
            services.AddScoped<IRoleRightService, RoleRightService>();
            
            services.AddSingleton<ILogService, LogService>();


            #endregion

            #region elasticsearch

            services.AddElasticSearch(Configuration);
            
            #endregion
            
        }

        public void Configure(IApplicationBuilder app, ILogService logger, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Standard Exception Handling
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //Custom Exception Handling.
                app.ConfigureExceptionHandler(logger);
            }
            
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
        
    }
}