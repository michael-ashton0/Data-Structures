using System.Collections;
namespace ListSymbolTableLib;

//***all enumerator related functions were written by chatGPT***
public class MyListST<K, V> : IEnumerable<K> where K : IComparable<K>
{
    public class Node<K, V>
    {
        public K key;
        public V value;
        public Node<K, V> next;

        public Node(K key, V value = default)
        {
            this.value = value;
            this.key = key;
            this.next = null;
        }
    }
    private Node<K, V> head;
    private Node<K, V> tail;
    private int count;
    /// <summary>
    /// intializes the list to zero
    /// </summary>
    public MyListST()
    {
        Clear();
        //O(1)
    }
    /// <summary>
    /// Adds a value to the list O(1)
    /// </summary>
    /// <param name="item"></param>
    public void Add(K key, V value = default)
    {
        if (count == 0)
        {
            head = tail = new Node<K,V>(key, value);
            count = 1;
        }
        else
        {
            tail.next = tail = new Node<K,V>(key, value);
            count++;
        }
    }
    /// <summary>
    /// Checks to see if the list contains a value O(n)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool ContainsKey(K key)
    {
        try
        {
            WalkToKey(key);
            return true;
        }
        catch (KeyNotFoundException e) 
        {
            return false;
        }
    }
    /// <summary>
    /// removes a value from the list at the given key O(n)
    /// </summary>
    /// <param name="idx"></param>
    /// <exception cref="IndexOutOfRangeException">thrown when an invalid index is given</exception>
    public void RemoveAt(K key)
    /*{
        if (key == null)
        {
            Node<K, V> removal = WalkToKey(key);
            Node<K, V> prev = WalkToKey(key - 1);

            prev.next = prev.next.next;

            count--;
        }
        else if (key.CompareTo(count) == 1|| key.CompareTo(0) == 0)
        {
            throw new IndexOutOfRangeException();
        }

        else if (key.CompareTo(count - 1) == 0)
        {
            Node<K, V> prev = WalkToKey(key - 1);
            prev.next = null;
            tail = prev;
            count--;
        }

        else if (key.CompareTo(0) == 0)
        {
            head = head.next;
            count--;
        }

        else
        {
            throw new Exception($"Value {key} caused an error.");
        }
    }*/

        //^code from the linked list that I could not get to work
        //completely rewritten method below
    {
        if (head == null)
        {
            throw new InvalidOperationException("The symbol table is empty.");
        }

        if (key == null)
        {
            throw new InvalidOperationException("Key cannot be null.");
        }

        Node<K,V> current = head;
        Node<K,V> prev = null;

        while (current != null)
        {
            if (current.key.Equals(key))
            {
                if (prev == null)
                {
                    head = current.next;
                }
                else
                {
                    prev.next = current.next;
                }

                count--;
                return;
            }

            prev = current;
            current = current.next;
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
    /* public K TestWalkToNode(int idx)
    {
        Node<K> curr = WalkToNode(idx);
        return curr.item;
    } */

    /// <summary>
    /// Sister program to insert that streamlines the code O(n)
    /// </summary>
    /// <param name="idx"></param>
    /// <returns>throws an exception if index to search for is invalid</returns>
    private Node<K,V> WalkToKey(K key)
    {
        Node<K, V> curr = head;
        while (curr != null)
        {
            if (curr.key.Equals(key))
            {
                return curr;
            }
            curr = curr.next;
        }
        throw new KeyNotFoundException($"Key '{key}' was not found in the list.");
    }
    /* /// <summary>
    /// used to find the smallest value in the list O(n)
    /// </summary>
    /// <returns></returns>
    public K Minimum()
    {
        K smallestSoFar = head.item;
        Node<K> curr = head;
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
    public K Maximum()
    {
        K biggestSoFar = head.item;
        Node<K> curr = head;
        while (curr != null)
        {
            if (curr.item.CompareTo(biggestSoFar) == 1)
            {
                biggestSoFar = curr.item;
            }

            curr = curr.next;
        }
        return biggestSoFar;
    } */
    /// <summary>
    /// copies a value to an array from the list O(n)
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public V this[K key]
    {
        get
        {
            Node<K,V> curr = WalkToKey(key);
            return curr.value;
        }
        set
        {
            Node<K,V> curr = WalkToKey(key);
            curr.value = value;
        }
    }
    /// <summary>
    /// enumerator O(n)
    /// </summary>
    /// <returns></returns>
    public IEnumerator<K> GetEnumerator()
    {
        Node<K,V> curr = head;
        while (curr != null)
        {
            yield return curr.key;
            curr = curr.next;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
