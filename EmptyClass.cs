using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace provod
{
    class provod
    {
        public int minPos = 0;
        public int maxPos = 2;
        public provod(int minPos, int maxPos)
        {
            this.minPos = minPos;
            this.maxPos = maxPos;
        }
        public static void Exp()
        {
            int str = 5;
            int pos = 0;
            int starPos = 0;
            DriveInfo[] dibil = DriveInfo.GetDrives();
            Arrow show = new Arrow();
            provod e = new provod(minPos: 0, maxPos: 2);
            show.ShowDrives(dibil, pos, starPos);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (str == 5 && pos > e.minPos)
                        {
                            pos--;
                            show.ShowDrives(dibil, pos, starPos);
                            starPos--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (str == 5 && pos < e.maxPos)
                        {
                            pos++;
                            show.ShowDrives(dibil, pos, starPos);
                            starPos++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        str = pos;
                        str = 1;
                        show.ShowFiles(dibil, pos, starPos, str);
                        pos = 0;
                        starPos = 0;
                        str = 5;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
