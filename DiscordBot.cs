using DSharpPlus;
using DSharpPlus.EventArgs;
using LLama;
using LLama.Common;

namespace StoryTeller;
public class DiscordBot
{
    private DiscordClient discord;
    public DiscordBot(ChatSession chatSession)
    {
        discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = "MTE0MjU1MzUwMjk0NjcwOTUxNA.GIoCPk.FdBjsipBphI8ldkQVr0z0l8Sz_HYXJOW8ajZpM",
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });
        
        discord.MessageCreated += async (s, e) =>
        {
            if (e.Message.Author.IsBot)
            {
                return;
            }
            
            //this will be the primer for the bot
            var theMessage = e.Message.Content.ToLower();
            
            chatSession.Chat(theMessage, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "\r\n" } });
            
            var reply = ""; //TODO: call the LlamaBot class here
            
            foreach (var text in chatSession.Chat(theMessage,
                         new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "Developer1:" } }))
            {
                Console.Write(text);
                reply += text;
            }
            
            if (e.Message.Content.ToLower().Contains("darkmage"))
            {
                await e.Message.RespondAsync("8bit!");
                return;
            }
            
            await e.Message.RespondAsync(reply);
        };
        
        // Event to execute after bot is ready
        discord.Ready += OnBotReady;
        
        RunAsync().GetAwaiter().GetResult();
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
    
    public async Task RunAsync()
    {
        await discord.ConnectAsync();
        await Task.Delay(-1);
    }
}