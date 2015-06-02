using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneration.Generation
{
    public interface IMazeGenerator
    {
        void Generate(Maze maze);
    }
}
