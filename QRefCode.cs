using System;
using System.Collections;
using System.Text;


//All docstrings written using ChatGPT
namespace QueueLib
{
    /// <summary>
    /// Represents a generic queue implementation.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    public class MyQueue<T> : IEnumerable<T>
    {
        class MyNode<T>
        {
            public T item;
            public MyNode<T> next;

            /// <summary>
            /// Initializes a new instance of the MyNode class with the specified data.
            /// </summary>
            /// <param name="data">The data to be stored in the node.</param>
            public MyNode(T data)
            {
                item = data;
                next = null;
            }
        }

        private MyNode<T> head;
        private MyNode<T> tail;
        private int count;

        /// <summary>
        /// Initializes a new instance of the MyQueue class.
        /// </summary>
        public MyQueue()
        {
            count = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Checks if the queue is empty.
        /// </summary>
        /// <returns>True if empty, false if not.</returns>
        public bool isEmpty()
        {
            return count == 0;
        }

        /// <summary>
        /// Adds an item to the end of the queue.
        /// </summary>
        /// <param name="item">The value to be added to the queue.</param>
        public void Enqueue(T item)
        {
            if (count == 0)
            {
                MyNode<T> node = new MyNode<T>(item);
                tail.next = node;
                tail = node;
                count++;
            }
            else
            {
                head = tail = new MyNode<T>(item);
                count += 1;
            }
        }

        /// <summary>
        /// Removes the first item in the queue.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
        public void Dequeue()
        {
            if (count >= 2)
            {
                head = null;
                head.next = head;
                count--;
            }
            else if (count == 1)
            {
                head = null;
                tail = null;
                count = 0;
            }
            else
            {
                throw new InvalidOperationException("Cannot Dequeue, queue is empty");
            }
        }

        /// <summary>
        /// Gets the enumerator for the queue.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Converts the queue to an array.
        /// </summary>
        /// <returns>An array representation of the queue.</returns>
        //shared with my by Dhruv
        public T[] ToArray()
        {
            T[] array = new T[count];
            foreach (T item in this)
            {
                Console.WriteLine(item);
            }
            return array;
        }

        /// <summary>
        /// Prepares the enumerator function for cycling through the queue.
        /// </summary>
        /// <returns>The enumerator for the queue.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            MyNode<T> curr = head;
            while (curr != null)
            {
                yield return curr.item;
                curr = curr.next;
            }
        }

        /// <summary>
        /// Returns a brief summary of the queue.
        /// </summary>
        /// <returns>A string representation of the queue.</returns>
        /// <remarks>Written by ChatGPT.</remarks>
        public override string ToString()
        {
            if (count == 0)
            {
                return "Queue is empty.";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append($"Queue has {count} elements, and the values are: ");

            // Display the last three values
            MyNode<T> current = tail;
            for (int i = 0; i < Math.Min(3, count); i++)
            {
                sb.Append($"{current.item}");

                if (i < 2 && current.next != null)
                {
                    sb.Append(", ");
                }

                current = current.next;
            }

            sb.Append(" | "); // Add a separator between last three and first three values

            // Display the first three values
            current = head;
            for (int i = 0; i < Math.Min(3, count); i++)
            {
                sb.Append($"{current.item}");

                if (i < 2 && current.next != null)
                {
                    sb.Append(", ");
                }

                current = current.next;
            }

            return sb.ToString();
        }
    }
}
