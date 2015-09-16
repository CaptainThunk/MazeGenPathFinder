using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneration
{
    public class Node
    {
        public ushort Walls = (ushort)NodeWall.All;
        public bool Visited = false;

        public bool Equals(Node node)
        {
            return (this.Walls == node.Walls);
        }
    }
    
    public enum NodeWall : ushort
    {
        North = 0x0001,
        East = 0x0010,
        South = 0x0100,
        West = 0x1000,
        All = 0x1111
    }

    public struct NodePtr
    {
        public uint x, y;

        public NodePtr(uint _x, uint _y)
        {
            x = _x;
            y = _y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NodePtr))
            {
                return false;
            }
            NodePtr otherPtr = (NodePtr)obj;
            return this.x == otherPtr.x && this.y == otherPtr.y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}
