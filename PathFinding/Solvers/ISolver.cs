using MazeGeneration;
using System.Collections.Generic;

namespace PathFinding.Solvers
{
    public interface ISolver
    {
        ISet<NodePtr> Solve(NodePtr startPoint, NodePtr endPoint);
    }
}