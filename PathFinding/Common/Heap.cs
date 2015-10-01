using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding.Common
{
    public class MaxHeap<T> where T : IComparable
    {
        private T[] _storage;
        private int _usedStorage = 0;

        /// <summary>
        /// Initialises new, empty Heap.
        /// </summary>
        /// <param name="size">Size of Heap.</param>
        public MaxHeap(uint size)
        {
            _storage = new T[size];
        }

        /// <summary>
        /// Initialises a heap from an existing array.
        /// </summary>
        /// <param name="data">Array of T that gets copied in to new Heap.</param>
        public MaxHeap(T[] data)
        {
            _storage = new T[data.Count()];
            data.CopyTo(_storage, 0);
            _usedStorage = data.Length;
        }

        public T[] Data
        {
            get { return _storage; }
        }

        public int Size
        {
            get { return _usedStorage; }
        }

        public int Parent(int index)
        {
            return (int)Math.Floor((index - 1) / 2.0);
        }

        public int LeftChild(int index)
        {
            return index * 2 + 1;
        }

        public int RightChild(int index)
        {
            return (index * 2) + 2;
        }

        public void Insert(T obj)
        {
            if (_storage.Length == _usedStorage)
            {
                Array.Resize(ref _storage, _storage.Length * 2);
            }

            _usedStorage++;
            _storage[_usedStorage - 1] = default(T);
            IncreaseKey(_usedStorage - 1, obj);
        }

        /// <summary>
        /// Used to build a Heap that satisfies the Max-Heap
        /// property.
        /// </summary>
        public void BuildMaxHeap()
        {
            for (int i = (int)Math.Floor(_usedStorage / 2.0) - 1; i >= 0; i--)
            {
                MaxHeapify(i);
            }
        }

        public void MaxHeapify(int index)
        {
            var leftChild = LeftChild(index);
            var rightChild = RightChild(index);
            int largest;

            if (2*index+1 < _usedStorage && _storage[index].CompareTo(_storage[leftChild]) < 0)
            {
                largest = leftChild;
            } else
            {
                largest = index;
            }

            if (2*index+2 < _usedStorage && _storage[largest].CompareTo(_storage[rightChild]) < 0)
            {
                largest = rightChild;
            }

            if (!largest.Equals(index))
            {
                ExchangeNodes(index, largest);
                MaxHeapify(largest);
            }
        }

        public T Max
        {
            get { return _storage[0]; }
        }

        public T ExtractMax()
        {
            if (_usedStorage < 1)
            {
                throw new OverflowException("Heap is empty");
            }

            var max = _storage[0];
            _storage[0] = _storage[_usedStorage-- - 1];

            MaxHeapify(0);

            return max;
        }

        public void IncreaseKey(int index, T key)
        {
            if (key.CompareTo(_storage[index]) < 0)
            {
                throw new ArgumentException("New key is smaller than current key.");
            }

            _storage[index] = key;
            while (index > 1 && _storage[Parent(index)].CompareTo(_storage[index]) < 0)
            {
                ExchangeNodes(index, Parent(index));
                index = Parent(index);
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
