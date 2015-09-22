using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneration.Generation
{
    public class PrimsAlgorithm : IMazeGenerator
    {
        private Random r;

        public PrimsAlgorithm(Random rng)
        {
            r = rng;
        }

        public void Generate(Maze maze)
        {
            // Starting position
            uint x = (uint)r.Next(maze.Width);
            uint y = (uint)r.Next(maze.Height);
            NodePtr startingPoint = new NodePtr(x, y);
            maze[startingPoint].Visited = true;

            // Initialise from starting position
            var vistitedNodes = new HashSet<NodePtr>();
            vistitedNodes.Add(startingPoint);

            var frontierNodes = new HashSet<NodePtr>(GetFrontierNodes(maze, startingPoint));

            // Loop through frontier
            while (frontierNodes.Count > 0)
            {
                var frontierNode = frontierNodes.OrderBy(n => r.NextDouble()).First();

                var validFrontierNodeEdges = maze.GetNeighbours(frontierNode)
                                                    .Where(n => n.HasValue && maze[n.Value].Visited)
                                                    .Cast<NodePtr>()
                                                    .Where(n => Math.Abs((int)n.x - frontierNode.x + (int)n.y - frontierNode.y) == 1);
                if (validFrontierNodeEdges.Count() > 0)
                {
                    var nodeToTunnelTo = validFrontierNodeEdges.OrderBy(n => r.NextDouble()).FirstOrDefault();
                    maze.Tunnel(frontierNode, nodeToTunnelTo);
                }

                maze[frontierNode].Visited = true;
                vistitedNodes.Add(frontierNode);
                frontierNodes.UnionWith(GetFrontierNodes(maze, frontierNode));
                frontierNodes.Remove(frontierNode);
            }
        }
        
        /// <summary>
        /// Gets the nodes classes as frontier nodes, which contains only nodes
        /// that are in the cardinal directions outwards from nodes within the maze.
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static IEnumerable<NodePtr> GetFrontierNodes(Maze maze, NodePtr node)
        {
            return maze.GetNeighbours(node).Where(n => n.HasValue && !maze[n.Value].Visited)
                                            .Cast<NodePtr>()
                                            .Where(fn => Math.Abs((int)fn.x - node.x + (int)fn.y - node.y) == 1);
        }
    }
}
