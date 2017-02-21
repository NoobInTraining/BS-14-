using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS14Library
{
    /// <summary>
    /// Methods concirning the windows console come in here
    /// </summary>
    public class Console
    {
        /// <summary>
        /// Prints "Press any key to exit" on the console
        /// + waits for user input to exit
        /// + prints 3 dots
        /// </summary>
        static public void PrintExitText()
        {
            System.Console.Write("Press anykey to exit");
            printDots();
            System.Console.ReadKey();
        }

        /// <summary>
        /// prints the dots for the exit screen
        /// </summary>
        private static void printDots()
        {
            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    System.Console.Write(".");
                    i++;
                    System.Threading.Thread.Sleep(500);
                    if (i == 3)
                    {
                        System.Console.Write("\b\b\b   \b\b\b");
                        i = 0;
                        System.Threading.Thread.Sleep(500);
                    }        
                        
                }
            });
        }
    }
}
