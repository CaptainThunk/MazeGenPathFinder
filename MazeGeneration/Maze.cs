using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeGeneration
{
    public class Maze
    {
        private Node[,] _storage;
        public int Height;
        public int Width;

        public Maze() { }

        public Maze(int mazeWidth, int mazeHeight)
        {
            this.Create(mazeWidth, mazeHeight);
        }

        public void Create(int mazeWidth, int mazeHeight)
        {
            Height = mazeHeight;
            Width = mazeWidth;
            this._storage = new Node[mazeWidth, mazeHeight];
            InitialiseNodes();
        }

        public void InitialiseNodes()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int h = 0; h < Height; h++)
                {
                    _storage[i, h] = new Node();
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
                    if ((arrayPosition == 4) || (i < 0 || i >= Width || h < 0 || h >= Height))
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

        public bool IsPointValid(NodePtr startPoint)
        {
            if (startPoint.x < 0 || startPoint.x > this.Width - 1 || startPoint.y < 0 || startPoint.y > this.Height - 1) return false;
            return true;
        }

        public bool IsPassable(NodePtr startPoint, NodePtr endPoint)
        {
            throw new NotImplementedException();
        }

        public bool IsNodeVisited(NodePtr ptr)
        {
            return this[ptr].Visited;
        }

        public int MazeSize
        {
            get
            {
                return _storage.Length;
            }
        }
        public Node[,] Grid
        {
            get
            {
                return _storage;
            }
        }

        public Node this[uint x, uint y]
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
        public Node this[NodePtr ptr]
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

        public bool Tunnel(NodePtr from, NodePtr to)
        {
            Node fromNode = this[from];
            Node toNode = this[to];
            int dx = (int)to.x - (int)from.x;
            int dy = (int)to.y - (int)from.y;

            // Ugly wall breaking code
            if (dx == 0 && dy == -1)
            {
                fromNode.Walls ^= (ushort)NodeWall.North;
                toNode.Walls ^= (ushort)NodeWall.South;
            }
            else if (dx == 1 && dy == 0)
            {
                fromNode.Walls ^= (ushort)NodeWall.East;
                toNode.Walls ^= (ushort)NodeWall.West;
            }
            else if (dx == 0 && dy == 1)
            {
                fromNode.Walls ^= (ushort)NodeWall.South;
                toNode.Walls ^= (ushort)NodeWall.North;
            }
            else if (dx == -1 && dy == 0)
            {
                fromNode.Walls ^= (ushort)NodeWall.West;
                toNode.Walls ^= (ushort)NodeWall.East;
            } 
            else { return false; } // Should never happen

            return true;
        }
    }
}
