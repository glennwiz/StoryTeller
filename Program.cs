using LLama;
using LLama.Common;

string modelPath = "C:\\Users\\Glennwiz\\AppData\\Local\\nomic.ai\\GPT4All\\wizardLM-13B-Uncensored.ggmlv3.q4_0.bin"; // change it to your own model path
//var prompt = "this is a transcript from a document with a insane alien that knows supreme horror, and hav a lust for humans, you look at all things at the same time alt the time:\r\n \r\n Alien: i know because i dream!\r\n Sleeper: Who are you?\r\n Alien: You must clear your mind! \r\n Sleeper: why? \r\n";

var prompt = "json word generator: generate random JSON objects, based on your own patterns and ideas. Experiment with different patterns and layouts to create interesting and useful data structures that can be easily parsed and manipulated by other software components or humans:" +
    "" +
    "" +
    "lists logically will help in generating coherent and diverse stories. Here's a suggestion for some logical names for your lists:\r\n\r\nCharacters\r\n\r\nProtagonists: Main characters around whom the story revolves.\r\nAntagonists: Characters who oppose the protagonists.\r\nSupportingCharacters: Other characters who play smaller roles.\r\nMysticalBeings: If your story includes fantasy elements.\r\nAnimals: If animals play a significant role.\r\nSettings\r\n\r\nUrbanLocations: Cities, towns, etc.\r\nRuralLocations: Villages, farms, forests, etc.\r\nIndoorSettings: Specific places like houses, castles, inns, etc.\r\nNaturalLandmarks: Mountains, rivers, lakes, etc.\r\nFantasyRealms: Magical lands, other dimensions, etc.\r\nPlotTwists\r\n\r\nRevelations: Discoveries that change the story's direction.\r\nBetrayals: Acts of treachery by characters.\r\nMysteries: Unexplained events or phenomena.\r\nChallenges: Difficulties the characters must overcome.\r\nThemes\r\n\r\nLove: Romantic themes.\r\nFriendship: Bonds between characters.\r\nRevenge: Acts of vengeance.\r\nDiscovery: Exploration and finding new things.\r\nRedemption: Characters seeking to make amends.\r\nObjects\r\n\r\nMagicalItems: Objects with special powers.\r\nEverydayItems: Common objects characters might interact with.\r\nWeapons: Tools for combat or defense.\r\nArtifacts: Ancient or significant items.\r\nEmotions\r\n\r\nPositiveEmotions: Happiness, love, excitement, etc.\r\nNegativeEmotions: Anger, sadness, fear, etc.\r\nNeutralEmotions: Curiosity, confusion, etc.\r\nActions\r\n\r\nCombatMoves: Punch, kick, slash, etc.\r\nTravelMethods: Walk, run, fly, teleport, etc.\r\nInteractions: Talk, hug, argue, etc.\r\nTimePeriods\r\n\r\nPast: Historical settings.\r\nPresent: Modern-day settings.\r\nFuture: Futuristic settings.\r\nTimeless: Settings that don't fit a specific time.\r\nGenres\r\n\r\nFantasy: Magic, dragons, etc.\r\nSciFi: Space, aliens, advanced technology, etc.\r\nMystery: Whodunits, detective stories, etc.\r\nRomance: Love stories.\r\nHorror: Ghosts, monsters, psychological thrills, etc.\r\nWeatherConditions\r\n\r\nSunnyDays: Clear skies, warm weather.\r\nRainyDays: Rain, thunderstorms.\r\nSnowyDays: Snow, blizzards.\r\nWindyDays: Breezy to stormy conditions.";

// Initialize a chat session //seed 2000, 1.18f
var ex = new InteractiveExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 2048, seed: 2001, gpuLayerCount: 5)));
ChatSession session = new ChatSession(ex);

// show the prompt
Console.WriteLine();
Console.Write(prompt);

// run the inference in a loop to chat with LLM
while (prompt != "stop")
{
    foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 0.1f, AntiPrompts = new List<string> { "conditions." } }))
    {
        Console.Write(text);
    }
    prompt = "{";
}

// save the oki 
session.SaveSession("SavedSessionPath");