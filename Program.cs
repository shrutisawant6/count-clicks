using System.Runtime.InteropServices;

namespace CountClicks
{
    internal partial class Program
    {
        [LibraryImport("user32.dll")]
        public static partial short GetAsyncKeyState(int key);

        //Virtual Key Code
        const int VK_LBUTTON = 0x01; //left mouse button
        const int VK_RBUTTON = 0x02; //right mouse button

        static int clickCount = 0;

        static void Main(string[] args)
        {
            try
            {
                InitialTextFormatting();

                //monitor mouse clicks
                while (true)
                {
                    //count left click
                    if ((GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0)
                        clickCount++;

                    //count right click
                    if ((GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0)
                        clickCount++;

                    //press E to quit
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.E)
                    {
                        ResultTextFormatting(clickCount);
                        break;
                    }

                    //delay added
                    Thread.Sleep(100); 
                }
            }
            catch
            {
                ErrorTextFormatting();
            }

            Console.ReadLine();
        }

        static void InitialTextFormatting()
        {
            Console.WriteLine();
            SetColor(ConsoleColor.Yellow, ConsoleColor.DarkYellow);
            Console.WriteLine("┌────────────────────────────────────────────────┐");
            Console.WriteLine("│    Mouse click count process is been started.  │");
            Console.WriteLine("│    Press 'E' to stop and get the count.        │");
            Console.WriteLine("└────────────────────────────────────────────────┘");
            Console.WriteLine();
            Console.ResetColor();
        }

        static void ResultTextFormatting(int count)
        {
            string text = $"│    Total count = {count}";
            int desiredLength = 50;
            string formattedText = AddSpaces(text, desiredLength);
            SetColor(ConsoleColor.Green, ConsoleColor.White);
            Console.WriteLine("┌────────────────────────────────────────────────┐");
            Console.WriteLine(formattedText);
            Console.WriteLine("└────────────────────────────────────────────────┘");
            Console.ResetColor();
        }

        static void ErrorTextFormatting()
        {
            SetColor(ConsoleColor.Red, ConsoleColor.White);
            Console.WriteLine("┌────────────────────────────────────────────────┐");
            Console.WriteLine("│    Error has occured, rerun the application.   │");
            Console.WriteLine("└────────────────────────────────────────────────┘");
            Console.ResetColor();
        }

        static void SetColor(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }

        static string AddSpaces(string input, int desiredLength)
        {
            int spacesToAdd = Math.Max(0, desiredLength - input.Length) - 1;
            string spacedString = input + new string(' ', spacesToAdd);
            return spacedString + "│";
        }
    }
}
