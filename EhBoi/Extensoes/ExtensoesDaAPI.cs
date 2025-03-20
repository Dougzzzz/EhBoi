using EhBoi.Domain.Migrations;
using EhBoi.Infra.Data;

namespace EhBoi.Extensoes
{
    public static class ExtensoesDaAPI
    {
        public static async Task<WebApplication> MigrateDatabase(this WebApplication app, int retries = 5)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    logger.LogInformation("Tentando aplicar migrations. Tentativa {attempt} de {total}", i + 1, retries);
                    var migrationService = services.GetRequiredService<ServicoDeMigrations<EhBoiDbContext>>();
                    await migrationService.MigrateAsync();
                    logger.LogInformation("Migrations aplicadas com sucesso");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erro ao aplicar migrations. Tentativa {attempt} de {total}", i + 1, retries);
                    if (i == retries - 1) throw;
                    await Task.Delay(2000); // Aguarda 2 segundos antes de tentar novamente
                }
            }

            return app;
        }
    }
}
