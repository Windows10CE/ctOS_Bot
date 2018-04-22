using System;
using System.Collections.Generic;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DiscordBot_Test.Modules {
    [Group("Profile")]
    public class ctOS : ModuleBase<SocketCommandContext> {
        [Command]
        public async Task Default() {
            EmbedBuilder builder = new EmbedBuilder();

            builder
                .AddInlineField("profile show [name]", "Shows the profile of the name given.")
                .AddInlineField("profile import", "Imports a profile, must have a JSON file from ctOS_Registration attached to the message. (Doesn't exist, WIP)")
                .WithColor(Color.Red);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("show")]
        public async Task ShowProfileAsync([Remainder] string name) {
            string safeFileName(string s) {
                return s
                    .Replace("\\", "")
                    .Replace("/", "")
                    .Replace("\"", "")
                    .Replace("*", "")
                    .Replace(":", "")
                    .Replace("?", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("|", "")
                    .Replace(" ", "_");
            }

            if (File.Exists($@"C:\Users\aaron\Documents\Profiles\{safeFileName(name)}.json")) {
                string[] profile = RetrieveProfile(name);
                if (profile[0] != "error") {
                    EmbedBuilder builder = new EmbedBuilder();

                    int index = profile[8].IndexOf('.');
                    profile[8] = profile[8].Substring(0, index + 3) + @"%";

                    builder
                        .AddInlineField("Name", profile[0])
                        .AddInlineField("Gender", profile[1])
                        .AddInlineField("Age", profile[2])
                        .AddInlineField("Race", profile[4])
                        .AddInlineField("Occupation", profile[3])
                        .AddInlineField("Salary", profile[6])
                        .AddInlineField("Affiliations", profile[5])
                        .AddInlineField("Place of Birth", profile[7])
                        .AddInlineField("Threat Level", profile[8])
                        .WithColor(Color.DarkRed);

                    await ReplyAsync($"Profile of : {name}", false, builder.Build());
                } else await ReplyAsync($"{Context.User.Mention} there was an error.");
            } else await ReplyAsync($"{Context.User.Mention} profile for {name} was not found.");
        }
        // 0 = name, 1 = gender, 2 = age, 3 = occupation, 4 = race, 5 = affiliations, 6 = salary, 7 = place of birth, 8 = threat level
        public static string[] RetrieveProfile(string name) {
            string filename = $@"C:\Users\aaron\Documents\Profiles\{safeFileName(name)}.json";
            Console.WriteLine(filename);
            string safeFileName(string s) {
                return s
                    .Replace("\\", "")
                    .Replace("/", "")
                    .Replace("\"", "")
                    .Replace("*", "")
                    .Replace(":", "")
                    .Replace("?", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("|", "")
                    .Replace(" ", "_");
            }

            bool error = false;
            string GetJObjectValue(JObject array, string key) {
                foreach (KeyValuePair<string, JToken> keyValuePair in array) {
                    if (key == keyValuePair.Key) {
                        return keyValuePair.Value.ToString();
                    }
                }
                if (key == "Threat Level") {
                    return "No Threat Level Data Found";
                } else {
                    Console.WriteLine("Error, KeyValue pair not found for key: " + key);
                    return String.Empty;
                }
            }
            JObject profile;
            try {
                profile = JObject.Parse(File.ReadAllText(filename));
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                error = true;
                profile = new JObject();
            }

            if (!error) {
                string gender = GetJObjectValue(profile, "Gender");
                string age = GetJObjectValue(profile, "Age");
                string occupation = GetJObjectValue(profile, "Occupation");
                string race = GetJObjectValue(profile, "Race");
                string affiliations = GetJObjectValue(profile, "Affiliations");
                string salary = GetJObjectValue(profile, "Salary");
                string pob = GetJObjectValue(profile, "Place Of Birth");
                string threatLevel = GetJObjectValue(profile, "Threat Level");

                string[] info = { name, gender, age, occupation, race, affiliations, salary, pob, threatLevel }; // 0 = name, 1 = gender, 2 = age, 3 = occupation, 4 = race, 5 = affiliations, 6 = salary, 7 = place of birth, 8 = threat level

                return info;
            } else {
                string[] errorArray = { "error", "error", "error", "error", "error", "error", "error", "error", "error" };
                return errorArray;
            }
        }
        
        [Command("create"), RequireOwner]
        public async Task ImportJsonAsync([Remainder] string profile = "") {
            if (profile != "") {
                string[] splitChars = { ", " };
                string[] profileArray = profile.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                string filename = $@"C:\Users\aaron\Documents\Profiles\{safeFileName(profileArray[0])}.json";
                Console.WriteLine($"Creating new profile for {profileArray[0]}");
                string safeFileName(string s) {
                    return s
                        .Replace("\\", "")
                        .Replace("/", "")
                        .Replace("\"", "")
                        .Replace("*", "")
                        .Replace(":", "")
                        .Replace("?", "")
                        .Replace("<", "")
                        .Replace(">", "")
                        .Replace("|", "")
                        .Replace(" ", "_");
                }

                if (profileArray.Length == 8) {
                    double GetRandomNumber(double minimum, double maximum) {
                        Random random = new Random();
                        return random.NextDouble() * (maximum - minimum) + minimum;
                    }
                    JObject profileObj = new JObject(
                        new JProperty("Name", profileArray[0]),
                        new JProperty("Gender", profileArray[2]),
                        new JProperty("Place Of Birth", profileArray[7]),
                        new JProperty("Age", profileArray[1]),
                        new JProperty("Occupation", profileArray[3]),
                        new JProperty("Race", profileArray[4]),
                        new JProperty("Affiliations", profileArray[5]),
                        new JProperty("Salary", profileArray[6]),
                        new JProperty("Threat Level", GetRandomNumber(0, 100).ToString()));
                    if (File.Exists(filename)) {
                        File.Delete(filename);
                        File.WriteAllText(filename, profileObj.ToString());
                        await ReplyAsync($"Profile created for : {profileArray[0]}");
                    } else File.WriteAllText(filename, profileObj.ToString());
                } else if (profileArray.Length < 8) {
                    await ReplyAsync("Not enough arguments.");
                } else if (profileArray.Length > 8) {
                    await ReplyAsync("Too many arguments");
                } else await ReplyAsync("An error has occured.");
            } else {
                EmbedBuilder builder = new EmbedBuilder();

                builder.WithTitle("profile create")
                                    .WithDescription(@"profile create [name], [age], [gender], [occupation], [race], [affiliations], [salary], [place of birth]")
                                    .WithColor(Color.DarkTeal);
                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}