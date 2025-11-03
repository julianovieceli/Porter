using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Porter.Infra.Postgres.Repository;


namespace Porter.Tests;

public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program>
    where Program : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

            //var dbContextDescriptor = services.SingleOrDefault(
            //d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            //// 2. Remove the production DbContext registration
            //if (dbContextDescriptor != null)
            //{
            //    services.Remove(dbContextDescriptor);
            //}

            //var dbContextService = services.SingleOrDefault(
            //    d => d.ServiceType == typeof(AppDbContext));

            //if (dbContextService != null)
            //{
            //    services.Remove(dbContextService);
            //}

            //var cacheKeyDescriptor = services.SingleOrDefault(
            //d => d.ServiceType == typeof(Microsoft.EntityFrameworkCore.Infrastructure.IModelCacheKeyFactory));

            //if (cacheKeyDescriptor != null)
            //{
            //    services.Remove(cacheKeyDescriptor);
            //}



            //services.AddDbContext<AppDbContext>(options =>
            //    {
            //        options.UseInMemoryDatabase("PorterDbTest");
            //    });


            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                // apagando os dados
                db.Database.EnsureCreated();

                db.Clients.ExecuteDeleteAsync();
                db.Rooms.ExecuteDeleteAsync();
                db.Bookings.ExecuteDeleteAsync();


                db.SaveChanges();
                // Exemplo de populamento de dados
                //db.Clients.Add(new Product { Id = 1, Name = "Produto Teste", Price = 10.0m });
                // db.SaveChanges();
            }
        });
    }
}