using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding.Common
{
    public abstract class AbstractHeap<T> : IHeap<T>
    {
        protected int _usedStorage = 0;
        public virtual int Size
        {
            get { return _usedStorage; }
        }

        public abstract int Parent(int index);
        public abstract int LeftChild(int index);
        public abstract int RightChild(int index);
        public abstract void Insert(T obj);
        public abstract void Heapify(int index);
        public abstract void Empty();
    }
}
