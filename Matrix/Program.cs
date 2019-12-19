using System;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int windowWidth = Console.WindowWidth;
            Thread[] threads;
            Console.CursorVisible = false;

            int quant = 2;
            threads = new Thread[windowWidth / quant];
            int w = windowWidth / quant;
            if ((int)w >= windowWidth)
                w = windowWidth - 1;

            for (int i = 0; i < w; i++)
            {
                threads[i] = new Thread(Change);
                threads[i].Start(quant * i);
            }
        }

        static object block = new object();
        static Random rand = new Random();
        static char symbol;

        static void Change(object w)
        {
            int startOfhange;
            int windowHeigth = Console.WindowHeight;

            while (true)
            {
                int interval;
                Console.CursorVisible = false;
                startOfhange = 0;//rand.Next(0, windowHeigth);
                int length = rand.Next(0, windowHeigth);
                interval = rand.Next(0, 50);
                if (length + startOfhange > windowHeigth)
                    length = windowHeigth - startOfhange - 1;

                for (int i = startOfhange; i < 30; i++)
                {
                    Thread.Sleep(interval);
                    lock (block)
                    {
                        OutChar(ConsoleColor.White, (int)w, i);

                        if (i > startOfhange)
                            OutChar(ConsoleColor.Green, (int)w, i - 1);

                    }
                    for (int j = i - 2; j >= startOfhange; j--)
                    {
                        Thread.Sleep(interval);
                        lock (block)
                            OutChar(ConsoleColor.DarkGreen, (int)w, j);
                    }
                }

                lock (block)
                    for (int i = startOfhange; i < 30; i++)
                        OutChar(ConsoleColor.Black, (int)w, i);
            }
        }
        static void OutChar(ConsoleColor consoleColor, int X, int Y)
        {
            symbol = (char)rand.Next(0x0021, 0x007E);
            Console.ForegroundColor = consoleColor;
            Console.SetCursorPosition(X, Y);
            Console.Write(symbol);
        }

    }
}

