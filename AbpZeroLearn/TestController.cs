using Microsoft.AspNetCore.Mvc;

namespace AbpZeroLearn
{
    public class TestController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("text");
        }
    }
}
