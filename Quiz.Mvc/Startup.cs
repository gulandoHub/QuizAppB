using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using QuizMvc.Handlers.ImageHandler;
using QuizMvc.Helpers;
using QuizRepository;
using QuizService;
using QuizUtils.ImageWriter;


namespace QuizMvc
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = config;
        }

        private IConfiguration Configuration { get; }
        
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
            services.AddDbContext<QuizDBContext>(options => options.UseSqlServer(conString));
            services.AddScoped<IDbContext>(provider => provider.GetService<QuizDBContext>());

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
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");});
            #endregion
            
            #region authorization part
            
            #endregion
            
            #region generic repository and service
            
            //add generic repository and service
            services.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
            services.AddScoped(typeof(IService<>),typeof(Service<>));
            
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
            services.AddScoped<IImageService, ImageService>();
            
            services.AddSingleton<ILogService, LogService>();


            #endregion
            
            #region images
            
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IImageWriter, ImageWriter>();
            
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