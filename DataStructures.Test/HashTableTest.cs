using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Test
{
    [TestClass]
    public class HashTableTest
    {
        private HashTable<int, string> table;

        [TestInitialize]
        public void Setup()
        {
            table = new HashTable<int, string>(4);
            table.Put(1, "One");
            table.Put(2, "Two");
            table.Put(3, "Three");
            table.Put(4, "Four");
            table.Put(10000, "Ten Thousands");
        }

        [TestMethod]
        public void NotIn()
        {
            Assert.IsFalse(table.ContainsKey(6));
        }

        [TestMethod]
        public void In()
        {
            Assert.IsTrue(table.ContainsKey(1));
        }

        [TestMethod]
        public void TestGet()
        {
            Assert.AreEqual("Two", table.Get(2));
        }

        [TestMethod]
        [ExpectedException(typeof (KeyNotFoundException))]
        public void TestGetNotIn()
        {
            table.Get(12);
        }

        [TestMethod]
        [ExpectedException(typeof (KeyNotFoundException))]
        public void TestRemove()
        {
            Assert.IsTrue(table.Remove(3));
            table.Get(3);
        }

        [TestMethod]
        public void TestUpdate()
        {
            table.Put(1, "Not One");
            Assert.AreEqual(table.Get(1), "Not One");
        }

        [TestMethod]
        public void TestUpdate2()
        {
            table.Put(1, "Not One");
            table.Put(1, "Really Not One");
            Assert.AreEqual(table.Get(1), "Really Not One");
        }

        [TestMethod]
        [ExpectedException(typeof (KeyNotFoundException))]
        public void TestUpdateRemove()
        {
            table.Put(1, "Not One");
            Assert.AreEqual(table.Get(1), "Not One");
            table.Remove(1);
            table.Get(1);
        }

        [TestMethod]
        public void TestRemoveAgain()
        {
            table.Remove(1);
            Assert.IsFalse(table.Remove(1));
        }

        [TestMethod]
        public void TestSizeGrow()
        {
            var size = table.Size;
            for (int i = table.Count; i < (size*2) - 1; i ++)
            {
                table.Put(i, i.ToString());
            }
            Assert.AreEqual(size*2, table.Size);

            size = table.Size;
            for (int i = table.Count; i < (size*2) - 1; i++)
            {
                table.Put(i, i.ToString());
            }
            Assert.AreEqual(size*2, table.Size);

        }

        [TestMethod]
        public void TestSizeGrow2()
        {
            var t = new HashTable<int, string>(1024);
            t.Put(1, "One");
            Assert.AreEqual(1024, t.Size);
        }

        [TestMethod]
        public void TestSizeShrink()
        {
            var t = new HashTable<int, string>(16);
            t.Put(1, "One");
            t.Put(2, "Two");
            t.Put(3, "Three");
            t.Put(5, "Five");
            t.Put(6, "Six");
            t.Put(7, "Seven");
            t.Put(8, "Eight"); //8
            t.Remove(4); //7

            Assert.AreEqual(16, t.Size);

            t.Remove(1); //6
            t.Remove(2); //5
            t.Remove(3); //4
            Assert.AreEqual(8, t.Size);
        }
    }
}
