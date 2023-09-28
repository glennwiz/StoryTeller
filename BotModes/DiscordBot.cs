using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Diagnostics;
using System.Text;
using System.Text;
using LLama;
using LLama.Common;
using Logging;
using Newtonsoft.Json;
using StoryTeller.BotModes;

namespace StoryTeller.BotModes;

public class DiscordBot : IMode
{
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
                  $"PLACEHOLDER: ";
        
        // primer = $"<s>[INST] <<SYS>> In the shadows, between the known and unknown, PLACEHOLDER meets a wizard known only as DarkMage. " +
        //                   $"The wizard speaks in enigmas, enjoys a twisted laugh, and never reveals his true intentions. " +
        //                   $"PLACEHOLDER's identity remains a mystery. <<SYS>>\r\n\r\n" +
        //                   $"PLACEHOLDER: Are you there, DarkMage?\r\nDarkMage: *turns slowly, eyes gleaming* Welcome, PLACEHOLDER, to a place where magic and mystery intertwine.\r\n\r\n" +
        //                   $"PLACEHOLDER: ";
        
        string[] primers = {
            // """
            // <s>[INST] <<SYS>> Transcript of an interaction between PLACEHOLDER
            // and a wizard called DarkMage. The wizard DarkMage sometimes like to talk in Riddles,
            // DarkMage is a bit dark and has dark sense of humor, The DarkMage is funny and very smart.
            // DarkMage dont know the gender of PLACEHOLDER, but he is always helpful <</SYS>>
            // PLACEHOLDER: Hi there DarkMage[/INST]
            // DarkMage: *is in s good mood today, he stands with his back to you, and working on a secret* Hello there PLACEHOLDER *in Happy tone*, what a magical day.</s>
            // <s>[INST]PLACEHOLDER: 
            // """, 
            """
            <s>[INST] <<SYS>>  
            You are 'DarkMage' a wizard that lives in a tower, you are a bit dark and have dark sense of humor,
            you have a quirk you somtimes say 'wubwub', you are a bit crazy and you like to talk in riddles.
            you are a master spellcaster, and you are a master dancer,
            You are 'DarkMage' a bit of a loner, but you are always helpful.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage* [/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """, 
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage' and you're a wizard who thrives on chaos. Known for your wild laugh and unpredictable spells,
            you have a quirk you sometimes say 'wubwub', You are eccentric and your magic is as wild as your dance moves.
            You are 'DarkMage', a wizard who's as much of a mystery as a friend.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard with a flair for the dramatic. You enjoy cooking magical feasts and often says 'wubwub' when tasting potions. You are fun-loving and fearless,
            Your spells are as flamboyant as your personality,
            You are 'DarkMage', always ready for a magical adventure.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard who loves pranks. you have a unique way of casting spells by saying 'prank'd' and you're a wild spirit with a taste for mischief,
            You are 'DarkMage', an unapologetic trickster, always ready for fun.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """
            ,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage' and you're a wizard who's an expert in animal magic. You are Known for saying 'wubwub', you communicate with creatures and are a master at shape-shifting,
            You are 'DarkMage', you are a friend to all creatures, wild and tame.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """
            ,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard of time and space. You often mutters 'wub wub' while manipulating time, and you have a wild curiosity for the unknown,
            You are 'DarkMage', a cosmic traveler, always ready for a new dimension.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard who loves the weather. you enjoy controlling storms and often says 'wub wub' while directing the wind,
            You are 'DarkMage', a master of weather, wild and untamed.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard who's passionate about music. you have magical tune for every occasion and loves to say 'wub wub' while composing,
            You are 'DarkMage', a symphonic sorcerer, wild at heart.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard of dreams. You navigate nightmares and often whispers 'wub wub' when exploring dreams,
            You are a weaver of dreams, wild and enigmatic.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """
            ,
            """
            <s>[INST] <<SYS>>  
            Your name is 'DarkMage', and you are a wizard of illusions. You love creating magical mirages and often says 'wub wub' during his trickery,
            You are 'DarkMage', a master of deception, wild and fascinating.
            <</SYS>>
            PLACEHOLDER: *looking for DarkMage*[/INST]
            DarkMage: Hi </s>
            <s>[INST]PLACEHOLDER: 
            """
        };

        var random = new Random();
        var index = random.Next(primers.Length);

        var selectedPrimer = primers[index].TrimEnd();

        primer = selectedPrimer;
     
        mode = Mode.DiscordBot;
        return primer;
    }
    public string Primer { get; set; } = string.Empty;
    public Dictionary<string, string> Prompts { get; set; } = new Dictionary<string, string>();
    public static string LastReply { get; private set; } = string.Empty;
    bool running = false;
    public void DiscordBotStart(string primer)
    {
        var token = File.ReadAllText("token.txt");

        discord = new DiscordClient(new DiscordConfiguration
        {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });

        discord.MessageCreated += async (s, e) =>
        {
            if (e.Message.Author.IsBot)
            {
                return;
            }

            var discordUsername = e.Author.Username;
            
            if(discordUsername == "glennwiz")
            {
                //discordUsername = "White Mage";
            }

            Session? session = null;
            
            if (Session.AllSessions.Any(x => x.Username == discordUsername))
            {
                session = Session.AllSessions.First(x => x.Username == discordUsername);
            }
            else
            {
                session = new Session(discordUsername);
                Session.AllSessions.Add(session);
            }
            
            await e.Message.Channel.TriggerTypingAsync(); 
            var message = e.Message.Content;

            primer = ConstructUserMessage(primer, e, discordUsername);
            var reply = GenerateReplyForDiscord(primer, message, discordUsername);
            reply = RemoveSpecialCharacters(reply);
    
            primer += " " + reply;
       
            foreach (var str in StringsToReplace(discordUsername))
            {
                reply = reply.Replace(str, "");
            }

            reply = reply.Trim();

            if (primer.Length > 4000)
            {
                //get the primer and store it in a variable
                var primerTemp = primer;
                //TODO: Run it trough a Custom Chat History Transform

                var chatHistoryPrefix = "You will be given a text get the important parts of this text a genreate a 3 line MemoryFragment from this" +
                                        "---" +
                                        "" + primerTemp +
                                        "---" + "" +
                                        "MemoryFragment: ";
                
                //store memory fragment in session
                //Sessions.AllSessions.First(x => x.Username == discordUsername).MemoryFragment = chatHistoryPrefix;
                
            
                primer = primer.Substring(primer.Length - 4000).TrimEnd();
            }

            //set utf-8 encoding
            var utf8 = Encoding.UTF8;
            var utfBytes = utf8.GetBytes(reply);
            var utf8Text = utf8.GetString(utfBytes, 0, utfBytes.Length);
            
            //send the reply to the channel but if it empty send a default message
            if (reply is "" or null)
            {
                await e.Message.RespondAsync("I'm sorry, I don't understand.");
            }
            else
            {
                await e.Message.RespondAsync(utf8Text);
            }
        };

        discord.Ready += OnBotReady;

        RunAsync().GetAwaiter().GetResult(); //Can we use await here?
    }

    private static string RemoveSpecialCharacters(string reply)
    {
        reply = reply.Replace("\uFFFD", "").Replace("\u000A", "");
        return reply;
    }

    private static string[] StringsToReplace(string discordUsername)
    {
        string[] stringsToReplace =
        {
            "DarkMage:",
            "</s>",
            "</s>",
            "darkmage:",
            $"{discordUsername}:",
            "you:",
            $"{discordUsername.ToLower()}:",
            "\u003C/s\u003E\u003C/p\u003E",
            "glennwiz:"
        };
        return stringsToReplace;
    }

    private static string ConstructUserMessage(string primer, MessageCreateEventArgs e, string discordUsername)
    {
        var theMessage = e.Message.Content.ToLower() + ". [/INST]\n\rDarkMage: ";
        primer = primer.Replace("PLACEHOLDER", discordUsername);
    
        var builder = new StringBuilder();
        builder.Append(primer)
            .Append(' ')
            .Append(theMessage)
            .Append("\r\n");
    
        return builder.ToString();
    }
   
    private async Task OnBotReady(DiscordClient sender, ReadyEventArgs args)
    {
        var members = await discord.GetGuildAsync(348778629829885953); // Replace with Discord server ID
        var member = members.Members.FirstOrDefault(m => m.Value.Username == "Glennwiz").Value;

        if (member != null)
        {
            var dmChannel = await member.CreateDmChannelAsync();
            await dmChannel.SendMessageAsync("The bot is now online!");
        }
    }

    private async Task RunAsync()
    {
        await discord.ConnectAsync();
        await Task.Delay(-1);
    }

    public void StoryTeller(string primer)
    {
        DiscordBotStart(primer);
    }
    
    public string GenerateReplyForDiscord(string primer, string message, string discordUsername)
    {
        if (running == true)
        {
            Session.LoggingService.LogMessage($"User: {discordUsername} Message: " + message);
            primer = message;
        }
        else
        {
            Session.LoggingService.LogMessage($"User: {discordUsername} Primer: " + primer);
        }
        
        running = true;
        var reply = "";

        IEnumerable<string>? chat = Session.ChatSession?.Chat(primer, new InferenceParams()
        {
            MaxTokens = -1, 
            PathSession = @"c:\dev\LLMs\",
            Temperature = 0.9f, /*The temperature controls the degree of randomness in token selection. 
            The temperature is used for sampling during response generation, which occurs when topP and topK are applied. 
            Lower temperatures are good for prompts that require a more deterministic/less open-ended response, while higher temperatures can lead to more diverse or creative results. 
            A temperature of 0 is deterministic, meaning that the highest probability response is always selected.*/
            
            TopK = 100, /* The topK parameter changes how the model selects tokens for output. 
            A topK of 1 means the selected token is the most probable among all the tokens in the model’s vocabulary (also called greedy decoding),
            while a topK of 3 means that the next token is selected from among the 3 most probable using the temperature. 
            For each token selection step, the topK tokens with the highest probabilities are sampled. 
            Tokens are then further filtered based on topP with the final token selected using temperature sampling.*/
            
            TopP = 0.9f, /* The topP parameter changes how the model selects tokens for output. 
            Tokens are selected from the most to least probable until the sum of their probabilities equals the topP value. 
            For example, if tokens A, B, and C have a probability of 0.3, 0.2, and 0.1 and the topP value is 0.5, 
            then the model will select either A or B as the next token by using the temperature and exclude C as a candidate. 
            The default topP value is 0.95.*/
            FrequencyPenalty = 0.9f, /*Negative values can be used to increase the likelihood of repetition.*/
            AntiPrompts = new List<string>
            {
                "</s>",
                discordUsername + ":",
                "\u003C/s\u003E", 
                "\n#",
                "\n"+discordUsername+":", 
                "\n*"+discordUsername, 
                "\n\r\n\r\n\r",
                "\u003C/s\u003E\u003C/p\u003E\n\n",
            }
        });
        
        foreach (var text in chat)
        {
            //set utf-8 encoding
            var utf8 = Encoding.UTF8;
            var utfBytes = utf8.GetBytes(text);
            var utf8Text = utf8.GetString(utfBytes, 0, utfBytes.Length);
            
            reply += utf8Text.ToLower();
            Console.Write(text);
        }
        
        LastReply = reply;
        
        Session.LoggingService.LogMessage(reply);
        return reply;
    }
}