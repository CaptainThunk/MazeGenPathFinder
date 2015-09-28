using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding.Common;

namespace PathFindingTests
{
    [TestClass]
    public class HeapTests
    {
        private Heap<int> heap;

        [TestInitialize]
        public void Init()
        {
            heap = new Heap<int>(6);
        }

        [TestMethod]
        public void HeapIsConstructed()
        {
            Assert.IsInstanceOfType(heap, typeof(Heap<int>));
        }

        [TestMethod]
        public void HeapInsertsObjectInCorrectLocation()
        {

        }

        [TestMethod]
        public void MaxHeapifyCorrectsSingleViolationOfHeapProperty()
        {
            heap.MaxHeapify(0);
        }
    }
}
