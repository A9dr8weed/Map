using System;

namespace Map.Model
{
    /// <summary>
    /// Dictionary item.
    /// </summary>
    /// <typeparam name="TKey"> Key type. </typeparam>
    /// <typeparam name="TValue"> Data type for the value. </typeparam>
    public class Item<TKey, TValue>
    {
        /// <summary>
        /// Key.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Create a new instance of the Item class.
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <param name="value"> Value. </param>
        public Item(TKey key, TValue value)
        {
            // Check the input data for correctness.
            if (key == null || value == null)
            {
                throw new ArgumentNullException(nameof(key), nameof(value));
            }

            Key = key;
            Value = value;
        }

        /// <summary>
        /// Casting an object to a string.
        /// </summary>
        /// <returns> Stored value. </returns>
        public override string ToString()
        {
            return $"{Key} - {Value}";
        }
    }
}