using Mailer.BLL;
using Mailer.Contracts;
using Mailer.DAL;
using Mailer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Mailer.API;

public class Startup
{
    private string _connectionString;
    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        _connectionString = configuration.GetConnectionString("Server");
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<Context>(x => x.UseSqlServer(_connectionString));

        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        var options = optionsBuilder.UseSqlServer(_connectionString).Options;

        using (Context context = new Context(options))
        {
            context.Database.Migrate();
            if (!context.Users.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    User person = new()
                    {
                        Name = Faker.Name.FullName(),
                        Mail = Faker.Internet.Email()
                    };
                    context.Users.Add(person);
                }
                context.SaveChanges();
            }
        }

        services.AddControllers();

        services.AddScoped<ILetterManager, LetterManager>();
        services.AddScoped<ILetterRepository, LetterRepository>();
        services.AddScoped<IUserRepository, UserRepository>();


        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        services.AddMvc();

        services.AddOptions();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });


        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}