﻿using System.Collections.Generic;

namespace KissU.Core.CPlatform.Diagnostics
{
    /// <summary>
    /// Interface ICarrier Header Collection
    /// Implements the <see cref="IEnumerable{KeyValuePair{string, string}}" />
    /// </summary>
    /// <seealso cref="IEnumerable{KeyValuePair{string, string}}" />
    public interface ICarrierHeaderCollection : IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Add(string key, string value);
    }
}