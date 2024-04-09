using AutoMapper;
using DAL;
using BLL;
using ENTITIES;
using FluentValidation.AspNetCore;
using FluentValidation;
using ENTITIES.Context;
using ENTITIES.Entities;

namespace WebApi_IncidentsManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Fluent validation configuracion
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddFluentValidationClientsideAdapters();

            // DI for Fluent Validations
            builder.Services.AddScoped<IValidator<Process>, ProcessValidator>();
            builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();


            // Container to entities -Depedency injection
            builder.Services.AddScoped<IMSContext, IncidentsManagementContext>();

            //Depedency injection for GroupProcess
            builder.Services.AddScoped<IGroupProcessService, GroupProcessService>();
            builder.Services.AddScoped<IGroupProcessRepository, GroupProcessRepository>();

            //Depedency injection for Process
            builder.Services.AddScoped<IProcessService, ProcessService>();
            builder.Services.AddScoped<IProcessRepository, ProcessRepository>();

            //Depedency injection for Process
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            //Depedency injection for RootCause
            builder.Services.AddScoped<IRootCauseService, RootCauseService>();
            builder.Services.AddScoped<IRootCauseRepository, RootCauseRepository>();

            //Depedency injection for Standard
            builder.Services.AddScoped<IStandardService, StandardService>();
            builder.Services.AddScoped<IStandardRepository, StandardRepository>();

            //Depedency injection for Incidents
            builder.Services.AddScoped<IIncidentService, IncidentService>();
            builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

            //Depedency injection for IncidentClassifications
            builder.Services.AddScoped<IIncidentClassificationsService, IncidentClassificationService>();
            builder.Services.AddScoped<IIncidentClassificationsRepository, IncidentClassificationRepository>();

            //Depedency injection for IncidentResolution
            builder.Services.AddScoped<IIncidentResolutionService, IncidentResolutionService>();
            builder.Services.AddScoped<IIncidentResolutionRepository, IncidentResolutionRepository>();

            //Mapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MyMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
