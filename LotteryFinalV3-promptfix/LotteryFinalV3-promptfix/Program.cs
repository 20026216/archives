class Program
{
    static void Main()
    {
        int prompts = 7;                                                                 // change this for multiple prompts and winning numbers
        int lowestValue = 1;
        int highestValue = 10;                                                         // Reduce this number to see if the duplication prevention works, and to work with less numbers for testing
        Console.WriteLine("Welcome To The Lottery Game");

        Console.WriteLine($"Enter {prompts} Numbers within {lowestValue}-{highestValue}: \n");
        int[] userinputs = new int[prompts];
        InputNums(userinputs, lowestValue, highestValue);
        Console.WriteLine($"\nHere are the numbers you picked:");
        foreach (int num in userinputs)
        {
            Console.Write(num + " ");
        }


        Console.WriteLine("\nHere are the winning numbers:");
        int[] winningNumbers = new int[prompts];                     // made it neet
        GenerateWinNums(winningNumbers, lowestValue, highestValue);
        foreach (int num in winningNumbers)
        {
            Console.Write(num + " ");
        }

        Console.WriteLine();
        CheckWinNums(userinputs, winningNumbers);
    }

    static void InputNums(int[] userinputs, int lowestValue, int highestValue)
    {
        HashSet<int> antidupes = new HashSet<int>(); // to get unique numbers, from https://stackoverflow.com/questions/47240829/loops-for-prevent-duplicate-input-to-an-array
        for (int i = 0; i < userinputs.Length; i++)
        {
            int number;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write($"Num {i + 1} :\n");

                string input = Console.ReadLine();
                if (int.TryParse(input, out number))    // checks if its a number
                {
                    if (number >= lowestValue && number <= highestValue) // checks if the number inputted by the user is within the range of highest and lowest value
                    {
                        if (!antidupes.Contains(number)) // checks if the user number has been inputted before
                        {
                            userinputs[i] = number;
                            antidupes.Add(number);
                            validInput = true;

                        }
                        else
                        {
                            Console.WriteLine("\nWe only accept unique numbers sorry.");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid input, give a number within {lowestValue}-{highestValue} \n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid give a number: \n ");
                }
                userinputs[i] = number;

            }
            if (i != userinputs.Length - 1)
            {
                Console.WriteLine("\nAlright, gimme another one:");
            }

        }

        Array.Sort(userinputs); // mostly for binary search, but for easier viewing

    }

    static int[] GenerateWinNums(int[] winningNums, int lowestValue, int highestValue)
    {
        HashSet<int> antidupes = new HashSet<int>(); // same duplication prevention, when dupes are introduced with linear and binary searches, it counts 1 number as multiple matches

        Random rnd = new Random(); //referenced from the assessment example


        for (int i = 0; i < winningNums.Length; i++)
        {
            bool validNum = false;
            int randomNumber;
            while (!validNum)
            {
                randomNumber = rnd.Next(lowestValue, highestValue + 1);
                if (!antidupes.Contains(randomNumber))
                {
                    antidupes.Add(randomNumber);
                    winningNums[i] = randomNumber;
                    validNum = true;
                }
                //winningNums[i] = rnd.Next(lowestValue, highestValue);
            }

        }

        Array.Sort(winningNums); // mostly for binary search, but for easier viewing
        return winningNums;
    }

    static void CheckWinNums(int[] userinputs, int[] winningNumbers)
    {
        int matchingnums = 0;

        foreach (int userNumber in userinputs)
        {

            if (LinearSearch(winningNumbers, userNumber))      // Uncomment this line for linear search method instead
            //if (BinarySearch(winningNumbers, userNumber))
            {
                matchingnums++;
            }

        }
        if (matchingnums == userinputs.Length)
        {
            Console.WriteLine("\nYou won the Jackpot!");
        }
        else if (matchingnums > 0)
        {
            Console.WriteLine($"\nCongratulations! You've matched {matchingnums} numbers!");
        }
        else
        {
            Console.WriteLine("\nSorry, you've lost all your money!");
        }
    }

    static bool BinarySearch(int[] array, int value)  //Both searches winning numbers against the user inputs, returning by bools
    {
        int low = 0;
        int high = array.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (array[mid] == value)
            {
                return true;
            }
            else if (array[mid] > value)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return false;
    }

    static bool LinearSearch(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }
        return false;
    }


}