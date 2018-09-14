using System.Threading.Tasks;
using Discord.Commands;

namespace Guide.Modules
{
    public class Basics : ModuleBase<SocketCommandContext>
    {
        [Command("hello", RunMode = RunMode.Async)]
        public async Task Hello()
        {
            await ReplyAsync("Hi!");
        }
    }
}