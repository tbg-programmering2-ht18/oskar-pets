using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Pets
{
    class Program
    {
        static Dictionary<String, String> userPasswordDict = new Dictionary<string, string>();
        static Dictionary<String, Pet> userAnimalDict = new Dictionary<String, Pet>();
        static void Main(string[] args)
        {
            setup();

            bool userLoggedIn = false;
            bool done = false;
            string registerPassword = "";

            while (!done)
            {
                Console.WriteLine("Enter your username: ");
                string user = Console.ReadLine();

                bool userExist = userPasswordDict.TryGetValue(user,out registerPassword);
                if (userExist)
                {
                    Console.Write("Enter your password: ");
                    string password = ReadPassword();
                    if (password.CompareTo(registerPassword) == 0)
                    {
                        Console.WriteLine("Welocome {0}! Yout are now logged in!", user);
                        Pet registeredAnimal;
                        bool animalExist = userAnimalDict.TryGetValue(user, out registeredAnimal);
                        if (animalExist)
                        {
                            Console.WriteLine("This is your animal: {0}", registeredAnimal.Show());
                        }
                        else
                        {
                            Console.WriteLine("Sorry. There was no animal registered for you");
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("You entered the wrong password.");
                    }
                }
                else
                {
                    Console.WriteLine("The User {0} is not found", user);
                    Console.Write("Try again? (Enter yes or no): ");

                    string answer = Console.ReadLine();
                    done = (!answer.ToLower().StartsWith("y"));
                }
            }
            
        }

        private static void setup()
        {
            string path = @"C:\Tempr\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            String userFileName = path + "usr_register.txt";
            if (!File.Exists(userFileName))
            {
                FileStream f = File.Create(userFileName);
                f.Close();
                userPasswordDict.Add("sara", "123");
                userPasswordDict.Add("knas", "111");
                userPasswordDict.Add("hej", "222");

                string jsonUserPassword = JsonConvert.SerializeObject(userPasswordDict, Formatting.Indented);
                File.WriteAllText(userFileName, jsonUserPassword);
            }
            else
            {
                string json = File.ReadAllText(userFileName);
                userPasswordDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
            }
            String arFileName = path + "animal_Ragister.txt";
            if (!File.Exists(arFileName))
            {
                FileStream f = File.Create(arFileName);
                f.Close();
                userAnimalDict.Add("sara", new Pet("Cat", "Enpejst", "mjau", false));
                userAnimalDict.Add("sara", new Pet("Turtle", "Nani", "......", false));

                string jsonUserAnimal = JsonConvert.SerializeObject(userAnimalDict, Formatting.Indented);
            }
            else
            {
                string json = File.ReadAllText(arFileName);
                userAnimalDict = JsonConvert.DeserializeObject<Dictionary<String, Pet>>(json);
            }
        }

        private static string ReadPassword()
        {
            string pass = "";

            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key == null || key.Key != ConsoleKey.Enter)
            {
                pass += key.KeyChar;
                Console.Write("*");
                key = Console.ReadKey(true);

            }
            return pass;
        }
    }
}
