using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Spicy_Bot.Core.UserAccounts;
using System.Net;
using Newtonsoft.Json;
using Discord.Rest;
using Discord.Audio;
using System.Collections.Concurrent;

namespace Spicy_Bot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
       
        [Command("premium")]
        public async Task RevealSecret()
        {
            if (!UserIsSecretOwner((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync(":x: You need the Premium role to do that. " + Context.User.Mention);
                return;
            }
            else
            {
                var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync(Utilities.GetAlert("Work In Progress"));
            }
        }

        private bool UserIsSecretOwner(SocketGuildUser user)
        {
            string targetRoleName = "Premium";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }

        [Command("data")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync("Data Has " + DataStorage.GetPairsCount() + " pairs.");
        }



        [Command("setgame")]
        public async Task Set([Remainder]string message)
        {



            await Context.Client.SetGameAsync(message);
            Console.WriteLine("Game Set to " + message + " By " + Context.Client.CurrentUser.Username);

            var e = new EmbedBuilder();
            e.WithTitle("Game Set");
            e.WithDescription("Game Set to: " + message);
        }
    }
}