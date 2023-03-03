using System;
using System.Linq;

namespace Lab2
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private T[] array;
        private const int initialSize = 8;

        public int Count { get; private set; }

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;


        public MinHeap(T[] initialArray = null)
        {
            array = new T[initialSize];

            if (initialArray == null)
            {
                return;
            }

            foreach (var item in initialArray)
            {
                Add(item);
            }

        }

        /// <summary>
        /// Returns the min item but does NOT remove it.
        /// Time complexity: O(1)
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            return array[0];
        }

        // TODO
        /// <summary>
        /// Adds given item to the heap.
        /// Time complexity: O(log(n))
        /// </summary>
        public void Add(T item)
        {
            int nextEmptyIndex = Count;

            array[nextEmptyIndex] = item;

            TrickleUp(nextEmptyIndex);

            Count++;

            // resize if full
            if (Count == Capacity)
            {
                DoubleArrayCapacity();
            }

        }

        public T Extract()
        {
            return ExtractMin();
        }

        // TODO
        /// <summary>
        /// Removes and returns the max item in the min-heap.
        /// Time complexity: O( N )
        /// </summary>
        public T ExtractMax()
        {
            // linear search
            var max = 0;
            var i = 0;
            while (i < Count)
            {
                if (array[i].CompareTo(array[max]) > 0)
                {
                    max = i;
                }
                i++;
            }
            Swap(max, Count - 1);
            Count--;
            TrickleDown(max);
            return array[max];
            // remove max

        }

        // TODO
        /// <summary>
        /// Removes and returns the min item in the min-heap.
        /// Time complexity: O( log(n) )
        /// </summary>
        public T ExtractMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T min = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return min;
        }

        // TODO
        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O( N )
        /// </summary>
        public bool Contains(T value)
        {
            // linear search

            for(int i=0; i < Count; i++)
            {
                if (array[i].CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }

        // TODO
        /// <summary>
        /// Updates the first element with the given value from the heap.
        /// Time complexity: O( N )
        /// </summary>
        public void Update(T oldValue, T newValue)
        {
            if (array.Contains(oldValue))
            {
                int i = 0;
                while (array[i].CompareTo(oldValue) != 0)
                {
                    i++;
                }
                array[i] = newValue;
                if (newValue.CompareTo(oldValue) > 0)
                {
                    TrickleDown(i);
                }
                else
                {
                    TrickleUp(i);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        // TODO
        /// <summary>
        /// Removes the first element with the given value from the heap.
        /// Time complexity: O( N )
        /// </summary>
        public void Remove(T value)
        {
            if (array.Contains(value))
            {
                int i = 0;
                while (array[i].CompareTo(value) != 0)
                {
                    i++;
                }
                Swap(i, Count - 1);
                Count--;
                TrickleDown(i);
            }
            else
            {
                throw new Exception();
            }

        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleUp(int index)
        {
            if (index == 0)
            {
                return;
            }
            else if (array[index].CompareTo(array[Parent(index)]) > 0)
            {
                return;
            }
            else if (array[index].CompareTo(array[Parent(index)]) < 0)
            {
                Swap(index, Parent(index));
                TrickleUp(Parent(index));
            }

        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleDown(int index)
        {
            if (RightChild(index) > Count - 1)
            {
                if (LeftChild(index) > Count - 1)
                {
                    return;
                }
                else
                {
                    if (array[index].CompareTo(array[LeftChild(index)]) < 0)
                    {
                        return;
                    }
                    else
                    {
                        Swap(index, LeftChild(index));
                        TrickleDown(LeftChild(index));
                    }
                }
            }
            else
            {
                if (array[index].CompareTo(array[RightChild(index)]) < 0)
                {
                    if (array[index].CompareTo(array[LeftChild(index)]) > 0)
                    {
                        Swap(index, LeftChild(index));
                        TrickleDown(LeftChild(index));
                    }
                    return;
                }
                else
                {
                    Swap(index, RightChild(index));
                    TrickleDown(RightChild(index));
                }
            }
        }

        // TODO
        /// <summary>
        /// Gives the position of a node's parent, the node's position in the heap.
        /// </summary>
        private static int Parent(int position)
        {
            int parent = (position - 1) / 2;
            return parent;
        }

        // TODO
        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            int lchild = (2 * position) + 1;
            return lchild;
        }

        // TODO
        /// <summary>
        /// Returns the position of a node's right child, given the node's position.
        /// </summary>
        private static int RightChild(int position)
        {
            int rchild = (2 * position) + 2;
            return rchild;
        }

        private void Swap(int index1, int index2)
        {
            var temp = array[index1];

            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void DoubleArrayCapacity()
        {
            Array.Resize(ref array, array.Length * 2);
        }


    }
}


