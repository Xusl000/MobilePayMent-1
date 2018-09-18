﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PayCore;
using PayCore.Events;
using PayCore.Providers;

namespace PayMent.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient(c =>
            {
                IGateways gateways = new Gateways();
                gateways.Add(new AlipayGateway()
                {
                    Merchant = new Merchant()
                    {
                        AppId = Configuration["alipay:appid"],
                        Partner = Configuration["alipay:partner"],
                        Email = Configuration["alipay:seller_email"],
                        Key = Configuration["alipay:key"],
                        PublicKey = Configuration["alipay:publicKey"],
                        PrivateKey = Configuration["alipay:privateKey"],
                        NotifyUrl = new Uri(Configuration["alipay:notifyurl"]),
                        ReturnUrl = new Uri(Configuration["alipay:returnurl"]),
                    }
                });
                gateways.Add(new WeChatPayGataway()
                {
                    Merchant = new Merchant()
                    {
                        AppId = Configuration["wechatpayment:appid"],
                        Partner = Configuration["wechatpayment:mch_id"],
                        Key = Configuration["wechatpayment:key"],
                        NotifyUrl = new Uri(Configuration["wechatpayment:notifyurl"]),
                        ReturnUrl = new Uri(Configuration["wechatpayment:returnurl"]),
                    }
                });
                gateways.Add(new UnionPayGateway()
                {
                    Merchant = new Merchant()
                    {
                        Partner = Configuration["unionpay:merid"],
                        NotifyUrl = new Uri(Configuration["unionpay:notifyurl"]),
                        ReturnUrl = new Uri(Configuration["unionpay:returnurl"]),
                    }
                });
                return gateways;
            });

            services.AddTransient(c => new PaymentNotify(c.GetService<IGateways>().Merchants));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UsePayCore();
        }
    }
}
