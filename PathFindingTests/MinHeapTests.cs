using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Common;

namespace PathFindingTests
{
   
    [TestClass]
    public class MinHeapTests
    {
        private MinHeap<int> heap;
        private int[] heapData = new int[6] { 5, 4, 6, 1, 0, 2 };

        [TestInitialize]
        public void Init()
        {
            heap = new MinHeap<int>(heapData);
            heap.BuildMinHeap();
        }

        [TestMethod]
        public void HeapIsConstructed()
        {
            Assert.IsInstanceOfType(heap, typeof(MinHeap<int>));
        }

        [TestMethod]
        public void BuildMinHeapGeneratesCorrectMinHeap()
        {
            heap.BuildMinHeap();
            var validData = new int[6] { 0, 1, 2, 5, 4, 6 };
            for (int i = 0; i < validData.Length; i++)
            {
                if (validData[i] != heap.Data[i])
                {
                    Assert.Fail("Heap has not generated correct Max Heap order");
                }
            }
        }

        [TestMethod]
        public void MinReturnsAsExpected()
        {
            var max = heap.Min;
            Assert.AreEqual(0, max);
        }

        [TestMethod]
        public void PopMaxReturnsMinAndDecrementsSize()
        {
            var max = heap.ExtractMin();
            Assert.AreEqual(0, max);
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
            Assert.AreEqual(0, heap.Data[2]);
            Assert.AreEqual(0, heap.Data[0]);
        }

        [TestMethod]
        public void InsertIntoHeapAtMaxPosition()
        {
            heap.Insert(20);
            Assert.AreEqual(20, heap.Data[heap.Size - 1]);
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

        [TestMethod]
        public void InsertMultipleItemsGeneratesCorrectTree()
        {
            var expectedHeap = new int[14]
            {
                0, 2, 1, 5, 4, 4, 3, 11, 7, 12, 8, 10, 6, 20
            };
            Array.Resize(ref expectedHeap, 20);
            var blankHeap = new MinHeap<int>(10);
            blankHeap.Insert(10);
            blankHeap.Insert(11);
            blankHeap.Insert(1);
            blankHeap.Insert(2);
            blankHeap.Insert(12);
            blankHeap.Insert(0);
            blankHeap.Insert(3);
            blankHeap.Insert(7);
            blankHeap.Insert(5);
            blankHeap.Insert(8);
            blankHeap.Insert(4);
            blankHeap.Insert(4);
            blankHeap.Insert(6);
            blankHeap.Insert(20);
            Assert.AreEqual(14, blankHeap.Size);
            Assert.AreEqual(0, blankHeap.Min);
            Assert.AreEqual(20, blankHeap.Data[13]);
            CollectionAssert.AreEqual(expectedHeap, blankHeap.Data);
        }

        [TestMethod]
        public void ExtractMinPopsCorrectly()
        {
            var extractionHeap = new MinHeap<int>(new int[14]
            {
                0, 2, 1, 5, 4, 4, 3, 11, 7, 12, 8, 10, 6, 20
            });
            Assert.AreEqual(0, extractionHeap.ExtractMin());
            Assert.AreEqual(1, extractionHeap.ExtractMin());
            Assert.AreEqual(2, extractionHeap.ExtractMin());
            Assert.AreEqual(3, extractionHeap.ExtractMin());
            Assert.AreEqual(4, extractionHeap.ExtractMin());
            Assert.AreEqual(4, extractionHeap.ExtractMin());
            Assert.AreEqual(5, extractionHeap.ExtractMin());
            Assert.AreEqual(6, extractionHeap.ExtractMin());
            Assert.AreEqual(7, extractionHeap.ExtractMin());
            Assert.AreEqual(8, extractionHeap.ExtractMin());
            Assert.AreEqual(10, extractionHeap.ExtractMin());
            Assert.AreEqual(11, extractionHeap.ExtractMin());
            Assert.AreEqual(12, extractionHeap.ExtractMin());
            Assert.AreEqual(20, extractionHeap.ExtractMin());
            Assert.AreEqual(0, extractionHeap.Size);
        }
    }
}
