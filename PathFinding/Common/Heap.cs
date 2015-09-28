using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding.Common
{
    public class Heap<T> where T : IComparable<T>
    {
        private T[] _storage;
        private int _usedStorage = 0;

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
            _usedStorage = data.Length;
        }

        public int Parent(int index)
        {
            return (int)Math.Floor(index / 2.0);
        }

        public int LeftChild(int index)
        {
            return index * 2;
        }

        public int RightChild(int index)
        {
            return (index * 2) + 1;
        }

        public void Insert(T obj)
        {
            if (_storage.Length == _usedStorage)
            {
                Array.Resize(ref _storage, _storage.Length * 2);
            }

            int hole = _usedStorage++;
            for ( ; hole > 1 && obj.CompareTo(_storage[hole/2]) < 0 ; hole /= 2)
            {
                _storage[hole] = _storage[hole / 2];
            }
            _storage[hole] = obj;
        }

        public void BuildMaxHeap()
        {
            throw new NotImplementedException();
        }

        public void MaxHeapify(int index)
        {
            var leftChild = LeftChild(index);
            var rightChild = RightChild(index);
            int largest;

            if (2*index < _usedStorage && _storage[index].CompareTo(_storage[leftChild]) < 0)
            {
                largest = leftChild;
            } else
            {
                largest = index;
            }

            if (2*index+1 < _usedStorage && _storage[largest].CompareTo(_storage[rightChild]) < 0)
            {
                largest = rightChild;
            }

            if (!largest.Equals(index))
            {
                ExchangeNodes(index, largest);
                MaxHeapify(largest);
            }
        }

        private void ExchangeNodes(int i, int r)
        {
            var temp = _storage[r];
            _storage[r] = _storage[i];
            _storage[i] = temp;
        }
    }
}
