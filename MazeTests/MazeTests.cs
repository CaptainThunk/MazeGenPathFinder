using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGeneration;
using System.Linq;

namespace MazeTests
{
    [TestClass]
    public class MazeTests
    {
        private Maze maze;
        private int mazeHeight = 20;
        private int mazeWidth = 20;

        [TestInitialize]
        public void Init()
        {
            maze = new Maze();
            maze.Create(mazeHeight, mazeWidth);
        }

        [TestMethod]
        public void CreateMaze()
        {
            Assert.IsInstanceOfType(maze, typeof(Maze));
        }

        [TestMethod]
        public void MazeHasStorageOfSufficientSize()
        {
            Assert.IsTrue(maze.MazeSize == (mazeHeight * mazeWidth));
        }

        [TestMethod]
        public void MazeHasBeenInitialised()
        {
            foreach (int node in maze.Grid)
            {
                Assert.AreEqual(0x1111, node);
            }
        }

        [TestMethod]
        public void GetNeighboursFromMiddle()
        {
            uint x = (uint)Math.Floor(mazeWidth / 2.0);
            uint y = (uint)Math.Floor(mazeHeight / 2.0);
            NodePtr ptr = new NodePtr(x, y);
            var neighbours = maze.GetNeighbours(ptr);

            Assert.AreEqual(9, neighbours.Length); // Should always return 9 positions
            foreach (var node in neighbours)
            {
                /*
                 * No neighbour should be inaccessable
                 * e.g. out of bounds
                 */
                if (node != null)
                {
                    Assert.IsInstanceOfType(maze[(NodePtr)node], typeof(short));
                }
            }
            long previousNodeCoord = 0;
            int nullEntries = 0;
            foreach (var node in neighbours)
            {
                // Ensure that results are in row-major order
                if (node.HasValue)
                {
                    long nodeCoord = node.Value.x + ((node.Value.y - 1) * mazeWidth);
                    if (previousNodeCoord > 0)
                    {
                        long nodeDiff = nodeCoord - previousNodeCoord;
                        // TODO: Some assertion
                        // Diff needs to be within 1+null count, Width-2+null count
                        Assert.IsTrue(nodeDiff == 1 + nullEntries || nodeDiff == mazeWidth - 2 + nullEntries);
                    }
                    previousNodeCoord = nodeCoord;
                    nullEntries = 0;
                }
                else
                {
                    nullEntries++;
                }
            }
        }

        [TestMethod]
        public void GetNeighoursFromTopLeftExtreme()
        {
            NodePtr ptr = new NodePtr(0, 0);
            var neighbours = maze.GetNeighbours(ptr);

            /*
             *  x x x
             *  x o +
             *  x + +
             */
            Assert.AreEqual(9, neighbours.Length); // Should still return 9 positions
            Assert.AreEqual(3, neighbours.Count(n => n.HasValue)); // Should only return 3 valid positions
        }

        [TestMethod]
        public void GetNeighboursFromBottomRightExtreme()
        {
            NodePtr ptr = new NodePtr((uint)mazeWidth - 1, (uint)mazeHeight - 1);
            var neighbours = maze.GetNeighbours(ptr);

            /*
             *  + + x
             *  + o x
             *  x x x
             */
            Assert.AreEqual(9, neighbours.Length); // Should still return 9 positions
            Assert.AreEqual(3, neighbours.Count(n => n.HasValue)); // Should only return 3 valid positions
        }

        [TestMethod]
        public void NodeIsMarkedAsUnvisted()
        {
            Assert.IsFalse(maze.IsNodeVisited(new NodePtr(0, 0)));
        }

        [TestMethod]
        public void NodeIsMarkedAsVisited()
        {
            // Visit node and remove some walls
            NodePtr ptr = new NodePtr((uint)Math.Floor(mazeWidth / 2.0),
                                      (uint)Math.Floor(mazeWidth / 2.0));
            maze[ptr] ^= (short)(NodeWall.East | NodeWall.West);
            Assert.IsTrue(maze.IsNodeVisited(ptr));
            // Reset storage for future tests
            maze[ptr] = 0x1111;
        }
    }
}
