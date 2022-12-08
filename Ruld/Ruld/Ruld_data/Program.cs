using System;
using System.IO;

namespace Ruld
{
    class Program
    {
        static bool isWin;

        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
                Console.WriteLine();

            var playerIsNotOne = true;
            string[] map;
            char[] Line;
            string path = "";
            Console.Title = "Ruld";

            Console.WriteLine("Enter the map's name...");

        AskAgain:
            var mapsname = Console.ReadLine();

            try
            {
                path = Path.GetFullPath(@$"..\maps\{mapsname}.txt");
                map = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Can't find the \'{mapsname}\'");
                goto AskAgain;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Can't find the \'{mapsname}\'");
                goto AskAgain;
            }

            var playersPosition = new int[2];

            var x = map[0].Length;
            var y = map.Length;

            char[,] Blocks = new char[x, y];

            for (int i = 0; i < y; i++)
            {
                try 
                { 
                    Line = map[i].ToCharArray();
                    x = Line.Length;
                        
                    for (int j = 0; j < x; j++)
                    {
                        switch (Line[j])
                        {
                            case 'w':
                                Console.Write('■');
                                break;

                            case 'a':
                                Console.Write('ㅤ');
                                break;

                            case 'p':
                                Console.Write('●');
                                playersPosition[0] = i;
                                playersPosition[1] = j;

                                if (playerIsNotOne)
                                {
                                    playerIsNotOne = false;
                                }
                                else
                                {
                                    Console.WriteLine("Player is not one");
                                    goto AskAgain;
                                }

                                break;

                            case 's':
                                Console.Write('★');
                                break;

                            default:
                                Console.Write('ㅤ');
                                break;
                        }
    
                        Blocks[i, j] = Line[j];
                    }
                    Console.WriteLine();
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("\nIf map's form is not rectangular, please fix the map file.");
                    goto AskAgain;
                }
            }

            while (!isWin)
            {
                var ssw = new StringWriter();
                var move = Console.Read();

                switch (move)
                {
                    case 'd':
                        if (Blocks[playersPosition[0] + 1, playersPosition[1]] == 'a')
                        {
                            var temp = Blocks[playersPosition[0] + 1, playersPosition[1]];
                            Blocks[playersPosition[0] + 1, playersPosition[1]] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = temp;

                            playersPosition[0]++;
                        }
                        else if (Blocks[playersPosition[0] + 1, playersPosition[1]] == 's')
                        {
                            Blocks[playersPosition[0] + 1, playersPosition[1]] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = 'a';
                            Star();
                        }
                        break;

                    case 'r':
                        if (Blocks[playersPosition[0], playersPosition[1] + 1] == 'a')
                        {
                            var temp = Blocks[playersPosition[0], playersPosition[1] + 1];
                            Blocks[playersPosition[0], playersPosition[1] + 1] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = temp;

                            playersPosition[1]++;
                        }
                        else if (Blocks[playersPosition[0], playersPosition[1] + 1] == 's')
                        {
                            Blocks[playersPosition[0], playersPosition[1] + 1] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = 'a';
                            Star();
                        }
                        break;

                    case 'u':
                        if (Blocks[playersPosition[0] - 1, playersPosition[1]] == 'a')
                        {
                            var temp = Blocks[playersPosition[0] - 1, playersPosition[1]];
                            Blocks[playersPosition[0] - 1, playersPosition[1]] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = temp;

                            playersPosition[0]--;
                        }
                        else if (Blocks[playersPosition[0] - 1, playersPosition[1]] == 's')
                        {
                            Blocks[playersPosition[0] - 1, playersPosition[1]] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = 'a';
                            Star();
                        }
                        break;

                    case 'l':
                        if (Blocks[playersPosition[0], playersPosition[1] - 1] == 'a')
                        {
                            var temp = Blocks[playersPosition[0], playersPosition[1] - 1];
                            Blocks[playersPosition[0], playersPosition[1] - 1] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = temp;

                            playersPosition[1]--;
                        }
                        else if (Blocks[playersPosition[0], playersPosition[1] - 1] == 's')
                        {
                            Blocks[playersPosition[0], playersPosition[1] - 1] = Blocks[playersPosition[0], playersPosition[1]];
                            Blocks[playersPosition[0], playersPosition[1]] = 'a';
                            Star();
                        }

                        
                        break;

                    default:
                        break;
                }

                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        switch (Blocks[i, j])
                        {
                            case 'w':
                                ssw.Write('■');
                                break;

                            case 'a':
                                ssw.Write('ㅤ');
                                break;

                            case 'p':
                                ssw.Write('●');
                                break;

                            case 's':
                                ssw.Write('★');
                                break;

                            default:
                                ssw.Write('ㅤ');
                                break;
                        }
                    }
                    ssw.WriteLine();
                }

                Console.WriteLine(ssw);
            }

            Console.WriteLine("You Win!\n");
            Console.WriteLine("Try again?");
            string yn = Console.ReadLine();

            if (yn == "Y" || yn == "y")
            {
                goto AskAgain;
            }

        }//Main

        static void Star()
        {
            isWin = true;
        }//Star
    }//program
}//namespace
