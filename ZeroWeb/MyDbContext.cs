using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ZeroWeb
{
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

        public DbSet<VmInfo> VmInfos { get; set; }
    }

    public class VmInfo : Entity
    {
        public string Name { get; set; }
    }

    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseMySql("server=127.0.0.1;database=lpw-test;uid=root;pwd=123456");
            return new MyDbContext(optionsBuilder.Options);
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

    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(
            AbpRoleManager<Role, User> roleManager,
            AbpUserStore<Role, User> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ISettingManager settingManager)
            : base(roleManager, userStore, optionsAccessor, passwordHasher, userValidators,
                passwordValidators, keyNormalizer, errors, services, logger, permissionManager,
                unitOfWorkManager, cacheManager, organizationUnitRepository, userOrganizationUnitRepository,
                organizationUnitSettings, settingManager)
        {
        }
    }

    public class RoleManager : AbpRoleManager<Role, User>
    {
        public RoleManager(
            AbpRoleStore<Role, User> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<AbpRoleManager<Role, User>> logger,
            IPermissionManager permissionManager,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRoleManagementConfig roleManagementConfig,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(store, roleValidators, keyNormalizer, errors, logger, permissionManager, cacheManager,
                unitOfWorkManager, roleManagementConfig, organizationUnitRepository, organizationUnitRoleRepository)
        {
        }
    }

    public class RoleStore : AbpRoleStore<Role, User>
    {
        public RoleStore(IUnitOfWorkManager unitOfWorkManager,
            IRepository<Role> roleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(unitOfWorkManager, roleRepository, rolePermissionSettingRepository)
        {
        }
    }
}
