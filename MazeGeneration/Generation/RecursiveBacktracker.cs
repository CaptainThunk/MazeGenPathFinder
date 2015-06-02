using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneration.Generation
{
    public class RecursiveBacktracker : IMazeGenerator
    {
        private Random r;

        public RecursiveBacktracker(Random rng)
        {
            r = rng;
        }

        public void Generate(Maze maze)
        {
            uint x = (uint)r.Next(maze.MazeWidth);
            uint y = (uint)r.Next(maze.MazeHeight);
            NodePtr ptr = new NodePtr(x, y);
            CarvePassage(ptr, maze);
        }

        private void CarvePassage(NodePtr ptr, Maze maze)
        {
            Node currentNode = maze[ptr];
            currentNode.Visited = true;

            var neighbours = maze.GetNeighbours(ptr);
            var cardinalNeighbours = new NodePtr?[4] {
                                         neighbours[1],
                                         neighbours[3],
                                         neighbours[5],
                                         neighbours[7]
                                     };

            var randomDirection = r.Next(4);
            var temp = cardinalNeighbours[randomDirection];
            cardinalNeighbours[randomDirection] = cardinalNeighbours[0];
            cardinalNeighbours[0] = temp;

            IEnumerable<NodePtr?> shuffled = cardinalNeighbours.OrderBy(n => r.Next()).ToArray();

            foreach (var node in shuffled.Where(n => n.HasValue).Select(n => (NodePtr)n))
            {
                Node nextNode = maze[node];
                if (!nextNode.Visited)
                {
                    int dx = (int)node.x - (int)ptr.x;
                    int dy = (int)node.y - (int)ptr.y;

                    // Ugly wall breaking code
                    if (dx == 0 && dy == -1)
                    {
                        currentNode.Walls ^= (ushort)NodeWall.North;
                        nextNode.Walls ^= (ushort)NodeWall.South;
                    }
                    else if (dx == 1 && dy == 0)
                    {
                        currentNode.Walls ^= (ushort)NodeWall.East;
                        nextNode.Walls ^= (ushort)NodeWall.West;
                    }
                    else if (dx == 0 && dy == 1)
                    {
                        currentNode.Walls ^= (ushort)NodeWall.South;
                        nextNode.Walls ^= (ushort)NodeWall.North;
                    }
                    else if (dx == -1 && dy == 0)
                    {
                        currentNode.Walls ^= (ushort)NodeWall.West;
                        nextNode.Walls ^= (ushort)NodeWall.East;
                    }
                    else { continue; } // Shouldn't ever hit

                    CarvePassage(node, maze);
                }
            }
        }
    }
}
