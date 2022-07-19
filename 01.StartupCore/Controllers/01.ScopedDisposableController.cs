using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StartupCore.Services.ScopedDisposableServices;

namespace StartupCore.Controllers
{
    [Route("MyDipose/[action]")]
    public class ScopedDisposableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public int Get([FromServices] IOrderService service1,
            [FromServices] IOrderService service2,
            [FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery] bool stop = false)
        {
            #region sub scope
            Console.WriteLine("=======create subinstance==========");
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IOrderService>();
            }
            Console.WriteLine("=======dispose subinstance==========");
            #endregion
            if (stop)
            {
                hostApplicationLifetime.StopApplication();
            }
            Console.WriteLine("=======Action end==========");
            return new Random(Guid.NewGuid().GetHashCode()).Next(0, int.MaxValue);
        }
        public int Get2(
            [FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery] bool stop = false)
        {
            if (stop)
            {
                hostApplicationLifetime.StopApplication();
            }
            Console.WriteLine("=======Action2 end==========");
            return new Random(Guid.NewGuid().GetHashCode()).Next(0, int.MaxValue);
        }
    }
}
