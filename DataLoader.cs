using Newtonsoft.Json;

namespace Bot_test
{
    public static class DataLoader
    {
        public static T Load<T>(string filename) where T : class
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json", filename);

                var jsonContent = File.ReadAllText(path);

                var x = JsonConvert.DeserializeObject<T>(jsonContent);

                return x;
            }
            catch (Exception ex)
            {
                // handle the exception or log it
                return null;
            }
        }
    }
}
