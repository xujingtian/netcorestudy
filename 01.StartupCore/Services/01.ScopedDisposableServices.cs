using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupCore.Services
{
    public class ScopedDisposableServices
    {
        public interface IOrderService
        {

        }

        public class DisposableOrderService : IOrderService, IDisposable
        {
            public void Dispose()
            {
                Console.WriteLine($"DisposableOrderService Disposed:{this.GetHashCode()}");
            }
        }
    }
}
