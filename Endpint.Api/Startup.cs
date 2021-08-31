using Application.Interfaces.Context;
using Application.Services.CartItems.Commands.AddCartItem;
using Application.Services.CartItems.Commands.RemoveCartItem;
using Application.Services.Carts.Commands.AddCart;
using Application.Services.Carts.Commands.RemoveCart;
using Application.Services.Carts.Queries;
using Application.Services.Carts.Queries.GetCartAndTotalPrice;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.Context;

namespace Endpint.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DbShoppingCart")));

            services.AddTransient<IDataBaseContext, DataBaseContext>();

            ///----------------Services
            services.AddScoped<IAddCartService, AddCartService>();
            services.AddScoped<IAddCartItemService, AddCartItemService>();
            services.AddScoped<IRemoveCartService, RemoveCartService>();
            services.AddScoped<IRemoveCartItemService, RemoveCartItemService>();
            services.AddScoped<IGetCartAndTotalPriceService, GetCartAndTotalPriceService>();
            services.AddScoped<IGetAllCarts, GetAllCarts>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Endpint.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endpint.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
