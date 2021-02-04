using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

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
            => Properties.Resources.经卦卦名对照;

        /// <summary>
        /// Get a trigram from its name.
        /// </summary>
        /// <param name="卦名">The name.</param>
        /// <param name="result">The trigram.</param>
        /// <returns>A value indicates whether the trigram has been found or not.</returns>
        public static bool 获取经卦(char 卦名, [NotNullWhen(true)] out 经卦? result)
        {
            var all = Properties.Resources.经卦卦名对照;
            var index = all.IndexOf(卦名);
            if (index == -1)
            {
                result = null;
                return false;
            }
            result = new 经卦(index, 卦名, 获取卦对应的自然现象(index), 获取卦画(index));
            return true;
        }

        /// <summary>
        /// Get a trigram from its painting.
        /// </summary>
        /// <param name="卦画">
        /// The painting.
        /// Its property <see cref="卦画.爻数"/> should be 3.
        /// </param>
        /// <param name="result">The trigram.</param>
        /// <returns>A value indicates whether the trigram has been found or not.</returns>
        /// <exception cref="ArgumentNullException"> <paramref name="卦画"/> is null.</exception>
        public static bool 获取经卦(卦画 卦画, [NotNullWhen(true)] out 经卦? result)
        {
            if (卦画 is null)
                throw new ArgumentNullException(nameof(卦画));
            if (卦画.爻数 != 3)
            {
                result = null;
                return false;
            }
            int index = default;
            using (var ms = new MemoryStream(Properties.Resources.经卦卦画对照))
            {
                var b = 卦画.ToByte();
                for (int i = 0; i < 8; i++)
                {
                    if (ms.ReadByte() == b)
                    {
                        index = i;
                        break;
                    }
                }
            }
            result = new 经卦(index, 获取卦名(index), 获取卦对应的自然现象(index), 卦画);
            return true;
        }
        internal static 经卦 获取经卦(int index)
        {
            return new 经卦(index, 获取卦名(index), 获取卦对应的自然现象(index), 获取卦画(index));
        }
        private static char 获取卦名(int index)
            => Properties.Resources.经卦卦名对照[index];
        private static char 获取卦对应的自然现象(int index)
            => Properties.Resources.经卦自然现象对照[index];
        private static 卦画 获取卦画(int index)
        {
            byte b;
            using (var ms = new MemoryStream(Properties.Resources.经卦卦画对照))
            {
                ms.Position = index;
                b = (byte)ms.ReadByte();
            }
            return 卦画.FromByte(b);
        }
    }
}
