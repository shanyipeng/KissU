﻿using System;
using System.Linq;
using System.Threading.Tasks;
using KissU.Core.CPlatform.Address;
using KissU.Core.CPlatform.Runtime.Client.Address.Resolvers.Implementation.Selectors;

namespace KissU.Core.Zookeeper.Internal.Cluster.Implementation.Selectors.Implementation
{
    /// <summary>
    /// ZookeeperRandomAddressSelector.
    /// Implements the
    /// <see
    ///     cref="KissU.Core.Zookeeper.Internal.Cluster.Implementation.Selectors.Implementation.ZookeeperAddressSelectorBase" />
    /// </summary>
    /// <seealso
    ///     cref="KissU.Core.Zookeeper.Internal.Cluster.Implementation.Selectors.Implementation.ZookeeperAddressSelectorBase" />
    public class ZookeeperRandomAddressSelector : ZookeeperAddressSelectorBase
    {
        #region Overrides of AddressSelectorBase

        /// <summary>
        /// 选择一个地址。
        /// </summary>
        /// <param name="context">地址选择上下文。</param>
        /// <returns>地址模型。</returns>
        protected override ValueTask<AddressModel> SelectAsync(AddressSelectContext context)
        {
            var address = context.Address.ToArray();
            var length = address.Length;

            var index = _generate(0, length);
            return new ValueTask<AddressModel>(address[index]);
        }

        #endregion Overrides of AddressSelectorBase

        #region Field

        private readonly Func<int, int, int> _generate;
        private readonly Random _random;

        #endregion Field

        #region Constructor

        /// <summary>
        /// 初始化一个以Random生成随机数的随机地址选择器。
        /// </summary>
        public ZookeeperRandomAddressSelector()
        {
            _random = new Random();
            _generate = (min, max) => _random.Next(min, max);
        }

        /// <summary>
        /// 初始化一个自定义的随机地址选择器。
        /// </summary>
        /// <param name="generate">随机数生成委托，第一个参数为最小值，第二个参数为最大值（不可以超过该值）。</param>
        /// <exception cref="ArgumentNullException">generate</exception>
        public ZookeeperRandomAddressSelector(Func<int, int, int> generate)
        {
            if (generate == null)
                throw new ArgumentNullException(nameof(generate));
            _generate = generate;
        }

        #endregion Constructor
    }
}