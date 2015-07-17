﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class HashTable<TKey, TValue>
    {
        private int size;
        private int count;

        public int Count
        {
            get { return count; }
        }

        private class HashItem
        {
            public HashItem(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public TKey Key { get; set; }
            public TValue Value { get; set; }
        }

        private List<HashItem>[] hashItems;

        public HashTable(int initialSize = 32)
        {
            this.size = initialSize;
            hashItems = new List<HashItem>[initialSize];
        }

        public void Put(TKey key, TValue value)
        {
            var index = key.GetHashCode()%size;
            var list = hashItems[index] ?? (hashItems[index] = new List<HashItem>());
            foreach (var hashItem in list)
            {
                if (Equals(hashItem.Key, key))
                {
                    hashItem.Value = value;                   
                    return;
                }
            }

            list.Add(new HashItem(key, value));
            count++;
            CheckForResize();            
        }

        public TValue Get(TKey key)
        {
            var index = key.GetHashCode()%size;
            var list = hashItems[index];
            if (list == null)
            {
                throw new KeyNotFoundException("There is no value with that key in the Hash Table");
            }

            foreach (var hashItem in list)
            {
                if (Equals(hashItem.Key, key))
                {
                    return hashItem.Value;
                }
            }

            throw new KeyNotFoundException("There is no value with that key in the Hash Table");
        }

        public bool Remove(TKey key)
        {
            var index = key.GetHashCode() % size;
            var list = hashItems[index];
            if (list == null)
            {
                return false;
            }

            HashItem item = null;
            foreach (var hashItem in list)
            {
                if (Equals(hashItem.Key, key))
                {                    
                    item = hashItem;
                    break;
                }
            }

            if (item == null)
            {
                return false;
            }

            list.Remove(item);
            count--;
            return true;
        }

        public bool ContainsKey(TKey key)
        {
            var index = key.GetHashCode() % size;
            var list = hashItems[index];
            if (list == null)
            {
                return false;
            }

            foreach (var hashItem in list)
            {
                if (Equals(hashItem.Key, key))
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckForResize()
        {
            if (count < size && count * 2 > size)
            {                
                return;
            }
            
            Resize();
        }

        private void Resize()
        {
            List<HashItem>[] temp = hashItems;

            if (count > size)
            {
                size *= 2;
            }
            else
            {
                size /= 2;
            }

            hashItems = new List<HashItem>[size];

            for (int i = 0; i < temp.Length; i++)
            {
                var list = temp[i];
                if (list == null)
                {
                    continue;
                }

                foreach (var item in list)
                {
                    Put(item.Key, item.Value);
                }
            }
        }
    }
}
