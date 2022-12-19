using System.Diagnostics;

class Arrow
{
    public void ShowDrives(DriveInfo[] drives, int position, int old_position)
    {
        int i = 0;
        string arrow = "->";
        foreach (DriveInfo drive in drives)
        {
            Console.SetCursorPosition(0, old_position);
            Console.WriteLine(arrow.Replace("->", "  "));
            Console.SetCursorPosition(0, position);
            Console.WriteLine(arrow);
            Console.SetCursorPosition(3, i);
            Console.WriteLine($"{drive.Name}\tдоступно {drive.TotalFreeSpace / 1073741824} Гб из {drive.TotalSize / 1073741824} Гб");
            i++;
        }
    }
    public void ShowFiles(DriveInfo[] drives, int position, int old_position, int page)
    {

        int b = 0;
        bool exit = true;
        string arrow = "->";
        Console.SetCursorPosition(0, old_position);
        Console.WriteLine(arrow.Replace("->", "  "));
        Console.SetCursorPosition(0, position);
        Console.WriteLine(arrow);
        string[] allFolders = Directory.GetDirectories(drives[old_position].Name);
        string[] allFiles = Directory.GetFiles(drives[old_position].Name);
        List<string> all = new List<string>();

        foreach (string folder in allFolders)
        {
            all.Add(folder);
            Console.SetCursorPosition(3, b);
            string fileName = Path.GetFileName(folder);
            DateTime fileDate = File.GetCreationTime(folder);
            Console.WriteLine(fileName);
            Console.SetCursorPosition(30, b);
            Console.WriteLine(fileDate);
            b++;
        }

        foreach (string file in allFiles)
        {
            all.Add(file);
            Console.SetCursorPosition(3, b);
            string fileName = Path.GetFileName(file);
            DateTime fileDate = File.GetCreationTime(file);
            string fileExtension = Path.GetExtension(file);
            Console.WriteLine(fileName);
            Console.SetCursorPosition(30, b);
            Console.WriteLine(fileDate);
            Console.SetCursorPosition(55, b);
            Console.WriteLine(fileExtension);
            b++;
        }


        while (exit)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (page == 1 && position > 0)
                    {
                        position--;
                        Console.SetCursorPosition(0, old_position);
                        Console.WriteLine(arrow.Replace("->", "  "));
                        Console.SetCursorPosition(0, position);
                        Console.WriteLine(arrow);
                        old_position--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (page == 1 && position < allFiles.Length + allFolders.Length - 1)
                    {
                        position++;
                        Console.SetCursorPosition(0, old_position);
                        Console.WriteLine(arrow.Replace("->", "  "));
                        Console.SetCursorPosition(0, position);
                        Console.WriteLine(arrow);
                        old_position++;
                    }
                    break;
                case ConsoleKey.Enter:
                    page = 2;
                    if (page == 2)
                    {
                        all = ShowOrOpen(old_position, position, all);
                        old_position = 0;
                        position = 0;

                        while (key.Key != ConsoleKey.Escape)
                        {
                            key = Console.ReadKey();
                            switch (key.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    if (position > 0)
                                    {
                                        position--;
                                        Console.SetCursorPosition(0, old_position);
                                        Console.WriteLine(arrow.Replace("->", "  "));
                                        Console.SetCursorPosition(0, position);
                                        Console.WriteLine(arrow);
                                        old_position--;
                                    }
                                    break;
                                case ConsoleKey.DownArrow:
                                    if (position < all.Count - 1)
                                    {
                                        position++;
                                        Console.SetCursorPosition(0, old_position);
                                        Console.WriteLine(arrow.Replace("->", "  "));
                                        Console.SetCursorPosition(0, position);
                                        Console.WriteLine(arrow);
                                        old_position++;
                                    }
                                    break;
                                case ConsoleKey.Enter:
                                    page = 3;
                                    all = ShowOrOpen(old_position, position, all);
                                    old_position = 0;
                                    position = 0;
                                    break;
                                case ConsoleKey.Escape:
                                    page = 5;
                                    Console.Clear();
                                    exit = false;
                                    ShowDrives(drives, 0, 0);
                                    break;
                            }
                        }
                    }
                    break;
                case ConsoleKey.Escape:
                    page = 5;
                    Console.Clear();
                    exit = false;
                    ShowDrives(drives, 0, 0);
                    break;
            }
        }
    }

    private static List<string> ShowOrOpen(int old_position, int position, List<string> all)
    {
        Console.Clear();
        string arrow = "->";
        int b = 0;
        List<string> result = new List<string>();
        string Extension = Path.GetExtension(all[old_position]);
        string path = Path.GetFullPath(all[old_position]);

        if (Extension != "")
        {
            Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
        }
        else
        {
            string[] folders = Directory.GetDirectories(all[old_position]);
            string[] files = Directory.GetFiles(all[old_position]);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(arrow);

            foreach (string file in folders)
            {
                Console.SetCursorPosition(3, b);
                string fileName = Path.GetFileName(file);
                DateTime fileDate = File.GetCreationTime(file);
                Console.WriteLine(fileName);
                Console.SetCursorPosition(50, b);
                Console.WriteLine(fileDate);
                b++;
                result.Add(file);
            }

            foreach (string file in files)
            {
                Console.SetCursorPosition(3, b);
                string fileName = Path.GetFileName(file);
                DateTime fileDate = File.GetCreationTime(file);
                string fileExtension = Path.GetExtension(file);
                Console.WriteLine(fileName);
                Console.SetCursorPosition(50, b);
                Console.WriteLine(fileDate);
                Console.SetCursorPosition(75, b);
                Console.WriteLine(fileExtension);
                b++;
                result.Add(file);
            }
        }
        all.Clear();
        all = result;
        return all;
    }
}
