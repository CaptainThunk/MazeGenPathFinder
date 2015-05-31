using MazeGeneration;
using System;

namespace ConsoleDemo
{
    class Program
    {
        static Maze maze;
        static int mazeWidth = 20, mazeHeight = 20;
        static int offset = 3;
        static char[,] display;
        static Random r;
        static Array directions;

        static void Main(string[] args)
        {
            r = new Random();
            maze = new Maze(mazeWidth, mazeHeight);
            directions = Enum.GetValues(typeof(NodeWall));
            display = new char[mazeWidth * offset, mazeHeight * offset];

            while (true)
            {
                for (int h = 0; h < mazeHeight; h++)
                {
                    for (int w = 0; w < mazeWidth; w++)
                    {
                        WriteNodeChar(w, h, maze[(uint)w, (uint)h]);
                    }
                }

                RandomUpdate();
                Console.Clear();
                DrawDisplay();
                Console.ReadKey();
            }
        }

        private static void RandomUpdate()
        {
            int x = r.Next(0, mazeWidth - 1);
            int y = r.Next(0, mazeHeight - 1);
            NodeWall cutThroughWall = (NodeWall)directions.GetValue(r.Next(directions.Length));

            maze[(uint)x, (uint)y] ^= (short)cutThroughWall;

            var neighbours = maze.GetNeighbours((uint)x, (uint)y);

            switch (cutThroughWall)
            {
                case NodeWall.North:
                    if (neighbours[1].HasValue)
                    {
                        maze[neighbours[1].Value] ^= (short)NodeWall.South;
                    }
                    break;
                case NodeWall.East:
                    if (neighbours[5].HasValue)
                    {
                        maze[neighbours[5].Value] ^= (short)NodeWall.West;
                    }
                    break;
                case NodeWall.South:
                    if (neighbours[7].HasValue)
                    {
                        maze[neighbours[7].Value] ^= (short)NodeWall.North;
                    }
                    break;
                case NodeWall.West:
                    if (neighbours[3].HasValue)
                    {
                        maze[neighbours[3].Value] ^= (short)NodeWall.East;
                    }
                    break;
                default:
                    break;
            }
        }

        private static void DrawDisplay()
        {
            for (int y = 0; y < mazeHeight * offset; y++)
            {
                for (int x = 0; x < mazeWidth * offset; x++)
                {
                    Console.Write(display[x, y]);
                }
                Console.WriteLine();
            }
        }

        private static void WriteNodeChar(int x, int y, short node)
        {
            int xOffset = x * offset;
            int yOffset = y * offset;
            for (int h = yOffset; h < yOffset + offset; h++)
            {
                for (int w = xOffset; w < xOffset + offset; w++)
                {
                    if (w == xOffset + 1 && h == yOffset + 1)
                    {
                        display[w, h] = ' ';
                    }
                    else if (w == xOffset + 1 && h == yOffset)
                    {
                        display[w, h] = (node & (short)NodeWall.North) == (short)NodeWall.North ? 'X' : ' ';
                    }
                    else if (w == xOffset && h == yOffset + 1)
                    {
                        display[w, h] = (node & (short)NodeWall.West) == (short)NodeWall.West ? 'X' : ' ';
                    }
                    else if (w == xOffset + 2 && h == yOffset + 1)
                    {
                        display[w, h] = (node & (short)NodeWall.East) == (short)NodeWall.East ? 'X' : ' ';
                    }
                    else if (w == xOffset + 1 && h == yOffset + 2)
                    {
                        display[w, h] = (node & (short)NodeWall.South) == (short)NodeWall.South ? 'X' : ' ';
                    }
                    else
                    {
                        display[w, h] = 'X';
                    }

                }
            }
        }
    }
}
