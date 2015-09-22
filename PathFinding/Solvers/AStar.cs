using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneration;

namespace PathFinding.Solvers
{
    public class AStar : ISolver
    {
        private Maze maze;

        public AStar(Maze maze)
        {
            this.maze = maze;
        }

        public ISet<NodePtr> Solve(NodePtr startPoint, NodePtr endPoint)
        {
            throw new NotImplementedException();
        }
    }
}
