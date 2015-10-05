using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Solvers;
using MazeGeneration;
using MazeGeneration.Generation;

namespace PathFindingTests
{
    [TestClass]
    public class AStarTests
    {
        private Random rng;
        private Maze maze;
        private IMazeGenerator generator;
        private const int mazeWidth = 5;
        private const int mazeHeight = 5;
        private NodePtr startPoint = new NodePtr(1, 1);
        private NodePtr endPoint = new NodePtr(4, 2);

        private AStar solver;

        [TestInitialize]
        public void Init()
        {
            maze = new Maze(mazeWidth, mazeHeight);
            rng = new Random(1001);
            generator = new PrimsAlgorithm(rng);
            generator.Generate(maze);

            solver = new AStar(maze);
        }
        
        [TestMethod]
        public void AStarSolverIsConstructed()
        {
            Assert.IsInstanceOfType(solver, typeof(AStar));
        }

        [TestMethod]
        public void AStarSolverSolvesForGivenMaze()
        {
            var actualSolution = new NodePtr[]
            {
                new NodePtr(1, 1),
                new NodePtr(4, 2)
            };

            var solution = solver.Solve(startPoint, endPoint);

            //Assert.AreEqual(actualSolution.Length, solution.Count);

            foreach (var node in actualSolution)
            {
                if (!solution.Contains(node)) Assert.Fail();
            }
        }
    }
}
