namespace PathFinding.Common
{
    public interface IHeap<T>
    {
        void Insert(T obj);
        void Heapify(int index);
    }
}