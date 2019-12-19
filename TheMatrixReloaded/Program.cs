//3. Расширьте задание 2, так, чтобы в одном столбце одновременно могло быть две цепочки символов.

using System;
using System.Threading;

namespace TheMatrixReloaded
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
            int w = windowWidth / quant-1;
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
            int windowHeigth = Console.WindowHeight;
            int startOfhange = 0;//rand.Next(0, windowHeigth);
            int length = rand.Next(4, windowHeigth);
            while (true)
            {
                int interval;
                Console.CursorVisible = false;
                interval = 40 * rand.Next(0, 100) / length;
                ShowLine(length, startOfhange, (int)w, interval);

                int innerStartOfhange = length + rand.Next(0, windowHeigth - length - 1);
                int innerLength = rand.Next(3, windowHeigth - startOfhange);
                var thread = new Thread(
              () => ShowLine(innerLength, innerStartOfhange, (int)w, interval));
                thread.Start();

            }
        }
        static void ShowLine(int length, int startOfhange, object w, int interval)
        {
     

                for (int i = startOfhange; i < length; i++)
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
                    for (int i = startOfhange; i < length; i++)
                        OutChar(ConsoleColor.Black, (int)w, i);
            
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
