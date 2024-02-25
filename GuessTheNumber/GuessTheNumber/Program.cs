namespace GuessTheNumber
{
    internal class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            bool isChoosingTries = true;
            while (isChoosingTries)
            {
                Console.WriteLine("Choose how many tries you want to have:");
                Console.WriteLine("1. 5 tries");
                Console.WriteLine("2. 10 tries");

                if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("Invalid input! Please try again!");
                    Console.Clear();
                    continue;
                }
                else
                {
                    GuessNumber(choice);
                }

                isChoosingTries = false;
            }
        }

        static void GuessNumber(int choice)
        {
            int mainNumber;
            int maxTries;
            int upperLimit;

            if (choice == 1)
            {
                mainNumber = random.Next(1, 50);
                maxTries = 5;
                upperLimit = 50;
            }
            else
            {
                mainNumber = random.Next(1, 201);
                maxTries = 10;
                upperLimit = 200;
            }

            Console.WriteLine($"Guess the number between 1 and {upperLimit}! You have {maxTries} tries. Enter '0' for a hint (costs 3 tries)");

            int tries = maxTries;

            while (tries > 0)
            {
                string input = Console.ReadLine();

                if (input == "0")
                {
                    if (tries > 3)
                    {
                        Hint(mainNumber, upperLimit, choice == 1 ? 10 : 30);
                        tries -= 3;
                        Console.WriteLine($"You have {tries} tries left!");
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough tries for a hint. Keep playing without a hint.");
                    }
                    continue;
                }

                if (!int.TryParse(input, out int guessNumber))
                {
                    Console.WriteLine("Invalid input! Please enter a valid number.");
                    continue;
                }

                if (guessNumber < mainNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guessNumber > mainNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You have guessed the number. It is {mainNumber}");
                    return;
                }

                tries--;
                Console.WriteLine($"You have {tries} tries left!");
            }

            Console.WriteLine($"Sorry, you have run out of tries. The number was {mainNumber}");
        }

        static void Hint(int mainNumber, int upperLimit, int range)
        {
            int lowerBound = Math.Max(1, mainNumber - range);
            int upperBound = Math.Min(upperLimit, mainNumber + range);

            Console.WriteLine($"The number is in the range from {lowerBound} to {upperBound}");
        }
    }
}
