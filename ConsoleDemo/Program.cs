using MazeGeneration;
using MazeGeneration.Generation;
using PathFinding.Solvers;
using System;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleDemo
{
    class Program
    {
        static Maze maze;
        static int mazeWidth = 5, mazeHeight = 5;
        static int offset = 3;
        static char[,] display;
        static Random r;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //r = new Random((int)DateTime.Now.Ticks);
            r = new Random(1001);
            maze = new Maze(mazeWidth, mazeHeight);
            display = new char[mazeWidth * offset, mazeHeight * offset];

            Console.WriteLine("Choose a type of maze to generate:\n");
            Console.WriteLine("1: Recursive Backtracker");
            Console.WriteLine("2: Randomised Prim's Algorithm");
            var mazeOption = Console.ReadKey().Key;

            IMazeGenerator generator;

            switch (mazeOption)
            {
                case ConsoleKey.D2:
                    generator = new PrimsAlgorithm(r);
                    break;
                case ConsoleKey.D1:
                default:
                    generator = new RecursiveBacktracker(r);
                    break;
            }
            Console.Clear();

            Console.WriteLine("Choose a type of solver, if any:\n");
            Console.WriteLine("0: No solver");
            Console.WriteLine("1: Recursive Solver");
            var solverOption = Console.ReadKey().Key;

            ISolver solver;

            switch (solverOption)
            {
                case ConsoleKey.D1:
                    solver = new RecursiveSolver(maze);
                    break;
                default:
                    solver = null;
                    break;
            }

            while (true)
            {
                maze.InitialiseNodes();
                generator.Generate(maze);
                Console.Clear();

                for (int h = 0; h < mazeHeight; h++)
                {
                    for (int w = 0; w < mazeWidth; w++)
                    {
                        WriteNodeChar(w, h, maze[(uint)w, (uint)h].Walls);
                    }
                }

                if (solver != null)
                {
                    Random r = new Random();
                    NodePtr startPoint = new NodePtr((uint)maze.Width + 1, (uint)maze.Height + 1); // Purposefully not valid
                    NodePtr endPoint = new NodePtr((uint)maze.Width + 1, (uint)maze.Height + 1);
                    while (true)
                    {
                        //TODO: Probably refactor
                        var startPointIsValid = maze.IsPointValid(startPoint);
                        var endPointIsValid = maze.IsPointValid(endPoint);
                        if (startPointIsValid && endPointIsValid) break;

                        if (!startPointIsValid) startPoint = new NodePtr((uint)r.Next(maze.Width), (uint)r.Next(maze.Height));
                        if (!endPointIsValid) endPoint = new NodePtr((uint)r.Next(maze.Width), (uint)r.Next(maze.Height));
                    }
                    var solution = solver.Solve(startPoint, endPoint);

                    WriteSolution(startPoint, endPoint, solution);
                    Console.WriteLine(string.Format("Solving for: Start - {0}x, {1}y; End - {2}x, {3}y",
                                                    startPoint.x, startPoint.y,
                                                    endPoint.x, endPoint.y));
                }

                DrawDisplay();
                Console.WriteLine();
                Console.WriteLine("Press any key to regenerate, or Escape to exit....");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
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

        private static void WriteNodeChar(int x, int y, ushort node)
        {
            int xOffset = x * offset;
            int yOffset = y * offset;
            for (int h = yOffset; h < yOffset + offset; h++)
            {
                for (int w = xOffset; w < xOffset + offset; w++)
                {
                    // Ugly drawing code
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
        private static void WriteSolution(NodePtr start, NodePtr end, ISet<NodePtr> solution)
        {
            foreach (var node in solution)
            {
                var xOffset = node.x * offset + 1;
                var yOffset = node.y * offset + 1;

                if (node.Equals(start) || node.Equals(end))
                {
                    display[xOffset, yOffset] = 'O';
                }
                else
                {
                    display[xOffset, yOffset] = '.';
                }
            }
        }
    }
}
