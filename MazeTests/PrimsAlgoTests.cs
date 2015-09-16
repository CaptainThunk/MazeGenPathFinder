using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGeneration;
using MazeGeneration.Generation;

namespace MazeTests
{
    [TestClass]
    public class PrimsAlgoTests
    {
        private PrimsAlgorithm generator;
        private Random rng;
        private Maze maze;
        private const int _mazeWidth = 5;
        private const int _mazeHeight = 5;

        [TestInitialize]
        public void TestInit()
        {
            rng = new Random(1001); // Repeatable tests
            maze = new Maze(_mazeWidth, _mazeHeight);
            generator = new PrimsAlgorithm(rng);
        }

        [TestMethod]
        public void PrimsGeneratorHasProperlyInitialised()
        {
            Assert.IsInstanceOfType(generator, typeof(PrimsAlgorithm));
        }

        [TestMethod]
        public void PrimsGeneratorWillGenerate()
        {
            generator.Generate(maze);
            Assert.IsNotNull(maze);
        }

        [TestMethod]
        public void PrimsGeneratorModifiesMaze()
        {
            Maze freshMaze = new Maze(_mazeWidth, _mazeHeight);

            generator.Generate(maze);

            var nodeCount = freshMaze.MazeSize;
            var nodesUntouched = 0;
            for (int y = 0; y < _mazeHeight; y++)
            {
                for (int x = 0; x < _mazeWidth; x++)
                {
                    if (maze.Grid[x, y].Equals(freshMaze.Grid[x, y]))
                    {
                        nodesUntouched++;
                    }
                }
            }

            Assert.AreNotEqual(nodeCount, nodesUntouched);
        }
    }
}
