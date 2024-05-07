using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListLib
{

    //***all enumerator related functions were written by chatGPT***
    public class MyLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node<T>
        {
            public T item;
            public Node<T> next;

            public Node(T data = default)
            {
                item = data;
                next = null;
            }
        }
        private Node<T> head;
        private Node<T> tail;
        private int count;
        /// <summary>
        /// intializes the list to zero
        /// </summary>
        public MyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;

            //O(1)
        }
         /// <summary>
         /// Adds a value to the list O(1)
         /// </summary>
         /// <param name="item"></param>
        public void Add(T item)
        {
            if (count == 0)
            {
                head = tail = new Node<T>(item);
                count = 1;
            }
            else
            {
                tail.next = tail = new Node<T>(item);
                count++;
            }
        }
        /// <summary>
        /// Checks to see if the list contains a value O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.item.Equals(item))
                {
                    return true;
                }
                curr = curr.next;
            }
            return false;
        }
         /// <summary>
         /// Finds the index (position) of a particular value O(n)
         /// </summary>
         /// <param name="item"></param>
         /// <returns></returns>
        public int IndexOf(T item)
        {
            int position = 0;
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.item.Equals(item))
                {
                    return position;
                }
                curr = curr.next;
                position++;
            }
            return -1;
        }
        /// <summary>
        /// Inserts a value at a point in the list O(n)
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="item"></param>
        public void Insert(int idx, T item)
        {
            if (count == 0 || idx == count)
            {
                Add(item);
            }
            
            else if (idx == 0)
            {
                Node<T> node = new Node<T>(item);
                node.next = head;
                head = node;
            }

            else
            {
                Node<T> prev = WalkToNode(idx--);
                Node<T> next = prev.next;

                Node<T> node = new Node<T>(item);
                node.next = next;
                prev.next = node;

                count++;
            }      
        }
        /// <summary>
        /// removes a value from the list at the given index O(n)
        /// </summary>
        /// <param name="idx"></param>
        /// <exception cref="IndexOutOfRangeException">thrown when an invalid index is given</exception>
        public void RemoveAt(int idx)
        {
            if (idx > 0 && idx < count - 1)
            {
                Node<T> removal = WalkToNode(idx);
                Node<T> prev = WalkToNode(idx-1);

                prev.next = prev.next.next;

                count--;
            }
            else if (idx > count || idx < 0)
            {
                throw new IndexOutOfRangeException();
            }

            else if (idx == count - 1)
            {
                Node<T> prev = WalkToNode(idx - 1);
                prev.next = null;
                tail = prev;
                count--;
            }

            else if (idx == 0)
            {
                head = head.next;
                count--;
            }

            else
            {
                throw new Exception($"Value {idx} caused an error.");
            } 
        }
        /// <summary>
        /// resets list to default null values O(1)
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
        /// <summary>
        /// wrapper tester program for WalkToNode
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        /* public T TestWalkToNode(int idx)
        {
            Node<T> curr = WalkToNode(idx);
            return curr.item;
        } */

        /// <summary>
        /// Sister program to insert that streamlines the code O(n)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>throws an exception if index to search for is invalid</returns>
        private Node<T> WalkToNode(int idx)
        {
            if (idx >= 0 && idx < count)
            {
                int posit = 0;
                Node<T> curr = head;
                while (posit < idx)
                {
                    curr = curr.next;
                    posit++;
                }
                return curr;
            }

            throw new IndexOutOfRangeException($"Index {idx} is invalid for a linked list with {count} elements.");
        }
        /// <summary>
        /// used to find the smallest value in the list O(n)
        /// </summary>
        /// <returns></returns>
        public T Minimum()
        {
            T smallestSoFar = head.item;
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.item.CompareTo(smallestSoFar) == -1)
                {
                    smallestSoFar = curr.item;
                }               
                curr = curr.next;
            }
            return smallestSoFar;
        }
        /// <summary>
        /// used to find the largest value in the list O(n)
        /// </summary>
        /// <returns></returns>
        public T Maximum()
        {
            T biggestSoFar = head.item;
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.item.CompareTo(biggestSoFar) == 1)
                {
                    biggestSoFar = curr.item;
                }

                curr = curr.next;
            }
            return biggestSoFar;
        }
        /// <summary>
        /// copies a value to an array from the list O(n)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public T this[int idx]
        {
            get
            {
                if (idx < 0)
                {
                    idx = idx + count;
                }
                Node<T> curr = WalkToNode(idx);           
                return curr.item;
            }
            set
            {
                if (idx < 0)
                {
                    idx = idx + count;
                }
                Node<T> curr = WalkToNode(idx);
                curr.item = value;
            }
        }
        /// <summary>
        /// copies the elements of the list to a new array O(n)
        /// </summary>
        /// <returns>An array containing the elements of the linked list.</returns>
        public T[] ToArray()
        {
            T[] listArray = new T[count];
            int index = 0;
            Node<T> current = head;
            while (current != null)
            {
                listArray[index++] = current.item;
                current = current.next;
            }
            return listArray;
        }
        //The following ToString function was made by chatGPT
        /// <summary>
        /// Returns a string representation of the linked list. O(1)
        /// </summary>
        /// <returns>A string representation of the linked list.</returns>
        public override string ToString()
        {
            if (count == 0)
                return "Empty List";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Node<T> current = head;
            while (current != null)
            {
                sb.Append(current.item.ToString());
                if (current.next != null)
                    sb.Append(" -> ");
                current = current.next;
            }
            return sb.ToString();
        }
        /// <summary>
        /// enumerator O(n)
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.item;
                current = current.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// returns the node at the halfway point of the list
        /// </summary>
        /// <returns></returns>
        public T GetHalf()
        {
            Node<T> curr = GetHalfNode(head);
            if (curr == null)
            {
                throw new InvalidOperationException("List empty");
            }
            return curr.item;
        }
        private Node<T> GetHalfNode(Node<T> curr)
        {
            if (curr == null)
            {
                return null;
            }
            Node<T> swcurr = curr;
            Node<T> fwcurr = curr.next;
            while (fwcurr != null)
            {
                fwcurr = fwcurr.next;
                if (fwcurr == null)
                {
                    break;
                }
                fwcurr = fwcurr.next;
                swcurr = swcurr.next;
            }          
            return swcurr;
        }
        /// <summary>
        /// runs the merge sort algorithms
        /// </summary>
        public void MergeSort()
        {
            head = MergeSort(head);
        }
        /// <summary>
        /// Sorts the list into ascending order using a Merging algorithm
        /// </summary>
        private Node<T> MergeSort(Node<T> head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            Node<T> mid = GetHalfNode(head);
            Node<T> midplusone = mid.next;
            mid.next = null;

            Node<T>left = MergeSort(head);
            Node<T> right = MergeSort(midplusone);

            return Merge(left, right);
        }
        /// <summary>
        /// sorts and merges the split lists in asceending order
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private Node<T> Merge(Node<T> left, Node<T> right)
        {
            Node<T> result = null;

            if (left == null)
                return right;
            if (right == null)
                return left;

            if (left.item.CompareTo(right.item) < 0)
            {
                result = left;
                result.next = Merge(left.next, right);
            }
            else
            {
                result = right;
                result.next = Merge(left, right.next);
            }

            return result;
        }
        /// <summary>
        /// sorts the list into ascending order
        /// </summary>
        public void SimpleSort()
        {
            Node<T> curr = head;
            while (curr != null)
            {
                Node<T> min = curr;
                Node<T> nextNode = curr.next;
                while (nextNode != null)
                {
                    if (nextNode.item.CompareTo(min.item) == -1)
                    {
                        min = nextNode;
                    }
                    nextNode = nextNode.next;
                }
                T temp = curr.item;
                curr.item = min.item;
                min.item = temp;
                curr = curr.next;
            }
        }
        /// <summary>
        /// checks to see if the list has been successfully sorted
        /// </summary>
        /// <returns></returns>
        public bool IsSorted()
        {
            Node<T> curr = head;
            while (curr.next != null)
            {
                if (curr.item.CompareTo(curr.next.item) == 1)
                {
                    return false;
                }
                curr = curr.next;
            }
            return true;
        }
    }
}
