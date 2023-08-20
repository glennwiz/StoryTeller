using System.Diagnostics;
using DSharpPlus;
using DSharpPlus.EventArgs;
using LLama;
using LLama.Common;

namespace StoryTeller;

public class DiscordBot : IMode
{
    List<int> runList = new List<int>();
    private DiscordClient discord;
    
    public static string DiscordPrimer(out Mode mode1)
    {
        string primer1;
        Console.WriteLine("You chose DiscordBot.");
        primer1 = $"<s>[INST] <<SYS>> Transcript of an interaction between PLACEHOLDER " +
                  $"and a wizard called DarkMage. The wizard somtimes like to talk in Riddles, " +
                  $"he is a bit dark and has dark humor, The DarkMage is funny and very smart. " +
                  $"He dont know the gender of PLACEHOLDER <</SYS>>\r\n\r\n" +
                  $"PLACEHOLDER: Hi there DarkMage \r\n" +
                  $"DarkMage: *is in s good mood today, he stands with his back to you, and working on a secret* Hello there PLACEHOLDER *in Happy tone*, what a magical day.\r\n\r\n" +
                  $"PLACEHOLDER: [/INST]";
        mode1 = Mode.DiscordBot;
        return primer1;
    }

    public DiscordBot()
    {
        Debug.WriteLine("DiscordBot.cs: DiscordBot()");
    }
    
    public void DiscordBotStart(string primer)
    {
        discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = File.ReadAllText("token.txt"),
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
            
            Sessions.AllSessions.Add(new Sessions(discordUsername));
            
            await e.Message.Channel.TriggerTypingAsync();  // Bot starts typing
            
            //this will be the primer for the bot
            var theMessage = e.Message.Content.ToLower();
            primer = primer.Replace("PLACEHOLDER", discordUsername);
            primer += " " + theMessage + "\r\n";

            Debug.WriteLine("-> | primer | " + primer);
            var reply = ""; 
            foreach (var text in Sessions.ChatSession.Chat(primer,
                     new InferenceParams()
                     {
                         Temperature = 0.9f, AntiPrompts = new List<string> { discordUsername+":" }
                     }))
            {
                Console.Write(text);
                reply += text;
            }
            
            primer += " " + reply;
            Debug.WriteLine("Reply is now: "+ reply);
            reply = reply.Remove(reply.Length - (discordUsername.Length+1));
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
        
        RunAsync().GetAwaiter().GetResult();
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
        runList.Add((int) watch.ElapsedMilliseconds);
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
}