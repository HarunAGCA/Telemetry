using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Futek.Telemetry.Business.Abstract;
using Futek.Telemetry.Business.Concrete;
using Futek.Telemetry.DataAccess.Abstract;
using Futek.Telemetry.DataAccess.Concrete.EntityFramework;
using Futek.Telemetry.Hardware.Abstract;
using Futek.Telemetry.Hardware.Concrete.SerialPortReceiver;
using Futek.Telemetry.MvcWebUI.Dtos;
using Futek.Telemetry.MvcWebUI.SignalRHubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Futek.Telemetry.MvcWebUI
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
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSingleton<SerialDataReceiverBase, SerialPortDataReceiver>();
            services.AddSingleton<SensorValuesDto, SensorValuesDto>();
            services.AddSingleton<ISensorValueService, SensorValueManager>();
            services.AddSingleton<ISensorValueDal, EfSensorValueDal>();



            /*
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*
            app.UseHttpsRedirection();

            app.UseSession();
            app.UseAuthorization();
            */

            app.UseStaticFiles();
            app.UseRouting();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                endpoints.MapHub<SensorValuesHub>("/sensorValuesHub");
            });

        }

       
    }
}
