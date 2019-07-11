using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AbpZeroLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = AbpBootstrapper.Create<MyStartupModule>();
            bootstrapper.Initialize();
            var iocManager = bootstrapper.IocManager;

            var flag = iocManager.IsRegistered<AbpEditionManager>();

            var tenantManager = iocManager.Resolve<TenantManager>();

            var tenant = tenantManager.FindByTenancyName<Tenant,User>("test");

            Console.WriteLine("Success");
            Console.ReadLine();
        }
    }

    public class MyDbContext : AbpZeroDbContext<Tenant, Role, User, MyDbContext>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class User : AbpUser<User>
    {

    }

    public class Role : AbpRole<User>
    {

    }

    public class Tenant : AbpTenant<User>
    {
        public Tenant()
            : base()
        {

        }
        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {

        }
    }

    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            AbpEditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore)
            : base(tenantRepository,
                  tenantFeatureRepository,
                  editionManager,
                  featureValueStore)
        {
        }
    }
}
