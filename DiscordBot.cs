using System.Diagnostics;
using DSharpPlus;
using DSharpPlus.EventArgs;
using LLama;
using LLama.Common;

namespace StoryTeller;
public class DiscordBot
{
    private DiscordClient discord;
    public DiscordBot(ChatSession chatSession, string primer)
    {
        discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = File.ReadAllText("token.txt"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });
        
        discord.MessageCreated += async (s, e) =>
        {
            if (e.Message.Author.IsBot)
            {
                return;
            }
            //debug log the message
            Debug.WriteLine("-> | message | " + e.Message.Content);
            
            if (e.Message.Content.ToLower().Contains("darkmage"))
            {
                await e.Message.RespondAsync("8bit!");
                return;
            }
            await e.Message.Channel.TriggerTypingAsync();  // Bot starts typing
            
            //this will be the primer for the bot
            var theMessage = e.Message.Content.ToLower();
            
            primer += " " + theMessage + "\r\n";
            //debug log the message
            Debug.WriteLine("-> | primer | " + primer);
            var reply = ""; 
            foreach (var text in chatSession.Chat(primer,
                         new InferenceParams() { Temperature = 0.9f, AntiPrompts = new List<string> { "User:" } }))
            {
                Console.Write(text);
                reply += text;
            }
            primer += " " + reply;
            
            Debug.WriteLine("------");
            Debug.WriteLine(reply);
            Debug.WriteLine("------");
            
            //reply contains the response and the last 5chars are "User:" and i need to remove it from the reply
            reply = reply.Remove(reply.Length - 5);
            
            //i need to remove the name DarkMage from the reply
            reply = reply.Replace("DarkMage:", "");
            
            //i need to trim the primer in front, it cant grow forever
            //if lengt is greater then 200 trim front so that only 200 chars remain
            
            
            
            await e.Message.RespondAsync(reply);
        };
        
        // Event to execute after bot is ready
        discord.Ready += OnBotReady;
        
        RunAsync().GetAwaiter().GetResult();
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
}