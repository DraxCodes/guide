using Discord.WebSocket;
using System.Threading.Tasks;

namespace Guide.HelperRole
{
    public class HelperRoleHandler
    {
        private DiscordSocketClient _client;

        public HelperRoleHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
            _client.UserVoiceStateUpdated += UserVoiceChannelUpdate;
        }

        private Task UserVoiceChannelUpdate(SocketUser user, SocketVoiceState vsBefore, SocketVoiceState vsAfter)
        {
            var guildUser = user as SocketGuildUser;
            if (guildUser is null)
                return Task.CompletedTask;

            using (var roleHelper = new HelperRoleProvider(guildUser, vsBefore, vsAfter))
            {
                if (roleHelper.ShouldGetRole) { roleHelper.AddHelperRole(); }
                else { roleHelper.RemoveHelperRole(); }
                return Task.CompletedTask;
            }
        }
    }
}
