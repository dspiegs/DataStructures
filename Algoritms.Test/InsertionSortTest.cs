using System;
using Algoritms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Test
{
    [TestClass]
    public class InsertionSortTest
    {
        [TestMethod]
        public void Sort()
        {
            int[] a = {3, 2, 1};
            a = a.InsertionSort();
            Assert.AreEqual(1, a[0]);
            Assert.AreEqual(2, a[1]);
            Assert.AreEqual(3, a[2]);
        }
    }
}
