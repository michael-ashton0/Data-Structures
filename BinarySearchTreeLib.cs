using System;
using System.Collections.IEnumerator;
using System.Collections.IEnumerable.GetEnumerator();
namespace BinarySearchTreeLib
{
    public class MyBST<K, V> where K : IComparable<K>
    {
        class Node<K, V>
        {
            public int count;
            public K key;
            public V value;
            public Node<K, V> l;
            public Node<K, V> r;

            public Node(K key, V value = default)
            {
                this.key = key;
                this.value = value;
                this.l = null;
                this.r = null;
                this.count = 1;
            }
        }
        Node<K, V> root;
        private int count;
        public void MyBinST()
        {
            root = null;
            count = 0;
        }
        /// <summary>
        /// Gets the total number of nodes in the BST
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                return root.count;
            }
        }
        /// <summary>
        /// returns the maximum value of the tree
        /// </summary>
        /// <returns>max of tree</returns>
        public K Max()
        {
            return Max(root);
        }
        /// <summary>
        /// bg process of public Max()
        /// </summary>
        /// <param name="subroot"></param>
        /// <returns>max</returns>
        private K Max(Node<K, V> subroot)
        {
            subroot = root;
            while (subroot.l != null)
            {
                int comp = subroot.key.CompareTo(subroot.l.key);
                if (comp == -1)
                {
                    subroot = subroot.l;
                }
            }
            return subroot.key;
        }
        /// <summary>
        /// returns min of tree
        /// </summary>
        /// <param name="key"></param>
        /// <returns>min</returns>
        public K Min()
        {
            if (root.count == 0)
            {
                throw new InvalidOperationException("There is no max of an empty tree");
            }
            else
            {
                Node<K, V> subroot = root;
                return Min(subroot);
            }
        }
        /// <summary>
        /// bg program for public Min()
        /// </summary>
        /// <param name="subroot"></param>
        /// <returns>min</returns>
        private K Min(Node<K, V> subroot)
        {
            if (subroot.r == null)
            {
                return subroot.key;
            }
            return Min(subroot.r);
        }
        /// <summary>
        /// Adds a new node to the BST
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(K key, V value)
        {
            root = Add(root, key, value);
        }
        /// <summary>
        /// Backbone of public add funciton
        /// </summary>
        /// <param name="subroot"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private Node<K, V> Add(Node<K, V> subroot, K key, V value)
        {
            //insertion point
            if (subroot == null)
            {
                return new Node<K, V>(key, value);
            }
            int compare = subroot.key.CompareTo(key);
            //if keys are equal
            if (compare == 0)
            {
                throw new InvalidOperationException($"Duplicate node key");
            }
            //if key is greater than
            else if (compare == 1)
            {
                subroot.r = Add(subroot.r, key, value);
            }
            //if key is less than
            else
            {
                subroot.l = Add(subroot.l, key, value);
            }
            subroot.count++;
            return subroot;
        }
        // ------- COULD NOT FIGURE OUT THE REMOVE FUNCTION TO SAVE MY LIFE this is what I had below -------
        /// <summary>
        /// removes a specified node from the tree
        /// </summary>
        /// <param name="key"></param>
        /*public void Remove(K key)
        {
            root = Remove(root, key) ;        
        }
        /// <summary>
        /// bg program for the public remove function
        /// </summary>
        /// <param name="subroot"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private Node<K,V> Remove(Node<K,V> subroot, K key)
        {
            if (subroot == null)
            {
                throw new KeyNotFoundException($"Node '{subroot}' was not found");
            }
            int comp = key.CompareTo(subroot.key);
            if (comp == -1)
            {
                subroot.l = Remove(subroot.l, key);
            }
            else if (comp == 1)
            {
                subroot.r = Remove(subroot.r, key);
            }
            else
            {
                if (subroot.l == null)
                {
                    //code for if no left child is found
                    subroot = subroot.r;
                }
                else if (subroot.r == null)
                {
                    //code for if no right child is found
                    subroot = subroot.l;
                }
                else
                {
                    //code for if there is both a right and left child
                    Node<K, V> successor = subroot.r;
                    while(subroot.r != null)
                    {
                        subroot = subroot.r;
                    }
                    successor = subroot;
                    successor.value = subroot.value;
                    successor.key = subroot.key;
                }
                Remove(subroot, subroot.key);
            }
        } */
        
        /// <summary>
        /// Returns the value of a keyed node
        /// </summary>
        public V This(K key)
        {
            V value = GetNode(key).value;
            return value;
        }
        /// <summary>
        /// does the BST contain this key?
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true or false</returns>
        public bool ContainsKey(K key)
        {
            if (GetNode == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Finds the successor of the bst
        /// </summary>
        /// <returns>the next highest value after the root</returns>
        public K Successor(K key)
        {
            Node<K, V> target = Successorbg(key);
            return target.key;
        }
        /// <summary>
        /// bg program for the public successor method
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private Node<K,V> Successorbg(K key)
        {
            Node<K, V> subroot = GetNode(key);
            if (subroot == null)
            {
                throw new InvalidOperationException($"The tree is empty");
            }
            subroot = subroot.l;
            while (subroot.r != null)
            {
                subroot = subroot.r;
            }
            return subroot;
        }
        /// <summary>
        /// Returns the node at a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private Node<K, V> GetNode(K key)
        {
            Node<K, V> subroot = root;
            while (subroot != null)
            {
                int comp = subroot.key.CompareTo(key);

                if (comp == 0)
                {
                    return subroot;
                }
                else if (comp == -1)
                {
                    subroot = subroot.l;
                }
                else
                {
                    subroot = subroot.r;
                }
            }
            throw new KeyNotFoundException($"Key '{key}' was not found");

        }
        /// <summary>
        /// prints the BST in order of key
        /// </summary>
        public void PrintInOrder()
        {
            PrintInOrder(root);
        }
        /// <summary>
        /// Bg program for PrintInOrder()
        /// </summary>
        /// <param name="subroot"></param>
        private void PrintInOrder(Node<K,V> subroot)
        {
            if (subroot != null)
            {
                PrintInOrder(subroot.l);
                Console.WriteLine(subroot.key);
                PrintInOrder(subroot.r);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// prints the current and left and right nodes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Node<K, V> subroot = root;
            string myself = $"{subroot.key}";
            string myL = (subroot.l == null) ? "NULL" : $"{subroot.l.key}";
            string myR = (subroot.r == null) ? "NULL" : $"{subroot.r.key}";
            return $"{myL} <- {myself} -> {myR}";
        }
        //enum hint copy from linkedlist will need to add a third enumerator function that is recursive
        //enums do not return, they yield return
        //enumerator written by ChatGPT
        public IEnumerator<K> GetEnumerator()
        {
            // Create a stack to perform an iterative in-order traversal
            Stack<Node<K, V>> stack = new Stack<Node<K, V>>();
            Node<K, V> current = root;

            // Traverse the BST until the current node is null and the stack is empty
            while (current != null || stack.Count > 0)
            {
                // Push all left children of the current node onto the stack
                while (current != null)
                {
                    stack.Push(current);
                    current = current.l;
                }

                // Pop the top node from the stack (leftmost node)
                current = stack.Pop();

                // Yield return the key of the current node
                yield return current.key;

                // Move to the right child of the current node
                current = current.r;
            }            
        }
    }
}