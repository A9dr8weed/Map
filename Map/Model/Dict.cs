using System;
using System.Collections;
using System.Collections.Generic;

namespace Map.Model
{
    /// <summary>
    /// The dictionary is built on an array.
    /// </summary>
    /// <typeparam name="TKey"> Key type. </typeparam>
    /// <typeparam name="TValue"> Data type for the value. </typeparam>
    public class Dict<TKey, TValue> : IEnumerable
    {
        /// <summary>
        /// Size of array.
        /// </summary>
        private readonly int size = 10;

        /// <summary>
        /// Array of dictionary elements.
        /// </summary>
        private readonly Item<TKey, TValue>[] items;

        /// <summary>
        /// List of keys.
        /// </summary>
        public List<TKey> Keys = new List<TKey>();

        /// <summary>
        /// Number of items in the list.
        /// </summary>
        public int Count => items.Length;

        /// <summary>
        /// Consturctor with size setting.
        /// </summary>
        public Dict()
        {
            items = new Item<TKey, TValue>[size];
        }

        /// <summary>
        /// Add data to the dictionary.
        /// </summary>
        /// <param name="item"> The item to add as a key-value pair. </param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <c>null</c>.</exception>
        public void Add(Item<TKey, TValue> item)
        {
            // Check the input for correctness.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Received a hash (address in the array).
            int hash = GetHash(item.Key);

            // If the key is in the key list.
            if (Keys.Contains(item.Key))
            {
                Console.WriteLine($"Key {item.Key} even in the list.");

                return;
            }

            // If there is no element with such a key.
            if (items[hash] == null)
            {
                // Add the item key to the list of keys.
                Keys.Add(item.Key);
                // Add an element to the array.
                items[hash] = item;
            }
            else
            {
                bool placed = false;

                // Find the nearest empty cell (to the right of the key).
                for (int i = hash; i < size; i++)
                {
                    // If the next element is null.
                    if (items[i] == null)
                    {
                        // Add a key in the list of keys.
                        Keys.Add(item.Key);
                        // Add the value.
                        items[i] = item;
                        // Set the flag that the item is recorded and you do not need to start over.
                        placed = true;

                        // Exit the loop.
                        break;
                    }
                }

                // If false.
                if (!placed)
                {
                    // Start the loop from the beginning of the array to the hash.
                    for (int i = 0; i < hash; i++)
                    {
                        // If the cell is empty.
                        if (items[i] == null)
                        {
                            // Add the key to the list of keys.
                            Keys.Add(item.Key);
                            // Write the value to an array.
                            items[i] = item;
                            // Set the flag that the item is recorded and you do not need to start over.
                            placed = true;

                            // Exit the loop.
                            break;
                        }
                    }
                }

                // If false.
                if (!placed)
                {
                    throw new Exception("Dict is full");
                }
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

            // Get a hash (address in the array).
            int hash = GetHash(key);

            // If the key is in the key list.
            if (Keys.Contains(key))
            {
                Console.WriteLine($"Key {key} even in the list.");

                return;
            }

            // If there is no element with such a key.
            if (items[hash] == null)
            {
                // Add key to the key list.
                Keys.Add(key);

                // Create a new stored data item.
                items[hash] = new Item<TKey, TValue>(key, value)
                {
                    Key = key,
                    Value = value
                };
            }
            else
            {
                bool placed = false;

                // Find the nearest empty cell (to the right of our key).
                for (int i = hash; i < size; i++)
                {
                    // If the next element is null.
                    if (items[i] == null)
                    {
                        // Add a key to the key list.
                        Keys.Add(key);
                        // Write down the value.
                        items[i] = new Item<TKey, TValue>(key, value)
                        {
                            Key = key,
                            Value = value
                        };

                        // Set the flag that the item is saved and you do not need to start over.
                        placed = true;

                        // Exit the loop.
                        break;
                    }
                }

                // If false.
                if (!placed)
                {
                    // Start the loop from the beginning of the array to the hash.
                    for (int i = 0; i < hash; i++)
                    {
                        // If the cell is empty.
                        if (items[i] == null)
                        {
                            // Add a key to the key list.
                            Keys.Add(key);

                            // Write down the value.
                            items[i] = new Item<TKey, TValue>(key, value)
                            {
                                Key = key,
                                Value = value
                            };

                            // Set the flag that the item is saved and you do not need to start over.
                            placed = true;

                            // Exit the loop.
                            break;
                        }
                    }
                }

                // If false.
                if (!placed)
                {
                    throw new Exception("Dict is full");
                }
            }
        }

        /// <summary>
        /// Remove data from the collection by key.
        /// </summary>
        /// <param name="key"> The key by which the data is available. </param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public void Remove(TKey key)
        {
            // Check the input for correctness.
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Get a hash on the key.
            int hash = GetHash(key);

            // If the key is not in the list of keys then stop execution.
            if (!Keys.Contains(key))
            {
                Console.WriteLine($"Key {key} does not exist. Nothing to delete.");

                return;
            }

            // If an element with such a hash is null, ie it has already been deleted.
            if (items[hash] == null)
            {
                // Go through the array.
                for (int i = 0; i < size; i++)
                {
                    // If the item is not null and the keys match.
                    if (items[i]?.Key.Equals(key) == true)
                    {
                        // Delete item.
                        items[i] = null;
                        // Remove the key from the list.
                        Keys.Remove(key);

                        return;
                    }
                }
            }

            // If the key hash matches.
            if (items[hash].Key.Equals(key))
            {
                // Delete the item.
                items[hash] = null;
                // Remove the key from the list.
                Keys.Remove(key);
            }
            else
            {
                // Find the nearest empty cell (to the right of our key).
                for (int i = hash; i < size; i++)
                {
                    // If the next element is null stop execution.
                    if (items[i] == null)
                    {
                        return;
                    }

                    // If the same item.
                    if (items[i].Key.Equals(key))
                    {
                        // Delete item.
                        items[i] = null;
                        // Delete key.
                        Keys.Remove(key);

                        return;
                    }
                }

                // Start the loop from the beginning of the array to the hash.
                for (int i = 0; i < hash; i++)
                {
                    // If the next element is null stop execution.
                    if (items[i] == null)
                    {
                        return;
                    }

                    // If the same item.
                    if (items[i].Key.Equals(key))
                    {
                        // Delete item.
                        items[i] = null;
                        // Delete key.
                        Keys.Remove(key);

                        return;
                    }
                }
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
            // Проверяем входные данные на корректность.
            if (key == null || newValue == null)
            {
                throw new ArgumentNullException(nameof(key), nameof(newValue));
            }

            // Get a hash on the key.
            int hash = GetHash(key);

            // If there is no such key.
            if (!Keys.Contains(key))
            {
                Console.WriteLine($"Dictionary does not contain value with key {key}. Therefore you cannot change the values.");

                return;
            }

            // If the hash is equal to null.
            if (items[hash] == null)
            {
                // Go through the whole array.
                foreach (Item<TKey, TValue> item in items)
                {
                    // If you find a match on the key.
                    if (item.Key.Equals(key))
                    {
                        Console.WriteLine($"Changed from {item.Value} to {newValue}.");

                        // Change the stored value to a new one.
                        item.Value = newValue;
                    }
                }
            }

            // If the key hash matches.
            if (items[hash].Key.Equals(key))
            {
                Console.WriteLine($"Changed from {items[hash].Value} to {newValue}.");

                // Change the stored value to a new one.
                items[hash].Value = newValue;
            }
            else
            {
                // Find the nearest empty cell (to the right of our key).
                for (int i = hash; i < size; i++)
                {
                    // If the next element is null.
                    if (items[i] == null)
                    {
                        return;
                    }

                    // If the same item.
                    if (items[i].Key.Equals(key))
                    {
                        Console.WriteLine($"Changed from {items[i].Value} to {newValue}.");

                        // Change the stored value to a new one.
                        items[i].Value = newValue;
                    }
                }

                // Start the loop from the beginning of the array to the hash.
                for (int i = 0; i < hash; i++)
                {
                    // If the next element is null stop execution.
                    if (items[i] == null)
                    {
                        return;
                    }

                    // If the same item.
                    if (items[i].Key.Equals(key))
                    {
                        Console.WriteLine($"Changed from {items[i].Value} to {newValue}.");

                        // Change the stored value to a new one.
                        items[i].Value = newValue;
                    }
                }
            }
        }

        /// <summary>
        /// Get value by key.
        /// </summary>
        /// <param name="key"> The key by which the stored data is available. </param>
        /// <returns> The value of the stored data. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public TValue Search(TKey key)
        {
            // Проверяем входные данные на корректность.
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // Get a hash on the key.
            int hash = GetHash(key);

            // If there is no such key.
            if (!Keys.Contains(key))
            {
                Console.WriteLine($"Dictionary does not contain value with key {key}.");

                return default;
            }

            // If the hash is equal to null.
            if (items[hash] == null)
            {
                // Go through the whole array.
                foreach (Item<TKey, TValue> item in items)
                {
                    // If you find a match on the key.
                    if (item.Key.Equals(key))
                    {
                        // Return value.
                        return item.Value;
                    }
                }
            }

            // If the key hash matches.
            if (items[hash].Key.Equals(key))
            {
                // Return value.
                return items[hash].Value;
            }
            else
            {
                // Find the nearest empty cell (to the right of our key).
                for (int i = hash; i < size; i++)
                {
                    // If the next element is null stop execution.
                    if (items[i] == null)
                    {
                        return default;
                    }

                    // If the same item.
                    if (items[i].Key.Equals(key))
                    {
                        // Return value.
                        return items[i].Value;
                    }
                }

                // Start the loop from the beginning of the array to the hash.
                for (int i = 0; i < hash; i++)
                {
                    // If the next element is null stop execution.
                    if (items[i] == null)
                    {
                        return default;
                    }

                    // If the same item.
                    if (items[i].Key.Equals(key))
                    {
                        // Return value.
                        return items[i].Value;
                    }
                }
            }

            return default;
        }

        /// <summary>
        /// Hash function.
        /// </summary>
        /// <param name="key"> The key to determine the hash. </param>
        /// <returns> The hash code key, given the size of the array. </returns>
        private int GetHash(TKey key) => key.GetHashCode() % size;

        /// <summary>
        /// Recalculation of the collection.
        /// </summary>
        /// <returns> Items that are not null. </returns>
        public IEnumerator GetEnumerator()
        {
            foreach (Item<TKey, TValue> item in items)
            {
                if (item != null)
                {
                    yield return item;
                }
            }
        }
    }
}