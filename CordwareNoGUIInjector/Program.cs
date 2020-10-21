using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CordwareNoGUIInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cordware Injector - No GUI";
            try
            {
                ConsoleUtils.Log("What client do you use? (Or want to inject into) (Type discord for Discord, discordcanary for Canary, etc): ");
                string client = Console.ReadLine();
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!Directory.Exists($"{appData}\\{client}")) 
                {
                    ConsoleUtils.LogError("Could not find the client you're specifying you want to inject into. Try again.");
                    Console.ReadKey();
                    Console.Clear();
                    Main(null);
                }
                else
                {
                    var folders = Directory.GetDirectories($"{appData}\\{client}");
                    var versionfolderpath = folders[0];

                    if (Directory.Exists($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware"))
                        ConsoleUtils.LogError("Already Injected.");
                    else
                    {
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/index.txt").Result.Content.ReadAsStringAsync().Result);
                        Directory.CreateDirectory($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware");
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\payload.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/payload.js").Result.Content.ReadAsStringAsync().Result);
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\client.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/client.js").Result.Content.ReadAsStringAsync().Result);
                        Directory.CreateDirectory($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\API");
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\API\\API.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/API/API.js").Result.Content.ReadAsStringAsync().Result);
                        Directory.CreateDirectory($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\Plugins");
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\Plugins\\plugin.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/Plugins/plugin.js").Result.Content.ReadAsStringAsync().Result);
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\Plugins\\antimutespoofer.cord.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/Plugins/antimutespoofer.cord.js").Result.Content.ReadAsStringAsync().Result);
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\Plugins\\nobuildoverride.cord.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/Plugins/nobuildoverride.cord.js").Result.Content.ReadAsStringAsync().Result);
                        ConsoleUtils.Log("Injected with 2 Plugins successfully (Anti Mute Spoofer and No Build Override)");
                    }
                }
            }
            catch(Exception e) {
                ConsoleUtils.LogError($"An exception occurred while injecting\nException: {e}\nMake an issue with this on the github.");
            }
            Console.ReadLine();
        }
    }
}
