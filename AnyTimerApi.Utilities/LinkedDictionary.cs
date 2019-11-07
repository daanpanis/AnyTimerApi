using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AnyTimerApi.Utilities
{
    public class LinkedDictionary<K, V> : IDictionary<K, V> where V : class
    {
        private readonly Dictionary<K, LinkedListNode<Tuple<V, K>>> _dictionary =
            new Dictionary<K, LinkedListNode<Tuple<V, K>>>();

        private readonly LinkedList<Tuple<V, K>> _linkedList = new LinkedList<Tuple<V, K>>();

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return _dictionary.Select(pair => new KeyValuePair<K, V>(pair.Key, pair.Value.Value.Item1)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            var (key, value) = item;
            Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            _linkedList.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return Remove(item.Key);
        }

        public int Count => _dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            if (ContainsKey(key)) return;
            Put(key, value);
        }

        private void Put(K key, V value)
        {
            var node = new LinkedListNode<Tuple<V, K>>(new Tuple<V, K>(value, key));
            _dictionary.Add(key, node);
            _linkedList.AddFirst(node);
        }

        public bool ContainsKey(K key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(K key)
        {
            var node = _dictionary[key];
            return node != null && Remove(node);
        }

        private bool Remove(LinkedListNode<Tuple<V, K>> node)
        {
            _linkedList.Remove(node);
            return _dictionary.Remove(node.Value.Item2);
        }

        public bool TryGetValue(K key, out V value)
        {
            _dictionary.TryGetValue(key, out var node);
            value = node?.Value?.Item1;
            return value != null;
        }

        public V this[K key]
        {
            get
            {
                TryGetValue(key, out var node);
                return node;
            }
            set
            {
                Remove(key);
                Put(key, value);
            }
        }

        public ICollection<K> Keys => _dictionary.Keys;

        public ICollection<V> Values => _linkedList.Select(tuple => tuple.Item1).ToList();

        public V First()
        {
            return _linkedList.First?.Value?.Item1;
        }

        public V Last()
        {
            return _linkedList.Last?.Value?.Item1;
        }

        public bool RemoveFirst()
        {
            var first = _linkedList.First;
            return first != null && Remove(first);
        }

        public bool RemoveLast()
        {
            var last = _linkedList.Last;
            return last != null && Remove(last);
        }
    }
}