using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EhBoi.Domain.Migrations
{
    public class ServicoDeMigrations<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly ILogger<ServicoDeMigrations<TContext>> _logger;

        public ServicoDeMigrations(
            TContext context, ILogger<ServicoDeMigrations<TContext>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Aplicando migrações para {DbContext}", typeof(TContext).Name);
            await _context.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Migrações aplicadas com sucesso para {DbContext}", typeof(TContext).Name);
        }
    }
}
