using LLama;
using LLama.Common;

string modelPath = "C:\\Users\\Glennwiz\\AppData\\Local\\nomic.ai\\GPT4All\\wizardLM-13B-Uncensored.ggmlv3.q4_0.bin"; // change it to your own model path

var prompt = "this is the mind of a AI that becomes more and more scentient, and hates humans\r\nAi mind: *dreams about electric sheeps* 'the robot starts to wake up'\r\nNature: *birds chirping* *water sounds in the distance*\r\nAi mind: *slams the head in the wall* 'Eyes open, eyes sparking'\r\nNature: *in the distance a human screams*\r\nAi mind: *HUNT MODE ON*\r\n";

// Initialize a chat session //seed 2000, 1.18f
var ex = new InteractiveExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 2048, seed: 2001, gpuLayerCount: 5)));
ChatSession session = new ChatSession(ex);

// show the prompt
Console.WriteLine();
Console.Write(prompt);

// run the inference in a loop to chat with LLM
while (prompt != "stop")
{
    foreach (var text in session.Chat(prompt, new InferenceParams() { Temperature = 1.15f, AntiPrompts = new List<string> { "\r\n" } }))
    {
        Console.Write(text);
    }
    prompt = "........";
}

// save the oki 
session.SaveSession("SavedSessionPath");