using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding.Common
{
    public class Heap<T>
    {
        private T[] _storage;

        /// <summary>
        /// Initialises new, empty Heap.
        /// </summary>
        /// <param name="size">Size of Heap.</param>
        public Heap(uint size)
        {
            _storage = new T[size];
        }

        /// <summary>
        /// Initialises a heap from an existing array.
        /// </summary>
        /// <param name="data">Array of T that gets copied in to new Heap.</param>
        public Heap(T[] data)
        {
            _storage = new T[data.Count()];
            data.CopyTo(_storage, 0);
        }

        public void MaxHeapify(uint index)
        {
            throw new NotImplementedException();
        }
    }
}
