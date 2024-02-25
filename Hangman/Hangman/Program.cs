using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    internal class Program
    {
        private static Random random = new Random();
        private static List<string> wordDictionary = new List<string> { "car", "football", "porsche", "house", "apple", "banana", "instagram", "peach" };

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman :)");
            Console.WriteLine("<--------------------------------->");

            string randomWord = SelectRandomWord();
            int maxTries = 6;

            PlayHangman(randomWord, maxTries);

            Console.WriteLine("\nThe word was: " + randomWord);
            Console.WriteLine("Thank You for Playing!");
        }

        private static string SelectRandomWord()
        {
            int i = random.Next(wordDictionary.Count);
            return wordDictionary[i];
        }

        private static void PlayHangman(string randomWord, int maxTries)
        {
            int remainingTries = maxTries;
            HashSet<char> correctLettersGuessed = new HashSet<char>();
            HashSet<char> incorrectLettersGuessed = new HashSet<char>();

            while (remainingTries > 0)
            {
                DisplayCurrentGameState(correctLettersGuessed, randomWord, remainingTries, incorrectLettersGuessed);
                char letterGuessed = GetLetterGuessFromUser(correctLettersGuessed, incorrectLettersGuessed);

                if (randomWord.Contains(letterGuessed))
                {
                    correctLettersGuessed.Add(letterGuessed);
                    if (CheckGameWon(correctLettersGuessed, randomWord))
                    {
                        Console.WriteLine("\nCongratulations! You guessed the word!");
                        return;
                    }
                }
                else
                {
                    if (!incorrectLettersGuessed.Contains(letterGuessed))
                    {
                        remainingTries--;
                        incorrectLettersGuessed.Add(letterGuessed);
                    }
                    else
                    {
                        Console.WriteLine("\nYou've already guessed that letter. Please try a different one.");
                    }
                }
            }

            Console.WriteLine("\nSorry, you've run out of tries. The word was: " + randomWord);
        }

        private static void DisplayCurrentGameState(HashSet<char> correctLettersGuessed, string randomWord, int remainingTries, HashSet<char> incorrectLettersGuessed)
        {
            Console.WriteLine($"\nTries Left: {remainingTries}");
            Console.WriteLine("Your Correct Guesses: " + string.Join(" ", correctLettersGuessed));
            Console.WriteLine("Your Incorrect Guesses: " + string.Join(" ", incorrectLettersGuessed));
            PrintWord(correctLettersGuessed, randomWord);
        }

        private static char GetLetterGuessFromUser(HashSet<char> correctLettersGuessed, HashSet<char> incorrectLettersGuessed)
        {
            char letterGuessed;

            while (true)
            {
                Console.Write("\nGuess a Letter: ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Invalid input. Please enter a single letter.");
                    continue;
                }

                letterGuessed = char.ToLower(input[0]);

                if (correctLettersGuessed.Contains(letterGuessed) || incorrectLettersGuessed.Contains(letterGuessed))
                {
                    Console.WriteLine("Letter already guessed. Please try a different letter.");
                    continue;
                }

                break;
            }

            return letterGuessed;
        }

        private static void PrintWord(HashSet<char> correctLettersGuessed, string randomWord)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in randomWord)
            {
                sb.Append(correctLettersGuessed.Contains(char.ToLower(c)) ? c : '_');
                sb.Append(" ");
            }
            Console.WriteLine("Word: " + sb.ToString());
        }

        private static bool CheckGameWon(HashSet<char> correctLettersGuessed, string randomWord)
        {
            foreach (char c in randomWord.ToLower())
            {
                if (!correctLettersGuessed.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
