using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneration;

namespace PathFinding.Solvers
{
    public class RecursiveSolver : ISolver
    {
        private Maze maze;
        private NodePtr start, end;
        private bool[,] hasVisted;
        private bool[,] path;

        public RecursiveSolver(Maze maze)
        {
            this.maze = maze;
            hasVisted = new bool[maze.Width, maze.Height];
            path = new bool[maze.Width, maze.Height];

            // Initialise
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    hasVisted[x, y] = false;
                    path[x, y] = false;
                }
            }
        }

        public ISet<NodePtr> Solve(NodePtr startPoint, NodePtr endPoint)
        {
            start = startPoint;
            end = endPoint;

            bool b = Solve(start.x, start.y);

            if (!b)
            {
                return null;
            }

            var resultSet = new HashSet<NodePtr>();
            for (uint y = 0; y < maze.Height; y++)
            {
                for (uint x = 0; x < maze.Width; x++)
                {
                    if (path[x, y])
                    {
                        resultSet.Add(new NodePtr(x, y));
                    }
                }
            }
            return resultSet;
        }

        private bool Solve(uint x, uint y)
        {
            if ((x == end.x) && (y == end.y))
            {
                path[x, y] = true;
                return true;
            }
            if (hasVisted[x, y]) return false;
            hasVisted[x, y] = true;

            if (x != 0)
            {
                if (Solve(x - 1, y))
                {
                    path[x, y] = true;
                    return true;
                }
            }
            if (x != maze.Width - 1)
            {
                if (Solve(x + 1, y))
                {
                    path[x, y] = true;
                    return true;
                }
            }
            if (y != 0)
            {
                if (Solve(x, y - 1))
                {
                    path[x, y] = true;
                    return true;
                }
            }
            if (y != maze.Height - 1)
            {
                if (Solve(x, y + 1))
                {
                    path[x, y] = true;
                    return true;
                }
            }

            return false;
        }
    }
}
