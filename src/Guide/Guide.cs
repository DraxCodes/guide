using System.Threading.Tasks;
using Guide.Connection;
using Guide.Handlers;
using Guide.HelperRole;
using Guide.Services;

namespace Guide
{
    public class Guide
    {
        private readonly IConnection connection;
        private readonly ICommandHandler commandHandler;
        private readonly ServicesBootstrapper servicesBootstrapper;
        private readonly HelperRoleHandler helperRoleHandler;

        public Guide(IConnection connection, ICommandHandler commandHandler, ServicesBootstrapper servicesBootstrapper, HelperRoleHandler helperRoleHandler)
        {
            this.connection = connection;
            this.commandHandler = commandHandler;
            this.servicesBootstrapper = servicesBootstrapper;
            this.helperRoleHandler = helperRoleHandler;
        }

        public async Task Run()
        {
            await connection.Connect();
            await commandHandler.InitializeAsync();
            helperRoleHandler.InitializeAsync();
            await Task.Delay(-1);
        }
    }
}
