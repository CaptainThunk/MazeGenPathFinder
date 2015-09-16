using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Solvers;
using MazeGeneration;
using MazeGeneration.Generation;

namespace PathFindingTests
{
    [TestClass]
    public class RecursiveSolverTests
    {
        private Random rng;
        private Maze maze;
        private IMazeGenerator generator;
        private const int mazeWidth = 5;
        private const int mazeHeight = 5;
        private NodePtr startPoint = new NodePtr(1, 1);
        private NodePtr endPoint = new NodePtr(4, 2);

        private RecursiveSolver solver;

        [TestInitialize]
        public void Init()
        {
            maze = new Maze(mazeWidth, mazeHeight);
            rng = new Random(1001);
            generator = new PrimsAlgorithm(rng);
            generator.Generate(maze);

            solver = new RecursiveSolver(maze);
        }

        [TestMethod]
        public void RecursiveSolverInitialises()
        {
            Assert.IsInstanceOfType(solver, typeof(RecursiveSolver));
        }

        [TestMethod]
        public void RecursiveSolverSolvesForGivenMaze()
        {
            solver.Solve(startPoint, endPoint);
        }
    }
}
