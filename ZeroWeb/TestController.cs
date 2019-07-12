using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization.Roles;
using Abp.Dependency;
using Microsoft.AspNetCore.Mvc;

namespace ZeroWeb
{
    public class TestController : AbpController
    {
        private readonly RoleManager _roleManager;

        public TestController(IIocManager iocManager, RoleManager roleManager)
        {
            var roleStore = iocManager.Resolve<RoleStore>();
        }

        public IActionResult Index()
        {
            return Ok("test");
        }
    }
}
