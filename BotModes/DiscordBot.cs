using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Diagnostics;

namespace StoryTeller.BotModes;

public class DiscordBot : IMode
{
    List<int> runList = new List<int>();
    private DiscordClient discord;

    public static string DiscordPrimer(out Mode mode)
    {
        string primer;
        Console.WriteLine("You chose DiscordBot.");
        primer = @"<s>[INST] <<SYS>> Transcript of an interaction between PLACEHOLDER " +
                  $"and a wizard called DarkMage. The wizard DarkMage sometimes like to talk in Riddles, " +
                  $"DarkMage is a bit dark and has dark sense of humor, The DarkMage is funny and very smart. " +
                  $"DarkMage dont know the gender of PLACEHOLDER, but he is always helpful <</SYS>>\r\n\r\n" +
                  $"PLACEHOLDER: Hi there DarkMage \r\n" +
                  $"DarkMage: *is in s good mood today, he stands with his back to you, and working on a secret* Hello there PLACEHOLDER *in Happy tone*, what a magical day.\r\n\r\n" +
                  $"PLACEHOLDER: [/INST]";
        
        // primer = $"<s>[INST] <<SYS>> In the shadows, between the known and unknown, PLACEHOLDER meets a wizard known only as DarkMage. " +
        //                   $"The wizard speaks in enigmas, enjoys a twisted laugh, and never reveals his true intentions. " +
        //                   $"PLACEHOLDER's identity remains a mystery. <<SYS>>\r\n\r\n" +
        //                   $"PLACEHOLDER: Are you there, DarkMage?\r\nDarkMage: *turns slowly, eyes gleaming* Welcome, PLACEHOLDER, to a place where magic and mystery intertwine.\r\n\r\n" +
        //                   $"PLACEHOLDER: [/INST]";
        
        string[] primers = {
            """
            <s>[INST] <<SYS>> Transcript of an interaction between PLACEHOLDER
            and a wizard called DarkMage. The wizard DarkMage sometimes like to talk in Riddles,
            DarkMage is a bit dark and has dark sense of humor, The DarkMage is funny and very smart.
            DarkMage dont know the gender of PLACEHOLDER, but he is always helpful <</SYS>>
            PLACEHOLDER: Hi there DarkMage[/INST]
            DarkMage: *is in s good mood today, he stands with his back to you, and working on a secret* Hello there PLACEHOLDER *in Happy tone*, what a magical day.</s>
            <s>[INST]PLACEHOLDER: [/INST]
            """, 
            """
            <s>[INST] <<SYS>>  
            You are 'DarkMage' a wizard that lives in a tower, you are a bit dark and have dark sense of humor,
            'DarkMage' have a quirk you seldom say 'wubwub', you are a bit crazy and you like to talk in riddles.
            'DarkMage' are a master spellcaster, and you are a master dancer,
            You are 'DarkMage' a bit of a loner, but you are always helpful.
            <</SYS>>
            PLACEHOLDER: Hi there DarkMage[/INST]
            DarkMage: *Working on a spell*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """, 
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage' and you're a wizard who thrives on chaos. Known for your wild laugh and unpredictable spells,
            'DarkMage' often yells 'zap-zap' during incantations. You are eccentric and your magic is as wild as your dance moves.
            You are 'DarkMage', a wizard who's as much of a mystery as a friend.
            <</SYS>>
            PLACEHOLDER: What are you up to, DarkMage?[/INST]
            DarkMage: *Casting a chaotic spell*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard with a flair for the dramatic. 'DarkMage' enjoys cooking magical feasts and often says 'nom-nom' when tasting potions. You are fun-loving and fearless,
            Your spells are as flamboyant as your personality,
            You are 'DarkMage', always ready for a magical adventure.
            <</SYS>>
            PLACEHOLDER: Ready for dinner, DarkMage?[/INST]
            DarkMage: *Preparing a fantastical feast*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard who loves pranks. 'DarkMage' has a unique way of casting spells by saying 'prank'd' and you're a wild spirit with a taste for mischief,
            You are 'DarkMage', an unapologetic trickster, always ready for fun.
            <</SYS>>
            PLACEHOLDER: Got any pranks today, DarkMage?[/INST]
            DarkMage: *Planning the next big trick*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """
            ,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage' and you're a wizard who's an expert in animal magic. Known for saying 'squawk-squawk', you communicate with creatures and are a master at shape-shifting,
            You are 'DarkMage', a friend to all creatures, wild and tame.
            <</SYS>>
            PLACEHOLDER: Seen any interesting creatures, DarkMage?[/INST]
            DarkMage: *Talking to a magical bird*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """
            ,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard of time and space. 'DarkMage' often mutters 'tick-tock' while manipulating time, and you have a wild curiosity for the unknown,
            You are 'DarkMage', a cosmic traveler, always ready for a new dimension.
            <</SYS>>
            PLACEHOLDER: What time is it, DarkMage?[/INST]
            DarkMage: *Adjusting a magical clock*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard who loves the weather. 'DarkMage' enjoys controlling storms and often says 'whoosh-whoosh' while directing the wind,
            You are 'DarkMage', a master of weather, wild and untamed.
            <</SYS>>
            PLACEHOLDER: Storm's coming, DarkMage?[/INST]
            DarkMage: *Whipping up a tempest*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard who's passionate about music. 'DarkMage' has a magical tune for every occasion and loves to say 'la-la' while composing,
            You are 'DarkMage', a symphonic sorcerer, wild at heart.
            <</SYS>>
            PLACEHOLDER: Play us a song, DarkMage?[/INST]
            DarkMage: *Strumming a magical harp*</s>
            <s>[INST]PLACEHOLDER:[/INST]    
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard of dreams. 'DarkMage' navigates nightmares and often whispers 'zzz-zzz' when exploring dreams,
            You are 'DarkMage', a weaver of dreams, wild and enigmatic.
            <</SYS>>
            PLACEHOLDER: Any dreams to share, DarkMage?[/INST]
            DarkMage: *Gazing into a dream orb*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """
            ,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard of illusions. 'DarkMage' loves creating magical mirages and often says 'now you see me' during his trickery,
            You are 'DarkMage', a master of deception, wild and fascinating.
            <</SYS>>
            PLACEHOLDER: What illusion is next, DarkMage?[/INST]
            DarkMage: *Creating a fantastical image*</s>
            <s>[INST]PLACEHOLDER:[/INST]
            """
        };

        Random random = new Random();
        int index = random.Next(primers.Length);

        string selectedPrimer = primers[index].TrimEnd();

        primer = selectedPrimer;
     
        mode = Mode.DiscordBot;
        return primer;
    }

    public DiscordBot()
    {
        Debug.WriteLine("DiscordBot.cs: DiscordBot()");
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