using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Diagnostics;

namespace StoryTeller.BotModes;

public class DiscordBot : IMode
{
    List<int> runList = new List<int>();
    private DiscordClient discord;

    public static string DiscordPrimer(out Mode mode1)
    {
        string primer1;
        Console.WriteLine("You chose DiscordBot.");
        primer1 = $"<s>[INST] <<SYS>> A Role play of an interaction between PLACEHOLDER " +
                  $"and a wizard called DarkMage. DarkMage is the greatest wizard that have ever lived and the ever will be. The wizard sometimes like to talk in Riddles, " +
                  $"he is a bit dark in hes demeanor and has dark sense of humor, The DarkMage is very funny and very smart. " +
                  $"DarkMage is male, but he dont know if PLACEHOLDER is female or male yet, He want to learn more about PLACEHOLDER, and he have a quirk he sometime say 'wubwub'<</SYS>>\r\n\r\n" +
                  $"PLACEHOLDER: Hi there DarkMage \r\n" +
                  $"DarkMage: *is in s good mood today, he stands with his back to you, and working on a secret* Hello there PLACEHOLDER *in Happy tone*, what a magical day.\r\n\r\n" +
                  $"PLACEHOLDER:[/INST]";
        mode1 = Mode.DiscordBot;
        return primer1;
    }

    public DiscordBot()
    {
        Debug.WriteLine("DiscordBot.cs: DiscordBot()");
    }

    public void OnRequestNextStringReceived(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"Received string: {e.Message}");
    }

    public async void DiscordBotStart(string primer)
    {
        var token = File.ReadAllText("token.txt");

        discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });

        discord.MessageCreated += async (s, e) =>
        {
            var watch = Stopwatch(e, out var startTick);

            if (e.Message.Author.IsBot)
            {
                return;
            }

            var discordUsername = e.Author.Username;
            //Check if the user has a session
            if (Sessions.AllSessions.Any(x => x.Username == discordUsername))
            {
                //If the user has a session, load it
                var session = Sessions.AllSessions.First(x => x.Username == discordUsername);
            }
            else
            {
                //If the user doesn't have a session, create one
                Sessions.AllSessions.Add(new Sessions(discordUsername));
            }

            //Sessions.AllSessions.Add(new Sessions(discordUsername));

            await e.Message.Channel.TriggerTypingAsync();  // Bot starts typing

            //print the message to the console
            Debug.WriteLine("-> | message | " + e.Message.Content);
            var message = e.Message.Content;

            //this will be the primer for the bot
            var theMessage = e.Message.Content.ToLower();
            primer = primer.Replace("PLACEHOLDER", discordUsername);
            primer += " " + theMessage + "\r\n";

            Debug.WriteLine("-> | primer | " + primer);
            var reply = "";

            reply = Sessions.AllSessions.First(x => x.Username == discordUsername).GenerateReplyForDiscord(primer, message, discordUsername);
            reply = reply.Replace("\uFFFD", "").Replace("\u000A", "");
            foreach (char c in reply)
            {
                Debug.WriteLine($"Character: {c}, Unicode: {((int)c).ToString("X4")}");
            }
            
            primer += " " + reply;
            Debug.WriteLine("Reply is now: " + reply);
            reply = reply.Remove(reply.Length - (discordUsername.Length + 1));
            reply = reply.Replace("DarkMage:", "");

            if (primer.Length > 400)
            {
                primer = primer.Substring(primer.Length - 400);
            }

            watch.Stop();
            PrintDebugStats(watch, startTick);

            await e.Message.RespondAsync(reply);
        };

        discord.Ready += OnBotReady;

        RunAsync().GetAwaiter().GetResult(); //Can we use await here?
    }

    private static Stopwatch Stopwatch(MessageCreateEventArgs e, out long startTick)
    {
        Debug.WriteLine("-> | message | " + e.Message.Content);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        startTick = watch.ElapsedTicks;
        Debug.WriteLine("StartTick: " + startTick);
        return watch;
    }

    private void PrintDebugStats(Stopwatch watch, long startTick)
    {
        var endTick = watch.ElapsedTicks;
        Debug.WriteLine("EndTick: " + endTick);
        Debug.WriteLine("Ticks: " + (endTick - startTick));
        Debug.WriteLine("Milliseconds: " + watch.ElapsedMilliseconds);
        Debug.WriteLine("Seconds: " + watch.Elapsed.Seconds);
        Debug.WriteLine("Minutes: " + watch.Elapsed.Minutes);

        //want to save the watch outside of the function to compare runs and see if it gets slower over time
        runList.Add((int)watch.ElapsedMilliseconds);
        //print all the runs
        for (int i = 0; i < runList.Count; i++)
        {
            Debug.WriteLine("run " + i + " took " + runList[i] + "ms");
        }
    }

    private async Task OnBotReady(DiscordClient sender, ReadyEventArgs args)
    {
        var members = await discord.GetGuildAsync(348778629829885953); // Replace with Discord server ID
        var member = members.Members.FirstOrDefault(m => m.Value.Username == "glennwiz").Value;

        if (member != null)
        {
            var dmChannel = await member.CreateDmChannelAsync();
            await dmChannel.SendMessageAsync("The bot is now online!");
        }
    }

    public async Task RunAsync()
    {
        await discord.ConnectAsync();
        await Task.Delay(-1);
    }

    public void StoryTeller(string prompt)
    {
        throw new NotImplementedException();
    }

    public class CustomEventArgs : EventArgs
    {
        public string Message { get; set; }
        public CustomEventArgs(string message)
        {
            Message = message;
        }
    }
}