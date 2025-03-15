using EhBoi.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EhBoi.Test
{
    public class TesteBase : IDisposable
    {
        protected ServiceProvider _serviceProvider;
        public TesteBase()
        {
            var services = new ServiceCollection();
            services.AddDbContext<EhBoiDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }

    }

}
