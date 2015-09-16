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
            throw new NotImplementedException();
        }
    }
}
