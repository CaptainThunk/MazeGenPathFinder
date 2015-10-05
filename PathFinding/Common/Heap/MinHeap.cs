using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding.Common
{
    public class MinHeap<T> : AbstractHeap<T> where T : IComparable
    {
        private T[] _storage;

        public MinHeap(int size)
        {
            _storage = new T[size];
        }

        public MinHeap(T[] data)
        {
            _storage = new T[data.Count()];
            data.CopyTo(_storage, 0);
            _usedStorage = data.Length;
        }

        public T[] Data
        {
            get { return _storage; }
        }

        public T Min
        {
            get { return _storage[0]; }
        }

        public void BuildMinHeap()
        {
            for (int i = (int)Math.Floor(_usedStorage / 2.0) - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public override void Heapify(int index)
        {
            var leftChild = LeftChild(index);
            var rightChild = RightChild(index);
            int smallest;

            if (2*index+1 < _usedStorage && _storage[index].CompareTo(_storage[leftChild]) > 0)
            {
                smallest = leftChild;
            } else
            {
                smallest = index;
            }

            if (2*index+2 < _usedStorage && _storage[smallest].CompareTo(_storage[rightChild]) > 0)
            {
                smallest = rightChild;
            }

            if (!smallest.Equals(index))
            {
                ExchangeNodes(index, smallest);
                Heapify(smallest);
            }
        }

        public override void Insert(T obj)
        {
            if (_storage.Length == _usedStorage)
            {
                Array.Resize(ref _storage, _storage.Length * 2);
            }

            _storage[++_usedStorage - 1] = default(T);
            DecreaseKey(_usedStorage - 1, obj);
        }

        public override int LeftChild(int index)
        {
            return index * 2 + 1;
        }

        public override int Parent(int index)
        {
            return (int)Math.Floor((index - 1) / 2.0);
        }

        public override int RightChild(int index)
        {
            return index * 2 + 2;
        }

        public T ExtractMin()
        {
            if (_usedStorage < 1)
            {
                throw new OverflowException("Heap is empty.");
            }

            var min = _storage[0];
            _storage[0] = _storage[_usedStorage-- - 1];
            Heapify(0);

            return min;
        }

        private void DecreaseKey(int index, T key)
        {
            //if (key.CompareTo(_storage[index]) > 0)
            //{
            //    throw new ArgumentException("New key is larger than current key.");
            //}

            _storage[index] = key;
            while (index > 1 && _storage[Parent(index)].CompareTo(_storage[index]) > 0)
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

        public override void Empty()
        {
            for (int i = 0; i < _storage.Count(); i++)
            {
                _storage[i] = default(T);
            }
            _usedStorage = 0;
        }
    }
}
