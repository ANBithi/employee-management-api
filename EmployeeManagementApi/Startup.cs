using EmployeeManagementApi.Repositories;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagementApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDbContext>(x => new DbContext("mongodb://localhost:27017","EmployeeManagement"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IEmployeeAddressRepository, EmployeeAddressRepository>();
            services.AddScoped<IEmployeeInfoRepository, EmployeeInfoRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<ILeaveCountRepository, LeaveCountRepository>();
            services.AddScoped<IWorkBookRepository, WorkBookRepository>();
            services.AddScoped<IFinanceRepository, FinanceRepository>();
            services.AddScoped<IAcademicsRepository, EmployeeAcademicsRepository>();
            services.AddScoped<IEmpProfQualificationRepository, EmpProfQualificationRepository>();
            services.AddScoped<IEmployeeExperienceRepository, EmployeeExperienceRepository>();
            services.AddScoped<IResignRepository, ResignRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }

        // This method gets called by the runtime. method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()

            );
            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
