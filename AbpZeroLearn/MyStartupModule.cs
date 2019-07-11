using Abp.Modules;
using Abp.Zero.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Features;

namespace AbpZeroLearn
{
    [DependsOn(typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class MyStartupModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "server=127.0.0.1;database=lpw-test;uid=root;pwd=123456";
            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.Register<IFeatureValueStore, AbpFeatureValueStore<Tenant, User>>();
            IocManager.RegisterAssemblyByConvention(typeof(MyStartupModule).Assembly);
            Configuration.Modules.AbpEfCore().AddDbContext<MyDbContext>(options =>
            {
                if (!string.IsNullOrEmpty(options.ConnectionString))
                {
                    options.DbContextOptions.UseMySql(options.ConnectionString);
                }

                if (options.ExistingConnection != null)
                {
                    options.DbContextOptions.UseMySql(options.ExistingConnection);
                }
            });
            base.Initialize();
        }
    }
}
