using Microsoft.VisualStudio.TestTools.UnitTesting;
using A101095885;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A101095885.Tests
{
    [TestClass()]
    public class BoundedBagTests
    {


        [TestMethod()]
        public void getNameTest()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            Assert.AreEqual("bag", b.getName());
        }

        [TestMethod()]
        public void isEmptyTest1()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            Assert.IsTrue(b.isEmpty());
        }

        [TestMethod()]
        public void isEmptyTest2()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            Assert.IsFalse(b.isEmpty());
        }

        [TestMethod()]
        public void isEmptyTest3()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            b.remove();
            Assert.IsTrue(b.isEmpty());

        }

        [TestMethod()]
        public void isFullTest1()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 0);
            Assert.IsTrue(b.isFull());
        }

        [TestMethod()]
        public void isFullTest2()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 1);
            b.insert("one");
            Assert.IsTrue(b.isFull());
        }

        [TestMethod()]
        public void isFullTest3()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 2);
            b.insert("one");
            Assert.IsFalse(b.isFull());
        }
        [TestMethod()]
        public void insertTest1()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            b.insert("two");
            b.insert("one");
            Assert.IsTrue(b.isFull());
        }

        [TestMethod()]
        public void insertTest2()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            b.insert("two");
            b.insert("one");
            try
            {
                b.insert("four");
            }
            catch (BagFullException e) { }
            finally
            {
                Assert.AreEqual("bag", b.getName());
            }
        }
        [TestMethod()]
        public void insertTest3()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            b.insert("two");
            b.insert("one");
            Assert.IsFalse(b.isEmpty());
        }

        [TestMethod()]
        public void removeTest1()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            string s = b.remove();
            Assert.IsTrue(s.Equals("one"));
        }

        [TestMethod()]
        public void removeTest2()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 0);
            try
            {
                b.remove();
            }
            catch (BagEmptyException e) { }
            finally
            {
                Assert.AreEqual("bag", b.getName());
            }

        }
        [TestMethod()]
        public void removeTest3()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            b.insert("two");
            b.insert("two");
            try
            {
                b.remove();
                b.remove();
            }
            catch (BagEmptyException e) { }
            finally
            {
                Assert.IsFalse(b.isFull());
            }

        }

        [TestMethod()]
        public void loadsaveTest()
        {
            BoundedBag<string> b = new BoundedBag<string>("bag", 3);
            b.insert("one");
            b.insert("two");
            b.insert("three");
            b.saveBag("C:/Users/Goragottsen/Desktop/gbc/COMP2129/assignment/A101095885/A101095885/mybag.txt");
            BoundedBag<string> newb = new BoundedBag<string>("new", 3);
            newb.loadBag("C:/Users/Goragottsen/Desktop/gbc/COMP2129/assignment/A101095885/A101095885/mybag.txt");
            newb.remove();
            Assert.IsFalse(newb.isFull());
        }
    }
}