using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Common;

namespace PathFindingTests
{
    [TestClass]
    public class MaxHeapTests
    {
        private MaxHeap<int> heap;
        private int[] heapData = new int[6] { 5, 4, 6, 1, 0, 2 };

        [TestInitialize]
        public void Init()
        {
            heap = new MaxHeap<int>(heapData);
            heap.BuildMaxHeap();
        }

        [TestMethod]
        public void HeapIsConstructed()
        {
            Assert.IsInstanceOfType(heap, typeof(MaxHeap<int>));
        }

        [TestMethod]
        public void BuildMaxHeapGeneratesCorrectMaxHeap()
        {
            heap.BuildMaxHeap();
            var validData = new int[6] { 6, 4, 5, 1, 0, 2 };
            for (int i = 0; i < validData.Length; i++)
            {
                if (validData[i] != heap.Data[i])
                {
                    Assert.Fail("Heap has not generated correct Max Heap order");
                }
            }
        }

        [TestMethod]
        public void MaxReturnsAsExpected()
        {
            var max = heap.Max;
            Assert.AreEqual(6, max);
        }

        [TestMethod]
        public void PopMaxReturnsMaxAndDecrementsSize()
        {
            var max = heap.ExtractMax();
            Assert.AreEqual(6, max);
            Assert.AreEqual(5, heap.Size);
        }

        [TestMethod]
        public void InsertObjectIntoHeapIncreasesSizeOfArray()
        {
            var initialHeapSize = heap.Size;
            heap.Insert(0);
            var newHeapSize = heap.Size;
            Assert.AreNotEqual(initialHeapSize, newHeapSize);
        }

        [TestMethod]
        public void InsertIntoHeapAtMinPosition()
        {
            heap.Insert(0);
            Assert.AreEqual(0, heap.Data[heap.Size - 1]);
        }

        [TestMethod]
        public void InsertIntoHeapAtMaxPosition()
        {
            heap.Insert(20);
            Assert.AreEqual(20, heap.Data[0]);
        }

        [TestMethod]
        public void GetRootAsParentOdd()
        {
            var parent = heap.Parent(1);
            Assert.AreEqual(0, parent);
        }

        [TestMethod]
        public void GetRootAsParentEvent()
        {
            var parent = heap.Parent(2);
            Assert.AreEqual(0, parent);
        }

        [TestMethod]
        public void GetParentOfOddIndex()
        {
            var parent = heap.Parent(3);
            Assert.AreEqual(1, parent);
        }

        [TestMethod]
        public void GetParentOfEvenIndex()
        {
            var parent = heap.Parent(4);
            Assert.AreEqual(1, parent);
        }

        [TestMethod]
        public void GetLeftChildOfOddIndex()
        {
            var left = heap.LeftChild(1);
            Assert.AreEqual(3, left);
        }

        [TestMethod]
        public void GetRightChildOfOddIndex()
        {
            var right = heap.RightChild(1);
            Assert.AreEqual(4, right);
        }

        [TestMethod]
        public void GetLeftChildOfEvenIndex()
        {
            var left = heap.LeftChild(2);
            Assert.AreEqual(5, left);
        }

        [TestMethod]
        public void GetRightChildOfEvenIndex()
        {
            var right = heap.RightChild(2);
            Assert.AreEqual(6, right);
        }
    }
}
