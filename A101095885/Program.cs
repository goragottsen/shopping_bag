using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

//Task 1
namespace A101095885
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BoundedBag<string> b = new BoundedBag<string>("ShoppingList", 10);
                b.insert("apple");
                b.insert("eggs");
                b.insert("milk");
                b.saveBag("C:/Users/Goragottsen/Desktop/gbc/COMP2129/assignment/A101095885/A101095885/mybag.txt");
                BoundedBag<string> c = new BoundedBag<string>("ShoppingList", 10);
                c.loadBag("C:/Users/Goragottsen/Desktop/gbc/COMP2129/assignment/A101095885/A101095885/mybag.txt");
                WriteLine(c.remove());
                WriteLine(c.remove());
                WriteLine(c.remove());                
            }
            catch(BagFullException e)
            {
                
            }
            catch (BagEmptyException e)
            {
                
            }
            ReadKey();
        }
    }

    //Task 2
    public interface Bag<T> where T : class
    {
        T remove();
        void insert(T item);
        string getName();
        bool isEmpty();
        void saveBag(string path);
        void loadBag(string path);
    }

    //Task 3
    public class BagEmptyException : Exception
    {
        public BagEmptyException() { }

        public BagEmptyException(string message) : base(message)
        {
            WriteLine(message);
        }
    }

    public class BagFullException : Exception
    {
        public BagFullException() { }

        public BagFullException(string message) : base(message)
        {
            WriteLine(message);
        }
    }

    //Task 4
    public class BoundedBag<T> : Bag<T> where T : class
    {
        private string bagName; //bag name
        private int size; //max size of the bag
        private int lastIndex;
        private T[] items;
        private Random rnd;

        public BoundedBag(string name, int size)
        {
            bagName = name;
            this.size = size;
            rnd = new Random();
            items = new T[size];
            lastIndex = -1;
        }
        public string getName()
        {
            return bagName;
        }
        public bool isEmpty()
        {
            if (lastIndex == -1)
                return true;
            return false;
        }
        public bool isFull()
        {
            if (lastIndex == size)
                return true;
            return false;
        }

        public void loadBag(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                string[] lines = File.ReadAllLines(@path);
                for (int i = 0; i < lines.Length; i++)
                {
                    T it = (T)Convert.ChangeType(lines[i], typeof(T));
                    items[i] = it;                    
                }
                lastIndex = lines.Length - 1;
            }
        }

        public void saveBag(string path)
        {            
                StreamWriter sw = new StreamWriter(@path);
                foreach (T item in items)
                {
                    if (item != null)
                        sw.WriteLine(item);
                    else
                        break;
                }
                sw.Close();        
        }
        public void insert(T item)
        {
            if (isFull())
            {
                throw new BagFullException("The Bag is full");
            }
            else if (isEmpty())
            {
                lastIndex = 0;
                items[lastIndex] = (T)Convert.ChangeType(item, typeof(T));
                lastIndex++;
            }
            else
            {
                items[lastIndex] = (T)Convert.ChangeType(item, typeof(T));
                lastIndex++;
            }
        }
        public T remove()
        {
            if (!isEmpty())
            {
                int i = rnd.Next(0, lastIndex);
                T removed = items[i];
                items[i] = items[lastIndex];
                items[lastIndex] = null;
                lastIndex--;
                return removed;
            }
            else
            {
                throw new BagEmptyException("The bag is empty");
            }
            
        }
    }
}

