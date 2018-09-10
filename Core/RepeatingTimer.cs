using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Spicy_Bot.Core
{
    internal static class RepeatingTimer
    {
        private static Timer loopingTimer;
        private static SocketTextChannel channel;

        internal static Task StartTimer()
        {
            channel = Global.Client.GetGuild(476586832063365130).GetTextChannel(476586832063365133);

            loopingTimer = new Timer()
            {
                Interval = 7000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            var em = new EmbedBuilder();
            em.WithTitle("Automated Message");
            em.WithDescription("FUCK ME DADDY DIE DIE DIE DIE DIE");
            em.WithColor(new Color(0, 255, 0));

            await channel.SendMessageAsync("", false, em);
        }
    }
}