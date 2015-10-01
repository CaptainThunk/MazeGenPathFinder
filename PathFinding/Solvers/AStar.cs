using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneration;
using PathFinding.Common;

namespace PathFinding.Solvers
{
    public class AStar : ISolver
    {
        private class AStarNodePtr : IComparable
        {
            public int Priority;
            public NodePtr Node;

            public AStarNodePtr(int priority, NodePtr node)
            {
                Priority = priority;
                Node = node;
            }

            public int CompareTo(object obj)
            {
                if (obj.GetType() == typeof(AStarNodePtr))
                {
                    var otherNodePtr = (AStarNodePtr)obj;
                    return Priority.CompareTo(otherNodePtr.Priority);
                }
                return 1; 
            }
        }

        private Maze maze;
        private MaxHeap<AStarNodePtr> priorityQueue; //the frontier

        public AStar(Maze maze)
        {
            this.maze = maze;
        }

        public ISet<NodePtr> Solve(NodePtr startPoint, NodePtr endPoint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the heuristic which A* will use to make
        /// decisions on which part of the frontier to 
        /// explore; uses the manhattan distance.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The manhattan distance between two points</returns>
        private int GetHeuristic(NodePtr a, NodePtr b)
        {
            return (int)Math.Abs(a.x - b.x) + (int)Math.Abs(a.y - b.y);
        }
    }
}
