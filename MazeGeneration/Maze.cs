using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeGeneration
{
    public class Maze
    {
        private short[,] _storage;
        public int MazeHeight;
        public int MazeWidth;

        public Maze() { }

        public Maze(int mazeWidth, int mazeHeight)
        {
            this.Create(mazeWidth, mazeHeight);
        }

        public void Create(int mazeWidth, int mazeHeight)
        {
            MazeHeight = mazeHeight;
            MazeWidth = mazeWidth;
            this._storage = new short[mazeWidth, mazeHeight];
            InitialiseNodes();
        }

        private void InitialiseNodes()
        {
            short initialValue = (short)(NodeWall.North | NodeWall.East | NodeWall.South | NodeWall.West);
            for (int i = 0; i < MazeWidth; i++)
            {
                for (int h = 0; h < MazeHeight; h++)
                {
                    _storage[i, h] = initialValue;
                }
            }
        }

        public NodePtr?[] GetNeighbours(NodePtr node)
        {
            return GetNeighbours(node.x, node.y);
        }

        public NodePtr?[] GetNeighbours(uint x, uint y)
        {
            NodePtr?[] neighbours = new NodePtr?[9];
            int arrayPosition = 0;
            for (int h = ((int)y - 1); h <= y + 1; h++) // Column
            {
                for (int i = ((int)x - 1); i <= x + 1; i++) // Row
                {
                    if ((arrayPosition == 4) || (i < 0 || i >= MazeWidth || h < 0 || h >= MazeHeight))
                    {
                        /*
                         * Return null for this position if it lands outside of
                         * valid array bound, or, if it is not a neigbour 
                         * i.e. itself, the middle position
                         * 
                         *  + + +
                         *  + x + <--- x is the node whose neighbours we want
                         *  + + +
                         *  
                         *  When iterating over the results, it will allow the calling
                         *  code to have a static array length that can skip nulls 
                         *  in the event of recursion.
                         */
                        neighbours[arrayPosition] = null;
                    }
                    else
                    {
                        neighbours[arrayPosition] = new NodePtr((uint)i, (uint)h);
                    }
                    ++arrayPosition;
                }
            }

            return neighbours;
        }

        public bool IsNodeVisited(NodePtr ptr)
        {
            return this[ptr] != 0x1111;
        }

        public int MazeSize
        {
            get
            {
                return _storage.Length;
            }
        }
        public short[,] Grid
        {
            get
            {
                return _storage;
            }
        }

        public short this[uint x, uint y]
        {
            get
            {
                return this.Grid[x, y];
            }
            set
            {
                this.Grid[x, y] = value;
            }
        }
        public short this[NodePtr ptr]
        {
            get
            {
                return this[ptr.x, ptr.y];
            }
            set
            {
                this[ptr.x, ptr.y] = value;
            }
        }
    }
}
