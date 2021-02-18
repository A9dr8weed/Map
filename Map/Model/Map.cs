using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Map.Model
{
    /// <summary>
    /// Implementation of the map on the list.
    /// </summary>
    /// <typeparam name="TKey"> Key data type. </typeparam>
    /// <typeparam name="TValue"> Data type for the value. </typeparam>
    public class Map<TKey, TValue> : IEnumerable
    {
        /// <summary>
        /// Collection of elements.
        /// </summary>
        private readonly List<Item<TKey, TValue>> items = new List<Item<TKey, TValue>>();

        /// <summary>
        /// List for storing keys.
        /// </summary>
        public IReadOnlyList<TKey> keys => items.ConvertAll(x => x.Key);

        /// <summary>
        /// Number of items in the list.
        /// </summary>
        public int Count => items.Count;

        /// <summary>
        /// Add data to the dictionary.
        /// </summary>
        /// <param name="item"> The item to add as a key-value pair. </param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <c>null</c>.</exception>
        public void Add(Item<TKey, TValue> item)
        {
            // Check the input data for correctness.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // If there is no such key then we add an element to a collection.
            if (!keys.Contains(item.Key))
            {
                items.Add(item);
            }
            else
            {
                Console.WriteLine($"The dictionary already contains a value with a key {item.Key}. So you can't add {item.Value}.");
            }
        }

        /// <summary>
        /// Add data to the dictionary.
        /// </summary>
        /// <param name="key"> The key by which the stored data is available. </param>
        /// <param name="value"> The data stored in the dictionary. </param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(TKey key, TValue value)
        {
            // Check the input for correctness.
            if (key == null || value == null)
            {
                throw new ArgumentNullException(nameof(key), nameof(value));
            }

            if (!keys.Contains(key))
            {
                // Create a new stored data item.
                Item<TKey, TValue> item = new Item<TKey, TValue>(key, value)
                {
                    Key = key,
                    Value = value
                };

                // Add data to the collection.
                items.Add(item);
            }
            else
            {
                Console.WriteLine($"The dictionary already contains a value with a key {key}. So you can't add {value}.");
            }
        }

        /// <summary>
        /// Change stored data by key.
        /// </summary>
        /// <param name="key"> The key by which the stored data is available. </param>
        /// <param name="newValue"> New data value. </param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(TKey key, TValue newValue)
        {
            // Check the input data for correctness.
            if (key == null || newValue == null)
            {
                throw new ArgumentNullException(nameof(key), nameof(newValue));
            }

            // Get an item by key.
            Item<TKey, TValue> item = items.SingleOrDefault(i => i.Key.Equals(key));

            // If the data is found by key.
            if (item != null)
            {
                Console.WriteLine($"Changed from {item.Value} to {newValue}.");

                // Change the stored value to a new one.
                item.Value = newValue;
            }
            else
            {
                Console.WriteLine($"Dictionary does not contain value with key {key}. Therefore you cannot change the values.");
            }
        }

        /// <summary>
        /// Remove data from the collection by key.
        /// </summary>
        /// <param name="key"> The key by which the data is available. </param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public void Remove(TKey key)
        {
            // Check the input data for correctness.
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Get a data item from a collection by key.
            Item<TKey, TValue> item = items.SingleOrDefault(x => x.Key.Equals(key));

            // If the data is found by key.
            if (item != null)
            {
                // Remove them from the collection.
                items.Remove(item);
            }
            else
            {
                Console.WriteLine($"No such key exists {key}. Unable to delete.");
            }
        }

        /// <summary>
        /// Get value by key.
        /// </summary>
        /// <param name="key"> The key by which the stored data is available. </param>
        /// <returns> The value of the stored data. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"></exception>
        public TValue Search(TKey key)
        {
            // Check the input data for correctness.
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Get the value on the key and return it.
            return (items.SingleOrDefault(i => i.Key.Equals(key)) ?? throw new ArgumentException($"Dictionary does not contain value with key {key}.", nameof(key))).Value;
        }

        /// <summary>
        /// Recalculation of the collection.
        /// </summary>
        /// <returns> Items. </returns>
        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}