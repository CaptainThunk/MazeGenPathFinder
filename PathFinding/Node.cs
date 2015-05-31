using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public enum NodeWall : ushort
    {
        North = 0x0001,
        East = 0x0010,
        South = 0x0100,
        West = 0x1000
    }

    public struct NodePtr
    {
        public uint x, y;

        public NodePtr(uint _x, uint _y)
        {
            x = _x;
            y = _y;
        }
    }
}
