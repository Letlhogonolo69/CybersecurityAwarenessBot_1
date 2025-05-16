using System;
using System.Collections.Generic;
using System.Threading;
using NAudio.Wave;

namespace CybersecurityAwarenessBot
{
    class Program
    {
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> { "Use strong, unique passwords.", "Avoid using personal info in passwords.", "Change your passwords regularly." } },
            { "scam", new List<string> { "Be cautious of unexpected emails or messages.", "Verify links before clicking.", "Watch out for fake websites." } },
            { "privacy", new List<string> { "Limit the personal info you share online.", "Review your privacy settings regularly.", "Use two-factor authentication for added security." } }
        };

        static Dictionary<string, string> userMemory = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PlayVoiceGreeting();
            Console.ForegroundColor = ConsoleColor.Green;
            DisplayAsciiLogo();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nEnter your name: ");
            Console.ResetColor();
            string userName = Console.ReadLine();
            userMemory["name"] = userName;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nWelcome, {userName}! Let's keep you safe online.");
            Console.ResetColor();
            ShowAvailableTopics();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nWhat would you like to ask about cybersecurity? (Type 'exit' to quit): ");
                Console.ResetColor();
                string userInput = Console.ReadLine().ToLower();
                if (userInput == "exit") break;
                RespondToQuestion(userInput, userName);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nThank you for using the Cybersecurity Awareness Bot, {userMemory["name"]}! Stay safe!");
            Console.ResetColor();
        }

        static void PlayVoiceGreeting()
        {
            try
            {
                using (var audioFile = new AudioFileReader("voice_greeting.wav"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing voice greeting: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void DisplayAsciiLogo()
        {
            Console.WriteLine("=============================");
            Console.WriteLine("  CYBERSECURITY AWARENESS BOT ");
            Console.WriteLine("=============================");
            Console.WriteLine("   Protecting Your Digital Life");
            Console.WriteLine("=============================");
        }

        static void ShowAvailableTopics()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nHere are some topics you can ask me about:");
            Console.WriteLine("- Password Safety");
            Console.WriteLine("- Phishing Awareness");
            Console.WriteLine("- Safe Browsing");
            Console.WriteLine("- Two-Factor Authentication");
            Console.WriteLine("- Social Media Privacy");
            Console.WriteLine("- Scams and Fraud");
            Console.ResetColor();
        }

        static void RespondToQuestion(string userInput, string userName)
        {
            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    var responses = keywordResponses[keyword];
                    var random = new Random();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{userName}, {responses[random.Next(responses.Count)]}");
                    Console.ResetColor();
                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Hmm, I don't have a response for that yet. Try asking about cybersecurity!");
            Console.ResetColor();
        }
    }
}
