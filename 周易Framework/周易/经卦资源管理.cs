using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace 周易
{
    public sealed partial class 经卦
    {
        /// <summary>
        /// Get the names of all trigrams.
        /// This is arranged according to the number of innate trigrams -- 
        /// 1: Ch'ien (Heaven);
        /// 2: Tui (Lake);
        /// 3: Li (Fire);
        /// 4: Chên (Thunder);
        /// 5: Sun (Wind);
        /// 6: K'an (Water);
        /// 7: Kên (Mountain);
        /// 8: K'un (Earth).
        /// </summary>
        public static IEnumerable<char> 全部经卦卦名
            => Resource.经卦卦名对照;

        /// <summary>
        /// Get a trigram from its name.
        /// </summary>
        /// <param name="卦名">The name.</param>
        /// <returns>The trigram.</returns>
        public static 经卦 获取经卦(char 卦名)
        {
            var all = Resource.经卦卦名对照;
            var intIndex = all.IndexOf(卦名);
            if (intIndex == -1)
            {
                throw new ArgumentOutOfRangeException(nameof(卦名));
            }
            byte index = (byte)intIndex;
            return new 经卦(index, 卦名, 获取卦对应的自然现象(index), 获取卦画(index));
        }
        /// <summary>
        /// Get a trigram from its painting.
        /// </summary>
        /// <param name="卦画">The painting.</param>
        /// <returns>The trigram.</returns>
        public static 经卦 获取经卦(卦画 卦画)
        {
            byte? index = null;
            using (var ms = new MemoryStream(Resource.经卦卦画对照))
            {
                var b = 卦画.ToByte();
                for (byte i = 0; i < 8; i++)
                {
                    if (ms.ReadByte() == b)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if (!index.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(卦画));
            }
            return new 经卦(
                index.Value, 获取卦名(index.Value), 获取卦对应的自然现象(index.Value), 卦画);
        }
        internal static 经卦 获取经卦(byte index)
        {
            return new 经卦(index, 获取卦名(index), 获取卦对应的自然现象(index), 获取卦画(index));
        }
        private static char 获取卦名(byte index)
            => Resource.经卦卦名对照[index];
        private static char 获取卦对应的自然现象(byte index)
            => Resource.经卦自然现象对照[index];
        private static 卦画 获取卦画(byte index)
        {
            byte b;
            using (var ms = new MemoryStream(Resource.经卦卦画对照))
            {
                ms.Position = index;
                b = (byte)ms.ReadByte();
            }
            return 卦画.FromByte(b);
        }
    }
}
