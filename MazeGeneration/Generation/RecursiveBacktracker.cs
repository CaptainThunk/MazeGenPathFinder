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
            uint x = (uint)r.Next(maze.Width);
            uint y = (uint)r.Next(maze.Height);
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

            IEnumerable<NodePtr?> shuffled = cardinalNeighbours.OrderBy(n => r.Next()).ToArray();

            foreach (var node in shuffled.Where(n => n.HasValue).Select(n => (NodePtr)n))
            {
                Node nextNode = maze[node];
                if (!nextNode.Visited)
                {
                    maze.Tunnel(node, ptr);
                    CarvePassage(node, maze);
                }
            }
        }
    }
}
