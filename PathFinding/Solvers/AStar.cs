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
            public float Priority;
            public NodePtr Node;

            public AStarNodePtr(float priority, NodePtr node)
            {
                Priority = priority;
                Node = node;
            }

            public int CompareTo(object obj)
            {
                if (obj != null && obj.GetType() == typeof(AStarNodePtr))
                {
                    var otherNodePtr = (AStarNodePtr)obj;
                    return Priority.CompareTo(otherNodePtr.Priority);
                }
                return 1;
            }
        }

        private Maze maze;
        private MinHeap<AStarNodePtr> priorityQueue; //the frontier
        bool[,] hasVisited;

        public AStar(Maze maze)
        {
            this.maze = maze;
            hasVisited = new bool[maze.Width, maze.Height];
        }

        private void InitSolver()
        {
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    hasVisited[x, y] = false;
                }
            }
        }

        public ISet<NodePtr> Solve(NodePtr startPoint, NodePtr endPoint)
        {
            InitSolver();
            priorityQueue = new MinHeap<AStarNodePtr>(maze.MazeSize);
            priorityQueue.Insert(new AStarNodePtr(0, startPoint));

            var cameFrom = new Dictionary<NodePtr, NodePtr?>();
            var runningCost = new Dictionary<NodePtr, float>();

            cameFrom[startPoint] = null;
            runningCost[startPoint] = 0;

            NodePtr last;
            while (priorityQueue.Size > 0)
            {
                var node = priorityQueue.ExtractMin();
                last = node.Node;
                hasVisited[node.Node.x, node.Node.y] = true;

                if (node.Node.Equals(endPoint))
                {
                    cameFrom[last] = node.Node;
                    break;
                }

                foreach (var next in maze.GetNeighbours(node.Node)
                                            .Where(n => n.HasValue && !hasVisited[n.Value.x, n.Value.y] && maze.IsPassable(node.Node, n.Value))
                                            .Cast<NodePtr>()
                                            .Where(fn => Math.Abs((int)fn.x - node.Node.x + (int)fn.y - node.Node.y) == 1))
                {
                    var newCost = runningCost[node.Node] + 1;
                    
                    if (!runningCost.Keys.Contains(next) || newCost < runningCost[next])
                    {
                        runningCost[next] = newCost;
                        var priority = newCost + GetHeuristic(endPoint, next);
                        cameFrom[next] = node.Node;

                        if (next.Equals(endPoint))
                        {
                            priorityQueue.Empty();
                            break;
                        }
                        else
                        {
                            priorityQueue.Insert(new AStarNodePtr(priority, next));
                        }
                    }
                }
            }

            var results = new HashSet<NodePtr>();
            var resultNode = endPoint;
            while (!resultNode.Equals(startPoint))
            {
                results.Add(resultNode);
                resultNode = cameFrom[resultNode].Value;
            }
            results.Add(startPoint);

            return results;
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
            return (int)Math.Abs((int)a.x - b.x) + (int)Math.Abs((int)a.y - b.y);
        }
    }
}
