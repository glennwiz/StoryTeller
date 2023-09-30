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
            Temperature = 0.9f, /**/
            
            TopK = 100, /* */
            
            TopP = 0.9f, /* .*/
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