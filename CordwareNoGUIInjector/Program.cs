﻿using CordwareNoGUIInjector.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\index.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/index.txt").Result.Content.ReadAsStringAsync().Result);
                        Directory.CreateDirectory($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware");
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\payload.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/payload.js").Result.Content.ReadAsStringAsync().Result);
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\client.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/client.js").Result.Content.ReadAsStringAsync().Result);
                        Directory.CreateDirectory($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\API");
                        File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\API\\API.js", new HttpClient().GetAsync("https://raw.githubusercontent.com/Yaekith/Cordware/main/API/API.js").Result.Content.ReadAsStringAsync().Result);
                        Directory.CreateDirectory($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\Plugins");
                        var MainBranch = JsonConvert.DeserializeObject<TreeBranchGather>(new HttpClient().GetAsync("https://api.github.com/repos/Yaekith/Cordware/git/trees/main?recursive=1").Result.Content.ReadAsStringAsync().Result);
                        int pluginCount = 0;

                        foreach(var path in MainBranch.tree)
                        {
                            if (path.path.Contains("Plugins") && path.path != "Plugins")
                            {
                                pluginCount++;
                                File.WriteAllText($"{versionfolderpath}\\modules\\discord_desktop_core\\Cordware\\Plugins\\{path.path.Split('/')[1]}", new HttpClient().GetAsync($"https://raw.githubusercontent.com/Yaekith/Cordware/main/{path.path}").Result.Content.ReadAsStringAsync().Result);
                            }
                        }

                        ConsoleUtils.Log($"Injected with {pluginCount} plugin(s) successfully!");
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
