using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Solvers;
using MazeGeneration;

namespace PathFindingTests
{
    [TestClass]
    public class AStarTests
    {
        private AStar solver;
        private Maze maze;
        private int mazeWidth = 5, mazeHeight = 5;

        [TestInitialize]
        public void Init()
        {
            maze = new Maze(mazeWidth, mazeHeight);
            solver = new AStar(maze);
        }

        [TestMethod]
        public void AStarSolverIsConstructed()
        {
            Assert.IsInstanceOfType(solver, typeof(AStar));
        }
    }
}
